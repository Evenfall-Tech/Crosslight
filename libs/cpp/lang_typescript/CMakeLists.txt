# Based on commit https://github.com/antlr/grammars-v4/commit/6590fdd07b9fa1b4a66025c8315ee2c93bbcbe7d

cmake_minimum_required(VERSION 3.14)

set(BINARY_NAME "cl_lang_typescript")

set(LIBRARY_VERSION_MAJOR ${CL_VERSION_MAJOR})
set(LIBRARY_VERSION_STRING ${CL_VERSION_STRING})
set(LIBRARY_BASE_PATH "${CMAKE_CURRENT_SOURCE_DIR}")

file(
	  GLOB SOURCES
	  "${LIBRARY_BASE_PATH}/src/*.cpp"
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
add_library(CL::${BINARY_NAME} ALIAS ${BINARY_NAME})

target_link_libraries(${BINARY_NAME} PRIVATE cl_core cl_lang)

target_include_directories(${BINARY_NAME} PRIVATE
    ${ANTLR_TypescriptGrammarLexer_OUTPUT_DIR}
    ${ANTLR_TypescriptGrammarParser_OUTPUT_DIR}
    "${LIBRARY_BASE_PATH}/private_include"
)

set_target_properties(
    ${BINARY_NAME} PROPERTIES
#    VERSION                  ${LIBRARY_VERSION_STRING} # produces clang error on macos
#    SOVERSION                ${LIBRARY_VERSION_MAJOR} # clang-14: error: invalid argument '-current_version 0.1.0' only allowed with '-dynamiclib'
    FOLDER                   ${CL_FOLDER_PLUGINS}
    RUNTIME_OUTPUT_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
    LIBRARY_OUTPUT_DIRECTORY "${CMAKE_RUNTIME_OUTPUT_DIRECTORY}/plugins"
)

target_compile_features(${BINARY_NAME} PUBLIC cxx_std_17)
set_target_properties(${BINARY_NAME} PROPERTIES CXX_EXTENSIONS OFF)

install(
    TARGETS ${BINARY_NAME}
    LIBRARY DESTINATION "${CMAKE_INSTALL_LIBDIR}/plugins"
)
