/**
 * @file core/nodes/node_type.h
 * @brief The list of supported in-built nodes for the API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include <stddef.h>
#include "core/definitions.h"

CL_BEGIN_C_DECLS

/**
 * @brief Type of the given node.
 */
enum cl_node_type {
    none = 0, /**< No payload given. */
    source_root = 1, /**< Textual source file root. */
    scope = 2, /**< Declaration scope. */
    heap_type = 3, /**< Complex type stored on the heap. */
    access_modifier = 4, /**< Accessibility level modifier. */
};

CL_END_C_DECLS
