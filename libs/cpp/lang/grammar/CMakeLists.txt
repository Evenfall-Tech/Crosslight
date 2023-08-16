message(STATUS "Building grammar")
# Call macro to add lexer and grammar to your build dependencies.
antlr_target(
  SampleGrammarLexer TLexer.g4 LEXER
  PACKAGE antlrcpptest)

antlr_target(
  SampleGrammarParser TParser.g4 PARSER
  PACKAGE antlrcpptest
  DEPENDS_ANTLR SampleGrammarLexer
  COMPILE_FLAGS -lib ${ANTLR_SampleGrammarLexer_OUTPUT_DIR})

add_custom_target(samplegrammar DEPENDS ${ANTLR_SampleGrammarLexer_CXX_OUTPUTS} ${ANTLR_SampleGrammarParser_CXX_OUTPUTS})

# include generated files in project environment
set(SampleGrammarLexer_OUTPUT_DIR ${ANTLR_SampleGrammarLexer_OUTPUT_DIR})
set(SampleGrammarParser_OUTPUT_DIR ${ANTLR_SampleGrammarParser_OUTPUT_DIR})
set(SampleGrammarLexer_OUTPUT_DIR ${SampleGrammarLexer_OUTPUT_DIR} PARENT_SCOPE)
set(SampleGrammarParser_OUTPUT_DIR ${SampleGrammarParser_OUTPUT_DIR} PARENT_SCOPE)

# add generated grammar to demo binary target
set(SampleGrammarLexer_CXX_OUTPUTS ${ANTLR_SampleGrammarLexer_CXX_OUTPUTS})
set(SampleGrammarParser_CXX_OUTPUTS ${ANTLR_SampleGrammarParser_CXX_OUTPUTS})
set(SampleGrammarLexer_CXX_OUTPUTS ${SampleGrammarLexer_CXX_OUTPUTS} PARENT_SCOPE)
set(SampleGrammarParser_CXX_OUTPUTS ${SampleGrammarParser_CXX_OUTPUTS} PARENT_SCOPE)
