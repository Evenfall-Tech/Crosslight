#include "lang_typescript/language.hpp"
#include <string>
#include <cstring>
#include "antlr4-runtime.h"
#include "LexerTs.h"
#include "ParserTs.h"
#include "core/resource.h"
#include "core/node.h"

using namespace cl::lang::typescript;
using namespace antlr4;

language::language()
    : _types_input({"text/plain"}),
    _types_output({}) {
}

std::unique_ptr<cl_node>
language::parse_source(const std::unique_ptr<cl_resource>& resource) {
    if (!resource) {
        return {};
    }

    for (auto type : _types_input) {
        if (strcmp(type, resource->content_type) == 0) {
            // Given type is supported.

            ANTLRInputStream input((const char*)resource->content);
            LexerTs lexer(&input);
            CommonTokenStream tokens(&lexer);

            tokens.fill();

            for (auto token : tokens.getTokens()) {
                auto common_token = dynamic_cast<const CommonToken*>(token);

                if (common_token != nullptr) {
                    //std::cout << common_token->toString(&lexer) << std::endl;
                }
                else {
                    //std::cout << token->toString() << std::endl;
                }
            }

            ParserTs parser(&tokens);
            tree::ParseTree* tree = parser.program();

            //std::cout << tree->toStringTree(&parser, true) << std::endl << std::endl;

            return {};
        }
    }

    return {};
}

const std::set<const char*>&
language::resource_types_input() {
    return _types_input;
}

const std::set<const char*>&
language::resource_types_output() {
    return _types_output;
}
