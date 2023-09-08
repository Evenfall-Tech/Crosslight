/**
 * @file ParserTsBase.h
 * @brief Grammars written for ANTLR v4.
 * @link https://github.com/antlr/grammars-v4/blob/443916c7460a8f69e66666873c0088d2b3ec64c8/javascript/typescript/Cpp/TypeScriptParserBase.h
 * 
 * @author Andrii Artiushok <loony.developer@gmail.com>
 * @copyright (c) 2019 Andrii Artiushok
 *
 * This library is released under MIT <https://opensource.org/license/mit/> license
 */

#pragma once

#include "antlr4-runtime.h"

namespace cl::lang::typescript {

class ParserTsBase : public antlr4::Parser {
public:
    ParserTsBase(antlr4::TokenStream *input);
    bool p(std::string str);
    bool prev(std::string str);
    bool n(std::string str);
    bool next(std::string str);
    bool notLineTerminator();
    bool notOpenBraceAndNotFunction();
    bool closeBrace();
    bool here(int type);
    bool lineTerminatorAhead();
};

} // namespace cl::lang::typescript
