#pragma once

/* Using information from https://sourceforge.net/p/predef/wiki/OperatingSystems/
 */

#if defined(_WIN32) || defined(__WIN32__) || \
    defined(__TOS_WIN__) || defined(__WINDOWS__) || \
    defined(__CYGWIN__)
#  define CL_WINDOWS 1
#else
#  define CL_WINDOWS 0
#endif

#if defined(__unix__) || defined(__unix)
#  define CL_UNIX 1
#else
#  define CL_UNIX 0
#endif

#if defined(__linux__) || defined(linux) || defined(__linux)
#  define CL_LINUX 1
#else
#  define CL_LINUX 0
#endif

#if defined(__APPLE__) || defined(__MACH__)
#  define CL_MACOS 1
#else
#  define CL_MACOS 0
#endif
