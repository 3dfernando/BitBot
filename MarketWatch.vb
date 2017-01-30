Imports System.ComponentModel
Imports Newtonsoft.Json

Public Class frmBitWatch
    Public SymbolList As New List(Of String)
    Public SymbolPairValidity(,) As Boolean
    Dim webClient As New System.Net.WebClient
    Const Address_Prefix As String = "https://poloniex.com/public?command=returnOrderBook&currencyPair="
    Const Address_Suffix As String = "&depth=10"

    Public UpdatedPriceArray(,) As ConversionData 'Holds the array of updated prices.
    Public DisplayInfo As Long
    Public BaseCurrency As Long
    Public ReceivedString As String
    Public MinPing, MaxPing As Long


#Region "SUBS"
    Private Sub frmBitWatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Init_Symbols()

        'Fills the combo box of the symbols
        cmbBaseSymbol.Items.Clear()
        For Each Symbol As String In SymbolList
            cmbBaseSymbol.Items.Add(Symbol)
        Next

        'Selects the first symbol
        cmbBaseSymbol.SelectedIndex = 0

        'Updates the table of symbols
        DrawTable()

        'Updates the prices
        If Not BGUpdateData.IsBusy Then BGUpdateData.RunWorkerAsync()

        'Fills up the ShowInfo Combobox
        cmbShowInfo.Items.Clear()
        cmbShowInfo.Items.Add("Best Order")
        cmbShowInfo.Items.Add("Triangulation Result")
        cmbShowInfo.SelectedIndex = 0

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

        'Tests the address validity by sending strings to Poloniex
        Dim Address As String
        Dim result As String
        ReDim SymbolPairValidity(SymbolList.Count - 1, SymbolList.Count - 1)

        For I As Long = 0 To SymbolList.Count - 1
            For J As Long = 0 To SymbolList.Count - 1
                Address = Address_Prefix & SymbolList.Item(I) & "_" & SymbolList.Item(J) & Address_Suffix
                result = webClient.DownloadString(Address)

                SymbolPairValidity(I, J) = (InStr(1, result, "error", CompareMethod.Text) = 0) 'If no error is found, the symbol is valid

            Next
        Next
    End Sub

    Private Sub cmbBaseSymbol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBaseSymbol.SelectedIndexChanged
        'Manages the change of base symbol updating the table
        BaseCurrency = cmbBaseSymbol.SelectedIndex
        DisplayInfo = cmbShowInfo.SelectedIndex
        If Not BGUpdateData.IsBusy Then BGUpdateData.RunWorkerAsync()
    End Sub

    Private Sub DrawTable()
        'Will update the columns and rows of the table
        dgvSymbols.Rows.Clear()
        dgvSymbols.Columns.Clear()
        'dgvSymbols.AllowUserToAddRows = False
        'dgvSymbols.AllowUserToDeleteRows = False
        'dgvSymbols.AllowUserToOrderColumns = False
        'dgvSymbols.Enabled = False

        'Adds columns
        For J As Integer = 0 To SymbolList.Count - 1
            dgvSymbols.Columns.Add(SymbolList.Item(J), SymbolList.Item(J))
            dgvSymbols.Columns(J).Width = 75
        Next
        dgvSymbols.RowHeadersWidth = 75

        'Adds rows
        For I As Integer = 0 To SymbolList.Count - 1
            'Rows
            dgvSymbols.Rows.Add()
            dgvSymbols.Rows(I).HeaderCell.Value = SymbolList.Item(I)
        Next
    End Sub

    Private Sub BGUpdateData_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BGUpdateData.DoWork
        'Enables the ping requests to be performed in the background

        Dim OrdersIJ As OrderBook
        Dim OrdersBase As OrderBook

        MinPing = Long.MaxValue
        MaxPing = Long.MinValue

        ReDim UpdatedPriceArray(SymbolList.Count - 1, SymbolList.Count - 1)
        For I As Integer = 0 To SymbolList.Count - 1
            For J As Integer = 0 To SymbolList.Count - 1
                UpdatedPriceArray(I, J) = New ConversionData
            Next
        Next

        For I As Integer = 0 To SymbolList.Count - 1
            For J As Integer = 0 To SymbolList.Count - 1
                If SymbolPairValidity(I, J) Then
                    'This symbol pair can be asked in the server. 
                    OrdersIJ = RecoverOrderbook(I, J)
                    UpdatedPriceArray(I, J).Price = OrdersIJ.Ask(0, 0)
                    UpdatedPriceArray(I, J).Volume = OrdersIJ.Ask(0, 1)

                    UpdatedPriceArray(J, I).Price = 1 / OrdersIJ.Bid(0, 0)
                    UpdatedPriceArray(J, I).Volume = OrdersIJ.Bid(0, 1)

                    'Computes min and max pings
                    If MinPing > OrdersIJ.Ping_ms Then MinPing = OrdersIJ.Ping_ms
                    If MaxPing < OrdersIJ.Ping_ms Then MaxPing = OrdersIJ.Ping_ms
                End If
            Next
        Next

        Dim ProfitArray(,) As Double
        ReDim ProfitArray(SymbolList.Count - 1, SymbolList.Count - 1)
        Dim FirstConversion As Double
        Dim LastConversion As Double

        If DisplayInfo = 1 Then 'Triangulation Result <<<<<<<<<<<<<<<<<<<<<<not working>>>>>>>>>>>>>>>>>>>>>>>>>>
            For I As Integer = 0 To SymbolList.Count - 1
                For J As Integer = 0 To SymbolList.Count - 1
                    If SymbolPairValidity(I, J) And I <> BaseCurrency And J <> BaseCurrency Then
                        'I, J combinations
                        If SymbolPairValidity(BaseCurrency, I) Then
                            OrdersBase = RecoverOrderbook(BaseCurrency, I)
                            FirstConversion = OrdersBase.Ask(0, 0)
                        ElseIf SymbolPairValidity(I, BaseCurrency) Then
                            OrdersBase = RecoverOrderbook(I, BaseCurrency)
                            FirstConversion = 1 / OrdersBase.Bid(0, 0)
                        End If

                        If SymbolPairValidity(J, BaseCurrency) Then
                            OrdersBase = RecoverOrderbook(J, BaseCurrency)
                            LastConversion = OrdersBase.Ask(0, 0)
                        ElseIf SymbolPairValidity(BaseCurrency, J) Then
                            OrdersBase = RecoverOrderbook(BaseCurrency, J)
                            LastConversion = 1 / OrdersBase.Bid(0, 0)
                        End If


                        UpdatedPriceArray(I, J).TriangulationResult = FirstConversion * UpdatedPriceArray(I, J).Price * LastConversion

                        'J,I combinations
                        If SymbolPairValidity(BaseCurrency, J) Then
                            OrdersBase = RecoverOrderbook(BaseCurrency, J)
                            FirstConversion = OrdersBase.Ask(0, 0)
                        ElseIf SymbolPairValidity(J, BaseCurrency) Then
                            OrdersBase = RecoverOrderbook(J, BaseCurrency)
                            FirstConversion = 1 / OrdersBase.Bid(0, 0)
                        End If

                        If SymbolPairValidity(I, BaseCurrency) Then
                            OrdersBase = RecoverOrderbook(I, BaseCurrency)
                            LastConversion = OrdersBase.Ask(0, 0)
                        ElseIf SymbolPairValidity(BaseCurrency, I) Then
                            OrdersBase = RecoverOrderbook(BaseCurrency, I)
                            LastConversion = 1 / OrdersBase.Bid(0, 0)
                        End If


                        UpdatedPriceArray(J, I).TriangulationResult = FirstConversion * UpdatedPriceArray(J, I).Price * LastConversion


                    End If
                Next
            Next

        End If

    End Sub

    Private Sub BGUpdateData_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BGUpdateData.RunWorkerCompleted
        'Updates the DGV
        For I As Integer = 0 To SymbolList.Count - 1
            For J As Integer = 0 To SymbolList.Count - 1
                dgvSymbols.Item(I, J).Value = "-"
            Next
        Next

        For I As Integer = 0 To SymbolList.Count - 1
            For J As Integer = 0 To SymbolList.Count - 1
                'If SymbolPairValidity(I, J) And Not UpdatedPriceArray(I, J).Price = 0 Then
                dgvSymbols.Item(I, J).Value = UpdatedPriceArray(I, J).TriangulationResult
                dgvSymbols.Item(J, I).Value = UpdatedPriceArray(J, I).TriangulationResult

                dgvSymbols.Item(I, J).ToolTipText = "Volume: " & Trim(Str(UpdatedPriceArray(I, J).Volume)) & vbCrLf & "Price: " & Trim(Str(UpdatedPriceArray(I, J).Price))
                dgvSymbols.Item(J, I).ToolTipText = "Volume: " & Trim(Str(UpdatedPriceArray(J, I).Volume)) & vbCrLf & "Price: " & Trim(Str(UpdatedPriceArray(J, I).Price))
                'End If
            Next
        Next

        'Updates the ping
        lblPing.Text = "Min Ping: " & Trim(Str(MinPing)) & " ms; Max Ping: " & Trim(Str(MaxPing)) & " ms"
    End Sub

#End Region


#Region "Auxiliary Functions"

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


        'Deserializes the string and parses it onto the Orderbook object
        Dim SymbolPairTrades As JSONObject = JsonConvert.DeserializeObject(Of JSONObject)(result)
        ParsedOrderbook.Ask = ParseString(SymbolPairTrades.asks)
        ParsedOrderbook.Bid = ParseString(SymbolPairTrades.bids)
        ParsedOrderbook.isFrozen = (SymbolPairTrades.isFrozen = "1")
        ParsedOrderbook.seq = SymbolPairTrades.seq
        ParsedOrderbook.Ping_ms = pingTime.ElapsedMilliseconds

        RecoverOrderbook = ParsedOrderbook

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
        Public Price As Double
        Public Volume As Double
        Public TriangulationResult As Double
    End Class


#End Region

    Private Sub cmbShowInfo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbShowInfo.SelectedIndexChanged
        BaseCurrency = cmbBaseSymbol.SelectedIndex
        DisplayInfo = cmbShowInfo.SelectedIndex
        If Not BGUpdateData.IsBusy Then BGUpdateData.RunWorkerAsync()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        BaseCurrency = cmbBaseSymbol.SelectedIndex
        DisplayInfo = cmbShowInfo.SelectedIndex
        If Not BGUpdateData.IsBusy Then BGUpdateData.RunWorkerAsync()
    End Sub


End Class
