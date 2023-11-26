#pragma once

#undef ANTLR4CPP_EXPORTS
#include "ParserTsBaseVisitor.h"
#include <list>
#include "lang_typescript/language_options.hpp"

struct cl_node;

namespace cl::lang::typescript {
using child_list = std::list<antlr4::tree::ParseTree*>;

class visitor : public ParserTsBaseVisitor {
public:
    visitor() = delete;
    explicit visitor(const language_options& options);
    struct cl_node* get_root();

    std::any visitProgram(ParserTs::ProgramContext *ctx) override;
    std::any visitNamespaceDeclaration(ParserTs::NamespaceDeclarationContext *ctx) override;
    std::any visitClassDeclaration(ParserTs::ClassDeclarationContext *ctx) override;

protected:
    virtual std::any visitChildren(
        antlr4::tree::ParseTree *node,
        const child_list* children);
    std::any defaultResult() override;
    std::any aggregateResult(std::any aggregate, std::any nextResult) override;

private:
    struct cl_node* _root;
    struct cl_node* _parent;
    const language_options& _options;

    /**
     * @brief Visit node and a custom child list.
     *
     * @param ctx ParseTree node, corresponding to the generated Crosslight node.
     * @param payload Pointer to acquired payload memory.
     * @param payload_type Type of the payload. Corresponds to @ref cl_node_type values.
     * @param children List of children to visit. If `0`, visit all children.
     * @param visit_children Should children be visited at all.
     */
    std::any visitNode(
        antlr4::tree::ParseTree* ctx,
        void* payload,
        std::size_t payload_type,
        const child_list* children,
        bool visit_children);
};
}
