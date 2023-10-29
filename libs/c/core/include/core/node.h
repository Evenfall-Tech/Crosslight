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

CL_BEGIN_C_DECLS

/**
 * @brief Base type for language node trees.
 */
struct cl_node {
    const void* payload; /**< Payload node containing specialized fields. */
    size_t payload_type; /**< Type of the payload contained in this node. Should use values from @ref cl_node_type. */
    const struct cl_node* parent; /**< Parent of the current node. Used for faster traversing. */
    const struct cl_node* children; /**< Children of the current node. */
    size_t child_count; /**< Number of children the current node has. */
};

/**
 * @brief Delete a node tree starting from root.
 * @todo Rewrite without recursion.
 *
 * @param[in] root The root of the node tree.
 * @param[in] term_children `0` if children should not be terminated, `1` otherwise.
 * @param[in] term The memory termination function.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_API size_t cl_node_term(struct cl_node* root, size_t term_children, void(*term)(void*));

CL_END_C_DECLS
