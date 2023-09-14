cmake_minimum_required(VERSION 3.14)

set(PROJECT_NAME "crosslight_lang_csharp_ref")
set(BINARY_NAME "cl_lang_csharp_ref")

project("${PROJECT_NAME}")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

add_custom_target(${BINARY_NAME}
    COMMAND ${CL_DOTNET_COMMAND} publish 
        -c "$<CONFIG>"
        -r ${CL_DOTNET_RID}
        -o "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
#    VERSION                  ${LIBRARY_VERSION_STRING} # produces clang error on macos
#    SOVERSION                ${LIBRARY_VERSION_MAJOR} # clang-14: error: invalid argument '-current_version 0.1.0' only allowed with '-dynamiclib'
    FOLDER                   ${CL_FOLDER_PLUGINS}
    RUNTIME_OUTPUT_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
    LIBRARY_OUTPUT_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
)
