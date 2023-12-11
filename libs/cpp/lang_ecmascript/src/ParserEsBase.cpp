#include "lang_ecmascript/ParserEsBase.hpp"
#include "ParserEs.h"

using namespace antlr4;
using namespace cl::lang::ecmascript;

ParserEsBase::ParserEsBase(TokenStream *input)
        : Parser(input) {
}

size_t
ParserEsBase::getNextTokenType() {
    auto token = _input->LT(1);

    if (token) {
        return token->getType();
    }

    return 0;
}

size_t
ParserEsBase::getPrevTokenType() {
    auto token = _input->LT(-1);

    if (token) {
        return token->getType();
    }

    return 0;
}

bool
ParserEsBase::nextTextEq(const char *str) {
    auto token = _input->LT(1);

    if (token) {
        return token->getText() == str;
    }

    return false;
}

bool
ParserEsBase::prevTextEq(const char *str) {
    auto token = _input->LT(-1);

    if (token) {
        return token->getText() == str;
    }

    return false;
}

bool
ParserEsBase::isValidExpressionStatement() {
    auto token = _input->LT(1);

    if (token) {
        auto text = token->getText();
        return text != "{" && text != "function" && text != "class";
        // TODO: also async [no LineTerminator here] function, let [
    }

    return false;
}
