Module APIFunctions
    Public Class TickerData
        Public BidPrices() As Price
        Public AskPrices() As Price
        Public isFrozen As Boolean
    End Class

    Public Class Price
        Public Value As Double
        Public Quantity As Double
    End Class

End Module
