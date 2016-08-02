Class VigenereDemo
    Private Modding As Boolean = False

    Sub Ready() Handles Me.Loaded
        CalcTutorialButton()
    End Sub

    Sub CalcTutorialButton()
        If PlainBox.Text.Length = 0 Or KeyNormalize(KeyBox.Text).Count = 0 Then
            TutorialButton.IsEnabled = False
        Else
            TutorialButton.IsEnabled = True
        End If
    End Sub

    Sub KeyChanged() Handles KeyBox.TextChanged
        UpdateCiphered()
    End Sub

    Sub UpdateCiphered() Handles PlainBox.TextChanged
        If Not Modding Then
            Modding = True
            CipheredBox.Text = VigenereEncode(KeyBox.Text, PlainBox.Text)
            Modding = False
        End If
        CalcTutorialButton()
    End Sub

    Sub UpdatePlain() Handles CipheredBox.TextChanged
        If Not Modding Then
            Modding = True
            PlainBox.Text = VigenereDecode(KeyBox.Text, CipheredBox.Text)
            Modding = False
        End If
        CalcTutorialButton()
    End Sub

    Sub ShowTutorial() Handles TutorialButton.Click
        Dim win = New Modal(Window.GetWindow(Me), New Tutorial(KeyBox.Text, PlainBox.Text))
        win.Width = Window.GetWindow(Me).Width * 0.8
        win.Height = Window.GetWindow(Me).Height * 0.8
        win.Title = "Tutorial"
        win.ShowDialog()
    End Sub
End Class
