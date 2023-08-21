cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "Example Parse")
set(BINARY_NAME "parse")

project("${PROJECT_NAME}" LANGUAGES CXX)

set(APP_BASE_PATH "${PROJECT_SOURCE_DIR}")

file(
	GLOB SOURCES
	"${APP_BASE_PATH}/src/*.cpp"
)

add_executable(
	${BINARY_NAME} ${SOURCES}
)
