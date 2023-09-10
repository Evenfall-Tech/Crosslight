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
    std::unique_ptr<cl_node> parse_source(const std::unique_ptr<cl_resource>& resource);
    virtual const std::set<const char*>& resource_types_input() override;
    virtual const std::set<const char*>& resource_types_output() override;
private:
    std::set<const char*> _types_input;
    std::set<const char*> _types_output;
};

} // namespace cl::lang::typescript
