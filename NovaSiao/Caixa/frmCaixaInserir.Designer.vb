<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCaixaInserir
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
        Me.btnInserir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnContaEscolher = New VIBlend.WinForms.Controls.vButton()
        Me.txtConta = New System.Windows.Forms.TextBox()
        Me.lblDtInicialTexto = New System.Windows.Forms.Label()
        Me.lblDtInicialValor = New System.Windows.Forms.Label()
        Me.chkDiario = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pnlDtFinal = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMax = New System.Windows.Forms.Label()
        Me.dtpFinal = New System.Windows.Forms.DateTimePicker()
        Me.lblFilial = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.pnlDtFinal.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(387, 50)
        Me.Panel1.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(77, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(310, 50)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Efetuar Fechamento de Caixa"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnInserir
        '
        Me.btnInserir.Image = Global.NovaSiao.My.Resources.Resources.accept
        Me.btnInserir.Location = New System.Drawing.Point(67, 345)
        Me.btnInserir.Name = "btnInserir"
        Me.btnInserir.Size = New System.Drawing.Size(120, 48)
        Me.btnInserir.TabIndex = 6
        Me.btnInserir.Text = "&Inserir"
        Me.btnInserir.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnInserir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnInserir.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_30px
        Me.btnCancelar.Location = New System.Drawing.Point(197, 345)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(120, 48)
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(111, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Conta do Caixa:"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(358, 25)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Escolha a CONTA e a DATA para fazer o fechamento:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnContaEscolher
        '
        Me.btnContaEscolher.AllowAnimations = True
        Me.btnContaEscolher.BackColor = System.Drawing.Color.Transparent
        Me.btnContaEscolher.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnContaEscolher.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnContaEscolher.Location = New System.Drawing.Point(340, 142)
        Me.btnContaEscolher.Name = "btnContaEscolher"
        Me.btnContaEscolher.RoundedCornersMask = CType(15, Byte)
        Me.btnContaEscolher.RoundedCornersRadius = 0
        Me.btnContaEscolher.Size = New System.Drawing.Size(34, 31)
        Me.btnContaEscolher.TabIndex = 10
        Me.btnContaEscolher.TabStop = False
        Me.btnContaEscolher.Text = "..."
        Me.btnContaEscolher.UseCompatibleTextRendering = True
        Me.btnContaEscolher.UseVisualStyleBackColor = False
        Me.btnContaEscolher.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'txtConta
        '
        Me.txtConta.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConta.Location = New System.Drawing.Point(129, 142)
        Me.txtConta.Name = "txtConta"
        Me.txtConta.Size = New System.Drawing.Size(205, 31)
        Me.txtConta.TabIndex = 12
        '
        'lblDtInicialTexto
        '
        Me.lblDtInicialTexto.Location = New System.Drawing.Point(12, 239)
        Me.lblDtInicialTexto.Name = "lblDtInicialTexto"
        Me.lblDtInicialTexto.Size = New System.Drawing.Size(111, 19)
        Me.lblDtInicialTexto.TabIndex = 15
        Me.lblDtInicialTexto.Text = "Data do Caixa:"
        Me.lblDtInicialTexto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDtInicialValor
        '
        Me.lblDtInicialValor.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDtInicialValor.Location = New System.Drawing.Point(129, 235)
        Me.lblDtInicialValor.Name = "lblDtInicialValor"
        Me.lblDtInicialValor.Size = New System.Drawing.Size(139, 27)
        Me.lblDtInicialValor.TabIndex = 17
        Me.lblDtInicialValor.Text = "00/00/0000"
        Me.lblDtInicialValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkDiario
        '
        Me.chkDiario.AutoSize = True
        Me.chkDiario.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.chkDiario.Checked = True
        Me.chkDiario.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDiario.Location = New System.Drawing.Point(150, 198)
        Me.chkDiario.Name = "chkDiario"
        Me.chkDiario.Size = New System.Drawing.Size(107, 23)
        Me.chkDiario.TabIndex = 18
        Me.chkDiario.Text = "Caixa Diário"
        Me.chkDiario.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(215, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(12, 192)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(362, 35)
        Me.Panel2.TabIndex = 19
        '
        'pnlDtFinal
        '
        Me.pnlDtFinal.Controls.Add(Me.Label5)
        Me.pnlDtFinal.Controls.Add(Me.lblMax)
        Me.pnlDtFinal.Controls.Add(Me.dtpFinal)
        Me.pnlDtFinal.Location = New System.Drawing.Point(12, 265)
        Me.pnlDtFinal.Name = "pnlDtFinal"
        Me.pnlDtFinal.Size = New System.Drawing.Size(362, 68)
        Me.pnlDtFinal.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(32, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 19)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Data Final:"
        '
        'lblMax
        '
        Me.lblMax.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMax.Location = New System.Drawing.Point(114, 44)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(138, 19)
        Me.lblMax.TabIndex = 19
        Me.lblMax.Text = "máx.:"
        '
        'dtpFinal
        '
        Me.dtpFinal.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFinal.Location = New System.Drawing.Point(117, 10)
        Me.dtpFinal.Name = "dtpFinal"
        Me.dtpFinal.ShowUpDown = True
        Me.dtpFinal.Size = New System.Drawing.Size(134, 31)
        Me.dtpFinal.TabIndex = 17
        '
        'lblFilial
        '
        Me.lblFilial.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilial.Location = New System.Drawing.Point(129, 62)
        Me.lblFilial.Name = "lblFilial"
        Me.lblFilial.Size = New System.Drawing.Size(245, 32)
        Me.lblFilial.TabIndex = 21
        Me.lblFilial.Text = "Filial"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(79, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 19)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Filial:"
        '
        'frmCaixaInserir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(387, 406)
        Me.Controls.Add(Me.lblFilial)
        Me.Controls.Add(Me.chkDiario)
        Me.Controls.Add(Me.lblDtInicialValor)
        Me.Controls.Add(Me.lblDtInicialTexto)
        Me.Controls.Add(Me.txtConta)
        Me.Controls.Add(Me.btnContaEscolher)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnInserir)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlDtFinal)
        Me.Name = "frmCaixaInserir"
        Me.Controls.SetChildIndex(Me.pnlDtFinal, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.btnInserir, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.btnContaEscolher, 0)
        Me.Controls.SetChildIndex(Me.txtConta, 0)
        Me.Controls.SetChildIndex(Me.lblDtInicialTexto, 0)
        Me.Controls.SetChildIndex(Me.lblDtInicialValor, 0)
        Me.Controls.SetChildIndex(Me.chkDiario, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.lblFilial, 0)
        Me.Panel1.ResumeLayout(False)
        Me.pnlDtFinal.ResumeLayout(False)
        Me.pnlDtFinal.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnInserir As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnContaEscolher As VIBlend.WinForms.Controls.vButton
    Friend WithEvents txtConta As TextBox
    Friend WithEvents lblDtInicialTexto As Label
    Friend WithEvents lblDtInicialValor As Label
    Friend WithEvents chkDiario As CheckBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents pnlDtFinal As Panel
    Friend WithEvents lblMax As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents dtpFinal As DateTimePicker
    Friend WithEvents lblFilial As Label
    Friend WithEvents Label1 As Label
End Class
