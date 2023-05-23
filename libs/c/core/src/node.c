#include "core/node.h"

size_t cl_node_append(struct cl_node* parent, struct cl_node* child) {
    parent->children = child;
    return parent->child_count + 1;
}
