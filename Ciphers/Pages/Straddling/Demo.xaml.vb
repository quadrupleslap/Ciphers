Class StraddlingDemo
    Sub UpdateEncoded() Handles KeyBox.TextChanged, N1.ValueChanged, N2.ValueChanged, PlainBox.TextChanged
        If Not IsInitialized Then Return
        CipheredBox.Text = StraddlingEncode(KeyBox.Text, N1.Value, N2.Value, PlainBox.Text)
    End Sub
End Class
