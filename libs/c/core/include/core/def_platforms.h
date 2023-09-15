/**
 * @file core/def_platforms.h
 * @version 1
 * @brief Pre-defined Compiler Macros Wiki
 * https://github.com/cpredef/predef/
 * 
 * @author Alibek Omarov <a1ba.omarov@gmail.com>
 * @copyright (c) 2022 Alibek Omarov
 *
 * This library is released under CC-BY 4.0 <https://creativecommons.org/licenses/by/4.0/> license.
 */

#pragma once

#if defined(_WIN32) || defined(__WIN32__) || \
    defined(__TOS_WIN__) || defined(__WINDOWS__) || \
    defined(__CYGWIN__)
#  define CL_WINDOWS 1
#else
/**
 * Set to 1 if the target OS is from the Windows family, otherwise 0.
 */
#  define CL_WINDOWS 0
#endif

#if defined(__unix__) || defined(__unix)
#  define CL_UNIX 1
#else
/**
 * Set to 1 if the target OS is from the Unix family, otherwise 0.
 */
#  define CL_UNIX 0
#endif

#if defined(__linux__) || defined(linux) || defined(__linux)
#  define CL_LINUX 1
#else
/**
 * Set to 1 if the target OS is from the Linux family, otherwise 0.
 */
#  define CL_LINUX 0
#endif

#if defined(__APPLE__) || defined(__MACH__)
#  define CL_MACOS 1
#else
/**
 * Set to 1 if the target OS is from the MacOS family, otherwise 0.
 */
#  define CL_MACOS 0
#endif
