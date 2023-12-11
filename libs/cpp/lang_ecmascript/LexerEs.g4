lexer grammar LexerEs;

channels { ERROR }

options {
    superClass=LexerEsBase;
}

@header {
    #include "lang_ecmascript/LexerEsBase.hpp"
}

// 12.9 Literals

// 12.9.1 Null Literals

NullLiteral
    : 'null'
    ;

// 12.9.2 Boolean Literals

BooleanLiteral
    : 'true'
    | 'false'
    ;

fragment NumericLiteralSeparator
    : '_'
    ;

// 12.9.3 Numeric Literals

NumericLiteral
    : DecimalLiteral
    | DecimalBigIntegerLiteral
    | NonDecimalIntegerLiteral_Sep
    | NonDecimalIntegerLiteral_Sep BigIntLiteralSuffix
    | LegacyOctalIntegerLiteral
    ;

fragment DecimalBigIntegerLiteral
    : '0' BigIntLiteralSuffix
    | NonZeroDigit DecimalDigits_Sep? BigIntLiteralSuffix
    | NonZeroDigit NumericLiteralSeparator DecimalDigits_Sep BigIntLiteralSuffix
    ;

fragment NonDecimalIntegerLiteral
    : BinaryIntegerLiteral
    | OctalIntegerLiteral
    | HexIntegerLiteral
    ;

fragment NonDecimalIntegerLiteral_Sep
    : BinaryIntegerLiteral_Sep
    | OctalIntegerLiteral_Sep
    | HexIntegerLiteral_Sep
    ;

fragment BigIntLiteralSuffix
    : 'n'
    ;

fragment DecimalLiteral
    : DecimalIntegerLiteral DotPunctuator DecimalDigits_Sep?  ExponentPart_Sep?
    | DotPunctuator DecimalDigits_Sep  ExponentPart_Sep?
    | DecimalIntegerLiteral ExponentPart_Sep?
    ;

fragment DecimalIntegerLiteral
    : '0'
    | NonZeroDigit
    | NonZeroDigit NumericLiteralSeparator? DecimalDigits_Sep
    | NonOctalDecimalIntegerLiteral
    ;

fragment DecimalDigits
    : DecimalDigit+
    ;

fragment DecimalDigits_Sep
    : DecimalDigit (NumericLiteralSeparator? DecimalDigit)*
    ;

fragment DecimalDigit
    : [0-9]
    ;

fragment NonZeroDigit
    : [1-9]
    ;

fragment ExponentPart
    : ExponentIndicator SignedInteger
    ;

fragment ExponentPart_Sep
    : ExponentIndicator SignedInteger_Sep
    ;

fragment ExponentIndicator
    : [eE]
    ;

fragment SignedInteger
    : DecimalDigits
    | '+' DecimalDigits
    | '-' DecimalDigits
    ;

fragment SignedInteger_Sep
    : DecimalDigits_Sep
    | '+' DecimalDigits_Sep
    | '-' DecimalDigits_Sep
    ;

fragment BinaryIntegerLiteral
    : '0b' BinaryDigits
    | '0B' BinaryDigits
    ;

fragment BinaryIntegerLiteral_Sep
    : '0b' BinaryDigits_Sep
    | '0B' BinaryDigits_Sep
    ;

fragment BinaryDigits
    : BinaryDigit+
    ;

fragment BinaryDigits_Sep
    : BinaryDigit (NumericLiteralSeparator? BinaryDigit)*
    ;

fragment BinaryDigit
    : [0-1]
    ;

fragment OctalIntegerLiteral
    : '0o' OctalDigits
    | '0O' OctalDigits
    ;

fragment OctalIntegerLiteral_Sep
    : '0o' OctalDigits_Sep
    | '0O' OctalDigits_Sep
    ;

fragment OctalDigits
    : OctalDigit+
    ;

fragment OctalDigits_Sep
    : OctalDigit (NumericLiteralSeparator? OctalDigit)*
    ;

fragment LegacyOctalIntegerLiteral
    : '0' OctalDigit (OctalDigit)*
    ;

fragment NonOctalDecimalIntegerLiteral
    : ('0' NonOctalDigit | LegacyOctalLikeDecimalIntegerLiteral NonOctalDigit) DecimalDigit*
    ;

fragment LegacyOctalLikeDecimalIntegerLiteral
    : '0' OctalDigit+
    ;

fragment OctalDigit
    : [0-7]
    ;

fragment NonOctalDigit
    : [8-9]
    ;

fragment HexIntegerLiteral
    : '0x' HexDigits
    | '0X' HexDigits
    ;

fragment HexIntegerLiteral_Sep
    : '0x' HexDigits_Sep
    | '0X' HexDigits_Sep
    ;

fragment HexDigits
    : HexDigit+
    ;

fragment HexDigits_Sep
    : HexDigit (NumericLiteralSeparator? HexDigit)*
    ;

fragment HexDigit
    : [0-9a-fA-F]
    ;

// 12.9.4 String Literals

StringLiteral
    : '"' DoubleStringCharacters? '"'
    | '\'' SingleStringCharacters? '\''
    ;

fragment DoubleStringCharacters
    : DoubleStringCharacter+
    ;

fragment SingleStringCharacters
    : SingleStringCharacter+
    ;

fragment DoubleStringCharacter
    : SourceCharacter {!isLastLineTerminator() && !isLastCharacterOneOf("\"\\")}?
    | '\u2028' // Line Separator (LS)
    | '\u2029' // Paragraph Separator (PS)
    | '\\' EscapeSequence
    | LineContinuation
    ;

fragment SingleStringCharacter
    : SourceCharacter {!isLastLineTerminator() && !isLastCharacterOneOf("'\\")}?
    | '\u2028' // Line Separator (LS)
    | '\u2029' // Paragraph Separator (PS)
    | '\\' EscapeSequence
    | LineContinuation
    ;

fragment LineContinuation
    : '\\' LineTerminatorSequence
    ;

fragment EscapeSequence
    : CharacterEscapeSequence
    | '0' {!isNextCharacter('0', '9')}?
    | LegacyOctalEscapeSequence
    | NonOctalDecimalEscapeSequence
    | HexEscapeSequence
    | UnicodeEscapeSequence
    ;

fragment CharacterEscapeSequence
    : SingleEscapeCharacter
    | NonEscapeCharacter
    ;

fragment SingleEscapeCharacter
    : '\''
    | '"'
    | '\\'
    | [bfnrtv]
    ;

fragment NonEscapeCharacter
    : SourceCharacter {!isLastLineTerminator() && !isLastEscapeCharacter()}?
    ;

fragment EscapeCharacter
    : SingleEscapeCharacter
    | DecimalDigit
    | 'x'
    | 'u'
    ;

fragment LegacyOctalEscapeSequence
    : '0' {isNextCharacter('8', '9')}?
    | NonZeroOctalDigit {!isNextCharacter('0', '7')}?
    | ZeroToThree OctalDigit {!isNextCharacter('0', '7')}?
    | FourToSeven OctalDigit
    | ZeroToThree OctalDigit OctalDigit
    ;

fragment NonZeroOctalDigit
    : [1-7]
    ;

fragment ZeroToThree
    : [0-3]
    ;

fragment FourToSeven
    : [4-7]
    ;

fragment NonOctalDecimalEscapeSequence
    : [8-9]
    ;

fragment HexEscapeSequence
    : 'x' HexDigit HexDigit
    ;

fragment UnicodeEscapeSequence
    : 'u' Hex4Digits
    | 'u' LeftBracesLiteral CodePoint RightBracesLiteral
    ;

fragment Hex4Digits
    : HexDigit HexDigit HexDigit HexDigit
    ;

// 12.9.5 Regular Expression Literals

RegularExpressionLiteral
    : '/' RegularExpressionBody '/' RegularExpressionFlags
    ;

fragment RegularExpressionBody
    : RegularExpressionFirstChar RegularExpressionChars
    ;

fragment RegularExpressionChars
    : RegularExpressionChar*
    ;

fragment RegularExpressionFirstChar
    : RegularExpressionNonTerminator {!isLastCharacterOneOf("*\\/[")}?
    | RegularExpressionBackslashSequence
    | RegularExpressionClass
    ;

fragment RegularExpressionChar
    : RegularExpressionNonTerminator {!isLastCharacterOneOf("\\/[")}?
    | RegularExpressionBackslashSequence
    | RegularExpressionClass
    ;

fragment RegularExpressionBackslashSequence
    : '\\' RegularExpressionNonTerminator
    ;

fragment RegularExpressionNonTerminator
    : SourceCharacter {!isLastLineTerminator()}?
    ;

fragment RegularExpressionClass
    : LeftBracketsLiteral RegularExpressionClassChars RightBracketsLiteral
    ;

fragment RegularExpressionClassChars
    : RegularExpressionClassChar*
    ;

fragment RegularExpressionClassChar
    : RegularExpressionNonTerminator {!isLastCharacterOneOf("]\\")}?
    | RegularExpressionBackslashSequence
    ;

fragment RegularExpressionFlags
    : IdentifierPartChar*
    ;

// 12.9.6 Template Literal Lexical Components

Template
    : NoSubstitutionTemplate
    | TemplateHead
    ;

NoSubstitutionTemplate
    : '`' TemplateCharacters? '`'
    ;

TemplateHead
    : '`' TemplateCharacters? '${'
    ;

TemplateSubstitutionTail
    : TemplateMiddle
    | TemplateTail
    ;

TemplateMiddle
    : RightBracesLiteral TemplateCharacters? '${'
    ;

TemplateTail
    : RightBracesLiteral TemplateCharacters? '`'
    ;

fragment TemplateCharacters
    : TemplateCharacter+
    ;

fragment TemplateCharacter
    : '$' {!isNextCharacter("{")}?
    | '\\' TemplateEscapeSequence
    | '\\' NotEscapeSequence
    | LineContinuation
    | LineTerminatorSequence
    | SourceCharacter {!isLastCharacterOneOf("`\\$") && !isLastLineTerminator()}?
    ;

fragment TemplateEscapeSequence
    : CharacterEscapeSequence
    | '0' {!isNextCharacter('0', '9')}?
    | HexEscapeSequence
    | UnicodeEscapeSequence
    ;

fragment NotEscapeSequence
    : '0' DecimalDigit
    | [1-9]
    | 'x' {!isNextHexDigit()}?
    | 'x' HexDigit {!isNextHexDigit()}?
    | 'u' {!isNextHexDigit() && !isNextCharacter("{")}?
    | 'u' HexDigit {!isNextHexDigit()}?
    | 'u' HexDigit HexDigit {!isNextHexDigit()}?
    | 'u' HexDigit HexDigit HexDigit {!isNextHexDigit()}?
    | 'u{' {!isNextHexDigit()}?
    | 'u{' NotCodePoint {!isNextHexDigit()}?
    | 'u{' CodePoint {!isNextHexDigit() && !isNextCharacter("}")}?
    ;

fragment NotCodePoint
    : HexDigits {lastHexDigitsMV() > 0x10FFFF}?
    ;

fragment CodePoint
    : HexDigits {lastHexDigitsMV() <= 0x10FFFF}?
    ;

// 12.8 Punctuators

fragment Punctuator
    : OptionalChainingPunctuator
    | OtherPunctuator
    ;

OptionalChainingPunctuator
    : '?.' {!isNextCharacter('0', '9')}?
    ;

fragment OtherPunctuator
    : LeftBracesLiteral | LeftParenthesesLiteral | RightParenthesesLiteral | LeftBracketsLiteral | RightBracketsLiteral
    | ThreeDotsPunctuator
    | LessEqualPunctuator
    | MoreEqualPunctuator
    | LooselyEqualPunctuator
    | LooselyNotEqualPunctuator
    | StrictlyEqualPunctuator
    | StrictlyNotEqualPunctuator
    | ExponentiatePunctuator
    | IncrementPunctuator
    | DecrementPunctuator
    | LeftShiftPunctuator
    | SignedRightShiftPunctuator
    | UnsignedRightShiftPunctuator
    | LogicalANDPunctuator
    | LogicalORAssignmentPunctuator
    | CoalescePunctuator
    | AddAssignmentPunctuator
    | SubtractAssignmentPunctuator
    | MultiplyAssignmentPunctuator
    | RemainderAssignmentPunctuator
    | ExponentiateAssignmentPunctuator
    | LeftShiftAssignmentPunctuator
    | SignedRightShiftAssignmentPunctuator
    | UnsignedRightShiftAssignmentPunctuator
    | BitwiseANDAssignmentPunctuator
    | BitwiseORAssignmentPunctuator
    | BitwiseXORAssignmentPunctuator
    | LogicalANDAssignmentPunctuator
    | LogicalORAssignmentPunctuator
    | CoalesceAssignmentPunctuator
    | ArrowPunctuator
    | DotPunctuator
    | ConditionalHeadPunctuator
    | ConditionalTailPunctuator
    | AssignmentPunctuator
    | BitwiseANDPunctuator
    | BitwiseORPunctuator
    | BitwiseXORPunctuator
    | LogicalNOTPunctuator
    | BitwiseNOTPunctuator
    | CommaPunctuator
    | LessPunctuator
    | MorePunctuator
    | AddPunctuator
    | MultiplyPunctuator
    | RemainderPunctuator
    | SubtractPunctuator
    ;

UnsignedRightShiftAssignmentPunctuator
    : '>>>='
    ;

ThreeDotsPunctuator
    : '...'
    ;

StrictlyEqualPunctuator
    : '==='
    ;

StrictlyNotEqualPunctuator
    : '!=='
    ;

UnsignedRightShiftPunctuator
    : '>>>'
    ;

ExponentiateAssignmentPunctuator
    : '**='
    ;

LeftShiftAssignmentPunctuator
    : '<<='
    ;

SignedRightShiftAssignmentPunctuator
    : '>>='
    ;

LogicalANDAssignmentPunctuator
    : '&&='
    ;

LogicalORAssignmentPunctuator
    : '||='
    ;

CoalesceAssignmentPunctuator
    : '??='
    ;

LessEqualPunctuator
    : '<='
    ;

MoreEqualPunctuator
    : '>='
    ;

LooselyEqualPunctuator
    : '=='
    ;

LooselyNotEqualPunctuator
    : '!='
    ;

ExponentiatePunctuator
    : '**'
    ;

IncrementPunctuator
    : '++'
    ;

DecrementPunctuator
    : '--'
    ;

LeftShiftPunctuator
    : '<<'
    ;

SignedRightShiftPunctuator
    : '>>'
    ;

LogicalANDPunctuator
    : '&&'
    ;

LogicalORPunctuator
    : '||'
    ;

CoalescePunctuator
    : '??'
    ;

AddAssignmentPunctuator
    : '+='
    ;

SubtractAssignmentPunctuator
    : '-='
    ;

MultiplyAssignmentPunctuator
    : '*='
    ;

RemainderAssignmentPunctuator
    : '%='
    ;

BitwiseANDAssignmentPunctuator
    : '&='
    ;

BitwiseORAssignmentPunctuator
    : '|='
    ;

BitwiseXORAssignmentPunctuator
    : '^='
    ;

ArrowPunctuator
    : '=>'
    ;

LeftParenthesesLiteral
    : '('
    ;

RightParenthesesLiteral
    : ')'
    ;

LeftBracketsLiteral
    : '['
    ;

RightBracketsLiteral
    : ']'
    ;

LeftBracesLiteral
    : '{'
    ;

RightBracesLiteral
    : '}'
    ;

DotPunctuator
    : '.'
    ;

SemicolonPunctuator
    : ';'
    ;

ConditionalHeadPunctuator
    : '?'
    ;

ConditionalTailPunctuator
    : ':'
    ;

AssignmentPunctuator
    : '='
    ;

BitwiseANDPunctuator
    : '&'
    ;

BitwiseORPunctuator
    : '|'
    ;

BitwiseXORPunctuator
    : '^'
    ;

LogicalNOTPunctuator
    : '!'
    ;

BitwiseNOTPunctuator
    : '~'
    ;

CommaPunctuator
    : ','
    ;

LessPunctuator
    : '<'
    ;

MorePunctuator
    : '>'
    ;

AddPunctuator
    : '+'
    ;

MultiplyPunctuator
    : '*'
    ;

RemainderPunctuator
    : '%'
    ;

SubtractPunctuator
    : '-'
    ;

DivPunctuator
    : DivideAssignmentPunctuator
    | DividePunctuator
    ;

DivideAssignmentPunctuator
    : '/='
    ;

DividePunctuator
    : '/'
    ;

// 12.7 Names and Keywords

AwaitKeyword
    : 'await'
    ;

BreakKeyword
    : 'break'
    ;

CaseKeyword
    : 'case'
    ;

CatchKeyword
    : 'catch'
    ;

ClassKeyword
    : 'class'
    ;

ConstKeyword
    : 'const'
    ;

ContinueKeyword
    : 'continue'
    ;

DebuggerKeyword
    : 'debugger'
    ;

DefaultKeyword
    : 'default'
    ;

DeleteKeyword
    : 'delete'
    ;

DoKeyword
    : 'do'
    ;

ElseKeyword
    : 'else'
    ;

ExportKeyword
    : 'export'
    ;

ExtendsKeyword
    : 'extends'
    ;

FinallyKeyword
    : 'finally'
    ;

ForKeyword
    : 'for'
    ;

FunctionKeyword
    : 'function'
    ;

IfKeyword
    : 'if'
    ;

ImportKeyword
    : 'import'
    ;

InKeyword
    : 'in'
    ;

InstanceofKeyword
    : 'instanceof'
    ;

LetKeyword // Note: not part of ECMAScript 6.0 ReservedWord
    : 'let'
    ;

NewKeyword
    : 'new'
    ;

OfKeyword
    : 'of'
    ;

ReturnKeyword
    : 'return'
    ;

SuperKeyword
    : 'super'
    ;

SwitchKeyword
    : 'switch'
    ;

TargetLiteral
    : 'target'
    ;

MetaLiteral
    : 'meta'
    ;

ThisKeyword
    : 'this'
    ;

ThrowKeyword
    : 'throw'
    ;

TryKeyword
    : 'try'
    ;

TypeofKeyword
    : 'typeof'
    ;

VarKeyword
    : 'var'
    ;

VoidKeyword
    : 'void'
    ;

WhileKeyword
    : 'while'
    ;

WithKeyword
    : 'with'
    ;

YieldKeyword
    : 'yield'
    ;

AsyncKeyword
    : 'async'
    ;

AsKeyword
    : 'as'
    ;

FromKeyword
    : 'from'
    ;

GetKeyword
    : 'get'
    ;

SetKeyword
    : 'set'
    ;

StaticKeyword
    : 'static'
    ;

ReservedWord // TODO: add strict reserved identifiers like 'let'
    : AwaitKeyword
    | BreakKeyword
    | CaseKeyword
    | CatchKeyword
    | ClassKeyword
    | ConstKeyword
    | ContinueKeyword
    | DebuggerKeyword
    | DefaultKeyword
    | DeleteKeyword
    | DoKeyword
    | ElseKeyword
    | 'enum'
    | ExportKeyword
    | ExtendsKeyword
//    | 'false'
    | FinallyKeyword
    | ForKeyword
    | FunctionKeyword
    | IfKeyword
    | ImportKeyword
    | InKeyword
    | InstanceofKeyword
    | NewKeyword
//    | 'null'
    | OfKeyword
    | ReturnKeyword
    | SuperKeyword
    | SwitchKeyword
    | ThisKeyword
    | ThrowKeyword
//    | 'true'
    | TryKeyword
    | TypeofKeyword
    | VarKeyword
    | VoidKeyword
    | WhileKeyword
    | WithKeyword
    | YieldKeyword
    ;

PrivateIdentifier
    : '#' IdentifierName
    ;

IdentifierName
    : IdentifierStart IdentifierPart*
    ;

fragment IdentifierStart
    : IdentifierStartChar
    |  '\\' UnicodeEscapeSequence
    ;

fragment IdentifierPart
    : IdentifierPartChar
    | '\\' UnicodeEscapeSequence
    ;

fragment IdentifierStartChar
    : UnicodeIDStart
    | '$'
    | '_'
    ;

fragment IdentifierPartChar
    : UnicodeIDContinue
    | '$'
    | '\u200C' // Zero Width Non-Joiner (ZWNJ)
    | '\u200D' // Zero Width Joiner (ZWJ)
    ;

fragment AsciiLetter
    : [a-zA-Z]
    ;

fragment UnicodeIDStart
    : [\p{ID_Start}]
    ;

fragment UnicodeIDContinue
    : [\p{ID_Continue}]
    ;

// 12.6 Tokens

CommonToken
    : IdentifierName
    | PrivateIdentifier
    | Punctuator
    | NumericLiteral
    | StringLiteral
    | Template
    ;

// 12.5 Hashbang Comments

HashbangComment
    : '#!' SingleLineCommentChars?
    ;

// 12.4 Comments

Comment
    : MultiLineComment
    | SingleLineComment
    ;

fragment MultiLineComment
    : '/*' MultiLineCommentChars? '*/'
    ;

fragment MultiLineCommentChars
    : MultiLineNotAsteriskChar MultiLineCommentChars?
    | MultiplyPunctuator PostAsteriskCommentChars?
    ;

fragment PostAsteriskCommentChars
    : MultiLineNotForwardSlashOrAsteriskChar MultiLineCommentChars?
    | MultiplyPunctuator PostAsteriskCommentChars?
    ;

fragment MultiLineNotAsteriskChar
    : SourceCharacter {!isLastCharacter("*")}?
    ;

fragment MultiLineNotForwardSlashOrAsteriskChar
    : SourceCharacter {!isLastCharacter("*") && !isLastCharacter("/")}?
    ;

fragment SingleLineComment
    : '//' SingleLineCommentChars?
    ;

fragment SingleLineCommentChars
    : SingleLineCommentChar SingleLineCommentChars?
    ;

fragment SingleLineCommentChar
    : SourceCharacter {!isLastLineTerminator()}?
    ;

// 12.3 Line Terminators

LineTerminator
    : '\u000A' // Line Feed (LF)
    | '\u000D' // Carriage Return (CR)
    | '\u2028' // Line Separator (LS)
    | '\u2029' // Paragraph Separator (PS)
    ;

fragment LineTerminatorSequence
    : '\u000A' // Line Feed (LF)
    | '\u000D\u000A' // (CRLF)
    | '\u000D' // Carriage Return (CR)
    | '\u2028' // Line Separator (LS)
    | '\u2029' // Paragraph Separator (PS)
    ;

// 12.2 White Space

WhiteSpace
    : '\u0009' // Character Tabulation (TAB)
    | '\u000B' // Line Tabulation (VT)
    | '\u000C' // Form Feed (FF)
    | '\uFEFF' // Zero Width No-Break Space (ZWNBSP)
    | USP
    ;

fragment USP
    : '\u0020' // Space (SP)
    | '\u00A0' // No-Break Space (NBSP)
    | '\u1680' // Ogham Space Mark
    | '\u2000' // En Quad
    | '\u2001' // Em Quad
    | '\u2002' // En Space
    | '\u2003' // Em Space
    | '\u2004' // Three-Per-Em Space
    | '\u2005' // Four-Per-Em Space
    | '\u2006' // Six-Per-Em Space
    | '\u2007' // Figure Space
    | '\u2008' // Punctuation Space
    | '\u2009' // Thin Space
    | '\u200A' // Hair Space
    | '\u202F' // Narrow No-Break Space (NNBSP)
    | '\u205F' // Medium Mathematical Space (MMSP)
    | '\u3000' // Ideographic Space
    ;

// 11.1 Source Text

fragment SourceCharacter
    : '\u{00000}'..'\u{E007F}' // Taken from the current Unicode range
    ;
