<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVendaEntrada
    Inherits NovaSiao.frmModNBorder

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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtValor = New Controles.Text_Monetario()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.cmbFormaTipo = New Controles.ComboBox_OnlyValues()
        Me.cmbForma = New Controles.ComboBox_OnlyValues()
        Me.lblFilial = New System.Windows.Forms.Label()
        Me.lblConta = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(362, 50)
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(136, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(226, 50)
        Me.lblTitulo.Text = "Entrada / Pagamento"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(38, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Tipo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 133)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Forma"
        '
        'txtValor
        '
        Me.txtValor.Location = New System.Drawing.Point(81, 179)
        Me.txtValor.Name = "txtValor"
        Me.txtValor.Size = New System.Drawing.Size(93, 27)
        Me.txtValor.TabIndex = 2
        Me.txtValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 182)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Valor"
        '
        'btnOK
        '
        Me.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnOK.Image = Global.NovaSiao.My.Resources.Resources.dollar_currency_sign
        Me.btnOK.Location = New System.Drawing.Point(65, 227)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(111, 48)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "&Receber"
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_30px
        Me.btnCancelar.Location = New System.Drawing.Point(183, 227)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(111, 48)
        Me.btnCancelar.TabIndex = 4
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'cmbFormaTipo
        '
        Me.cmbFormaTipo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFormaTipo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFormaTipo.FormattingEnabled = True
        Me.cmbFormaTipo.Location = New System.Drawing.Point(82, 81)
        Me.cmbFormaTipo.Name = "cmbFormaTipo"
        Me.cmbFormaTipo.RestrictContentToListItems = True
        Me.cmbFormaTipo.Size = New System.Drawing.Size(154, 27)
        Me.cmbFormaTipo.TabIndex = 0
        '
        'cmbForma
        '
        Me.cmbForma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbForma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbForma.FormattingEnabled = True
        Me.cmbForma.Location = New System.Drawing.Point(82, 130)
        Me.cmbForma.Name = "cmbForma"
        Me.cmbForma.RestrictContentToListItems = True
        Me.cmbForma.Size = New System.Drawing.Size(234, 27)
        Me.cmbForma.TabIndex = 1
        '
        'lblFilial
        '
        Me.lblFilial.BackColor = System.Drawing.Color.Gainsboro
        Me.lblFilial.Location = New System.Drawing.Point(4, 291)
        Me.lblFilial.Name = "lblFilial"
        Me.lblFilial.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblFilial.Size = New System.Drawing.Size(162, 23)
        Me.lblFilial.TabIndex = 5
        Me.lblFilial.Text = "Filial"
        Me.lblFilial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblConta
        '
        Me.lblConta.BackColor = System.Drawing.Color.Gainsboro
        Me.lblConta.Location = New System.Drawing.Point(169, 291)
        Me.lblConta.Name = "lblConta"
        Me.lblConta.Padding = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblConta.Size = New System.Drawing.Size(189, 23)
        Me.lblConta.TabIndex = 5
        Me.lblConta.Text = "Conta"
        Me.lblConta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmVendaPagamento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(362, 318)
        Me.Controls.Add(Me.lblConta)
        Me.Controls.Add(Me.lblFilial)
        Me.Controls.Add(Me.cmbForma)
        Me.Controls.Add(Me.cmbFormaTipo)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtValor)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmVendaPagamento"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtValor, 0)
        Me.Controls.SetChildIndex(Me.btnOK, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.cmbFormaTipo, 0)
        Me.Controls.SetChildIndex(Me.cmbForma, 0)
        Me.Controls.SetChildIndex(Me.lblFilial, 0)
        Me.Controls.SetChildIndex(Me.lblConta, 0)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtValor As Controles.Text_Monetario
    Friend WithEvents Label3 As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents cmbFormaTipo As Controles.ComboBox_OnlyValues
    Friend WithEvents cmbForma As Controles.ComboBox_OnlyValues
    Friend WithEvents lblFilial As Label
    Friend WithEvents lblConta As Label
End Class
