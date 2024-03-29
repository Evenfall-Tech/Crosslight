/**
 * @file core/config.h
 * @brief The key-value pair configuration wrapper.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include <stdlib.h>
#include "core/definitions.h"

CL_BEGIN_C_DECLS

/**
 * @brief Key-value pair configuration wrapper.
 * Hierarchial keys supported through `key1/key2`.
 */
struct cl_config;

/**
 * @brief Create a new instance of the config.
 * 
 * @return `0` if creation failed, address otherwise.
 * 
 * @warning The caller is responsible for deleting the instance with @ref cl_config_term(struct cl_config*).
 */
CL_API struct cl_config* cl_config_init();

/**
 * @brief Delete an instance of the config.
 * 
 * @param[in] config Config instance to delete.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_API size_t cl_config_term(struct cl_config* config);

/**
 * @brief Get a string value from the config based on a key.
 * 
 * @param[in] context The config to get the value from.
 * @param[in] key The key to get the value for.
 * @return `0` if no such key found or value is `0`, pointer to the string otherwise.
 * 
 * @warning The caller should create a copy of the value if modification is desired.
 */
CL_API const char* cl_config_string_get(const struct cl_config* context, const char* key);

/**
 * @brief Set a string value in the config based on a key.
 * If the key is already present, replace the value.
 * 
 * @param[in] context The config to set the value in.
 * @param[in] key The key to set the value for.
 * @param[in] value The value to set. Will be copied. Can be `0`.
 * @return `0` if setting value for key failed, `1` otherwise.
 * 
 * @warning If a memory allocation error occurs, the function should terminate gracefully
 * and not change the @p context state.
 */
CL_API size_t cl_config_string_set(struct cl_config* context, const char* key, const char* value);

CL_END_C_DECLS
