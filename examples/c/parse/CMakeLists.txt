cmake_minimum_required(VERSION 3.14)

set(PROJECT_NAME "Example Parse")
set(BINARY_NAME "parse")

project("${PROJECT_NAME}" LANGUAGES C)

set(APP_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

file(
    GLOB SOURCES
    "${APP_BASE_PATH}/src/*.c"
)

add_executable(
    ${BINARY_NAME} ${SOURCES}
)

target_link_libraries(${BINARY_NAME} PRIVATE cl_core)

add_dependencies(${BINARY_NAME} cl_lang cl_lang_typescript)

get_target_property(ANTLR_RUNTIME_LIBRARIES cl_lang target_antlr_libraries)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    FOLDER                   ${CL_FOLDER_EXAMPLES}
    RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)

set(dependencies $<TARGET_FILE:cl_core> ${ANTLR_RUNTIME_LIBRARIES} $<TARGET_FILE:cl_lang> $<TARGET_FILE:cl_lang_typescript>)

add_custom_command(
    TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy ${dependencies} .
    WORKING_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)
