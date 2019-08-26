Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions.Expression

Module Extensions
    ' We need the TResult type parameter so the compiler can recognize the second parameter as an expression tree
    <Extension> Sub SetColumns(Of TSource, TResult)(dg As DataGrid, selector As Expression(Of Func(Of TSource, TResult)))
        dg.Columns.Clear()

        Dim fields = ParseFields(selector)

        Dim visitor As New XamlBindingPathVisitor

        fields.ForEachT(Sub(name, expr)
                            visitor.Visit(expr)
                            If visitor.Last.Path Is Nothing Then
                                Throw New InvalidOperationException("Unable to generate binding path from expression")
                            End If

                            Dim col As New DataGridTextColumn With {
                                .Header = If(name.IsNullOrWhitespace, visitor.Last.Path, name),
                                .Binding = New Binding(visitor.Last.Path) With {
                                    .StringFormat = visitor.Last.StringFormat
                                }
                            }

                            dg.Columns.Add(col)
                        End Sub)
    End Sub
End Module

Module Functions
    ''' <summary>
    ''' Parses an expression into names and subexpressions.<br/>
    ''' The following expressions are handled:<br/>
    ''' * an array; the individual elements are the subexpressions<br/>
    ''' * an anonymous type; the initializers are the subexpressions, and the names are the initialized members<br/>
    ''' * a scalar-returning expression<br/>
    ''' * the parameter itself; creates a property name / subexpression pair for each property
    ''' If the expression is an anonymous type, the corresponding name for each expression is the initialized member. For other expressions, the name is a string rendering of the expression, based on the language parameter.
    ''' </summary>
    ''' <param name="language">Can be <code>"C#"</code> or <code>"Visual Basic"</code></param>
    Function ParseFields(Of TSource, TResult)(fieldsExpression As Expression(Of Func(Of TSource, TResult)), Optional Language As String = "Visual Basic") As List(Of (name As String, expr As Expression))
        ParseFields = Nothing 'otherwise the compiler complains, because there;s no exhaustiveness checking on Select in VB.NET

        Dim formatter = Language
        If formatter <> "Visual Basic" Then formatter = "C#"

        Dim body = fieldsExpression.Body
        Select Case True

            ' array with elements
            Case ExecIfType(Of NewArrayExpression)(body, Function(newArrayExpr)
                                                             If body.NodeType <> ExpressionType.NewArrayInit Then Return False
                                                             ParseFields = newArrayExpr.Expressions.Select(Function(x) ("", x)).ToList
                                                         End Function)

            ' anonyous type
            Case ExecIfType(Of NewExpression)(body, Function(newExpr)
                                                        If Not newExpr.Type.IsAnonymous Then Return False
                                                        ParseFields = newExpr.Constructor.GetParameters.Select(Function(x) x.Name).Zip(newExpr.Arguments).ToList
                                                    End Function)

                ' the parameter itself
            Case body Is fieldsExpression.Parameters(0)
                ParseFields = body.Type.GetProperties.Select(Function(x) (x.Name, CType(PropertyOrField(body, x.Name), Expression))).ToList

            Case body.Type.IsScalar
                ParseFields = {("", body)}.ToList

            Case Else
                Throw New ArgumentException("Unhandled expression")

        End Select

    End Function
End Module