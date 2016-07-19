Class MainWindow
    Private Sub Ready() Handles Me.Initialized
        Navigate(New Uri("Pages/Title.xaml", UriKind.RelativeOrAbsolute))
    End Sub
End Class
