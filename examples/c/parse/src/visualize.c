#include "parse/visualize.h"
#include "stdio.h"
#include "string.h"
#include "core/nodes/node_type.h"

const size_t tab_size = 4;

const char* print_tree_node_type(size_t type) {
    switch (type) {
        case none:
            return "none";
        case source_root:
            return "source_root";
        case scope:
            return "scope";
        case heap_type:
            return "heap_type";
        default:
            return "unknown";
    }
}

void
print_tree_node(const struct cl_node* node, size_t nest_level) {
    if (node == 0) {
        return;
    }

    char* offset = "";

    if (nest_level > 0) {
        offset = (char*)malloc(tab_size * nest_level);
        memset(offset, ' ', tab_size * nest_level);
        offset[tab_size * nest_level - 1] = 0;
    }

    printf("%snode %s\n", offset, print_tree_node_type(node->payload_type));

    for (size_t i = 0; i < node->child_count; ++i) {
        print_tree_node(node->children + i, nest_level + 1);
    }
}

void
print_tree(const struct cl_node* root) {
    printf("Crosslight tree:\n");

    if (root == 0) {
        return;
    }

    print_tree_node(root, 0);
    printf("Crosslight tree end.\n");
}
