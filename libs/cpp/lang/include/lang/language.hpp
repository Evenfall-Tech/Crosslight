#pragma once

#include <set>

struct cl_resource_types;

namespace cl::lang
{

/**
 *  @brief A simple language interface to unify language implementations.
 */
class language {
public:
    /**
     *  @brief Get all supported input MIME-types.
     */
    virtual const std::set<const char*>& resource_types_input() = 0;
    /**
     *  @brief Get all supported output MIME-types.
     */
    virtual const std::set<const char*>& resource_types_output() = 0;

    /**
     *  @brief Get the C representation of input resource types.
     */
    cl_resource_types* cl_resource_types_input();
    /**
     *  @brief Get the C representation of output resource types.
     */
    cl_resource_types* cl_resource_types_output();

    virtual ~language();
};

} // namespace cl::lang
