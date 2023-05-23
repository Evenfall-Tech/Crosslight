#pragma once

#include <stdlib.h>
#include "core/definitions.h"

/**
 * @brief Base type for language node trees.
*/
struct cl_node {
    void* payload; /**< Payload node containing specialized fields. */
    struct cl_node* parent; /**< Parent of the current node. Used only for faster traversing. */
    struct cl_node* children;
    size_t child_count;
};

size_t EXPORT cl_node_append(struct cl_node* parent, struct cl_node* child);
