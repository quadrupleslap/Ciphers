Public Class Tutorial
    Private Key As String
    Private NormalizedKey As String
    Private Plain As String
    Private Encoded As String
    Private CurStep As Integer = 1

    Sub New(key As String, plain As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Key = key
        Me.Plain = plain

        Dim keyStr = key
        keyStr = keyStr.ToUpper
        Dim k As New List(Of Char)
        For i = 0 To keyStr.Length - 1
            If UPPER.Contains(keyStr(i)) Then
                k.Add(keyStr(i))
            End If
        Next i

        NormalizedKey = New String(k.ToArray)
        Encoded = VigenereEncode(key, plain)
    End Sub

    Sub Ready() Handles Me.Loaded
        LoadStep()
    End Sub

    Sub NextStep() Handles Right.Click
        CurStep += 1
        LoadStep()
    End Sub

    Sub PrevStep() Handles Left.Click
        CurStep -= 1
        LoadStep()
    End Sub

    Sub LoadStep()
        If CurStep = 1 Then
            Left.IsEnabled = False
            Right.IsEnabled = True

            LeftText.Inlines.Clear()
            LeftText.Inlines.AddRange({
                New Run With {.Text = "Key:", .FontSize = 24}, New LineBreak,
                New Run(Key), New LineBreak, New LineBreak,
                New Run With {.Text = "Text:", .FontSize = 24}, New LineBreak,
                New Run(Plain)
            })

            RightText.Inlines.Clear()
            RightText.Inlines.AddRange({
                New Run With {.Text = $"Step {CurStep}:", .FontSize = 24}, New LineBreak,
                New Run("We start by normalizing the key. This means that we remove everything that isn't in the alphabet and make everything uppercase, so that the encoding is process much simpler. Effectively:"), New LineBreak, New LineBreak,
                New Run(Key), New LineBreak, New LineBreak,
                New Run("Becomes:"), New LineBreak, New LineBreak,
                New Run(NormalizedKey)
            })
        ElseIf CurStep < Plain.Length + 2 Then
            Dim i = CurStep - 2
            Dim j = i Mod NormalizedKey.Length
            Dim c = Char.ToUpper(Plain(i))
            Dim keyChar = NormalizedKey(j)

            Left.IsEnabled = True
            Right.IsEnabled = True

            LeftText.Inlines.Clear()
            LeftText.Inlines.AddRange({
                New Run With {.Text = "Key:", .FontSize = 24}, New LineBreak,
                New Run(NormalizedKey.Substring(0, j)),
                New Underline(New Run(NormalizedKey(j))) With {.Foreground = Brushes.Tomato},
                New Run(NormalizedKey.Substring(j + 1)),
                New LineBreak, New LineBreak,
                New Run With {.Text = "Text:", .FontSize = 24}, New LineBreak,
                New Run(Encoded.Substring(0, i)) With {.Foreground = Brushes.DarkCyan},
                New Underline(New Run(Plain(i))) With {.Foreground = Brushes.Tomato},
                New Run(Plain.Substring(i + 1))
            })

            RightText.Inlines.Clear()
            If UPPER.Contains(c) Then
                Dim res = (UPPER.IndexOf(keyChar) + UPPER.IndexOf(c)) Mod UPPER.Length
                Dim resChar = UPPER(res)

                RightText.Inlines.AddRange({
                    New Run With {.Text = $"Step {CurStep}:", .FontSize = 24}, New LineBreak,
                    New Run(If(i <> 0 And j <> 0, "We move forward to the next letter." & vbNewLine & vbNewLine, "")),
                    New Run(If(i <> 0 And j = 0, "We move forward to the next letter. Since we reached the end of the key, we go back to the start." & vbNewLine & vbNewLine, "")),
                    New Run($"The current letter in the key is {keyChar}, the {ToOrdinal(UPPER.IndexOf(keyChar))} letter of the alphabet."),
                    New Run($" The current letter in the message is {c}, the {ToOrdinal(UPPER.IndexOf(c))} letter of the alphabet."),
                    New Run(" Adding the two together, we get:" & vbNewLine & vbNewLine),
                    New Run($"{UPPER.IndexOf(keyChar)} + {UPPER.IndexOf(c)} = {res} mod {UPPER.Length}"), New LineBreak, New LineBreak,
                    New Run($"The {ToOrdinal(res)} letter of the alphabet is {resChar}. So we'd encode this letter as {resChar}.")
                })
            Else
                RightText.Inlines.AddRange({
                    New Run With {.Text = $"Step {CurStep}:", .FontSize = 24}, New LineBreak,
                    New Run($"'{c}' isn't in the alphabet, so it stays the same.")
                })
            End If
        Else
            Left.IsEnabled = True
            Right.IsEnabled = False

            LeftText.Inlines.Clear()
            LeftText.Inlines.AddRange({
                New Run With {.Text = "Key:", .FontSize = 24}, New LineBreak,
                New Run(NormalizedKey), New LineBreak, New LineBreak,
                New Run With {.Text = "Text:", .FontSize = 24}, New LineBreak,
                New Run(Encoded)
            })

            RightText.Inlines.Clear()
            RightText.Inlines.AddRange({
                New Run With {.Text = $"Step {CurStep}:", .FontSize = 24}, New LineBreak,
                New Run("And that's it! The message is completely encoded, and all that's left is to give it to someone who knows the key!")
            })
        End If
    End Sub

    Shared Function ToOrdinal(i As Integer) As String
        Dim j As Integer = i Mod 10
        Dim k As Integer = i Mod 100

        If j = 1 And k <> 11 Then
            Return $"{i}st"
        End If

        If j = 2 And k <> 12 Then
            Return $"{i}nd"
        End If

        If j = 3 And k <> 13 Then
            Return $"{i}rd"
        End If

        Return $"{i}th"
    End Function
End Class
