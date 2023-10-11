/**
 * @file core/nodes/scope.h
 * @brief The scope node API.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

/**
 * @brief Declaration scope, mapping to e.g. namespaces.
 * 
 * Empty scope is assumed to be global. If a truly empty scope is desired, an empty node can be placed inside.
 */
struct cl_node_scope {
    const char* identifier; /**< Full identifier of the scope, can be separated with '.'. Can be empty or `0`. */
};
