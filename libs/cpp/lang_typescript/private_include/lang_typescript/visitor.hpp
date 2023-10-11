#pragma once

#undef ANTLR4CPP_EXPORTS
#include "ParserTsBaseVisitor.h"
#include "lang_typescript/language_options.hpp"

struct cl_node;

namespace cl::lang::typescript {
class visitor : public ParserTsBaseVisitor {
public:
    visitor() = delete;
    visitor(const language_options& options);
    struct cl_node* get_root();

    virtual std::any visitProgram(ParserTs::ProgramContext *ctx) override;
    virtual std::any visitNamespaceDeclaration(ParserTs::NamespaceDeclarationContext *ctx) override;
    virtual std::any visitNamespaceName(ParserTs::NamespaceNameContext *ctx) override;

protected:
    virtual std::any defaultResult() override;
    virtual std::any aggregateResult(std::any aggregate, std::any nextResult) override;

private:
    struct cl_node* _root;
    struct cl_node* _parent;
    const language_options& _options;
};
}
