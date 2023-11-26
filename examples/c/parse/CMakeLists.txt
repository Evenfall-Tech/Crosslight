cmake_minimum_required(VERSION 3.14)

set(PROJECT_NAME "Example Parse")
set(BINARY_NAME "parse")

project("${PROJECT_NAME}" LANGUAGES C)

set(APP_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

file(
    GLOB SOURCES
    "${APP_BASE_PATH}/include/*.h"
    "${APP_BASE_PATH}/src/*.c"
)

add_executable(
    ${BINARY_NAME} ${SOURCES}
)

target_include_directories(${BINARY_NAME} PRIVATE "${APP_BASE_PATH}/include")
target_link_libraries(${BINARY_NAME} PRIVATE cl_core cl_lang)

add_dependencies(${BINARY_NAME} cl_lang_base_antlr cl_lang_ecmascript cl_lang_csharp_ref)

get_target_property(ANTLR_RUNTIME_LIBRARIES cl_lang_base_antlr target_antlr_libraries)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    FOLDER                   ${CL_FOLDER_EXAMPLES}
    RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)

set(
    dependencies
    $<TARGET_FILE:cl_core> ${ANTLR_RUNTIME_LIBRARIES} $<TARGET_FILE:cl_lang>  $<TARGET_FILE:cl_lang_base_antlr>
    #$<TARGET_FILE:cl_lang_ecmascript>
    #"${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins/${CMAKE_SHARED_LIBRARY_PREFIX}cl_lang_csharp_ref${CMAKE_SHARED_LIBRARY_SUFFIX}"
)

add_custom_command(
    TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy ${dependencies} .
    WORKING_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)

add_custom_command(
    TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:cl_core> ./${CMAKE_SHARED_LIBRARY_PREFIX}cl_core${CMAKE_SHARED_LIBRARY_SUFFIX}
    WORKING_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
)
add_custom_command(
    TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:cl_lang> ./${CMAKE_SHARED_LIBRARY_PREFIX}cl_lang${CMAKE_SHARED_LIBRARY_SUFFIX}
    WORKING_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
)
