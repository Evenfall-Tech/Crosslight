/**
 * @file core/node.h
 * @brief The generic node API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include <stdlib.h>
#include "core/definitions.h"

/**
 * @brief Base type for language node trees.
 */
CL_C_DECL struct cl_node {
    const void* payload; /**< Payload node containing specialized fields. */
    size_t payload_type; /**< Type of the payload contained in this node. Should use values from @ref cl_node_type. */
    const struct cl_node* parent; /**< Parent of the current node. Used for faster traversing. */
    const struct cl_node* children; /**< Children of the current node. */
    size_t child_count; /**< Number of children the current node has. */
};
