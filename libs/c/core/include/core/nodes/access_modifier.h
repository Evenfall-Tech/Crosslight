/**
 * @file core/nodes/access_modifier.h
 * @brief The access_modifier node API.
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
 * @brief Access modifier type. Access modifiers do not support type combinations.
 */
enum cl_node_access_modifier_type {
    access_modifier_none = 0, /**< No access modifier. Acts as a fallback, should not be used normally. */
    access_modifier_public = 1, /**< The type or member can be accessed by any other code in the current or other compilation units. */
    access_modifier_protected = 2, /**< The type or member can be accessed only by sibling members or derivatives of the parent type. */
    access_modifier_private = 3, /**< The type or member can be accessed only by sibling members. */
};

/**
 * @brief Access modifier for types, members, and other nodes.
 */
struct cl_node_access_modifier {
    enum cl_node_access_modifier_type type; /**< Type of the access modifier. Does not support type combinations. */
};

/**
 * @brief Delete the content of and the payload itself.
 *
 * @param[in] payload The payload to delete along with its content.
 * @param[in] term The memory termination function.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_API size_t cl_node_access_modifier_term(struct cl_node_access_modifier* payload, void(*term)(void*));

CL_END_C_DECLS
