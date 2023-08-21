cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "crosslight_lang_ts")
set(BINARY_NAME "cl_lang_ts")

project("${PROJECT_NAME}" LANGUAGES CXX)

set(APP_BASE_PATH "${PROJECT_SOURCE_DIR}")

file(
	GLOB SOURCES
	"${APP_BASE_PATH}/src/*.cpp"
)

# set ANTLR4 JAR file path for ANTLR_TARGET command
get_target_property(ANTLR_EXECUTABLE cl_lang target_antlr_executable)

ANTLR_TARGET(
  TypescriptGrammarLexer LexerTs.g4 LEXER
  PACKAGE cl::lang::typescript)

add_executable(
  ${BINARY_NAME} src/main.cpp
  ${ANTLR_TypescriptGrammarLexer_CXX_OUTPUTS})

target_link_libraries(${BINARY_NAME} PRIVATE cl_lang)
target_include_directories(${BINARY_NAME} PRIVATE ${ANTLR_TypescriptGrammarLexer_OUTPUT_DIR})
