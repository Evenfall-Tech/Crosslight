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

add_subdirectory("libs/c")
add_subdirectory("libs/cpp")
add_subdirectory("libs/refs")

if(CL_BUILD_EXAMPLES)
    add_subdirectory("examples/c")
endif()

if((CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME OR CL_BUILD_TESTS) AND BUILD_TESTING)
    add_subdirectory("tests")
endif()
