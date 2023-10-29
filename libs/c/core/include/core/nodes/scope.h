/**
 * @file core/nodes/scope.h
 * @brief The scope node API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include <stdlib.h>
#include "core/definitions.h"

CL_BEGIN_C_DECLS

/**
 * @brief Declaration scope, mapping to e.g. namespaces.
 * 
 * Empty scope is assumed to be global (e.g. a file-scoped namespace). If a truly empty scope is desired, an empty node can be placed inside.
 */
struct cl_node_scope {
    const char* identifier; /**< Full identifier of the scope, can be separated with '.'. Can be empty or `0`. */
};

/**
 * @brief Delete the content of and the payload itself.
 *
 * @param[in] payload The payload to delete along with its content.
 * @param[in] term The memory termination function.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_API size_t cl_node_scope_term(struct cl_node_scope* payload, void(*term)(void*));

CL_END_C_DECLS
