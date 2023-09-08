# Based on commit https://github.com/antlr/grammars-v4/commit/6590fdd07b9fa1b4a66025c8315ee2c93bbcbe7d

cmake_minimum_required(VERSION 3.19.0)

set(PROJECT_NAME "crosslight_lang_typescript")
set(BINARY_NAME "cl_lang_typescript")

project("${PROJECT_NAME}" LANGUAGES CXX)

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})

set(APP_BASE_PATH "${PROJECT_SOURCE_DIR}")

file(
	  GLOB SOURCES
	  "${APP_BASE_PATH}/src/*.cpp"
)

# set ANTLR4 JAR file path for ANTLR_TARGET command
get_target_property(ANTLR_EXECUTABLE cl_lang target_antlr_executable)
get_target_property(LANG_API cl_core target_lang)

ANTLR_TARGET(
    TypescriptGrammarLexer LexerTs.g4 LEXER
    PACKAGE cl::lang::typescript
)

ANTLR_TARGET(
    TypescriptGrammarParser ParserTs.g4 PARSER VISITOR
    PACKAGE cl::lang::typescript
    DEPENDS_ANTLR TypescriptGrammarLexer
    COMPILE_FLAGS -lib ${ANTLR_TypescriptGrammarLexer_OUTPUT_DIR}
)

add_library(
    ${BINARY_NAME} MODULE
    src/LexerTsBase.cpp
    private_include/lang_typescript/LexerTsBase.hpp
    src/ParserTsBase.cpp
    private_include/lang_typescript/ParserTsBase.hpp
    ${ANTLR_TypescriptGrammarLexer_CXX_OUTPUTS}
    ${ANTLR_TypescriptGrammarParser_CXX_OUTPUTS}
    
    ${LANG_API}
    src/main.cpp
    private_include/lang_typescript/language.hpp
    src/language.cpp
)

target_link_libraries(${BINARY_NAME} PRIVATE cl_core cl_lang)

target_include_directories(${BINARY_NAME} PRIVATE
    ${ANTLR_TypescriptGrammarLexer_OUTPUT_DIR}
    ${ANTLR_TypescriptGrammarParser_OUTPUT_DIR}
    "${CMAKE_CURRENT_SOURCE_DIR}/private_include"
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
    VERSION	      ${LIBRARY_VERSION_STRING}
    SOVERSION     ${LIBRARY_VERSION_MAJOR}
    RUNTIME_OUTPUT_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
    LIBRARY_OUTPUT_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
)

install(
    TARGETS ${BINARY_NAME}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
)
