#include "lang_typescript/visitor.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"

using namespace cl::lang::typescript;

visitor::visitor(const language_options& options)
    : _options{options} {
}

struct cl_node*
visitor::get_root() {
    return _root;
}

std::any
visitor::visitProgram(ParserTs::ProgramContext *ctx) {
    struct cl_node* node = (struct cl_node*)_options.acquire(sizeof(struct cl_node));
    
    if (node == nullptr) {
        return defaultResult();
    }

    if (_root == nullptr) {
        _root = node;
    }

    node->parent = _parent;
    node->payload_type = cl_node_type::source_root;

    auto payload = (struct cl_node_source_root*)_options.acquire(sizeof(struct cl_node_source_root));
    payload->file_name = nullptr;
    node->payload = payload;

    auto oldParent = _parent;
    _parent = node;
    //auto result = visitChildren(ctx);
    node->children = nullptr;
    node->child_count = 0;
    _parent = oldParent;

    return defaultResult();
}
