#pragma once

#include <memory>
#include "antlr4-runtime.h"
#include "lang/language.hpp"

struct cl_resource;
struct cl_node;

namespace cl::lang::typescript
{

class language : public cl::lang::language {
public:
    language();
    const struct cl_node* transform_input(const struct cl_resource* resource) const override;
    const struct cl_resource* transform_output(const struct cl_node* node) const override;
    const struct cl_node* transform_modify(const struct cl_node* node) const override;
    const std::set<const char*>& resource_types_input() const override;
    const std::set<const char*>& resource_types_output() const override;
private:
    std::set<const char*> _types_input;
    std::set<const char*> _types_output;
};

} // namespace cl::lang::typescript
