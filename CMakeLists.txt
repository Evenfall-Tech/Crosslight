cmake_minimum_required(VERSION 3.14)

set(CL_BINARY_DIR ${CMAKE_CURRENT_BINARY_DIR})
set(CL_VERSION_MAJOR 0)
set(CL_VERSION_STRING 0.2)

project(
    "Crosslight"
    VERSION ${CL_VERSION_STRING}
    DESCRIPTION "A language translation/interpretation engine for cross-language compilation/execution."
    HOMEPAGE_URL "https://github.com/Evenfall-Tech/Crosslight"
)

set(CL_BUILD_TESTS ON CACHE BOOL "Should Crosslight tests be built?")
set(CL_BUILD_EXAMPLES ON CACHE BOOL "Should Crosslight examples be built?")
set(CL_BUILD_DOCS ON CACHE BOOL "Should Crosslight docs be built?")
set(CL_BUILD_CSHARP ON CACHE BOOL "Should Crosslight C# port and reference be built?")

if(CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME)
    set_property(GLOBAL PROPERTY USE_FOLDERS ON)
    set(CL_FOLDER_PREFIX "Crosslight ")

    set(CMAKE_LIBRARY_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/lib") # Compiled static libraries and *.lib
    set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/lib") # Compiled shared libraries
    set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/bin") # Compiled executables and *.dll

    # CMAKE_RUNTIME_OUTPUT_DIRECTORY also used in plugins for copying plugins into a separate directory.
    # CMAKE_ARCHIVE_OUTPUT_DIRECTORY also used in lang for copying ANTLR4 libraries to output.
endif()

set(CL_FOLDER_RUNTIME  "${CL_FOLDER_PREFIX}Runtime")
set(CL_FOLDER_PLUGINS  "${CL_FOLDER_PREFIX}Plugins")
set(CL_FOLDER_EXAMPLES "${CL_FOLDER_PREFIX}Examples")
set(CL_FOLDER_TESTS    "${CL_FOLDER_PREFIX}Tests")

include(GNUInstallDirs)
include(FetchContent)

if(CL_BUILD_TESTS OR CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME)
    include(CTest)
endif()

if(CL_BUILD_DOCS OR CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME)
    find_package(Doxygen)
    if(Doxygen_FOUND)
        add_subdirectory("docs")
    else()
        message(STATUS "Doxygen not found, not building docs.")
    endif()
endif()

set(CL_INSTALLABLE_LIBS "" CACHE INTERNAL "")
set(CL_INSTALL_CONFIGDIR "${CMAKE_INSTALL_LIBDIR}/cmake/${PROJECT_NAME}")

if(CMAKE_CXX_COMPILER_ID STREQUAL "Clang")
    add_compile_options($<$<CONFIG:Debug>:-fsanitize=address>)
    add_link_options($<$<CONFIG:Debug>:-fsanitize=address>)
endif()

add_subdirectory("libs/c")
add_subdirectory("libs/cpp")

if(CL_BUILD_CSHARP)
    set(
        CL_DOTNET_DIR ""
        CACHE PATH "Folder containing the dotnet executable."
    )

    if(NOT CL_DOTNET_COMMAND OR (CL_DOTNET_COMMAND MATCHES "CL_DOTNET_COMMAND-NOTFOUND"))
        if(CL_DOTNET_DIR)
            find_program(
                CL_DOTNET_COMMAND dotnet
                HINTS ${CL_DOTNET_DIR}
                DOC "Path to the dotnet program."
            )
        else()
            find_program(
                CL_DOTNET_COMMAND dotnet
                DOC "Path to the dotnet program."
            )
        endif()
    endif()

    if(NOT CL_DOTNET_RID)
        if(CMAKE_SYSTEM_NAME MATCHES "Linux")
            if(CMAKE_SYSTEM_PROCESSOR MATCHES "x86_64")
                set(CL_DOTNET_RID "linux-x64" CACHE STRING "Dotnet target RuntimeIdentifier.")
            elseif(CMAKE_SYSTEM_PROCESSOR MATCHES "x86")
                set(CL_DOTNET_RID "linux-x86" CACHE STRING "Dotnet target RuntimeIdentifier.")
            else()
                message(FATAL_ERROR "Unsupported architecture ${CMAKE_SYSTEM_NAME} ${CMAKE_SYSTEM_PROCESSOR}.")
            endif()
        elseif(CMAKE_SYSTEM_NAME MATCHES "Darwin")
            if(CMAKE_SYSTEM_PROCESSOR MATCHES "arm64")
                set(CL_DOTNET_RID "osx-arm64" CACHE STRING "Dotnet target RuntimeIdentifier.")
            else()
                message(FATAL_ERROR "Unsupported architecture ${CMAKE_SYSTEM_NAME} ${CMAKE_SYSTEM_PROCESSOR}.")
            endif()
        elseif(CMAKE_SYSTEM_NAME MATCHES "Windows")
            if(CMAKE_SYSTEM_PROCESSOR MATCHES "AMD64") # Reports AMD although it actually is x64
                if(CMAKE_SIZEOF_VOID_P EQUAL 8)
                    set(CL_DOTNET_RID "win-x64" CACHE STRING "Dotnet target RuntimeIdentifier.")
                else() # cmake reports AMD64 on Windows, but we might be building for 32-bit.
                    set(CL_DOTNET_RID "win-x86" CACHE STRING "Dotnet target RuntimeIdentifier.")
                endif()
            elseif(CMAKE_SYSTEM_PROCESSOR MATCHES "x86_64")
                set(CL_DOTNET_RID "win-x64" CACHE STRING "Dotnet target RuntimeIdentifier.")
            elseif(CMAKE_SYSTEM_PROCESSOR MATCHES "x86")
                set(CL_DOTNET_RID "win-x86" CACHE STRING "Dotnet target RuntimeIdentifier.")
            else()
                message(FATAL_ERROR "Unsupported architecture ${CMAKE_SYSTEM_NAME} ${CMAKE_SYSTEM_PROCESSOR}.")
            endif()
        else()
            message(FATAL_ERROR "Unsupported architecture ${CMAKE_SYSTEM_NAME} ${CMAKE_SYSTEM_PROCESSOR}.")
        endif()
    endif()
    
    add_subdirectory("libs/csharp")
endif()

add_subdirectory("libs/refs")

if(CL_BUILD_EXAMPLES)
    add_subdirectory("examples/c")

    if(CL_BUILD_CSHARP)
        add_subdirectory("examples/csharp")
    endif()
endif()

if((CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME OR CL_BUILD_TESTS) AND BUILD_TESTING)
    add_subdirectory("tests")
endif()

install(
    TARGETS ${CL_INSTALLABLE_LIBS}
    EXPORT CrosslightTargets
    LIBRARY DESTINATION "${CMAKE_INSTALL_LIBDIR}" COMPONENT lib
    ARCHIVE DESTINATION "${CMAKE_INSTALL_LIBDIR}" COMPONENT lib
    RUNTIME DESTINATION "${CMAKE_INSTALL_BINDIR}" COMPONENT bin
)

install(
    FILES "LICENSE"
    DESTINATION "${CMAKE_INSTALL_DOCDIR}"
)

include(CMakePackageConfigHelpers)
write_basic_package_version_file(
    ${CMAKE_CURRENT_BINARY_DIR}/cmake/CrosslightVersion.cmake
    VERSION ${CL_VERSION_STRING}
    COMPATIBILITY SameMajorVersion
)

file(
    WRITE "${CMAKE_CURRENT_BINARY_DIR}/CrosslightConfig.cmake.in"
    "include(\"\${CMAKE_CURRENT_LIST_DIR}/CrosslightTargets.cmake\")"
)
export(
    EXPORT CrosslightTargets
    FILE "${CMAKE_CURRENT_BINARY_DIR}/CrosslightTargets.cmake"
)
configure_file(
    "${CMAKE_CURRENT_BINARY_DIR}/CrosslightConfig.cmake.in"
    "${CMAKE_CURRENT_BINARY_DIR}/CrosslightConfig.cmake"
    COPYONLY
)

configure_package_config_file(
    "${CMAKE_CURRENT_BINARY_DIR}/CrosslightConfig.cmake.in"
    "${CMAKE_CURRENT_BINARY_DIR}/cmake/CrosslightConfig.cmake"
    INSTALL_DESTINATION "${CL_INSTALL_CONFIGDIR}"
)
install(EXPORT CrosslightTargets
    FILE CrosslightTargets.cmake
    DESTINATION "${CL_INSTALL_CONFIGDIR}"
)
install(FILES
    "${CMAKE_CURRENT_BINARY_DIR}/cmake/CrosslightConfig.cmake"
    "${CMAKE_CURRENT_BINARY_DIR}/cmake/CrosslightVersion.cmake"
    DESTINATION "${CL_INSTALL_CONFIGDIR}"
)
