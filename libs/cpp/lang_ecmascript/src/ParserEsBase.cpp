#include "lang_ecmascript/ParserEsBase.hpp"
#include "ParserEs.h"

using namespace antlr4;
using namespace cl::lang::ecmascript;

ParserEsBase::ParserEsBase(TokenStream *input)
        : Parser(input) {
}