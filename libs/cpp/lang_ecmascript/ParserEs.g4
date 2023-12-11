// https://tc39.es/ecma262/2023

parser grammar ParserEs;

options {
    tokenVocab=LexerEs;
    superClass=ParserEsBase;
}

@header {
    #include "lang_ecmascript/ParserEsBase.hpp"
}

// 15 ECMAScript Language: Functions and Classes

// 15.1 Parameter Lists

uniqueFormalParameters
    : formalParameters
    ;

uniqueFormalParameters_Yield
    : formalParameters_Yield
    ;

uniqueFormalParameters_Await
    : formalParameters_Await
    ;

uniqueFormalParameters_Yield_Await
    : formalParameters_Yield_Await
    ;

formalParameters
    :
    | functionRestParameter
    | formalParameterList CommaPunctuator?
    | formalParameterList CommaPunctuator functionRestParameter
    ;

formalParameters_Yield
    :
    | functionRestParameter_Yield
    | formalParameterList_Yield CommaPunctuator?
    | formalParameterList_Yield CommaPunctuator functionRestParameter_Yield
    ;

formalParameters_Await
    :
    | functionRestParameter_Await
    | formalParameterList_Await CommaPunctuator?
    | formalParameterList_Await CommaPunctuator functionRestParameter_Await
    ;

formalParameters_Yield_Await
    :
    | functionRestParameter_Yield_Await
    | formalParameterList_Yield_Await CommaPunctuator?
    | formalParameterList_Yield_Await CommaPunctuator functionRestParameter_Yield_Await
    ;

formalParameterList
    : formalParameter (CommaPunctuator formalParameter)*
    ;

formalParameterList_Yield
    : formalParameter_Yield (CommaPunctuator formalParameter_Yield)*
    ;

formalParameterList_Await
    : formalParameter_Await (CommaPunctuator formalParameter_Await)*
    ;

formalParameterList_Yield_Await
    : formalParameter_Yield_Await (CommaPunctuator formalParameter_Yield_Await)*
    ;

functionRestParameter
    : bindingRestElement
    ;

functionRestParameter_Yield
    : bindingRestElement_Yield
    ;

functionRestParameter_Await
    : bindingRestElement_Await
    ;

functionRestParameter_Yield_Await
    : bindingRestElement_Yield_Await
    ;

formalParameter
    : bindingElement
    ;

formalParameter_Yield
    : bindingElement_Yield
    ;

formalParameter_Await
    : bindingElement_Await
    ;

formalParameter_Yield_Await
    : bindingElement_Yield_Await
    ;

// 15.2 Function Definitions

functionDeclaration
    : FunctionKeyword bindingIdentifier LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    // | FunctionKeyword LeftParenthesesLiteral formalParameter RightParenthesesLiteral
    //   LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Yield
    : FunctionKeyword bindingIdentifier_Yield LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    // | FunctionKeyword LeftParenthesesLiteral formalParameter RightParenthesesLiteral
    //   LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Await
    : FunctionKeyword bindingIdentifier_Await LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    // | FunctionKeyword LeftParenthesesLiteral formalParameter RightParenthesesLiteral
    //   LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Default
    : FunctionKeyword bindingIdentifier? LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Yield_Await
    : FunctionKeyword bindingIdentifier_Yield_Await LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    // | FunctionKeyword LeftParenthesesLiteral formalParameter RightParenthesesLiteral
    //   LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Yield_Default
    : FunctionKeyword bindingIdentifier_Yield? LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Await_Default
    : FunctionKeyword bindingIdentifier_Await? LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionDeclaration_Yield_Await_Default
    : FunctionKeyword bindingIdentifier_Yield_Await? LeftParenthesesLiteral formalParameter RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionExpression
    : FunctionKeyword bindingIdentifier? LeftParenthesesLiteral formalParameters RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

functionBody
    : functionStatementList
    ;

functionBody_Yield
    : functionStatementList_Yield
    ;

functionBody_Await
    : functionStatementList_Await
    ;

functionBody_Yield_Await
    : functionStatementList_Yield_Await
    ;

functionStatementList
    : statementList_Return?
    ;

functionStatementList_Yield
    : statementList_Yield_Return?
    ;

functionStatementList_Await
    : statementList_Await_Return?
    ;

functionStatementList_Yield_Await
    : statementList_Yield_Await_Return?
    ;

// 15.3 Arrow Function Definitions

arrowFunction
    : arrowParameters /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody
    ;

arrowFunction_In
    : arrowParameters /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody_In
    ;

arrowFunction_Yield
    : arrowParameters_Yield /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody
    ;

arrowFunction_Await
    : arrowParameters_Await /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody
    ;

arrowFunction_In_Yield
    : arrowParameters_Yield /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody_In
    ;

arrowFunction_In_Await
    : arrowParameters_Await /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody_In
    ;

arrowFunction_Yield_Await
    : arrowParameters_Yield /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody
    ;

arrowFunction_In_Yield_Await
    : arrowParameters_Yield_Await /* TODO: no LineTerminator here */ ArrowPunctuator conciseBody_In
    ;

arrowParameters
    : bindingIdentifier
    | coverParenthesizedExpressionAndArrowParameterList
    ;

arrowParameters_Yield
    : bindingIdentifier_Yield
    | coverParenthesizedExpressionAndArrowParameterList_Yield
    ;

arrowParameters_Await
    : bindingIdentifier_Await
    | coverParenthesizedExpressionAndArrowParameterList_Await
    ;

arrowParameters_Yield_Await
    : bindingIdentifier_Yield_Await
    | coverParenthesizedExpressionAndArrowParameterList_Yield_Await
    ;

conciseBody
    : {!nextTextEq("{")}? expressionBody
    | LeftBracesLiteral functionBody RightBracesLiteral
    ;

conciseBody_In
    : {!nextTextEq("{")}? expressionBody_In
    | LeftBracesLiteral functionBody RightBracesLiteral
    ;

expressionBody
    : assignmentExpression
    ;

expressionBody_In
    : assignmentExpression_In
    ;

expressionBody_Await
    : assignmentExpression_Await
    ;

expressionBody_In_Await
    : assignmentExpression_In_Await
    ;

// 15.4 Method Definitions

methodDefinition
    : classElementName LeftParenthesesLiteral uniqueFormalParameters RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | generatorMethod
    | asyncMethod
    | asyncGeneratorMethod
    | GetKeyword classElementName LeftParenthesesLiteral RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | SetKeyword classElementName LeftParenthesesLiteral propertySetParameterList RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

methodDefinition_Yield
    : classElementName_Yield LeftParenthesesLiteral uniqueFormalParameters RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | generatorMethod_Yield
    | asyncMethod_Yield
    | asyncGeneratorMethod_Yield
    | GetKeyword classElementName_Yield LeftParenthesesLiteral RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | SetKeyword classElementName_Yield LeftParenthesesLiteral propertySetParameterList RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

methodDefinition_Await
    : classElementName_Await LeftParenthesesLiteral uniqueFormalParameters RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | generatorMethod_Await
    | asyncMethod_Await
    | asyncGeneratorMethod_Await
    | GetKeyword classElementName_Await LeftParenthesesLiteral RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | SetKeyword classElementName_Await LeftParenthesesLiteral propertySetParameterList RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

methodDefinition_Yield_Await
    : classElementName_Yield_Await LeftParenthesesLiteral uniqueFormalParameters RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | generatorMethod_Yield_Await
    | asyncMethod_Yield_Await
    | asyncGeneratorMethod_Yield_Await
    | GetKeyword classElementName_Yield_Await LeftParenthesesLiteral RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    | SetKeyword classElementName_Yield_Await LeftParenthesesLiteral propertySetParameterList RightParenthesesLiteral
      LeftBracesLiteral functionBody RightBracesLiteral
    ;

propertySetParameterList
    : formalParameter
    ;

// 15.5 Generator Function Definitions

generatorDeclaration
    : FunctionKeyword MultiplyPunctuator bindingIdentifier LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    // | FunctionKeyword MultiplyPunctuator LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
    //   LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Yield
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    // | FunctionKeyword MultiplyPunctuator LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
    //   LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Await
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Await LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    // | FunctionKeyword MultiplyPunctuator LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
    //   LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Default
    : FunctionKeyword MultiplyPunctuator bindingIdentifier? LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Yield_Await
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield_Await LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    // | FunctionKeyword MultiplyPunctuator LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
    //   LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Yield_Default
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield? LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Await_Default
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Await? LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorDeclaration_Yield_Await_Default
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield_Await? LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorExpression
    : FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield? LeftParenthesesLiteral formalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorMethod
    : MultiplyPunctuator classElementName LeftParenthesesLiteral uniqueFormalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorMethod_Yield
    : MultiplyPunctuator classElementName_Yield LeftParenthesesLiteral uniqueFormalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorMethod_Await
    : MultiplyPunctuator classElementName_Await LeftParenthesesLiteral uniqueFormalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorMethod_Yield_Await
    : MultiplyPunctuator classElementName_Yield_Await LeftParenthesesLiteral uniqueFormalParameters_Yield RightParenthesesLiteral
      LeftBracesLiteral generatorBody RightBracesLiteral
    ;

generatorBody
    : functionBody_Yield
    ;

yieldExpression
    : YieldKeyword
    | YieldKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator? assignmentExpression_Yield
    ;

yieldExpression_In
    : YieldKeyword
    | YieldKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator? assignmentExpression_In_Yield
    ;

yieldExpression_Await
    : YieldKeyword
    | YieldKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator? assignmentExpression_Yield_Await
    ;

yieldExpression_In_Await
    : YieldKeyword
    | YieldKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator? assignmentExpression_In_Yield_Await
    ;

// 15.6 Async Generator Function Definitions

asyncGeneratorDeclaration
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    // | AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator
    //   LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
    //   LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Yield
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    // | AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator
    //   LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
    //   LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Await
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    // | AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator
    //   LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
    //   LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier?
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Yield_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield_Await
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    // | AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator
    //   LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
    //   LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Yield_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield?
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Await_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Await?
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorDeclaration_Yield_Await_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield_Await?
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorExpression
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword MultiplyPunctuator bindingIdentifier_Yield_Await?
      LeftParenthesesLiteral formalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorMethod
    : AsyncKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator classElementName
      LeftParenthesesLiteral uniqueFormalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorMethod_Yield
    : AsyncKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator classElementName_Yield
      LeftParenthesesLiteral uniqueFormalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorMethod_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator classElementName_Await
      LeftParenthesesLiteral uniqueFormalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorMethod_Yield_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ MultiplyPunctuator classElementName_Yield_Await
      LeftParenthesesLiteral uniqueFormalParameters_Yield_Await RightParenthesesLiteral
      LeftBracesLiteral asyncGeneratorBody RightBracesLiteral
    ;

asyncGeneratorBody
    : functionBody_Yield_Await
    ;

// 15.7 Class Definitions

classDeclaration
    : ClassKeyword bindingIdentifier classTail
    ;

classDeclaration_Yield
    : ClassKeyword bindingIdentifier_Yield classTail_Yield
    ;

classDeclaration_Await
    : ClassKeyword bindingIdentifier_Await classTail_Await
    ;

classDeclaration_Default
    : ClassKeyword bindingIdentifier? classTail
    ;

classDeclaration_Yield_Await
    : ClassKeyword bindingIdentifier_Yield_Await classTail_Yield_Await
    ;

classDeclaration_Yield_Default
    : ClassKeyword bindingIdentifier_Yield? classTail_Yield
    ;

classDeclaration_Await_Default
    : ClassKeyword bindingIdentifier_Await? classTail_Await
    ;

classDeclaration_Yield_Await_Default
    : ClassKeyword bindingIdentifier_Yield_Await? classTail_Yield_Await
    ;

classExpression
    : ClassKeyword bindingIdentifier? classTail
    ;

classExpression_Yield
    : ClassKeyword bindingIdentifier_Yield? classTail_Yield
    ;

classExpression_Await
    : ClassKeyword bindingIdentifier_Await? classTail_Await
    ;

classExpression_Yield_Await
    : ClassKeyword bindingIdentifier_Yield_Await? classTail_Yield_Await
    ;

classTail
    : classHeritage? LeftBracesLiteral classBody? RightBracesLiteral
    ;

classTail_Yield
    : classHeritage_Yield? LeftBracesLiteral classBody_Yield? RightBracesLiteral
    ;

classTail_Await
    : classHeritage_Await? LeftBracesLiteral classBody_Await? RightBracesLiteral
    ;

classTail_Yield_Await
    : classHeritage_Yield_Await? LeftBracesLiteral classBody_Yield_Await? RightBracesLiteral
    ;

classHeritage
    : ExtendsKeyword leftHandSideExpression
    ;

classHeritage_Yield
    : ExtendsKeyword leftHandSideExpression_Yield
    ;

classHeritage_Await
    : ExtendsKeyword leftHandSideExpression_Await
    ;

classHeritage_Yield_Await
    : ExtendsKeyword leftHandSideExpression_Yield_Await
    ;

classBody
    : classElementList
    ;

classBody_Yield
    : classElementList_Yield
    ;

classBody_Await
    : classElementList_Await
    ;

classBody_Yield_Await
    : classElementList_Yield_Await
    ;

classElementList
    : classElement+
    ;

classElementList_Yield
    : classElement_Yield+
    ;

classElementList_Await
    : classElement_Await+
    ;

classElementList_Yield_Await
    : classElement_Yield_Await+
    ;

classElement
    : StaticKeyword? methodDefinition
    | StaticKeyword? fieldDefinition SemicolonPunctuator
    | classStaticBlock
    | SemicolonPunctuator
    ;

classElement_Yield
    : StaticKeyword? methodDefinition_Yield
    | StaticKeyword? fieldDefinition_Yield SemicolonPunctuator
    | classStaticBlock
    | SemicolonPunctuator
    ;

classElement_Await
    : StaticKeyword? methodDefinition_Await
    | StaticKeyword? fieldDefinition_Await SemicolonPunctuator
    | classStaticBlock
    | SemicolonPunctuator
    ;

classElement_Yield_Await
    : StaticKeyword? methodDefinition_Yield_Await
    | StaticKeyword? fieldDefinition_Yield_Await SemicolonPunctuator
    | classStaticBlock
    | SemicolonPunctuator
    ;

fieldDefinition
    : classElementName initializer_In?
    ;

fieldDefinition_Yield
    : classElementName_Yield initializer_In_Yield?
    ;

fieldDefinition_Await
    : classElementName_Await initializer_In_Await?
    ;

fieldDefinition_Yield_Await
    : classElementName_Yield_Await initializer_In_Yield_Await?
    ;

classElementName
    : propertyName
    | PrivateIdentifier
    ;

classElementName_Yield
    : propertyName_Yield
    | PrivateIdentifier
    ;

classElementName_Await
    : propertyName_Await
    | PrivateIdentifier
    ;

classElementName_Yield_Await
    : propertyName_Yield_Await
    | PrivateIdentifier
    ;

classStaticBlock
    : StaticKeyword LeftBracesLiteral classStaticBlockBody RightBracesLiteral
    ;

classStaticBlockBody
    : classStaticBlockStatementList
    ;

classStaticBlockStatementList
    : statementList_Await?
    ;

// 15.8 Async Function Definitions

asyncFunctionDeclaration
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Yield
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Yield
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Await
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier?
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Yield_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Yield_Await
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Yield_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Yield?
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Await_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Await?
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionDeclaration_Yield_Await_Default
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Yield_Await?
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionExpression
    : AsyncKeyword /* TODO: no LineTerminator here */ FunctionKeyword bindingIdentifier_Await?
      LeftParenthesesLiteral formalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncMethod
    : AsyncKeyword /* TODO: no LineTerminator here */ classElementName
      LeftParenthesesLiteral uniqueFormalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncMethod_Yield
    : AsyncKeyword /* TODO: no LineTerminator here */ classElementName_Yield
      LeftParenthesesLiteral uniqueFormalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncMethod_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ classElementName_Await
      LeftParenthesesLiteral uniqueFormalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncMethod_Yield_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ classElementName_Yield_Await
      LeftParenthesesLiteral uniqueFormalParameters_Await RightParenthesesLiteral
      LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncFunctionBody
    : functionBody_Await
    ;

awaitExpression
    : AwaitKeyword unaryExpression_Await
    ;

awaitExpression_Yield
    : AwaitKeyword unaryExpression_Yield_Await
    ;

// 15.9 Async Arrow Function Definitions

asyncArrowFunction
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    | coverCallExpressionAndAsyncArrowHead /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    ;

asyncArrowFunction_In
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    | coverCallExpressionAndAsyncArrowHead /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    ;

asyncArrowFunction_Yield
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier_Yield /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    | coverCallExpressionAndAsyncArrowHead_Yield /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    ;

asyncArrowFunction_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    | coverCallExpressionAndAsyncArrowHead_Await /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    ;

asyncArrowFunction_In_Yield
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier_Yield /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    | coverCallExpressionAndAsyncArrowHead_Yield /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    ;

asyncArrowFunction_In_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    | coverCallExpressionAndAsyncArrowHead_Await /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    ;

asyncArrowFunction_Yield_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier_Yield /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    | coverCallExpressionAndAsyncArrowHead_Yield_Await /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody
    ;

asyncArrowFunction_In_Yield_Await
    : AsyncKeyword /* TODO: no LineTerminator here */ asyncArrowBindingIdentifier_Yield /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    | coverCallExpressionAndAsyncArrowHead_Yield_Await /* TODO: no LineTerminator here */
      ArrowPunctuator asyncConciseBody_In
    ;

asyncConciseBody
    : {!nextTextEq("{")}? expressionBody_Await
    | LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncConciseBody_In
    : {!nextTextEq("{")}? expressionBody_In_Await
    | LeftBracesLiteral asyncFunctionBody RightBracesLiteral
    ;

asyncArrowBindingIdentifier
    : bindingIdentifier_Await
    ;

asyncArrowBindingIdentifier_Yield
    : bindingIdentifier_Yield_Await
    ;

coverCallExpressionAndAsyncArrowHead
    : memberExpression arguments
    ;

coverCallExpressionAndAsyncArrowHead_Yield
    : memberExpression_Yield arguments_Yield
    ;

coverCallExpressionAndAsyncArrowHead_Await
    : memberExpression_Await arguments_Await
    ;

coverCallExpressionAndAsyncArrowHead_Yield_Await
    : memberExpression_Yield_Await arguments_Yield_Await
    ;

// 14 ECMAScript Language: Statements and Declarations

statement
    : blockStatement
    | variableStatement
    | emptyStatement
    | expressionStatement
    | ifStatement
    | breakableStatement
    | continueStatement
    | breakStatement
    // | returnStatement
    | withStatement
    | labelledStatement
    | throwStatement
    | tryStatement
    | debuggerStatement
    ;

statement_Yield
    : blockStatement_Yield
    | variableStatement_Yield
    | emptyStatement
    | expressionStatement_Yield
    | ifStatement_Yield
    | breakableStatement_Yield
    | continueStatement_Yield
    | breakStatement_Yield
    // | returnStatement_Yield
    | withStatement_Yield
    | labelledStatement_Yield
    | throwStatement_Yield
    | tryStatement_Yield
    | debuggerStatement
    ;

statement_Await
    : blockStatement_Await
    | variableStatement_Await
    | emptyStatement
    | expressionStatement_Await
    | ifStatement_Await
    | breakableStatement_Await
    | continueStatement_Await
    | breakStatement_Await
    // | returnStatement_Await
    | withStatement_Await
    | labelledStatement_Await
    | throwStatement_Await
    | tryStatement_Await
    | debuggerStatement
    ;

statement_Return
    : blockStatement_Return
    | variableStatement
    | emptyStatement
    | expressionStatement
    | ifStatement_Return
    | breakableStatement_Return
    | continueStatement
    | breakStatement
    | returnStatement
    | withStatement_Return
    | labelledStatement_Return
    | throwStatement
    | tryStatement_Return
    | debuggerStatement
    ;

statement_Yield_Await
    : blockStatement_Yield_Await
    | variableStatement_Yield_Await
    | emptyStatement
    | expressionStatement_Yield_Await
    | ifStatement_Yield_Await
    | breakableStatement_Yield_Await
    | continueStatement_Yield_Await
    | breakStatement_Yield_Await
    // | returnStatement_Yield_Await
    | withStatement_Yield_Await
    | labelledStatement_Yield_Await
    | throwStatement_Yield_Await
    | tryStatement_Yield_Await
    | debuggerStatement
    ;

statement_Yield_Return
    : blockStatement_Yield_Return
    | variableStatement_Yield
    | emptyStatement
    | expressionStatement_Yield
    | ifStatement_Yield_Return
    | breakableStatement_Yield_Return
    | continueStatement_Yield
    | breakStatement_Yield
    | returnStatement_Yield
    | withStatement_Yield_Return
    | labelledStatement_Yield_Return
    | throwStatement_Yield
    | tryStatement_Yield_Return
    | debuggerStatement
    ;

statement_Await_Return
    : blockStatement_Await_Return
    | variableStatement_Await
    | emptyStatement
    | expressionStatement_Await
    | ifStatement_Await_Return
    | breakableStatement_Await_Return
    | continueStatement_Await
    | breakStatement_Await
    | returnStatement_Await
    | withStatement_Await_Return
    | labelledStatement_Await_Return
    | throwStatement_Await
    | tryStatement_Await_Return
    | debuggerStatement
    ;

statement_Yield_Await_Return
    : blockStatement_Yield_Await_Return
    | variableStatement_Yield_Await
    | emptyStatement
    | expressionStatement_Yield_Await
    | ifStatement_Yield_Await_Return
    | breakableStatement_Yield_Await_Return
    | continueStatement_Yield_Await
    | breakStatement_Yield_Await
    | returnStatement_Yield_Await
    | withStatement_Yield_Await_Return
    | labelledStatement_Yield_Await_Return
    | throwStatement_Yield_Await
    | tryStatement_Yield_Await_Return
    | debuggerStatement
    ;

declaration
    : hoistableDeclaration
    | classDeclaration
    | lexicalDeclaration_In
    ;

declaration_Yield
    : hoistableDeclaration_Yield
    | classDeclaration_Yield
    | lexicalDeclaration_In_Yield
    ;

declaration_Await
    : hoistableDeclaration_Await
    | classDeclaration_Await
    | lexicalDeclaration_In_Await
    ;

declaration_Yield_Await
    : hoistableDeclaration_Yield_Await
    | classDeclaration_Yield_Await
    | lexicalDeclaration_In_Yield_Await
    ;

hoistableDeclaration
    : functionDeclaration
    | generatorDeclaration
    | asyncFunctionDeclaration
    | asyncGeneratorDeclaration
    ;

hoistableDeclaration_Yield
    : functionDeclaration_Yield
    | generatorDeclaration_Yield
    | asyncFunctionDeclaration_Yield
    | asyncGeneratorDeclaration_Yield
    ;

hoistableDeclaration_Await
    : functionDeclaration_Await
    | generatorDeclaration_Await
    | asyncFunctionDeclaration_Await
    | asyncGeneratorDeclaration_Await
    ;

hoistableDeclaration_Default
    : functionDeclaration_Default
    | generatorDeclaration_Default
    | asyncFunctionDeclaration_Default
    | asyncGeneratorDeclaration_Default
    ;

hoistableDeclaration_Yield_Await
    : functionDeclaration_Yield_Await
    | generatorDeclaration_Yield_Await
    | asyncFunctionDeclaration_Yield_Await
    | asyncGeneratorDeclaration_Yield_Await
    ;

hoistableDeclaration_Yield_Default
    : functionDeclaration_Yield_Default
    | generatorDeclaration_Yield_Default
    | asyncFunctionDeclaration_Yield_Default
    | asyncGeneratorDeclaration_Yield_Default
    ;

hoistableDeclaration_Await_Default
    : functionDeclaration_Await_Default
    | generatorDeclaration_Await_Default
    | asyncFunctionDeclaration_Await_Default
    | asyncGeneratorDeclaration_Await_Default
    ;

hoistableDeclaration_Yield_Await_Default
    : functionDeclaration_Yield_Await_Default
    | generatorDeclaration_Yield_Await_Default
    | asyncFunctionDeclaration_Yield_Await_Default
    | asyncGeneratorDeclaration_Yield_Await_Default
    ;

breakableStatement
    : iterationStatement
    | switchStatement
    ;

breakableStatement_Yield
    : iterationStatement_Yield
    | switchStatement_Yield
    ;

breakableStatement_Await
    : iterationStatement_Await
    | switchStatement_Await
    ;

breakableStatement_Return
    : iterationStatement_Return
    | switchStatement_Return
    ;

breakableStatement_Yield_Await
    : iterationStatement_Yield_Await
    | switchStatement_Yield_Await
    ;

breakableStatement_Yield_Return
    : iterationStatement_Yield_Return
    | switchStatement_Yield_Return
    ;

breakableStatement_Await_Return
    : iterationStatement_Await_Return
    | switchStatement_Await_Return
    ;

breakableStatement_Yield_Await_Return
    : iterationStatement_Yield_Await_Return
    | switchStatement_Yield_Await_Return
    ;

// 14.2 Block

blockStatement
    : block
    ;

blockStatement_Yield
    : block_Yield
    ;

blockStatement_Await
    : block_Await
    ;

blockStatement_Return
    : block_Return
    ;

blockStatement_Yield_Await
    : block_Yield_Await
    ;

blockStatement_Yield_Return
    : block_Yield_Return
    ;

blockStatement_Await_Return
    : block_Await_Return
    ;

blockStatement_Yield_Await_Return
    : block_Yield_Await_Return
    ;

block
    : LeftBracesLiteral statementList? RightBracesLiteral
    ;

block_Yield
    : LeftBracesLiteral statementList_Yield? RightBracesLiteral
    ;

block_Await
    : LeftBracesLiteral statementList_Await? RightBracesLiteral
    ;

block_Return
    : LeftBracesLiteral statementList_Return? RightBracesLiteral
    ;

block_Yield_Await
    : LeftBracesLiteral statementList_Yield_Await? RightBracesLiteral
    ;

block_Yield_Return
    : LeftBracesLiteral statementList_Yield_Return? RightBracesLiteral
    ;

block_Await_Return
    : LeftBracesLiteral statementList_Await_Return? RightBracesLiteral
    ;

block_Yield_Await_Return
    : LeftBracesLiteral statementList_Yield_Await_Return? RightBracesLiteral
    ;

statementList
    : statementListItem+
    ;

statementList_Yield
    : statementListItem_Yield+
    ;

statementList_Await
    : statementListItem_Await+
    ;

statementList_Return
    : statementListItem_Return+
    ;

statementList_Yield_Await
    : statementListItem_Yield_Await+
    ;

statementList_Yield_Return
    : statementListItem_Yield_Return+
    ;

statementList_Await_Return
    : statementListItem_Await_Return+
    ;

statementList_Yield_Await_Return
    : statementListItem_Yield_Await_Return+
    ;

statementListItem
    : statement
    | declaration
    ;

statementListItem_Yield
    : statement_Yield
    | declaration_Yield
    ;

statementListItem_Await
    : statement_Await
    | declaration_Await
    ;

statementListItem_Return
    : statement_Return
    | declaration
    ;

statementListItem_Yield_Await
    : statement_Yield_Await
    | declaration_Yield_Await
    ;

statementListItem_Yield_Return
    : statement_Yield_Return
    | declaration_Yield
    ;

statementListItem_Await_Return
    : statement_Await_Return
    | declaration_Await
    ;

statementListItem_Yield_Await_Return
    : statement_Yield_Await_Return
    | declaration_Yield_Await
    ;

// 14.3 Declarations and the Variable Statement

// 14.3.1 Let and Const Declarations

lexicalDeclaration
    : letOrConst bindingList
    ;

lexicalDeclaration_In
    : letOrConst bindingList_In
    ;

lexicalDeclaration_Yield
    : letOrConst bindingList_Yield
    ;

lexicalDeclaration_Await
    : letOrConst bindingList_Await
    ;

lexicalDeclaration_In_Yield
    : letOrConst bindingList_In_Yield
    ;

lexicalDeclaration_In_Await
    : letOrConst bindingList_In_Await
    ;

lexicalDeclaration_Yield_Await
    : letOrConst bindingList_Yield_Await
    ;

lexicalDeclaration_In_Yield_Await
    : letOrConst bindingList_In_Yield_Await
    ;

letOrConst
    : LetKeyword
    | ConstKeyword
    ;

bindingList
    : lexicalBinding (CommaPunctuator lexicalBinding)*
    ;

bindingList_In
    : lexicalBinding_In (CommaPunctuator lexicalBinding_In)*
    ;

bindingList_Yield
    : lexicalBinding_Yield (CommaPunctuator lexicalBinding_Yield)*
    ;

bindingList_Await
    : lexicalBinding_Await (CommaPunctuator lexicalBinding_Await)*
    ;

bindingList_In_Yield
    : lexicalBinding_In_Yield (CommaPunctuator lexicalBinding_In_Yield)*
    ;

bindingList_In_Await
    : lexicalBinding_In_Await (CommaPunctuator lexicalBinding_In_Await)*
    ;

bindingList_Yield_Await
    : lexicalBinding_Yield_Await (CommaPunctuator lexicalBinding_Yield_Await)*
    ;

bindingList_In_Yield_Await
    : lexicalBinding_In_Yield_Await (CommaPunctuator lexicalBinding_In_Yield_Await)*
    ;

lexicalBinding
    : bindingIdentifier initializer?
    | bindingPattern initializer
    ;

lexicalBinding_In
    : bindingIdentifier initializer_In?
    | bindingPattern initializer_In
    ;

lexicalBinding_Yield
    : bindingIdentifier_Yield initializer_Yield?
    | bindingPattern_Yield initializer_Yield
    ;

lexicalBinding_Await
    : bindingIdentifier_Await initializer_Await?
    | bindingPattern_Await initializer_Await
    ;

lexicalBinding_In_Yield
    : bindingIdentifier_Yield initializer_In_Yield?
    | bindingPattern_Yield initializer_In_Yield
    ;

lexicalBinding_In_Await
    : bindingIdentifier_Await initializer_In_Await?
    | bindingPattern_Await initializer_In_Await
    ;

lexicalBinding_Yield_Await
    : bindingIdentifier_Yield_Await initializer_Yield_Await?
    | bindingPattern_Yield_Await initializer_Yield_Await
    ;

lexicalBinding_In_Yield_Await
    : bindingIdentifier_Yield_Await initializer_In_Yield_Await?
    | bindingPattern_Yield_Await initializer_In_Yield_Await
    ;

// 14.3.2 Variable Statement

variableStatement
    : VarKeyword variableDeclarationList_In
    ;

variableStatement_Yield
    : VarKeyword variableDeclarationList_In_Yield
    ;

variableStatement_Await
    : VarKeyword variableDeclarationList_In_Await
    ;

variableStatement_Yield_Await
    : VarKeyword variableDeclarationList_In_Yield_Await
    ;

variableDeclarationList
    : variableDeclaration (CommaPunctuator variableDeclaration)*
    ;

variableDeclarationList_In
    : variableDeclaration_In (CommaPunctuator variableDeclaration_In)*
    ;

variableDeclarationList_Yield
    : variableDeclaration_Yield (CommaPunctuator variableDeclaration_Yield)*
    ;

variableDeclarationList_Await
    : variableDeclaration_Await (CommaPunctuator variableDeclaration_Await)*
    ;

variableDeclarationList_In_Yield
    : variableDeclaration_In_Yield (CommaPunctuator variableDeclaration_In_Yield)*
    ;

variableDeclarationList_In_Await
    : variableDeclaration_In_Await (CommaPunctuator variableDeclaration_In_Await)*
    ;

variableDeclarationList_Yield_Await
    : variableDeclaration_Yield_Await (CommaPunctuator variableDeclaration_Yield_Await)*
    ;

variableDeclarationList_In_Yield_Await
    : variableDeclaration_In_Yield_Await (CommaPunctuator variableDeclaration_In_Yield_Await)*
    ;

variableDeclaration
    : bindingIdentifier initializer?
    | bindingPattern initializer
    ;

variableDeclaration_In
    : bindingIdentifier initializer_In?
    | bindingPattern initializer_In
    ;

variableDeclaration_Yield
    : bindingIdentifier_Yield initializer_Yield?
    | bindingPattern_Yield initializer_Yield
    ;

variableDeclaration_Await
    : bindingIdentifier_Await initializer_Await?
    | bindingPattern_Await initializer_Await
    ;

variableDeclaration_In_Yield
    : bindingIdentifier_Yield initializer_In_Yield?
    | bindingPattern_Yield initializer_In_Yield
    ;

variableDeclaration_In_Await
    : bindingIdentifier_Await initializer_In_Await?
    | bindingPattern_Await initializer_In_Await
    ;

variableDeclaration_Yield_Await
    : bindingIdentifier_Yield_Await initializer_Yield_Await?
    | bindingPattern_Yield_Await initializer_Yield_Await
    ;

variableDeclaration_In_Yield_Await
    : bindingIdentifier_Yield_Await initializer_In_Yield_Await?
    | bindingPattern_Yield_Await initializer_In_Yield_Await
    ;

// 14.3.3 Destructuring Binding Patterns

bindingPattern
    : objectBindingPattern
    | arrayBindingPattern
    ;

bindingPattern_Yield
    : objectBindingPattern_Yield
    | arrayBindingPattern_Yield
    ;

bindingPattern_Await
    : objectBindingPattern_Await
    | arrayBindingPattern_Await
    ;

bindingPattern_Yield_Await
    : objectBindingPattern_Yield_Await
    | arrayBindingPattern_Yield_Await
    ;

objectBindingPattern
    : LeftBracesLiteral RightBracesLiteral
    | LeftBracesLiteral bindingRestProperty RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList CommaPunctuator bindingRestProperty? RightBracesLiteral
    ;

objectBindingPattern_Yield
    : LeftBracesLiteral RightBracesLiteral
    | LeftBracesLiteral bindingRestProperty_Yield RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList_Yield RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList_Yield CommaPunctuator bindingRestProperty_Yield? RightBracesLiteral
    ;

objectBindingPattern_Await
    : LeftBracesLiteral RightBracesLiteral
    | LeftBracesLiteral bindingRestProperty_Await RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList_Await RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList_Await CommaPunctuator bindingRestProperty_Await? RightBracesLiteral
    ;

objectBindingPattern_Yield_Await
    : LeftBracesLiteral RightBracesLiteral
    | LeftBracesLiteral bindingRestProperty_Yield_Await RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList_Yield_Await RightBracesLiteral
    | LeftBracesLiteral bindingPropertyList_Yield_Await CommaPunctuator bindingRestProperty_Yield_Await? RightBracesLiteral
    ;

arrayBindingPattern
    : LeftBracketsLiteral elision? bindingRestElement? RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList CommaPunctuator elision? bindingRestElement? RightBracketsLiteral
    ;

arrayBindingPattern_Yield
    : LeftBracketsLiteral elision? bindingRestElement_Yield? RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList_Yield RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList_Yield CommaPunctuator elision? bindingRestElement_Yield? RightBracketsLiteral
    ;

arrayBindingPattern_Await
    : LeftBracketsLiteral elision? bindingRestElement_Await? RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList_Await RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList_Await CommaPunctuator elision? bindingRestElement_Await? RightBracketsLiteral
    ;

arrayBindingPattern_Yield_Await
    : LeftBracketsLiteral elision? bindingRestElement_Yield_Await? RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList_Yield_Await RightBracketsLiteral
    | LeftBracketsLiteral bindingElementList_Yield_Await CommaPunctuator elision? bindingRestElement_Yield_Await? RightBracketsLiteral
    ;

bindingRestProperty
    : ThreeDotsPunctuator bindingIdentifier
    ;

bindingRestProperty_Yield
    : ThreeDotsPunctuator bindingIdentifier_Yield
    ;

bindingRestProperty_Await
    : ThreeDotsPunctuator bindingIdentifier_Await
    ;

bindingRestProperty_Yield_Await
    : ThreeDotsPunctuator bindingIdentifier_Yield_Await
    ;

bindingPropertyList
    : bindingProperty (CommaPunctuator bindingProperty)*
    ;

bindingPropertyList_Yield
    : bindingProperty_Yield (CommaPunctuator bindingProperty_Yield)*
    ;

bindingPropertyList_Await
    : bindingProperty_Await (CommaPunctuator bindingProperty_Await)*
    ;

bindingPropertyList_Yield_Await
    : bindingProperty_Yield_Await (CommaPunctuator bindingProperty_Yield_Await)*
    ;

bindingElementList
    : bindingElisionElement (CommaPunctuator bindingElisionElement)*
    ;

bindingElementList_Yield
    : bindingElisionElement_Yield (CommaPunctuator bindingElisionElement_Yield)*
    ;

bindingElementList_Await
    : bindingElisionElement_Await (CommaPunctuator bindingElisionElement_Await)*
    ;

bindingElementList_Yield_Await
    : bindingElisionElement_Yield_Await (CommaPunctuator bindingElisionElement_Yield_Await)*
    ;

bindingElisionElement
    : elision? bindingElement
    ;

bindingElisionElement_Yield
    : elision? bindingElement_Yield
    ;

bindingElisionElement_Await
    : elision? bindingElement_Await
    ;

bindingElisionElement_Yield_Await
    : elision? bindingElement_Yield_Await
    ;

bindingProperty
    : singleNameBinding
    | propertyName ConditionalTailPunctuator bindingElement
    ;

bindingProperty_Yield
    : singleNameBinding_Yield
    | propertyName_Yield ConditionalTailPunctuator bindingElement_Yield
    ;

bindingProperty_Await
    : singleNameBinding_Await
    | propertyName_Await ConditionalTailPunctuator bindingElement_Await
    ;

bindingProperty_Yield_Await
    : singleNameBinding_Yield_Await
    | propertyName_Yield_Await ConditionalTailPunctuator bindingElement_Yield_Await
    ;

bindingElement
    : singleNameBinding
    | bindingPattern initializer_In?
    ;

bindingElement_Yield
    : singleNameBinding_Yield
    | bindingPattern_Yield initializer_In_Yield?
    ;

bindingElement_Await
    : singleNameBinding_Await
    | bindingPattern_Await initializer_In_Await?
    ;

bindingElement_Yield_Await
    : singleNameBinding_Yield_Await
    | bindingPattern_Yield_Await initializer_In_Yield_Await?
    ;

singleNameBinding
    : bindingIdentifier initializer_In?
    ;

singleNameBinding_Yield
    : bindingIdentifier_Yield initializer_In_Yield?
    ;

singleNameBinding_Await
    : bindingIdentifier_Await initializer_In_Await?
    ;

singleNameBinding_Yield_Await
    : bindingIdentifier_Yield_Await initializer_In_Yield_Await?
    ;

bindingRestElement
    : ThreeDotsPunctuator bindingIdentifier
    | ThreeDotsPunctuator bindingPattern
    ;

bindingRestElement_Yield
    : ThreeDotsPunctuator bindingIdentifier_Yield
    | ThreeDotsPunctuator bindingPattern_Yield
    ;

bindingRestElement_Await
    : ThreeDotsPunctuator bindingIdentifier_Await
    | ThreeDotsPunctuator bindingPattern_Await
    ;

bindingRestElement_Yield_Await
    : ThreeDotsPunctuator bindingIdentifier_Yield_Await
    | ThreeDotsPunctuator bindingPattern_Yield_Await
    ;

// 14.4 Empty Statement

emptyStatement
    : SemicolonPunctuator
    ;

// 14.5 Expression Statement

expressionStatement
    : {isValidExpressionStatement()}? expression_In
    ;

expressionStatement_Yield
    : {isValidExpressionStatement()}? expression_In_Yield
    ;

expressionStatement_Await
    : {isValidExpressionStatement()}? expression_In_Await
    ;

expressionStatement_Yield_Await
    : {isValidExpressionStatement()}? expression_In_Yield_Await
    ;

// 14.6 The if Statement

ifStatement
    : IfKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement ElseKeyword statement
    | IfKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement {getNextTokenType() != InKeyword}?
    ;

ifStatement_Yield
    : IfKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield ElseKeyword statement_Yield
    | IfKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield {getNextTokenType() != InKeyword}?
    ;

ifStatement_Await
    : IfKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await ElseKeyword statement_Await
    | IfKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await {getNextTokenType() != InKeyword}?
    ;

ifStatement_Return
    : IfKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement_Return ElseKeyword statement_Return
    | IfKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement_Return {getNextTokenType() != InKeyword}?
    ;

ifStatement_Yield_Await
    : IfKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await ElseKeyword statement_Yield_Await
    | IfKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await {getNextTokenType() != InKeyword}?
    ;

ifStatement_Yield_Return
    : IfKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield_Return ElseKeyword statement_Yield_Return
    | IfKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield_Return {getNextTokenType() != InKeyword}?
    ;

ifStatement_Await_Return
    : IfKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await_Return ElseKeyword statement_Await_Return
    | IfKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await_Return {getNextTokenType() != InKeyword}?
    ;

ifStatement_Yield_Await_Return
    : IfKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return ElseKeyword statement_Yield_Await_Return
    | IfKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return {getNextTokenType() != InKeyword}?
    ;

// 14.7 Iteration Statements

iterationStatement
    : doWhileStatement
    | whileStatement
    | forStatement
    | forInOfStatement
    ;

iterationStatement_Yield
    : doWhileStatement_Yield
    | whileStatement_Yield
    | forStatement_Yield
    | forInOfStatement_Yield
    ;

iterationStatement_Await
    : doWhileStatement_Await
    | whileStatement_Await
    | forStatement_Await
    | forInOfStatement_Await
    ;

iterationStatement_Return
    : doWhileStatement_Return
    | whileStatement_Return
    | forStatement_Return
    | forInOfStatement_Return
    ;

iterationStatement_Yield_Await
    : doWhileStatement_Yield_Await
    | whileStatement_Yield_Await
    | forStatement_Yield_Await
    | forInOfStatement_Yield_Await
    ;

iterationStatement_Yield_Return
    : doWhileStatement_Yield_Return
    | whileStatement_Yield_Return
    | forStatement_Yield_Return
    | forInOfStatement_Yield_Return
    ;

iterationStatement_Await_Return
    : doWhileStatement_Await_Return
    | whileStatement_Await_Return
    | forStatement_Await_Return
    | forInOfStatement_Await_Return
    ;

iterationStatement_Yield_Await_Return
    : doWhileStatement_Yield_Await_Return
    | whileStatement_Yield_Await_Return
    | forStatement_Yield_Await_Return
    | forInOfStatement_Yield_Await_Return
    ;

// 14.7.2 The do-while Statement

doWhileStatement
    : DoKeyword statement WhileKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Yield
    : DoKeyword statement_Yield WhileKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Await
    : DoKeyword statement_Await WhileKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Return
    : DoKeyword statement_Return WhileKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Yield_Await
    : DoKeyword statement_Yield_Await WhileKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Yield_Return
    : DoKeyword statement_Yield_Return WhileKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Await_Return
    : DoKeyword statement_Await_Return WhileKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral SemicolonPunctuator
    ;

doWhileStatement_Yield_Await_Return
    : DoKeyword statement_Yield_Await_Return WhileKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral SemicolonPunctuator
    ;

// 14.7.3 The while Statement

whileStatement
    : WhileKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement
    ;

whileStatement_Yield
    : WhileKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield
    ;

whileStatement_Await
    : WhileKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await
    ;

whileStatement_Return
    : WhileKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement_Return
    ;

whileStatement_Yield_Await
    : WhileKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    ;

whileStatement_Yield_Return
    : WhileKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield_Return
    ;

whileStatement_Await_Return
    : WhileKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await_Return
    ;

whileStatement_Yield_Await_Return
    : WhileKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    ;

// 14.7.4 The for Statement

forStatement
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression? SemicolonPunctuator
      expression_In? SemicolonPunctuator expression_In? RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList SemicolonPunctuator
      expression_In? SemicolonPunctuator expression_In? RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration
      expression_In? SemicolonPunctuator expression_In? RightParenthesesLiteral statement
    ;

forStatement_Yield
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression_Yield? SemicolonPunctuator
      expression_In_Yield? SemicolonPunctuator expression_In_Yield? RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList_Yield SemicolonPunctuator
      expression_In_Yield? SemicolonPunctuator expression_In_Yield? RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration_Yield
      expression_In_Yield? SemicolonPunctuator expression_In_Yield? RightParenthesesLiteral statement_Yield
    ;

forStatement_Await
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression_Await? SemicolonPunctuator
      expression_In_Await? SemicolonPunctuator expression_In_Await? RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList_Await SemicolonPunctuator
      expression_In_Await? SemicolonPunctuator expression_In_Await? RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration_Await
      expression_In_Await? SemicolonPunctuator expression_In_Await? RightParenthesesLiteral statement_Await
    ;

forStatement_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression? SemicolonPunctuator
      expression_In? SemicolonPunctuator expression_In? RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList SemicolonPunctuator
      expression_In? SemicolonPunctuator expression_In? RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration
      expression_In? SemicolonPunctuator expression_In? RightParenthesesLiteral statement_Return
    ;

forStatement_Yield_Await
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression_Yield_Await? SemicolonPunctuator
      expression_In_Yield_Await? SemicolonPunctuator expression_In_Yield_Await? RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList_Yield_Await SemicolonPunctuator
      expression_In_Yield_Await? SemicolonPunctuator expression_In_Yield_Await? RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration_Yield_Await
      expression_In_Yield_Await? SemicolonPunctuator expression_In_Yield_Await? RightParenthesesLiteral statement_Yield_Await
    ;

forStatement_Yield_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression_Yield? SemicolonPunctuator
      expression_In_Yield? SemicolonPunctuator expression_In_Yield? RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList_Yield SemicolonPunctuator
      expression_In_Yield? SemicolonPunctuator expression_In_Yield? RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration_Yield
      expression_In_Yield? SemicolonPunctuator expression_In_Yield? RightParenthesesLiteral statement_Yield_Return
    ;

forStatement_Await_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression_Await? SemicolonPunctuator
      expression_In_Await? SemicolonPunctuator expression_In_Await? RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList_Await SemicolonPunctuator
      expression_In_Await? SemicolonPunctuator expression_In_Await? RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration_Await
      expression_In_Await? SemicolonPunctuator expression_In_Await? RightParenthesesLiteral statement_Await_Return
    ;

forStatement_Yield_Await_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ expression_Yield_Await? SemicolonPunctuator
      expression_In_Yield_Await? SemicolonPunctuator expression_In_Yield_Await? RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword variableDeclarationList_Yield_Await SemicolonPunctuator
      expression_In_Yield_Await? SemicolonPunctuator expression_In_Yield_Await? RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral lexicalDeclaration_Yield_Await
      expression_In_Yield_Await? SemicolonPunctuator expression_In_Yield_Await? RightParenthesesLiteral statement_Yield_Await_Return
    ;

// 14.7.5 The for-in, for-of, and for-await-of Statements

forInOfStatement
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression InKeyword
      expression_In RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding InKeyword
      expression_In RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier initializer InKeyword
      expression_In RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral forDeclaration InKeyword
      expression_In RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression OfKeyword
      assignmentExpression_In RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding OfKeyword
      assignmentExpression_In RightParenthesesLiteral statement
    | ForKeyword LeftParenthesesLiteral forDeclaration OfKeyword
      assignmentExpression_In RightParenthesesLiteral statement
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression OfKeyword
    //  assignmentExpression_In RightParenthesesLiteral statement
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding OfKeyword
    //  assignmentExpression_In RightParenthesesLiteral statement
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration OfKeyword
    //  assignmentExpression_In RightParenthesesLiteral statement
    ;

forInOfStatement_Yield
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier_Yield initializer_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression_Yield OfKeyword
      assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield OfKeyword
      assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield OfKeyword
      assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression_Yield OfKeyword
    //  assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield OfKeyword
    //  assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration_Yield OfKeyword
    //  assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield
    ;

forInOfStatement_Await
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier_Await initializer_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral forDeclaration_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword LeftParenthesesLiteral forDeclaration_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await
    | ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await
    ;

forInOfStatement_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression InKeyword
      expression_In RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding InKeyword
      expression_In RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier initializer InKeyword
      expression_In RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration InKeyword
      expression_In RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression OfKeyword
      assignmentExpression_In RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding OfKeyword
      assignmentExpression_In RightParenthesesLiteral statement_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration OfKeyword
      assignmentExpression_In RightParenthesesLiteral statement_Return
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression OfKeyword
    //  assignmentExpression_In RightParenthesesLiteral statement_Return
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding OfKeyword
    //  assignmentExpression_In RightParenthesesLiteral statement_Return
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration OfKeyword
    //  assignmentExpression_In RightParenthesesLiteral statement_Return
    ;

forInOfStatement_Yield_Await
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier_Yield_Await initializer_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    | ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    ;

forInOfStatement_Yield_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier_Yield initializer_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield InKeyword
      expression_In_Yield RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression_Yield OfKeyword
      assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield OfKeyword
      assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield OfKeyword
      assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield_Return
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression_Yield OfKeyword
    //  assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield_Return
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield OfKeyword
    //  assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield_Return
    //| ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration_Yield OfKeyword
    //  assignmentExpression_In_Yield RightParenthesesLiteral statement_Yield_Return
    ;

forInOfStatement_Await_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier_Await initializer_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration_Await InKeyword
      expression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await_Return
    | ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration_Await OfKeyword
      assignmentExpression_In_Await RightParenthesesLiteral statement_Await_Return
    ;

forInOfStatement_Yield_Await_Return
    : ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let [ */ leftHandSideExpression_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword bindingIdentifier_Yield_Await initializer_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield_Await InKeyword
      expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral /* TODO: lookahead != let, async of */ leftHandSideExpression_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword LeftParenthesesLiteral forDeclaration_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword AwaitKeyword LeftParenthesesLiteral /* TODO: lookahead != let */ leftHandSideExpression_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword AwaitKeyword LeftParenthesesLiteral VarKeyword forBinding_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    | ForKeyword AwaitKeyword LeftParenthesesLiteral forDeclaration_Yield_Await OfKeyword
      assignmentExpression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    ;

forDeclaration
    : letOrConst forBinding
    ;

forDeclaration_Yield
    : letOrConst forBinding_Yield
    ;

forDeclaration_Await
    : letOrConst forBinding_Await
    ;

forDeclaration_Yield_Await
    : letOrConst forBinding_Yield_Await
    ;

forBinding
    : bindingIdentifier
    | bindingPattern
    ;

forBinding_Yield
    : bindingIdentifier_Yield
    | bindingPattern_Yield
    ;

forBinding_Await
    : bindingIdentifier_Await
    | bindingPattern_Await
    ;

forBinding_Yield_Await
    : bindingIdentifier_Yield_Await
    | bindingPattern_Yield_Await
    ;

// 14.8 The continue Statement

continueStatement
    : ContinueKeyword SemicolonPunctuator
    | ContinueKeyword /* TODO: no LineTerminator here */ labelIdentifier SemicolonPunctuator
    ;

continueStatement_Yield
    : ContinueKeyword SemicolonPunctuator
    | ContinueKeyword /* TODO: no LineTerminator here */ labelIdentifier_Yield SemicolonPunctuator
    ;

continueStatement_Await
    : ContinueKeyword SemicolonPunctuator
    | ContinueKeyword /* TODO: no LineTerminator here */ labelIdentifier_Await SemicolonPunctuator
    ;

continueStatement_Yield_Await
    : ContinueKeyword SemicolonPunctuator
    | ContinueKeyword /* TODO: no LineTerminator here */ labelIdentifier_Yield_Await SemicolonPunctuator
    ;

// 14.9 The break Statement

breakStatement
    : BreakKeyword SemicolonPunctuator
    | BreakKeyword /* TODO: no LineTerminator here */ labelIdentifier SemicolonPunctuator
    ;

breakStatement_Yield
    : BreakKeyword SemicolonPunctuator
    | BreakKeyword /* TODO: no LineTerminator here */ labelIdentifier_Yield SemicolonPunctuator
    ;

breakStatement_Await
    : BreakKeyword SemicolonPunctuator
    | BreakKeyword /* TODO: no LineTerminator here */ labelIdentifier_Await SemicolonPunctuator
    ;

breakStatement_Yield_Await
    : BreakKeyword SemicolonPunctuator
    | BreakKeyword /* TODO: no LineTerminator here */ labelIdentifier_Yield_Await SemicolonPunctuator
    ;

// 14.10 The return Statement

returnStatement
    : ReturnKeyword SemicolonPunctuator
    | ReturnKeyword /* TODO: no LineTerminator here */ expression_In SemicolonPunctuator
    ;

returnStatement_Yield
    : ReturnKeyword SemicolonPunctuator
    | ReturnKeyword /* TODO: no LineTerminator here */ expression_In_Yield SemicolonPunctuator
    ;

returnStatement_Await
    : ReturnKeyword SemicolonPunctuator
    | ReturnKeyword /* TODO: no LineTerminator here */ expression_In_Await SemicolonPunctuator
    ;

returnStatement_Yield_Await
    : ReturnKeyword SemicolonPunctuator
    | ReturnKeyword /* TODO: no LineTerminator here */ expression_In_Yield_Await SemicolonPunctuator
    ;

// 14.11 The with Statement

withStatement
    : WithKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement
    ;

withStatement_Yield
    : WithKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield
    ;

withStatement_Await
    : WithKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await
    ;

withStatement_Return
    : WithKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral statement_Return
    ;

withStatement_Yield_Await
    : WithKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await
    ;

withStatement_Yield_Return
    : WithKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral statement_Yield_Return
    ;

withStatement_Await_Return
    : WithKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral statement_Await_Return
    ;

withStatement_Yield_Await_Return
    : WithKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral statement_Yield_Await_Return
    ;

// 14.12 The switch Statement

switchStatement
    : SwitchKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral caseBlock
    ;

switchStatement_Yield
    : SwitchKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral caseBlock_Yield
    ;

switchStatement_Await
    : SwitchKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral caseBlock_Await
    ;

switchStatement_Return
    : SwitchKeyword LeftParenthesesLiteral expression_In RightParenthesesLiteral caseBlock_Return
    ;

switchStatement_Yield_Await
    : SwitchKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral caseBlock_Yield_Await
    ;

switchStatement_Yield_Return
    : SwitchKeyword LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral caseBlock_Yield_Return
    ;

switchStatement_Await_Return
    : SwitchKeyword LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral caseBlock_Await_Return
    ;

switchStatement_Yield_Await_Return
    : SwitchKeyword LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral caseBlock_Yield_Await_Return
    ;

caseBlock
    : LeftBracesLiteral caseClauses? (defaultClause caseClauses?)? RightBracesLiteral
    ;

caseBlock_Yield
    : LeftBracesLiteral caseClauses_Yield? (defaultClause_Yield caseClauses_Yield?)? RightBracesLiteral
    ;

caseBlock_Await
    : LeftBracesLiteral caseClauses_Await? (defaultClause_Await caseClauses_Await?)? RightBracesLiteral
    ;

caseBlock_Return
    : LeftBracesLiteral caseClauses_Return? (defaultClause_Return caseClauses_Return?)? RightBracesLiteral
    ;

caseBlock_Yield_Await
    : LeftBracesLiteral caseClauses_Yield_Await? (defaultClause_Yield_Await caseClauses_Yield_Await?)? RightBracesLiteral
    ;

caseBlock_Yield_Return
    : LeftBracesLiteral caseClauses_Yield_Return? (defaultClause_Yield_Return caseClauses_Yield_Return?)? RightBracesLiteral
    ;

caseBlock_Await_Return
    : LeftBracesLiteral caseClauses_Await_Return? (defaultClause_Await_Return caseClauses_Await_Return?)? RightBracesLiteral
    ;

caseBlock_Yield_Await_Return
    : LeftBracesLiteral caseClauses_Yield_Await_Return? (defaultClause_Yield_Await_Return caseClauses_Yield_Await_Return?)? RightBracesLiteral
    ;

caseClauses
    : caseClause+
    ;

caseClauses_Yield
    : caseClause_Yield+
    ;

caseClauses_Await
    : caseClause_Await+
    ;

caseClauses_Return
    : caseClause_Return+
    ;

caseClauses_Yield_Await
    : caseClause_Yield_Await+
    ;

caseClauses_Yield_Return
    : caseClause_Yield_Return+
    ;

caseClauses_Await_Return
    : caseClause_Await_Return+
    ;

caseClauses_Yield_Await_Return
    : caseClause_Yield_Await_Return+
    ;

caseClause
    : CaseKeyword expression_In ConditionalTailPunctuator statementList?
    ;

caseClause_Yield
    : CaseKeyword expression_In_Yield ConditionalTailPunctuator statementList_Yield?
    ;

caseClause_Await
    : CaseKeyword expression_In_Await ConditionalTailPunctuator statementList_Await?
    ;

caseClause_Return
    : CaseKeyword expression_In ConditionalTailPunctuator statementList_Return?
    ;

caseClause_Yield_Await
    : CaseKeyword expression_In_Yield_Await ConditionalTailPunctuator statementList_Yield_Await?
    ;

caseClause_Yield_Return
    : CaseKeyword expression_In_Yield ConditionalTailPunctuator statementList_Yield_Return?
    ;

caseClause_Await_Return
    : CaseKeyword expression_In_Await ConditionalTailPunctuator statementList_Await_Return?
    ;

caseClause_Yield_Await_Return
    : CaseKeyword expression_In_Yield_Await ConditionalTailPunctuator statementList_Yield_Await_Return?
    ;

defaultClause
    : DefaultKeyword ConditionalTailPunctuator statementList?
    ;

defaultClause_Yield
    : DefaultKeyword ConditionalTailPunctuator statementList_Yield?
    ;

defaultClause_Await
    : DefaultKeyword ConditionalTailPunctuator statementList_Await?
    ;

defaultClause_Return
    : DefaultKeyword ConditionalTailPunctuator statementList_Return?
    ;

defaultClause_Yield_Await
    : DefaultKeyword ConditionalTailPunctuator statementList_Yield_Await?
    ;

defaultClause_Yield_Return
    : DefaultKeyword ConditionalTailPunctuator statementList_Yield_Return?
    ;

defaultClause_Await_Return
    : DefaultKeyword ConditionalTailPunctuator statementList_Await_Return?
    ;

defaultClause_Yield_Await_Return
    : DefaultKeyword ConditionalTailPunctuator statementList_Yield_Await_Return?
    ;

// 14.13 Labelled Statements

labelledStatement
    : labelIdentifier ConditionalTailPunctuator labelledItem
    ;

labelledStatement_Yield
    : labelIdentifier_Yield ConditionalTailPunctuator labelledItem_Yield
    ;

labelledStatement_Await
    : labelIdentifier_Await ConditionalTailPunctuator labelledItem_Await
    ;

labelledStatement_Return
    : labelIdentifier ConditionalTailPunctuator labelledItem_Return
    ;

labelledStatement_Yield_Await
    : labelIdentifier_Yield_Await ConditionalTailPunctuator labelledItem_Yield_Await
    ;

labelledStatement_Yield_Return
    : labelIdentifier_Yield ConditionalTailPunctuator labelledItem_Yield_Return
    ;

labelledStatement_Await_Return
    : labelIdentifier_Await ConditionalTailPunctuator labelledItem_Await_Return
    ;

labelledStatement_Yield_Await_Return
    : labelIdentifier_Yield_Await ConditionalTailPunctuator labelledItem_Yield_Await_Return
    ;

labelledItem
    : statement
    | functionDeclaration
    ;

labelledItem_Yield
    : statement_Yield
    | functionDeclaration_Yield
    ;

labelledItem_Await
    : statement_Await
    | functionDeclaration_Await
    ;

labelledItem_Return
    : statement_Return
    | functionDeclaration
    ;

labelledItem_Yield_Await
    : statement_Yield_Await
    | functionDeclaration_Yield_Await
    ;

labelledItem_Yield_Return
    : statement_Yield_Return
    | functionDeclaration_Yield
    ;

labelledItem_Await_Return
    : statement_Await_Return
    | functionDeclaration_Await
    ;

labelledItem_Yield_Await_Return
    : statement_Yield_Await_Return
    | functionDeclaration_Yield_Await
    ;

// 14.14 The throw Statement

throwStatement
    : ThrowKeyword /* TODO: no LineTerminator here */ expression_In SemicolonPunctuator
    ;

throwStatement_Yield
    : ThrowKeyword /* TODO: no LineTerminator here */ expression_In_Yield SemicolonPunctuator
    ;

throwStatement_Await
    : ThrowKeyword /* TODO: no LineTerminator here */ expression_In_Await SemicolonPunctuator
    ;

throwStatement_Yield_Await
    : ThrowKeyword /* TODO: no LineTerminator here */ expression_In_Yield_Await SemicolonPunctuator
    ;

// 14.15 The try Statement

tryStatement
    : TryKeyword block catchStatement
    | TryKeyword block finallyStatement
    | TryKeyword block catchStatement finallyStatement
    ;

tryStatement_Yield
    : TryKeyword block_Yield catchStatement_Yield
    | TryKeyword block_Yield finallyStatement_Yield
    | TryKeyword block_Yield catchStatement_Yield finallyStatement_Yield
    ;

tryStatement_Await
    : TryKeyword block_Await catchStatement_Await
    | TryKeyword block_Await finallyStatement_Await
    | TryKeyword block_Await catchStatement_Await finallyStatement_Await
    ;

tryStatement_Return
    : TryKeyword block_Return catchStatement_Return
    | TryKeyword block_Return finallyStatement_Return
    | TryKeyword block_Return catchStatement_Return finallyStatement_Return
    ;

tryStatement_Yield_Await
    : TryKeyword block_Yield_Await catchStatement_Yield_Await
    | TryKeyword block_Yield_Await finallyStatement_Yield_Await
    | TryKeyword block_Yield_Await catchStatement_Yield_Await finallyStatement_Yield_Await
    ;

tryStatement_Yield_Return
    : TryKeyword block_Yield_Return catchStatement_Yield_Return
    | TryKeyword block_Yield_Return finallyStatement_Yield_Return
    | TryKeyword block_Yield_Return catchStatement_Yield_Return finallyStatement_Yield_Return
    ;

tryStatement_Await_Return
    : TryKeyword block_Await_Return catchStatement_Await_Return
    | TryKeyword block_Await_Return finallyStatement_Await_Return
    | TryKeyword block_Await_Return catchStatement_Await_Return finallyStatement_Await_Return
    ;

tryStatement_Yield_Await_Return
    : TryKeyword block_Yield_Await_Return catchStatement_Yield_Await_Return
    | TryKeyword block_Yield_Await_Return finallyStatement_Yield_Await_Return
    | TryKeyword block_Yield_Await_Return catchStatement_Yield_Await_Return finallyStatement_Yield_Await_Return
    ;

catchStatement
    : CatchKeyword (LeftParenthesesLiteral catchParameter RightParenthesesLiteral)? block
    ;

catchStatement_Yield
    : CatchKeyword (LeftParenthesesLiteral catchParameter_Yield RightParenthesesLiteral)? block_Yield
    ;

catchStatement_Await
    : CatchKeyword (LeftParenthesesLiteral catchParameter_Await RightParenthesesLiteral)? block_Await
    ;

catchStatement_Return
    : CatchKeyword (LeftParenthesesLiteral catchParameter RightParenthesesLiteral)? block_Return
    ;

catchStatement_Yield_Await
    : CatchKeyword (LeftParenthesesLiteral catchParameter_Yield_Await RightParenthesesLiteral)? block_Yield_Await
    ;

catchStatement_Yield_Return
    : CatchKeyword (LeftParenthesesLiteral catchParameter_Yield RightParenthesesLiteral)? block_Yield_Return
    ;

catchStatement_Await_Return
    : CatchKeyword (LeftParenthesesLiteral catchParameter_Await RightParenthesesLiteral)? block_Await_Return
    ;

catchStatement_Yield_Await_Return
    : CatchKeyword (LeftParenthesesLiteral catchParameter_Yield_Await RightParenthesesLiteral)? block_Yield_Await_Return
    ;

finallyStatement
    : FinallyKeyword block
    ;

finallyStatement_Yield
    : FinallyKeyword block_Yield
    ;

finallyStatement_Await
    : FinallyKeyword block_Await
    ;

finallyStatement_Return
    : FinallyKeyword block_Return
    ;

finallyStatement_Yield_Await
    : FinallyKeyword block_Yield_Await
    ;

finallyStatement_Yield_Return
    : FinallyKeyword block_Yield_Return
    ;

finallyStatement_Await_Return
    : FinallyKeyword block_Await_Return
    ;

finallyStatement_Yield_Await_Return
    : FinallyKeyword block_Yield_Await_Return
    ;

catchParameter
    : bindingIdentifier
    | bindingPattern
    ;

catchParameter_Yield
    : bindingIdentifier_Yield
    | bindingPattern_Yield
    ;

catchParameter_Await
    : bindingIdentifier_Await
    | bindingPattern_Await
    ;

catchParameter_Yield_Await
    : bindingIdentifier_Yield_Await
    | bindingPattern_Yield_Await
    ;

// 14.16 The debugger Statement

debuggerStatement
    : DebuggerKeyword SemicolonPunctuator
    ;

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

labelIdentifier
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
    : IdentifierName // ReservedWord is not matched, because ReservedWord is first in Lexer and doesn't conflict
    ;

// 13.2 Primary Expression

primaryExpression
    : ThisKeyword
    | identifierReference
    | literal
    | arrayLiteral
    | objectLiteral
    | functionExpression
    | classExpression
    | generatorExpression
    | asyncFunctionExpression
    | asyncGeneratorExpression
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
    | functionExpression
    | classExpression_Yield
    | generatorExpression
    | asyncFunctionExpression
    | asyncGeneratorExpression
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
    | functionExpression
    | classExpression_Await
    | generatorExpression
    | asyncFunctionExpression
    | asyncGeneratorExpression
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
    | functionExpression
    | classExpression_Yield_Await
    | generatorExpression
    | asyncFunctionExpression
    | asyncGeneratorExpression
    | RegularExpressionLiteral
    | templateLiteral_Yield_Await
    | coverParenthesizedExpressionAndArrowParameterList_Yield_Await
    ;

coverParenthesizedExpressionAndArrowParameterList
    : LeftParenthesesLiteral expression_In RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In CommaPunctuator RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingIdentifier RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingPattern RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In CommaPunctuator ThreeDotsPunctuator bindingIdentifier RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In CommaPunctuator ThreeDotsPunctuator bindingPattern RightParenthesesLiteral
    ;

coverParenthesizedExpressionAndArrowParameterList_Yield
    : LeftParenthesesLiteral expression_In_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield CommaPunctuator RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingIdentifier_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingPattern_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield CommaPunctuator ThreeDotsPunctuator bindingIdentifier_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield CommaPunctuator ThreeDotsPunctuator bindingPattern_Yield RightParenthesesLiteral
    ;

coverParenthesizedExpressionAndArrowParameterList_Await
    : LeftParenthesesLiteral expression_In_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Await CommaPunctuator RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingIdentifier_Await RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingPattern_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Await CommaPunctuator ThreeDotsPunctuator bindingIdentifier_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Await CommaPunctuator ThreeDotsPunctuator bindingPattern_Await RightParenthesesLiteral
    ;

coverParenthesizedExpressionAndArrowParameterList_Yield_Await
    : LeftParenthesesLiteral expression_In_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield_Await CommaPunctuator RightParenthesesLiteral
    | LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingIdentifier_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral ThreeDotsPunctuator bindingPattern_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield_Await CommaPunctuator ThreeDotsPunctuator bindingIdentifier_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral expression_In_Yield_Await CommaPunctuator ThreeDotsPunctuator bindingPattern_Yield_Await RightParenthesesLiteral
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
    | LeftBracketsLiteral elementList CommaPunctuator elision? RightBracketsLiteral
    ;

arrayLiteral_Yield
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield CommaPunctuator elision? RightBracketsLiteral
    ;

arrayLiteral_Await
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList_Await RightBracketsLiteral
    | LeftBracketsLiteral elementList_Await CommaPunctuator elision? RightBracketsLiteral
    ;

arrayLiteral_Yield_Await
    : LeftBracketsLiteral elision? RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield_Await RightBracketsLiteral
    | LeftBracketsLiteral elementList_Yield_Await CommaPunctuator elision? RightBracketsLiteral
    ;

elementList
    : elision? assignmentExpression_In
    | elision? spreadElement
    | elementList CommaPunctuator elision? assignmentExpression_In
    | elementList CommaPunctuator elision? spreadElement
    ;

elementList_Yield
    : elision? assignmentExpression_In_Yield
    | elision? spreadElement_Yield
    | elementList_Yield CommaPunctuator elision? assignmentExpression_In_Yield
    | elementList_Yield CommaPunctuator elision? spreadElement_Yield
    ;

elementList_Await
    : elision? assignmentExpression_In_Await
    | elision? spreadElement_Await
    | elementList_Await CommaPunctuator elision? assignmentExpression_In_Await
    | elementList_Await CommaPunctuator elision? spreadElement_Await
    ;

elementList_Yield_Await
    : elision? assignmentExpression_In_Yield_Await
    | elision? spreadElement_Yield_Await
    | elementList_Yield_Await CommaPunctuator elision? assignmentExpression_In_Yield_Await
    | elementList_Yield_Await CommaPunctuator elision? spreadElement_Yield_Await
    ;

elision
    : CommaPunctuator+
    ;

spreadElement
    : ThreeDotsPunctuator assignmentExpression_In
    ;

spreadElement_Yield
    : ThreeDotsPunctuator assignmentExpression_In_Yield
    ;

spreadElement_Await
    : ThreeDotsPunctuator assignmentExpression_In_Await
    ;

spreadElement_Yield_Await
    : ThreeDotsPunctuator assignmentExpression_In_Yield_Await
    ;

// 13.2.5 Object Initializer

objectLiteral
    : LeftBracesLiteral (propertyDefinitionList CommaPunctuator?)? RightBracesLiteral
    ;

objectLiteral_Yield
    : LeftBracesLiteral (propertyDefinitionList_Yield CommaPunctuator?)? RightBracesLiteral
    ;

objectLiteral_Await
    : LeftBracesLiteral (propertyDefinitionList_Await CommaPunctuator?)? RightBracesLiteral
    ;

objectLiteral_Yield_Await
    : LeftBracesLiteral (propertyDefinitionList_Yield_Await CommaPunctuator?)? RightBracesLiteral
    ;

propertyDefinitionList
    : propertyDefinition (CommaPunctuator propertyDefinition)*
    ;

propertyDefinitionList_Yield
    : propertyDefinition_Yield (CommaPunctuator propertyDefinition_Yield)*
    ;

propertyDefinitionList_Await
    : propertyDefinition_Await (CommaPunctuator propertyDefinition_Await)*
    ;

propertyDefinitionList_Yield_Await
    : propertyDefinition_Yield_Await (CommaPunctuator propertyDefinition_Yield_Await)*
    ;

propertyDefinition
    : identifierReference
    | coverInitializedName
    | propertyName ConditionalTailPunctuator assignmentExpression_In
    | methodDefinition
    | ThreeDotsPunctuator assignmentExpression_In
    ;

propertyDefinition_Yield
    : identifierReference_Yield
    | coverInitializedName_Yield
    | propertyName_Yield ConditionalTailPunctuator assignmentExpression_In_Yield
    | methodDefinition_Yield
    | ThreeDotsPunctuator assignmentExpression_In_Yield
    ;

propertyDefinition_Await
    : identifierReference_Await
    | coverInitializedName_Await
    | propertyName_Await ConditionalTailPunctuator assignmentExpression_In_Await
    | methodDefinition_Await
    | ThreeDotsPunctuator assignmentExpression_In_Await
    ;

propertyDefinition_Yield_Await
    : identifierReference_Yield_Await
    | coverInitializedName_Yield_Await
    | propertyName_Yield_Await ConditionalTailPunctuator assignmentExpression_In_Yield_Await
    | methodDefinition_Yield_Await
    | ThreeDotsPunctuator assignmentExpression_In_Yield_Await
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
    : AssignmentPunctuator assignmentExpression
    ;

initializer_In
    : AssignmentPunctuator assignmentExpression_In
    ;

initializer_Yield
    : AssignmentPunctuator assignmentExpression_Yield
    ;

initializer_Await
    : AssignmentPunctuator assignmentExpression_Await
    ;

initializer_In_Yield
    : AssignmentPunctuator assignmentExpression_In_Yield
    ;

initializer_In_Await
    : AssignmentPunctuator assignmentExpression_In_Await
    ;

initializer_Yield_Await
    : AssignmentPunctuator assignmentExpression_Yield_Await
    ;

initializer_In_Yield_Await
    : AssignmentPunctuator assignmentExpression_In_Yield_Await
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
    | memberExpression DotPunctuator IdentifierName
    | memberExpression templateLiteral_Tagged
    | superProperty
    | metaProperty
    | NewKeyword memberExpression arguments
    | memberExpression DotPunctuator PrivateIdentifier
    ;

memberExpression_Yield
    : primaryExpression_Yield
    | memberExpression_Yield LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | memberExpression_Yield DotPunctuator IdentifierName
    | memberExpression_Yield templateLiteral_Yield_Tagged
    | superProperty_Yield
    | metaProperty
    | NewKeyword memberExpression_Yield arguments_Yield
    | memberExpression_Yield DotPunctuator PrivateIdentifier
    ;

memberExpression_Await
    : primaryExpression_Await
    | memberExpression_Await LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | memberExpression_Await DotPunctuator IdentifierName
    | memberExpression_Await templateLiteral_Await_Tagged
    | superProperty_Await
    | metaProperty
    | NewKeyword memberExpression_Await arguments_Await
    | memberExpression_Await DotPunctuator PrivateIdentifier
    ;

memberExpression_Yield_Await
    : primaryExpression_Yield_Await
    | memberExpression_Yield_Await LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | memberExpression_Yield_Await DotPunctuator IdentifierName
    | memberExpression_Yield_Await templateLiteral_Yield_Await_Tagged
    | superProperty_Yield_Await
    | metaProperty
    | NewKeyword memberExpression_Yield_Await arguments_Yield_Await
    | memberExpression_Yield_Await DotPunctuator PrivateIdentifier
    ;

superProperty
    : SuperKeyword LeftBracketsLiteral expression_In RightBracketsLiteral
    | SuperKeyword DotPunctuator IdentifierName
    ;

superProperty_Yield
    : SuperKeyword LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | SuperKeyword DotPunctuator IdentifierName
    ;

superProperty_Await
    : SuperKeyword LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | SuperKeyword DotPunctuator IdentifierName
    ;

superProperty_Yield_Await
    : SuperKeyword LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | SuperKeyword DotPunctuator IdentifierName
    ;

metaProperty
    : newTarget
    | importMeta
    ;

newTarget
    : NewKeyword DotPunctuator TargetLiteral
    ;

importMeta
    : ImportKeyword DotPunctuator MetaLiteral
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
    : coverCallExpressionAndAsyncArrowHead
    | superCall
    | importCall
    | callExpression arguments
    | callExpression LeftBracketsLiteral expression_In RightBracketsLiteral
    | callExpression DotPunctuator IdentifierName
    | callExpression templateLiteral_Tagged
    | callExpression DotPunctuator PrivateIdentifier
    ;

callExpression_Yield
    : coverCallExpressionAndAsyncArrowHead_Yield
    | superCall_Yield
    | importCall_Yield
    | callExpression_Yield arguments_Yield
    | callExpression_Yield LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | callExpression_Yield DotPunctuator IdentifierName
    | callExpression_Yield templateLiteral_Yield_Tagged
    | callExpression_Yield DotPunctuator PrivateIdentifier
    ;

callExpression_Await
    : coverCallExpressionAndAsyncArrowHead_Await
    | superCall_Await
    | importCall_Await
    | callExpression_Await arguments_Await
    | callExpression_Await LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | callExpression_Await DotPunctuator IdentifierName
    | callExpression_Await templateLiteral_Await_Tagged
    | callExpression_Await DotPunctuator PrivateIdentifier
    ;

callExpression_Yield_Await
    : coverCallExpressionAndAsyncArrowHead_Yield_Await
    | superCall_Yield_Await
    | importCall_Yield_Await
    | callExpression_Yield_Await arguments_Yield_Await
    | callExpression_Yield_Await LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | callExpression_Yield_Await DotPunctuator IdentifierName
    | callExpression_Yield_Await templateLiteral_Yield_Await_Tagged
    | callExpression_Yield_Await DotPunctuator PrivateIdentifier
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
    | LeftParenthesesLiteral argumentList CommaPunctuator RightParenthesesLiteral
    ;

arguments_Yield
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield CommaPunctuator RightParenthesesLiteral
    ;

arguments_Await
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Await RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Await CommaPunctuator RightParenthesesLiteral
    ;

arguments_Yield_Await
    : LeftParenthesesLiteral RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield_Await RightParenthesesLiteral
    | LeftParenthesesLiteral argumentList_Yield_Await CommaPunctuator RightParenthesesLiteral
    ;

argumentList
    : assignmentExpression_In
    | ThreeDotsPunctuator assignmentExpression_In
    | argumentList CommaPunctuator assignmentExpression_In
    | argumentList CommaPunctuator ThreeDotsPunctuator assignmentExpression_In
    ;

argumentList_Yield
    : assignmentExpression_In_Yield
    | ThreeDotsPunctuator assignmentExpression_In_Yield
    | argumentList_Yield CommaPunctuator assignmentExpression_In_Yield
    | argumentList_Yield CommaPunctuator ThreeDotsPunctuator assignmentExpression_In_Yield
    ;

argumentList_Await
    : assignmentExpression_In_Await
    | ThreeDotsPunctuator assignmentExpression_In_Await
    | argumentList_Await CommaPunctuator assignmentExpression_In_Await
    | argumentList_Await CommaPunctuator ThreeDotsPunctuator assignmentExpression_In_Await
    ;

argumentList_Yield_Await
    : assignmentExpression_In_Yield_Await
    | ThreeDotsPunctuator assignmentExpression_In_Yield_Await
    | argumentList_Yield_Await CommaPunctuator assignmentExpression_In_Yield_Await
    | argumentList_Yield_Await CommaPunctuator ThreeDotsPunctuator assignmentExpression_In_Yield_Await
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
    : OptionalChainingPunctuator arguments
    | OptionalChainingPunctuator LeftBracketsLiteral expression_In RightBracketsLiteral
    | OptionalChainingPunctuator IdentifierName
    | OptionalChainingPunctuator templateLiteral_Tagged
    | OptionalChainingPunctuator PrivateIdentifier
    | optionalChain arguments
    | optionalChain LeftBracketsLiteral expression_In RightBracketsLiteral
    | optionalChain DotPunctuator IdentifierName
    | optionalChain templateLiteral_Tagged
    | optionalChain DotPunctuator PrivateIdentifier
    ;

optionalChain_Yield
    : OptionalChainingPunctuator arguments_Yield
    | OptionalChainingPunctuator LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | OptionalChainingPunctuator IdentifierName
    | OptionalChainingPunctuator templateLiteral_Yield_Tagged
    | OptionalChainingPunctuator PrivateIdentifier
    | optionalChain_Yield arguments_Yield
    | optionalChain_Yield LeftBracketsLiteral expression_In_Yield RightBracketsLiteral
    | optionalChain_Yield DotPunctuator IdentifierName
    | optionalChain_Yield templateLiteral_Yield_Tagged
    | optionalChain_Yield DotPunctuator PrivateIdentifier
    ;

optionalChain_Await
    : OptionalChainingPunctuator arguments_Await
    | OptionalChainingPunctuator LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | OptionalChainingPunctuator IdentifierName
    | OptionalChainingPunctuator templateLiteral_Await_Tagged
    | OptionalChainingPunctuator PrivateIdentifier
    | optionalChain_Await arguments_Await
    | optionalChain_Await LeftBracketsLiteral expression_In_Await RightBracketsLiteral
    | optionalChain_Await DotPunctuator IdentifierName
    | optionalChain_Await templateLiteral_Await_Tagged
    | optionalChain_Await DotPunctuator PrivateIdentifier
    ;

optionalChain_Yield_Await
    : OptionalChainingPunctuator arguments_Yield_Await
    | OptionalChainingPunctuator LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | OptionalChainingPunctuator IdentifierName
    | OptionalChainingPunctuator templateLiteral_Yield_Await_Tagged
    | OptionalChainingPunctuator PrivateIdentifier
    | optionalChain_Yield_Await arguments_Yield_Await
    | optionalChain_Yield_Await LeftBracketsLiteral expression_In_Yield_Await RightBracketsLiteral
    | optionalChain_Yield_Await DotPunctuator IdentifierName
    | optionalChain_Yield_Await templateLiteral_Yield_Await_Tagged
    | optionalChain_Yield_Await DotPunctuator PrivateIdentifier
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
    | leftHandSideExpression /* TODO: [no LineTerminator here] */ IncrementPunctuator
    | leftHandSideExpression /* TODO: [no LineTerminator here] */ DecrementPunctuator
    | IncrementPunctuator unaryExpression
    | DecrementPunctuator unaryExpression
    ;

updateExpression_Yield
    : leftHandSideExpression_Yield
    | leftHandSideExpression_Yield /* TODO: [no LineTerminator here] */ IncrementPunctuator
    | leftHandSideExpression_Yield /* TODO: [no LineTerminator here] */ DecrementPunctuator
    | IncrementPunctuator unaryExpression_Yield
    | DecrementPunctuator unaryExpression_Yield
    ;

updateExpression_Await
    : leftHandSideExpression_Await
    | leftHandSideExpression_Await /* TODO: [no LineTerminator here] */ IncrementPunctuator
    | leftHandSideExpression_Await /* TODO: [no LineTerminator here] */ DecrementPunctuator
    | IncrementPunctuator unaryExpression_Await
    | DecrementPunctuator unaryExpression_Await
    ;

updateExpression_Yield_Await
    : leftHandSideExpression_Yield_Await
    | leftHandSideExpression_Yield_Await /* TODO: [no LineTerminator here] */ IncrementPunctuator
    | leftHandSideExpression_Yield_Await /* TODO: [no LineTerminator here] */ DecrementPunctuator
    | IncrementPunctuator unaryExpression_Yield_Await
    | DecrementPunctuator unaryExpression_Yield_Await
    ;

// 13.5 Unary Operators

unaryExpression
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | AddPunctuator | SubtractPunctuator | BitwiseNOTPunctuator | LogicalNOTPunctuator)* (updateExpression /*| awaitExpression*/)
    ;

unaryExpression_Yield
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | AddPunctuator | SubtractPunctuator | BitwiseNOTPunctuator | LogicalNOTPunctuator)* (updateExpression_Yield /*| awaitExpression_Yield*/)
    ;

unaryExpression_Await
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | AddPunctuator | SubtractPunctuator | BitwiseNOTPunctuator | LogicalNOTPunctuator)* (updateExpression_Await) // TODO: | awaitExpression)
    ;

unaryExpression_Yield_Await
    : (DeleteKeyword | VoidKeyword | TypeofKeyword | AddPunctuator | SubtractPunctuator | BitwiseNOTPunctuator | LogicalNOTPunctuator)* (updateExpression_Yield_Await) // TODO: | awaitExpression_Yield)
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
    : MultiplyPunctuator
    | DividePunctuator
    | RemainderPunctuator
    ;

// 13.8 Additive Operators

additiveExpression
    : multiplicativeExpression
    | additiveExpression AddPunctuator multiplicativeExpression
    | additiveExpression SubtractPunctuator multiplicativeExpression
    ;

additiveExpression_Yield
    : multiplicativeExpression_Yield
    | additiveExpression_Yield AddPunctuator multiplicativeExpression_Yield
    | additiveExpression_Yield SubtractPunctuator multiplicativeExpression_Yield
    ;

additiveExpression_Await
    : multiplicativeExpression_Await
    | additiveExpression_Await AddPunctuator multiplicativeExpression_Await
    | additiveExpression_Await SubtractPunctuator multiplicativeExpression_Await
    ;

additiveExpression_Yield_Await
    : multiplicativeExpression_Yield_Await
    | additiveExpression_Yield_Await AddPunctuator multiplicativeExpression_Yield_Await
    | additiveExpression_Yield_Await SubtractPunctuator multiplicativeExpression_Yield_Await
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
    | relationalExpression LessPunctuator shiftExpression
    | relationalExpression MorePunctuator shiftExpression
    | relationalExpression LessEqualPunctuator shiftExpression
    | relationalExpression MoreEqualPunctuator shiftExpression
    | relationalExpression InstanceofKeyword shiftExpression
    //| relationalExpression_In InKeyword shiftExpression
    //| PrivateIdentifier InKeyword shiftExpression
    ;

relationalExpression_In
    : shiftExpression
    | relationalExpression_In LessPunctuator shiftExpression
    | relationalExpression_In MorePunctuator shiftExpression
    | relationalExpression_In LessEqualPunctuator shiftExpression
    | relationalExpression_In MoreEqualPunctuator shiftExpression
    | relationalExpression_In InstanceofKeyword shiftExpression
    | relationalExpression_In InKeyword shiftExpression
    | PrivateIdentifier InKeyword shiftExpression
    ;

relationalExpression_Yield
    : shiftExpression_Yield
    | relationalExpression_Yield LessPunctuator shiftExpression_Yield
    | relationalExpression_Yield MorePunctuator shiftExpression_Yield
    | relationalExpression_Yield LessEqualPunctuator shiftExpression_Yield
    | relationalExpression_Yield MoreEqualPunctuator shiftExpression_Yield
    | relationalExpression_Yield InstanceofKeyword shiftExpression_Yield
    //| relationalExpression_In_Yield InKeyword shiftExpression_Yield
    //| PrivateIdentifier InKeyword shiftExpression_Yield
    ;

relationalExpression_Await
    : shiftExpression_Await
    | relationalExpression_Await LessPunctuator shiftExpression_Await
    | relationalExpression_Await MorePunctuator shiftExpression_Await
    | relationalExpression_Await LessEqualPunctuator shiftExpression_Await
    | relationalExpression_Await MoreEqualPunctuator shiftExpression_Await
    | relationalExpression_Await InstanceofKeyword shiftExpression_Await
    //| relationalExpression_In_Await InKeyword shiftExpression_Await
    //| PrivateIdentifier InKeyword shiftExpression_Await
    ;

relationalExpression_In_Yield
    : shiftExpression_Yield
    | relationalExpression_In_Yield LessPunctuator shiftExpression_Yield
    | relationalExpression_In_Yield MorePunctuator shiftExpression_Yield
    | relationalExpression_In_Yield LessEqualPunctuator shiftExpression_Yield
    | relationalExpression_In_Yield MoreEqualPunctuator shiftExpression_Yield
    | relationalExpression_In_Yield InstanceofKeyword shiftExpression_Yield
    | relationalExpression_In_Yield InKeyword shiftExpression_Yield
    | PrivateIdentifier InKeyword shiftExpression_Yield
    ;

relationalExpression_In_Await
    : shiftExpression_Await
    | relationalExpression_In_Await LessPunctuator shiftExpression_Await
    | relationalExpression_In_Await MorePunctuator shiftExpression_Await
    | relationalExpression_In_Await LessEqualPunctuator shiftExpression_Await
    | relationalExpression_In_Await MoreEqualPunctuator shiftExpression_Await
    | relationalExpression_In_Await InstanceofKeyword shiftExpression_Await
    | relationalExpression_In_Await InKeyword shiftExpression_Await
    | PrivateIdentifier InKeyword shiftExpression_Await
    ;

relationalExpression_Yield_Await
    : shiftExpression_Yield_Await
    | relationalExpression_Yield_Await LessPunctuator shiftExpression_Yield_Await
    | relationalExpression_Yield_Await MorePunctuator shiftExpression_Yield_Await
    | relationalExpression_Yield_Await LessEqualPunctuator shiftExpression_Yield_Await
    | relationalExpression_Yield_Await MoreEqualPunctuator shiftExpression_Yield_Await
    | relationalExpression_Yield_Await InstanceofKeyword shiftExpression_Yield_Await
    //| relationalExpression_In_Yield_Await InKeyword shiftExpression_Yield_Await
    //| PrivateIdentifier InKeyword shiftExpression_Yield_Await
    ;

relationalExpression_In_Yield_Await
    : shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await LessPunctuator shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await MorePunctuator shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await LessEqualPunctuator shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await MoreEqualPunctuator shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await InstanceofKeyword shiftExpression_Yield_Await
    | relationalExpression_In_Yield_Await InKeyword shiftExpression_Yield_Await
    | PrivateIdentifier InKeyword shiftExpression_Yield_Await
    ;

// 13.11 Equality Operators

equalityExpression
    : relationalExpression
    | equalityExpression LooselyEqualPunctuator relationalExpression
    | equalityExpression LooselyNotEqualPunctuator relationalExpression
    | equalityExpression StrictlyEqualPunctuator relationalExpression
    | equalityExpression StrictlyNotEqualPunctuator relationalExpression
    ;

equalityExpression_In
    : relationalExpression_In
    | equalityExpression_In LooselyEqualPunctuator relationalExpression_In
    | equalityExpression_In LooselyNotEqualPunctuator relationalExpression_In
    | equalityExpression_In StrictlyEqualPunctuator relationalExpression_In
    | equalityExpression_In StrictlyNotEqualPunctuator relationalExpression_In
    ;

equalityExpression_Yield
    : relationalExpression_Yield
    | equalityExpression_Yield LooselyEqualPunctuator relationalExpression_Yield
    | equalityExpression_Yield LooselyNotEqualPunctuator relationalExpression_Yield
    | equalityExpression_Yield StrictlyEqualPunctuator relationalExpression_Yield
    | equalityExpression_Yield StrictlyNotEqualPunctuator relationalExpression_Yield
    ;

equalityExpression_Await
    : relationalExpression_Await
    | equalityExpression_Await LooselyEqualPunctuator relationalExpression_Await
    | equalityExpression_Await LooselyNotEqualPunctuator relationalExpression_Await
    | equalityExpression_Await StrictlyEqualPunctuator relationalExpression_Await
    | equalityExpression_Await StrictlyNotEqualPunctuator relationalExpression_Await
    ;

equalityExpression_In_Yield
    : relationalExpression_In_Yield
    | equalityExpression_In_Yield LooselyEqualPunctuator relationalExpression_In_Yield
    | equalityExpression_In_Yield LooselyNotEqualPunctuator relationalExpression_In_Yield
    | equalityExpression_In_Yield StrictlyEqualPunctuator relationalExpression_In_Yield
    | equalityExpression_In_Yield StrictlyNotEqualPunctuator relationalExpression_In_Yield
    ;

equalityExpression_In_Await
    : relationalExpression_In_Await
    | equalityExpression_In_Await LooselyEqualPunctuator relationalExpression_In_Await
    | equalityExpression_In_Await LooselyNotEqualPunctuator relationalExpression_In_Await
    | equalityExpression_In_Await StrictlyEqualPunctuator relationalExpression_In_Await
    | equalityExpression_In_Await StrictlyNotEqualPunctuator relationalExpression_In_Await
    ;

equalityExpression_Yield_Await
    : relationalExpression_Yield_Await
    | equalityExpression_Yield_Await LooselyEqualPunctuator relationalExpression_Yield_Await
    | equalityExpression_Yield_Await LooselyNotEqualPunctuator relationalExpression_Yield_Await
    | equalityExpression_Yield_Await StrictlyEqualPunctuator relationalExpression_Yield_Await
    | equalityExpression_Yield_Await StrictlyNotEqualPunctuator relationalExpression_Yield_Await
    ;

equalityExpression_In_Yield_Await
    : relationalExpression_In_Yield_Await
    | equalityExpression_In_Yield_Await LooselyEqualPunctuator relationalExpression_In_Yield_Await
    | equalityExpression_In_Yield_Await LooselyNotEqualPunctuator relationalExpression_In_Yield_Await
    | equalityExpression_In_Yield_Await StrictlyEqualPunctuator relationalExpression_In_Yield_Await
    | equalityExpression_In_Yield_Await StrictlyNotEqualPunctuator relationalExpression_In_Yield_Await
    ;

// 13.12 Binary Bitwise Operators

bitwiseANDExpression
    : equalityExpression (BitwiseANDPunctuator equalityExpression)*
    ;

bitwiseANDExpression_In
    : equalityExpression_In (BitwiseANDPunctuator equalityExpression_In)*
    ;

bitwiseANDExpression_Yield
    : equalityExpression_Yield (BitwiseANDPunctuator equalityExpression_Yield)*
    ;

bitwiseANDExpression_Await
    : equalityExpression_Await (BitwiseANDPunctuator equalityExpression_Await)*
    ;

bitwiseANDExpression_In_Yield
    : equalityExpression_In_Yield (BitwiseANDPunctuator equalityExpression_In_Yield)*
    ;

bitwiseANDExpression_In_Await
    : equalityExpression_In_Await (BitwiseANDPunctuator equalityExpression_In_Await)*
    ;

bitwiseANDExpression_Yield_Await
    : equalityExpression_Yield_Await (BitwiseANDPunctuator equalityExpression_Yield_Await)*
    ;

bitwiseANDExpression_In_Yield_Await
    : equalityExpression_In_Yield_Await (BitwiseANDPunctuator equalityExpression_In_Yield_Await)*
    ;

bitwiseXORExpression
    : bitwiseANDExpression (BitwiseXORPunctuator bitwiseANDExpression)*
    ;

bitwiseXORExpression_In
    : bitwiseANDExpression_In (BitwiseXORPunctuator bitwiseANDExpression_In)*
    ;

bitwiseXORExpression_Yield
    : bitwiseANDExpression_Yield (BitwiseXORPunctuator bitwiseANDExpression_Yield)*
    ;

bitwiseXORExpression_Await
    : bitwiseANDExpression_Await (BitwiseXORPunctuator bitwiseANDExpression_Await)*
    ;

bitwiseXORExpression_In_Yield
    : bitwiseANDExpression_In_Yield (BitwiseXORPunctuator bitwiseANDExpression_In_Yield)*
    ;

bitwiseXORExpression_In_Await
    : bitwiseANDExpression_In_Await (BitwiseXORPunctuator bitwiseANDExpression_In_Await)*
    ;

bitwiseXORExpression_Yield_Await
    : bitwiseANDExpression_Yield_Await (BitwiseXORPunctuator bitwiseANDExpression_Yield_Await)*
    ;

bitwiseXORExpression_In_Yield_Await
    : bitwiseANDExpression_In_Yield_Await (BitwiseXORPunctuator bitwiseANDExpression_In_Yield_Await)*
    ;

bitwiseORExpression
    : bitwiseXORExpression (BitwiseORPunctuator bitwiseXORExpression)*
    ;

bitwiseORExpression_In
    : bitwiseXORExpression_In (BitwiseORPunctuator bitwiseXORExpression_In)*
    ;

bitwiseORExpression_Yield
    : bitwiseXORExpression_Yield (BitwiseORPunctuator bitwiseXORExpression_Yield)*
    ;

bitwiseORExpression_Await
    : bitwiseXORExpression_Await (BitwiseORPunctuator bitwiseXORExpression_Await)*
    ;

bitwiseORExpression_In_Yield
    : bitwiseXORExpression_In_Yield (BitwiseORPunctuator bitwiseXORExpression_In_Yield)*
    ;

bitwiseORExpression_In_Await
    : bitwiseXORExpression_In_Await (BitwiseORPunctuator bitwiseXORExpression_In_Await)*
    ;

bitwiseORExpression_Yield_Await
    : bitwiseXORExpression_Yield_Await (BitwiseORPunctuator bitwiseXORExpression_Yield_Await)*
    ;

bitwiseORExpression_In_Yield_Await
    : bitwiseXORExpression_In_Yield_Await (BitwiseORPunctuator bitwiseXORExpression_In_Yield_Await)*
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
    : logicalANDExpression (LogicalORPunctuator logicalANDExpression)*
    ;

logicalORExpression_In
    : logicalANDExpression_In (LogicalORPunctuator logicalANDExpression_In)*
    ;

logicalORExpression_Yield
    : logicalANDExpression_Yield (LogicalORPunctuator logicalANDExpression_Yield)*
    ;

logicalORExpression_Await
    : logicalANDExpression_Await (LogicalORPunctuator logicalANDExpression_Await)*
    ;

logicalORExpression_In_Yield
    : logicalANDExpression_In_Yield (LogicalORPunctuator logicalANDExpression_In_Yield)*
    ;

logicalORExpression_In_Await
    : logicalANDExpression_In_Await (LogicalORPunctuator logicalANDExpression_In_Await)*
    ;

logicalORExpression_Yield_Await
    : logicalANDExpression_Yield_Await (LogicalORPunctuator logicalANDExpression_Yield_Await)*
    ;

logicalORExpression_In_Yield_Await
    : logicalANDExpression_In_Yield_Await (LogicalORPunctuator logicalANDExpression_In_Yield_Await)*
    ;

coalesceExpression
    : bitwiseORExpression (CoalescePunctuator bitwiseORExpression)+
    ;

coalesceExpressionHead
    : coalesceExpression
    | bitwiseORExpression
    ;

coalesceExpression_In
    : bitwiseORExpression_In (CoalescePunctuator bitwiseORExpression_In)+
    ;

coalesceExpressionHead_In
    : coalesceExpression_In
    | bitwiseORExpression_In
    ;

coalesceExpression_Yield
    : bitwiseORExpression_Yield (CoalescePunctuator bitwiseORExpression_Yield)+
    ;

coalesceExpressionHead_Yield
    : coalesceExpression_Yield
    | bitwiseORExpression_Yield
    ;

coalesceExpression_Await
    : bitwiseORExpression_Await (CoalescePunctuator bitwiseORExpression_Await)+
    ;

coalesceExpressionHead_Await
    : coalesceExpression_Await
    | bitwiseORExpression_Await
    ;

coalesceExpression_In_Yield
    : bitwiseORExpression_In_Yield (CoalescePunctuator bitwiseORExpression_In_Yield)+
    ;

coalesceExpressionHead_In_Yield
    : coalesceExpression_In_Yield
    | bitwiseORExpression_In_Yield
    ;

coalesceExpression_In_Await
    : bitwiseORExpression_In_Await (CoalescePunctuator bitwiseORExpression_In_Await)+
    ;

coalesceExpressionHead_In_Await
    : coalesceExpression_In_Await
    | bitwiseORExpression_In_Await
    ;

coalesceExpression_Yield_Await
    : bitwiseORExpression_Yield_Await (CoalescePunctuator bitwiseORExpression_Yield_Await)+
    ;

coalesceExpressionHead_Yield_Await
    : coalesceExpression_Yield_Await
    | bitwiseORExpression_Yield_Await
    ;

coalesceExpression_In_Yield_Await
    : bitwiseORExpression_In_Yield_Await (CoalescePunctuator bitwiseORExpression_In_Yield_Await)+
    ;

coalesceExpressionHead_In_Yield_Await
    : coalesceExpression_In_Yield_Await
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
    | shortCircuitExpression ConditionalHeadPunctuator assignmentExpression_In ConditionalTailPunctuator assignmentExpression
    ;

conditionalExpression_In
    : shortCircuitExpression_In
    | shortCircuitExpression_In ConditionalHeadPunctuator assignmentExpression_In ConditionalTailPunctuator assignmentExpression_In
    ;

conditionalExpression_Yield
    : shortCircuitExpression_Yield
    | shortCircuitExpression_Yield ConditionalHeadPunctuator assignmentExpression_In_Yield ConditionalTailPunctuator assignmentExpression_Yield
    ;

conditionalExpression_Await
    : shortCircuitExpression_Await
    | shortCircuitExpression_Await ConditionalHeadPunctuator assignmentExpression_In_Await ConditionalTailPunctuator assignmentExpression_Await
    ;

conditionalExpression_In_Yield
    : shortCircuitExpression_In_Yield
    | shortCircuitExpression_In_Yield ConditionalHeadPunctuator assignmentExpression_In_Yield ConditionalTailPunctuator assignmentExpression_In_Yield
    ;

conditionalExpression_In_Await
    : shortCircuitExpression_In_Await
    | shortCircuitExpression_In_Await ConditionalHeadPunctuator assignmentExpression_In_Await ConditionalTailPunctuator assignmentExpression_In_Await
    ;

conditionalExpression_Yield_Await
    : shortCircuitExpression_Yield_Await
    | shortCircuitExpression_Yield_Await ConditionalHeadPunctuator assignmentExpression_In_Yield_Await ConditionalTailPunctuator assignmentExpression_Yield_Await
    ;

conditionalExpression_In_Yield_Await
    : shortCircuitExpression_In_Yield_Await
    | shortCircuitExpression_In_Yield_Await ConditionalHeadPunctuator assignmentExpression_In_Yield_Await ConditionalTailPunctuator assignmentExpression_In_Yield_Await
    ;

// 13.15 Assignment Operators

assignmentExpression
    : conditionalExpression
    //| yieldExpression
    | arrowFunction
    | asyncArrowFunction
    | leftHandSideExpression AssignmentPunctuator assignmentExpression
    | leftHandSideExpression assignmentOperator assignmentExpression
    | leftHandSideExpression LogicalANDAssignmentPunctuator assignmentExpression
    | leftHandSideExpression LogicalORAssignmentPunctuator assignmentExpression
    | leftHandSideExpression CoalesceAssignmentPunctuator assignmentExpression
    ;

assignmentExpression_In
    : conditionalExpression_In
    //| yieldExpression_In
    | arrowFunction_In
    | asyncArrowFunction_In
    | leftHandSideExpression AssignmentPunctuator assignmentExpression_In
    | leftHandSideExpression assignmentOperator assignmentExpression_In
    | leftHandSideExpression LogicalANDAssignmentPunctuator assignmentExpression_In
    | leftHandSideExpression LogicalORAssignmentPunctuator assignmentExpression_In
    | leftHandSideExpression CoalesceAssignmentPunctuator assignmentExpression_In
    ;

assignmentExpression_Yield
    : conditionalExpression_Yield
    | yieldExpression
    | arrowFunction_Yield
    | asyncArrowFunction_Yield
    | leftHandSideExpression_Yield AssignmentPunctuator assignmentExpression_Yield
    | leftHandSideExpression_Yield assignmentOperator assignmentExpression_Yield
    | leftHandSideExpression_Yield LogicalANDAssignmentPunctuator assignmentExpression_Yield
    | leftHandSideExpression_Yield LogicalORAssignmentPunctuator assignmentExpression_Yield
    | leftHandSideExpression_Yield CoalesceAssignmentPunctuator assignmentExpression_Yield
    ;

assignmentExpression_Await
    : conditionalExpression_Await
    //| yieldExpression
    | arrowFunction_Await
    | asyncArrowFunction_Await
    | leftHandSideExpression_Await AssignmentPunctuator assignmentExpression_Await
    | leftHandSideExpression_Await assignmentOperator assignmentExpression_Await
    | leftHandSideExpression_Await LogicalANDAssignmentPunctuator assignmentExpression_Await
    | leftHandSideExpression_Await LogicalORAssignmentPunctuator assignmentExpression_Await
    | leftHandSideExpression_Await CoalesceAssignmentPunctuator assignmentExpression_Await
    ;

assignmentExpression_In_Yield
    : conditionalExpression_In_Yield
    | yieldExpression_In
    | arrowFunction_In_Yield
    | asyncArrowFunction_In_Yield
    | leftHandSideExpression_Yield AssignmentPunctuator assignmentExpression_In_Yield
    | leftHandSideExpression_Yield assignmentOperator assignmentExpression_In_Yield
    | leftHandSideExpression_Yield LogicalANDAssignmentPunctuator assignmentExpression_In_Yield
    | leftHandSideExpression_Yield LogicalORAssignmentPunctuator assignmentExpression_In_Yield
    | leftHandSideExpression_Yield CoalesceAssignmentPunctuator assignmentExpression_In_Yield
    ;

assignmentExpression_In_Await
    : conditionalExpression_In_Await
    //| yieldExpression_In_Await
    | arrowFunction_In_Await
    | asyncArrowFunction_In_Await
    | leftHandSideExpression_Await AssignmentPunctuator assignmentExpression_In_Await
    | leftHandSideExpression_Await assignmentOperator assignmentExpression_In_Await
    | leftHandSideExpression_Await LogicalANDAssignmentPunctuator assignmentExpression_In_Await
    | leftHandSideExpression_Await LogicalORAssignmentPunctuator assignmentExpression_In_Await
    | leftHandSideExpression_Await CoalesceAssignmentPunctuator assignmentExpression_In_Await
    ;

assignmentExpression_Yield_Await
    : conditionalExpression_Yield_Await
    | yieldExpression_Await
    | arrowFunction_Yield_Await
    | asyncArrowFunction_Yield_Await
    | leftHandSideExpression_Yield_Await AssignmentPunctuator assignmentExpression_Yield_Await
    | leftHandSideExpression_Yield_Await assignmentOperator assignmentExpression_Yield_Await
    | leftHandSideExpression_Yield_Await LogicalANDAssignmentPunctuator assignmentExpression_Yield_Await
    | leftHandSideExpression_Yield_Await LogicalORAssignmentPunctuator assignmentExpression_Yield_Await
    | leftHandSideExpression_Yield_Await CoalesceAssignmentPunctuator assignmentExpression_Yield_Await
    ;

assignmentExpression_In_Yield_Await
    : conditionalExpression_In_Yield_Await
    | yieldExpression_In_Await
    | arrowFunction_In_Yield_Await
    | asyncArrowFunction_In_Yield_Await
    | leftHandSideExpression_Yield_Await AssignmentPunctuator assignmentExpression_In_Yield_Await
    | leftHandSideExpression_Yield_Await assignmentOperator assignmentExpression_In_Yield_Await
    | leftHandSideExpression_Yield_Await LogicalANDAssignmentPunctuator assignmentExpression_In_Yield_Await
    | leftHandSideExpression_Yield_Await LogicalORAssignmentPunctuator assignmentExpression_In_Yield_Await
    | leftHandSideExpression_Yield_Await CoalesceAssignmentPunctuator assignmentExpression_In_Yield_Await
    ;

assignmentOperator
    : MultiplyAssignmentPunctuator
    | DivideAssignmentPunctuator
    | RemainderAssignmentPunctuator
    | AddAssignmentPunctuator
    | SubtractAssignmentPunctuator
    | LeftShiftAssignmentPunctuator
    | SignedRightShiftAssignmentPunctuator
    | UnsignedRightShiftAssignmentPunctuator
    | BitwiseANDAssignmentPunctuator
    | BitwiseXORAssignmentPunctuator
    | BitwiseORAssignmentPunctuator
    | ExponentiateAssignmentPunctuator
    ;

// 13.16 Comma Operator ( , )

expression
    : assignmentExpression (CommaPunctuator assignmentExpression)*
    ;

expression_In
    : assignmentExpression_In (CommaPunctuator assignmentExpression_In)*
    ;

expression_Yield
    : assignmentExpression_Yield (CommaPunctuator assignmentExpression_Yield)*
    ;

expression_Await
    : assignmentExpression_Await (CommaPunctuator assignmentExpression_Await)*
    ;

expression_In_Yield
    : assignmentExpression_In_Yield (CommaPunctuator assignmentExpression_In_Yield)*
    ;

expression_In_Await
    : assignmentExpression_In_Await (CommaPunctuator assignmentExpression_In_Await)*
    ;

expression_Yield_Await
    : assignmentExpression_Yield_Await (CommaPunctuator assignmentExpression_Yield_Await)*
    ;

expression_In_Yield_Await
    : assignmentExpression_In_Yield_Await (CommaPunctuator assignmentExpression_In_Yield_Await)*
    ;

// 12 ECMAScript Language: Lexical Grammar

inputElementDiv
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | DivPunctuator
    | RightBracesLiteral
    ;

inputElementRegExp
    : WhiteSpace
    | LineTerminator
    | Comment
    | CommonToken
    | RightBracesLiteral
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
