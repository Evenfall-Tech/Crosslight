#include "lang/language.hpp"
#include <cstring>
#include <memory>
#include <new>
#include "core/resource.h"

using namespace cl::lang;

language::~language() = default;

static const struct cl_resource_types*
to_cl_resource_types(const std::set<const char *>& types) {
    auto types_size = types.size();
    auto* result = new(std::nothrow) struct cl_resource_types;

    if (result == nullptr) {
        return nullptr;
    }

    result->content_types_size = types_size;
    auto* content_types = new(std::nothrow) const char*[types_size];

    if (content_types == nullptr) {
        delete result;
        
        return nullptr;
    }

    std::size_t i = 0;
    for (auto type : types) {
        auto child_size = strlen(type) + 1;
        char* str = new(std::nothrow) char[child_size];

        if (str == nullptr) {
            for (std::size_t k = 0; k < i; ++k) {
                delete[] content_types[i];
            }

            delete[] content_types;
            delete result;

            return nullptr;
        }

        str[child_size - 1] = 0;
        strcpy(str, type);
        content_types[i] = str;
        ++i;
    }

    result->content_types = content_types;

    return result;
}

const struct cl_resource_types*
language::cl_resource_types_input() const {
    return to_cl_resource_types(resource_types_input());
}

const struct cl_resource_types*
language::cl_resource_types_output() const {
    return to_cl_resource_types(resource_types_output());
}

size_t
language::cl_resource_types_term(const struct cl_resource_types* types) {
    if (types == nullptr || types->content_types == nullptr) {
        return 1;
    }

    for (size_t i = 0; i < types->content_types_size; ++i) {
        if (types->content_types[i] != nullptr) {
            delete[] types->content_types[i];
        }
    }

    delete[] types->content_types;

    delete types;

    return 1;
}
