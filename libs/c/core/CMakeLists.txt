cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "crosslight_core")
set(BINARY_NAME "cl_core")

project("${PROJECT_NAME}" C)

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})

include(GNUInstallDirs)

set(LIBRARY_BASE_PATH "${PROJECT_SOURCE_DIR}")



set(PUBLIC_SOURCES_FILES
    "${LIBRARY_BASE_PATH}/src/node.c"
    "${LIBRARY_BASE_PATH}/src/config.c"
)

set(PRIVATE_SOURCES_FILES
    "${LIBRARY_BASE_PATH}/private_src/utils.c"
)

set(PUBLIC_HEADERS_FILES
    "${LIBRARY_BASE_PATH}/include/core/def_platforms.h"
    "${LIBRARY_BASE_PATH}/include/core/def_utils.h"
    "${LIBRARY_BASE_PATH}/include/core/def_visibility.h"
    "${LIBRARY_BASE_PATH}/include/core/definitions.h"
    "${LIBRARY_BASE_PATH}/include/core/message.h"
    "${LIBRARY_BASE_PATH}/include/core/resource.h"
    "${LIBRARY_BASE_PATH}/include/core/result.h"
    "${LIBRARY_BASE_PATH}/include/core/config.h"
    "${LIBRARY_BASE_PATH}/include/core/node.h"
)

set(PRIVATE_HEADERS_FILES
    "${LIBRARY_BASE_PATH}/private_include/core/utils.h"
)

# set(CMAKE_C_VISIBILITY_PRESET hidden)

add_library(
    ${BINARY_NAME} SHARED
    ${PUBLIC_SOURCES_FILES}
    ${PRIVATE_SOURCES_FILES}
    ${PUBLIC_HEADERS_FILES}
    ${PRIVATE_HEADERS_FILES}
)

target_include_directories(
    ${BINARY_NAME}
    PUBLIC "${LIBRARY_BASE_PATH}/include"
    PRIVATE "${LIBRARY_BASE_PATH}/private_include"
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	            ${LIBRARY_VERSION_STRING}
    SOVERSION           ${LIBRARY_VERSION_MAJOR}
    C_STANDARD          17
    C_STANDARD_REQUIRED ON
    PUBLIC_HEADER       "${PUBLIC_HEADERS_FILES}"
    target_lang         "${LIBRARY_BASE_PATH}/include/core/language.h"
)

install(
    TARGETS ${BINARY_NAME}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    PUBLIC_HEADER DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}
)
