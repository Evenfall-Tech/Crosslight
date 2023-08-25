#pragma once

#include <stdlib.h>

/**
 * @brief External data resource. Usually in form of a text source file,
 * but can be any arbitrary data.
 */
struct cl_resource {
    const u_int8_t* content; /**< Data, provided by the resource. */
    size_t content_size; /**< Size of the data in bytes. */
    const char* content_type; /**< MIME-type of the data. */
};
