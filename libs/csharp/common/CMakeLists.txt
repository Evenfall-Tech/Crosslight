cmake_minimum_required(VERSION 3.14)

set(PROJECT_NAME "crosslight_csharp")
set(BINARY_NAME "cl_csharp")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

project(
    "${PROJECT_NAME}"
    VERSION ${LIBRARY_VERSION_STRING}
)

add_custom_target(${BINARY_NAME}
    COMMAND ${CL_DOTNET_COMMAND} publish 
        -c "$<CONFIG>"
        -r ${CL_DOTNET_RID}
        -o "${CMAKE_ARCHIVE_OUTPUT_DIRECTORY}"
        -p:AssemblyName=${CMAKE_SHARED_LIBRARY_PREFIX}${BINARY_NAME}
    WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
#    VERSION                  ${LIBRARY_VERSION_STRING} # produces clang error on macos
#    SOVERSION                ${LIBRARY_VERSION_MAJOR} # clang-14: error: invalid argument '-current_version 0.1.0' only allowed with '-dynamiclib'
    FOLDER                   ${CL_FOLDER_PLUGINS}
)

install(
    PROGRAMS "${CMAKE_ARCHIVE_OUTPUT_DIRECTORY}/${CMAKE_SHARED_LIBRARY_PREFIX}${BINARY_NAME}${CMAKE_SHARED_LIBRARY_SUFFIX}"
    DESTINATION "${CMAKE_INSTALL_LIBDIR}" COMPONENT lib
)

install(
    FILES "${CMAKE_CURRENT_SOURCE_DIR}/bin/$<CONFIG>/net7.0/${CL_DOTNET_RID}/${CMAKE_SHARED_LIBRARY_PREFIX}${BINARY_NAME}.pdb"
    DESTINATION "${CMAKE_INSTALL_LIBDIR}"
    CONFIGURATIONS Debug
    COMPONENT lib
)
