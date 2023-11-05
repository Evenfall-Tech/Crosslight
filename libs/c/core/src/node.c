#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "core/nodes/scope.h"
#include "core/nodes/heap_type.h"

static size_t
cl_node_term_internal(struct cl_node* root, size_t term_children, void(*term)(void*), size_t term_root) {
    if (term == 0) {
        return 0;
    }

    if (root == 0) {
        return 1;
    }

    if (root->payload != 0) {
        switch (root->payload_type) {
            case source_root:
                if (cl_node_source_root_term((struct cl_node_source_root*)root->payload, term) == 0) {
                    return 0;
                }
                break;
            case scope:
                if (cl_node_scope_term((struct cl_node_scope*)root->payload, term) == 0) {
                    return 0;
                }
                break;
            case heap_type:
                if (cl_node_heap_type_term((struct cl_node_heap_type*)root->payload, term) == 0) {
                    return 0;
                }
                break;
        }
    }

    size_t child_count = root->child_count;

    if (term_children != 0 && child_count > 0 && root->children != 0) {
        for (size_t i = child_count - 1; i != (size_t)-1; --i) {
            if (cl_node_term_internal((struct cl_node*)(root->children + i), term_children, term, i == 0) == 0) {
                return 0;
            }
        }
    }

    if (term_root) {
        term(root);
    }

    return 1;
}

size_t
cl_node_term(struct cl_node* root, size_t term_children, void(*term)(void*)) {
    return cl_node_term_internal(root, term_children, term, 1);
}

size_t
cl_node_source_root_term(struct cl_node_source_root* payload, void(*term)(void*)) {
    if (term == 0) {
        return 0;
    }

    if (payload == 0) {
        return 1;
    }

    if (payload->file_name != 0) {
        term((void*)payload->file_name);
    }

    term(payload);

    return 1;
}

size_t
cl_node_scope_term(struct cl_node_scope* payload, void(*term)(void*)) {
    if (term == 0) {
        return 0;
    }

    if (payload == 0) {
        return 1;
    }

    if (payload->identifier != 0) {
        term((void*)payload->identifier);
    }

    term(payload);

    return 1;
}

size_t
cl_node_heap_type_term(struct cl_node_heap_type* payload, void(*term)(void*)) {
    if (term == 0) {
        return 0;
    }

    if (payload == 0) {
        return 1;
    }

    if (payload->identifier != 0) {
        term((void*)payload->identifier);
    }

    term(payload);

    return 1;
}
