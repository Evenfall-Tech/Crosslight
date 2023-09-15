cmake_minimum_required(VERSION 3.14)

set(PROJECT_NAME "crosslight_core")
set(BINARY_NAME "cl_core")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})

project(
    "${PROJECT_NAME}"
    VERSION ${LIBRARY_VERSION_STRING}
    LANGUAGES C
)

include(GNUInstallDirs)

set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")



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
    "${LIBRARY_BASE_PATH}/include/core/node/types.h"
    "${LIBRARY_BASE_PATH}/include/core/node/source_root.h"
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
add_library(CL::${BINARY_NAME} ALIAS ${BINARY_NAME})

target_include_directories(
    ${BINARY_NAME}
    PUBLIC "${LIBRARY_BASE_PATH}/include"
    PRIVATE "${LIBRARY_BASE_PATH}/private_include"
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	            ${LIBRARY_VERSION_STRING}
    SOVERSION           ${LIBRARY_VERSION_MAJOR}
    FOLDER              ${CL_FOLDER_RUNTIME}
    target_lang         "${LIBRARY_BASE_PATH}/include/core/language.h"
)

target_compile_features(${BINARY_NAME} PUBLIC c_std_17)
set_target_properties(${BINARY_NAME} PROPERTIES C_EXTENSIONS OFF)

install(
    TARGETS ${BINARY_NAME}
    LIBRARY DESTINATION "${CMAKE_INSTALL_LIBDIR}" COMPONENT lib
    ARCHIVE DESTINATION "${CMAKE_INSTALL_LIBDIR}" COMPONENT lib
    RUNTIME DESTINATION "${CMAKE_INSTALL_BINDIR}" COMPONENT bin
)
install(
    DIRECTORY "${LIBRARY_BASE_PATH}/include/core"
    DESTINATION "${CMAKE_INSTALL_INCLUDEDIR}"
    COMPONENT dev
    FILES_MATCHING
    PATTERN "*.h"
)
