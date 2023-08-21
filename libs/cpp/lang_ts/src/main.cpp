#include <iostream>

#include "antlr4-runtime.h"
#include "LexerTs.h"
//#include "TParser.h"

using namespace cl::lang::typescript;
using namespace antlr4;

int main(int , const char **) {
  ANTLRInputStream input(u8"//hello world");
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

  /*TParser parser(&tokens);
  tree::ParseTree* tree = parser.main();

  std::cout << tree->toStringTree(&parser, true) << std::endl << std::endl;*/

  return 0;
}
