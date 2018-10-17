<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAPagarProcurar
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.txtCredorCadastro = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnMesAtual = New VIBlend.WinForms.Controls.vButton()
        Me.btnPeriodoAnterior = New VIBlend.WinForms.Controls.vArrowButton()
        Me.btnPeriodoPosterior = New VIBlend.WinForms.Controls.vArrowButton()
        Me.lblPeriodo = New System.Windows.Forms.Label()
        Me.chkPeriodoTodos = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvListagem = New System.Windows.Forms.DataGridView()
        Me.clnVencimento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnCredor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnOrigem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnForma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnValor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnSituacao = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.pnlVenda = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlMes = New System.Windows.Forms.Panel()
        Me.dtMes = New System.Windows.Forms.MonthCalendar()
        Me.btnProcurar = New VIBlend.WinForms.Controls.vButton()
        Me.btnClose = New VIBlend.WinForms.Controls.vFormButton()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pnlSituacao = New System.Windows.Forms.Panel()
        Me.rbtTodas = New System.Windows.Forms.RadioButton()
        Me.rbtNegociadas = New System.Windows.Forms.RadioButton()
        Me.rbtNegativadas = New System.Windows.Forms.RadioButton()
        Me.rbtCanceladas = New System.Windows.Forms.RadioButton()
        Me.rbtEmAberto = New System.Windows.Forms.RadioButton()
        Me.rbtQuitadas = New System.Windows.Forms.RadioButton()
        Me.cmbCobrancaForma = New Controles.ComboBox_OnlyValues()
        Me.btnQuitar = New System.Windows.Forms.Button()
        Me.mnuOperacoes = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.QuitarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NegativarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormalizarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.VerOrigemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvListagem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVenda.SuspendLayout()
        Me.pnlMes.SuspendLayout()
        Me.pnlSituacao.SuspendLayout()
        Me.mnuOperacoes.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnClose, 0)
        '
        'lblTitulo
        '
        Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.None
        Me.lblTitulo.Location = New System.Drawing.Point(686, 7)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(219, 34)
        Me.lblTitulo.Text = "A Pagar - Procurar"
        '
        'txtCredorCadastro
        '
        Me.txtCredorCadastro.Location = New System.Drawing.Point(156, 101)
        Me.txtCredorCadastro.Name = "txtCredorCadastro"
        Me.txtCredorCadastro.Size = New System.Drawing.Size(407, 27)
        Me.txtCredorCadastro.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(96, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 19)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Credor"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel2.Controls.Add(Me.btnMesAtual)
        Me.Panel2.Controls.Add(Me.btnPeriodoAnterior)
        Me.Panel2.Controls.Add(Me.btnPeriodoPosterior)
        Me.Panel2.Controls.Add(Me.lblPeriodo)
        Me.Panel2.Controls.Add(Me.chkPeriodoTodos)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Location = New System.Drawing.Point(622, 66)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(310, 66)
        Me.Panel2.TabIndex = 5
        '
        'btnMesAtual
        '
        Me.btnMesAtual.AllowAnimations = True
        Me.btnMesAtual.BackColor = System.Drawing.Color.Transparent
        Me.btnMesAtual.Location = New System.Drawing.Point(253, 33)
        Me.btnMesAtual.Name = "btnMesAtual"
        Me.btnMesAtual.RoundedCornersMask = CType(15, Byte)
        Me.btnMesAtual.RoundedCornersRadius = 0
        Me.btnMesAtual.Size = New System.Drawing.Size(48, 25)
        Me.btnMesAtual.TabIndex = 7
        Me.btnMesAtual.TabStop = False
        Me.btnMesAtual.Text = "Atual"
        Me.btnMesAtual.UseVisualStyleBackColor = False
        Me.btnMesAtual.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'btnPeriodoAnterior
        '
        Me.btnPeriodoAnterior.AllowAnimations = True
        Me.btnPeriodoAnterior.ArrowDirection = System.Windows.Forms.ArrowDirection.Left
        Me.btnPeriodoAnterior.Location = New System.Drawing.Point(18, 33)
        Me.btnPeriodoAnterior.Maximum = 100
        Me.btnPeriodoAnterior.Minimum = 0
        Me.btnPeriodoAnterior.Name = "btnPeriodoAnterior"
        Me.btnPeriodoAnterior.Size = New System.Drawing.Size(25, 25)
        Me.btnPeriodoAnterior.TabIndex = 6
        Me.btnPeriodoAnterior.TabStop = False
        Me.btnPeriodoAnterior.Text = "VArrowButton1"
        Me.btnPeriodoAnterior.Value = 0
        Me.btnPeriodoAnterior.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'btnPeriodoPosterior
        '
        Me.btnPeriodoPosterior.AllowAnimations = True
        Me.btnPeriodoPosterior.ArrowDirection = System.Windows.Forms.ArrowDirection.Right
        Me.btnPeriodoPosterior.Location = New System.Drawing.Point(223, 33)
        Me.btnPeriodoPosterior.Maximum = 100
        Me.btnPeriodoPosterior.Minimum = 0
        Me.btnPeriodoPosterior.Name = "btnPeriodoPosterior"
        Me.btnPeriodoPosterior.Size = New System.Drawing.Size(25, 25)
        Me.btnPeriodoPosterior.TabIndex = 6
        Me.btnPeriodoPosterior.TabStop = False
        Me.btnPeriodoPosterior.Text = "VArrowButton1"
        Me.btnPeriodoPosterior.Value = 0
        Me.btnPeriodoPosterior.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'lblPeriodo
        '
        Me.lblPeriodo.BackColor = System.Drawing.Color.Transparent
        Me.lblPeriodo.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPeriodo.Location = New System.Drawing.Point(14, 30)
        Me.lblPeriodo.Name = "lblPeriodo"
        Me.lblPeriodo.Size = New System.Drawing.Size(238, 30)
        Me.lblPeriodo.TabIndex = 6
        Me.lblPeriodo.Text = "Novembro | 2017"
        Me.lblPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkPeriodoTodos
        '
        Me.chkPeriodoTodos.AutoSize = True
        Me.chkPeriodoTodos.Location = New System.Drawing.Point(109, 4)
        Me.chkPeriodoTodos.Name = "chkPeriodoTodos"
        Me.chkPeriodoTodos.Size = New System.Drawing.Size(152, 23)
        Me.chkPeriodoTodos.TabIndex = 5
        Me.chkPeriodoTodos.TabStop = False
        Me.chkPeriodoTodos.Text = "Todos | Sem Limite"
        Me.chkPeriodoTodos.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Vencimento"
        '
        'dgvListagem
        '
        Me.dgvListagem.AllowUserToAddRows = False
        Me.dgvListagem.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure
        Me.dgvListagem.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvListagem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListagem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clnVencimento, Me.clnCredor, Me.clnOrigem, Me.clnForma, Me.clnValor, Me.clnSituacao})
        Me.dgvListagem.GridColor = System.Drawing.Color.LightSteelBlue
        Me.dgvListagem.Location = New System.Drawing.Point(12, 175)
        Me.dgvListagem.MultiSelect = False
        Me.dgvListagem.Name = "dgvListagem"
        Me.dgvListagem.ReadOnly = True
        Me.dgvListagem.RowHeadersWidth = 30
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSlateGray
        Me.dgvListagem.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvListagem.Size = New System.Drawing.Size(920, 368)
        Me.dgvListagem.TabIndex = 8
        '
        'clnVencimento
        '
        Me.clnVencimento.Frozen = True
        Me.clnVencimento.HeaderText = "Vencimento"
        Me.clnVencimento.Name = "clnVencimento"
        Me.clnVencimento.ReadOnly = True
        '
        'clnCredor
        '
        Me.clnCredor.Frozen = True
        Me.clnCredor.HeaderText = "Credor"
        Me.clnCredor.Name = "clnCredor"
        Me.clnCredor.ReadOnly = True
        Me.clnCredor.Width = 280
        '
        'clnOrigem
        '
        Me.clnOrigem.HeaderText = "Origem"
        Me.clnOrigem.Name = "clnOrigem"
        Me.clnOrigem.ReadOnly = True
        Me.clnOrigem.Width = 150
        '
        'clnForma
        '
        Me.clnForma.HeaderText = "Forma"
        Me.clnForma.Name = "clnForma"
        Me.clnForma.ReadOnly = True
        Me.clnForma.Width = 150
        '
        'clnValor
        '
        Me.clnValor.HeaderText = "Valor"
        Me.clnValor.Name = "clnValor"
        Me.clnValor.ReadOnly = True
        '
        'clnSituacao
        '
        Me.clnSituacao.HeaderText = "Situação"
        Me.clnSituacao.Name = "clnSituacao"
        Me.clnSituacao.ReadOnly = True
        Me.clnSituacao.Width = 80
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.ForeColor = System.Drawing.Color.DarkRed
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_24x24
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.Location = New System.Drawing.Point(789, 599)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(143, 41)
        Me.btnCancelar.TabIndex = 13
        Me.btnCancelar.Text = "&Fechar"
        Me.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEditar.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnEditar.Image = Global.NovaSiao.My.Resources.Resources.editar
        Me.btnEditar.Location = New System.Drawing.Point(640, 599)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(143, 41)
        Me.btnEditar.TabIndex = 12
        Me.btnEditar.Text = "&Editar Origem"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'pnlVenda
        '
        Me.pnlVenda.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlVenda.Controls.Add(Me.Label10)
        Me.pnlVenda.Controls.Add(Me.Label9)
        Me.pnlVenda.Controls.Add(Me.Label6)
        Me.pnlVenda.Controls.Add(Me.Label7)
        Me.pnlVenda.Controls.Add(Me.Label5)
        Me.pnlVenda.Controls.Add(Me.Label8)
        Me.pnlVenda.Location = New System.Drawing.Point(12, 146)
        Me.pnlVenda.Name = "pnlVenda"
        Me.pnlVenda.Size = New System.Drawing.Size(920, 28)
        Me.pnlVenda.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(826, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 19)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Situação"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(766, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(42, 19)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Valor"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(565, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 19)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Forma de Cobrança"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(132, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 19)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Credor"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(414, 4)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(109, 19)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Origem do Pag."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Location = New System.Drawing.Point(33, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 19)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Vencimento"
        '
        'pnlMes
        '
        Me.pnlMes.Controls.Add(Me.dtMes)
        Me.pnlMes.Location = New System.Drawing.Point(671, 363)
        Me.pnlMes.Name = "pnlMes"
        Me.pnlMes.Size = New System.Drawing.Size(234, 166)
        Me.pnlMes.TabIndex = 6
        Me.pnlMes.Visible = False
        '
        'dtMes
        '
        Me.dtMes.Location = New System.Drawing.Point(3, 0)
        Me.dtMes.Name = "dtMes"
        Me.dtMes.TabIndex = 0
        '
        'btnProcurar
        '
        Me.btnProcurar.AllowAnimations = True
        Me.btnProcurar.BackColor = System.Drawing.Color.Transparent
        Me.btnProcurar.Enabled = False
        Me.btnProcurar.Image = Global.NovaSiao.My.Resources.Resources.search1
        Me.btnProcurar.ImageAbsolutePosition = New System.Drawing.Point(10, 5)
        Me.btnProcurar.Location = New System.Drawing.Point(12, 599)
        Me.btnProcurar.Name = "btnProcurar"
        Me.btnProcurar.RoundedCornersMask = CType(15, Byte)
        Me.btnProcurar.RoundedCornersRadius = 0
        Me.btnProcurar.Size = New System.Drawing.Size(126, 41)
        Me.btnProcurar.TabIndex = 10
        Me.btnProcurar.TabStop = False
        Me.btnProcurar.Text = "&Procurar"
        Me.btnProcurar.TextAbsolutePosition = New System.Drawing.Point(25, 5)
        Me.btnProcurar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnProcurar.UseAbsoluteImagePositioning = True
        Me.btnProcurar.UseAbsoluteTextPositioning = True
        Me.btnProcurar.UseVisualStyleBackColor = False
        Me.btnProcurar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'btnClose
        '
        Me.btnClose.AllowAnimations = True
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.ButtonType = VIBlend.WinForms.Controls.vFormButtonType.CloseButton
        Me.btnClose.ForeColor = System.Drawing.Color.Firebrick
        Me.btnClose.Location = New System.Drawing.Point(912, 12)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RibbonStyle = False
        Me.btnClose.RoundedCornersMask = CType(15, Byte)
        Me.btnClose.ShowFocusRectangle = False
        Me.btnClose.Size = New System.Drawing.Size(20, 20)
        Me.btnClose.TabIndex = 47
        Me.btnClose.TabStop = False
        Me.btnClose.UseVisualStyleBackColor = False
        Me.btnClose.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2003SILVER
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(16, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(134, 19)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Forma de Cobrança"
        '
        'pnlSituacao
        '
        Me.pnlSituacao.BackColor = System.Drawing.Color.LightSteelBlue
        Me.pnlSituacao.Controls.Add(Me.rbtTodas)
        Me.pnlSituacao.Controls.Add(Me.rbtNegociadas)
        Me.pnlSituacao.Controls.Add(Me.rbtNegativadas)
        Me.pnlSituacao.Controls.Add(Me.rbtCanceladas)
        Me.pnlSituacao.Controls.Add(Me.rbtEmAberto)
        Me.pnlSituacao.Controls.Add(Me.rbtQuitadas)
        Me.pnlSituacao.Location = New System.Drawing.Point(12, 549)
        Me.pnlSituacao.Name = "pnlSituacao"
        Me.pnlSituacao.Size = New System.Drawing.Size(920, 42)
        Me.pnlSituacao.TabIndex = 9
        '
        'rbtTodas
        '
        Me.rbtTodas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtTodas.Location = New System.Drawing.Point(691, 4)
        Me.rbtTodas.Name = "rbtTodas"
        Me.rbtTodas.Size = New System.Drawing.Size(112, 33)
        Me.rbtTodas.TabIndex = 6
        Me.rbtTodas.TabStop = True
        Me.rbtTodas.Text = "Todas"
        Me.rbtTodas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtTodas.UseVisualStyleBackColor = True
        '
        'rbtNegociadas
        '
        Me.rbtNegociadas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtNegociadas.Location = New System.Drawing.Point(573, 4)
        Me.rbtNegociadas.Name = "rbtNegociadas"
        Me.rbtNegociadas.Size = New System.Drawing.Size(112, 33)
        Me.rbtNegociadas.TabIndex = 5
        Me.rbtNegociadas.TabStop = True
        Me.rbtNegociadas.Text = "Negociadas"
        Me.rbtNegociadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtNegociadas.UseVisualStyleBackColor = True
        '
        'rbtNegativadas
        '
        Me.rbtNegativadas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtNegativadas.Location = New System.Drawing.Point(455, 4)
        Me.rbtNegativadas.Name = "rbtNegativadas"
        Me.rbtNegativadas.Size = New System.Drawing.Size(112, 33)
        Me.rbtNegativadas.TabIndex = 4
        Me.rbtNegativadas.TabStop = True
        Me.rbtNegativadas.Text = "Negativadas"
        Me.rbtNegativadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtNegativadas.UseVisualStyleBackColor = True
        '
        'rbtCanceladas
        '
        Me.rbtCanceladas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtCanceladas.Location = New System.Drawing.Point(337, 4)
        Me.rbtCanceladas.Name = "rbtCanceladas"
        Me.rbtCanceladas.Size = New System.Drawing.Size(112, 33)
        Me.rbtCanceladas.TabIndex = 3
        Me.rbtCanceladas.TabStop = True
        Me.rbtCanceladas.Text = "Canceladas"
        Me.rbtCanceladas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtCanceladas.UseVisualStyleBackColor = True
        '
        'rbtEmAberto
        '
        Me.rbtEmAberto.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtEmAberto.Location = New System.Drawing.Point(101, 4)
        Me.rbtEmAberto.Name = "rbtEmAberto"
        Me.rbtEmAberto.Size = New System.Drawing.Size(112, 33)
        Me.rbtEmAberto.TabIndex = 1
        Me.rbtEmAberto.TabStop = True
        Me.rbtEmAberto.Text = "Em Aberto"
        Me.rbtEmAberto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtEmAberto.UseVisualStyleBackColor = True
        '
        'rbtQuitadas
        '
        Me.rbtQuitadas.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtQuitadas.Location = New System.Drawing.Point(219, 4)
        Me.rbtQuitadas.Name = "rbtQuitadas"
        Me.rbtQuitadas.Size = New System.Drawing.Size(112, 33)
        Me.rbtQuitadas.TabIndex = 2
        Me.rbtQuitadas.TabStop = True
        Me.rbtQuitadas.Text = "Quitadas"
        Me.rbtQuitadas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtQuitadas.UseVisualStyleBackColor = True
        '
        'cmbCobrancaForma
        '
        Me.cmbCobrancaForma.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCobrancaForma.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCobrancaForma.FormattingEnabled = True
        Me.cmbCobrancaForma.Location = New System.Drawing.Point(156, 68)
        Me.cmbCobrancaForma.Name = "cmbCobrancaForma"
        Me.cmbCobrancaForma.RestrictContentToListItems = True
        Me.cmbCobrancaForma.Size = New System.Drawing.Size(187, 27)
        Me.cmbCobrancaForma.TabIndex = 2
        '
        'btnQuitar
        '
        Me.btnQuitar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuitar.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnQuitar.Image = Global.NovaSiao.My.Resources.Resources.dollar_currency_sign
        Me.btnQuitar.Location = New System.Drawing.Point(491, 599)
        Me.btnQuitar.Name = "btnQuitar"
        Me.btnQuitar.Size = New System.Drawing.Size(143, 41)
        Me.btnQuitar.TabIndex = 11
        Me.btnQuitar.Text = "&Quitar"
        Me.btnQuitar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnQuitar.UseVisualStyleBackColor = True
        '
        'mnuOperacoes
        '
        Me.mnuOperacoes.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QuitarToolStripMenuItem, Me.CancelarToolStripMenuItem, Me.NegativarToolStripMenuItem, Me.NormalizarToolStripMenuItem, Me.ToolStripSeparator2, Me.VerOrigemToolStripMenuItem})
        Me.mnuOperacoes.Name = "mnuOperacoes"
        Me.mnuOperacoes.Size = New System.Drawing.Size(134, 120)
        '
        'QuitarToolStripMenuItem
        '
        Me.QuitarToolStripMenuItem.Name = "QuitarToolStripMenuItem"
        Me.QuitarToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.QuitarToolStripMenuItem.Text = "Quitar"
        '
        'CancelarToolStripMenuItem
        '
        Me.CancelarToolStripMenuItem.Name = "CancelarToolStripMenuItem"
        Me.CancelarToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.CancelarToolStripMenuItem.Text = "Cancelar"
        '
        'NegativarToolStripMenuItem
        '
        Me.NegativarToolStripMenuItem.Name = "NegativarToolStripMenuItem"
        Me.NegativarToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.NegativarToolStripMenuItem.Text = "Negativar"
        '
        'NormalizarToolStripMenuItem
        '
        Me.NormalizarToolStripMenuItem.Name = "NormalizarToolStripMenuItem"
        Me.NormalizarToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.NormalizarToolStripMenuItem.Text = "Normalizar"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(130, 6)
        '
        'VerOrigemToolStripMenuItem
        '
        Me.VerOrigemToolStripMenuItem.Name = "VerOrigemToolStripMenuItem"
        Me.VerOrigemToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.VerOrigemToolStripMenuItem.Text = "Ver Origem"
        '
        'frmAPagarProcurar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(944, 649)
        Me.Controls.Add(Me.cmbCobrancaForma)
        Me.Controls.Add(Me.pnlSituacao)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.btnProcurar)
        Me.Controls.Add(Me.pnlMes)
        Me.Controls.Add(Me.pnlVenda)
        Me.Controls.Add(Me.btnQuitar)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.dgvListagem)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCredorCadastro)
        Me.Name = "frmAPagarProcurar"
        Me.Text = "Procurar Saída de Produto"
        Me.Controls.SetChildIndex(Me.txtCredorCadastro, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.dgvListagem, 0)
        Me.Controls.SetChildIndex(Me.btnCancelar, 0)
        Me.Controls.SetChildIndex(Me.btnEditar, 0)
        Me.Controls.SetChildIndex(Me.btnQuitar, 0)
        Me.Controls.SetChildIndex(Me.pnlVenda, 0)
        Me.Controls.SetChildIndex(Me.pnlMes, 0)
        Me.Controls.SetChildIndex(Me.btnProcurar, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.pnlSituacao, 0)
        Me.Controls.SetChildIndex(Me.cmbCobrancaForma, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.dgvListagem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlVenda.ResumeLayout(False)
        Me.pnlVenda.PerformLayout()
        Me.pnlMes.ResumeLayout(False)
        Me.pnlSituacao.ResumeLayout(False)
        Me.mnuOperacoes.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCredorCadastro As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents chkPeriodoTodos As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblPeriodo As Label
    Friend WithEvents btnPeriodoAnterior As VIBlend.WinForms.Controls.vArrowButton
    Friend WithEvents btnPeriodoPosterior As VIBlend.WinForms.Controls.vArrowButton
    Friend WithEvents dgvListagem As DataGridView
    Friend WithEvents btnCancelar As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents pnlVenda As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents btnMesAtual As VIBlend.WinForms.Controls.vButton
    Friend WithEvents pnlMes As Panel
    Friend WithEvents dtMes As MonthCalendar
    Friend WithEvents btnProcurar As VIBlend.WinForms.Controls.vButton
    Friend WithEvents btnClose As VIBlend.WinForms.Controls.vFormButton
    Friend WithEvents txtDespesaTipo As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents pnlSituacao As Panel
    Friend WithEvents rbtTodas As RadioButton
    Friend WithEvents rbtNegociadas As RadioButton
    Friend WithEvents rbtNegativadas As RadioButton
    Friend WithEvents rbtCanceladas As RadioButton
    Friend WithEvents rbtEmAberto As RadioButton
    Friend WithEvents rbtQuitadas As RadioButton
    Friend WithEvents cmbCobrancaForma As Controles.ComboBox_OnlyValues
    Friend WithEvents clnVencimento As DataGridViewTextBoxColumn
    Friend WithEvents clnCredor As DataGridViewTextBoxColumn
    Friend WithEvents clnOrigem As DataGridViewTextBoxColumn
    Friend WithEvents clnForma As DataGridViewTextBoxColumn
    Friend WithEvents clnValor As DataGridViewTextBoxColumn
    Friend WithEvents clnSituacao As DataGridViewTextBoxColumn
    Friend WithEvents btnQuitar As Button
    Friend WithEvents mnuOperacoes As ContextMenuStrip
    Friend WithEvents QuitarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CancelarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NegativarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NormalizarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents VerOrigemToolStripMenuItem As ToolStripMenuItem
End Class
