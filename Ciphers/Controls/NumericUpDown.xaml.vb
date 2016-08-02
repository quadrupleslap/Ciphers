Public Class NumericUpDown
    Private Shared Sub Warn(title, msg)
        MessageBox.Show(msg, title, MessageBoxButton.OK, MessageBoxImage.Warning)
    End Sub

    Public Property Modulo As Integer? = New Integer?
    Public Property Max As Integer? = New Integer?
    Public Property Min As Integer? = New Integer?

    Private _Value As Integer = 0
    Public Property Value As Integer
        Get
            Return _Value
        End Get
        Set(val As Integer)
            If (Max.HasValue And val > Max) Or (Min.HasValue And val < Min) Then
                Throw New ArgumentOutOfRangeException
            End If

            If Modulo.HasValue Then
                val = (val Mod Modulo + Modulo) Mod Modulo
            End If

            Modding = True
            Box.Text = val.ToString()
            Modding = False

            _Value = val
            If IsInitialized Then
                RaiseEvent ValueChanged(val)
            End If
        End Set
    End Property

    Public Event ValueChanged(newValue As String)

    Sub New()
        InitializeComponent()
        Value = Value
    End Sub

    Private Sub Inc()
        If (Max.HasValue And Value + 1 > Max) Then
            Warn("Out of Range", "Sorry, that's as far as it goes.")
            Return
        End If

        Value += 1
    End Sub

    Private Sub Dec()
        If (Min.HasValue And Value - 1 < Min) Then
            Warn("Out of Range", "Sorry, it can't go any lower.")
            Return
        End If

        Value -= 1
    End Sub

    Private Modding As Boolean = False
    Private Sub InputChanged()
        If Modding Then
            Return
        End If

        BorderThickness = New Thickness(2)

        Dim val = -1
        If Integer.TryParse(Box.Text, val) Then
            If Modulo.HasValue Then
                val = (val Mod Modulo + Modulo) Mod Modulo
            End If

            If Not ((Max.HasValue And val > Max) Or (Min.HasValue And val < Min)) Then
                BorderThickness = New Thickness(0)
                _Value = val
                If IsInitialized Then
                    RaiseEvent ValueChanged(val)
                End If
            End If
        End If
    End Sub

    Private Sub Unfocused()

        Try
            Dim val = Integer.Parse(Box.Text)
            If (Max.HasValue And val > Max) Then
                Warn("Invalid Input", "Please enter a smaller number.")
            ElseIf (Min.HasValue And val < Min) Then
                Warn("Invalid Input", "Please enter a larger number.")
            Else
                Value = val
                Return
            End If
        Catch ex As FormatException
            Warn("Invalid Input", "Please enter a whole number.")
        Catch ex As OverflowException
            Warn("Invalid Input", "The given number is way too big.")
        End Try

        Box.Text = Value
    End Sub
End Class
