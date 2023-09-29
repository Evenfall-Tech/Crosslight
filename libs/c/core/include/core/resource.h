/**
 * @file core/resource.h
 * @brief The external data resource API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include <stdint.h>

/**
 * @brief External data resource. Usually in form of a text source file,
 * but can be any arbitrary data.
 */
struct cl_resource {
    const uint8_t* content; /**< Data, provided by the resource. */
    size_t content_size; /**< Size of the data in bytes. */
    const char* content_type; /**< MIME-type of the data. */
};

/**
 * @brief List of supported MIME-types for a certain language.
 */
struct cl_resource_types {
    const char* const* content_types; /**< MIME-type array. Null-pointer if @p content_types_size is `0`. */
    size_t content_types_size;  /**< Number of elements in the array. */
};
