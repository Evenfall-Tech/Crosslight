#include <iostream>
#include <memory>
#include <new>
#include <string.h>
#include "lang_typescript/language.hpp"

#include "core/language.h"
#include "core/resource.h"
#include "core/config.h"

using namespace cl::lang::typescript;

const struct cl_node*
language_transform_input(const void* context, const struct cl_resource* resource) {
    if (context == 0 || resource == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->transform_input(resource);
}

const struct cl_resource*
language_transform_output(const void* context, const struct cl_node* node) {
    if (context == 0 || node == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->transform_output(node);
}

const struct cl_node*
language_transform_modify(const void* context, const struct cl_node* node) {
    if (context == 0 || node == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->transform_modify(node);
}

const void*
language_init(const struct cl_config* config) {
    if (config == 0) {
        return 0;
    }

    auto* lang = new(std::nothrow) language{};

    if (lang == 0) {
        return 0;
    }

    return lang;
}

size_t
language_term(const void* context) {
    if (context == 0) {
        return 1;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    delete lang;

    return 1;
}

const struct cl_resource_types*
language_resource_types_input(const void* context) {
    if (context == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->cl_resource_types_input();
}

const struct cl_resource_types*
language_resource_types_output(const void* context) {
    if (context == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->cl_resource_types_output();
}

size_t
language_resource_types_term(const void* context, const struct cl_resource_types* types) {
    if (context == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->cl_resource_types_term(types);
}
