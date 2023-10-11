#include "lang_typescript/visitor.hpp"
#include <cstring>
#include <list>
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"

using namespace cl::lang::typescript;
using list = std::list<struct cl_node*>;

visitor::visitor(const language_options& options)
    : _options{options},
      _root{nullptr},
      _parent{nullptr} {
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

    if (payload != nullptr) {
        payload->file_name = nullptr;
    }

    node->payload = payload;

    auto old_parent = _parent;
    _parent = node;

    auto result = visitChildren(ctx);
    auto& merged = std::any_cast<list&>(result);

    node->child_count = merged.size();

    if (node->child_count > 0) {
        auto byte_size = sizeof(struct cl_node) * node->child_count;
        auto* children = (struct cl_node*)_options.acquire(byte_size);

        if (children == nullptr) {
            node->child_count = 0;
            node->children = nullptr;
        }
        else {
            size_t i = 0;

            for (auto val : merged) {
                *(children + i) = *val;
            }

            node->children = children;
        }
    }
    else {
        node->children = nullptr;
    }
    
    _parent = old_parent;

    return list{node};
}

std::any
visitor::visitNamespaceDeclaration(ParserTs::NamespaceDeclarationContext *ctx) {
    return defaultResult();
}

std::any
visitor::visitNamespaceName(ParserTs::NamespaceNameContext *ctx) {
    return defaultResult();
}

std::any
visitor::defaultResult() {
    return list{};
}

std::any
visitor::aggregateResult(std::any aggregate, std::any nextResult) {
    auto& merged = std::any_cast<list&>(aggregate);
    auto& next = std::any_cast<list&>(nextResult);
    merged.splice(merged.end(), next, next.begin(), next.end());
    return aggregate;
}
