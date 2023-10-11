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

using namespace cl::lang::typescript;
using namespace antlr4;

language::language(const struct cl_config* config)
    : _types_input({"text/plain", "text/x-typescript"}),
    _types_output({}) {
    AcquireT memoryAcquire = nullptr;
    ReleaseT memoryRelease = nullptr;
    bool processUnsupported = false;

    auto memoryAcquireStr = cl_config_string_get(config, "Memory/Acquire");
    auto memoryReleaseStr = cl_config_string_get(config, "Memory/Release");
    auto processUnsupportedStr = cl_config_string_get(config, "Parsing/ProcessUnsupported");

    if (memoryAcquireStr == 0) {
        memoryAcquire = malloc;
    }
    else {
        try {
            auto ss = std::stringstream{memoryAcquireStr};
            void* result;
            ss >> result;
            memoryAcquire = (AcquireT)result;
        }
        catch (...) {
            memoryAcquire = malloc;
        }
    }

    if (memoryReleaseStr == 0) {
        memoryRelease = free;
    }
    else {
        try {
            auto ss = std::stringstream{memoryReleaseStr};
            void* result;
            ss >> result;
            memoryRelease = (ReleaseT)result;
        }
        catch (...) {
            memoryRelease = free;
        }
    }

    if (processUnsupportedStr != 0) {
        processUnsupported = std::string{"true"} == processUnsupportedStr;
    }

    _options = std::make_unique<language_options>();
    _options->acquire = memoryAcquire;
    _options->release = memoryRelease;
    _options->process_unsupported = processUnsupported;
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

            ParserTs parser(&tokens);
            tree::ParseTree* tree = parser.program();

            if (tree == nullptr) {
                return 0;
            }

            visitor v{*_options};
            tree->accept(&v);

            std::cout << tree->toStringTree(&parser, true) << std::endl << std::endl;

            return v.get_root();
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
