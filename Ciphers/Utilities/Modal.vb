Public Class Modal
    Inherits Window

    Sub New(owner As Window, content As Object)
        Me.Owner = owner
        Me.Content = content
        WindowStyle = WindowStyle.SingleBorderWindow
        ResizeMode = ResizeMode.NoResize
        WindowStartupLocation = WindowStartupLocation.CenterScreen
    End Sub
End Class
