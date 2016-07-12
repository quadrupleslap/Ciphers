Class CipherWheel
    Sub Print()
        Dim imgUri = New Uri("pack://application:,,,/Images/Printouts/cipher-wheel.png", UriKind.Absolute)
        Dim printer = DirectCast(Windows.Application.Current, Application).Printer
        printer.PrintImage(imgUri)
    End Sub
End Class
