#include "lang_typescript/visitor.hpp"
#include <algorithm>
#include <list>
#include <iostream>
#include "lang/utils.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "core/nodes/scope.h"
#include "core/nodes/heap_type.h"

using namespace cl::lang::typescript;
using list = std::list<struct cl_node*>;

template <typename T>
CL_ALWAYS_INLINE static T* acquire_entity(AcquireT acquire) {
    return static_cast<T*>(acquire(sizeof(T)));
}

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
    auto* payload = acquire_entity<struct cl_node_source_root>(_options.acquire);

    if (payload != nullptr) {
        payload->file_name = nullptr;
        std::cout << "Parsing source_root " << (payload->file_name == nullptr ? "" : payload->file_name) << std::endl;

        return visitNode(ctx, payload, cl_node_type::source_root, true);
    }

    return defaultResult();
}

std::any
visitor::visitNamespaceDeclaration(ParserTs::NamespaceDeclarationContext *ctx) {
    auto* payload = acquire_entity<struct cl_node_scope>(_options.acquire);

    if (payload != nullptr) {
        auto name_context = ctx->namespaceName();

        if (name_context != nullptr) {
            auto name = name_context->getText();
            payload->identifier = utils::string_duplicate(name.c_str(), _options.acquire);
        }
        else {
            payload->identifier = nullptr;
        }
        std::cout << "Parsing scope " << payload->identifier << std::endl;

        auto result = visitNode(ctx, payload, cl_node_type::scope, true);
        auto& merged = std::any_cast<list&>(result);

        // Create an empty child to specify truly empty scope.
        if (!merged.empty() && merged.front()->child_count == 0) {
            auto* current = merged.front();
            auto* empty_child = acquire_entity<struct cl_node>(_options.acquire);

            empty_child->child_count = 0;
            empty_child->children = nullptr;
            empty_child->payload_type = cl_node_type::none;
            empty_child->payload = nullptr;
            empty_child->parent = current;

            current->child_count = 1;
            current->children = empty_child;
        }

        return result;
    }

    return defaultResult();
}

std::any
visitor::visitClassDeclaration(ParserTs::ClassDeclarationContext *ctx) {
    auto* payload = acquire_entity<struct cl_node_heap_type>(_options.acquire);

    if (payload != nullptr) {
        auto name_context = ctx->Identifier();

        if (name_context != nullptr) {
            auto name = name_context->getText();
            payload->identifier = utils::string_duplicate(name.c_str(), _options.acquire);
        }
        else {
            payload->identifier = nullptr;
        }
        std::cout << "Parsing heap_type " << payload->identifier << std::endl;

        return visitNode(ctx, payload, cl_node_type::heap_type, true);
    }

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

std::any
visitor::visitNode(antlr4::tree::ParseTree* ctx, void *payload, std::size_t payload_type, bool visit_children) {
    auto* node = acquire_entity<struct cl_node>(_options.acquire);

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
