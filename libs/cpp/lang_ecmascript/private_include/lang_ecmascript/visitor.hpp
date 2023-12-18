#pragma once

#undef ANTLR4CPP_EXPORTS
#include "ParserEsBaseVisitor.h"
#include <list>
#include "lang_ecmascript/language_options.hpp"

struct cl_node;

namespace cl::lang::ecmascript {

using child_list = std::list<antlr4::tree::ParseTree*>;

class visitor : public ParserEsBaseVisitor {
public:
    visitor() = delete;
    explicit visitor(const language_options& options);
    struct cl_node* get_root();

    std::any visitArrowFunction_In(ParserEs::ArrowFunction_InContext *ctx) override;
protected:

private:
    struct cl_node* _root;
    struct cl_node* _parent;
    const language_options& _options;
};

} // namespace cl::lang::ecmascript
