/**
 * @file core/nodes/heap_type.h
 * @brief The heap_type node API.
 *
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

/**
 * @brief Complex type stored on the heap, mapping to e.g. allocated classes.
 */
struct cl_node_heap_type {
    const char* identifier; /**< Identifier of the type. Can be empty or `0`. */
};
