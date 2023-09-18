#include "stdio.h"
#include "stdint.h"
#include "stdlib.h"
#include "core/definitions.h"
#include "core/config.h"
#include "core/resource.h"
#include "core/node.h"

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
    return dlopen(path, RTLD_NOW | RTLD_LOCAL);
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

int
main(int argc, char **argv) {
    // Load library.

    const char library_name[] =
#if CL_WINDOWS == 1
        "./cl_lang_typescript.dll";
#elif CL_LINUX == 1
        //"./libcl_lang_typescript.so";
        "./libcl_lang_csharp_ref.so";
#elif CL_MACOS == 1
        "./cl_lang_typescript.dylib";
#endif

    void* lang_library = library_init(library_name);

    if (lang_library == 0) {
        printf("Failed to load library %s.\n", library_name);
        return 1;
    }

    const void* (*lang_init)(const struct cl_config*) =
        function_init(lang_library, "language_init");

    if (lang_init == 0) {
        printf("Failed to load function language_init.\n");
        return 1;
    }

    const size_t (*lang_term)(const void*) =
        function_init(lang_library, "language_term");

    if (lang_term == 0) {
        printf("Failed to load function lang_term.\n");
        return 1;
    }

    const struct cl_node* (*lang_input)(const void*, const struct cl_resource*) =
        function_init(lang_library, "language_transform_input");

    if (lang_term == 0) {
        printf("Failed to load function language_transform_input.\n");
        return 1;
    }
    
    const char code[] =
        u8"export interface IVector {"
        u8"data: byte[];"
        u8"length: size;"
        u8"}";
    const char type[] = "text/plain";

    // Setup resource.

    struct cl_resource resource;
    resource.content = code;
    resource.content_type = type;
    resource.content_size = sizeof(code);

    struct cl_config* config = cl_config_init();

    const void* lang = lang_init(config);

    const struct cl_node* tree = lang_input(lang, &resource);
    printf("Tree: %d.\n", (int)(size_t)tree);
    
    lang_term(lang);

    cl_config_term(config);

    library_term(lang_library);

    return 0;
}