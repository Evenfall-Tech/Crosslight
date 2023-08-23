cmake_minimum_required(VERSION 3.19.0)

project("crosslight")

set(CL_BINARY_DIR ${CMAKE_CURRENT_BINARY_DIR})

add_subdirectory("../libs/c" "libs/c")
add_subdirectory("../libs/cpp" "libs/cpp")
add_subdirectory("../examples/c" "examples/c")