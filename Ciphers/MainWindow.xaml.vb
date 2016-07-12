Class MainWindow
    Private Sub Ready() Handles Me.Initialized
        Me.Navigate(New Uri("Pages/Title.xaml", UriKind.RelativeOrAbsolute))
    End Sub
End Class
