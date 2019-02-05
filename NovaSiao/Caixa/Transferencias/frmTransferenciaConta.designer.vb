<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTransferenciaConta
    Inherits NovaSiao.frmModFinBorder

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtSaidaData = New Controles.MaskText_Data()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtValorPago = New Controles.Text_Monetario()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAcrescimo = New Controles.Text_Monetario()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSaidaValor = New System.Windows.Forms.Label()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.txtObservacao = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtContaCredito = New System.Windows.Forms.TextBox()
        Me.btnContaEscolher = New VIBlend.WinForms.Controls.vButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblFilial = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.VButton1 = New VIBlend.WinForms.Controls.vButton()
        Me.txtContaDebito = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.VButton2 = New VIBlend.WinForms.Controls.vButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.VButton3 = New VIBlend.WinForms.Controls.vButton()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblFilial)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Size = New System.Drawing.Size(477, 50)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label18, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblFilial, 0)
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(310, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(167, 50)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Transferências"
        '
        'txtSaidaData
        '
        Me.txtSaidaData.Location = New System.Drawing.Point(170, 221)
        Me.txtSaidaData.Mask = "00/00/0000"
        Me.txtSaidaData.Name = "txtSaidaData"
        Me.txtSaidaData.Size = New System.Drawing.Size(109, 27)
        Me.txtSaidaData.TabIndex = 6
        Me.txtSaidaData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 224)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 19)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Data do Débito:"
        '
        'txtValorPago
        '
        Me.txtValorPago.Location = New System.Drawing.Point(170, 254)
        Me.txtValorPago.Name = "txtValorPago"
        Me.txtValorPago.Size = New System.Drawing.Size(109, 27)
        Me.txtValorPago.TabIndex = 8
        Me.txtValorPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(118, 257)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 19)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Valor:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_30px
        Me.btnCancelar.Location = New System.Drawing.Point(246, 428)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(120, 48)
        Me.btnCancelar.TabIndex = 16
        Me.btnCancelar.Text = "&Fechar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(88, 290)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 19)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Comissão:"
        '
        'txtAcrescimo
        '
        Me.txtAcrescimo.Location = New System.Drawing.Point(170, 287)
        Me.txtAcrescimo.Name = "txtAcrescimo"
        Me.txtAcrescimo.Size = New System.Drawing.Size(109, 27)
        Me.txtAcrescimo.TabIndex = 10
        Me.txtAcrescimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(46, 327)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 19)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Valor a ser Pago:"
        '
        'lblSaidaValor
        '
        Me.lblSaidaValor.BackColor = System.Drawing.Color.Transparent
        Me.lblSaidaValor.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaidaValor.Location = New System.Drawing.Point(169, 323)
        Me.lblSaidaValor.Name = "lblSaidaValor"
        Me.lblSaidaValor.Size = New System.Drawing.Size(109, 25)
        Me.lblSaidaValor.TabIndex = 12
        Me.lblSaidaValor.Text = "R$ 0,00"
        Me.lblSaidaValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnQuitar
        '
        Me.btnQuitar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnQuitar.Image = Global.NovaSiao.My.Resources.Resources.dollar_currency_sign
        Me.btnQuitar.Location = New System.Drawing.Point(116, 428)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(120, 48)
        Me.btnQuitar.TabIndex = 15
        Me.btnQuitar.Text = "&Quitar"
        Me.btnQuitar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'txtObservacao
        '
        Me.txtObservacao.Location = New System.Drawing.Point(170, 364)
        Me.txtObservacao.Multiline = True
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(234, 54)
        Me.txtObservacao.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(74, 364)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 19)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Observação:"
        '
        'txtContaCredito
        '
        Me.txtContaCredito.Location = New System.Drawing.Point(170, 101)
        Me.txtContaCredito.Name = "txtContaCredito"
        Me.txtContaCredito.Size = New System.Drawing.Size(205, 27)
        Me.txtContaCredito.TabIndex = 3
        '
        'btnContaEscolher
        '
        Me.btnContaEscolher.AllowAnimations = True
        Me.btnContaEscolher.BackColor = System.Drawing.Color.Transparent
        Me.btnContaEscolher.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnContaEscolher.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContaEscolher.Location = New System.Drawing.Point(381, 101)
        Me.btnContaEscolher.Name = "btnContaEscolher"
        Me.btnContaEscolher.RoundedCornersMask = CType(15, Byte)
        Me.btnContaEscolher.RoundedCornersRadius = 0
        Me.btnContaEscolher.Size = New System.Drawing.Size(34, 27)
        Me.btnContaEscolher.TabIndex = 4
        Me.btnContaEscolher.TabStop = False
        Me.btnContaEscolher.Text = "..."
        Me.btnContaEscolher.UseCompatibleTextRendering = True
        Me.btnContaEscolher.UseVisualStyleBackColor = False
        Me.btnContaEscolher.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(54, 104)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(110, 19)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "Conta da Saída:"
        '
        'lblFilial
        '
        Me.lblFilial.BackColor = System.Drawing.Color.Transparent
        Me.lblFilial.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilial.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblFilial.Location = New System.Drawing.Point(9, 16)
        Me.lblFilial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilial.Name = "lblFilial"
        Me.lblFilial.Size = New System.Drawing.Size(191, 30)
        Me.lblFilial.TabIndex = 4
        Me.lblFilial.Text = "Filial"
        Me.lblFilial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.LightGray
        Me.Label18.Location = New System.Drawing.Point(83, 4)
        Me.Label18.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 13)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Filial:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 71)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 19)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Conta da Saída:"
        '
        'VButton1
        '
        Me.VButton1.AllowAnimations = True
        Me.VButton1.BackColor = System.Drawing.Color.Transparent
        Me.VButton1.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.VButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VButton1.Location = New System.Drawing.Point(381, 68)
        Me.VButton1.Name = "VButton1"
        Me.VButton1.RoundedCornersMask = CType(15, Byte)
        Me.VButton1.RoundedCornersRadius = 0
        Me.VButton1.Size = New System.Drawing.Size(34, 27)
        Me.VButton1.TabIndex = 4
        Me.VButton1.TabStop = False
        Me.VButton1.Text = "..."
        Me.VButton1.UseCompatibleTextRendering = True
        Me.VButton1.UseVisualStyleBackColor = False
        Me.VButton1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'txtContaDebito
        '
        Me.txtContaDebito.Location = New System.Drawing.Point(170, 68)
        Me.txtContaDebito.Name = "txtContaDebito"
        Me.txtContaDebito.Size = New System.Drawing.Size(205, 27)
        Me.txtContaDebito.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(54, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 19)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Conta da Saída:"
        '
        'VButton2
        '
        Me.VButton2.AllowAnimations = True
        Me.VButton2.BackColor = System.Drawing.Color.Transparent
        Me.VButton2.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.VButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VButton2.Location = New System.Drawing.Point(381, 134)
        Me.VButton2.Name = "VButton2"
        Me.VButton2.RoundedCornersMask = CType(15, Byte)
        Me.VButton2.RoundedCornersRadius = 0
        Me.VButton2.Size = New System.Drawing.Size(34, 27)
        Me.VButton2.TabIndex = 4
        Me.VButton2.TabStop = False
        Me.VButton2.Text = "..."
        Me.VButton2.UseCompatibleTextRendering = True
        Me.VButton2.UseVisualStyleBackColor = False
        Me.VButton2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(170, 134)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(205, 27)
        Me.TextBox1.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(54, 170)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 19)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Conta da Saída:"
        '
        'VButton3
        '
        Me.VButton3.AllowAnimations = True
        Me.VButton3.BackColor = System.Drawing.Color.Transparent
        Me.VButton3.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.VButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VButton3.Location = New System.Drawing.Point(381, 167)
        Me.VButton3.Name = "VButton3"
        Me.VButton3.RoundedCornersMask = CType(15, Byte)
        Me.VButton3.RoundedCornersRadius = 0
        Me.VButton3.Size = New System.Drawing.Size(34, 27)
        Me.VButton3.TabIndex = 4
        Me.VButton3.TabStop = False
        Me.VButton3.Text = "..."
        Me.VButton3.UseCompatibleTextRendering = True
        Me.VButton3.UseVisualStyleBackColor = False
        Me.VButton3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(170, 167)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(205, 27)
        Me.TextBox2.TabIndex = 3
        '
        'frmTransferenciaConta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(477, 494)
        Me.Controls.Add(Me.txtContaDebito)
        Me.Controls.Add(Me.VButton1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.VButton3)
        Me.Controls.Add(Me.txtContaCredito)
        Me.Controls.Add(Me.VButton2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnContaEscolher)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtObservacao)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.txtAcrescimo)
        Me.Controls.Add(Me.txtValorPago)
        Me.Controls.Add(Me.lblSaidaValor)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSaidaData)
        Me.KeyPreview = True
        Me.Name = "frmTransferenciaConta"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.txtSaidaData, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.lblSaidaValor, 0)
        Me.Controls.SetChildIndex(Me.txtValorPago, 0)
        Me.Controls.SetChildIndex(Me.txtAcrescimo, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.btnQuitar, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtObservacao, 0)
        Me.Controls.SetChildIndex(Me.Label17, 0)
        Me.Controls.SetChildIndex(Me.btnContaEscolher, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.VButton2, 0)
        Me.Controls.SetChildIndex(Me.txtContaCredito, 0)
        Me.Controls.SetChildIndex(Me.VButton3, 0)
        Me.Controls.SetChildIndex(Me.TextBox1, 0)
        Me.Controls.SetChildIndex(Me.TextBox2, 0)
        Me.Controls.SetChildIndex(Me.VButton1, 0)
        Me.Controls.SetChildIndex(Me.txtContaDebito, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSaidaData As Controles.MaskText_Data
    Friend WithEvents Label1 As Label
    Friend WithEvents txtValorPago As Controles.Text_Monetario
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCancelar As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAcrescimo As Controles.Text_Monetario
    Friend WithEvents Label6 As Label
    Friend WithEvents lblSaidaValor As Label
    Friend WithEvents btnQuitar As Button
    Friend WithEvents txtObservacao As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtContaCredito As TextBox
    Friend WithEvents btnContaEscolher As VIBlend.WinForms.Controls.vButton
    Friend WithEvents Label17 As Label
    Friend WithEvents lblFilial As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents VButton1 As VIBlend.WinForms.Controls.vButton
    Friend WithEvents txtContaDebito As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents VButton2 As VIBlend.WinForms.Controls.vButton
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents VButton3 As VIBlend.WinForms.Controls.vButton
    Friend WithEvents TextBox2 As TextBox
End Class
