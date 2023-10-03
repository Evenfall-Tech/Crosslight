#pragma once

#include "core/def_visibility.h"

#undef CL_API
#if defined(CL_LANG_COMPILATION)
#  define CL_API HEDLEY_PUBLIC
#  define CL_API_OBJ HEDLEY_PUBLIC
#else
/**
 * Defines a macro for the exported and imported symbols.
 */
#  define CL_API HEDLEY_IMPORT
/**
 * Defines a macro for the exported and imported C++ classes and structs.
 */
#  if defined(_WIN32) || defined(__CYGWIN__)
#    define CL_API_OBJ HEDLEY_IMPORT
#  else
#    define CL_API_OBJ
#  endif
#endif
