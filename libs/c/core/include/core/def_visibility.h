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
#  define CL_BEGIN_C_DECLS
#  define CL_END_C_DECLS
#  define CL_C_DECL
#endif

#if defined(CL_COMPILATION)
#  define CL_API HEDLEY_PUBLIC
#else
#  define CL_API HEDLEY_IMPORT
#endif
