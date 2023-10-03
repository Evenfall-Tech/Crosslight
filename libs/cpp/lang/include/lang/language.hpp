#pragma once

#include <set>
#include "lang/def_visibility.hpp"

struct cl_resource_types;
struct cl_resource;
struct cl_node;

namespace cl::lang
{

/**
 * @brief A simple language interface to unify language implementations.
 */
class CL_API_OBJ language {
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
     * @brief Transform an input resource to a node tree.
     */
    virtual const struct cl_node* transform_input(const struct cl_resource* resource) const = 0;
    
    /**
     * @brief Transform a node tree to an output resource.
     */
    virtual const struct cl_resource* transform_output(const struct cl_node* node) const = 0;
    
    /**
     * @brief Transform a node tree to a different form.
     */
    virtual const struct cl_node* transform_modify(const struct cl_node* node) const = 0;

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
