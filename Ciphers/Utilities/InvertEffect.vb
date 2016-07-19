Public Class InvertEffect
    Inherits Effects.ShaderEffect

    Private Shared _pixelShader As Effects.PixelShader = New Effects.PixelShader With {
        .UriSource = New Uri("pack://application:,,,/Resources/Invert.ps", UriKind.Absolute)
    }

    Public Shared ReadOnly InputProperty As DependencyProperty =
        RegisterPixelShaderSamplerProperty("Input", GetType(InvertEffect), 0)

    Public Property Input
        Get
            Return DirectCast(GetValue(InputProperty), Brush)
        End Get
        Set(value)
            SetValue(InputProperty, value)
        End Set
    End Property

    Sub New()
        PixelShader = _pixelShader
        UpdateShaderValue(InputProperty)
    End Sub
End Class
