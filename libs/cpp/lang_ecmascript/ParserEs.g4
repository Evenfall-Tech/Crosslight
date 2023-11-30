// https://tc39.es/ecma262/2023

parser grammar ParserEs;

options {
    tokenVocab=LexerEs;
    superClass=ParserEsBase;
}

@header {
    #include "lang_ecmascript/ParserEsBase.hpp"
}

// 13 ECMAScript Language: Expressions

identifierReference
    : identifier
    | YieldKeyword
    | AwaitKeyword
    ;

identifierReference_Yield
    : identifier
    | AwaitKeyword
    ;

identifierReference_Await
    : identifier
    | YieldKeyword
    ;

identifierReference_Yield_Await
    : identifier
    ;

bindingIdentifier
    : identifier
    | YieldKeyword
    | AwaitKeyword
    ;

bindingIdentifier_Yield
    : identifier
    | YieldKeyword
    | AwaitKeyword
    ;

bindingIdentifier_Await
    : identifier
    | YieldKeyword
    | AwaitKeyword
    ;

bindingIdentifier_Yield_Await
    : identifier
    | YieldKeyword
    | AwaitKeyword
    ;

labelIdentifier_Yield
    : identifier
    | AwaitKeyword
    ;

labelIdentifier_Await
    : identifier
    | YieldKeyword
    ;

labelIdentifier_Yield_Await
    : identifier
    ;

identifier
    : IdentifierName // TODO: but not ReservedWord
    ;

// 13.2 Primary Expression

primaryExpression
    : ThisKeyword
    | identifierReference
    | literal
    | arrayLiteral
    | objectLiteral
    // TODO: | FunctionExpression
    // TODO: | ClassExpression
    // TODO: | GeneratorExpression
    // TODO: | AsyncFunctionExpression
    // TODO: | AsyncGeneratorExpression
    | RegularExpressionLiteral
    | templateLiteral
    | coverParenthesizedExpressionAndArrowParameterList
    ;

primaryExpression_Yield
    : ThisKeyword
    | identifierReference_Yield
    | literal
    | arrayLiteral_Yield
    | objectLiteral_Yield
    // TODO: | FunctionExpression
    // TODO: | ClassExpression_Yield
    // TODO: | GeneratorExpression
    // TODO: | AsyncFunctionExpression
    // TODO: | AsyncGeneratorExpression
    | RegularExpressionLiteral
    | templateLiteral_Yield
    | coverParenthesizedExpressionAndArrowParameterList_Yield
    ;

primaryExpression_Await
    : ThisKeyword
    | identifierReference_Await
    | literal
    | arrayLiteral_Await
    | objectLiteral_Await
    // TODO: | FunctionExpression
    // TODO: | ClassExpression_Await
    // TODO: | GeneratorExpression
    // TODO: | AsyncFunctionExpression
    // TODO: | AsyncGeneratorExpression
    | RegularExpressionLiteral
    | templateLiteral_Await
    | coverParenthesizedExpressionAndArrowParameterList_Await
    ;

primaryExpression_Yield_Await
    : ThisKeyword
    | identifierReference_Yield_Await
    | literal
    | arrayLiteral_Yield_Await
    | objectLiteral_Yield_Await
    // TODO: | FunctionExpression
    // TODO: | ClassExpression_Yield_Await
    // TODO: | GeneratorExpression
    // TODO: | AsyncFunctionExpression
    // TODO: | AsyncGeneratorExpression
    | RegularExpressionLiteral
    | templateLiteral_Yield_Await
    | coverParenthesizedExpressionAndArrowParameterList_Yield_Await
    ;

coverParenthesizedExpressionAndArrowParameterList
    : LeftParenthesesLiteral expression_In RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In ',' RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsLiteral bindingIdentifier RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral ThreeDotsLiteral bindingPattern RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In ',' ThreeDotsLiteral bindingIdentifier RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral expression_In ',' ThreeDotsLiteral bindingPattern RightParenthesesLiteral
    ;

coverParenthesizedExpressionAndArrowParameterList_Yield
    : LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield ',' RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsLiteral bindingIdentifier_Yield RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral ThreeDotsLiteral bindingPattern_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield ',' ThreeDotsLiteral bindingIdentifier_Yield RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral expression_In_Yield ',' ThreeDotsLiteral bindingPattern_Yield RightParenthesesLiteral
    ;

coverParenthesizedExpressionAndArrowParameterList_Await
    : LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Await ',' RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsLiteral bindingIdentifier_Await RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral ThreeDotsLiteral bindingPattern_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Await ',' ThreeDotsLiteral bindingIdentifier_Await RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral expression_In_Await ',' ThreeDotsLiteral bindingPattern_Await RightParenthesesLiteral
    ;

coverParenthesizedExpressionAndArrowParameterList_Yield_Await
    : LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield_Await ',' RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsLiteral bindingIdentifier_Yield_Await RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral ThreeDotsLiteral bindingPattern_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield_Await ',' ThreeDotsLiteral bindingIdentifier_Yield_Await RightParenthesesLiteral
    // TODO: | LeftParenthesesLiteral expression_In_Yield_Await ',' ThreeDotsLiteral bindingPattern_Yield_Await RightParenthesesLiteral
    ;

// 13.2.3 Literals

literal
    : NullLiteral
    | BooleanLiteral
    | NumericLiteral
    | StringLiteral
    ;

// 13.2.4 Array Initializer

arrayLiteral
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList RightBracketsLiteral
    | LeftBracketsLiteral elementList ',' elision? RightBracketsLiteral
    ;

arrayLiteral_Yield
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield ',' elision? RightBracketsLiteral
    ;

arrayLiteral_Await
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList_Await RightBracketsLiteral
    | LeftBracketsLiteral elementList_Await ',' elision? RightBracketsLiteral
    ;

arrayLiteral_Yield_Await
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield_Await RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield_Await ',' elision? RightBracketsLiteral
    ;

elementList
    : elision? assignmentExpression_In
    | elision? spreadElement
    | elementList ',' elision? assignmentExpression_In
    | elementList ',' elision? spreadElement
    ;

elementList_Yield
    : elision? assignmentExpression_In_Yield
    | elision? spreadElement_Yield
    | elementList_Yield ',' elision? assignmentExpression_In_Yield
    | elementList_Yield ',' elision? spreadElement_Yield
    ;

elementList_Await
    : elision? assignmentExpression_In_Await
    | elision? spreadElement_Await
    | elementList_Await ',' elision? assignmentExpression_In_Await
    | elementList_Await ',' elision? spreadElement_Await
    ;

elementList_Yield_Await
    : elision? assignmentExpression_In_Yield_Await
    | elision? spreadElement_Yield_Await
    | elementList_Yield_Await ',' elision? assignmentExpression_In_Yield_Await
    | elementList_Yield_Await ',' elision? spreadElement_Yield_Await
    ;

elision
    : ','+
    ;

spreadElement
    : ThreeDotsLiteral assignmentExpression_In
    ;

spreadElement_Yield
    : ThreeDotsLiteral assignmentExpression_In_Yield
    ;

spreadElement_Await
    : ThreeDotsLiteral assignmentExpression_In_Await
    ;

spreadElement_Yield_Await
    : ThreeDotsLiteral assignmentExpression_In_Yield_Await
    ;

// 13.2.5 Object Initializer

objectLiteral
    : LeftBracesLiteral (propertyDefinitionList ','?)? RightBracesLiteral
    ;

objectLiteral_Yield
    : LeftBracesLiteral (propertyDefinitionList_Yield ','?)? RightBracesLiteral
    ;

objectLiteral_Await
    : LeftBracesLiteral (propertyDefinitionList_Await ','?)? RightBracesLiteral
    ;

objectLiteral_Yield_Await
    : LeftBracesLiteral (propertyDefinitionList_Yield_Await ','?)? RightBracesLiteral
    ;

propertyDefinitionList
    : propertyDefinition (',' propertyDefinition)*
    ;

propertyDefinitionList_Yield
    : propertyDefinition_Yield (',' propertyDefinition_Yield)*
    ;

propertyDefinitionList_Await
    : propertyDefinition_Await (',' propertyDefinition_Await)*
    ;

propertyDefinitionList_Yield_Await
    : propertyDefinition_Yield_Await (',' propertyDefinition_Yield_Await)*
    ;

propertyDefinition
    : identifierReference
    | coverInitializedName
    | propertyName ':' assignmentExpression_In
    // TODO: | methodDefinition
    | ThreeDotsLiteral assignmentExpression_In
    ;

propertyDefinition_Yield
    : identifierReference_Yield
    | coverInitializedName_Yield
    | propertyName_Yield ':' assignmentExpression_In_Yield
    // TODO: | methodDefinition_Yield
    | ThreeDotsLiteral assignmentExpression_In_Yield
    ;

propertyDefinition_Await
    : identifierReference_Await
    | coverInitializedName_Await
    | propertyName_Await ':' assignmentExpression_In_Await
    // TODO: | methodDefinition_Await
    | ThreeDotsLiteral assignmentExpression_In_Await
    ;

propertyDefinition_Yield_Await
    : identifierReference_Yield_Await
    | coverInitializedName_Yield_Await
    | propertyName_Yield_Await ':' assignmentExpression_In_Yield_Await
    // TODO: | methodDefinition_Yield_Await
    | ThreeDotsLiteral assignmentExpression_In_Yield_Await
    ;

propertyName
    : literalPropertyName
    | computedPropertyName
    ;

propertyName_Yield
    : literalPropertyName
    | computedPropertyName_Yield
    ;

propertyName_Await
    : literalPropertyName
    | computedPropertyName_Await
    ;

propertyName_Yield_Await
    : literalPropertyName
    | computedPropertyName_Yield_Await
    ;

literalPropertyName
    : IdentifierName
    | StringLiteral
    | NumericLiteral
    ;

computedPropertyName
    : LeftBracketsLiteral assignmentExpression_In RightBracketsLiteral
    ;

computedPropertyName_Yield
    : LeftBracketsLiteral assignmentExpression_In_Yield RightBracketsLiteral
    ;

computedPropertyName_Await
    : LeftBracketsLiteral assignmentExpression_In_Await RightBracketsLiteral
    ;

computedPropertyName_Yield_Await
    : LeftBracketsLiteral assignmentExpression_In_Yield_Await RightBracketsLiteral
    ;

coverInitializedName
    : identifierReference initializer_In
    ;

coverInitializedName_Yield
    : identifierReference initializer_In_Yield
    ;

coverInitializedName_Await
    : identifierReference_Await initializer_In_Await
    ;

coverInitializedName_Yield_Await
    : identifierReference_Yield_Await initializer_In_Yield_Await
    ;

initializer
    : '=' assignmentExpression
    ;

initializer_In
    : '=' assignmentExpression_In
    ;

initializer_Yield
    : '=' assignmentExpression_Yield
    ;

initializer_Await
    : '=' assignmentExpression_Await
    ;

initializer_In_Yield
    : '=' assignmentExpression_In_Yield
    ;

initializer_In_Await
    : '=' assignmentExpression_In_Await
    ;

initializer_Yield_Await
    : '=' assignmentExpression_Yield_Await
    ;

initializer_In_Yield_Await
    : '=' assignmentExpression_In_Yield_Await
    ;

// 13.2.8 Template Literals

templateLiteral
    : NoSubstitutionTemplate
    | substitutionTemplate
    ;

templateLiteral_Yield
    : NoSubstitutionTemplate
    | substitutionTemplate_Yield
    ;

templateLiteral_Await
    : NoSubstitutionTemplate
    | substitutionTemplate_Await
    ;

templateLiteral_Tagged
    : NoSubstitutionTemplate
    | substitutionTemplate_Tagged
    ;

templateLiteral_Yield_Await
    : NoSubstitutionTemplate
    | substitutionTemplate_Yield_Await
    ;

templateLiteral_Yield_Tagged
    : NoSubstitutionTemplate
    | substitutionTemplate_Yield_Tagged
    ;

templateLiteral_Await_Tagged
    : NoSubstitutionTemplate
    | substitutionTemplate_Await_Tagged
    ;

templateLiteral_Yield_Await_Tagged
    : NoSubstitutionTemplate
    | substitutionTemplate_Yield_Await_Tagged
    ;

substitutionTemplate
    : TemplateHead expression_In templateSpans
    ;

substitutionTemplate_Yield
    : TemplateHead expression_In_Yield templateSpans_Yield
    ;

substitutionTemplate_Await
    : TemplateHead expression_In_Await templateSpans_Await
    ;

substitutionTemplate_Tagged
    : TemplateHead expression_In templateSpans_Tagged
    ;

substitutionTemplate_Yield_Await
    : TemplateHead expression_In_Yield_Await templateSpans_Yield_Await
    ;

substitutionTemplate_Yield_Tagged
    : TemplateHead expression_In_Yield templateSpans_Yield_Tagged
    ;

substitutionTemplate_Await_Tagged
    : TemplateHead expression_In_Await templateSpans_Await_Tagged
    ;

substitutionTemplate_Yield_Await_Tagged
    : TemplateHead expression_In_Yield_Await templateSpans_Yield_Await_Tagged
    ;

templateSpans
    : templateMiddleList? TemplateTail
    ;

templateSpans_Yield
    : templateMiddleList_Yield? TemplateTail
    ;

templateSpans_Await
    : templateMiddleList_Await? TemplateTail
    ;

templateSpans_Tagged
    : templateMiddleList_Tagged? TemplateTail
    ;

templateSpans_Yield_Await
    : templateMiddleList_Yield_Await? TemplateTail
    ;

templateSpans_Yield_Tagged
    : templateMiddleList_Yield_Tagged? TemplateTail
    ;

templateSpans_Await_Tagged
    : templateMiddleList_Await_Tagged? TemplateTail
    ;

templateSpans_Yield_Await_Tagged
    : templateMiddleList_Yield_Await_Tagged? TemplateTail
    ;

templateMiddleList
    : (TemplateMiddle expression_In)+
    ;

templateMiddleList_Yield
    : (TemplateMiddle expression_In_Yield)+
    ;

templateMiddleList_Await
    : (TemplateMiddle expression_In_Await)+
    ;

templateMiddleList_Tagged
    : (TemplateMiddle expression_In)+
    ;

templateMiddleList_Yield_Await
    : (TemplateMiddle expression_In_Yield_Await)+
    ;

templateMiddleList_Yield_Tagged
    : (TemplateMiddle expression_In_Yield)+
    ;

templateMiddleList_Await_Tagged
    : (TemplateMiddle expression_In_Await)+
    ;

templateMiddleList_Yield_Await_Tagged
    : (TemplateMiddle expression_In_Yield_Await)+
    ;

// 13.3 Left-Hand-Side Expressions

memberExpression
    : primaryExpression
    | memberExpression LeftBracketsLiteral expression_In RightBracketsLiteral
    | memberExpression '.' IdentifierName
    | memberExpression templateLiteral_Tagged
    | superProperty
    | metaProperty
    | NewKeyword memberExpression arguments
    | memberExpression '.' PrivateIdentifier
    ;

memberExpression_Yield
    : primaryExpression_Yield
    | memberExpression_Yield LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | memberExpression_Yield '.' IdentifierName
    | memberExpression_Yield templateLiteral_Yield_Tagged
    | superProperty_Yield
    | metaProperty
    | NewKeyword memberExpression_Yield arguments_Yield
    | memberExpression_Yield '.' PrivateIdentifier
    ;

memberExpression_Await
    : primaryExpression_Await
    | memberExpression_Await LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | memberExpression_Await '.' IdentifierName
    | memberExpression_Await templateLiteral_Await_Tagged
    | superProperty_Await
    | metaProperty
    | NewKeyword memberExpression_Await arguments_Await
    | memberExpression_Await '.' PrivateIdentifier
    ;

memberExpression_Yield_Await
    : primaryExpression_Yield_Await
    | memberExpression_Yield_Await LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | memberExpression_Yield_Await '.' IdentifierName
    | memberExpression_Yield_Await templateLiteral_Yield_Await_Tagged
    | superProperty_Yield_Await
    | metaProperty
    | NewKeyword memberExpression_Yield_Await arguments_Yield_Await
    | memberExpression_Yield_Await '.' PrivateIdentifier
    ;

superProperty
    : SuperKeyword LeftBracketsLiteral expression_In RightBracketsLiteral
    | SuperKeyword '.' IdentifierName
    ;

superProperty_Yield
    : SuperKeyword LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | SuperKeyword '.' IdentifierName
    ;

superProperty_Await
    : SuperKeyword LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | SuperKeyword '.' IdentifierName
    ;

superProperty_Yield_Await
    : SuperKeyword LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | SuperKeyword '.' IdentifierName
    ;

metaProperty
    : newTarget
    | importMeta
    ;

newTarget
    : NewKeyword '.' TargetLiteral
    ;

importMeta
    : ImportKeyword '.' MetaLiteral
    ;

newExpression
    : memberExpression
    | NewKeyword newExpression
    ;

newExpression_Yield
    : memberExpression_Yield
    | NewKeyword newExpression_Yield
    ;

newExpression_Await
    : memberExpression_Await
    | NewKeyword newExpression_Await
    ;

newExpression_Yield_Await
    : memberExpression_Yield_Await
    | NewKeyword newExpression_Yield_Await
    ;

callExpression
    // TODO: : coverCallExpressionAndAsyncArrowHead
    : superCall
    | importCall
    | callExpression arguments
    | callExpression LeftBracketsLiteral expression_In RightBracketsLiteral
    | callExpression '.' IdentifierName
    | callExpression templateLiteral_Tagged
    | callExpression '.' PrivateIdentifier
    ;

callExpression_Yield
    // TODO: : coverCallExpressionAndAsyncArrowHead_Yield
    : superCall_Yield
    | importCall_Yield
    | callExpression_Yield arguments_Yield
    | callExpression_Yield LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | callExpression_Yield '.' IdentifierName
    | callExpression_Yield templateLiteral_Yield_Tagged
    | callExpression_Yield '.' PrivateIdentifier
    ;

callExpression_Await
    // TODO: : coverCallExpressionAndAsyncArrowHead_Await
    : superCall_Await
    | importCall_Await
    | callExpression_Await arguments_Await
    | callExpression_Await LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | callExpression_Await '.' IdentifierName
    | callExpression_Await templateLiteral_Await_Tagged
    | callExpression_Await '.' PrivateIdentifier
    ;

callExpression_Yield_Await
    // TODO: : coverCallExpressionAndAsyncArrowHead_Yield_Await
    : superCall_Yield_Await
    | importCall_Yield_Await
    | callExpression_Yield_Await arguments_Yield_Await
    | callExpression_Yield_Await LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | callExpression_Yield_Await '.' IdentifierName
    | callExpression_Yield_Await templateLiteral_Yield_Await_Tagged
    | callExpression_Yield_Await '.' PrivateIdentifier
    ;

superCall
    : SuperKeyword arguments
    ;

superCall_Yield
    : SuperKeyword arguments_Yield
    ;

superCall_Await
    : SuperKeyword arguments_Await
    ;

superCall_Yield_Await
    : SuperKeyword arguments_Yield_Await
    ;

importCall
    : ImportKeyword LeftParenthesesLiteral assignmentExpression_In RightParenthesesLiteral
    ;

importCall_Yield
    : ImportKeyword LeftParenthesesLiteral assignmentExpression_In_Yield RightParenthesesLiteral
    ;

importCall_Await
    : ImportKeyword LeftParenthesesLiteral assignmentExpression_In_Await RightParenthesesLiteral
    ;

importCall_Yield_Await
    : ImportKeyword LeftParenthesesLiteral assignmentExpression_In_Yield_Await RightParenthesesLiteral
    ;

arguments
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList ',' RightParenthesesLiteral
    ;

arguments_Yield
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield ',' RightParenthesesLiteral
    ;

arguments_Await
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Await RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Await ',' RightParenthesesLiteral
    ;

arguments_Yield_Await
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield_Await ',' RightParenthesesLiteral
    ;

argumentList
    : assignmentExpression_In
    | ThreeDotsLiteral assignmentExpression_In
    | argumentList ',' assignmentExpression_In
    | argumentList ',' ThreeDotsLiteral assignmentExpression_In
    ;

argumentList_Yield
    : assignmentExpression_In_Yield
    | ThreeDotsLiteral assignmentExpression_In_Yield
    | argumentList_Yield ',' assignmentExpression_In_Yield
    | argumentList_Yield ',' ThreeDotsLiteral assignmentExpression_In_Yield
    ;

argumentList_Await
    : assignmentExpression_In_Await
    | ThreeDotsLiteral assignmentExpression_In_Await
    | argumentList_Await ',' assignmentExpression_In_Await
    | argumentList_Await ',' ThreeDotsLiteral assignmentExpression_In_Await
    ;

argumentList_Yield_Await
    : assignmentExpression_In_Yield_Await
    | ThreeDotsLiteral assignmentExpression_In_Yield_Await
    | argumentList_Yield_Await ',' assignmentExpression_In_Yield_Await
    | argumentList_Yield_Await ',' ThreeDotsLiteral assignmentExpression_In_Yield_Await
    ;

optionalExpression
    : memberExpression optionalChain
    | callExpression optionalChain
    | optionalExpression optionalChain
    ;

optionalExpression_Yield
    : memberExpression_Yield optionalChain_Yield
    | callExpression_Yield optionalChain_Yield
    | optionalExpression_Yield optionalChain_Yield
    ;

optionalExpression_Await
    : memberExpression_Await optionalChain_Await
    | callExpression_Await optionalChain_Await
    | optionalExpression_Await optionalChain_Await
    ;

optionalExpression_Yield_Await
    : memberExpression_Yield_Await optionalChain_Yield_Await
    | callExpression_Yield_Await optionalChain_Yield_Await
    | optionalExpression_Yield_Await optionalChain_Yield_Await
    ;

optionalChain
    : '?.' arguments
    | '?.' LeftBracketsLiteral expression_In RightBracketsLiteral
    | '?.' IdentifierName
    | '?.' templateLiteral_Tagged
    | '?.' PrivateIdentifier
    | optionalChain arguments
    | optionalChain LeftBracketsLiteral expression_In RightBracketsLiteral
    | optionalChain '.' IdentifierName
    | optionalChain templateLiteral_Tagged
    | optionalChain '.' PrivateIdentifier
    ;

optionalChain_Yield
    : '?.' arguments_Yield
    | '?.' LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | '?.' IdentifierName
    | '?.' templateLiteral_Yield_Tagged
    | '?.' PrivateIdentifier
    | optionalChain_Yield arguments_Yield
    | optionalChain_Yield LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | optionalChain_Yield '.' IdentifierName
    | optionalChain_Yield templateLiteral_Yield_Tagged
    | optionalChain_Yield '.' PrivateIdentifier
    ;

optionalChain_Await
    : '?.' arguments_Await
    | '?.' LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | '?.' IdentifierName
    | '?.' templateLiteral_Await_Tagged
    | '?.' PrivateIdentifier
    | optionalChain_Await arguments_Await
    | optionalChain_Await LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | optionalChain_Await '.' IdentifierName
    | optionalChain_Await templateLiteral_Await_Tagged
    | optionalChain_Await '.' PrivateIdentifier
    ;

optionalChain_Yield_Await
    : '?.' arguments_Yield_Await
    | '?.' LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | '?.' IdentifierName
    | '?.' templateLiteral_Yield_Await_Tagged
    | '?.' PrivateIdentifier
    | optionalChain_Yield_Await arguments_Yield_Await
    | optionalChain_Yield_Await LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | optionalChain_Yield_Await '.' IdentifierName
    | optionalChain_Yield_Await templateLiteral_Yield_Await_Tagged
    | optionalChain_Yield_Await '.' PrivateIdentifier
    ;

leftHandSideExpression
    : newExpression
    | callExpression
    | optionalExpression
    ;

leftHandSideExpression_Yield
    : newExpression_Yield
    | callExpression_Yield
    | optionalExpression_Yield
    ;

leftHandSideExpression_Await
    : newExpression_Await
    | callExpression_Await
    | optionalExpression_Await
    ;

leftHandSideExpression_Yield_Await
    : newExpression_Yield_Await
    | callExpression_Yield_Await
    | optionalExpression_Yield_Await
    ;

// 13.4 Update Expressions

updateExpression
    : leftHandSideExpression
    | leftHandSideExpression /* TODO: [no LineTerminator here] */ '++'
    | leftHandSideExpression /* TODO: [no LineTerminator here] */ '--'
    | '++' unaryExpression
    | '--' unaryExpression
    ;

updateExpression_Yield
    : leftHandSideExpression_Yield
    | leftHandSideExpression_Yield /* TODO: [no LineTerminator here] */ '++'
    | leftHandSideExpression_Yield /* TODO: [no LineTerminator here] */ '--'
    | '++' unaryExpression_Yield
    | '--' unaryExpression_Yield
    ;

updateExpression_Await
    : leftHandSideExpression_Await
    | leftHandSideExpression_Await /* TODO: [no LineTerminator here] */ '++'
    | leftHandSideExpression_Await /* TODO: [no LineTerminator here] */ '--'
    | '++' unaryExpression_Await
    | '--' unaryExpression_Await
    ;

updateExpression_Yield_Await
    : leftHandSideExpression_Yield_Await
    | leftHandSideExpression_Yield_Await /* TODO: [no LineTerminator here] */ '++'
    | leftHandSideExpression_Yield_Await /* TODO: [no LineTerminator here] */ '--'
    | '++' unaryExpression_Yield_Await
    | '--' unaryExpression_Yield_Await
    ;

// 13.5 Unary Operators

unaryExpression
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | '+' | '-' | '~' | '!')* (updateExpression /*| awaitExpression*/)
    ;

unaryExpression_Yield
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | '+' | '-' | '~' | '!')* (updateExpression_Yield /*| awaitExpression_Yield*/)
    ;

unaryExpression_Await
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | '+' | '-' | '~' | '!')* (updateExpression_Await) // TODO: | awaitExpression)
    ;

unaryExpression_Yield_Await
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | '+' | '-' | '~' | '!')* (updateExpression_Yield_Await) // TODO: | awaitExpression_Yield)
    ;

// 13.6 Exponentiation Operator

exponentiationExpression
    : (updateExpression ExponentiatePunctuator)* unaryExpression
    ;

exponentiationExpression_Yield
    : (updateExpression_Yield ExponentiatePunctuator)* unaryExpression_Yield
    ;

exponentiationExpression_Await
    : (updateExpression_Await ExponentiatePunctuator)* unaryExpression_Await
    ;

exponentiationExpression_Yield_Await
    : (updateExpression_Yield_Await ExponentiatePunctuator)* unaryExpression_Yield_Await
    ;

// 13.7 Multiplicative Operators

multiplicativeExpression
    : exponentiationExpression (multiplicativeOperator exponentiationExpression)*
    ;

multiplicativeExpression_Yield
    : exponentiationExpression_Yield (multiplicativeOperator exponentiationExpression_Yield)*
    ;

multiplicativeExpression_Await
    : exponentiationExpression_Await (multiplicativeOperator exponentiationExpression_Await)*
    ;

multiplicativeExpression_Yield_Await
    : exponentiationExpression_Yield_Await (multiplicativeOperator exponentiationExpression_Yield_Await)*
    ;

multiplicativeOperator
    : '*'
    | '/'
    | '%'
    ;

// 13.8 Additive Operators

additiveExpression
    : multiplicativeExpression
    | additiveExpression '+' multiplicativeExpression
    | additiveExpression '-' multiplicativeExpression
    ;

additiveExpression_Yield
    : multiplicativeExpression_Yield
    | additiveExpression_Yield '+' multiplicativeExpression_Yield
    | additiveExpression_Yield '-' multiplicativeExpression_Yield
    ;

additiveExpression_Await
    : multiplicativeExpression_Await
    | additiveExpression_Await '+' multiplicativeExpression_Await
    | additiveExpression_Await '-' multiplicativeExpression_Await
    ;

additiveExpression_Yield_Await
    : multiplicativeExpression_Yield_Await
    | additiveExpression_Yield_Await '+' multiplicativeExpression_Yield_Await
    | additiveExpression_Yield_Await '-' multiplicativeExpression_Yield_Await
    ;

// 13.9 Bitwise Shift Operators

shiftExpression
    : additiveExpression
    | shiftExpression LeftShiftPunctuator additiveExpression
    | shiftExpression SignedRightShiftPunctuator additiveExpression
    | shiftExpression UnsignedRightShiftPunctuator additiveExpression
    ;

shiftExpression_Yield
    : additiveExpression_Yield
    | shiftExpression_Yield LeftShiftPunctuator additiveExpression_Yield
    | shiftExpression_Yield SignedRightShiftPunctuator additiveExpression_Yield
    | shiftExpression_Yield UnsignedRightShiftPunctuator additiveExpression_Yield
    ;

shiftExpression_Await
    : additiveExpression_Await
    | shiftExpression_Await LeftShiftPunctuator additiveExpression_Await
    | shiftExpression_Await SignedRightShiftPunctuator additiveExpression_Await
    | shiftExpression_Await UnsignedRightShiftPunctuator additiveExpression_Await
    ;

shiftExpression_Yield_Await
    : additiveExpression_Yield_Await
    | shiftExpression_Yield_Await LeftShiftPunctuator additiveExpression_Yield_Await
    | shiftExpression_Yield_Await SignedRightShiftPunctuator additiveExpression_Yield_Await
    | shiftExpression_Yield_Await UnsignedRightShiftPunctuator additiveExpression_Yield_Await
    ;

// 13.10 Relational Operators

relationalExpression
    : shiftExpression
    | relationalExpression '<' shiftExpression
    | relationalExpression '>' shiftExpression
    | relationalExpression '<=' shiftExpression
    | relationalExpression '>=' shiftExpression
    | relationalExpression InstanceofKeyword shiftExpression
    //| relationalExpression_In InKeyword shiftExpression
    //| PrivateIdentifier InKeyword shiftExpression
    ;

relationalExpression_In
    : shiftExpression
    | relationalExpression_In '<' shiftExpression
    | relationalExpression_In '>' shiftExpression
    | relationalExpression_In '<=' shiftExpression
    | relationalExpression_In '>=' shiftExpression
    | relationalExpression_In InstanceofKeyword shiftExpression
    | relationalExpression_In InKeyword shiftExpression
    | PrivateIdentifier InKeyword shiftExpression
    ;

relationalExpression_Yield
    : shiftExpression_Yield
    | relationalExpression_Yield '<' shiftExpression_Yield
    | relationalExpression_Yield '>' shiftExpression_Yield
    | relationalExpression_Yield '<=' shiftExpression_Yield
    | relationalExpression_Yield '>=' shiftExpression_Yield
    | relationalExpression_Yield InstanceofKeyword shiftExpression_Yield
    //| relationalExpression_In_Yield InKeyword shiftExpression_Yield
    //| PrivateIdentifier InKeyword shiftExpression_Yield
    ;

relationalExpression_Await
    : shiftExpression_Await
    | relationalExpression_Await '<' shiftExpression_Await
    | relationalExpression_Await '>' shiftExpression_Await
    | relationalExpression_Await '<=' shiftExpression_Await
    | relationalExpression_Await '>=' shiftExpression_Await
    | relationalExpression_Await InstanceofKeyword shiftExpression_Await
    //| relationalExpression_In_Await InKeyword shiftExpression_Await
    //| PrivateIdentifier InKeyword shiftExpression_Await
    ;

relationalExpression_In_Yield
    : shiftExpression_Yield
    | relationalExpression_In_Yield '<' shiftExpression_Yield
    | relationalExpression_In_Yield '>' shiftExpression_Yield
    | relationalExpression_In_Yield '<=' shiftExpression_Yield
    | relationalExpression_In_Yield '>=' shiftExpression_Yield
    | relationalExpression_In_Yield InstanceofKeyword shiftExpression_Yield
    | relationalExpression_In_Yield InKeyword shiftExpression_Yield
    | PrivateIdentifier InKeyword shiftExpression_Yield
    ;

relationalExpression_In_Await
    : shiftExpression_Await
    | relationalExpression_In_Await '<' shiftExpression_Await
    | relationalExpression_In_Await '>' shiftExpression_Await
    | relationalExpression_In_Await '<=' shiftExpression_Await
    | relationalExpression_In_Await '>=' shiftExpression_Await
    | relationalExpression_In_Await InstanceofKeyword shiftExpression_Await
    | relationalExpression_In_Await InKeyword shiftExpression_Await
    | PrivateIdentifier InKeyword shiftExpression_Await
    ;

relationalExpression_Yield_Await
    : shiftExpression_Yield_Await
    | relationalExpression_Yield_Await '<' shiftExpression_Yield_Await
    | relationalExpression_Yield_Await '>' shiftExpression_Yield_Await
    | relationalExpression_Yield_Await '<=' shiftExpression_Yield_Await
    | relationalExpression_Yield_Await '>=' shiftExpression_Yield_Await
    | relationalExpression_Yield_Await InstanceofKeyword shiftExpression_Yield_Await
    //| relationalExpression_In_Yield_Await InKeyword shiftExpression_Yield_Await
    //| PrivateIdentifier InKeyword shiftExpression_Yield_Await
    ;

relationalExpression_In_Yield_Await
    : shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await '<' shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await '>' shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await '<=' shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await '>=' shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await InstanceofKeyword shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await InKeyword shiftExpression_Yield_Await
    | PrivateIdentifier InKeyword shiftExpression_Yield_Await
    ;

// 13.11 Equality Operators

equalityExpression
    : relationalExpression
    | equalityExpression '==' relationalExpression // TODO: recursion
    | equalityExpression '!=' relationalExpression // TODO: recursion
    | equalityExpression StrictlyEqualPunctuator relationalExpression // TODO: recursion
    | equalityExpression StrictlyNotEqualPunctuator relationalExpression // TODO: recursion
    ;

equalityExpression_In
    : relationalExpression_In
    | equalityExpression_In '==' relationalExpression_In // TODO: recursion
    | equalityExpression_In '!=' relationalExpression_In // TODO: recursion
    | equalityExpression_In StrictlyEqualPunctuator relationalExpression_In // TODO: recursion
    | equalityExpression_In StrictlyNotEqualPunctuator relationalExpression_In // TODO: recursion
    ;

equalityExpression_Yield
    : relationalExpression_Yield
    | equalityExpression_Yield '==' relationalExpression_Yield // TODO: recursion
    | equalityExpression_Yield '!=' relationalExpression_Yield // TODO: recursion
    | equalityExpression_Yield StrictlyEqualPunctuator relationalExpression_Yield // TODO: recursion
    | equalityExpression_Yield StrictlyNotEqualPunctuator relationalExpression_Yield // TODO: recursion
    ;

equalityExpression_Await
    : relationalExpression_Await
    | equalityExpression_Await '==' relationalExpression_Await // TODO: recursion
    | equalityExpression_Await '!=' relationalExpression_Await // TODO: recursion
    | equalityExpression_Await StrictlyEqualPunctuator relationalExpression_Await // TODO: recursion
    | equalityExpression_Await StrictlyNotEqualPunctuator relationalExpression_Await // TODO: recursion
    ;

equalityExpression_In_Yield
    : relationalExpression_In_Yield
    | equalityExpression_In_Yield '==' relationalExpression_In_Yield // TODO: recursion
    | equalityExpression_In_Yield '!=' relationalExpression_In_Yield // TODO: recursion
    | equalityExpression_In_Yield StrictlyEqualPunctuator relationalExpression_In_Yield // TODO: recursion
    | equalityExpression_In_Yield StrictlyNotEqualPunctuator relationalExpression_In_Yield // TODO: recursion
    ;

equalityExpression_In_Await
    : relationalExpression_In_Await
    | equalityExpression_In_Await '==' relationalExpression_In_Await // TODO: recursion
    | equalityExpression_In_Await '!=' relationalExpression_In_Await // TODO: recursion
    | equalityExpression_In_Await StrictlyEqualPunctuator relationalExpression_In_Await // TODO: recursion
    | equalityExpression_In_Await StrictlyNotEqualPunctuator relationalExpression_In_Await // TODO: recursion
    ;

equalityExpression_Yield_Await
    : relationalExpression_Yield_Await
    | equalityExpression_Yield_Await '==' relationalExpression_Yield_Await // TODO: recursion
    | equalityExpression_Yield_Await '!=' relationalExpression_Yield_Await // TODO: recursion
    | equalityExpression_Yield_Await StrictlyEqualPunctuator relationalExpression_Yield_Await // TODO: recursion
    | equalityExpression_Yield_Await StrictlyNotEqualPunctuator relationalExpression_Yield_Await // TODO: recursion
    ;

equalityExpression_In_Yield_Await
    : relationalExpression_In_Yield_Await
    | equalityExpression_In_Yield_Await '==' relationalExpression_In_Yield_Await // TODO: recursion
    | equalityExpression_In_Yield_Await '!=' relationalExpression_In_Yield_Await // TODO: recursion
    | equalityExpression_In_Yield_Await StrictlyEqualPunctuator relationalExpression_In_Yield_Await // TODO: recursion
    | equalityExpression_In_Yield_Await StrictlyNotEqualPunctuator relationalExpression_In_Yield_Await // TODO: recursion
    ;

// 13.12 Binary Bitwise Operators

bitwiseANDExpression
    : equalityExpression ('&' equalityExpression)*
    ;

bitwiseANDExpression_In
    : equalityExpression_In ('&' equalityExpression_In)*
    ;

bitwiseANDExpression_Yield
    : equalityExpression_Yield ('&' equalityExpression_Yield)*
    ;

bitwiseANDExpression_Await
    : equalityExpression_Await ('&' equalityExpression_Await)*
    ;

bitwiseANDExpression_In_Yield
    : equalityExpression_In_Yield ('&' equalityExpression_In_Yield)*
    ;

bitwiseANDExpression_In_Await
    : equalityExpression_In_Await ('&' equalityExpression_In_Await)*
    ;

bitwiseANDExpression_Yield_Await
    : equalityExpression_Yield_Await ('&' equalityExpression_Yield_Await)*
    ;

bitwiseANDExpression_In_Yield_Await
    : equalityExpression_In_Yield_Await ('&' equalityExpression_In_Yield_Await)*
    ;

bitwiseXORExpression
    : bitwiseANDExpression ('^' bitwiseANDExpression)*
    ;

bitwiseXORExpression_In
    : bitwiseANDExpression_In ('^' bitwiseANDExpression_In)*
    ;

bitwiseXORExpression_Yield
    : bitwiseANDExpression_Yield ('^' bitwiseANDExpression_Yield)*
    ;

bitwiseXORExpression_Await
    : bitwiseANDExpression_Await ('^' bitwiseANDExpression_Await)*
    ;

bitwiseXORExpression_In_Yield
    : bitwiseANDExpression_In_Yield ('^' bitwiseANDExpression_In_Yield)*
    ;

bitwiseXORExpression_In_Await
    : bitwiseANDExpression_In_Await ('^' bitwiseANDExpression_In_Await)*
    ;

bitwiseXORExpression_Yield_Await
    : bitwiseANDExpression_Yield_Await ('^' bitwiseANDExpression_Yield_Await)*
    ;

bitwiseXORExpression_In_Yield_Await
    : bitwiseANDExpression_In_Yield_Await ('^' bitwiseANDExpression_In_Yield_Await)*
    ;

bitwiseORExpression
    : bitwiseXORExpression ('|' bitwiseXORExpression)*
    ;

bitwiseORExpression_In
    : bitwiseXORExpression_In ('|' bitwiseXORExpression_In)*
    ;

bitwiseORExpression_Yield
    : bitwiseXORExpression_Yield ('|' bitwiseXORExpression_Yield)*
    ;

bitwiseORExpression_Await
    : bitwiseXORExpression_Await ('|' bitwiseXORExpression_Await)*
    ;

bitwiseORExpression_In_Yield
    : bitwiseXORExpression_In_Yield ('|' bitwiseXORExpression_In_Yield)*
    ;

bitwiseORExpression_In_Await
    : bitwiseXORExpression_In_Await ('|' bitwiseXORExpression_In_Await)*
    ;

bitwiseORExpression_Yield_Await
    : bitwiseXORExpression_Yield_Await ('|' bitwiseXORExpression_Yield_Await)*
    ;

bitwiseORExpression_In_Yield_Await
    : bitwiseXORExpression_In_Yield_Await ('|' bitwiseXORExpression_In_Yield_Await)*
    ;

// 13.13 Binary Logical Operators

logicalANDExpression
    : bitwiseORExpression (LogicalANDPunctuator bitwiseORExpression)*
    ;

logicalANDExpression_In
    : bitwiseORExpression_In (LogicalANDPunctuator bitwiseORExpression_In)*
    ;

logicalANDExpression_Yield
    : bitwiseORExpression_Yield (LogicalANDPunctuator bitwiseORExpression_Yield)*
    ;

logicalANDExpression_Await
    : bitwiseORExpression_Await (LogicalANDPunctuator bitwiseORExpression_Await)*
    ;

logicalANDExpression_In_Yield
    : bitwiseORExpression_In_Yield (LogicalANDPunctuator bitwiseORExpression_In_Yield)*
    ;

logicalANDExpression_In_Await
    : bitwiseORExpression_In_Await (LogicalANDPunctuator bitwiseORExpression_In_Await)*
    ;

logicalANDExpression_Yield_Await
    : bitwiseORExpression_Yield_Await (LogicalANDPunctuator bitwiseORExpression_Yield_Await)*
    ;

logicalANDExpression_In_Yield_Await
    : bitwiseORExpression_In_Yield_Await (LogicalANDPunctuator bitwiseORExpression_In_Yield_Await)*
    ;

logicalORExpression
    : logicalANDExpression ('||' logicalANDExpression)*
    ;

logicalORExpression_In
    : logicalANDExpression_In ('||' logicalANDExpression_In)*
    ;

logicalORExpression_Yield
    : logicalANDExpression_Yield ('||' logicalANDExpression_Yield)*
    ;

logicalORExpression_Await
    : logicalANDExpression_Await ('||' logicalANDExpression_Await)*
    ;

logicalORExpression_In_Yield
    : logicalANDExpression_In_Yield ('||' logicalANDExpression_In_Yield)*
    ;

logicalORExpression_In_Await
    : logicalANDExpression_In_Await ('||' logicalANDExpression_In_Await)*
    ;

logicalORExpression_Yield_Await
    : logicalANDExpression_Yield_Await ('||' logicalANDExpression_Yield_Await)*
    ;

logicalORExpression_In_Yield_Await
    : logicalANDExpression_In_Yield_Await ('||' logicalANDExpression_In_Yield_Await)*
    ;

coalesceExpression
    : coalesceExpressionHead CoalescePunctuator bitwiseORExpression
    ;

coalesceExpressionHead
    : coalesceExpression // TODO: recursion
    | bitwiseORExpression
    ;

coalesceExpression_In
    : coalesceExpressionHead_In CoalescePunctuator bitwiseORExpression_In
    ;

coalesceExpressionHead_In
    : coalesceExpression_In // TODO: recursion
    | bitwiseORExpression_In
    ;

coalesceExpression_Yield
    : coalesceExpressionHead_Yield CoalescePunctuator bitwiseORExpression_Yield
    ;

coalesceExpressionHead_Yield
    : coalesceExpression_Yield // TODO: recursion
    | bitwiseORExpression_Yield
    ;

coalesceExpression_Await
    : coalesceExpressionHead_Await CoalescePunctuator bitwiseORExpression_Await
    ;

coalesceExpressionHead_Await
    : coalesceExpression_Await // TODO: recursion
    | bitwiseORExpression_Await
    ;

coalesceExpression_In_Yield
    : coalesceExpressionHead_In_Yield CoalescePunctuator bitwiseORExpression_In_Yield
    ;

coalesceExpressionHead_In_Yield
    : coalesceExpression_In_Yield // TODO: recursion
    | bitwiseORExpression_In_Yield
    ;

coalesceExpression_In_Await
    : coalesceExpressionHead_In_Await CoalescePunctuator bitwiseORExpression_In_Await
    ;

coalesceExpressionHead_In_Await
    : coalesceExpression_In_Await // TODO: recursion
    | bitwiseORExpression_In_Await
    ;

coalesceExpression_Yield_Await
    : coalesceExpressionHead_Yield_Await CoalescePunctuator bitwiseORExpression_Yield_Await
    ;

coalesceExpressionHead_Yield_Await
    : coalesceExpression_Yield_Await // TODO: recursion
    | bitwiseORExpression_Yield_Await
    ;

coalesceExpression_In_Yield_Await
    : coalesceExpressionHead_In_Yield_Await CoalescePunctuator bitwiseORExpression_In_Yield_Await
    ;

coalesceExpressionHead_In_Yield_Await
    : coalesceExpression_In_Yield_Await // TODO: recursion
    | bitwiseORExpression_In_Yield_Await
    ;

shortCircuitExpression
    : logicalORExpression
    | coalesceExpression
    ;

shortCircuitExpression_In
    : logicalORExpression_In
    | coalesceExpression_In
    ;

shortCircuitExpression_Yield
    : logicalORExpression_Yield
    | coalesceExpression_Yield
    ;

shortCircuitExpression_Await
    : logicalORExpression_Await
    | coalesceExpression_Await
    ;

shortCircuitExpression_In_Yield
    : logicalORExpression_In_Yield
    | coalesceExpression_In_Yield
    ;

shortCircuitExpression_In_Await
    : logicalORExpression_In_Await
    | coalesceExpression_In_Await
    ;

shortCircuitExpression_Yield_Await
    : logicalORExpression_Yield_Await
    | coalesceExpression_Yield_Await
    ;

shortCircuitExpression_In_Yield_Await
    : logicalORExpression_In_Yield_Await
    | coalesceExpression_In_Yield_Await
    ;

// 13.14 Conditional Operator ( ? : )

conditionalExpression
    : shortCircuitExpression
    | shortCircuitExpression '?' assignmentExpression_In ':' assignmentExpression
    ;

conditionalExpression_In
    : shortCircuitExpression_In
    | shortCircuitExpression_In '?' assignmentExpression_In ':' assignmentExpression_In
    ;

conditionalExpression_Yield
    : shortCircuitExpression_Yield
    | shortCircuitExpression_Yield '?' assignmentExpression_In_Yield ':' assignmentExpression_Yield
    ;

conditionalExpression_Await
    : shortCircuitExpression_Await
    | shortCircuitExpression_Await '?' assignmentExpression_In_Await ':' assignmentExpression_Await
    ;

conditionalExpression_In_Yield
    : shortCircuitExpression_In_Yield
    | shortCircuitExpression_In_Yield '?' assignmentExpression_In_Yield ':' assignmentExpression_In_Yield
    ;

conditionalExpression_In_Await
    : shortCircuitExpression_In_Await
    | shortCircuitExpression_In_Await '?' assignmentExpression_In_Await ':' assignmentExpression_In_Await
    ;

conditionalExpression_Yield_Await
    : shortCircuitExpression_Yield_Await
    | shortCircuitExpression_Yield_Await '?' assignmentExpression_In_Yield_Await ':' assignmentExpression_Yield_Await
    ;

conditionalExpression_In_Yield_Await
    : shortCircuitExpression_In_Yield_Await
    | shortCircuitExpression_In_Yield_Await '?' assignmentExpression_In_Yield_Await ':' assignmentExpression_In_Yield_Await
    ;

// 13.15 Assignment Operators

assignmentExpression
    : conditionalExpression
    //| yieldExpression
    // TODO: | arrowFunction
    // TODO: | asyncArrowFunction
    | leftHandSideExpression '=' assignmentExpression // TODO: recursion
    | leftHandSideExpression assignmentOperator assignmentExpression // TODO: recursion
    | leftHandSideExpression LogicalANDAssignmentPunctuator assignmentExpression // TODO: recursion
    | leftHandSideExpression LogicalORAssignmentPunctuator assignmentExpression // TODO: recursion
    | leftHandSideExpression CoalesceAssignmentPunctuator assignmentExpression // TODO: recursion
    ;

assignmentExpression_In
    : conditionalExpression_In
    //| yieldExpression_In
    // TODO: | arrowFunction_In
    // TODO: | asyncArrowFunction_In
    | leftHandSideExpression '=' assignmentExpression_In // TODO: recursion
    | leftHandSideExpression assignmentOperator assignmentExpression_In // TODO: recursion
    | leftHandSideExpression LogicalANDAssignmentPunctuator assignmentExpression_In // TODO: recursion
    | leftHandSideExpression LogicalORAssignmentPunctuator assignmentExpression_In // TODO: recursion
    | leftHandSideExpression CoalesceAssignmentPunctuator assignmentExpression_In // TODO: recursion
    ;

assignmentExpression_Yield
    : conditionalExpression_Yield
    // TODO: | yieldExpression
    // TODO: | arrowFunction_Yield
    // TODO: | asyncArrowFunction_Yield
    | leftHandSideExpression_Yield '=' assignmentExpression_Yield // TODO: recursion
    | leftHandSideExpression_Yield assignmentOperator assignmentExpression_Yield // TODO: recursion
    | leftHandSideExpression_Yield LogicalANDAssignmentPunctuator assignmentExpression_Yield // TODO: recursion
    | leftHandSideExpression_Yield LogicalORAssignmentPunctuator assignmentExpression_Yield // TODO: recursion
    | leftHandSideExpression_Yield CoalesceAssignmentPunctuator assignmentExpression_Yield // TODO: recursion
    ;

assignmentExpression_Await
    : conditionalExpression_Await
    //| yieldExpression
    // TODO: | arrowFunction_Await
    // TODO: | asyncArrowFunction_Await
    | leftHandSideExpression_Await '=' assignmentExpression_Await // TODO: recursion
    | leftHandSideExpression_Await assignmentOperator assignmentExpression_Await // TODO: recursion
    | leftHandSideExpression_Await LogicalANDAssignmentPunctuator assignmentExpression_Await // TODO: recursion
    | leftHandSideExpression_Await LogicalORAssignmentPunctuator assignmentExpression_Await // TODO: recursion
    | leftHandSideExpression_Await CoalesceAssignmentPunctuator assignmentExpression_Await // TODO: recursion
    ;

assignmentExpression_In_Yield
    : conditionalExpression_In_Yield
    // TODO: | yieldExpression_In
    // TODO: | arrowFunction_In_Yield
    // TODO: | asyncArrowFunction_In_Yield
    | leftHandSideExpression_Yield '=' assignmentExpression_In_Yield // TODO: recursion
    | leftHandSideExpression_Yield assignmentOperator assignmentExpression_In_Yield // TODO: recursion
    | leftHandSideExpression_Yield LogicalANDAssignmentPunctuator assignmentExpression_In_Yield // TODO: recursion
    | leftHandSideExpression_Yield LogicalORAssignmentPunctuator assignmentExpression_In_Yield // TODO: recursion
    | leftHandSideExpression_Yield CoalesceAssignmentPunctuator assignmentExpression_In_Yield // TODO: recursion
    ;

assignmentExpression_In_Await
    : conditionalExpression_In_Await
    //| yieldExpression_In_Await
    // TODO: | arrowFunction_In_Await
    // TODO: | asyncArrowFunction_In_Await
    | leftHandSideExpression_Await '=' assignmentExpression_In_Await // TODO: recursion
    | leftHandSideExpression_Await assignmentOperator assignmentExpression_In_Await // TODO: recursion
    | leftHandSideExpression_Await LogicalANDAssignmentPunctuator assignmentExpression_In_Await // TODO: recursion
    | leftHandSideExpression_Await LogicalORAssignmentPunctuator assignmentExpression_In_Await // TODO: recursion
    | leftHandSideExpression_Await CoalesceAssignmentPunctuator assignmentExpression_In_Await // TODO: recursion
    ;

assignmentExpression_Yield_Await
    : conditionalExpression_Yield_Await
    // TODO: | yieldExpression_Await
    // TODO: | arrowFunction_Yield_Await
    // TODO: | asyncArrowFunction_Yield_Await
    | leftHandSideExpression_Yield_Await '=' assignmentExpression_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await assignmentOperator assignmentExpression_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await LogicalANDAssignmentPunctuator assignmentExpression_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await LogicalORAssignmentPunctuator assignmentExpression_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await CoalesceAssignmentPunctuator assignmentExpression_Yield_Await // TODO: recursion
    ;

assignmentExpression_In_Yield_Await
    : conditionalExpression_In_Yield_Await
    // TODO: | yieldExpression_In_Await
    // TODO: | arrowFunction_In_Yield_Await
    // TODO: | asyncArrowFunction_In_Yield_Await
    | leftHandSideExpression_Yield_Await '=' assignmentExpression_In_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await assignmentOperator assignmentExpression_In_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await LogicalANDAssignmentPunctuator assignmentExpression_In_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await LogicalORAssignmentPunctuator assignmentExpression_In_Yield_Await // TODO: recursion
    | leftHandSideExpression_Yield_Await CoalesceAssignmentPunctuator assignmentExpression_In_Yield_Await // TODO: recursion
    ;

assignmentOperator
    : '*='
    | '/='
    | '%='
    | '+='
    | '-='
    | LeftShiftAssignmentPunctuator
    | SignedRightShiftAssignmentPunctuator
    | UnsignedRightShiftAssignmentPunctuator
    | '&='
    | '^='
    | '|='
    | ExponentiateAssignmentPunctuator
    ;

// 13.16 Comma Operator ( , )

expression
    : assignmentExpression (',' assignmentExpression)*
    ;

expression_In
    : assignmentExpression_In (',' assignmentExpression_In)*
    ;

expression_Yield
    : assignmentExpression_Yield (',' assignmentExpression_Yield)*
    ;

expression_Await
    : assignmentExpression_Await (',' assignmentExpression_Await)*
    ;

expression_In_Yield
    : assignmentExpression_In_Yield (',' assignmentExpression_In_Yield)*
    ;

expression_In_Await
    : assignmentExpression_In_Await (',' assignmentExpression_In_Await)*
    ;

expression_Yield_Await
    : assignmentExpression_Yield_Await (',' assignmentExpression_Yield_Await)*
    ;

expression_In_Yield_Await
    : assignmentExpression_In_Yield_Await (',' assignmentExpression_In_Yield_Await)*
    ;

// 12 ECMAScript Language: Lexical Grammar

inputElementDiv
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | DivPunctuator
    | RightBracePunctuator
    ;

inputElementRegExp
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | RightBracePunctuator
    | RegularExpressionLiteral
    ;

inputElementRegExpOrTemplateTail
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | RegularExpressionLiteral
    | TemplateSubstitutionTail
    ;

inputElementTemplateTail
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | DivPunctuator
    | TemplateSubstitutionTail
    ;

inputElementHashbangOrRegExp
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | HashbangComment
    | RegularExpressionLiteral
    ;

program
    : EOF
    ;
