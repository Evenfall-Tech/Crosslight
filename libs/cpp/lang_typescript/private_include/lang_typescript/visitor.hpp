#pragma once

#include "ParserTsBaseVisitor.h"
#include "lang_typescript/language_options.hpp"

struct cl_node;

namespace cl::lang::typescript {
class visitor : public ParserTsBaseVisitor {
public:
    visitor(const language_options& options);
    struct cl_node* get_root();

    virtual std::any visitProgram(ParserTs::ProgramContext *ctx) override;

private:
    struct cl_node* _root = nullptr;
    struct cl_node* _parent = nullptr;
    const language_options& _options;
};
}
