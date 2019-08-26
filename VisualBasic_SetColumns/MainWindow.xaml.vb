Imports [Shared].Globals

Class MainWindow
    Sub New()
        InitializeComponent()

        dg.ItemsSource = PersonList

        SetAnonymousType()
    End Sub

    Sub SetAnonymousType()
        dg.SetColumns(Function(p As Person) New With {
            .Number = p.ID,
            .Last = p.LastName,
            .First = p.FirstName
        })
    End Sub

    Private Sub btnAnonymousType_Click(sender As Object, e As RoutedEventArgs) Handles btnAnonymousType.Click
        SetAnonymousType()
    End Sub

    Private Sub btnArray_Click(sender As Object, e As RoutedEventArgs) Handles btnArray.Click
        dg.SetColumns(Function(p As Person) {p.FirstName, p.LastName, p.DateOfBirth})
    End Sub

    Private Sub btnSingleProperty_Click(sender As Object, e As RoutedEventArgs) Handles btnSingleProperty.Click
        dg.SetColumns(Function(p As Person) p.Email)
    End Sub

    Private Sub btnParameter_Click(sender As Object, e As RoutedEventArgs) Handles btnParameter.Click
        dg.SetColumns(Function(p As Person) p)
    End Sub

    Private Sub btnStringFormat_Click(sender As Object, e As RoutedEventArgs) Handles btnStringFormat.Click
        dg.SetColumns(Function(p As Person) New With {
            p.ID,
            p.LastName,
            p.FirstName,
            .UnformattedBirthDate = p.DateOfBirth,
            .FormattedBirthDate = $"{p.DateOfBirth:D}"
        })
    End Sub
End Class
