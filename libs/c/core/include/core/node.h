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

CL_C_DECL
size_t
cl_node_append(struct cl_node* parent, struct cl_node* child);
