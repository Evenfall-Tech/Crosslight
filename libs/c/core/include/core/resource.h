#pragma once

#include <stdlib.h>

/**
 * @brief External data resource. Usually in form of a text source file,
 * but can be any arbitrary data.
 */
struct cl_resource {
    u_int8_t* content; /**< Data, provided by the resource. */
    size_t content_size; /**< Size of the data in bytes. */
    char* content_type; /**< MIME-type of the data. */
};

/**
 * @brief List of supported MIME-types for a certain language.
 */
struct cl_resource_types {
    char** content_types; /**< MIME-type array. */
    size_t content_types_size;  /**< Number of elements in the array. */
};
