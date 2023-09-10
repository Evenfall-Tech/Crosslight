#include <iostream>
#include <memory>
#include <string.h>
#include "lang_typescript/language.hpp"

#include "core/language.h"

using namespace cl::lang::typescript;

CL_RESULT
language_init() {
    const char code[] =
        u8"export interface IVector {"
        u8"data: byte[];"
        u8"length: size;"
        u8"}";
    const char type[] = "text/plain";

    language lang{};

    auto resource = std::make_unique<cl_resource>();
    resource->content = new u_int8_t[sizeof(code)];
    resource->content_size = sizeof(code);
    memcpy(resource->content, code, sizeof(code));
    resource->content_type = new char[sizeof(type)];
    memcpy(resource->content_type, type, sizeof(type));

    lang.parse_source(resource);

    resource->content_size = 0;
    delete[] resource->content;
    delete[] resource->content_type;

    return 0;
}
