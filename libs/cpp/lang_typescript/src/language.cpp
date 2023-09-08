#include "lang_typescript/language.hpp"
#include <string>
#include "core/resource.h"

using namespace cl::lang::typescript;
using namespace antlr4;

language::language()
    : _types_input({"text/plain"}),
    _types_output({}) {
}

std::unique_ptr<tree::ParseTree> language::parse_source(const std::unique_ptr<cl_resource>& resource) {
    if (!resource) {
        return {};
    }

    if (_types_input.find(resource->content_type) != _types_input.end()) {
        // Given type is supported.
    }
}

const std::set<const char*>& language::resource_types_input() {
    return _types_input;
}

const std::set<const char*>& language::resource_types_output() {
    return _types_output;
}
