#pragma once

#undef ANTLR4CPP_EXPORTS
#include "antlr4-runtime.h"

namespace cl::lang::ecmascript {

    class ParserEsBase : public antlr4::Parser {
    public:
        ParserEsBase(antlr4::TokenStream *input);
    };

} // namespace cl::lang::ecmascript
