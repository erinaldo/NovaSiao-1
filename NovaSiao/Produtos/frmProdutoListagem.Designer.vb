<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProdutoListagem
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
        Me.txtAutor = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.btnEditar = New System.Windows.Forms.Button()
        Me.btnClose = New VIBlend.WinForms.Controls.vFormButton()
        Me.btnTipo = New VIBlend.WinForms.Controls.vButton()
        Me.txtProdutoTipo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnNova = New System.Windows.Forms.Button()
        Me.pnlAtivas = New System.Windows.Forms.Panel()
        Me.rbtInativas = New System.Windows.Forms.RadioButton()
        Me.rbtAtivas = New System.Windows.Forms.RadioButton()
        Me.mnuListagem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtProduto = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbIDSituacao = New Controles.ComboBox_OnlyValues()
        Me.lblFilial = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lstListagem = New ComponentOwl.BetterListView.BetterListView()
        Me.clnRGProduto = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnPreco = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnEstoque = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnEstoqueMinimo = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnEstoqueIdeal = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnProduto = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.clnAutor = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.chkAlterarSituacao = New System.Windows.Forms.CheckBox()
        Me.btnPrintEtiquetas = New System.Windows.Forms.Button()
        Me.btnPrintListagem = New System.Windows.Forms.Button()
        Me.clnAtivo = New ComponentOwl.BetterListView.BetterListViewColumnHeader()
        Me.txtProdutoCategoria = New System.Windows.Forms.TextBox()
        Me.txtProdutoSubTipo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.pnlAtivas.SuspendLayout()
        CType(Me.lstListagem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblFilial)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Size = New System.Drawing.Size(1022, 50)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnClose, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label6, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblFilial, 0)
        '
        'lblTitulo
        '
        Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.None
        Me.lblTitulo.Location = New System.Drawing.Point(683, 9)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(301, 34)
        Me.lblTitulo.TabIndex = 2
        Me.lblTitulo.Text = "Produto Procura Avançada"
        '
        'txtAutor
        '
        Me.txtAutor.Location = New System.Drawing.Point(134, 554)
        Me.txtAutor.Name = "txtAutor"
        Me.txtAutor.Size = New System.Drawing.Size(355, 27)
        Me.txtAutor.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(84, 557)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 19)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Autor"
        '
        'btnFechar
        '
        Me.btnFechar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnFechar.ForeColor = System.Drawing.Color.DarkRed
        Me.btnFechar.Image = Global.NovaSiao.My.Resources.Resources.block
        Me.btnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFechar.Location = New System.Drawing.Point(789, 621)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(142, 41)
        Me.btnFechar.TabIndex = 17
        Me.btnFechar.Text = "&Fechar"
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnEditar.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnEditar.Image = Global.NovaSiao.My.Resources.Resources.editar
        Me.btnEditar.Location = New System.Drawing.Point(491, 621)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(142, 41)
        Me.btnEditar.TabIndex = 15
        Me.btnEditar.Text = "&Editar"
        Me.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.AllowAnimations = True
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.ButtonType = VIBlend.WinForms.Controls.vFormButtonType.CloseButton
        Me.btnClose.ForeColor = System.Drawing.Color.Firebrick
        Me.btnClose.Location = New System.Drawing.Point(991, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RibbonStyle = False
        Me.btnClose.RoundedCornersMask = CType(15, Byte)
        Me.btnClose.ShowFocusRectangle = False
        Me.btnClose.Size = New System.Drawing.Size(20, 20)
        Me.btnClose.TabIndex = 3
        Me.btnClose.TabStop = False
        Me.btnClose.UseVisualStyleBackColor = False
        Me.btnClose.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2003SILVER
        '
        'btnTipo
        '
        Me.btnTipo.AllowAnimations = True
        Me.btnTipo.BackColor = System.Drawing.Color.Transparent
        Me.btnTipo.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.btnTipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTipo.Location = New System.Drawing.Point(898, 491)
        Me.btnTipo.Name = "btnTipo"
        Me.btnTipo.RoundedCornersMask = CType(15, Byte)
        Me.btnTipo.RoundedCornersRadius = 0
        Me.btnTipo.Size = New System.Drawing.Size(34, 27)
        Me.btnTipo.TabIndex = 3
        Me.btnTipo.TabStop = False
        Me.btnTipo.Text = "..."
        Me.btnTipo.UseCompatibleTextRendering = True
        Me.btnTipo.UseVisualStyleBackColor = False
        Me.btnTipo.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'txtProdutoTipo
        '
        Me.txtProdutoTipo.Location = New System.Drawing.Point(688, 491)
        Me.txtProdutoTipo.Name = "txtProdutoTipo"
        Me.txtProdutoTipo.Size = New System.Drawing.Size(199, 27)
        Me.txtProdutoTipo.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(571, 494)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(111, 19)
        Me.Label12.TabIndex = 1
        Me.Label12.Text = "Tipo de Produto"
        '
        'btnNova
        '
        Me.btnNova.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNova.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNova.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnNova.Image = Global.NovaSiao.My.Resources.Resources.add_24x24
        Me.btnNova.Location = New System.Drawing.Point(640, 621)
        Me.btnNova.Name = "btnNova"
        Me.btnNova.Size = New System.Drawing.Size(142, 41)
        Me.btnNova.TabIndex = 16
        Me.btnNova.Text = "&Nova"
        Me.btnNova.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNova.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNova.UseVisualStyleBackColor = True
        '
        'pnlAtivas
        '
        Me.pnlAtivas.BackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.pnlAtivas.Controls.Add(Me.rbtInativas)
        Me.pnlAtivas.Controls.Add(Me.rbtAtivas)
        Me.pnlAtivas.Location = New System.Drawing.Point(613, 61)
        Me.pnlAtivas.Name = "pnlAtivas"
        Me.pnlAtivas.Size = New System.Drawing.Size(253, 39)
        Me.pnlAtivas.TabIndex = 8
        '
        'rbtInativas
        '
        Me.rbtInativas.AutoSize = True
        Me.rbtInativas.Location = New System.Drawing.Point(128, 7)
        Me.rbtInativas.Name = "rbtInativas"
        Me.rbtInativas.Size = New System.Drawing.Size(98, 23)
        Me.rbtInativas.TabIndex = 1
        Me.rbtInativas.TabStop = True
        Me.rbtInativas.Text = "Concluídas"
        Me.rbtInativas.UseVisualStyleBackColor = True
        '
        'rbtAtivas
        '
        Me.rbtAtivas.AutoSize = True
        Me.rbtAtivas.Location = New System.Drawing.Point(29, 7)
        Me.rbtAtivas.Name = "rbtAtivas"
        Me.rbtAtivas.Size = New System.Drawing.Size(67, 23)
        Me.rbtAtivas.TabIndex = 0
        Me.rbtAtivas.TabStop = True
        Me.rbtAtivas.Text = "Ativas"
        Me.rbtAtivas.UseVisualStyleBackColor = True
        '
        'mnuListagem
        '
        Me.mnuListagem.Name = "MenuFab"
        Me.mnuListagem.Size = New System.Drawing.Size(61, 4)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(543, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 19)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Situação"
        '
        'txtProduto
        '
        Me.txtProduto.Location = New System.Drawing.Point(134, 521)
        Me.txtProduto.Name = "txtProduto"
        Me.txtProduto.Size = New System.Drawing.Size(355, 27)
        Me.txtProduto.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(69, 524)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Produto"
        '
        'cmbIDSituacao
        '
        Me.cmbIDSituacao.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIDSituacao.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIDSituacao.FormattingEnabled = True
        Me.cmbIDSituacao.Location = New System.Drawing.Point(613, 106)
        Me.cmbIDSituacao.Name = "cmbIDSituacao"
        Me.cmbIDSituacao.RestrictContentToListItems = True
        Me.cmbIDSituacao.Size = New System.Drawing.Size(253, 27)
        Me.cmbIDSituacao.TabIndex = 10
        '
        'lblFilial
        '
        Me.lblFilial.BackColor = System.Drawing.Color.Transparent
        Me.lblFilial.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilial.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblFilial.Location = New System.Drawing.Point(7, 17)
        Me.lblFilial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilial.Name = "lblFilial"
        Me.lblFilial.Size = New System.Drawing.Size(204, 30)
        Me.lblFilial.TabIndex = 0
        Me.lblFilial.Text = "Filial"
        Me.lblFilial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Silver
        Me.Label6.Location = New System.Drawing.Point(90, 4)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Filial"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lstListagem
        '
        Me.lstListagem.BackColor = System.Drawing.Color.White
        Me.lstListagem.CheckBoxes = ComponentOwl.BetterListView.BetterListViewCheckBoxes.TwoState
        Me.lstListagem.Columns.Add(Me.clnRGProduto)
        Me.lstListagem.Columns.Add(Me.clnProduto)
        Me.lstListagem.Columns.Add(Me.clnAutor)
        Me.lstListagem.Columns.Add(Me.clnPreco)
        Me.lstListagem.Columns.Add(Me.clnEstoque)
        Me.lstListagem.Columns.Add(Me.clnEstoqueMinimo)
        Me.lstListagem.Columns.Add(Me.clnEstoqueIdeal)
        Me.lstListagem.Columns.Add(Me.clnAtivo)
        Me.lstListagem.Font = New System.Drawing.Font("Pathway Gothic One", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstListagem.ForeColorColumns = System.Drawing.Color.Black
        Me.lstListagem.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.KeepSelection
        Me.lstListagem.Location = New System.Drawing.Point(12, 171)
        Me.lstListagem.Margin = New System.Windows.Forms.Padding(3, 3, 3, 10)
        Me.lstListagem.MultiSelect = False
        Me.lstListagem.Name = "lstListagem"
        Me.lstListagem.Size = New System.Drawing.Size(998, 304)
        Me.lstListagem.TabIndex = 11
        '
        'clnRGProduto
        '
        Me.clnRGProduto.AllowResize = False
        Me.clnRGProduto.ForeColor = System.Drawing.Color.Black
        Me.clnRGProduto.Name = "clnRGProduto"
        Me.clnRGProduto.Text = "Reg."
        '
        'clnPreco
        '
        Me.clnPreco.AllowResize = False
        Me.clnPreco.Name = "clnPreco"
        Me.clnPreco.Text = "Preço"
        '
        'clnEstoque
        '
        Me.clnEstoque.AllowResize = False
        Me.clnEstoque.Name = "clnEstoque"
        Me.clnEstoque.Text = "Estoque"
        '
        'clnEstoqueMinimo
        '
        Me.clnEstoqueMinimo.AllowResize = False
        Me.clnEstoqueMinimo.Name = "clnEstoqueMinimo"
        Me.clnEstoqueMinimo.Text = "E.Min."
        '
        'clnEstoqueIdeal
        '
        Me.clnEstoqueIdeal.AllowResize = False
        Me.clnEstoqueIdeal.Name = "clnEstoqueIdeal"
        Me.clnEstoqueIdeal.Text = "E.Ideal"
        '
        'clnProduto
        '
        Me.clnProduto.Name = "clnProduto"
        Me.clnProduto.Text = "Produto"
        '
        'clnAutor
        '
        Me.clnAutor.Name = "clnAutor"
        Me.clnAutor.Text = "Autor"
        '
        'chkAlterarSituacao
        '
        Me.chkAlterarSituacao.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAlterarSituacao.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkAlterarSituacao.Enabled = False
        Me.chkAlterarSituacao.Image = Global.NovaSiao.My.Resources.Resources.refresh1
        Me.chkAlterarSituacao.Location = New System.Drawing.Point(12, 621)
        Me.chkAlterarSituacao.Name = "chkAlterarSituacao"
        Me.chkAlterarSituacao.Size = New System.Drawing.Size(166, 41)
        Me.chkAlterarSituacao.TabIndex = 12
        Me.chkAlterarSituacao.Text = "Alterar Situação"
        Me.chkAlterarSituacao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkAlterarSituacao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.chkAlterarSituacao.UseVisualStyleBackColor = True
        '
        'btnPrintEtiquetas
        '
        Me.btnPrintEtiquetas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintEtiquetas.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPrintEtiquetas.Enabled = False
        Me.btnPrintEtiquetas.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnPrintEtiquetas.Image = Global.NovaSiao.My.Resources.Resources.print
        Me.btnPrintEtiquetas.Location = New System.Drawing.Point(185, 621)
        Me.btnPrintEtiquetas.Name = "btnPrintEtiquetas"
        Me.btnPrintEtiquetas.Size = New System.Drawing.Size(120, 41)
        Me.btnPrintEtiquetas.TabIndex = 13
        Me.btnPrintEtiquetas.Text = "Etiquetas"
        Me.btnPrintEtiquetas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrintEtiquetas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPrintEtiquetas.UseVisualStyleBackColor = True
        '
        'btnPrintListagem
        '
        Me.btnPrintListagem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintListagem.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnPrintListagem.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnPrintListagem.Image = Global.NovaSiao.My.Resources.Resources.print
        Me.btnPrintListagem.Location = New System.Drawing.Point(312, 621)
        Me.btnPrintListagem.Name = "btnPrintListagem"
        Me.btnPrintListagem.Size = New System.Drawing.Size(120, 41)
        Me.btnPrintListagem.TabIndex = 14
        Me.btnPrintListagem.Text = "Listagem"
        Me.btnPrintListagem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrintListagem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPrintListagem.UseVisualStyleBackColor = True
        '
        'clnAtivo
        '
        Me.clnAtivo.Name = "clnAtivo"
        Me.clnAtivo.Text = "Ativo"
        '
        'txtProdutoCategoria
        '
        Me.txtProdutoCategoria.Location = New System.Drawing.Point(688, 557)
        Me.txtProdutoCategoria.Name = "txtProdutoCategoria"
        Me.txtProdutoCategoria.Size = New System.Drawing.Size(199, 27)
        Me.txtProdutoCategoria.TabIndex = 21
        '
        'txtProdutoSubTipo
        '
        Me.txtProdutoSubTipo.Location = New System.Drawing.Point(688, 524)
        Me.txtProdutoSubTipo.Name = "txtProdutoSubTipo"
        Me.txtProdutoSubTipo.Size = New System.Drawing.Size(199, 27)
        Me.txtProdutoSubTipo.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(622, 527)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 19)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "SubTipo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(610, 560)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 19)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Categoria"
        '
        'frmProdutoListagem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(1022, 673)
        Me.Controls.Add(Me.txtProdutoCategoria)
        Me.Controls.Add(Me.txtProdutoSubTipo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkAlterarSituacao)
        Me.Controls.Add(Me.lstListagem)
        Me.Controls.Add(Me.txtProduto)
        Me.Controls.Add(Me.cmbIDSituacao)
        Me.Controls.Add(Me.pnlAtivas)
        Me.Controls.Add(Me.btnTipo)
        Me.Controls.Add(Me.txtProdutoTipo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.btnNova)
        Me.Controls.Add(Me.btnPrintListagem)
        Me.Controls.Add(Me.btnPrintEtiquetas)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtAutor)
        Me.Name = "frmProdutoListagem"
        Me.Text = "Procurar Saída de Produto"
        Me.Controls.SetChildIndex(Me.txtAutor, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.btnFechar, 0)
        Me.Controls.SetChildIndex(Me.btnEditar, 0)
        Me.Controls.SetChildIndex(Me.btnPrintEtiquetas, 0)
        Me.Controls.SetChildIndex(Me.btnPrintListagem, 0)
        Me.Controls.SetChildIndex(Me.btnNova, 0)
        Me.Controls.SetChildIndex(Me.Label12, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.txtProdutoTipo, 0)
        Me.Controls.SetChildIndex(Me.btnTipo, 0)
        Me.Controls.SetChildIndex(Me.pnlAtivas, 0)
        Me.Controls.SetChildIndex(Me.cmbIDSituacao, 0)
        Me.Controls.SetChildIndex(Me.txtProduto, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.lstListagem, 0)
        Me.Controls.SetChildIndex(Me.chkAlterarSituacao, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.txtProdutoSubTipo, 0)
        Me.Controls.SetChildIndex(Me.txtProdutoCategoria, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlAtivas.ResumeLayout(False)
        Me.pnlAtivas.PerformLayout()
        CType(Me.lstListagem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtAutor As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnFechar As Button
    Friend WithEvents btnEditar As Button
    Friend WithEvents btnClose As VIBlend.WinForms.Controls.vFormButton
    Friend WithEvents btnTipo As VIBlend.WinForms.Controls.vButton
    Friend WithEvents txtProdutoTipo As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents btnNova As Button
    Friend WithEvents pnlAtivas As Panel
    Friend WithEvents rbtInativas As RadioButton
    Friend WithEvents rbtAtivas As RadioButton
    Friend WithEvents mnuListagem As ContextMenuStrip
    Friend WithEvents cmbIDSituacao As Controles.ComboBox_OnlyValues
    Friend WithEvents Label1 As Label
    Friend WithEvents txtProduto As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblFilial As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lstListagem As ComponentOwl.BetterListView.BetterListView
    Friend WithEvents clnRGProduto As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnPreco As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnEstoque As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnEstoqueMinimo As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnEstoqueIdeal As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnProduto As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents clnAutor As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents chkAlterarSituacao As CheckBox
    Friend WithEvents btnPrintEtiquetas As Button
    Friend WithEvents btnPrintListagem As Button
    Friend WithEvents clnAtivo As ComponentOwl.BetterListView.BetterListViewColumnHeader
    Friend WithEvents txtProdutoCategoria As TextBox
    Friend WithEvents txtProdutoSubTipo As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
End Class
