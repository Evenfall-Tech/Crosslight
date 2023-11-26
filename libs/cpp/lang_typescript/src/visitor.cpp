#include "lang_typescript/visitor.hpp"
#include <algorithm>
#include <iostream>
#include <optional>
#include "lang/utils.hpp"
#include "lang/exceptions/not_implemented_parsing_exception.hpp"
#include "lang/exceptions/not_supported_parsing_exception.hpp"
#include "core/node.h"
#include "core/nodes/node_type.h"
#include "core/nodes/source_root.h"
#include "core/nodes/scope.h"
#include "core/nodes/heap_type.h"

using namespace cl::lang::typescript;
using namespace cl::lang::exceptions;
using list = std::list<struct cl_node*>;

template <typename T>
CL_ALWAYS_INLINE static T* acquire_entity(cl::lang::AcquireT acquire) {
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

    if (payload) {
        payload->file_name = nullptr;
        std::cout << "Parsing source_root " << (payload->file_name == nullptr ? "" : payload->file_name) << std::endl;
        auto* elements = ctx->sourceElements();

        if (!elements) {
            return visitNode(ctx, payload, ::source_root, {}, false);
        }

        auto children_container = elements->sourceElement();
        child_list children{};

        for (auto* child_wrapper : children_container) {
            // TODO: parse sourceElement export keyword.
            auto* child = child_wrapper->statement();

            if (auto* syntax = child->namespaceDeclaration()) {
                children.push_back(syntax);
            }
            else {
                switch (_options.unsupported_behavior) {
                    case unsupported_behavior_type::type_pass:
                        continue;
                    case unsupported_behavior_type::type_skip:
                        return visitNode(ctx, payload, ::source_root, nullptr, false);
                    case unsupported_behavior_type::type_throw:
                    default:
                        throw not_implemented_parsing_exception{child->toString() + " is not yet supported."};
                }
            }
        }

        return visitNode(ctx, payload, ::source_root, &children, true);
    }

    return defaultResult();
}

std::any
visitor::visitNamespaceDeclaration(ParserTs::NamespaceDeclarationContext *ctx) {
    auto* payload = acquire_entity<struct cl_node_scope>(_options.acquire);

    if (!payload) return defaultResult();

    auto name_context = ctx->namespaceName();

    if (name_context) {
        auto name = name_context->getText();
        payload->identifier = utils::string_duplicate(name.c_str(), _options.acquire);
    }
    else {
        payload->identifier = nullptr;
    }
    std::cout << "Parsing scope " << payload->identifier << std::endl;

    list merged{};
    std::any result = defaultResult();
    auto* statements = ctx->statementList();

    if (statements) {
        auto children_container = statements->statement();
        child_list children{};

        for (auto* child : children_container) {
            if (auto* syntax = child->classDeclaration()) {
                children.push_back(syntax);
            }
            else {
                switch (_options.unsupported_behavior) {
                    case unsupported_behavior_type::type_pass:
                        continue;
                    case unsupported_behavior_type::type_skip:
                        return visitNode(ctx, payload, ::scope, nullptr, false);
                    case unsupported_behavior_type::type_throw:
                    default:
                        throw not_implemented_parsing_exception{child->toString() + " is not yet supported."};
                }
            }
        }

        result = visitNode(ctx, payload, cl_node_type::scope, &children, true);
        merged = std::any_cast<list>(result);
    }

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

        child_list children{};
        auto* children_wrapper = ctx->classTail();

        if (children_wrapper)
        {
            auto children_container = children_wrapper->classElement();

            for (auto* child : children_container) {
                if (auto* syntax = child->statement()) {
                    children.push_back(syntax);
                }
                else {
                    switch (_options.unsupported_behavior) {
                        case unsupported_behavior_type::type_pass:
                            continue;
                        case unsupported_behavior_type::type_skip:
                            return visitNode(ctx, payload, ::heap_type, nullptr, false);
                        case unsupported_behavior_type::type_throw:
                        default:
                            throw not_implemented_parsing_exception{child->toString() + " is not yet supported."};
                    }
                }
            }
        }

        return visitNode(ctx, payload, cl_node_type::heap_type, &children, true);
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
visitor::visitChildren(
    antlr4::tree::ParseTree *node,
    const child_list* children) {
    std::any result = defaultResult();

    if (children) {
        size_t n = children->size();
        auto child = children->begin();

        for (size_t i = 0; i < n; i++) {
            if (!shouldVisitNextChild(node, result)) {
                break;
            }

            std::any childResult = (*child)->accept(this);
            result = aggregateResult(std::move(result), std::move(childResult));
            ++child;
        }
    }
    else {
        size_t n = node->children.size();

        for (size_t i = 0; i < n; i++) {
            if (!shouldVisitNextChild(node, result)) {
                break;
            }

            std::any childResult = node->children[i]->accept(this);
            result = aggregateResult(std::move(result), std::move(childResult));
        }
    }

    return result;
}

std::any
visitor::visitNode(
    antlr4::tree::ParseTree* ctx,
    void *payload,
    std::size_t payload_type,
    const child_list* children,
    bool visit_children) {
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
        auto result = visitChildren(ctx, children);
        auto& merged = std::any_cast<list&>(result);

        node->child_count = merged.size();

        if (node->child_count > 0) {
            auto byte_size = sizeof(struct cl_node) * node->child_count;
            auto* children_memory = (struct cl_node*)_options.acquire(byte_size);

            if (children_memory == nullptr) {
                node->child_count = 0;
                node->children = nullptr;
            }
            else {
                size_t i = 0;

                for (auto* val : merged) {
                    *(children_memory + i) = *val;
                }

                node->children = children_memory;
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
