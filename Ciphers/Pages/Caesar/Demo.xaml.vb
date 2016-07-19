Class CaesarDemo
    Private Modding As Boolean = False

    Sub New()
        InitializeComponent()

        For i = 0 To UPPER.Length - 1
            Dim plain = New Label With {.Content = UPPER(i)}
            Grid.SetColumn(plain, i)
            PlainGrid.Children.Add(plain)

            PlainGrid.ColumnDefinitions.Add(New ColumnDefinition With {
                .Width = New GridLength(1, GridUnitType.Star)
            })
        Next

        ComponentModel.DependencyPropertyDescriptor.
            FromProperty(ActualWidthProperty, GetType(Grid)).
            AddValueChanged(AlphabetTable, AddressOf Hoi)

        TryShift()
    End Sub

    Protected Overrides Sub Finalize()
        ComponentModel.DependencyPropertyDescriptor.
            FromProperty(ActualWidthProperty, GetType(Grid)).
            RemoveValueChanged(AlphabetTable, AddressOf Hoi)
        MyBase.Finalize()
    End Sub

    Sub MoveCursor(c As Char)
        If UPPER.Contains(c) Then
            TableCursor.BeginAnimation(
                MarginProperty,
                New Animation.ThicknessAnimation(
                    New Thickness(
                        151 + UPPER.IndexOf(c) * AlphabetTable.ActualWidth / UPPER.Length,
                        81.39, 0, 0),
                    New Duration(TimeSpan.FromSeconds(0.15)),
                    Animation.FillBehavior.HoldEnd),
                Animation.HandoffBehavior.SnapshotAndReplace)
            TableCursor.Background.BeginAnimation(
                TileBrush.ViewboxProperty,
                New Animation.RectAnimation(
                    New Rect(UPPER.IndexOf(c) / UPPER.Length, 0.0, 1.0, 1.0),
                    New Duration(TimeSpan.FromSeconds(0.15)),
                    Animation.FillBehavior.HoldEnd),
                Animation.HandoffBehavior.SnapshotAndReplace)
        End If
    End Sub

    Sub TryShift()
        If Not IsInitialized Then Return

        GridPan.BeginAnimation(
            TileBrush.ViewportProperty,
            New Animation.RectAnimation(
                New Rect(-ShiftBox.Value / UPPER.Length, 0.0, 1.0, 1.0),
                New Duration(TimeSpan.FromSeconds(0.25)),
                Animation.FillBehavior.HoldEnd),
            Animation.HandoffBehavior.SnapshotAndReplace)
    End Sub

    Sub Hoi()
        TableCursor.Width = AlphabetTable.ActualWidth / UPPER.Length
    End Sub

    Sub ShiftChanged() Handles ShiftBox.ValueChanged
        TryShift()
        UpdateCiphered()
    End Sub

    Sub UpdateCiphered() Handles PlainBox.TextChanged
        If Not Modding Then
            Modding = True
            CipheredBox.Text = CaesarEncode(ShiftBox.Value, PlainBox.Text)
            Modding = False
        End If
    End Sub

    Sub UpdatePlain() Handles CipheredBox.TextChanged
        If Not Modding Then
            Modding = True
            PlainBox.Text = CaesarDecode(ShiftBox.Value, CipheredBox.Text)
            Modding = False
        End If
    End Sub

    Private Sub PlainInput(sender As Object, e As KeyEventArgs) Handles PlainBox.KeyDown
        If KEYS.Contains(e.Key) Then
            MoveCursor(UPPER(Array.IndexOf(KEYS, e.Key)))
        End If
    End Sub

    Private Sub EncodedInput(sender As Object, e As KeyEventArgs) Handles CipheredBox.KeyDown
        If KEYS.Contains(e.Key) Then
            MoveCursor(CaesarDecode(ShiftBox.Value, UPPER(Array.IndexOf(KEYS, e.Key))))
        End If
    End Sub
End Class
