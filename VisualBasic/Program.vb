Imports System
Imports System.Linq.Expressions
Imports System.Linq.Expressions.Expression
Imports ExpressionToString
Imports [Shared]
Imports [Shared].Methods
Imports Simple.OData.Client

Module Program
    Sub Main(args As String())
        RunSample(Sub() SimpleODataClient())
        RunSample(Sub() HourMessage())
    End Sub

    ''' <summary>Constructing OData web requests using expression trees and the Simple.OData.Client library</summary>
    Sub SimpleODataClient()
        Dim client = New ODataClient("https://services.odata.org/v4/TripPinServiceRW/")

        Dim command = client.For(Of Person).
            Filter(Function(x) x.Trips.Any(Function(y) y.Budget > 3000)).
            Top(2).
            Select(Function(x) New With {x.LastName, x.FirstName})

        Dim commandText = Task.Run(Function() command.GetCommandTextAsync()).Result
        Console.WriteLine(commandText)
        Console.WriteLine()

        Dim people = Task.Run(Function() command.FindEntriesAsync).Result
        For Each p In people
            p.Write()
        Next
    End Sub

    ''' <summary>
    ''' Builds the expression tree of a lambda with the following code<br/><br/>
    ''' Dim msg As String = "Hello"<br/>
    ''' If DateTime.Now.Hour > 18 Then msg = "Good night"<br/>
    ''' Console.WriteLine(msg)
    ''' </summary>
    Sub HourMessage()
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

        Console.WriteLine(expr.ToString("Visual Basic"))
    End Sub
End Module
