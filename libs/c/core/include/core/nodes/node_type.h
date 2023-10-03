/**
 * @file core/nodes/node_type.h
 * @brief The list of supported in-built nodes for the API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include "core/definitions.h"

/**
 * @brief Type of the given node.
 */
CL_C_DECL enum cl_node_type {
    none = 0, /**< No payload given. */
    source_root = 1, /**< Textual source file root. */
};
