using Crosslight.API.Nodes;
using Crosslight.API.Nodes.Metadata;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.CSharp.Syntax.PatternMatching;

namespace Crosslight.Language.CIL.Nodes
{
    public class DummyCILAstVisitor : IAstVisitor<Node>
    {
        private Node CreateDummy(AstNode astNode)
        {
            Node result = new DummyNode(astNode.GetType().Name);
            foreach (var child in astNode.Children)
            {
                var adapted = child.AcceptVisitor(this);
                result.Children.Add(adapted);
            }
            return result;
        }

        private Node CreateDummy(AstNode astNode, string name)
        {
            Node result = new DummyNode(name);
            foreach (var child in astNode.Children)
            {
                var adapted = child.AcceptVisitor(this);
                result.Children.Add(adapted);
            }
            return result;
        }

        public Node VisitAccessor(Accessor accessor)
        {
            return CreateDummy(accessor);
        }

        public Node VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            return CreateDummy(anonymousMethodExpression);
        }

        public Node VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            return CreateDummy(anonymousTypeCreateExpression);
        }

        public Node VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            return CreateDummy(arrayCreateExpression);
        }

        public Node VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            return CreateDummy(arrayInitializerExpression);
        }

        public Node VisitArraySpecifier(ArraySpecifier arraySpecifier)
        {
            return CreateDummy(arraySpecifier);
        }

        public Node VisitAsExpression(AsExpression asExpression)
        {
            return CreateDummy(asExpression);
        }

        public Node VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            return CreateDummy(assignmentExpression);
        }

        public Node VisitAttribute(ICSharpCode.Decompiler.CSharp.Syntax.Attribute attribute)
        {
            return CreateDummy(attribute);
        }

        public Node VisitAttributeSection(AttributeSection attributeSection)
        {
            return CreateDummy(attributeSection);
        }

        public Node VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            return CreateDummy(baseReferenceExpression);
        }

        public Node VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            return CreateDummy(binaryOperatorExpression);
        }

        public Node VisitBlockStatement(BlockStatement blockStatement)
        {
            return CreateDummy(blockStatement);
        }

        public Node VisitBreakStatement(BreakStatement breakStatement)
        {
            return CreateDummy(breakStatement);
        }

        public Node VisitCaseLabel(CaseLabel caseLabel)
        {
            return CreateDummy(caseLabel);
        }

        public Node VisitCastExpression(CastExpression castExpression)
        {
            return CreateDummy(castExpression);
        }

        public Node VisitCatchClause(CatchClause catchClause)
        {
            return CreateDummy(catchClause);
        }

        public Node VisitCheckedExpression(CheckedExpression checkedExpression)
        {
            return CreateDummy(checkedExpression);
        }

        public Node VisitCheckedStatement(CheckedStatement checkedStatement)
        {
            return CreateDummy(checkedStatement);
        }

        public Node VisitComment(Comment comment)
        {
            return CreateDummy(comment);
        }

        public Node VisitComposedType(ComposedType composedType)
        {
            return CreateDummy(composedType);
        }

        public Node VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            return CreateDummy(conditionalExpression);
        }

        public Node VisitConstraint(Constraint constraint)
        {
            return CreateDummy(constraint);
        }

        public Node VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            return CreateDummy(constructorDeclaration);
        }

        public Node VisitConstructorInitializer(ConstructorInitializer constructorInitializer)
        {
            return CreateDummy(constructorInitializer);
        }

        public Node VisitContinueStatement(ContinueStatement continueStatement)
        {
            return CreateDummy(continueStatement);
        }

        public Node VisitCSharpTokenNode(CSharpTokenNode cSharpTokenNode)
        {
            return CreateDummy(cSharpTokenNode, $"token: {cSharpTokenNode}");
        }

        public Node VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            return CreateDummy(customEventDeclaration);
        }

        public Node VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression)
        {
            return CreateDummy(defaultValueExpression);
        }

        public Node VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            return CreateDummy(delegateDeclaration);
        }

        public Node VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        {
            return CreateDummy(destructorDeclaration);
        }

        public Node VisitDirectionExpression(DirectionExpression directionExpression)
        {
            return CreateDummy(directionExpression);
        }

        public Node VisitDocumentationReference(DocumentationReference documentationReference)
        {
            return CreateDummy(documentationReference);
        }

        public Node VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            return CreateDummy(doWhileStatement);
        }

        public Node VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            return CreateDummy(emptyStatement);
        }

        public Node VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            return CreateDummy(enumMemberDeclaration);
        }

        public Node VisitErrorNode(AstNode errorNode)
        {
            return CreateDummy(errorNode);
        }

        public Node VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            return CreateDummy(eventDeclaration);
        }

        public Node VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            return CreateDummy(expressionStatement);
        }

        public Node VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        {
            return CreateDummy(externAliasDeclaration);
        }

        public Node VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            return CreateDummy(fieldDeclaration);
        }

        public Node VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration)
        {
            return CreateDummy(fixedFieldDeclaration);
        }

        public Node VisitFixedStatement(FixedStatement fixedStatement)
        {
            return CreateDummy(fixedStatement);
        }

        public Node VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer)
        {
            return CreateDummy(fixedVariableInitializer);
        }

        public Node VisitForeachStatement(ForeachStatement foreachStatement)
        {
            return CreateDummy(foreachStatement);
        }

        public Node VisitForStatement(ForStatement forStatement)
        {
            return CreateDummy(forStatement);
        }

        public Node VisitFunctionPointerType(FunctionPointerType functionPointerType)
        {
            return CreateDummy(functionPointerType);
        }

        public Node VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement)
        {
            return CreateDummy(gotoCaseStatement);
        }

        public Node VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement)
        {
            return CreateDummy(gotoDefaultStatement);
        }

        public Node VisitGotoStatement(GotoStatement gotoStatement)
        {
            return CreateDummy(gotoStatement);
        }

        public Node VisitIdentifier(Identifier identifier)
        {
            return CreateDummy(identifier, $"Id: {identifier.Name}");
        }

        public Node VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            return CreateDummy(identifierExpression);
        }

        public Node VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            return CreateDummy(ifElseStatement);
        }

        public Node VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        {
            return CreateDummy(indexerDeclaration);
        }

        public Node VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            return CreateDummy(indexerExpression);
        }

        public Node VisitInterpolatedStringExpression(InterpolatedStringExpression interpolatedStringExpression)
        {
            return CreateDummy(interpolatedStringExpression);
        }

        public Node VisitInterpolatedStringText(InterpolatedStringText interpolatedStringText)
        {
            return CreateDummy(interpolatedStringText);
        }

        public Node VisitInterpolation(Interpolation interpolation)
        {
            return CreateDummy(interpolation);
        }

        public Node VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            return CreateDummy(invocationExpression);
        }

        public Node VisitIsExpression(IsExpression isExpression)
        {
            return CreateDummy(isExpression);
        }

        public Node VisitLabelStatement(LabelStatement labelStatement)
        {
            return CreateDummy(labelStatement);
        }

        public Node VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            return CreateDummy(lambdaExpression);
        }

        public Node VisitLocalFunctionDeclarationStatement(LocalFunctionDeclarationStatement localFunctionDeclarationStatement)
        {
            return CreateDummy(localFunctionDeclarationStatement);
        }

        public Node VisitLockStatement(LockStatement lockStatement)
        {
            return CreateDummy(lockStatement);
        }

        public Node VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            return CreateDummy(memberReferenceExpression);
        }

        public Node VisitMemberType(MemberType memberType)
        {
            return CreateDummy(memberType);
        }

        public Node VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            return CreateDummy(methodDeclaration);
        }

        public Node VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
        {
            return CreateDummy(namedArgumentExpression);
        }

        public Node VisitNamedExpression(NamedExpression namedExpression)
        {
            return CreateDummy(namedExpression);
        }

        public Node VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            return CreateDummy(namespaceDeclaration);
        }

        public Node VisitNullNode(AstNode nullNode)
        {
            return CreateDummy(nullNode);
        }

        public Node VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression)
        {
            return CreateDummy(nullReferenceExpression);
        }

        public Node VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            return CreateDummy(objectCreateExpression);
        }

        public Node VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            return CreateDummy(operatorDeclaration);
        }

        public Node VisitOutVarDeclarationExpression(OutVarDeclarationExpression outVarDeclarationExpression)
        {
            return CreateDummy(outVarDeclarationExpression);
        }

        public Node VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        {
            return CreateDummy(parameterDeclaration);
        }

        public Node VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            return CreateDummy(parenthesizedExpression);
        }

        public Node VisitPatternPlaceholder(AstNode placeholder, Pattern pattern)
        {
            return CreateDummy(placeholder);
        }

        public Node VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression)
        {
            return CreateDummy(pointerReferenceExpression);
        }

        public Node VisitPreProcessorDirective(PreProcessorDirective preProcessorDirective)
        {
            return CreateDummy(preProcessorDirective);
        }

        public Node VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            return CreateDummy(primitiveExpression, $"primExp: {primitiveExpression.Format} '{primitiveExpression.Value}'");
        }

        public Node VisitPrimitiveType(PrimitiveType primitiveType)
        {
            return CreateDummy(primitiveType, $"primT: {primitiveType.Keyword}");
        }

        public Node VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            return CreateDummy(propertyDeclaration);
        }

        public Node VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause)
        {
            return CreateDummy(queryContinuationClause);
        }

        public Node VisitQueryExpression(QueryExpression queryExpression)
        {
            return CreateDummy(queryExpression);
        }

        public Node VisitQueryFromClause(QueryFromClause queryFromClause)
        {
            return CreateDummy(queryFromClause);
        }

        public Node VisitQueryGroupClause(QueryGroupClause queryGroupClause)
        {
            return CreateDummy(queryGroupClause);
        }

        public Node VisitQueryJoinClause(QueryJoinClause queryJoinClause)
        {
            return CreateDummy(queryJoinClause);
        }

        public Node VisitQueryLetClause(QueryLetClause queryLetClause)
        {
            return CreateDummy(queryLetClause);
        }

        public Node VisitQueryOrderClause(QueryOrderClause queryOrderClause)
        {
            return CreateDummy(queryOrderClause);
        }

        public Node VisitQueryOrdering(QueryOrdering queryOrdering)
        {
            return CreateDummy(queryOrdering);
        }

        public Node VisitQuerySelectClause(QuerySelectClause querySelectClause)
        {
            return CreateDummy(querySelectClause);
        }

        public Node VisitQueryWhereClause(QueryWhereClause queryWhereClause)
        {
            return CreateDummy(queryWhereClause);
        }

        public Node VisitReturnStatement(ReturnStatement returnStatement)
        {
            return CreateDummy(returnStatement);
        }

        public Node VisitSimpleType(SimpleType simpleType)
        {
            return CreateDummy(simpleType);
        }

        public Node VisitSizeOfExpression(SizeOfExpression sizeOfExpression)
        {
            return CreateDummy(sizeOfExpression);
        }

        public Node VisitStackAllocExpression(StackAllocExpression stackAllocExpression)
        {
            return CreateDummy(stackAllocExpression);
        }

        public Node VisitSwitchSection(SwitchSection switchSection)
        {
            return CreateDummy(switchSection);
        }

        public Node VisitSwitchStatement(SwitchStatement switchStatement)
        {
            return CreateDummy(switchStatement);
        }

        public Node VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            return CreateDummy(syntaxTree);
        }

        public Node VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            return CreateDummy(thisReferenceExpression);
        }

        public Node VisitThrowExpression(ThrowExpression throwExpression)
        {
            return CreateDummy(throwExpression);
        }

        public Node VisitThrowStatement(ThrowStatement throwStatement)
        {
            return CreateDummy(throwStatement);
        }

        public Node VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            return CreateDummy(tryCatchStatement);
        }

        public Node VisitTupleExpression(TupleExpression tupleExpression)
        {
            return CreateDummy(tupleExpression);
        }

        public Node VisitTupleType(TupleAstType tupleType)
        {
            return CreateDummy(tupleType);
        }

        public Node VisitTupleTypeElement(TupleTypeElement tupleTypeElement)
        {
            return CreateDummy(tupleTypeElement);
        }

        public Node VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            return CreateDummy(typeDeclaration, $"typedecl: {typeDeclaration.ClassType}");
        }

        public Node VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            return CreateDummy(typeOfExpression);
        }

        public Node VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration)
        {
            return CreateDummy(typeParameterDeclaration);
        }

        public Node VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            return CreateDummy(typeReferenceExpression);
        }

        public Node VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            return CreateDummy(unaryOperatorExpression);
        }

        public Node VisitUncheckedExpression(UncheckedExpression uncheckedExpression)
        {
            return CreateDummy(uncheckedExpression);
        }

        public Node VisitUncheckedStatement(UncheckedStatement uncheckedStatement)
        {
            return CreateDummy(uncheckedStatement);
        }

        public Node VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression)
        {
            return CreateDummy(undocumentedExpression);
        }

        public Node VisitUnsafeStatement(UnsafeStatement unsafeStatement)
        {
            return CreateDummy(unsafeStatement);
        }

        public Node VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            return CreateDummy(usingAliasDeclaration);
        }

        public Node VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            return CreateDummy(usingDeclaration, $"using: {usingDeclaration.Namespace}");
        }

        public Node VisitUsingStatement(UsingStatement usingStatement)
        {
            return CreateDummy(usingStatement);
        }

        public Node VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            return CreateDummy(variableDeclarationStatement);
        }

        public Node VisitVariableInitializer(VariableInitializer variableInitializer)
        {
            return CreateDummy(variableInitializer);
        }

        public Node VisitWhileStatement(WhileStatement whileStatement)
        {
            return CreateDummy(whileStatement);
        }

        public Node VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement)
        {
            return CreateDummy(yieldBreakStatement);
        }

        public Node VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement)
        {
            return CreateDummy(yieldReturnStatement);
        }
    }
}
