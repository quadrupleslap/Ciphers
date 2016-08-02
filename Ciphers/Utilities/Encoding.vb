Public Module Encoding
    Public LOWER As String = "abcdefghijklmnopqrstuvwxyz"
    Public UPPER As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Public KEYS() As Key = {
        Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G, Key.H, Key.I, Key.J, Key.K, Key.L, Key.M,
        Key.N, Key.O, Key.P, Key.Q, Key.R, Key.S, Key.T, Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z
    }

    Dim encoded
    Public Function CaesarEncode(rot As Integer, inp As String) As String
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

    Public Function CaesarDecode(rot As Integer, inp As String) As String
        Return CaesarEncode(-rot, inp)
    End Function

    Public Function KeyNormalize(keyStr As String) As Char()
        keyStr = keyStr.ToUpper
        Dim key As New List(Of Char)
        For i = 0 To keyStr.Length - 1
            If UPPER.Contains(keyStr(i)) Then
                key.Add(keyStr(i))
            End If
        Next i
        Return key.ToArray
    End Function

    Public Function VigenereEncode(keyStr As String, inp As String) As String
        Dim key = KeyNormalize(keyStr)
        If key.Count = 0 Then Return inp
        Dim out(inp.Length - 1) As Char

        For i = 0 To inp.Length - 1
            Dim c = inp(i)
            If UPPER.Contains(c) Then
                Dim rot = UPPER.IndexOf(key(i Mod key.Count))
                Dim original = UPPER.IndexOf(c)
                Dim encoded = (original + rot) Mod UPPER.Length
                out(i) = UPPER(encoded)
            ElseIf LOWER.Contains(c) Then
                Dim rot = UPPER.IndexOf(key(i Mod key.Count))
                Dim original = LOWER.IndexOf(c)
                Dim encoded = (original + rot) Mod UPPER.Length
                out(i) = LOWER(encoded)
            Else
                out(i) = c
            End If
        Next i

        Return New String(out)
    End Function

    Public Function VigenereDecode(keyStr As String, inp As String) As String
        Dim key = KeyNormalize(keyStr)
        If key.Count = 0 Then Return inp
        Dim out(inp.Length - 1) As Char

        For i = 0 To inp.Length - 1
            Dim c = inp(i)
            If UPPER.Contains(c) Then
                Dim rot = UPPER.IndexOf(key(i Mod key.Count))
                Dim original = UPPER.IndexOf(c)
                Dim encoded = (original + UPPER.Length - rot) Mod UPPER.Length
                out(i) = UPPER(encoded)
            ElseIf LOWER.Contains(c) Then
                Dim rot = UPPER.IndexOf(key(i Mod key.Count))
                Dim original = LOWER.IndexOf(c)
                Dim encoded = (original + UPPER.Length - rot) Mod UPPER.Length
                out(i) = LOWER(encoded)
            Else
                out(i) = c
            End If
        Next i

        Return New String(out)
    End Function

    Public Function StraddlingEncode(keyStr As String, n1 As Integer, n2 As Integer, inp As String) As String
        Debug.Assert(0 <= n1 < 10 And 0 <= n2 < 10)

        keyStr = keyStr.ToUpper
        inp = inp.ToUpper

        Dim nMin = Math.Min(n1, n2)
        Dim nMax = Math.Max(n1, n2)
        If nMin = nMax Then
            If nMin = 9 Then
                nMin = 0
            Else
                nMax = nMax + 1
            End If
        End If

        n1 = nMin
        n2 = nMax

        Dim key As New List(Of Char)
        For i = 0 To keyStr.Length - 1
            If UPPER.Contains(keyStr(i)) And Not key.Contains(keyStr(i)) Then
                key.Add(keyStr(i))
            End If
        Next i

        For i = 0 To UPPER.Length - 1
            If Not key.Contains(UPPER(i)) Then
                key.Add(UPPER(i))
            End If
        Next i

        Dim table As New Hashtable
        For i = 0 To key.Count - 1
            If i < n1 Then
                table(key(i)) = i
            ElseIf i < n2 - 1 Then
                table(key(i)) = i + 1
            ElseIf i < 8 Then
                table(key(i)) = i + 2
            ElseIf i < 18 Then
                table(key(i)) = n1 * 10 + i - 8
            ElseIf i < 28 Then
                table(key(i)) = n2 * 10 + i - 18
            Else
                Throw New Exception("Unreachable!")
            End If
        Next

        Dim out As New List(Of Integer)
        For i = 0 To inp.Length - 1
            If table.ContainsKey(inp(i)) Then
                out.Add(table(inp(i)))
            End If
        Next

        Return String.Join("", out.ToArray)
    End Function

    Public Function StraddlingDecode(key As String, keyN1 As Integer, keyN2 As Integer, inp As String) As String
        Throw New NotImplementedException
    End Function
End Module
