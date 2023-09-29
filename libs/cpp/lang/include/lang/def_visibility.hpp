#pragma once

#include "core/def_visibility.h"

#undef CL_API
#if defined(CL_LANG_COMPILATION)
#  define CL_API HEDLEY_PUBLIC
#else
/**
 * Defines a macro for the exported and imported symbols.
 */
#  define CL_API HEDLEY_IMPORT
#endif
