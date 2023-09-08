/**
 * @file LexerTsBase.h
 * @brief Grammars written for ANTLR v4.
 * @link https://github.com/antlr/grammars-v4/blob/443916c7460a8f69e66666873c0088d2b3ec64c8/javascript/typescript/Cpp/TypeScriptLexerBase.h
 * 
 * @author Andrii Artiushok <loony.developer@gmail.com>
 * @copyright (c) 2019 Andrii Artiushok
 *
 * This library is released under MIT <https://opensource.org/license/mit/> license
 */

#pragma once

#include <stack>
#include "antlr4-runtime.h"

namespace cl::lang::typescript {

class LexerTsBase : public antlr4::Lexer
{
public:
    LexerTsBase(antlr4::CharStream *input);

    std::stack<bool> scopeStrictModes;

    bool lastToken = false;
    size_t lastTokenType = 0;

    bool useStrictDefault = false;
    bool useStrictCurrent = false;

    bool getStrictDefault();
    void setUseStrictDefault(bool value);
    bool IsStrictMode();
    virtual std::unique_ptr<antlr4::Token> nextToken() override;
    void ProcessOpenBrace();
    void ProcessCloseBrace();
    void ProcessStringLiteral();
    bool IsRegexPossible();
    void StartTemplateString();
    bool IsInTemplateString();
    void IncreaseTemplateDepth();
    void DecreaseTemplateDepth();

private:
    int templateDepth = 0;
    int bracesDepth = 0;
};

} // namespace cl::lang::typescript
