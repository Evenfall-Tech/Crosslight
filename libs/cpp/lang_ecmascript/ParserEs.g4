// https://tc39.es/ecma262/2023

parser grammar ParserEs;

options {
    tokenVocab=LexerEs;
    superClass=ParserEsBase;
}

@header {
    #include "lang_ecmascript/ParserEsBase.hpp"
}

program
    : EOF
    ;
