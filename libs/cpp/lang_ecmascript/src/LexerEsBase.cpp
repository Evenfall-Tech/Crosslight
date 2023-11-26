#include "lang_ecmascript/LexerEsBase.hpp"
#include "LexerEs.h"

using namespace antlr4;
using namespace cl::lang::ecmascript;

LexerEsBase::LexerEsBase(CharStream *input)
    : Lexer(input) {}

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
    if (lastToken || lastTokenType == LexerEs::OpenBrace)
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
        case LexerEs::Identifier:
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
            return false;
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
