Imports System.Data.Entity
Imports System.Reflection
Imports Microsoft.VisualBasic.CompilerServices

Public Class LikeVisitor
    Inherits ExpressionVisitor

    Shared ReadOnly LikeMethods() As MethodInfo = {
        GetType(LikeOperator).GetMethod("LikeString"),
        GetType(LikeOperator).GetMethod("LikeObject")
    }

    Shared ReadOnly DbFunctionsLike As MethodInfo = GetType(DbFunctions).GetMethod("Like", {GetType(String), GetType(String)})

    Protected Overrides Function VisitMethodCall(node As MethodCallExpression) As Expression
        ' Is this node using the LikeString or LikeObject method? If not, leave it alone.
        If node.Method.NotIn(LikeMethods) Then Return MyBase.VisitMethodCall(node)

        Dim argExpression = node.Arguments(1)

        ' Visual Basic inserts a conversion node whenever the types don't exactly match, but rather one inherits from  / implements the other
        ' This will happen here if the LikeObject method -- which takes an object as its' second parameter -- is used.
        Dim hasConversions As Boolean
        Dim patternExpression As Expression = Nothing
        argExpression.SansDerivedConvert.Unpack(hasConversions, patternExpression)

        ' We can only map the syntax from VB Like to SQL Like if the pattern is a constant expressions
        ' If the pattern is not a string, say a number or date, we're not going to replace
        ExecIfType(Of ConstantExpression)(patternExpression, Function(cexpr)
                                                                 If patternExpression.Type <> GetType(String) Then Return False

                                                                 Dim oldPattern = CType(cexpr.Value, String)
                                                                 Dim newPattern = oldPattern.Replace("*", "%")

                                                                 'TODO see C# implementation details

                                                                 patternExpression = Constant(newPattern)
                                                                 argExpression = If(
                                                                    hasConversions,
                                                                    Expression.Convert(patternExpression, argExpression.Type),
                                                                    patternExpression
                                                                )
                                                             End Function)

        Return [Call](
            DbFunctionsLike,
            Visit(node.Arguments(0)),
            Visit(patternExpression)
        )
    End Function
End Class
