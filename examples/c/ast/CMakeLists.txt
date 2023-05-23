cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "Example AST")
set(BINARY_NAME "ast")

project("${PROJECT_NAME}")

set(APP_VERSION_STRING 0.1)

include(GNUInstallDirs)

set(APP_BASE_PATH "${PROJECT_SOURCE_DIR}")

set(THREADS_PREFER_PTHREAD_FLAG ON)
find_package(Threads REQUIRED)

include(CheckIncludeFile)


	
file(
	GLOB SOURCES
	"${APP_BASE_PATH}/src/*.cpp"
)

add_executable(
	${BINARY_NAME} ${SOURCES}
)

target_include_directories(
	${BINARY_NAME} PRIVATE "${APP_BASE_PATH}/"
)

target_link_directories(${BINARY_NAME} PRIVATE "/usr/local/lib/")
	
target_link_libraries(${BINARY_NAME} PRIVATE "clcore")
