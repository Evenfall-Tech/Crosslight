cmake_minimum_required(VERSION 3.14)

set(BINARY_NAME "cl_lang")
set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

set(CMAKE_CXX_STANDARD 17)

set(PUBLIC_HEADERS_FILES
    "${LIBRARY_BASE_PATH}/include/lang/config.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/language.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/utils.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/builders/allocator.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/builders/builder.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/builders/builders.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/builders/source_root.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/builders/scope.hpp"
    "${LIBRARY_BASE_PATH}/include/lang/builders/heap_type.hpp"
)

# add generated grammar to demo binary target
add_library(
    ${BINARY_NAME} SHARED
    ${PUBLIC_HEADERS_FILES}
    "${LIBRARY_BASE_PATH}/src/language.cpp"
    "${LIBRARY_BASE_PATH}/src/config.cpp"
    "${LIBRARY_BASE_PATH}/src/utils.cpp"
    "${LIBRARY_BASE_PATH}/src/builders/allocator.cpp"
    "${LIBRARY_BASE_PATH}/src/builders/builder.cpp"
    "${LIBRARY_BASE_PATH}/src/builders/builders.cpp"
    "${LIBRARY_BASE_PATH}/src/builders/source_root.cpp"
    "${LIBRARY_BASE_PATH}/src/builders/scope.cpp"
    "${LIBRARY_BASE_PATH}/src/builders/heap_type.cpp"
)
add_library(CL::${BINARY_NAME} ALIAS ${BINARY_NAME})

target_link_libraries(
    ${BINARY_NAME}
    PUBLIC cl_core
)
target_include_directories(${BINARY_NAME}
    PUBLIC $<BUILD_INTERFACE:${LIBRARY_BASE_PATH}/include> $<INSTALL_INTERFACE:${CMAKE_INSTALL_INCLUDEDIR}>
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	                ${LIBRARY_VERSION_STRING}
    SOVERSION               ${LIBRARY_VERSION_MAJOR}
    FOLDER                  ${CL_FOLDER_RUNTIME}
)

target_compile_definitions(${BINARY_NAME} PRIVATE CL_LANG_COMPILATION)
target_compile_features(${BINARY_NAME} PUBLIC cxx_std_17)
set_target_properties(${BINARY_NAME} PROPERTIES CXX_EXTENSIONS OFF)

list(APPEND CL_INSTALLABLE_LIBS ${BINARY_NAME})
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
