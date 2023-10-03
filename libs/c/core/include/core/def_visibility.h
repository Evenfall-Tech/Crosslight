/**
 * @file core/def_visibility.h
 * @brief The public API macros.
 * 
 * @author Mykola Morozov
 * @copyright (c) 2023 Evenfall-Tech
 *
 * This library is released under MPL-2.0 <https://github.com/Evenfall-Tech/Crosslight/blob/master/LICENSE> license.
 */

#pragma once

#include "core/def_utils.h"

#if defined(CL_BEGIN_C_DECLS)
#  undef CL_BEGIN_C_DECLS
#endif
#if defined(CL_END_C_DECLS)
#  undef CL_END_C_DECLS
#endif
#if defined(CL_C_DECL)
#  undef CL_C_DECL
#endif
#if defined(__cplusplus)
#  define CL_BEGIN_C_DECLS extern "C" {
#  define CL_END_C_DECLS }
#  define CL_C_DECL extern "C"
#else
/**
 * Begins a C declaration in a C++ source file.
 */
#  define CL_BEGIN_C_DECLS
/**
 * Ends a C declaration in a C++ source file.
 */
#  define CL_END_C_DECLS
/**
 * Begins a single-line C declaration in a C++ source file.
 */
#  define CL_C_DECL
#endif

#undef CL_API
#if defined(CL_CORE_COMPILATION)
#  define CL_API HEDLEY_PUBLIC
#else
/**
 * Defines a macro for the exported and imported symbols.
 */
#  define CL_API HEDLEY_IMPORT
#endif
