/**
 * @file core/nodes/source_root.h
 * @brief The source root node API.
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
 * @brief Textual source file root.
 */
struct cl_node_source_root {
    const char* file_name; /**< Name of the file that contains this root in UTF-8 format. Can be empty or `0`. */
};

/**
 * @brief Delete the content of and the payload itself.
 *
 * @param[in] payload The payload to delete along with its content.
 * @param[in] term The memory termination function.
 * @return `0` if deletion failed, `1` otherwise.
 */
CL_API size_t cl_node_source_root_term(struct cl_node_source_root* payload, void(*term)(void*));

CL_END_C_DECLS
