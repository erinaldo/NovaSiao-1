<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAPagarQuitar
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbIDConta = New Controles.ComboBox_OnlyValues()
        Me.txtValorPago = New Controles.Text_Monetario()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbtParcial = New System.Windows.Forms.RadioButton()
        Me.rbtTotal = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAcrescimo = New Controles.Text_Monetario()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSaidaValor = New System.Windows.Forms.Label()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.txtObservacao = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(428, 50)
        Me.Panel1.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(257, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(171, 50)
        Me.lblTitulo.Text = "A Pagar - Quitar"
        '
        'txtSaidaData
        '
        Me.txtSaidaData.Location = New System.Drawing.Point(170, 134)
        Me.txtSaidaData.Mask = "00/00/0000"
        Me.txtSaidaData.Name = "txtSaidaData"
        Me.txtSaidaData.Size = New System.Drawing.Size(109, 27)
        Me.txtSaidaData.TabIndex = 5
        Me.txtSaidaData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(52, 137)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Data do Débito:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 19)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Conta da Saída:"
        '
        'cmbIDConta
        '
        Me.cmbIDConta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIDConta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIDConta.FormattingEnabled = True
        Me.cmbIDConta.Location = New System.Drawing.Point(170, 101)
        Me.cmbIDConta.Name = "cmbIDConta"
        Me.cmbIDConta.RestrictContentToListItems = True
        Me.cmbIDConta.Size = New System.Drawing.Size(234, 27)
        Me.cmbIDConta.TabIndex = 3
        '
        'txtValorPago
        '
        Me.txtValorPago.Location = New System.Drawing.Point(170, 167)
        Me.txtValorPago.Name = "txtValorPago"
        Me.txtValorPago.Size = New System.Drawing.Size(109, 27)
        Me.txtValorPago.TabIndex = 7
        Me.txtValorPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 19)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Do Valor:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_30px
        Me.btnCancelar.Location = New System.Drawing.Point(222, 350)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(120, 48)
        Me.btnCancelar.TabIndex = 17
        Me.btnCancelar.Text = "&Fechar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel2.Controls.Add(Me.rbtParcial)
        Me.Panel2.Controls.Add(Me.rbtTotal)
        Me.Panel2.Location = New System.Drawing.Point(12, 59)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(402, 33)
        Me.Panel2.TabIndex = 1
        '
        'rbtParcial
        '
        Me.rbtParcial.AutoSize = True
        Me.rbtParcial.Location = New System.Drawing.Point(216, 5)
        Me.rbtParcial.Name = "rbtParcial"
        Me.rbtParcial.Size = New System.Drawing.Size(148, 23)
        Me.rbtParcial.TabIndex = 1
        Me.rbtParcial.TabStop = True
        Me.rbtParcial.Text = "Pagamento Parcial"
        Me.rbtParcial.UseVisualStyleBackColor = True
        '
        'rbtTotal
        '
        Me.rbtTotal.AutoSize = True
        Me.rbtTotal.Checked = True
        Me.rbtTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbtTotal.Location = New System.Drawing.Point(55, 5)
        Me.rbtTotal.Name = "rbtTotal"
        Me.rbtTotal.Size = New System.Drawing.Size(136, 23)
        Me.rbtTotal.TabIndex = 0
        Me.rbtTotal.TabStop = True
        Me.rbtTotal.Text = "Pagamento Total"
        Me.rbtTotal.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(83, 203)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 19)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Acréscimo:"
        '
        'txtAcrescimo
        '
        Me.txtAcrescimo.Location = New System.Drawing.Point(170, 200)
        Me.txtAcrescimo.Name = "txtAcrescimo"
        Me.txtAcrescimo.Size = New System.Drawing.Size(109, 27)
        Me.txtAcrescimo.TabIndex = 9
        Me.txtAcrescimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(45, 240)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 19)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Valor a ser Pago:"
        '
        'lblSaidaValor
        '
        Me.lblSaidaValor.BackColor = System.Drawing.Color.Transparent
        Me.lblSaidaValor.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaidaValor.Location = New System.Drawing.Point(169, 236)
        Me.lblSaidaValor.Name = "lblSaidaValor"
        Me.lblSaidaValor.Size = New System.Drawing.Size(109, 25)
        Me.lblSaidaValor.TabIndex = 13
        Me.lblSaidaValor.Text = "R$ 0,00"
        Me.lblSaidaValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnQuitar
        '
        Me.btnQuitar.Image = Global.NovaSiao.My.Resources.Resources.dollar_currency_sign
        Me.btnQuitar.Location = New System.Drawing.Point(92, 350)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(120, 48)
        Me.btnQuitar.TabIndex = 16
        Me.btnQuitar.Text = "&Quitar"
        Me.btnQuitar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'txtObservacao
        '
        Me.txtObservacao.Location = New System.Drawing.Point(170, 277)
        Me.txtObservacao.Multiline = True
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(234, 54)
        Me.txtObservacao.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(73, 277)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 19)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Observação:"
        '
        'frmAPagarQuitar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(428, 417)
        Me.Controls.Add(Me.txtObservacao)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.txtAcrescimo)
        Me.Controls.Add(Me.txtValorPago)
        Me.Controls.Add(Me.lblSaidaValor)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbIDConta)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSaidaData)
        Me.KeyPreview = True
        Me.Name = "frmAPagarQuitar"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.txtSaidaData, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.cmbIDConta, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.lblSaidaValor, 0)
        Me.Controls.SetChildIndex(Me.txtValorPago, 0)
        Me.Controls.SetChildIndex(Me.txtAcrescimo, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.btnQuitar, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.txtObservacao, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSaidaData As Controles.MaskText_Data
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbIDConta As Controles.ComboBox_OnlyValues
    Friend WithEvents txtValorPago As Controles.Text_Monetario
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCancelar As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents rbtParcial As RadioButton
    Friend WithEvents rbtTotal As RadioButton
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAcrescimo As Controles.Text_Monetario
    Friend WithEvents Label6 As Label
    Friend WithEvents lblSaidaValor As Label
    Friend WithEvents btnQuitar As Button
    Friend WithEvents txtObservacao As TextBox
    Friend WithEvents Label2 As Label
End Class
