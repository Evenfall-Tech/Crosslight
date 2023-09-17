cmake_minimum_required(VERSION 3.14)

set(BINARY_NAME "cl_tests")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

set(PUBLIC_SOURCES_FILES
    "${LIBRARY_BASE_PATH}/src/main.cpp"
    "${LIBRARY_BASE_PATH}/src/core/config.cpp"
)

set(DOCTEST_NO_INSTALL ON)
FetchContent_Declare(
    doctest
    GIT_REPOSITORY https://github.com/doctest/doctest.git
    GIT_TAG v2.4.11
)
FetchContent_MakeAvailable(doctest)

add_executable(
    ${BINARY_NAME}
    ${PUBLIC_SOURCES_FILES}
)

target_compile_features(${BINARY_NAME} PRIVATE cxx_std_17)
target_link_libraries(${BINARY_NAME} PRIVATE cl_core doctest)
target_compile_definitions(${BINARY_NAME} PRIVATE DOCTEST_CONFIG_SUPER_FAST_ASSERTS)
set_target_properties(
    ${BINARY_NAME} PROPERTIES
    FOLDER                   ${CL_FOLDER_TESTS}
    RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/tests"
)

add_test(NAME "${BINARY_NAME}_test" COMMAND ${BINARY_NAME})
