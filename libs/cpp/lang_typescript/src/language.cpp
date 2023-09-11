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

const cl_node*
language::transform_input(const struct cl_resource* resource) const {
    if (resource == 0) {
        return 0;
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

            return 0;
        }
    }

    return 0;
}

const struct cl_resource*
language::transform_output(const struct cl_node* node) const {
    return 0;
}

const struct cl_node*
language::transform_modify(const struct cl_node* node) const {
    return 0;
}

const std::set<const char*>&
language::resource_types_input() const {
    return _types_input;
}

const std::set<const char*>&
language::resource_types_output() const {
    return _types_output;
}
