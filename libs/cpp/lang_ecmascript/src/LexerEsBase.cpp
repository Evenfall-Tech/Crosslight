#include "lang_ecmascript/LexerEsBase.hpp"
#include <cstring>
#include <cstdlib>
#include "LexerEs.h"

using namespace antlr4;
using namespace cl::lang::ecmascript;

LexerEsBase::LexerEsBase(CharStream *input)
    : Lexer(input) {}

bool
LexerEsBase::isNextCharacter(char fromInclusive, char toInclusive) {
    auto* stream = getInputStream();

    if (!stream) {
        return false;
    }

    auto index = stream->index();
    auto size = stream->size();
    if (index >= size) {
        return false;
    }

    auto text = stream->getText(antlr4::misc::Interval{index, index});

    if (text.size() >= 1) {
        auto symbol = text.back();
        auto result = symbol >= fromInclusive && symbol <= toInclusive;

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isNextHexDigit() {
    auto* stream = getInputStream();

    if (!stream) {
        return false;
    }

    auto index = stream->index();
    auto size = stream->size();
    if (index >= size) {
        return false;
    }

    auto text = stream->getText(antlr4::misc::Interval{index, index});

    if (text.size() >= 1) {
        auto symbol = text.back();
        auto result =
            (symbol >= '0' && symbol <= '9') ||
            (symbol >= 'a' && symbol <= 'f') ||
            (symbol >= 'A' && symbol <= 'F');

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isLastCharacter(const char* str) {
    auto text = getText();
    auto str_length = strlen(str);

    if (text.size() >= str_length) {
        auto result = (0 == text.compare(
            text.size() - str_length,
            str_length,
            str));

        return result;
    }
    else {
        return false;
    }
}

uint64_t
LexerEsBase::lastHexDigitsMV() {
    auto text = getText();

    if (text.size() >= 1) {
        text.erase (std::remove(text.begin(), text.end(), '_'), text.end());
        auto result = std::strtoul(text.c_str(), nullptr, 16);

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isLastEscapeCharacter() {
    auto text = getText();

    if (text.size() >= 1) {
        auto symbol = text.back();
        auto result =
            symbol == 'x' || // EscapeCharacter
            symbol == 'u' ||
            (symbol >= '0' && symbol <= '9') || // DecimalDigit
            symbol == '\'' || // SingleEscapeCharacter
            symbol == '"' ||
            symbol == '\\' ||
            strchr("bfnrtv", symbol) != nullptr;

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isLastCharacterOneOf(const char *str) {
    auto text = getText();

    if (text.size() >= 1) {
        auto symbol = text.back();
        auto result = strchr(str, symbol) != nullptr;

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isLastCharacter(char fromInclusive, char toInclusive) {
    auto text = getText();

    if (text.size() >= 1) {
        auto symbol = text.back();
        auto result = symbol >= fromInclusive && symbol <= toInclusive;

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isNextCharacter(const char* str) {
    auto* stream = getInputStream();
    auto str_length = strlen(str);

    if (!stream || !str_length) {
        return false;
    }

    auto index = stream->index();
    auto end = index + str_length - 1; // Inclusive end of seeked substring.
    auto size = stream->size();
    if (end >= size) {
        return false;
    }

    auto text = stream->getText(antlr4::misc::Interval{index, end});

    if (text.size() >= str_length) {
        auto result = (0 == text.compare(
                text.size() - str_length,
                str_length,
                str));

        return result;
    }
    else {
        return false;
    }
}

bool
LexerEsBase::isLastLineTerminator() {
    // Taken from LexerEs.g4
    auto result =
        isLastCharacter("\u000A") // Line Feed (LF)
        || isLastCharacter("\u000D") // Carriage Return (CR)
        || isLastCharacter("\u2028") // Line Separator (LS)
        || isLastCharacter("\u2029") // Paragraph Separator (PS)
        ;

    return result;
}

bool
LexerEsBase::isTokenType(size_t type) {
    auto token = getToken();

    if (!token) {
        // No token has been produced yet: at the start of the input.
        return false;
    }

    return token->getType() == type;
}

bool
LexerEsBase::getStrictDefault()
{
    return useStrictDefault;
}

void
LexerEsBase::setUseStrictDefault(bool value)
{
    useStrictDefault = value;
    useStrictCurrent = value;
}

bool
LexerEsBase::IsStrictMode()
{
    return useStrictCurrent;
}

std::unique_ptr<antlr4::Token>
LexerEsBase::nextToken()
{
    auto next = Lexer::nextToken();

    if (next->getChannel() == Token::DEFAULT_CHANNEL)
    {
        // Keep track of the last token on the default channel.
        lastToken = true;
        lastTokenType = next->getType();
    }

    return next;
}

void
LexerEsBase::ProcessOpenBrace()
{
    bracesDepth++;
    useStrictCurrent = scopeStrictModes.size() > 0 && scopeStrictModes.top() ? true : useStrictDefault;
    scopeStrictModes.push(useStrictCurrent);
}

void
LexerEsBase::ProcessCloseBrace()
{
    bracesDepth--;

    if (scopeStrictModes.size() > 0)
    {
        useStrictCurrent = scopeStrictModes.top();
        scopeStrictModes.pop();
    }
    else
    {
        useStrictCurrent = useStrictDefault;
    }
}

void
LexerEsBase::ProcessStringLiteral()
{
    if (lastToken/* || lastTokenType == LexerEs::OpenBrace*/)
    {
        std::string text = getText();
        if (text == "\"use strict\"" || text == "'use strict'")
        {
            if (scopeStrictModes.size() > 0)
                scopeStrictModes.pop();
            useStrictCurrent = true;
            scopeStrictModes.push(useStrictCurrent);
        }
    }
}

bool
LexerEsBase::IsRegexPossible()
{
    if (lastToken)
    {
        // No token has been produced yet: at the start of the input,
        // no division is possible, so a regex literal _is_ possible.
        return true;
    }

    switch (lastTokenType)
    {
        /*case LexerEs::Identifier:
        case LexerEs::NullLiteral:
        case LexerEs::BooleanLiteral:
        case LexerEs::This:
        case LexerEs::CloseBracket:
        case LexerEs::CloseParen:
        case LexerEs::OctalIntegerLiteral:
        case LexerEs::DecimalLiteral:
        case LexerEs::HexIntegerLiteral:
        case LexerEs::StringLiteral:
        case LexerEs::PlusPlus:
        case LexerEs::MinusMinus:
            // After any of the tokens above, no regex literal can follow.
            return false;*/
        default:
            // In all other cases, a regex literal _is_ possible.
            return true;
    }
}

void
LexerEsBase::StartTemplateString() { bracesDepth = 0; }

bool
LexerEsBase::IsInTemplateString() { return templateDepth > 0 && bracesDepth == 0; }

void
LexerEsBase::IncreaseTemplateDepth() { ++templateDepth; }

void
LexerEsBase::DecreaseTemplateDepth() { --templateDepth; }
