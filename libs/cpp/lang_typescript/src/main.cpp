#include <iostream>
#include <memory>

#include "core/language.h"

#include "antlr4-runtime.h"
#include "LexerTs.h"
#include "ParserTs.h"

using namespace cl::lang::typescript;
using namespace antlr4;

CL_RESULT
language_init() {
  ANTLRInputStream input(
    u8"export interface IVector {"
    u8"data: byte[];"
    u8"length: size;"
    u8"}"
    );
  LexerTs lexer(&input);
  CommonTokenStream tokens(&lexer);

  tokens.fill();
  for (auto token : tokens.getTokens()) {
    auto common_token = dynamic_cast<const CommonToken*>(token);
    if (common_token != nullptr) {
      std::cout << common_token->toString(&lexer) << std::endl;
    }
    else {
      std::cout << token->toString() << std::endl;
    }
  }

  ParserTs parser(&tokens);
  tree::ParseTree* tree = parser.program();

  std::cout << tree->toStringTree(&parser, true) << std::endl << std::endl;
  return 0;
}
