#pragma once

#include <set>

namespace cl::lang
{

/**
 *  @brief A simple language interface to unify language implementations.
 */
class language {
public:
    virtual const std::set<const char*>& resource_types_input() = 0;
    virtual const std::set<const char*>& resource_types_output() = 0;
    virtual ~language() {}
};

} // namespace cl::lang
