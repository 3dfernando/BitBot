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
        Me.components = New System.ComponentModel.Container()
        Me.lblPing = New System.Windows.Forms.Label()
        Me.BGUpdateData = New System.ComponentModel.BackgroundWorker()
        Me.lstMarket = New System.Windows.Forms.ListView()
        Me.Threshold = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblPingTotal = New System.Windows.Forms.Label()
        CType(Me.Threshold, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblPing
        '
        Me.lblPing.AutoSize = True
        Me.lblPing.Location = New System.Drawing.Point(6, 16)
        Me.lblPing.Name = "lblPing"
        Me.lblPing.Size = New System.Drawing.Size(57, 13)
        Me.lblPing.TabIndex = 3
        Me.lblPing.Text = "Ping Time:"
        Me.lblPing.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BGUpdateData
        '
        '
        'lstMarket
        '
        Me.lstMarket.Location = New System.Drawing.Point(12, 12)
        Me.lstMarket.Name = "lstMarket"
        Me.lstMarket.Size = New System.Drawing.Size(802, 520)
        Me.lstMarket.TabIndex = 10
        Me.lstMarket.UseCompatibleStateImageBehavior = False
        '
        'Threshold
        '
        Me.Threshold.DecimalPlaces = 2
        Me.Threshold.Increment = New Decimal(New Integer() {1, 0, 0, 131072})
        Me.Threshold.Location = New System.Drawing.Point(9, 72)
        Me.Threshold.Name = "Threshold"
        Me.Threshold.Size = New System.Drawing.Size(72, 20)
        Me.Threshold.TabIndex = 11
        Me.Threshold.Value = New Decimal(New Integer() {5, 0, 0, 65536})
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Show Results Above:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(86, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(15, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "%"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Enabled = True
        Me.tmrUpdate.Interval = 5000
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPingTotal)
        Me.GroupBox1.Controls.Add(Me.lblPing)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Threshold)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(820, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 146)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        '
        'lblPingTotal
        '
        Me.lblPingTotal.AutoSize = True
        Me.lblPingTotal.Location = New System.Drawing.Point(6, 35)
        Me.lblPingTotal.Name = "lblPingTotal"
        Me.lblPingTotal.Size = New System.Drawing.Size(57, 13)
        Me.lblPingTotal.TabIndex = 14
        Me.lblPingTotal.Text = "Ping Time:"
        Me.lblPingTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmBitWatch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1048, 544)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lstMarket)
        Me.Name = "frmBitWatch"
        Me.Text = "Market Watch"
        CType(Me.Threshold, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblPing As Label
    Friend WithEvents BGUpdateData As System.ComponentModel.BackgroundWorker
    Friend WithEvents lstMarket As ListView
    Friend WithEvents Threshold As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents tmrUpdate As Timer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents lblPingTotal As Label
End Class
