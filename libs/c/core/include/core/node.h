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
#include "node/types.h"

/**
 * @brief Base type for language node trees.
 */
struct cl_node {
    const void* payload; /**< Payload node containing specialized fields. */
    enum cl_node_types payload_type; /**< Type of the payload contained in this node. */
    const struct cl_node* parent; /**< Parent of the current node. Used for faster traversing. */
    const struct cl_node* children; /**< Children of the current node. */
    size_t child_count; /**< Number of children the current node has. */
};
