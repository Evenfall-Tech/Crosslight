cmake_minimum_required(VERSION 3.14)

set(BINARY_NAME "cl_lang")
set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

set(CMAKE_CXX_STANDARD 17)

set(ANTLR4_WITH_STATIC_CRT OFF) # using /MD flag for antlr4_runtime (for Visual C++ compilers only)
set(ANTLR_BUILD_SHARED ON)
set(ANTLR_TAG 4.13.1)
set(DISABLE_WARNINGS ON)
set(ANTLR_BUILD_CPP_TESTS OFF)

set(ANTLR_EXECUTABLE "${LIBRARY_BASE_PATH}/vendor/antlr/antlr-${ANTLR_TAG}-complete.jar")
if(NOT EXISTS "${ANTLR_EXECUTABLE}")
    file(
        DOWNLOAD
        "https://www.antlr.org/download/antlr-${ANTLR_TAG}-complete.jar"
        "${ANTLR_EXECUTABLE}"
    )
endif()

FetchContent_Declare(
    antlr
    GIT_REPOSITORY https://github.com/antlr/antlr4
    GIT_TAG ${ANTLR_TAG}
    SOURCE_SUBDIR "runtime/Cpp"
)
FetchContent_MakeAvailable(antlr)
include(${antlr_SOURCE_DIR}/runtime/Cpp/cmake/FindANTLR.cmake)

set(PUBLIC_HEADERS_FILES
    "${LIBRARY_BASE_PATH}/include/lang/plugin.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/config.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/language.hpp"
)

# add generated grammar to demo binary target
add_library(
    ${BINARY_NAME} SHARED
    ${PUBLIC_HEADERS_FILES}
    "${LIBRARY_BASE_PATH}/src/language.cpp"
    "${LIBRARY_BASE_PATH}/src/config.cpp"
)
add_library(CL::${BINARY_NAME} ALIAS ${BINARY_NAME})

target_link_libraries(
    ${BINARY_NAME}
    INTERFACE antlr4_shared
    PUBLIC cl_core
)
target_include_directories(${BINARY_NAME}
    INTERFACE $<BUILD_INTERFACE:${antlr_SOURCE_DIR}/runtime/Cpp/runtime/src> $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}/antlr4-runtime>
    PUBLIC $<BUILD_INTERFACE:${LIBRARY_BASE_PATH}/include> $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>
)

set(ANTLR_RUNTIME_LIBRARIES $<TARGET_FILE:antlr4_shared> $<TARGET_LINKER_FILE:antlr4_shared>)
set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	                ${LIBRARY_VERSION_STRING}
    SOVERSION               ${LIBRARY_VERSION_MAJOR}
    FOLDER                  ${CL_FOLDER_RUNTIME}
    target_antlr_executable ${ANTLR_EXECUTABLE}
    target_antlr_libraries  "${ANTLR_RUNTIME_LIBRARIES}"
)

target_compile_definitions(${BINARY_NAME} PRIVATE CL_LANG_COMPILATION)
target_compile_features(${BINARY_NAME} PUBLIC cxx_std_17)
set_target_properties(${BINARY_NAME} PROPERTIES CXX_EXTENSIONS OFF)

add_custom_command(TARGET ${BINARY_NAME}
    POST_BUILD
    COMMAND ${CMAKE_COMMAND} -E make_directory ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY}
    COMMAND ${CMAKE_COMMAND} -E copy ${ANTLR_RUNTIME_LIBRARIES} ${CMAKE_ARCHIVE_OUTPUT_DIRECTORY}
)

list(APPEND CL_INSTALLABLE_LIBS ${BINARY_NAME} antlr4_shared)
set(CL_INSTALLABLE_LIBS ${CL_INSTALLABLE_LIBS} CACHE INTERNAL "")
install(
    DIRECTORY "${LIBRARY_BASE_PATH}/include/lang"
    DESTINATION "${CMAKE_INSTALL_INCLUDEDIR}"
    COMPONENT dev
    FILES_MATCHING
    PATTERN "*.hpp"
)

if(MSVC)
    install(
        FILES "$<TARGET_FILE_DIR:${BINARY_NAME}>/${CMAKE_SHARED_LIBRARY_PREFIX}${BINARY_NAME}.pdb"
        DESTINATION "${CMAKE_INSTALL_LIBDIR}"
        CONFIGURATIONS Debug
        COMPONENT lib
    )
endif()
