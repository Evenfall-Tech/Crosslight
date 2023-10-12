#include "stdio.h"
#include "stdint.h"
#include "stdlib.h"
#include "core/definitions.h"
#include "core/config.h"
#include "core/resource.h"
#include "core/node.h"
#include "parse/visualize.h"

#if CL_WINDOWS == 1
#  define WIN32_LEAN_AND_MEAN
#  include <windows.h>
#  undef WIN32_LEAN_AND_MEAN
#elif CL_UNIX == 1 || CL_MACOS == 1
#  include <dlfcn.h>
#endif

static void*
library_init(const char* path) {
#if CL_WINDOWS == 1
    return LoadLibraryA(path);
#else
    void* result = dlopen(path, RTLD_NOW | RTLD_LOCAL);
    
    if (result == 0) {
        printf("dlopen failed: %s\n", dlerror());
    }

    return result;
#endif
}

static size_t
library_term(void* library) {
#if CL_WINDOWS == 1
    return (size_t)FreeLibrary(library);
#else
    return (size_t)dlclose(library);
#endif
}

static void*
function_init(void* library, const char* name) {
#if CL_WINDOWS == 1
    return GetProcAddress(library, name);
#else
    return dlsym(library, name);
#endif
}

const struct cl_node* parse_input(struct cl_config* config, const struct cl_resource* resource) {
    const char library_name[] =
#if CL_WINDOWS == 1
        "./../bin/plugins/cl_lang_typescript.dll";
#elif CL_LINUX == 1
        "./../bin/plugins/libcl_lang_typescript.so";
#elif CL_MACOS == 1
        "./../bin/plugins/libcl_lang_typescript.so";
#endif

    void* lang_library = library_init(library_name);

    if (lang_library == 0) {
        printf("Failed to load library %s.\n", library_name);
        return 0;
    }

    const void* (*lang_init)(const struct cl_config*) =
        function_init(lang_library, "language_init");

    if (lang_init == 0) {
        printf("Failed to load function language_init.\n");
        return 0;
    }

    const size_t (*lang_term)(const void*) =
        function_init(lang_library, "language_term");

    if (lang_term == 0) {
        printf("Failed to load function lang_term.\n");
        return 0;
    }

    const struct cl_node* (*lang_input)(const void*, const struct cl_resource*) =
        function_init(lang_library, "language_transform_input");

    if (lang_input == 0) {
        printf("Failed to load function language_transform_input.\n");
        return 0;
    }

    const struct cl_resource_types* (*lang_rts_input)(const void*) =
        function_init(lang_library, "language_resource_types_input");

    if (lang_rts_input == 0) {
        printf("Failed to load function language_resource_types_input.\n");
        return 0;
    }

    const struct cl_resource_types* (*lang_rts_output)(const void*) =
        function_init(lang_library, "language_resource_types_output");

    if (lang_rts_output == 0) {
        printf("Failed to load function language_resource_types_output.\n");
        return 0;
    }

    size_t (*lang_rts_term)(const void*, const struct cl_resource_types*) =
        function_init(lang_library, "language_resource_types_term");

    if (lang_rts_term == 0) {
        printf("Failed to load function language_resource_types_term.\n");
        return 0;
    }

    const void* lang = lang_init(config);

    {
        const struct cl_resource_types* rts = lang_rts_input(lang);

        for (size_t i = 0; i < rts->content_types_size; ++i)
        {
            printf("Input type: %s.\n", rts->content_types[i]);
        }

        printf("Input type term: %d.\n", (int)lang_rts_term(lang, rts));
    }

    {
        const struct cl_resource_types* rts = lang_rts_output(lang);

        for (size_t i = 0; i < rts->content_types_size; ++i)
        {
            printf("Output type: %s.\n", rts->content_types[i]);
        }

        printf("Output type term: %d.\n", (int)lang_rts_term(lang, rts));
    }

    const struct cl_node* tree = lang_input(lang, resource);
    printf("Tree: %p.\n", (void*)tree);
    
    lang_term(lang);

    library_term(lang_library);

    return tree;
}

const struct cl_resource* parse_output(struct cl_config* config, const struct cl_node* node) {
    const char library_name[] =
#if CL_WINDOWS == 1
        "./../bin/plugins/cl_lang_csharp_ref.dll";
#elif CL_LINUX == 1
        "./../bin/plugins/libcl_lang_csharp_ref.so";
#elif CL_MACOS == 1
        "./../bin/plugins/libcl_lang_csharp_ref.dylib";
#endif

    void* lang_library = library_init(library_name);

    if (lang_library == 0) {
        printf("Failed to load library %s.\n", library_name);
        return 0;
    }

    const void* (*lang_init)(const struct cl_config*) =
        function_init(lang_library, "language_init");

    if (lang_init == 0) {
        printf("Failed to load function language_init.\n");
        return 0;
    }

    const size_t (*lang_term)(const void*) =
        function_init(lang_library, "language_term");

    if (lang_term == 0) {
        printf("Failed to load function lang_term.\n");
        return 0;
    }

    const struct cl_resource* (*lang_output)(const void*, const struct cl_node*) =
        function_init(lang_library, "language_transform_output");

    if (lang_output == 0) {
        printf("Failed to load function language_transform_output.\n");
        return 0;
    }

    const struct cl_resource_types* (*lang_rts_input)(const void*) =
        function_init(lang_library, "language_resource_types_input");

    if (lang_rts_input == 0) {
        printf("Failed to load function language_resource_types_input.\n");
        return 0;
    }

    const struct cl_resource_types* (*lang_rts_output)(const void*) =
        function_init(lang_library, "language_resource_types_output");

    if (lang_rts_output == 0) {
        printf("Failed to load function language_resource_types_output.\n");
        return 0;
    }

    size_t (*lang_rts_term)(const void*, const struct cl_resource_types*) =
        function_init(lang_library, "language_resource_types_term");

    if (lang_rts_term == 0) {
        printf("Failed to load function language_resource_types_term.\n");
        return 0;
    }

    const void* lang = lang_init(config);

    {
        const struct cl_resource_types* rts = lang_rts_input(lang);

        for (size_t i = 0; i < rts->content_types_size; ++i)
        {
            printf("Input type: %s.\n", rts->content_types[i]);
        }

        printf("Input type term: %d.\n", (int)lang_rts_term(lang, rts));
    }

    {
        const struct cl_resource_types* rts = lang_rts_output(lang);

        for (size_t i = 0; i < rts->content_types_size; ++i)
        {
            printf("Output type: %s.\n", rts->content_types[i]);
        }

        printf("Output type term: %d.\n", (int)lang_rts_term(lang, rts));
    }

    const struct cl_resource* resource = lang_output(lang, node);
    printf("Resource: %p.\n", (void*)resource);
    
    lang_term(lang);

    library_term(lang_library);

    return resource;
}

int
main(int argc, char **argv) {
    const char code[] =
        u8"namespace Shapes {"
        u8"export interface IVector {"
        u8"data: byte[];"
        u8"length: size;"
        u8"}"
        u8"}";
    const char type[] = "text/plain";

    // Setup resource.

    struct cl_resource resourceIn;
    resourceIn.content = (const uint8_t*)code;
    resourceIn.content_type = type;
    resourceIn.content_size = sizeof(code);
    
    printf("Input is below:\n%s\n", (const char*)resourceIn.content);

    struct cl_config* config = cl_config_init();

    char memoryAllocateString[33];
    sprintf(memoryAllocateString, "%p", (void*)&malloc);
    cl_config_string_set(config, "Memory/Acquire", memoryAllocateString);

    char memoryFreeString[33];
    sprintf(memoryFreeString, "%p", (void*)&free);
    cl_config_string_set(config, "Memory/Release", memoryFreeString);

    char parseProcessUnsupportedString[6];
    sprintf(parseProcessUnsupportedString, "true");
    cl_config_string_set(config, "Parsing/ProcessUnsupported", parseProcessUnsupportedString);

    const struct cl_node* node = parse_input(config, &resourceIn);
    print_tree(node);
    const struct cl_resource* resource = parse_output(config, node);
    
    printf("Output is below:\n%s\n", (const char*)resource->content);

    cl_config_term(config);

    return 0;
}