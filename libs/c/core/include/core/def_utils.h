/**
 * @file core/def_utils.h
 * @version 15
 * @brief Hedley - C/C++ a header file designed to smooth over some platform-specific annoyances.
 * https://nemequ.github.io/hedley/
 * 
 * @author Evan Nemerson <evan@nemerson.com>
 * @copyright (c) 2021 Evan Nemerson
 *
 * This library is released under CC0-1.0 <http://creativecommons.org/publicdomain/zero/1.0/> license.
 */

#pragma once

#if defined(HEDLEY_VERSION_ENCODE)
#  undef HEDLEY_VERSION_ENCODE
#endif
#define HEDLEY_VERSION_ENCODE(major,minor,revision) (((major) * 1000000) + ((minor) * 1000) + (revision))

#if defined(HEDLEY_IAR_VERSION)
#  undef HEDLEY_IAR_VERSION
#endif
#if defined(__IAR_SYSTEMS_ICC__)
#  if __VER__ > 1000
#    define HEDLEY_IAR_VERSION HEDLEY_VERSION_ENCODE((__VER__ / 1000000), ((__VER__ / 1000) % 1000), (__VER__ % 1000))
#  else
#    define HEDLEY_IAR_VERSION HEDLEY_VERSION_ENCODE(__VER__ / 100, __VER__ % 100, 0)
#  endif
#endif

#if defined(HEDLEY_IAR_VERSION_CHECK)
#  undef HEDLEY_IAR_VERSION_CHECK
#endif
#if defined(HEDLEY_IAR_VERSION)
#  define HEDLEY_IAR_VERSION_CHECK(major,minor,patch) (HEDLEY_IAR_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_IAR_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_COMPCERT_VERSION)
#  undef HEDLEY_COMPCERT_VERSION
#endif
#if defined(__COMPCERT_VERSION__)
#  define HEDLEY_COMPCERT_VERSION HEDLEY_VERSION_ENCODE(__COMPCERT_VERSION__ / 10000, (__COMPCERT_VERSION__ / 100) % 100, __COMPCERT_VERSION__ % 100)
#endif

#if defined(HEDLEY_COMPCERT_VERSION_CHECK)
#  undef HEDLEY_COMPCERT_VERSION_CHECK
#endif
#if defined(HEDLEY_COMPCERT_VERSION)
#  define HEDLEY_COMPCERT_VERSION_CHECK(major,minor,patch) (HEDLEY_COMPCERT_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_COMPCERT_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_PELLES_VERSION)
#  undef HEDLEY_PELLES_VERSION
#endif
#if defined(__POCC__)
#  define HEDLEY_PELLES_VERSION HEDLEY_VERSION_ENCODE(__POCC__ / 100, __POCC__ % 100, 0)
#endif

#if defined(HEDLEY_PELLES_VERSION_CHECK)
#  undef HEDLEY_PELLES_VERSION_CHECK
#endif
#if defined(HEDLEY_PELLES_VERSION)
#  define HEDLEY_PELLES_VERSION_CHECK(major,minor,patch) (HEDLEY_PELLES_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_PELLES_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_GNUC_VERSION)
#  undef HEDLEY_GNUC_VERSION
#endif
#if defined(__GNUC__) && defined(__GNUC_PATCHLEVEL__)
#  define HEDLEY_GNUC_VERSION HEDLEY_VERSION_ENCODE(__GNUC__, __GNUC_MINOR__, __GNUC_PATCHLEVEL__)
#elif defined(__GNUC__)
#  define HEDLEY_GNUC_VERSION HEDLEY_VERSION_ENCODE(__GNUC__, __GNUC_MINOR__, 0)
#endif

#if defined(HEDLEY_GNUC_VERSION_CHECK)
#  undef HEDLEY_GNUC_VERSION_CHECK
#endif
#if defined(HEDLEY_GNUC_VERSION)
#  define HEDLEY_GNUC_VERSION_CHECK(major,minor,patch) (HEDLEY_GNUC_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_GNUC_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_MSVC_VERSION)
#  undef HEDLEY_MSVC_VERSION
#endif
#if defined(_MSC_FULL_VER) && (_MSC_FULL_VER >= 140000000) && !defined(__ICL)
#  define HEDLEY_MSVC_VERSION HEDLEY_VERSION_ENCODE(_MSC_FULL_VER / 10000000, (_MSC_FULL_VER % 10000000) / 100000, (_MSC_FULL_VER % 100000) / 100)
#elif defined(_MSC_FULL_VER) && !defined(__ICL)
#  define HEDLEY_MSVC_VERSION HEDLEY_VERSION_ENCODE(_MSC_FULL_VER / 1000000, (_MSC_FULL_VER % 1000000) / 10000, (_MSC_FULL_VER % 10000) / 10)
#elif defined(_MSC_VER) && !defined(__ICL)
#  define HEDLEY_MSVC_VERSION HEDLEY_VERSION_ENCODE(_MSC_VER / 100, _MSC_VER % 100, 0)
#endif

#if defined(HEDLEY_MSVC_VERSION_CHECK)
#  undef HEDLEY_MSVC_VERSION_CHECK
#endif
#if !defined(HEDLEY_MSVC_VERSION)
#  define HEDLEY_MSVC_VERSION_CHECK(major,minor,patch) (0)
#elif defined(_MSC_VER) && (_MSC_VER >= 1400)
#  define HEDLEY_MSVC_VERSION_CHECK(major,minor,patch) (_MSC_FULL_VER >= ((major * 10000000) + (minor * 100000) + (patch)))
#elif defined(_MSC_VER) && (_MSC_VER >= 1200)
#  define HEDLEY_MSVC_VERSION_CHECK(major,minor,patch) (_MSC_FULL_VER >= ((major * 1000000) + (minor * 10000) + (patch)))
#else
#  define HEDLEY_MSVC_VERSION_CHECK(major,minor,patch) (_MSC_VER >= ((major * 100) + (minor)))
#endif

#if defined(HEDLEY_INTEL_VERSION)
#  undef HEDLEY_INTEL_VERSION
#endif
#if defined(__INTEL_COMPILER) && defined(__INTEL_COMPILER_UPDATE) && !defined(__ICL)
#  define HEDLEY_INTEL_VERSION HEDLEY_VERSION_ENCODE(__INTEL_COMPILER / 100, __INTEL_COMPILER % 100, __INTEL_COMPILER_UPDATE)
#elif defined(__INTEL_COMPILER) && !defined(__ICL)
#  define HEDLEY_INTEL_VERSION HEDLEY_VERSION_ENCODE(__INTEL_COMPILER / 100, __INTEL_COMPILER % 100, 0)
#endif

#if defined(HEDLEY_INTEL_VERSION_CHECK)
#  undef HEDLEY_INTEL_VERSION_CHECK
#endif
#if defined(HEDLEY_INTEL_VERSION)
#  define HEDLEY_INTEL_VERSION_CHECK(major,minor,patch) (HEDLEY_INTEL_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_INTEL_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_INTEL_CL_VERSION)
#  undef HEDLEY_INTEL_CL_VERSION
#endif
#if defined(__INTEL_COMPILER) && defined(__INTEL_COMPILER_UPDATE) && defined(__ICL)
#  define HEDLEY_INTEL_CL_VERSION HEDLEY_VERSION_ENCODE(__INTEL_COMPILER, __INTEL_COMPILER_UPDATE, 0)
#endif

#if defined(HEDLEY_INTEL_CL_VERSION_CHECK)
#  undef HEDLEY_INTEL_CL_VERSION_CHECK
#endif
#if defined(HEDLEY_INTEL_CL_VERSION)
#  define HEDLEY_INTEL_CL_VERSION_CHECK(major,minor,patch) (HEDLEY_INTEL_CL_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_INTEL_CL_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_PGI_VERSION)
#  undef HEDLEY_PGI_VERSION
#endif
#if defined(__PGI) && defined(__PGIC__) && defined(__PGIC_MINOR__) && defined(__PGIC_PATCHLEVEL__)
#  define HEDLEY_PGI_VERSION HEDLEY_VERSION_ENCODE(__PGIC__, __PGIC_MINOR__, __PGIC_PATCHLEVEL__)
#endif

#if defined(HEDLEY_PGI_VERSION_CHECK)
#  undef HEDLEY_PGI_VERSION_CHECK
#endif
#if defined(HEDLEY_PGI_VERSION)
#  define HEDLEY_PGI_VERSION_CHECK(major,minor,patch) (HEDLEY_PGI_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_PGI_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_ARM_VERSION)
#  undef HEDLEY_ARM_VERSION
#endif
#if defined(__CC_ARM) && defined(__ARMCOMPILER_VERSION)
#  define HEDLEY_ARM_VERSION HEDLEY_VERSION_ENCODE(__ARMCOMPILER_VERSION / 1000000, (__ARMCOMPILER_VERSION % 1000000) / 10000, (__ARMCOMPILER_VERSION % 10000) / 100)
#elif defined(__CC_ARM) && defined(__ARMCC_VERSION)
#  define HEDLEY_ARM_VERSION HEDLEY_VERSION_ENCODE(__ARMCC_VERSION / 1000000, (__ARMCC_VERSION % 1000000) / 10000, (__ARMCC_VERSION % 10000) / 100)
#endif

#if defined(HEDLEY_ARM_VERSION_CHECK)
#  undef HEDLEY_ARM_VERSION_CHECK
#endif
#if defined(HEDLEY_ARM_VERSION)
#  define HEDLEY_ARM_VERSION_CHECK(major,minor,patch) (HEDLEY_ARM_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_ARM_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_CRAY_VERSION)
#  undef HEDLEY_CRAY_VERSION
#endif
#if defined(_CRAYC)
#  if defined(_RELEASE_PATCHLEVEL)
#    define HEDLEY_CRAY_VERSION HEDLEY_VERSION_ENCODE(_RELEASE_MAJOR, _RELEASE_MINOR, _RELEASE_PATCHLEVEL)
#  else
#    define HEDLEY_CRAY_VERSION HEDLEY_VERSION_ENCODE(_RELEASE_MAJOR, _RELEASE_MINOR, 0)
#  endif
#endif

#if defined(HEDLEY_CRAY_VERSION_CHECK)
#  undef HEDLEY_CRAY_VERSION_CHECK
#endif
#if defined(HEDLEY_CRAY_VERSION)
#  define HEDLEY_CRAY_VERSION_CHECK(major,minor,patch) (HEDLEY_CRAY_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_CRAY_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_VERSION)
#  undef HEDLEY_TI_VERSION
#endif
#if \
    defined(__TI_COMPILER_VERSION__) && \
    ( \
      defined(__TMS470__) || defined(__TI_ARM__) || \
      defined(__MSP430__) || \
      defined(__TMS320C2000__) \
    )
#  if (__TI_COMPILER_VERSION__ >= 16000000)
#    define HEDLEY_TI_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#  endif
#endif

#if defined(HEDLEY_TI_VERSION_CHECK)
#  undef HEDLEY_TI_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_VERSION)
#  define HEDLEY_TI_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_ARMCL_VERSION)
#  undef HEDLEY_TI_ARMCL_VERSION
#endif
#if defined(__TI_COMPILER_VERSION__) && (defined(__TMS470__) || defined(__TI_ARM__))
#  define HEDLEY_TI_ARMCL_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#endif

#if defined(HEDLEY_TI_ARMCL_VERSION_CHECK)
#  undef HEDLEY_TI_ARMCL_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_ARMCL_VERSION)
#  define HEDLEY_TI_ARMCL_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_ARMCL_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_ARMCL_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_CL430_VERSION)
#  undef HEDLEY_TI_CL430_VERSION
#endif
#if defined(__TI_COMPILER_VERSION__) && defined(__MSP430__)
#  define HEDLEY_TI_CL430_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#endif

#if defined(HEDLEY_TI_CL430_VERSION_CHECK)
#  undef HEDLEY_TI_CL430_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_CL430_VERSION)
#  define HEDLEY_TI_CL430_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_CL430_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_CL430_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_CL2000_VERSION)
#  undef HEDLEY_TI_CL2000_VERSION
#endif
#if defined(__TI_COMPILER_VERSION__) && defined(__TMS320C2000__)
#  define HEDLEY_TI_CL2000_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#endif

#if defined(HEDLEY_TI_CL2000_VERSION_CHECK)
#  undef HEDLEY_TI_CL2000_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_CL2000_VERSION)
#  define HEDLEY_TI_CL2000_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_CL2000_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_CL2000_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_CL6X_VERSION)
#  undef HEDLEY_TI_CL6X_VERSION
#endif
#if defined(__TI_COMPILER_VERSION__) && defined(__TMS320C6X__)
#  define HEDLEY_TI_CL6X_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#endif

#if defined(HEDLEY_TI_CL6X_VERSION_CHECK)
#  undef HEDLEY_TI_CL6X_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_CL6X_VERSION)
#  define HEDLEY_TI_CL6X_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_CL6X_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_CL6X_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_CL7X_VERSION)
#  undef HEDLEY_TI_CL7X_VERSION
#endif
#if defined(__TI_COMPILER_VERSION__) && defined(__C7000__)
#  define HEDLEY_TI_CL7X_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#endif

#if defined(HEDLEY_TI_CL7X_VERSION_CHECK)
#  undef HEDLEY_TI_CL7X_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_CL7X_VERSION)
#  define HEDLEY_TI_CL7X_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_CL7X_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_CL7X_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_TI_CLPRU_VERSION)
#  undef HEDLEY_TI_CLPRU_VERSION
#endif
#if defined(__TI_COMPILER_VERSION__) && defined(__PRU__)
#  define HEDLEY_TI_CLPRU_VERSION HEDLEY_VERSION_ENCODE(__TI_COMPILER_VERSION__ / 1000000, (__TI_COMPILER_VERSION__ % 1000000) / 1000, (__TI_COMPILER_VERSION__ % 1000))
#endif

#if defined(HEDLEY_TI_CLPRU_VERSION_CHECK)
#  undef HEDLEY_TI_CLPRU_VERSION_CHECK
#endif
#if defined(HEDLEY_TI_CLPRU_VERSION)
#  define HEDLEY_TI_CLPRU_VERSION_CHECK(major,minor,patch) (HEDLEY_TI_CLPRU_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_TI_CLPRU_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_MCST_LCC_VERSION)
#  undef HEDLEY_MCST_LCC_VERSION
#endif
#if defined(__LCC__) && defined(__LCC_MINOR__)
#  define HEDLEY_MCST_LCC_VERSION HEDLEY_VERSION_ENCODE(__LCC__ / 100, __LCC__ % 100, __LCC_MINOR__)
#endif

#if defined(HEDLEY_MCST_LCC_VERSION_CHECK)
#  undef HEDLEY_MCST_LCC_VERSION_CHECK
#endif
#if defined(HEDLEY_MCST_LCC_VERSION)
#  define HEDLEY_MCST_LCC_VERSION_CHECK(major,minor,patch) (HEDLEY_MCST_LCC_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_MCST_LCC_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_SUNPRO_VERSION)
#  undef HEDLEY_SUNPRO_VERSION
#endif
#if defined(__SUNPRO_C) && (__SUNPRO_C > 0x1000)
#  define HEDLEY_SUNPRO_VERSION HEDLEY_VERSION_ENCODE((((__SUNPRO_C >> 16) & 0xf) * 10) + ((__SUNPRO_C >> 12) & 0xf), (((__SUNPRO_C >> 8) & 0xf) * 10) + ((__SUNPRO_C >> 4) & 0xf), (__SUNPRO_C & 0xf) * 10)
#elif defined(__SUNPRO_C)
#  define HEDLEY_SUNPRO_VERSION HEDLEY_VERSION_ENCODE((__SUNPRO_C >> 8) & 0xf, (__SUNPRO_C >> 4) & 0xf, (__SUNPRO_C) & 0xf)
#elif defined(__SUNPRO_CC) && (__SUNPRO_CC > 0x1000)
#  define HEDLEY_SUNPRO_VERSION HEDLEY_VERSION_ENCODE((((__SUNPRO_CC >> 16) & 0xf) * 10) + ((__SUNPRO_CC >> 12) & 0xf), (((__SUNPRO_CC >> 8) & 0xf) * 10) + ((__SUNPRO_CC >> 4) & 0xf), (__SUNPRO_CC & 0xf) * 10)
#elif defined(__SUNPRO_CC)
#  define HEDLEY_SUNPRO_VERSION HEDLEY_VERSION_ENCODE((__SUNPRO_CC >> 8) & 0xf, (__SUNPRO_CC >> 4) & 0xf, (__SUNPRO_CC) & 0xf)
#endif

#if defined(HEDLEY_SUNPRO_VERSION_CHECK)
#  undef HEDLEY_SUNPRO_VERSION_CHECK
#endif
#if defined(HEDLEY_SUNPRO_VERSION)
#  define HEDLEY_SUNPRO_VERSION_CHECK(major,minor,patch) (HEDLEY_SUNPRO_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_SUNPRO_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_INTEL_VERSION)
#  undef HEDLEY_INTEL_VERSION
#endif
#if defined(__INTEL_COMPILER) && defined(__INTEL_COMPILER_UPDATE) && !defined(__ICL)
#  define HEDLEY_INTEL_VERSION HEDLEY_VERSION_ENCODE(__INTEL_COMPILER / 100, __INTEL_COMPILER % 100, __INTEL_COMPILER_UPDATE)
#elif defined(__INTEL_COMPILER) && !defined(__ICL)
#  define HEDLEY_INTEL_VERSION HEDLEY_VERSION_ENCODE(__INTEL_COMPILER / 100, __INTEL_COMPILER % 100, 0)
#endif

#if defined(HEDLEY_INTEL_VERSION_CHECK)
#  undef HEDLEY_INTEL_VERSION_CHECK
#endif
#if defined(HEDLEY_INTEL_VERSION)
#  define HEDLEY_INTEL_VERSION_CHECK(major,minor,patch) (HEDLEY_INTEL_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_INTEL_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_ARM_VERSION)
#  undef HEDLEY_ARM_VERSION
#endif
#if defined(__CC_ARM) && defined(__ARMCOMPILER_VERSION)
#  define HEDLEY_ARM_VERSION HEDLEY_VERSION_ENCODE(__ARMCOMPILER_VERSION / 1000000, (__ARMCOMPILER_VERSION % 1000000) / 10000, (__ARMCOMPILER_VERSION % 10000) / 100)
#elif defined(__CC_ARM) && defined(__ARMCC_VERSION)
#  define HEDLEY_ARM_VERSION HEDLEY_VERSION_ENCODE(__ARMCC_VERSION / 1000000, (__ARMCC_VERSION % 1000000) / 10000, (__ARMCC_VERSION % 10000) / 100)
#endif

#if defined(HEDLEY_ARM_VERSION_CHECK)
#  undef HEDLEY_ARM_VERSION_CHECK
#endif
#if defined(HEDLEY_ARM_VERSION)
#  define HEDLEY_ARM_VERSION_CHECK(major,minor,patch) (HEDLEY_ARM_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_ARM_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_IBM_VERSION)
#  undef HEDLEY_IBM_VERSION
#endif
#if defined(__ibmxl__)
#  define HEDLEY_IBM_VERSION HEDLEY_VERSION_ENCODE(__ibmxl_version__, __ibmxl_release__, __ibmxl_modification__)
#elif defined(__xlC__) && defined(__xlC_ver__)
#  define HEDLEY_IBM_VERSION HEDLEY_VERSION_ENCODE(__xlC__ >> 8, __xlC__ & 0xff, (__xlC_ver__ >> 8) & 0xff)
#elif defined(__xlC__)
#  define HEDLEY_IBM_VERSION HEDLEY_VERSION_ENCODE(__xlC__ >> 8, __xlC__ & 0xff, 0)
#endif

#if defined(HEDLEY_IBM_VERSION_CHECK)
#  undef HEDLEY_IBM_VERSION_CHECK
#endif
#if defined(HEDLEY_IBM_VERSION)
#  define HEDLEY_IBM_VERSION_CHECK(major,minor,patch) (HEDLEY_IBM_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_IBM_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_HAS_ATTRIBUTE)
#  undef HEDLEY_HAS_ATTRIBUTE
#endif
#if \
  defined(__has_attribute) && \
  ( \
    (!defined(HEDLEY_IAR_VERSION) || HEDLEY_IAR_VERSION_CHECK(8,5,9)) \
  )
#  define HEDLEY_HAS_ATTRIBUTE(attribute) __has_attribute(attribute)
#else
#  define HEDLEY_HAS_ATTRIBUTE(attribute) (0)
#endif

#if defined(HEDLEY_GCC_VERSION)
#  undef HEDLEY_GCC_VERSION
#endif
#if \
  defined(HEDLEY_GNUC_VERSION) && \
  !defined(__clang__) && \
  !defined(HEDLEY_INTEL_VERSION) && \
  !defined(HEDLEY_PGI_VERSION) && \
  !defined(HEDLEY_ARM_VERSION) && \
  !defined(HEDLEY_CRAY_VERSION) && \
  !defined(HEDLEY_TI_VERSION) && \
  !defined(HEDLEY_TI_ARMCL_VERSION) && \
  !defined(HEDLEY_TI_CL430_VERSION) && \
  !defined(HEDLEY_TI_CL2000_VERSION) && \
  !defined(HEDLEY_TI_CL6X_VERSION) && \
  !defined(HEDLEY_TI_CL7X_VERSION) && \
  !defined(HEDLEY_TI_CLPRU_VERSION) && \
  !defined(__COMPCERT__) && \
  !defined(HEDLEY_MCST_LCC_VERSION)
#  define HEDLEY_GCC_VERSION HEDLEY_GNUC_VERSION
#endif

#if defined(HEDLEY_GCC_VERSION_CHECK)
#  undef HEDLEY_GCC_VERSION_CHECK
#endif
#if defined(HEDLEY_GCC_VERSION)
#  define HEDLEY_GCC_VERSION_CHECK(major,minor,patch) (HEDLEY_GCC_VERSION >= HEDLEY_VERSION_ENCODE(major, minor, patch))
#else
#  define HEDLEY_GCC_VERSION_CHECK(major,minor,patch) (0)
#endif

#if defined(HEDLEY_PRIVATE)
#  undef HEDLEY_PRIVATE
#endif
#if defined(HEDLEY_PUBLIC)
#  undef HEDLEY_PUBLIC
#endif
#if defined(HEDLEY_IMPORT)
#  undef HEDLEY_IMPORT
#endif
#if defined(_WIN32) || defined(__CYGWIN__)
#  define HEDLEY_PRIVATE
#  define HEDLEY_PUBLIC   __declspec(dllexport)
#  define HEDLEY_IMPORT   __declspec(dllimport)
#else
#  if \
    HEDLEY_HAS_ATTRIBUTE(visibility) || \
    HEDLEY_GCC_VERSION_CHECK(3,3,0) || \
    HEDLEY_SUNPRO_VERSION_CHECK(5,11,0) || \
    HEDLEY_INTEL_VERSION_CHECK(13,0,0) || \
    HEDLEY_ARM_VERSION_CHECK(4,1,0) || \
    HEDLEY_IBM_VERSION_CHECK(13,1,0) || \
    ( \
      defined(__TI_EABI__) && \
      ( \
        (HEDLEY_TI_CL6X_VERSION_CHECK(7,2,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
        HEDLEY_TI_CL6X_VERSION_CHECK(7,5,0) \
      ) \
    ) || \
    HEDLEY_MCST_LCC_VERSION_CHECK(1,25,10)
#    define HEDLEY_PRIVATE __attribute__((__visibility__("hidden")))
#    define HEDLEY_PUBLIC  __attribute__((__visibility__("default")))
#  else
#    define HEDLEY_PRIVATE
#    define HEDLEY_PUBLIC
#  endif
#  define HEDLEY_IMPORT    extern
#endif

#if defined(HEDLEY_INLINE)
#  undef HEDLEY_INLINE
#endif
#if \
  (defined(__STDC_VERSION__) && (__STDC_VERSION__ >= 199901L)) || \
  (defined(__cplusplus) && (__cplusplus >= 199711L))
#  define HEDLEY_INLINE inline
#elif \
  defined(HEDLEY_GCC_VERSION) || \
  HEDLEY_ARM_VERSION_CHECK(6,2,0)
#  define HEDLEY_INLINE __inline__
#elif \
  HEDLEY_MSVC_VERSION_CHECK(12,0,0) || \
  HEDLEY_INTEL_CL_VERSION_CHECK(2021,1,0) || \
  HEDLEY_ARM_VERSION_CHECK(4,1,0) || \
  HEDLEY_TI_ARMCL_VERSION_CHECK(5,1,0) || \
  HEDLEY_TI_CL430_VERSION_CHECK(3,1,0) || \
  HEDLEY_TI_CL2000_VERSION_CHECK(6,2,0) || \
  HEDLEY_TI_CL6X_VERSION_CHECK(8,0,0) || \
  HEDLEY_TI_CL7X_VERSION_CHECK(1,2,0) || \
  HEDLEY_TI_CLPRU_VERSION_CHECK(2,1,0) || \
  HEDLEY_MCST_LCC_VERSION_CHECK(1,25,10)
#  define HEDLEY_INLINE __inline
#else
#  define HEDLEY_INLINE
#endif

#if defined(HEDLEY_ALWAYS_INLINE)
#  undef HEDLEY_ALWAYS_INLINE
#endif
#if \
  HEDLEY_HAS_ATTRIBUTE(always_inline) || \
  HEDLEY_GCC_VERSION_CHECK(4,0,0) || \
  HEDLEY_INTEL_VERSION_CHECK(13,0,0) || \
  HEDLEY_SUNPRO_VERSION_CHECK(5,11,0) || \
  HEDLEY_ARM_VERSION_CHECK(4,1,0) || \
  HEDLEY_IBM_VERSION_CHECK(10,1,0) || \
  HEDLEY_TI_VERSION_CHECK(15,12,0) || \
  (HEDLEY_TI_ARMCL_VERSION_CHECK(4,8,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_ARMCL_VERSION_CHECK(5,2,0) || \
  (HEDLEY_TI_CL2000_VERSION_CHECK(6,0,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_CL2000_VERSION_CHECK(6,4,0) || \
  (HEDLEY_TI_CL430_VERSION_CHECK(4,0,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_CL430_VERSION_CHECK(4,3,0) || \
  (HEDLEY_TI_CL6X_VERSION_CHECK(7,2,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_CL6X_VERSION_CHECK(7,5,0) || \
  HEDLEY_TI_CL7X_VERSION_CHECK(1,2,0) || \
  HEDLEY_TI_CLPRU_VERSION_CHECK(2,1,0) || \
  HEDLEY_MCST_LCC_VERSION_CHECK(1,25,10) || \
  HEDLEY_IAR_VERSION_CHECK(8,10,0)
#  define HEDLEY_ALWAYS_INLINE __attribute__((__always_inline__)) HEDLEY_INLINE
#elif \
  HEDLEY_MSVC_VERSION_CHECK(12,0,0) || \
  HEDLEY_INTEL_CL_VERSION_CHECK(2021,1,0)
#  define HEDLEY_ALWAYS_INLINE __forceinline
#elif defined(__cplusplus) && \
    ( \
      HEDLEY_TI_ARMCL_VERSION_CHECK(5,2,0) || \
      HEDLEY_TI_CL430_VERSION_CHECK(4,3,0) || \
      HEDLEY_TI_CL2000_VERSION_CHECK(6,4,0) || \
      HEDLEY_TI_CL6X_VERSION_CHECK(6,1,0) || \
      HEDLEY_TI_CL7X_VERSION_CHECK(1,2,0) || \
      HEDLEY_TI_CLPRU_VERSION_CHECK(2,1,0) \
    )
#  define HEDLEY_ALWAYS_INLINE _Pragma("FUNC_ALWAYS_INLINE;")
#elif HEDLEY_IAR_VERSION_CHECK(8,0,0)
#  define HEDLEY_ALWAYS_INLINE _Pragma("inline=forced")
#else
#  define HEDLEY_ALWAYS_INLINE HEDLEY_INLINE
#endif

#if defined(HEDLEY_NEVER_INLINE)
#  undef HEDLEY_NEVER_INLINE
#endif
#if \
  HEDLEY_HAS_ATTRIBUTE(noinline) || \
  HEDLEY_GCC_VERSION_CHECK(4,0,0) || \
  HEDLEY_INTEL_VERSION_CHECK(13,0,0) || \
  HEDLEY_SUNPRO_VERSION_CHECK(5,11,0) || \
  HEDLEY_ARM_VERSION_CHECK(4,1,0) || \
  HEDLEY_IBM_VERSION_CHECK(10,1,0) || \
  HEDLEY_TI_VERSION_CHECK(15,12,0) || \
  (HEDLEY_TI_ARMCL_VERSION_CHECK(4,8,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_ARMCL_VERSION_CHECK(5,2,0) || \
  (HEDLEY_TI_CL2000_VERSION_CHECK(6,0,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_CL2000_VERSION_CHECK(6,4,0) || \
  (HEDLEY_TI_CL430_VERSION_CHECK(4,0,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_CL430_VERSION_CHECK(4,3,0) || \
  (HEDLEY_TI_CL6X_VERSION_CHECK(7,2,0) && defined(__TI_GNU_ATTRIBUTE_SUPPORT__)) || \
  HEDLEY_TI_CL6X_VERSION_CHECK(7,5,0) || \
  HEDLEY_TI_CL7X_VERSION_CHECK(1,2,0) || \
  HEDLEY_TI_CLPRU_VERSION_CHECK(2,1,0) || \
  HEDLEY_MCST_LCC_VERSION_CHECK(1,25,10) || \
  HEDLEY_IAR_VERSION_CHECK(8,10,0)
#  define HEDLEY_NEVER_INLINE __attribute__((__noinline__))
#elif \
  HEDLEY_MSVC_VERSION_CHECK(13,10,0) || \
  HEDLEY_INTEL_CL_VERSION_CHECK(2021,1,0)
#  define HEDLEY_NEVER_INLINE __declspec(noinline)
#elif HEDLEY_PGI_VERSION_CHECK(10,2,0)
#  define HEDLEY_NEVER_INLINE _Pragma("noinline")
#elif HEDLEY_TI_CL6X_VERSION_CHECK(6,0,0) && defined(__cplusplus)
#  define HEDLEY_NEVER_INLINE _Pragma("FUNC_CANNOT_INLINE;")
#elif HEDLEY_IAR_VERSION_CHECK(8,0,0)
#  define HEDLEY_NEVER_INLINE _Pragma("inline=never")
#elif HEDLEY_COMPCERT_VERSION_CHECK(3,2,0)
#  define HEDLEY_NEVER_INLINE __attribute((noinline))
#elif HEDLEY_PELLES_VERSION_CHECK(9,0,0)
#  define HEDLEY_NEVER_INLINE __declspec(noinline)
#else
#  define HEDLEY_NEVER_INLINE
#endif
