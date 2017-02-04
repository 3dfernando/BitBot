Imports System.ComponentModel
Imports Newtonsoft.Json

Public Class frmBitWatch
    Public SymbolList As New List(Of String)
    Public SymbolPairValidity(,) As Boolean
    Dim webClient As New System.Net.WebClient
    Const Address_Prefix As String = "https://poloniex.com/public?command=returnOrderBook&currencyPair="
    Const Address_Suffix As String = "&depth=10"

    Public UpdatedPriceArray(,) As ConversionData 'Holds the array of updated prices.
    'UpdatedPriceArray(A, B)
    'A = Index Of the currency being sold
    'B = Index Of the currency being bought
    'Result = Quantity of B you ended up with for each unity of A sold

    Public DisplayInfo As Long
    Public BaseCurrency As Long
    Public ReceivedString As String
    Public MinPing, MaxPing, TotalPing As Long


#Region "SUBS"
    Private Sub frmBitWatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init_Symbols()

        'Updates the prices
        If Not BGUpdateData.IsBusy Then BGUpdateData.RunWorkerAsync()

        'Loads columns on the listview
        lstMarket.View = View.Details
        lstMarket.FullRowSelect = True
        lstMarket.Items.Clear()
        lstMarket.Columns.Clear()
        lstMarket.Columns.Add("Date/Time", 100)
        lstMarket.Columns.Add("X1", 50)
        lstMarket.Columns.Add("X2", 50)
        lstMarket.Columns.Add("X3", 50)
        lstMarket.Columns.Add("X1X2", 120)
        lstMarket.Columns.Add("X2X3", 120)
        lstMarket.Columns.Add("X3X1", 120)
        lstMarket.Columns.Add("Result in X1", 130)


        DisplayInfo = 0
    End Sub



    Public Sub Init_Symbols()
        'Initializes the symbols in the system
        SymbolList.Clear()
        SymbolList.Add("USDT")
        SymbolList.Add("BTC")
        SymbolList.Add("ETH")
        SymbolList.Add("ETC")
        SymbolList.Add("XMR")
        SymbolList.Add("LTC")
        SymbolList.Add("DASH")
        SymbolList.Add("ZEC")
        SymbolList.Add("REP")
        SymbolList.Add("XRP")
        SymbolList.Add("NXT")
        SymbolList.Add("STR")
        SymbolList.Add("MAID")
        SymbolList.Add("BBR")
        SymbolList.Add("PASC")
        SymbolList.Add("FCT")
        SymbolList.Add("XEM")
        SymbolList.Add("BELA")
        SymbolList.Add("GAME")
        SymbolList.Add("XVC")
        SymbolList.Add("STRAT")
        SymbolList.Add("PINK")
        SymbolList.Add("DCR")

        'Tests the address validity by sending strings to Poloniex
        Dim Address As String
        Dim result As String
        ReDim SymbolPairValidity(SymbolList.Count - 1, SymbolList.Count - 1)

        For I As Long = 0 To SymbolList.Count - 1
            For J As Long = 0 To SymbolList.Count - 1
                Address = Address_Prefix & SymbolList.Item(I) & "_" & SymbolList.Item(J) & Address_Suffix
                result = webClient.DownloadString(Address)

                SymbolPairValidity(I, J) = (InStr(1, result, "error", CompareMethod.Text) = 0) Or IsNothing(result) 'If no error is found, the symbol is valid

            Next
        Next
    End Sub


    Private Sub BGUpdateData_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGUpdateData.DoWork
        'Enables the ping requests to be performed in the background

        Dim OrdersIJ As OrderBook

        MinPing = Long.MaxValue
        MaxPing = Long.MinValue
        TotalPing = 0

        ReDim UpdatedPriceArray(SymbolList.Count - 1, SymbolList.Count - 1)
        For I As Integer = 0 To SymbolList.Count - 1
            For J As Integer = 0 To SymbolList.Count - 1
                UpdatedPriceArray(I, J) = New ConversionData
            Next
        Next

        'UpdatedPriceArray(A, B)
        'A = Index Of the currency being sold
        'B = Index Of the currency being bought
        'Result = Quantity of B you ended up with for each unity of A sold

        For I As Integer = 0 To SymbolList.Count - 1
            For J As Integer = 0 To SymbolList.Count - 1
                If SymbolPairValidity(I, J) Then
                    'This symbol pair can be asked in the server. 
                    OrdersIJ = RecoverOrderbook(I, J)
                    UpdatedPriceArray(I, J).Result = 1 / OrdersIJ.Ask(0, 0)
                    UpdatedPriceArray(I, J).Volume = OrdersIJ.Ask(0, 1)

                    UpdatedPriceArray(J, I).Result = OrdersIJ.Bid(0, 0)
                    UpdatedPriceArray(J, I).Volume = OrdersIJ.Bid(0, 1)

                    'Computes min and max pings
                    If MinPing > OrdersIJ.Ping_ms Then MinPing = OrdersIJ.Ping_ms
                    If MaxPing < OrdersIJ.Ping_ms Then MaxPing = OrdersIJ.Ping_ms
                    TotalPing += OrdersIJ.Ping_ms
                End If
            Next
        Next


    End Sub

    Private Sub BGUpdateData_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGUpdateData.RunWorkerCompleted
        'Updates the DGV

        Dim X12, X23, X31 As Double
        Dim R As Double

        Dim Count As Long = 0
        Dim Line As String = ""
        Dim strFile As String = IO.Path.Combine(Environment.CurrentDirectory, "Log.csv")
        Dim fileExists As Boolean = IO.File.Exists(strFile)

        For I As Long = 0 To SymbolList.Count - 1
            For J As Long = 0 To SymbolList.Count - 1
                For K As Long = 0 To SymbolList.Count - 1
                    If Not (I = J Or J = K Or I = K) Then
                        R = Perform3Transactions(I, J, K, X12, X23, X31)
                        R = (R - 1) * 100

                        If R > (Threshold.Value) Then
                            Count += 1

                            If Count Mod 3 = 0 Then
                                lstMarket.Items.Add(DateTime.Now.ToString("dd/MM HH:mm:ss"))
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(SymbolList(I))
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(SymbolList(J))
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(SymbolList(K))
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(X12)
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(X23)
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(X31)
                                lstMarket.Items(lstMarket.Items.Count - 1).SubItems.Add(R)

                                Line = DateTime.Now.ToString("dd/MM HH:mm:ss") & ";" &
                                    SymbolList(I).ToString & ";" &
                                    SymbolList(J).ToString & ";" &
                                    SymbolList(K).ToString & ";" &
                                    X12.ToString & ";" &
                                    X23.ToString & ";" &
                                    X31.ToString & ";" &
                                    R.ToString

                                If fileExists Then
                                    Using SW As System.IO.StreamWriter = IO.File.AppendText(strFile)
                                        SW.WriteLine(Line)
                                    End Using
                                Else
                                    Using SW As System.IO.StreamWriter = IO.File.CreateText(strFile)
                                        SW.WriteLine(Line)
                                    End Using
                                End If


                            End If

                        End If
                    End If
                Next
            Next
        Next

        'Updates the ping
        lblPing.Text = "Min Ping: " & Trim(Str(MinPing)) & " ms; Max Ping: " & Trim(Str(MaxPing)) & " ms"
        lblPingTotal.Text = "Total Update Time: " & Trim(Str(TotalPing)) & " ms"
    End Sub

#End Region


#Region "Auxiliary Functions"

    Public Function Perform3Transactions(X1 As Long, X2 As Long, X3 As Long, ByRef X1X2 As Double, ByRef X2X3 As Double, ByRef X3X1 As Double) As Double
        'Performs a transaction selling X1 and buying X2, then
        'Selling X2 and buying X3 and
        'Selling X3 and buying X1 back.

        'UpdatedPriceArray(A, B)
        'A = Index Of the currency being sold
        'B = Index Of the currency being bought
        'Result = Quantity of B you ended up with for each unity of A sold


        'X1->X2
        X1X2 = UpdatedPriceArray(X1, X2).Result
        'X2->X3
        X2X3 = UpdatedPriceArray(X2, X3).Result
        'X3->X1
        X3X1 = UpdatedPriceArray(X3, X1).Result

        Perform3Transactions = X1X2 * X2X3 * X3X1

    End Function

    Public Function RecoverOrderbook(Symbol1Index As Long, Symbol2Index As Long) As OrderBook
        'This function will recover the orderbook for this symbol pair and put it into the structure orderbook
        Dim ParsedOrderbook As New OrderBook

        'Downloads the orderbook
        Dim Address As String
        Dim result As String

        Dim pingTime As New Stopwatch
        pingTime.Start()

        Address = Address_Prefix & SymbolList.Item(Symbol1Index) & "_" & SymbolList.Item(Symbol2Index) & Address_Suffix
        result = webClient.DownloadString(Address)

        pingTime.Stop()

        If Not IsNothing(result) Then
            'Deserializes the string and parses it onto the Orderbook object
            Dim SymbolPairTrades As JSONObject = JsonConvert.DeserializeObject(Of JSONObject)(result)
            ParsedOrderbook.Ask = ParseString(SymbolPairTrades.asks)
            ParsedOrderbook.Bid = ParseString(SymbolPairTrades.bids)
            ParsedOrderbook.isFrozen = (SymbolPairTrades.isFrozen = "1")
            ParsedOrderbook.seq = SymbolPairTrades.seq
            ParsedOrderbook.Ping_ms = pingTime.ElapsedMilliseconds

            RecoverOrderbook = ParsedOrderbook

        Else
            RecoverOrderbook = Nothing
        End If

    End Function

    Public Class OrderBook
        Public Ask(,) As Double 'Contains the order book ask offer list. (Price, Qty)
        Public Bid(,) As Double
        Public isFrozen As Boolean
        Public seq As Long
        Public Ping_ms As Long
    End Class

    Public Function ParseString(S As String(,)) As Double(,)
        'Will parse a string array into an array of doubles
        Dim tempArray As Double(,)
        ReDim tempArray(UBound(S, 1), UBound(S, 2))

        For I As Long = 0 To UBound(S, 1)
            For J As Long = 0 To UBound(S, 2)
                tempArray(I, J) = Val(S(I, J))
            Next
        Next

        ParseString = tempArray
    End Function


    Public Class JSONObject
        Public asks As String(,)
        Public bids As String(,)
        Public isFrozen As String
        Public seq As Long
    End Class


    Public Class ConversionData
        Public Result As Double
        Public Volume As Double
        Public TriangulationResult As Double
    End Class


#End Region

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        If Not BGUpdateData.IsBusy Then BGUpdateData.RunWorkerAsync()
    End Sub


End Class
