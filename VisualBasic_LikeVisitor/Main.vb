Imports System.Data.Entity
Imports System.Data.SQLite
Imports [Shared].Globals

Module Main
    Sub Main()
        RunSample(Sub() ErrorUsingLike())
        RunSample(Sub() VB_LikeVisitor())
        RunSample(Sub() CSharp_LikeVisitor())
    End Sub

    Sub ErrorUsingLike()
        Dim conn = New SQLiteConnection($"Data Source={DbPath}")
        Using ctx = New PeopleContext(conn)
            Dim personSource = ctx.Persons

            Dim qry = personSource.Where(Function(x) x.FirstName Like "*e*i*")
            Try
                For Each person In qry
                    person.Write(True)
                Next
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using
    End Sub

    Sub VB_LikeVisitor()
        Dim conn = New SQLiteConnection($"Data Source={DbPath}")
        Using ctx = New PeopleContext(conn)
            Dim personSource = ctx.Persons

            Dim expr As Expression(Of Func(Of Person, Boolean)) = Function(x) x.FirstName Like "*e*i*"
            Dim visitor = New LikeVisitor
            expr = CType(visitor.Visit(expr), Expression(Of Func(Of Person, Boolean)))

            Dim qry = personSource.Where(expr)
            For Each person In qry
                person.Write(True)
            Next
        End Using
    End Sub

    Sub CSharp_LikeVisitor()
        Dim conn = New SQLiteConnection($"Data Source={DbPath}")
        Using ctx = New PeopleContext(conn)
            Dim personSource = ctx.Persons

            Dim expr As Expression(Of Func(Of Person, Boolean)) = Function(x) x.FirstName Like "*e*i*"
            Dim visitor = New CSharp_LikeVisitor.LikeVisitor
            expr = CType(visitor.Visit(expr), Expression(Of Func(Of Person, Boolean)))

            Dim qry = personSource.Where(expr)
            For Each person In qry
                person.Write(True)
            Next
        End Using
    End Sub
End Module
