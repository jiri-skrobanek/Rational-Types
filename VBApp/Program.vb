Imports RationalTypes

Module Program

    Sub Main(args As String())
        Dim R As Rational
        Dim Exponent As Object
        R = New Rational(-5, 4)
        Exponent = 13
        Console.WriteLine(R.Pow(Exponent))
        Exponent = -2
        Console.WriteLine(R.Pow(Exponent))
    End Sub

End Module