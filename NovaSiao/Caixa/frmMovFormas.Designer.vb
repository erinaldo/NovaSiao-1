<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMovFormas
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMovFormas))
        Me.lstFormas = New ComponentOwl.BetterListView.BetterListView()
        Me.clnIDMovForma = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnMovForma = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.txtMovForma = New System.Windows.Forms.TextBox()
        Me.txtComissao = New Controles.Text_SoNumeros()
        Me.txtNoDias = New Controles.Text_SoNumeros()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.tsMenu = New System.Windows.Forms.ToolStrip()
        Me.btnNovo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSalvar = New System.Windows.Forms.ToolStripButton()
        Me.btnCancelar = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnExcluir = New System.Windows.Forms.ToolStripButton()
        Me.btnAtivo = New System.Windows.Forms.ToolStripButton()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblIDMovForma = New System.Windows.Forms.Label()
        Me.epValida = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtParcelas = New Controles.Text_SoNumeros()
        Me.btnClose = New VIBlend.WinForms.Controls.vFormButton()
        Me.btnMovTipos = New VIBlend.WinForms.Controls.vButton()
        Me.txtMovTipo = New System.Windows.Forms.TextBox()
        Me.txtOperadora = New System.Windows.Forms.TextBox()
        Me.txtCartao = New System.Windows.Forms.TextBox()
        Me.btnOperadora = New VIBlend.WinForms.Controls.vButton()
        Me.btnCartao = New VIBlend.WinForms.Controls.vButton()
        Me.Panel1.SuspendLayout()
        CType(Me.lstFormas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.tsMenu.SuspendLayout()
        CType(Me.epValida, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Size = New System.Drawing.Size(740, 50)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnClose, 0)
        '
        'lblTitulo
        '
        Me.lblTitulo.Location = New System.Drawing.Point(455, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(285, 50)
        Me.lblTitulo.Text = "Formas de Movimentação"
        '
        'lstFormas
        '
        Me.lstFormas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstFormas.ColorSortedColumn = System.Drawing.Color.LightBlue
        Me.lstFormas.Columns.Add(Me.clnIDMovForma)
        Me.lstFormas.Columns.Add(Me.clnMovForma)
        Me.lstFormas.DisplayMember = "IDMovForma"
        Me.lstFormas.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstFormas.FontColumns = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstFormas.ForeColor = System.Drawing.Color.Black
        Me.lstFormas.ForeColorColumns = System.Drawing.Color.Black
        Me.lstFormas.ForeColorGroups = System.Drawing.SystemColors.MenuHighlight
        Me.lstFormas.GridLines = ComponentOwl.BetterListView.BetterListViewGridLines.Grid
        Me.lstFormas.HeaderStyle = ComponentOwl.BetterListView.BetterListViewHeaderStyle.Nonclickable
        Me.lstFormas.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.KeepSelection
        Me.lstFormas.Location = New System.Drawing.Point(12, 65)
        Me.lstFormas.MultiSelect = False
        Me.lstFormas.Name = "lstFormas"
        Me.lstFormas.Size = New System.Drawing.Size(324, 285)
        Me.lstFormas.TabIndex = 1
        '
        'clnIDMovForma
        '
        Me.clnIDMovForma.AllowResize = False
        Me.clnIDMovForma.DisplayMember = "IDMovForma"
        Me.clnIDMovForma.Name = "clnIDMovForma"
        Me.clnIDMovForma.Text = "Reg."
        Me.clnIDMovForma.ValueMember = "IDMovForma"
        Me.clnIDMovForma.Width = 50
        '
        'clnMovForma
        '
        Me.clnMovForma.AllowResize = False
        Me.clnMovForma.DisplayMember = "MovForma"
        Me.clnMovForma.Name = "clnMovForma"
        Me.clnMovForma.Text = "Descrição"
        Me.clnMovForma.ValueMember = "MovForma"
        Me.clnMovForma.Width = 220
        '
        'txtMovForma
        '
        Me.txtMovForma.Location = New System.Drawing.Point(466, 98)
        Me.txtMovForma.MaxLength = 30
        Me.txtMovForma.Name = "txtMovForma"
        Me.txtMovForma.Size = New System.Drawing.Size(234, 27)
        Me.txtMovForma.TabIndex = 5
        '
        'txtComissao
        '
        Me.txtComissao.Inteiro = False
        Me.txtComissao.Location = New System.Drawing.Point(632, 230)
        Me.txtComissao.MaxLength = 10
        Me.txtComissao.Name = "txtComissao"
        Me.txtComissao.Size = New System.Drawing.Size(68, 27)
        Me.txtComissao.TabIndex = 18
        Me.txtComissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNoDias
        '
        Me.txtNoDias.Inteiro = True
        Me.txtNoDias.Location = New System.Drawing.Point(632, 263)
        Me.txtNoDias.Name = "txtNoDias"
        Me.txtNoDias.Size = New System.Drawing.Size(68, 27)
        Me.txtNoDias.TabIndex = 20
        Me.txtNoDias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(423, 134)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 19)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Tipo"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(387, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Descrição"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(382, 167)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 19)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Operadora"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(356, 200)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 19)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Tipo do Cartão"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(396, 233)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 19)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Parcelas"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(529, 233)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(97, 19)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Comissão (%)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(470, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(156, 19)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Primeira Parcela (dias)"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 366)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.tsMenu)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnFechar)
        Me.SplitContainer1.Size = New System.Drawing.Size(740, 48)
        Me.SplitContainer1.SplitterDistance = 632
        Me.SplitContainer1.TabIndex = 9
        Me.SplitContainer1.TabStop = False
        '
        'tsMenu
        '
        Me.tsMenu.AutoSize = False
        Me.tsMenu.BackColor = System.Drawing.Color.Transparent
        Me.tsMenu.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tsMenu.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tsMenu.ImageScalingSize = New System.Drawing.Size(30, 30)
        Me.tsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNovo, Me.ToolStripSeparator5, Me.btnSalvar, Me.btnCancelar, Me.ToolStripSeparator1, Me.btnExcluir, Me.btnAtivo})
        Me.tsMenu.Location = New System.Drawing.Point(0, 0)
        Me.tsMenu.Name = "tsMenu"
        Me.tsMenu.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.tsMenu.Size = New System.Drawing.Size(632, 48)
        Me.tsMenu.TabIndex = 0
        Me.tsMenu.TabStop = True
        Me.tsMenu.Text = "Menu Cliente PF"
        '
        'btnNovo
        '
        Me.btnNovo.Image = Global.NovaSiao.My.Resources.Resources.Adicionar1
        Me.btnNovo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNovo.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(76, 45)
        Me.btnNovo.Text = "&Novo"
        Me.btnNovo.ToolTipText = "Nova Forma"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 48)
        '
        'btnSalvar
        '
        Me.btnSalvar.Enabled = False
        Me.btnSalvar.Image = Global.NovaSiao.My.Resources.Resources.Salvar_PEQ
        Me.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSalvar.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(82, 45)
        Me.btnSalvar.Text = "&Salvar"
        Me.btnSalvar.ToolTipText = "Salvar Edição"
        '
        'btnCancelar
        '
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.Image = Global.NovaSiao.My.Resources.Resources.delete_page
        Me.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(100, 45)
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.ToolTipText = "Cancelar Edição"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 48)
        '
        'btnExcluir
        '
        Me.btnExcluir.Image = Global.NovaSiao.My.Resources.Resources.Lixeira
        Me.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnExcluir.Margin = New System.Windows.Forms.Padding(3, 1, 0, 2)
        Me.btnExcluir.Name = "btnExcluir"
        Me.btnExcluir.Size = New System.Drawing.Size(86, 45)
        Me.btnExcluir.Text = "E&xcluir"
        Me.btnExcluir.ToolTipText = "Excluir Forma"
        '
        'btnAtivo
        '
        Me.btnAtivo.AutoSize = False
        Me.btnAtivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnAtivo.CheckOnClick = True
        Me.btnAtivo.Image = Global.NovaSiao.My.Resources.Resources.Switch_ON_PEQ
        Me.btnAtivo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.btnAtivo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAtivo.Name = "btnAtivo"
        Me.btnAtivo.Size = New System.Drawing.Size(167, 41)
        Me.btnAtivo.Text = "Forma Ativa"
        Me.btnAtivo.ToolTipText = "Ativar/Desativar Forma"
        '
        'btnFechar
        '
        Me.btnFechar.BackColor = System.Drawing.Color.Transparent
        Me.btnFechar.CausesValidation = False
        Me.btnFechar.FlatAppearance.BorderSize = 0
        Me.btnFechar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightCoral
        Me.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue
        Me.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFechar.Image = Global.NovaSiao.My.Resources.Resources.Fechar
        Me.btnFechar.Location = New System.Drawing.Point(4, 0)
        Me.btnFechar.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(97, 48)
        Me.btnFechar.TabIndex = 0
        Me.btnFechar.Text = "&Fechar"
        Me.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(418, 69)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 19)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Reg."
        '
        'lblIDMovForma
        '
        Me.lblIDMovForma.BackColor = System.Drawing.Color.White
        Me.lblIDMovForma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIDMovForma.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDMovForma.Location = New System.Drawing.Point(466, 65)
        Me.lblIDMovForma.Margin = New System.Windows.Forms.Padding(0)
        Me.lblIDMovForma.Name = "lblIDMovForma"
        Me.lblIDMovForma.Size = New System.Drawing.Size(63, 27)
        Me.lblIDMovForma.TabIndex = 3
        Me.lblIDMovForma.Text = "0000"
        Me.lblIDMovForma.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'epValida
        '
        Me.epValida.ContainerControl = Me
        '
        'txtParcelas
        '
        Me.txtParcelas.Inteiro = True
        Me.txtParcelas.Location = New System.Drawing.Point(466, 230)
        Me.txtParcelas.Name = "txtParcelas"
        Me.txtParcelas.Size = New System.Drawing.Size(45, 27)
        Me.txtParcelas.TabIndex = 16
        Me.txtParcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnClose
        '
        Me.btnClose.AllowAnimations = True
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.NONE
        Me.btnClose.ButtonType = VIBlend.WinForms.Controls.vFormButtonType.CloseButton
        Me.btnClose.CausesValidation = False
        Me.btnClose.Location = New System.Drawing.Point(12, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RibbonStyle = False
        Me.btnClose.RoundedCornersMask = CType(15, Byte)
        Me.btnClose.ShowFocusRectangle = False
        Me.btnClose.Size = New System.Drawing.Size(19, 20)
        Me.btnClose.TabIndex = 27
        Me.btnClose.TabStop = False
        Me.btnClose.UseVisualStyleBackColor = False
        Me.btnClose.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.BLUEBLEND
        '
        'btnMovTipos
        '
        Me.btnMovTipos.AllowAnimations = True
        Me.btnMovTipos.BackColor = System.Drawing.Color.Transparent
        Me.btnMovTipos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnMovTipos.FlatAppearance.BorderSize = 0
        Me.btnMovTipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMovTipos.Image = CType(resources.GetObject("btnMovTipos.Image"), System.Drawing.Image)
        Me.btnMovTipos.ImageAbsolutePosition = New System.Drawing.Point(0, 0)
        Me.btnMovTipos.Location = New System.Drawing.Point(673, 131)
        Me.btnMovTipos.Name = "btnMovTipos"
        Me.btnMovTipos.RoundedCornersMask = CType(15, Byte)
        Me.btnMovTipos.RoundedCornersRadius = 2
        Me.btnMovTipos.Size = New System.Drawing.Size(27, 27)
        Me.btnMovTipos.StretchImage = True
        Me.btnMovTipos.TabIndex = 8
        Me.btnMovTipos.TabStop = False
        Me.btnMovTipos.UseAbsoluteImagePositioning = True
        Me.btnMovTipos.UseVisualStyleBackColor = True
        Me.btnMovTipos.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'txtMovTipo
        '
        Me.txtMovTipo.Location = New System.Drawing.Point(466, 131)
        Me.txtMovTipo.Name = "txtMovTipo"
        Me.txtMovTipo.Size = New System.Drawing.Size(201, 27)
        Me.txtMovTipo.TabIndex = 7
        '
        'txtOperadora
        '
        Me.txtOperadora.Location = New System.Drawing.Point(466, 164)
        Me.txtOperadora.Name = "txtOperadora"
        Me.txtOperadora.Size = New System.Drawing.Size(201, 27)
        Me.txtOperadora.TabIndex = 10
        '
        'txtCartao
        '
        Me.txtCartao.Location = New System.Drawing.Point(466, 197)
        Me.txtCartao.Name = "txtCartao"
        Me.txtCartao.Size = New System.Drawing.Size(201, 27)
        Me.txtCartao.TabIndex = 13
        '
        'btnOperadora
        '
        Me.btnOperadora.AllowAnimations = True
        Me.btnOperadora.BackColor = System.Drawing.Color.Transparent
        Me.btnOperadora.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnOperadora.FlatAppearance.BorderSize = 0
        Me.btnOperadora.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOperadora.Image = CType(resources.GetObject("btnOperadora.Image"), System.Drawing.Image)
        Me.btnOperadora.ImageAbsolutePosition = New System.Drawing.Point(0, 0)
        Me.btnOperadora.Location = New System.Drawing.Point(673, 164)
        Me.btnOperadora.Name = "btnOperadora"
        Me.btnOperadora.RoundedCornersMask = CType(15, Byte)
        Me.btnOperadora.RoundedCornersRadius = 2
        Me.btnOperadora.Size = New System.Drawing.Size(27, 27)
        Me.btnOperadora.StretchImage = True
        Me.btnOperadora.TabIndex = 11
        Me.btnOperadora.TabStop = False
        Me.btnOperadora.UseAbsoluteImagePositioning = True
        Me.btnOperadora.UseVisualStyleBackColor = True
        Me.btnOperadora.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'btnCartao
        '
        Me.btnCartao.AllowAnimations = True
        Me.btnCartao.BackColor = System.Drawing.Color.Transparent
        Me.btnCartao.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCartao.FlatAppearance.BorderSize = 0
        Me.btnCartao.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCartao.Image = CType(resources.GetObject("btnCartao.Image"), System.Drawing.Image)
        Me.btnCartao.ImageAbsolutePosition = New System.Drawing.Point(0, 0)
        Me.btnCartao.Location = New System.Drawing.Point(673, 197)
        Me.btnCartao.Name = "btnCartao"
        Me.btnCartao.RoundedCornersMask = CType(15, Byte)
        Me.btnCartao.RoundedCornersRadius = 2
        Me.btnCartao.Size = New System.Drawing.Size(27, 27)
        Me.btnCartao.StretchImage = True
        Me.btnCartao.TabIndex = 14
        Me.btnCartao.TabStop = False
        Me.btnCartao.UseAbsoluteImagePositioning = True
        Me.btnCartao.UseVisualStyleBackColor = True
        Me.btnCartao.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'frmMovFormas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(740, 414)
        Me.Controls.Add(Me.btnCartao)
        Me.Controls.Add(Me.btnOperadora)
        Me.Controls.Add(Me.txtCartao)
        Me.Controls.Add(Me.txtOperadora)
        Me.Controls.Add(Me.txtMovTipo)
        Me.Controls.Add(Me.btnMovTipos)
        Me.Controls.Add(Me.txtParcelas)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblIDMovForma)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNoDias)
        Me.Controls.Add(Me.txtComissao)
        Me.Controls.Add(Me.txtMovForma)
        Me.Controls.Add(Me.lstFormas)
        Me.KeyPreview = True
        Me.Name = "frmMovFormas"
        Me.Text = "Formas de Pagamento"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.lstFormas, 0)
        Me.Controls.SetChildIndex(Me.txtMovForma, 0)
        Me.Controls.SetChildIndex(Me.txtComissao, 0)
        Me.Controls.SetChildIndex(Me.txtNoDias, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.SplitContainer1, 0)
        Me.Controls.SetChildIndex(Me.lblIDMovForma, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.txtParcelas, 0)
        Me.Controls.SetChildIndex(Me.btnMovTipos, 0)
        Me.Controls.SetChildIndex(Me.txtMovTipo, 0)
        Me.Controls.SetChildIndex(Me.txtOperadora, 0)
        Me.Controls.SetChildIndex(Me.txtCartao, 0)
        Me.Controls.SetChildIndex(Me.btnOperadora, 0)
        Me.Controls.SetChildIndex(Me.btnCartao, 0)
        Me.Panel1.ResumeLayout(False)
        CType(Me.lstFormas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.tsMenu.ResumeLayout(False)
        Me.tsMenu.PerformLayout()
        CType(Me.epValida, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lstFormas As ComponentOwl.BetterListView.BetterListView
    Friend WithEvents clnIDMovForma As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnMovForma As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents txtMovForma As TextBox
    Friend WithEvents txtComissao As Controles.Text_SoNumeros
    Friend WithEvents txtNoDias As Controles.Text_SoNumeros
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents tsMenu As ToolStrip
    Friend WithEvents btnNovo As ToolStripButton
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents btnSalvar As ToolStripButton
    Friend WithEvents btnCancelar As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnExcluir As ToolStripButton
    Friend WithEvents btnAtivo As ToolStripButton
    Friend WithEvents btnFechar As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents lblIDMovForma As Label
    Friend WithEvents epValida As ErrorProvider
    Friend WithEvents txtParcelas As Controles.Text_SoNumeros
    Friend WithEvents btnClose As VIBlend.WinForms.Controls.vFormButton
    Friend WithEvents btnMovTipos As VIBlend.WinForms.Controls.vButton
    Friend WithEvents txtCartao As TextBox
    Friend WithEvents txtOperadora As TextBox
    Friend WithEvents txtMovTipo As TextBox
    Friend WithEvents btnCartao As VIBlend.WinForms.Controls.vButton
    Friend WithEvents btnOperadora As VIBlend.WinForms.Controls.vButton
End Class
