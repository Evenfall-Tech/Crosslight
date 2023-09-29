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
    "${LIBRARY_BASE_PATH}/include/core/nodes/node_type.h"
    "${LIBRARY_BASE_PATH}/include/core/nodes/source_root.h"
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
    PUBLIC $<BUILD_INTERFACE:${LIBRARY_BASE_PATH}/include> $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>
    PRIVATE "${LIBRARY_BASE_PATH}/private_include"
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	            ${LIBRARY_VERSION_STRING}
    SOVERSION           ${LIBRARY_VERSION_MAJOR}
    FOLDER              ${CL_FOLDER_RUNTIME}
    target_lang         "${LIBRARY_BASE_PATH}/include/core/language.h"
)

target_compile_definitions(${BINARY_NAME} PRIVATE CL_CORE_COMPILATION)
target_compile_features(${BINARY_NAME} PUBLIC c_std_17)
set_target_properties(${BINARY_NAME} PROPERTIES C_EXTENSIONS OFF)

list(APPEND CL_INSTALLABLE_LIBS ${BINARY_NAME})
set(CL_INSTALLABLE_LIBS ${CL_INSTALLABLE_LIBS} CACHE INTERNAL "")
install(
    DIRECTORY "${LIBRARY_BASE_PATH}/include/core"
    DESTINATION "${CMAKE_INSTALL_INCLUDEDIR}"
    COMPONENT dev
    FILES_MATCHING
    PATTERN "*.h"
)

if(MSVC)
    install(
        FILES "$<TARGET_FILE_DIR:${BINARY_NAME}>/${CMAKE_SHARED_LIBRARY_PREFIX}${BINARY_NAME}.pdb"
        DESTINATION "${CMAKE_INSTALL_LIBDIR}"
        CONFIGURATIONS Debug
        COMPONENT lib
    )
endif()
