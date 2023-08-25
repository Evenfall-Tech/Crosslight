#pragma once

#include <stdlib.h>

/**
 * @brief Base type for language node trees.
 */
struct cl_node {
    void* payload; /**< Payload node containing specialized fields. */
    size_t payload_type; /**< Type of the payload contained in this node. */
    struct cl_node* parent; /**< Parent of the current node. Used for faster traversing. */
    struct cl_node* children; /**< Children of the current node. */
    size_t child_count; /**< Number of children the current node has. */
};
