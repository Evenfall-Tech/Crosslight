#pragma once

#include <set>

struct cl_resource_types;

namespace cl::lang
{

/**
 * @brief A simple language interface to unify language implementations.
 */
class language {
public:
    /**
     * @brief Get all supported input MIME-types.
     */
    virtual const std::set<const char*>& resource_types_input() const = 0;
    /**
     * @brief Get all supported output MIME-types.
     */
    virtual const std::set<const char*>& resource_types_output() const = 0;

    /**
     * @brief Get the C representation of input resource types.
     */
    const struct cl_resource_types* cl_resource_types_input() const;
    /**
     * @brief Get the C representation of output resource types.
     */
    const struct cl_resource_types* cl_resource_types_output() const;
    /**
     * @brief Terminate the C representation of resource types.
     */
    const std::size_t cl_resource_types_term(const struct cl_resource_types* types) const;

    virtual ~language();
};

} // namespace cl::lang
