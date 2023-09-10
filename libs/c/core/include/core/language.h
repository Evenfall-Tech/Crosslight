#pragma once

#include "core/definitions.h"
#include "core/node.h"

struct cl_config;
struct cl_resource_types;

/**
 * @brief Create a new instance of the language context.
 * 
 * @param[in] config The configuration for initializing language options.
 * @return The created language context or `0` if failed.
 * 
 * @warning The caller is responsible for deleting the instance with @ref language_term(void*).
 */
CL_C_DECL void* language_init(const struct cl_config* config);

/**
 * @brief Delete an instance of an initialized language context.
 * 
 * @param[in] context The initialized language context.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_C_DECL size_t language_term(const void* context);

/**
 * @brief Get a set of supported MIME-types for language input resources.
 * 
 * @param[in] context The initialized language context.
 * @return A set of supported input MIME-types or `0` on error.
 * 
 * @warning The caller is responsible for deleting the instance with @ref cl_resource_types_term.
 */
CL_C_DECL const struct cl_resource_types* language_resource_types_input(const void* context);

/**
 * @brief Get a set of supported MIME-types for language output resources.
 * 
 * @param[in] context The initialized language context.
 * @return A set of supported output MIME-types or `0` on error.
 * 
 * @warning The caller is responsible for deleting the instance with @ref cl_resource_types_term.
 */
CL_C_DECL const struct cl_resource_types* language_resource_types_output(const void* context);

/**
 * @brief Delete an instance of the created @ref cl_resource_types.
 * 
 * @param[in] context The initialized language context.
 * @param[in] types Types instance to delete.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_C_DECL size_t language_resource_types_term(const void* context, const struct cl_resource_types* types);
