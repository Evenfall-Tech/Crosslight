# minimum required CMAKE version
cmake_minimum_required(VERSION 3.7 FATAL_ERROR)

set(PROJECT_NAME "crosslight_lang")
set(BINARY_NAME "cl_lang")

project("${PROJECT_NAME}")

list(APPEND CMAKE_MODULE_PATH ${CMAKE_CURRENT_SOURCE_DIR}/cmake)

# compiler must be 17
set(CMAKE_CXX_STANDARD 17)

# required if linking to static library
add_definitions(-DANTLR4CPP_STATIC)

# using /MD flag for antlr4_runtime (for Visual C++ compilers only)
set(ANTLR4_WITH_STATIC_CRT OFF)

# Specify the version of the antlr4 library needed for this project.
# By default the latest version of antlr4 will be used.  You can specify a
# specific, stable version by setting a repository tag value or a link
# to a zip file containing the libary source.
# set(ANTLR4_TAG 4.13.0)
# set(ANTLR4_ZIP_REPOSITORY https://github.com/antlr/antlr4/archive/refs/tags/4.13.0.zip)

# add external build for antlrcpp
include(ExternalAntlr4Cpp)
# add antrl4cpp artifacts to project environment
include_directories(${ANTLR4_INCLUDE_DIRS})

# set variable pointing to the antlr tool that supports C++
# this is not required if the jar file can be found under PATH environment
set(ANTLR_EXECUTABLE ${CMAKE_CURRENT_SOURCE_DIR}/vendor/antlr/antlr-4.13.0-complete.jar)
# add macros to generate ANTLR Cpp code from grammar
find_package(ANTLR REQUIRED)

# add generated grammar to demo binary target
add_library(
  ${BINARY_NAME} INTERFACE)

target_link_libraries(${BINARY_NAME} INTERFACE antlr4_static)
target_include_directories(${BINARY_NAME} INTERFACE "${PROJECT_SOURCE_DIR}/include")
set_target_properties(
  ${BINARY_NAME} PROPERTIES
  target_antlr_executable ${ANTLR_EXECUTABLE}
  target_antlr_dirs "${ANTLR4_INCLUDE_DIRS}")
