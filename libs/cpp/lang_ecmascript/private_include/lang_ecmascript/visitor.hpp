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

protected:

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
};

} // namespace cl::lang::ecmascript
