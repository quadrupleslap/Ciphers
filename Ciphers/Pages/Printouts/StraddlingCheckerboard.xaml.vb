Class StraddlingCheckerboard
    Public Key As String = ""
    Public N1 As Integer = 0
    Public N2 As Integer = 1

    Sub Print()
        Dim printer = DirectCast(Windows.Application.Current, Application).Printer
        Dim page = New Grid
        Dim styles As ResourceDictionary = Resources("PrintStyles")

        Dim rows = Math.Floor(printer.PrintableAreaHeight / 145),
            cols = Math.Floor(printer.PrintableAreaWidth / 328)

        page.Height = 145 * rows
        page.Width = 328 * cols

        For i = 1 To rows
            page.RowDefinitions.Add(New RowDefinition With {.Height = New GridLength(145)})
        Next

        For i = 1 To cols
            page.ColumnDefinitions.Add(New ColumnDefinition With {.Width = New GridLength(328)})
        Next

        For col = 1 To cols
            For row = 1 To rows
                Dim cell = New Grid With {.Height = 125, .Width = 308, .Margin = New Thickness(9)}
                PopulateGrid(cell)
                cell.Resources = styles

                For c = 1 To 4
                    cell.RowDefinitions.Add(New RowDefinition)
                Next

                For i = 1 To 11
                    cell.ColumnDefinitions.Add(New ColumnDefinition)
                Next

                Dim container = New Border With {
                    .Child = cell,
                    .BorderBrush = Brushes.LightGray,
                    .BorderThickness = New Thickness(1)
                }

                Grid.SetColumn(container, col - 1)
                Grid.SetRow(container, row - 1)
                page.Children.Add(container)
            Next
        Next

        printer.PrintVisual(page, "")
    End Sub

    Sub Ready() Handles Me.Loaded
        PopulateGrid(Preview)
    End Sub

    Sub UpdateKey() Handles KeyBox.TextChanged
        Key = KeyBox.Text
        PopulateGrid(Preview)
    End Sub

    Sub UpdateN1() Handles N1Box.ValueChanged
        N1 = N1Box.Value
        PopulateGrid(Preview)
    End Sub

    Sub UpdateN2() Handles N2Box.ValueChanged
        N2 = N2Box.Value
        PopulateGrid(Preview)
    End Sub

    Sub PopulateGrid(x As Grid)
        If Not IsInitialized Then Return
        x.Children.Clear()

        Dim keyStr = Key.ToUpper
        Dim nMin = Math.Min(N1, N2)
        Dim nMax = Math.Max(N1, N2)
        If nMin = nMax Then
            If nMin = 9 Then
                nMin = 0
            Else
                nMax = nMax + 1
            End If
        End If

        For i = 0 To 9
            Dim cell = New Label With {.Content = i}
            Grid.SetColumn(cell, i + 1)
            Grid.SetRow(cell, 0)
            x.Children.Add(cell)
        Next

        For i = 0 To 3
            If i = 2 Then
                Dim cell = New Label With {.Content = nMin}
                Grid.SetColumn(cell, 0)
                Grid.SetRow(cell, 2)
                x.Children.Add(cell)

                cell = New Label
                Grid.SetColumn(cell, nMin + 1)
                Grid.SetRow(cell, 0)
                x.Children.Add(cell)
            ElseIf i = 3 Then
                Dim cell = New Label With {.Content = nMax}
                Grid.SetColumn(cell, 0)
                Grid.SetRow(cell, 3)
                x.Children.Add(cell)

                cell = New Label
                Grid.SetColumn(cell, nMax + 1)
                Grid.SetRow(cell, 0)
                x.Children.Add(cell)
            Else
                Dim cell = New Label
                Grid.SetColumn(cell, 0)
                Grid.SetRow(cell, i)
                x.Children.Add(cell)
            End If
        Next

        Dim nKey As New List(Of Char) ' Normalized Key
        For i = 0 To keyStr.Length - 1
            If UPPER.Contains(keyStr(i)) And Not nKey.Contains(keyStr(i)) Then
                nKey.Add(keyStr(i))
            End If
        Next i

        For i = 0 To UPPER.Length - 1
            If Not nKey.Contains(UPPER(i)) Then
                nKey.Add(UPPER(i))
            End If
        Next i

        Dim table As New Hashtable
        For i = 0 To nKey.Count - 1
            Dim row As Integer, col As Integer
            If i < nMin Then
                table(nKey(i)) = i
                row = 0 : col = i
            ElseIf i < nMax - 1 Then
                table(nKey(i)) = i + 1
                row = 0 : col = i + 1
            ElseIf i < 8 Then
                table(nKey(i)) = i + 2
                row = 0 : col = i + 2
            ElseIf i < 18 Then
                table(nKey(i)) = nMin * 10 + i - 8
                row = 1 : col = i - 8
            ElseIf i < 28 Then
                table(nKey(i)) = nMax * 10 + i - 18
                row = 2 : col = i - 18
            Else
                Throw New Exception("Unreachable!")
            End If

            Dim cell = New Label With {.Content = nKey(i)}
            Grid.SetColumn(cell, col + 1)
            Grid.SetRow(cell, row + 1)
            x.Children.Add(cell)
        Next

        For i = 9 To 10
            Dim cell = New Label
            Grid.SetColumn(cell, i)
            Grid.SetRow(cell, 3)
            x.Children.Add(cell)
        Next
    End Sub
End Class
