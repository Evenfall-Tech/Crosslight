#include "lang_typescript/language.hpp"
#include <string>
#include <cstring>
#include <sstream>
#include <iostream>
#undef ANTLR4CPP_EXPORTS
#include "antlr4-runtime.h"
#include "LexerTs.h"
#include "ParserTs.h"
#include "core/resource.h"
#include "core/node.h"
#include "core/config.h"
#include "lang_typescript/visitor.hpp"

using namespace antlr4;
using namespace cl::lang::typescript;
using namespace std::literals::string_literals;

language::language(const struct cl_config* config)
    : _types_input({"text/plain", "text/x-typescript"}),
    _types_output({}) {
    AcquireT memory_acquire;
    ReleaseT memory_release;
    unsupported_behavior_type unsupported_behavior = unsupported_behavior_type::type_throw;

    auto memory_acquire_str = cl_config_string_get(config, "Memory/Acquire");
    auto memory_release_str = cl_config_string_get(config, "Memory/Release");
    auto unsupported_behavior_str = cl_config_string_get(config, "Parsing/UnsupportedBehavior");

    if (memory_acquire_str == nullptr) {
        memory_acquire = malloc;
    }
    else {
        try {
            auto ss = std::stringstream{memory_acquire_str};
            void* result;
            ss >> result;
            memory_acquire = (AcquireT)result;
        }
        catch (...) {
            memory_acquire = malloc;
        }
    }

    if (memory_release_str == nullptr) {
        memory_release = free;
    }
    else {
        try {
            auto ss = std::stringstream{memory_release_str};
            void* result;
            ss >> result;
            memory_release = (ReleaseT)result;
        }
        catch (...) {
            memory_release = free;
        }
    }

    if (unsupported_behavior_str != nullptr) {
        if ("0"s == unsupported_behavior_str || "throw"s == unsupported_behavior_str) {
            unsupported_behavior = unsupported_behavior_type::type_throw;
        }
        else if ("1"s == unsupported_behavior_str || "pass"s == unsupported_behavior_str) {
            unsupported_behavior = unsupported_behavior_type::type_pass;
        }
        else if ("2"s == unsupported_behavior_str || "skip"s == unsupported_behavior_str) {
            unsupported_behavior = unsupported_behavior_type::type_skip;
        }
    }

    _options = std::make_unique<language_options>();
    _options->acquire = memory_acquire;
    _options->release = memory_release;
    _options->unsupported_behavior = unsupported_behavior;
}

const cl_node*
language::transform_input(const struct cl_resource* resource) const {
    if (resource == nullptr) {
        return nullptr;
    }

    for (auto type : _types_input) {
        if (strcmp(type, resource->content_type) == 0) {
            // Given type is supported.

            ANTLRInputStream input((const char*)resource->content);
            LexerTs lexer(&input);
            CommonTokenStream tokens(&lexer);

            tokens.fill();

            ParserTs parser(&tokens);
            tree::ParseTree* tree = parser.ex();

            if (tree == nullptr) {
                return nullptr;
            }

            visitor v{*_options};
            tree->accept(&v);

            std::cout << tree->toStringTree(&parser, true) << std::endl << std::endl;

            return v.get_root();
        }
    }

    return nullptr;
}

const struct cl_resource*
language::transform_output(const struct cl_node* node) const {
    return nullptr;
}

const struct cl_node*
language::transform_modify(const struct cl_node* node) const {
    return nullptr;
}

const std::set<const char*>&
language::resource_types_input() const {
    return _types_input;
}

const std::set<const char*>&
language::resource_types_output() const {
    return _types_output;
}
