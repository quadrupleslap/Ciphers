Public Class ImagePrinter
    Inherits PrintDialog

    Sub PrintImage(img As BitmapImage)
        Dim vis = New Image With {
            .Width = PrintableAreaWidth,
            .Height = PrintableAreaHeight,
            .Source = img,
            .Stretch = Stretch.Uniform
        }

        PrintVisual(vis, "")
    End Sub

    Sub PrintImage(uri As Uri)
        Dim img = New BitmapImage(uri)

        If img.Width > img.Height Then
            img = New BitmapImage
            img.BeginInit()
            img.Rotation = Rotation.Rotate90
            img.UriSource = uri
            img.EndInit()
        End If

        PrintImage(img)
    End Sub
End Class
