cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "Example AST")
set(BINARY_NAME "ast")

project("${PROJECT_NAME}" LANGUAGES CXX)

set(APP_VERSION_STRING 0.1)

include(GNUInstallDirs)

set(APP_BASE_PATH "${PROJECT_SOURCE_DIR}")

set(THREADS_PREFER_PTHREAD_FLAG ON)
find_package(Threads REQUIRED)

include(CheckIncludeFile)



# Locate llvm on your system
find_package(LLVM REQUIRED CONFIG)

message(STATUS "Found LLVM ${LLVM_PACKAGE_VERSION}")
message(STATUS "Using LLVMConfig.cmake in: ${LLVM_DIR}")



file(
    GLOB SOURCES
    "${APP_BASE_PATH}/src/*.cpp"
)

add_executable(
    ${BINARY_NAME} ${SOURCES}
)

target_include_directories(
    ${BINARY_NAME}
    PRIVATE "${APP_BASE_PATH}/"
    PRIVATE "${LLVM_INCLUDE_DIRS}"
)

target_compile_definitions(
    ${BINARY_NAME}
    PRIVATE "${LLVM_DEFINITIONS}"
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    RUNTIME_OUTPUT_DIRECTORY "${CL_BINARY_DIR}/$<CONFIG>/examples"
)

# Link against LLVM libraries
llvm_map_components_to_libnames(llvm_libs core executionengine interpreter mc mcjit support nativecodegen analysis native bitwriter)

target_link_directories(${BINARY_NAME} PRIVATE "/usr/local/lib/")
	
target_link_libraries(${BINARY_NAME} PRIVATE "clcore" ${llvm_libs})
