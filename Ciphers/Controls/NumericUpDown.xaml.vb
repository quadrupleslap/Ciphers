Public Class NumericUpDown
    Public Property Modulo As Integer? = New Integer?

    Private _Value As Integer = 0
    Public Property Value As Integer
        Get
            Return _Value
        End Get
        Set(val As Integer)
            If Modulo.HasValue Then
                val = val Mod Modulo
                val = (val Mod Modulo + Modulo) Mod Modulo
            End If

            Modding = True
            Box.Text = val.ToString()
            Modding = False

            _Value = val
            RaiseEvent ValueChanged(val)
        End Set
    End Property

    Public Event ValueChanged(newValue As String)

    Sub New()
        InitializeComponent()
        Value = Value
    End Sub

    Private Sub Inc()
        Value += 1
    End Sub

    Private Sub Dec()
        Value -= 1
    End Sub

    Private Modding As Boolean = False
    Private Sub InputChanged()
        If Modding Then
            Return
        End If

        Dim val = -1
        If Integer.TryParse(Box.Text, val) Then
            If Modulo.HasValue Then
                val = val Mod Modulo
                val = (val Mod Modulo + Modulo) Mod Modulo
            End If

            _Value = val
            RaiseEvent ValueChanged(val)
        End If
    End Sub

    Private Sub Unfocused()
        Dim int = -1
        If Integer.TryParse(Box.Text, int) Then
            Value = int
        Else
            Box.Text = Value
            MessageBox.Show("Please enter a whole number.", "Invalid input.", MessageBoxButton.OK, MessageBoxImage.Warning)
        End If
    End Sub
End Class
