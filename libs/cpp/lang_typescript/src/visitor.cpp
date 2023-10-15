#include "lang_typescript/visitor.hpp"
#include <algorithm>
#include <list>
#include <iostream>
#include "lang/utils.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "core/nodes/scope.h"

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
    auto* payload = (struct cl_node_source_root*)_options.acquire(sizeof(struct cl_node_source_root));

    if (payload != nullptr) {
        payload->file_name = nullptr;
        std::cout << "Parsing source_root " << (payload->file_name == nullptr ? "" : payload->file_name) << std::endl;

        return visitNode(ctx, payload, cl_node_type::source_root, true);
    }

    return defaultResult();
}

std::any
visitor::visitNamespaceDeclaration(ParserTs::NamespaceDeclarationContext *ctx) {
    auto* payload = (struct cl_node_scope*)_options.acquire(sizeof(struct cl_node_scope));

    if (payload != nullptr) {
        auto name_context = std::find_if(ctx->children.begin(), ctx->children.end(), [](auto* node) {
            return typeid(*node) == typeid(ParserTs::NamespaceNameContext);
        });

        if (name_context != ctx->children.end()) {
            auto name = (*name_context)->getText();
            payload->identifier = utils::string_duplicate(name.c_str(), _options.acquire);
        }
        else {
            payload->identifier = nullptr;
        }
        std::cout << "Parsing scope " << payload->identifier << std::endl;

        return visitNode(ctx, payload, cl_node_type::scope, true);
    }

    return defaultResult();
}

std::any
visitor::visitNamespaceName(ParserTs::NamespaceNameContext *ctx) {
    return defaultResult(); // Taken care of inside visitNamespaceDeclaration.
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

std::any
visitor::visitNode(antlr4::tree::ParseTree* ctx, void *payload, std::size_t payload_type, bool visit_children) {
    auto* node = (struct cl_node*)_options.acquire(sizeof(struct cl_node));

    if (node == nullptr) {
        return defaultResult();
    }

    if (_root == nullptr) {
        _root = node;
    }

    node->parent = _parent;
    node->payload_type = payload_type;
    node->payload = payload;

    if (visit_children) {
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
    }
    else {
        node->child_count = 0;
        node->children = nullptr;
    }

    return list{node};
}
