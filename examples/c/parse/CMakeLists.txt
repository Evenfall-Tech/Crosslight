cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "Example Parse")
set(BINARY_NAME "parse")

project("${PROJECT_NAME}" LANGUAGES CXX)

set(APP_BASE_PATH "${PROJECT_SOURCE_DIR}")

file(
    GLOB SOURCES
    "${APP_BASE_PATH}/src/*.cpp"
)

add_executable(
    ${BINARY_NAME} ${SOURCES}
)

target_link_libraries(${BINARY_NAME} PRIVATE cl_core cl_lang)

add_dependencies(${BINARY_NAME} cl_lang_typescript)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)

add_custom_command(
    TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:cl_core> $<TARGET_FILE:cl_lang_typescript> .
    WORKING_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)
