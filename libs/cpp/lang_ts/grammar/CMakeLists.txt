message(STATUS "Building grammar Typescript")
# Call macro to add lexer and grammar to your build dependencies.

ANTLR_TARGET(
  TypescriptGrammarLexer LexerTs.g4 LEXER
  PACKAGE cl::lang::typescript)

add_custom_target(
  cl_lang_ts_grammar
  DEPENDS cl_lang ${ANTLR_TypescriptGrammarLexer_OUTPUTS})

# include generated files in project environment
set(TypescriptGrammarLexer_OUTPUT_DIR ${ANTLR_TypescriptGrammarLexer_OUTPUT_DIR})
set(TypescriptGrammarLexer_OUTPUT_DIR ${TypescriptGrammarLexer_OUTPUT_DIR} PARENT_SCOPE)

# add generated grammar to target
set(TypescriptGrammarLexer_CXX_OUTPUTS ${ANTLR_TypescriptGrammarLexer_CXX_OUTPUTS})
set(TypescriptGrammarLexer_CXX_OUTPUTS ${TypescriptGrammarLexer_CXX_OUTPUTS} PARENT_SCOPE)
