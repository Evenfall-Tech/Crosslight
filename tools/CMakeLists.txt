cmake_minimum_required(VERSION 3.19.0)

project("crosslight")

set(CL_BINARY_DIR ${PROJECT_BINARY_DIR})
set(CL_VERSION_MAJOR 0)
set(CL_VERSION_STRING 0.2)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_C_STANDARD   17)

set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/lib") # Compiled static libraries and *.lib
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/lib") # Compiled shared libraries
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/bin") # Compiled executables and *.dll

add_subdirectory("../libs/c" "libs/c")
add_subdirectory("../libs/cpp" "libs/cpp")
add_subdirectory("../libs/refs" "libs/refs")
add_subdirectory("../examples/c" "examples/c")
