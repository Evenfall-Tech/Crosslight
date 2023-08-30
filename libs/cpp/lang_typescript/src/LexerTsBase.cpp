#include "lang_typescript/LexerTsBase.hpp"
#include "LexerTs.h"

using namespace antlr4;
using namespace cl::lang::typescript;

LexerTsBase::LexerTsBase(CharStream *input) : Lexer(input)
{
}

bool LexerTsBase::getStrictDefault()
{
    return useStrictDefault;
}

void LexerTsBase::setUseStrictDefault(bool value)
{
    useStrictDefault = value;
    useStrictCurrent = value;
}

bool LexerTsBase::IsStrictMode()
{
    return useStrictCurrent;
}

std::unique_ptr<antlr4::Token> LexerTsBase::nextToken()
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

void LexerTsBase::ProcessOpenBrace()
{
    bracesDepth++;
    useStrictCurrent = scopeStrictModes.size() > 0 && scopeStrictModes.top() ? true : useStrictDefault;
    scopeStrictModes.push(useStrictCurrent);
}

void LexerTsBase::ProcessCloseBrace()
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

void LexerTsBase::ProcessStringLiteral()
{
    if (lastToken || lastTokenType == LexerTs::OpenBrace)
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

bool LexerTsBase::IsRegexPossible()
{
    if (lastToken)
    {
        // No token has been produced yet: at the start of the input,
        // no division is possible, so a regex literal _is_ possible.
        return true;
    }

    switch (lastTokenType)
    {
    case LexerTs::Identifier:
    case LexerTs::NullLiteral:
    case LexerTs::BooleanLiteral:
    case LexerTs::This:
    case LexerTs::CloseBracket:
    case LexerTs::CloseParen:
    case LexerTs::OctalIntegerLiteral:
    case LexerTs::DecimalLiteral:
    case LexerTs::HexIntegerLiteral:
    case LexerTs::StringLiteral:
    case LexerTs::PlusPlus:
    case LexerTs::MinusMinus:
        // After any of the tokens above, no regex literal can follow.
        return false;
    default:
        // In all other cases, a regex literal _is_ possible.
        return true;
    }
}

void LexerTsBase::StartTemplateString() { bracesDepth = 0; }

bool LexerTsBase::IsInTemplateString() { return templateDepth > 0 && bracesDepth == 0; }

void LexerTsBase::IncreaseTemplateDepth() { ++templateDepth; }

void LexerTsBase::DecreaseTemplateDepth() { --templateDepth; }