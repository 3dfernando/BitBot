<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBitWatch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblPing = New System.Windows.Forms.Label()
        Me.lblProcTime = New System.Windows.Forms.Label()
        Me.cmbBaseSymbol = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BGUpdateData = New System.ComponentModel.BackgroundWorker()
        Me.dgvSymbols = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbShowInfo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtString = New System.Windows.Forms.TextBox()
        CType(Me.dgvSymbols, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(543, 30)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 58)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblPing
        '
        Me.lblPing.AutoSize = True
        Me.lblPing.Location = New System.Drawing.Point(196, 9)
        Me.lblPing.Name = "lblPing"
        Me.lblPing.Size = New System.Drawing.Size(57, 13)
        Me.lblPing.TabIndex = 3
        Me.lblPing.Text = "Ping Time:"
        Me.lblPing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProcTime
        '
        Me.lblProcTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProcTime.AutoSize = True
        Me.lblProcTime.Location = New System.Drawing.Point(718, -18)
        Me.lblProcTime.Name = "lblProcTime"
        Me.lblProcTime.Size = New System.Drawing.Size(88, 13)
        Me.lblProcTime.TabIndex = 4
        Me.lblProcTime.Text = "Processing Time:"
        Me.lblProcTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbBaseSymbol
        '
        Me.cmbBaseSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBaseSymbol.FormattingEnabled = True
        Me.cmbBaseSymbol.Location = New System.Drawing.Point(94, 6)
        Me.cmbBaseSymbol.Name = "cmbBaseSymbol"
        Me.cmbBaseSymbol.Size = New System.Drawing.Size(96, 21)
        Me.cmbBaseSymbol.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Base Symbol:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BGUpdateData
        '
        '
        'dgvSymbols
        '
        Me.dgvSymbols.AllowUserToAddRows = False
        Me.dgvSymbols.AllowUserToOrderColumns = True
        Me.dgvSymbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSymbols.Location = New System.Drawing.Point(36, 133)
        Me.dgvSymbols.Name = "dgvSymbols"
        Me.dgvSymbols.Size = New System.Drawing.Size(602, 377)
        Me.dgvSymbols.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Show Info:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbShowInfo
        '
        Me.cmbShowInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShowInfo.FormattingEnabled = True
        Me.cmbShowInfo.Location = New System.Drawing.Point(94, 33)
        Me.cmbShowInfo.Name = "cmbShowInfo"
        Me.cmbShowInfo.Size = New System.Drawing.Size(96, 21)
        Me.cmbShowInfo.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Tag = ""
        Me.Label3.Text = "Sell:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(2, 135)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Tag = ""
        Me.Label4.Text = "Buy:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtString
        '
        Me.txtString.Location = New System.Drawing.Point(666, 17)
        Me.txtString.Multiline = True
        Me.txtString.Name = "txtString"
        Me.txtString.Size = New System.Drawing.Size(361, 492)
        Me.txtString.TabIndex = 12
        '
        'frmBitWatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1036, 522)
        Me.Controls.Add(Me.txtString)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbShowInfo)
        Me.Controls.Add(Me.dgvSymbols)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbBaseSymbol)
        Me.Controls.Add(Me.lblProcTime)
        Me.Controls.Add(Me.lblPing)
        Me.Controls.Add(Me.Button1)
        Me.Name = "frmBitWatch"
        Me.Text = "Market Watch"
        CType(Me.dgvSymbols, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents lblPing As Label
    Friend WithEvents lblProcTime As Label
    Friend WithEvents cmbBaseSymbol As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BGUpdateData As System.ComponentModel.BackgroundWorker
    Friend WithEvents dgvSymbols As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbShowInfo As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtString As TextBox
End Class
