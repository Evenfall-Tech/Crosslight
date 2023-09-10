#include <iostream>
#include <memory>
#include <new>
#include <string.h>
#include "lang_typescript/language.hpp"

#include "core/language.h"
#include "core/resource.h"
#include "core/config.h"

using namespace cl::lang::typescript;

void*
language_init(const struct cl_config* config) {
    if (config == 0) {
        return 0;
    }

    auto* lang = new(std::nothrow) language{};

    if (lang == 0) {
        return 0;
    }

    const char code[] =
        u8"export interface IVector {"
        u8"data: byte[];"
        u8"length: size;"
        u8"}";
    const char type[] = "text/plain";

    auto resource = std::make_unique<cl_resource>();
    auto* content = new u_int8_t[sizeof(code)];
    resource->content_size = sizeof(code);
    memcpy(content, code, sizeof(code));
    resource->content = content;
    auto* content_type = new char[sizeof(type)];
    memcpy(content_type, type, sizeof(type));
    resource->content_type = content_type;

    lang->parse_source(resource);

    resource->content_size = 0;
    delete[] resource->content;
    delete[] resource->content_type;

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
cl_resource_types_term(const void* context, const struct cl_resource_types* types) {
    if (context == 0) {
        return 0;
    }
    
    auto* lang = reinterpret_cast<const language*>(context);

    return lang->cl_resource_types_term(types);
}
