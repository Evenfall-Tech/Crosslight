#include "lang_typescript/ParserTsBase.hpp"
#include "ParserTs.h"

using namespace antlr4;
using namespace cl::lang::typescript;

ParserTsBase::ParserTsBase(TokenStream *input)
    : Parser(input) {
}

bool
ParserTsBase::p(std::string str) {
    return prev(str);
}

bool
ParserTsBase::prev(std::string str) {
    return _input->LT(-1)->getText() == str;
}

bool
ParserTsBase::n(std::string str) {
    return next(str);
}

bool
ParserTsBase::next(std::string str) {
    return _input->LT(1)->getText() == str;
}

bool
ParserTsBase::notLineTerminator() {
    return !here(ParserTs::LineTerminator);
}

bool
ParserTsBase::notOpenBraceAndNotFunction() {
    int nextTokenType = _input->LT(1)->getType();
    return nextTokenType != ParserTs::OpenBrace && nextTokenType != ParserTs::Function_;

}

bool
ParserTsBase::closeBrace() {
    return _input->LT(1)->getType() == ParserTs::CloseBrace;
}

bool
ParserTsBase::here(int type) {
    // Get the token ahead of the current index.
    int possibleIndexEosToken = this->getCurrentToken()->getTokenIndex() - 1;
    auto ahead = _input->get(possibleIndexEosToken);

    // Check if the token resides on the HIDDEN channel and if it's of the
    // provided type.
    return (ahead->getChannel() == Lexer::HIDDEN) && (ahead->getType() == type);
}

bool
ParserTsBase::lineTerminatorAhead() {
    // Get the token ahead of the current index.
    int possibleIndexEosToken = this->getCurrentToken()->getTokenIndex() - 1;
    auto ahead = _input->get(possibleIndexEosToken);

    if (ahead->getChannel() != Lexer::HIDDEN) {
        // We're only interested in tokens on the HIDDEN channel.
        return false;
    }

    if (ahead->getType() == ParserTs::LineTerminator) {
        // There is definitely a line terminator ahead.
        return true;
    }

    if (ahead->getType() == ParserTs::WhiteSpaces) {
        // Get the token ahead of the current whitespaces.
        possibleIndexEosToken = this->getCurrentToken()->getTokenIndex() - 2;
        ahead = _input->get(possibleIndexEosToken);
    }

    // Get the token's text and type.
    std::string text = ahead->getText();
    int type = ahead->getType();

    // Check if the token is, or contains a line terminator.
    return
        (
            type == ParserTs::MultiLineComment &&
            (text.find("\r") != std::string::npos || text.find("\n") != std::string::npos)
        ) ||
        (type == ParserTs::LineTerminator);
}