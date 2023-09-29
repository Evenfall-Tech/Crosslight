cmake_minimum_required(VERSION 3.14)

set(PROJECT_NAME "Example Translate")
set(BINARY_NAME "translate")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

project(
    "${PROJECT_NAME}"
    VERSION ${LIBRARY_VERSION_STRING}
)

add_custom_target(${BINARY_NAME}
    COMMAND ${CL_DOTNET_COMMAND} publish Crosslight.Examples.Translate.csproj
        -c "$<CONFIG>"
        -r ${CL_DOTNET_RID}
        --self-contained
        -o "${CL_BINARY_DIR}/$<CONFIG>/examples"
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
#    VERSION                  ${LIBRARY_VERSION_STRING} # produces clang error on macos
#    SOVERSION                ${LIBRARY_VERSION_MAJOR} # clang-14: error: invalid argument '-current_version 0.1.0' only allowed with '-dynamiclib'
    FOLDER                   ${CL_FOLDER_EXAMPLES}
)
