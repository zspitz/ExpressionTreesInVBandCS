Imports System
Imports System.Linq.Expressions
Imports System.Linq.Expressions.Expression
Imports ExpressionToString
Imports [Shared]
Imports [Shared].Methods
Imports Simple.OData.Client
Imports System.Console
Imports System.Reflection.BindingFlags
Imports System.Reflection

Module Program
    Sub Main(args As String())
        RunSample(Sub() Figure2_ExpressionsAsObjects())
        RunSample(Sub() Figure3_NodetypeValueName())
        RunSample(Sub() Figure5_BlocksAssignmentsStatements())
        RunSample(Sub() Figure6_QueryableWhere())
        RunSample(Sub() Figure7_SimpleODataClient())
        RunSample(Sub() Inline_GetMethod())
    End Sub

    Private ReadOnly simpleExpression As BinaryExpression =
        Equal(
            Add(
                Parameter(GetType(Integer), "n"),
                Constant(42)
            ),
            Constant(27)
        )

    ''' <summary>Shows the objects in the expression tree for n + 42 == 27, using object and collection initializers</summary>
    Sub Figure2_ExpressionsAsObjects()
        WriteLine(simpleExpression.ToString("Object notation", "Visual Basic"))
    End Sub

    ''' <summary>Shows the NodeType, Value and Name properties for n + 42 == 27; useful for visualizing the expression tree's structure</summary>
    Sub Figure3_NodetypeValueName()
        WriteLine(simpleExpression.ToString("Textual tree", "Visual Basic"))
    End Sub

    ''' <summary>
    ''' Builds the expression tree of a lambda with the following code<br/><br/>
    ''' <code>
    ''' Dim msg = "Hello"<br/>
    ''' If DateTime.Now.Hour > 18 Then msg = "Good night"<br/>
    ''' Console.WriteLine(msg)
    ''' </code>
    ''' </summary>
    Sub Figure5_BlocksAssignmentsStatements()
        Dim msg = Parameter(GetType(String), "msg") ' Dim msg As String
        Dim expr = Lambda( ' Function() ... End Function
            Block( ' The lambda contains multiple statements, so we need to wrap in a BlockExpression
                Assign(msg, Constant("Hello")), ' msg = "Hello"
                IfThen( ' If ... Then ... End If
                    GreaterThan( ' Date.Now.Hour > 18
                        MakeMemberAccess(
                            MakeMemberAccess(
                                Nothing,
                                GetType(Date).GetMember("Now").Single
                            ),
                            GetType(Date).GetMember("Hour").Single
                        ),
                        Constant(18)
                    ),
                    Assign(msg, Constant("Good night")) ' msg = "Good night"
                ),
                [Call]( ' Console.WriteLine(msg)
                    GetType(Console).GetMethod("WriteLine", {GetType(String)}),
                    msg
                )
            )
        )

        WriteLine(expr.ToString("Visual Basic"))
    End Sub

    ''' <summary>Shows an expression tree before and after the call to Queryable.Where</summary>
    Sub Figure6_QueryableWhere()
        Dim expressionBeforeWhere As Expression(Of Func(Of Person, Boolean)) = Function(p) p.LastName.StartsWith("D")
        WriteLine(expressionBeforeWhere.ToString("Textual tree", "Visual Basic"))
        WriteLine()

        Dim personSource = (New List(Of Person)).AsQueryable
        Dim qry = personSource.Where(expressionBeforeWhere)
        Dim expressionAfterWhere As Expression = qry.GetType().GetProperty("Expression", NonPublic Or Instance).GetValue(qry)
        WriteLine(expressionAfterWhere.ToString("Textual tree", "Visual Basic"))
    End Sub

    ''' <summary>Constructing OData web requests using expression trees and the Simple.OData.Client library</summary>
    Sub Figure7_SimpleODataClient()
        Dim client = New ODataClient("https://services.odata.org/v4/TripPinServiceRW/")

        Dim command = client.For(Of Person).
            Filter(Function(x) x.Trips.Any(Function(y) y.Budget > 3000)).
            Top(2).
            Select(Function(x) New With {x.LastName, x.FirstName})

        Dim commandText = Task.Run(Function() command.GetCommandTextAsync()).Result
        WriteLine(commandText)
        WriteLine()

        Dim people = Task.Run(Function() command.FindEntriesAsync).Result
        For Each p In people
            p.Write()
        Next
    End Sub

    ''' <summary>Use compiled expressions to target a specific MethodInfo, instead of reflection</summary>
    Sub Inline_GetMethod()
        Dim GetMethod As Func(Of Expression(Of Action), MethodInfo) = Function(expr) TryCast(expr.Body, MethodCallExpression)?.Method

        Dim mi = GetMethod(Sub() WriteLine())

        ' Without generics, this is simle enough to do with reflection:
        WriteLine(mi =
            GetType(Console).GetMethod("WriteLine", {})
        )

        ' But once generics are involved, it becomes much more complicated to get hold of a specific closed overload using reflection.
        ' You have to find the method whose name Is "Where" And whose second parameter's type is Expression<Func<T, bool>>, and not Expression<Func<T, int, bool>>
        ' Related: https : //github.com/dotnet/corefx/issues/16567 
        Dim q As IQueryable(Of Person) = Nothing
        mi = GetMethod(Sub() q.Where(Function(x) True))

        WriteLine(mi =
            GetType(Queryable).GetMethods().Single(Function(x)
                                                       If x.Name <> "Where" Then Return False
                                                       Return x.GetParameters(1).ParameterType.GetGenericArguments.Single.GetGenericArguments.Length = 2
                                                   End Function).MakeGenericMethod(GetType(Person))
        )
    End Sub
End Module
