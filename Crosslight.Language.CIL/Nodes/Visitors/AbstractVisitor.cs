using Crosslight.API.Nodes;
using ICSharpCode.Decompiler.CSharp.Syntax;
using ICSharpCode.Decompiler.CSharp.Syntax.PatternMatching;

namespace Crosslight.Language.CIL.Nodes.Visitors
{
    public abstract class AbstractVisitor<T> : IAstVisitor<Node>, ICILVisitor<T> where T : AstNode
    {
        protected VisitContext Context { get; }
        protected CILVisitOptions VisitOptions => Context?.Options;
        public AbstractVisitor(VisitContext context)
        {
            Context = context;
        }

        public bool CanBeVisited(AstNode node)
        {
            return node is T;
        }

        public abstract Node Visit(AstNode node);

        public abstract Node Visit(T node);

        public virtual Node VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitAsExpression(AsExpression asExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCastExpression(CastExpression castExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCheckedExpression(CheckedExpression checkedExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitDirectionExpression(DirectionExpression directionExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitInterpolatedStringExpression(InterpolatedStringExpression interpolatedStringExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitIsExpression(IsExpression isExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitNamedExpression(NamedExpression namedExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitOutVarDeclarationExpression(OutVarDeclarationExpression outVarDeclarationExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitSizeOfExpression(SizeOfExpression sizeOfExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitStackAllocExpression(StackAllocExpression stackAllocExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitThrowExpression(ThrowExpression throwExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTupleExpression(TupleExpression tupleExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUncheckedExpression(UncheckedExpression uncheckedExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryExpression(QueryExpression queryExpression)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryFromClause(QueryFromClause queryFromClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryLetClause(QueryLetClause queryLetClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryWhereClause(QueryWhereClause queryWhereClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryJoinClause(QueryJoinClause queryJoinClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryOrderClause(QueryOrderClause queryOrderClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryOrdering(QueryOrdering queryOrdering)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQuerySelectClause(QuerySelectClause querySelectClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitQueryGroupClause(QueryGroupClause queryGroupClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitAttribute(Attribute attribute)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitAttributeSection(AttributeSection attributeSection)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitBlockStatement(BlockStatement blockStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitBreakStatement(BreakStatement breakStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCheckedStatement(CheckedStatement checkedStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitContinueStatement(ContinueStatement continueStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitFixedStatement(FixedStatement fixedStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitForeachStatement(ForeachStatement foreachStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitForStatement(ForStatement forStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitGotoStatement(GotoStatement gotoStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitLabelStatement(LabelStatement labelStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitLockStatement(LockStatement lockStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitReturnStatement(ReturnStatement returnStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitSwitchStatement(SwitchStatement switchStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitSwitchSection(SwitchSection switchSection)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCaseLabel(CaseLabel caseLabel)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitThrowStatement(ThrowStatement throwStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCatchClause(CatchClause catchClause)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUncheckedStatement(UncheckedStatement uncheckedStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUnsafeStatement(UnsafeStatement unsafeStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitUsingStatement(UsingStatement usingStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitLocalFunctionDeclarationStatement(LocalFunctionDeclarationStatement localFunctionDeclarationStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitWhileStatement(WhileStatement whileStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitAccessor(Accessor accessor)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitConstructorInitializer(ConstructorInitializer constructorInitializer)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitVariableInitializer(VariableInitializer variableInitializer)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitSimpleType(SimpleType simpleType)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitMemberType(MemberType memberType)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTupleType(TupleAstType tupleType)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTupleTypeElement(TupleTypeElement tupleTypeElement)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitFunctionPointerType(FunctionPointerType functionPointerType)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitComposedType(ComposedType composedType)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitArraySpecifier(ArraySpecifier arraySpecifier)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitPrimitiveType(PrimitiveType primitiveType)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitPreProcessorDirective(PreProcessorDirective preProcessorDirective)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitDocumentationReference(DocumentationReference documentationReference)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitConstraint(Constraint constraint)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitCSharpTokenNode(CSharpTokenNode cSharpTokenNode)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitIdentifier(Identifier identifier)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitInterpolation(Interpolation interpolation)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitInterpolatedStringText(InterpolatedStringText interpolatedStringText)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitNullNode(AstNode nullNode)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitErrorNode(AstNode errorNode)
        {
            throw new System.NotImplementedException();
        }

        public virtual Node VisitPatternPlaceholder(AstNode placeholder, Pattern pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}
