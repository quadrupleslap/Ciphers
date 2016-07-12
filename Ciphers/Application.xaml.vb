Class Application
    Public Printer As New ImagePrinter

    Private Sub Ready() Handles Me.Startup
        If Not Printer.ShowDialog() Then
            Shutdown()
        End If
    End Sub
End Class
