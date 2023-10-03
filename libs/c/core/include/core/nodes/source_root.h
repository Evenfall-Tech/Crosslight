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

/**
 * @brief Textual source file root.
 */
struct cl_node_source_root {
    const char* file_name; /**< Name of the file that contains this root in UTF-8 format. Can be empty or `0`. */
};
