# minimum required CMAKE version
cmake_minimum_required(VERSION 3.7 FATAL_ERROR)

set(BINARY_NAME "cl_lang")
set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

include(GNUInstallDirs)

list(APPEND CMAKE_MODULE_PATH ${LIBRARY_BASE_PATH}/cmake)

set(CMAKE_CXX_STANDARD 17)

# using /MD flag for antlr4_runtime (for Visual C++ compilers only)
set(ANTLR4_WITH_STATIC_CRT OFF)
# add external build for antlrcpp
include(ExternalAntlr4Cpp)
# add antrl4cpp artifacts to project environment
include_directories(${ANTLR4_INCLUDE_DIRS})
set(ANTLR_EXECUTABLE ${LIBRARY_BASE_PATH}/vendor/antlr/antlr-4.13.0-complete.jar)
find_package(ANTLR REQUIRED)

set(PUBLIC_HEADERS_FILES
    "${LIBRARY_BASE_PATH}/include/lang/plugin.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/config.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/language.hpp"
)

# add generated grammar to demo binary target
add_library(
    ${BINARY_NAME} SHARED
    ${PUBLIC_HEADERS_FILES}
    "${LIBRARY_BASE_PATH}/src/language.cpp"
    "${LIBRARY_BASE_PATH}/src/config.cpp"
)
add_library(CL::${BINARY_NAME} ALIAS ${BINARY_NAME})

target_link_libraries(
    ${BINARY_NAME}
    INTERFACE antlr4_shared
    PUBLIC cl_core
)
target_include_directories(${BINARY_NAME}
    INTERFACE ${ANTLR4_INCLUDE_DIRS}
    PUBLIC "${LIBRARY_BASE_PATH}/include"
)
set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	                ${LIBRARY_VERSION_STRING}
    SOVERSION               ${LIBRARY_VERSION_MAJOR}
    FOLDER                  ${CL_FOLDER_RUNTIME}
    PUBLIC_HEADER           "${PUBLIC_HEADERS_FILES}"
    target_antlr_executable ${ANTLR_EXECUTABLE}
    target_antlr_libraries  "${ANTLR4_RUNTIME_LIBRARIES}"
)

target_compile_features(${BINARY_NAME} PUBLIC cxx_std_17)
set_target_properties(${BINARY_NAME} PROPERTIES CXX_EXTENSIONS OFF)

add_custom_command(TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy ${ANTLR4_RUNTIME_LIBRARIES} .
    WORKING_DIRECTORY ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY})

install(
    TARGETS ${BINARY_NAME}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    PUBLIC_HEADER DESTINATION ${CMAKE_INSTALL_INCLUDEDIR}
)
