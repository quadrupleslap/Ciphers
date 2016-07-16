Public Module Encoding
    Public LOWER As String = "abcdefghijklmnopqrstuvwxyz"
    Public UPPER As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Public KEYS() As Key = {
        Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J, Key.K, Key.L, Key.M,
        Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T, Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z
    }

    Dim encoded
    Public Function CaesarEncode(rot As Integer, inp As String)
        rot = (rot Mod UPPER.Length + UPPER.Length) Mod UPPER.Length
        Dim out(inp.Length - 1) As Char

        For i = 0 To inp.Length - 1
            Dim c = inp(i)
            If LOWER.Contains(c) Then
                Dim original = LOWER.IndexOf(c)
                Dim encoded = (original + rot) Mod LOWER.Length
                out(i) = LOWER(encoded)
            ElseIf UPPER.Contains(c) Then
                Dim original = UPPER.IndexOf(c)
                Dim encoded = (original + rot) Mod UPPER.Length
                out(i) = UPPER(encoded)
            Else
                out(i) = c
            End If
        Next i

        Return New String(out)
    End Function

    Public Function CaesarDecode(rot As Integer, inp As String)
        Return CaesarEncode(-rot, inp)
    End Function
End Module
