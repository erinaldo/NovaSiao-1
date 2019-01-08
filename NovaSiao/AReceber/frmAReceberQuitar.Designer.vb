<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAReceberQuitar
    Inherits NovaSiao.frmModNBorder

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtEntradaData = New Controles.MaskText_Data()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbIDConta = New Controles.ComboBox_OnlyValues()
        Me.txtDoValor = New Controles.Text_Monetario()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.rbtCreditar = New System.Windows.Forms.RadioButton()
        Me.rbtEntrada = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAcrescimo = New Controles.Text_Monetario()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblEntradaValor = New System.Windows.Forms.Label()
        Me.cmbIDMovForma = New Controles.ComboBox_OnlyValues()
        Me.cmbIDMovTipo = New Controles.ComboBox_OnlyValues()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.txtObservacao = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(426, 50)
        Me.Panel1.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(186, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(240, 50)
        Me.lblTitulo.Text = "A Receber - Quitar"
        '
        'txtEntradaData
        '
        Me.txtEntradaData.Location = New System.Drawing.Point(170, 134)
        Me.txtEntradaData.Mask = "00/00/0000"
        Me.txtEntradaData.Name = "txtEntradaData"
        Me.txtEntradaData.Size = New System.Drawing.Size(109, 27)
        Me.txtEntradaData.TabIndex = 3
        Me.txtEntradaData.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(65, 137)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 19)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Entrada Data:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(125, 19)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Conta da Entrada:"
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
        Me.cmbIDConta.TabIndex = 2
        '
        'txtDoValor
        '
        Me.txtDoValor.Location = New System.Drawing.Point(170, 167)
        Me.txtDoValor.Name = "txtDoValor"
        Me.txtDoValor.Size = New System.Drawing.Size(109, 27)
        Me.txtDoValor.TabIndex = 4
        Me.txtDoValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(95, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 19)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Do Valor:"
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_30px
        Me.btnCancelar.Location = New System.Drawing.Point(218, 407)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(128, 48)
        Me.btnCancelar.TabIndex = 10
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(218, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel2.Controls.Add(Me.rbtCreditar)
        Me.Panel2.Controls.Add(Me.rbtEntrada)
        Me.Panel2.Location = New System.Drawing.Point(12, 59)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(402, 33)
        Me.Panel2.TabIndex = 1
        '
        'rbtCreditar
        '
        Me.rbtCreditar.AutoSize = True
        Me.rbtCreditar.Location = New System.Drawing.Point(231, 5)
        Me.rbtCreditar.Name = "rbtCreditar"
        Me.rbtCreditar.Size = New System.Drawing.Size(79, 23)
        Me.rbtCreditar.TabIndex = 1
        Me.rbtCreditar.TabStop = True
        Me.rbtCreditar.Text = "Creditar"
        Me.rbtCreditar.UseVisualStyleBackColor = True
        '
        'rbtEntrada
        '
        Me.rbtEntrada.AutoSize = True
        Me.rbtEntrada.Checked = True
        Me.rbtEntrada.Cursor = System.Windows.Forms.Cursors.Default
        Me.rbtEntrada.Location = New System.Drawing.Point(102, 5)
        Me.rbtEntrada.Name = "rbtEntrada"
        Me.rbtEntrada.Size = New System.Drawing.Size(77, 23)
        Me.rbtEntrada.TabIndex = 0
        Me.rbtEntrada.TabStop = True
        Me.rbtEntrada.Text = "Entrada"
        Me.rbtEntrada.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(61, 203)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 19)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Do Acréscimo:"
        '
        'txtAcrescimo
        '
        Me.txtAcrescimo.Location = New System.Drawing.Point(170, 200)
        Me.txtAcrescimo.Name = "txtAcrescimo"
        Me.txtAcrescimo.Size = New System.Drawing.Size(109, 27)
        Me.txtAcrescimo.TabIndex = 5
        Me.txtAcrescimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 239)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(146, 19)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Valor a ser Recebido:"
        '
        'lblEntradaValor
        '
        Me.lblEntradaValor.BackColor = System.Drawing.Color.Transparent
        Me.lblEntradaValor.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntradaValor.Location = New System.Drawing.Point(170, 237)
        Me.lblEntradaValor.Name = "lblEntradaValor"
        Me.lblEntradaValor.Size = New System.Drawing.Size(109, 25)
        Me.lblEntradaValor.TabIndex = 3
        Me.lblEntradaValor.Text = "R$ 0,00"
        Me.lblEntradaValor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbIDMovForma
        '
        Me.cmbIDMovForma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIDMovForma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIDMovForma.FormattingEnabled = True
        Me.cmbIDMovForma.Location = New System.Drawing.Point(170, 307)
        Me.cmbIDMovForma.Name = "cmbIDMovForma"
        Me.cmbIDMovForma.RestrictContentToListItems = True
        Me.cmbIDMovForma.Size = New System.Drawing.Size(234, 27)
        Me.cmbIDMovForma.TabIndex = 7
        '
        'cmbIDMovTipo
        '
        Me.cmbIDMovTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIDMovTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIDMovTipo.FormattingEnabled = True
        Me.cmbIDMovTipo.Location = New System.Drawing.Point(170, 274)
        Me.cmbIDMovTipo.Name = "cmbIDMovTipo"
        Me.cmbIDMovTipo.RestrictContentToListItems = True
        Me.cmbIDMovTipo.Size = New System.Drawing.Size(154, 27)
        Me.cmbIDMovTipo.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(110, 310)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 19)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Forma:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(122, 277)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 19)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Tipo:"
        '
        'btnQuitar
        '
        Me.btnQuitar.Image = Global.NovaSiao.My.Resources.Resources.dollar_currency_sign
        Me.btnQuitar.Location = New System.Drawing.Point(81, 407)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(128, 48)
        Me.btnQuitar.TabIndex = 9
        Me.btnQuitar.Text = "&Quitar"
        Me.btnQuitar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'txtObservacao
        '
        Me.txtObservacao.Location = New System.Drawing.Point(170, 340)
        Me.txtObservacao.Multiline = True
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(234, 54)
        Me.txtObservacao.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(73, 340)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 19)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Observação:"
        '
        'frmAReceberQuitar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(426, 467)
        Me.Controls.Add(Me.txtObservacao)
        Me.Controls.Add(Me.cmbIDMovForma)
        Me.Controls.Add(Me.cmbIDMovTipo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.txtAcrescimo)
        Me.Controls.Add(Me.txtDoValor)
        Me.Controls.Add(Me.lblEntradaValor)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbIDConta)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtEntradaData)
        Me.KeyPreview = True
        Me.Name = "frmAReceberQuitar"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.txtEntradaData, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.cmbIDConta, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.lblEntradaValor, 0)
        Me.Controls.SetChildIndex(Me.txtDoValor, 0)
        Me.Controls.SetChildIndex(Me.txtAcrescimo, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.btnQuitar, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.cmbIDMovTipo, 0)
        Me.Controls.SetChildIndex(Me.cmbIDMovForma, 0)
        Me.Controls.SetChildIndex(Me.txtObservacao, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtEntradaData As Controles.MaskText_Data
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbIDConta As Controles.ComboBox_OnlyValues
    Friend WithEvents txtDoValor As Controles.Text_Monetario
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCancelar As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents rbtCreditar As RadioButton
    Friend WithEvents rbtEntrada As RadioButton
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAcrescimo As Controles.Text_Monetario
    Friend WithEvents Label6 As Label
    Friend WithEvents lblEntradaValor As Label
    Friend WithEvents cmbIDMovForma As Controles.ComboBox_OnlyValues
    Friend WithEvents cmbIDMovTipo As Controles.ComboBox_OnlyValues
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents btnQuitar As Button
    Friend WithEvents txtObservacao As TextBox
    Friend WithEvents Label2 As Label
End Class
