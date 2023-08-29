cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "crosslight_lang_csharp_ref")
set(BINARY_NAME "cl_lang_csharp_ref")

project("${PROJECT_NAME}")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})

add_custom_target(${BINARY_NAME}
    COMMAND ${CL_DOTNET_COMMAND} publish 
        -c "$<CONFIG>"
        -r ${CL_DOTNET_RID}
        -o "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
)
