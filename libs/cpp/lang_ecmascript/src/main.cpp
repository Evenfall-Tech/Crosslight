#include <new>
#include "lang_ecmascript/language.hpp"
#include "core/language.h"
#include "core/resource.h"

#pragma clang diagnostic push
#pragma ide diagnostic ignored "OCUnusedGlobalDeclarationInspection"
using namespace cl::lang::ecmascript;

const struct cl_node*
language_transform_input(const void* context, const struct cl_resource* resource) {
    if (context == nullptr || resource == nullptr) {
        return nullptr;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    return lang->transform_input(resource);
}

const struct cl_resource*
language_transform_output(const void* context, const struct cl_node* node) {
    if (context == nullptr || node == nullptr) {
        return nullptr;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    return lang->transform_output(node);
}

const struct cl_node*
language_transform_modify(const void* context, const struct cl_node* node) {
    if (context == nullptr || node == nullptr) {
        return nullptr;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    return lang->transform_modify(node);
}

const void*
language_init(const struct cl_config* config) {
    if (config == nullptr) {
        return nullptr;
    }

    auto* lang = new(std::nothrow) language{config};

    if (lang == nullptr) {
        return nullptr;
    }

    return lang;
}

size_t
language_term(const void* context) {
    if (context == nullptr) {
        return 1;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    delete lang;

    return 1;
}

const struct cl_resource_types*
language_resource_types_input(const void* context) {
    if (context == nullptr) {
        return nullptr;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    return lang->cl_resource_types_input();
}

const struct cl_resource_types*
language_resource_types_output(const void* context) {
    if (context == nullptr) {
        return nullptr;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    return lang->cl_resource_types_output();
}

size_t
language_resource_types_term(const void* context, const struct cl_resource_types* types) {
    if (context == nullptr) {
        return 0;
    }

    auto* lang = reinterpret_cast<const language*>(context);

    return language::cl_resource_types_term(types);
}

#pragma clang diagnostic pop
