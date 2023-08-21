lexer grammar LexerTs;

channels { ERROR, COMMENT }

MultiLineComment:               '/*' .*? '*/'             -> channel(COMMENT);
SingleLineComment:              '//' ~[\r\n\u2028\u2029]* -> channel(COMMENT);

UnexpectedCharacter:            . -> channel(ERROR);
