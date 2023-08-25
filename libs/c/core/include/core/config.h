#pragma once

#include <stdlib.h>
#include "core/definitions.h"
#include "core/result.h"

/**
 * @brief Key-value pair configuration wrapper.
 * Hierarchial keys supported through `key1/key2`.
 */
struct cl_config;

/**
 * @brief Create a new instance of the config.
 * The caller is responsible for the lifetime of the created object.
 * 
 * @return `0` if creation failed, address otherwise.
 */
CL_C_DECL struct cl_config* cl_config_new();

/**
 * @brief Delete an instance of the config.
 * 
 * @param[in] config Config instance to delete.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_C_DECL size_t cl_config_delete(struct cl_config* config);

/**
 * @brief Get a string value from the config based on a key.
 * 
 * @param[in] context The config to get the value from.
 * @param[in] key The key to get the value for.
 * @return `0` if no such key found or value is `0`, pointer to the string otherwise.
 * 
 * @note The caller should create a copy of the value if modification is desired.
 */
CL_C_DECL const char* cl_config_string_get(const struct cl_config* context, const char* key);

/**
 * @brief Set a string value in the config based on a key.
 * If the key is already present, replace the value.
 * 
 * @param[in] context The config to set the value in.
 * @param[in] key The key to set the value for.
 * @param[in] value The value to set. Can be `0`.
 * @return `0` if setting value for key failed, `1` otherwise.
 * 
 * @note If a memory allocation error occurs, the function should terminate gracefully
 * and not change the `context` state.
 */
CL_C_DECL size_t cl_config_string_set(struct cl_config* context, const char* key, const char* value);
