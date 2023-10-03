#pragma once

#include <memory>
#undef ANTLR4CPP_EXPORTS
#include "antlr4-runtime.h"
#include "lang/language.hpp"
#include "lang_typescript/language_options.hpp"

struct cl_resource;
struct cl_node;
struct cl_config;

namespace cl::lang::typescript
{
class language : public cl::lang::language {
public:
    language(const struct cl_config* config);
    const struct cl_node* transform_input(const struct cl_resource* resource) const override;
    const struct cl_resource* transform_output(const struct cl_node* node) const override;
    const struct cl_node* transform_modify(const struct cl_node* node) const override;
    const std::set<const char*>& resource_types_input() const override;
    const std::set<const char*>& resource_types_output() const override;
private:
    std::set<const char*> _types_input;
    std::set<const char*> _types_output;
    std::unique_ptr<language_options> _options;
};

} // namespace cl::lang::typescript
