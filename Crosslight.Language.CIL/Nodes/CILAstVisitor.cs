using Crosslight.API.Nodes;
using Crosslight.Language.CIL.Nodes.Visitors;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.CSharp.Syntax.PatternMatching;
using System;

namespace Crosslight.Language.CIL.Nodes
{
    public class CILAstVisitor : IAstVisitor<Node>
    {
        public VisitContext VisitContext { get; }

        public CILAstVisitor(CILVisitOptions options)
        {
            VisitContext = new VisitContext()
            {
                Options = options,
            };
            VisitContext.VisitFactory = new VisitFactory(VisitContext);
        }

        public Node VisitAccessor(Accessor accessor)
        {
            throw new NotImplementedException();
        }

        public Node VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitArraySpecifier(ArraySpecifier arraySpecifier)
        {
            throw new NotImplementedException();
        }

        public Node VisitAsExpression(AsExpression asExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitAttribute(ICSharpCode.Decompiler.CSharp.Syntax.Attribute attribute)
        {
            throw new NotImplementedException();
        }

        public Node VisitAttributeSection(AttributeSection attributeSection)
        {
            throw new NotImplementedException();
        }

        public Node VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitBlockStatement(BlockStatement blockStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitBreakStatement(BreakStatement breakStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitCaseLabel(CaseLabel caseLabel)
        {
            throw new NotImplementedException();
        }

        public Node VisitCastExpression(CastExpression castExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitCatchClause(CatchClause catchClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitCheckedExpression(CheckedExpression checkedExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitCheckedStatement(CheckedStatement checkedStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Node VisitComposedType(ComposedType composedType)
        {
            throw new NotImplementedException();
        }

        public Node VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitConstraint(Constraint constraint)
        {
            throw new NotImplementedException();
        }

        public Node VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitConstructorInitializer(ConstructorInitializer constructorInitializer)
        {
            throw new NotImplementedException();
        }

        public Node VisitContinueStatement(ContinueStatement continueStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitCSharpTokenNode(CSharpTokenNode cSharpTokenNode)
        {
            throw new NotImplementedException();
        }

        public Node VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitDirectionExpression(DirectionExpression directionExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitDocumentationReference(DocumentationReference documentationReference)
        {
            throw new NotImplementedException();
        }

        public Node VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitErrorNode(AstNode errorNode)
        {
            throw new NotImplementedException();
        }

        public Node VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitFixedStatement(FixedStatement fixedStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer)
        {
            throw new NotImplementedException();
        }

        public Node VisitForeachStatement(ForeachStatement foreachStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitForStatement(ForStatement forStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitFunctionPointerType(FunctionPointerType functionPointerType)
        {
            throw new NotImplementedException();
        }

        public Node VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitGotoStatement(GotoStatement gotoStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitIdentifier(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public Node VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitInterpolatedStringExpression(InterpolatedStringExpression interpolatedStringExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitInterpolatedStringText(InterpolatedStringText interpolatedStringText)
        {
            throw new NotImplementedException();
        }

        public Node VisitInterpolation(Interpolation interpolation)
        {
            throw new NotImplementedException();
        }

        public Node VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitIsExpression(IsExpression isExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitLabelStatement(LabelStatement labelStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitLocalFunctionDeclarationStatement(LocalFunctionDeclarationStatement localFunctionDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitLockStatement(LockStatement lockStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitMemberType(MemberType memberType)
        {
            throw new NotImplementedException();
        }

        public Node VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitNamedExpression(NamedExpression namedExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitNullNode(AstNode nullNode)
        {
            throw new NotImplementedException();
        }

        public Node VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitOutVarDeclarationExpression(OutVarDeclarationExpression outVarDeclarationExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitPatternPlaceholder(AstNode placeholder, Pattern pattern)
        {
            throw new NotImplementedException();
        }

        public Node VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitPreProcessorDirective(PreProcessorDirective preProcessorDirective)
        {
            throw new NotImplementedException();
        }

        public Node VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitPrimitiveType(PrimitiveType primitiveType)
        {
            throw new NotImplementedException();
        }

        public Node VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryExpression(QueryExpression queryExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryFromClause(QueryFromClause queryFromClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryGroupClause(QueryGroupClause queryGroupClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryJoinClause(QueryJoinClause queryJoinClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryLetClause(QueryLetClause queryLetClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryOrderClause(QueryOrderClause queryOrderClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryOrdering(QueryOrdering queryOrdering)
        {
            throw new NotImplementedException();
        }

        public Node VisitQuerySelectClause(QuerySelectClause querySelectClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitQueryWhereClause(QueryWhereClause queryWhereClause)
        {
            throw new NotImplementedException();
        }

        public Node VisitReturnStatement(ReturnStatement returnStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitSimpleType(SimpleType simpleType)
        {
            throw new NotImplementedException();
        }

        public Node VisitSizeOfExpression(SizeOfExpression sizeOfExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitStackAllocExpression(StackAllocExpression stackAllocExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitSwitchSection(SwitchSection switchSection)
        {
            throw new NotImplementedException();
        }

        public Node VisitSwitchStatement(SwitchStatement switchStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            var visitor = VisitContext.VisitFactory.GetVisitor(nameof(SyntaxTree)) as ICILVisitor<SyntaxTree>;
            return syntaxTree.AcceptVisitor(visitor);
        }

        public Node VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitThrowExpression(ThrowExpression throwExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitThrowStatement(ThrowStatement throwStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitTupleExpression(TupleExpression tupleExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitTupleType(TupleAstType tupleType)
        {
            throw new NotImplementedException();
        }

        public Node VisitTupleTypeElement(TupleTypeElement tupleTypeElement)
        {
            throw new NotImplementedException();
        }

        public Node VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitUncheckedExpression(UncheckedExpression uncheckedExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitUncheckedStatement(UncheckedStatement uncheckedStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression)
        {
            throw new NotImplementedException();
        }

        public Node VisitUnsafeStatement(UnsafeStatement unsafeStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            throw new NotImplementedException();
        }

        public Node VisitUsingStatement(UsingStatement usingStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitVariableInitializer(VariableInitializer variableInitializer)
        {
            throw new NotImplementedException();
        }

        public Node VisitWhileStatement(WhileStatement whileStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement)
        {
            throw new NotImplementedException();
        }

        public Node VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement)
        {
            throw new NotImplementedException();
        }
    }
}
