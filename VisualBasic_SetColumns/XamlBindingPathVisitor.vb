Imports System.Reflection
Imports System.Linq.Expressions

Class XamlBindingPathVisitor
    Inherits ExpressionVisitor

    Shared ReadOnly StringFormats As HashSet(Of MethodInfo) = GetType(String).GetMethods().
        Where(Function(x)
                  If x.Name <> "Format" Then Return False
                  Dim parameters = x.GetParameters
                  Return parameters.Length = 2 AndAlso parameters.First.ParameterType = GetType(String)
              End Function).
        ToHashSet

    Private isInitialized As Boolean
    Private current As (Path As String, StringFormat As String)
    Public Last As (Path As String, StringFormat As String)

    Private isBindablePath As Boolean = True

    Public Overrides Function Visit(node As Expression) As Expression
        Dim ret As Expression = Nothing
        If ExecIfType(Of LambdaExpression)(node, Function(lexpr)
                                                     ret = Visit(lexpr.Body)
                                                 End Function) Then Return ret

        ' https://stackoverflow.com/a/34794447/111794
        Dim firstVisit = Not isInitialized
        If (firstVisit) Then
            current.Path = ""
            isInitialized = True
        End If

        ' A MethodCallExpression representing a static method call will have its Expression property set to null
        ' Also, String.Format may be taking a ConstantExpression
        If node IsNot Nothing AndAlso TypeOf node IsNot ConstantExpression Then
            isBindablePath =
                TypeOf node Is MemberExpression OrElse
                TypeOf node Is ParameterExpression OrElse
                (TypeOf node Is UnaryExpression AndAlso node.NodeType.In(ExpressionType.Convert, ExpressionType.ConvertChecked))
            ' TODO how can we separate between any ParameterExpression and the ParameterExpression defined by the current lambda?

            If firstVisit Then
                isBindablePath = isBindablePath OrElse
                    ExecIfType(Of MethodCallExpression)(node, Function(mcexpr) StringFormats.Contains(mcexpr.Method))
            End If
        End If

        ' https://stackoverflow.com/questions/36737463/stop-traversal-with-expressionvisitor
        If Not (isBindablePath OrElse firstVisit) Then Return node

        ret = MyBase.Visit(node)

        If firstVisit Then
            If isBindablePath Then
                Last = (current.Path.Substring(1), current.StringFormat)
            Else
                Last = (Nothing, Nothing)
            End If
            current = (Nothing, Nothing)
            isBindablePath = True
            isInitialized = False
        End If

        Return ret
    End Function

    Protected Overrides Function VisitMember(node As MemberExpression) As Expression
        Dim ret = MyBase.VisitMember(node)
        current.Path += "." + node.Member.Name
        Return ret
    End Function

    Protected Overrides Function VisitMethodCall(node As MethodCallExpression) As Expression
        Dim ret = MyBase.VisitMethodCall(node)

        ' We're assuming this is a call to String.Format; all others should be excluded in the Visit override

        isBindablePath = isBindablePath AndAlso node.Arguments.Count = 2 AndAlso ExecIfType(Of ConstantExpression)(node.Arguments(0), Function(cexpr)
                                                                                                                                          current.StringFormat = CStr(cexpr.Value)
                                                                                                                                      End Function)

        Return ret
    End Function

End Class
