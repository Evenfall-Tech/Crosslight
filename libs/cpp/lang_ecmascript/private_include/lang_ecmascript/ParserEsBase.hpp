#pragma once

#undef ANTLR4CPP_EXPORTS
#include "antlr4-runtime.h"

namespace cl::lang::ecmascript {

    class ParserEsBase : public antlr4::Parser {
    public:
        ParserEsBase(antlr4::TokenStream *input);

        size_t getNextTokenType();
        size_t getPrevTokenType();
        bool nextTextEq(const char* str);
        bool prevTextEq(const char* str);
        bool isValidExpressionStatement();
    };

} // namespace cl::lang::ecmascript
