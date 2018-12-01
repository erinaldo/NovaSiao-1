<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSimplesSaida
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tabPrincipal = New VIBlend.WinForms.Controls.vTabControl()
        Me.vtab1 = New VIBlend.WinForms.Controls.vTabPage()
        Me.dgvItens = New System.Windows.Forms.DataGridView()
        Me.clnIDTransacaoItem = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnRGProduto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnProduto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnQuantidade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnPreco = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnSubTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnDesconto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.vtab2 = New VIBlend.WinForms.Controls.vTabPage()
        Me.dgvAReceber = New System.Windows.Forms.DataGridView()
        Me.clnID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnReg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnVencimento = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnPermanencia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnValor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtObservacao = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotalCobrado = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbIDPlano = New Controles.ComboBox_OnlyValues()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape3 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.LineShape1 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.vtab3 = New VIBlend.WinForms.Controls.vTabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dgvVendaNotas = New System.Windows.Forms.DataGridView()
        Me.ShapeContainer3 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape5 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.lblCli = New System.Windows.Forms.Label()
        Me.lblPessoaDestino = New System.Windows.Forms.Label()
        Me.btnData = New VIBlend.WinForms.Controls.vArrowButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_IdTexto = New System.Windows.Forms.Label()
        Me.lblTransacaoData = New System.Windows.Forms.Label()
        Me.lblIDTransacao = New System.Windows.Forms.Label()
        Me.btnClose = New VIBlend.WinForms.Controls.vFormButton()
        Me.lblTotalGeral = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblSituacao = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.mnuItens = New VIBlend.WinForms.Controls.vContextMenu()
        Me.MenuItemEditar = New System.Windows.Forms.MenuItem()
        Me.MenuItemInserir = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItemExcluir = New System.Windows.Forms.MenuItem()
        Me.VApplicationMenuItem2 = New VIBlend.WinForms.Controls.vApplicationMenuItem()
        Me.VApplicationMenuItem3 = New VIBlend.WinForms.Controls.vApplicationMenuItem()
        Me.VApplicationMenuItem4 = New VIBlend.WinForms.Controls.vApplicationMenuItem()
        Me.btnFinalizar = New VIBlend.WinForms.Controls.vButton()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblFilial = New System.Windows.Forms.Label()
        Me.VPanel1 = New VIBlend.WinForms.Controls.vPanel()
        Me.VPanel2 = New VIBlend.WinForms.Controls.vPanel()
        Me.Panel1.SuspendLayout()
        Me.tabPrincipal.SuspendLayout()
        Me.vtab1.SuspendLayout()
        CType(Me.dgvItens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vtab2.SuspendLayout()
        CType(Me.dgvAReceber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vtab3.SuspendLayout()
        CType(Me.dgvVendaNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.VPanel1.Content.SuspendLayout()
        Me.VPanel1.SuspendLayout()
        Me.VPanel2.Content.SuspendLayout()
        Me.VPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.lblFilial)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.lbl_IdTexto)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnData)
        Me.Panel1.Controls.Add(Me.lblSituacao)
        Me.Panel1.Controls.Add(Me.lblIDTransacao)
        Me.Panel1.Controls.Add(Me.lblTransacaoData)
        Me.Panel1.Size = New System.Drawing.Size(1049, 50)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Controls.SetChildIndex(Me.lblTransacaoData, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblIDTransacao, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblSituacao, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnData, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnClose, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lbl_IdTexto, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label15, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label3, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblFilial, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label18, 0)
        '
        'lblTitulo
        '
        Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.None
        Me.lblTitulo.Font = New System.Drawing.Font("Calibri", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Location = New System.Drawing.Point(807, 4)
        Me.lblTitulo.Size = New System.Drawing.Size(201, 42)
        Me.lblTitulo.Text = "Simples Saida"
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabPrincipal
        '
        Me.tabPrincipal.AllowAnimations = True
        Me.tabPrincipal.AllPagesHeight = 28
        Me.tabPrincipal.Controls.Add(Me.vtab1)
        Me.tabPrincipal.Controls.Add(Me.vtab2)
        Me.tabPrincipal.Controls.Add(Me.vtab3)
        Me.tabPrincipal.ForeColor = System.Drawing.Color.Black
        Me.tabPrincipal.Location = New System.Drawing.Point(9, 106)
        Me.tabPrincipal.Name = "tabPrincipal"
        Me.tabPrincipal.Padding = New System.Windows.Forms.Padding(0, 38, 0, 0)
        Me.tabPrincipal.Size = New System.Drawing.Size(1030, 500)
        Me.tabPrincipal.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top
        Me.tabPrincipal.TabIndex = 6
        Me.tabPrincipal.TabPages.Add(Me.vtab1)
        Me.tabPrincipal.TabPages.Add(Me.vtab2)
        Me.tabPrincipal.TabPages.Add(Me.vtab3)
        Me.tabPrincipal.TabsAreaBackColor = System.Drawing.Color.OldLace
        Me.tabPrincipal.TabsInitialOffset = 40
        Me.tabPrincipal.TabsShape = VIBlend.WinForms.Controls.TabsShape.Chrome
        Me.tabPrincipal.TabsSpacing = 10
        Me.tabPrincipal.TitleHeight = 38
        Me.tabPrincipal.UseTabsAreaBackColor = True
        Me.tabPrincipal.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.RETROBLUE
        '
        'vtab1
        '
        Me.vtab1.Controls.Add(Me.dgvItens)
        Me.vtab1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vtab1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab1.ForeColor = System.Drawing.Color.Black
        Me.vtab1.Location = New System.Drawing.Point(0, 38)
        Me.vtab1.Name = "vtab1"
        Me.vtab1.Padding = New System.Windows.Forms.Padding(0)
        Me.vtab1.SelectedTextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab1.Size = New System.Drawing.Size(1030, 462)
        Me.vtab1.TabIndex = 3
        Me.vtab1.Text = "Produtos"
        Me.vtab1.TextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab1.TooltipText = "TabPage"
        Me.vtab1.UseDefaultTextFont = False
        Me.vtab1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.RETROBLUE
        Me.vtab1.Visible = False
        '
        'dgvItens
        '
        Me.dgvItens.AllowUserToAddRows = False
        Me.dgvItens.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Azure
        Me.dgvItens.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvItens.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.dgvItens.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvItens.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.dgvItens.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvItens.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvItens.ColumnHeadersHeight = 30
        Me.dgvItens.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clnIDTransacaoItem, Me.clnRGProduto, Me.clnProduto, Me.clnQuantidade, Me.clnPreco, Me.clnSubTotal, Me.clnDesconto, Me.clnTotal})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvItens.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvItens.EnableHeadersVisualStyles = False
        Me.dgvItens.GridColor = System.Drawing.SystemColors.ActiveCaption
        Me.dgvItens.Location = New System.Drawing.Point(9, 9)
        Me.dgvItens.Name = "dgvItens"
        Me.dgvItens.ReadOnly = True
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvItens.RowHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvItens.RowHeadersWidth = 35
        Me.dgvItens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvItens.Size = New System.Drawing.Size(1012, 444)
        Me.dgvItens.TabIndex = 0
        '
        'clnIDTransacaoItem
        '
        Me.clnIDTransacaoItem.Frozen = True
        Me.clnIDTransacaoItem.HeaderText = "IDItem"
        Me.clnIDTransacaoItem.Name = "clnIDTransacaoItem"
        Me.clnIDTransacaoItem.ReadOnly = True
        Me.clnIDTransacaoItem.Visible = False
        '
        'clnRGProduto
        '
        Me.clnRGProduto.Frozen = True
        Me.clnRGProduto.HeaderText = "Reg."
        Me.clnRGProduto.Name = "clnRGProduto"
        Me.clnRGProduto.ReadOnly = True
        Me.clnRGProduto.Width = 70
        '
        'clnProduto
        '
        Me.clnProduto.Frozen = True
        Me.clnProduto.HeaderText = "Produto"
        Me.clnProduto.Name = "clnProduto"
        Me.clnProduto.ReadOnly = True
        Me.clnProduto.Width = 430
        '
        'clnQuantidade
        '
        Me.clnQuantidade.Frozen = True
        Me.clnQuantidade.HeaderText = "Qtde"
        Me.clnQuantidade.Name = "clnQuantidade"
        Me.clnQuantidade.ReadOnly = True
        Me.clnQuantidade.Width = 70
        '
        'clnPreco
        '
        Me.clnPreco.Frozen = True
        Me.clnPreco.HeaderText = "Preço"
        Me.clnPreco.Name = "clnPreco"
        Me.clnPreco.ReadOnly = True
        '
        'clnSubTotal
        '
        Me.clnSubTotal.Frozen = True
        Me.clnSubTotal.HeaderText = "SubTotal"
        Me.clnSubTotal.Name = "clnSubTotal"
        Me.clnSubTotal.ReadOnly = True
        '
        'clnDesconto
        '
        Me.clnDesconto.HeaderText = "Desc."
        Me.clnDesconto.Name = "clnDesconto"
        Me.clnDesconto.ReadOnly = True
        Me.clnDesconto.Width = 80
        '
        'clnTotal
        '
        Me.clnTotal.HeaderText = "Total"
        Me.clnTotal.Name = "clnTotal"
        Me.clnTotal.ReadOnly = True
        '
        'vtab2
        '
        Me.vtab2.Controls.Add(Me.dgvAReceber)
        Me.vtab2.Controls.Add(Me.txtObservacao)
        Me.vtab2.Controls.Add(Me.Label12)
        Me.vtab2.Controls.Add(Me.lblTotalCobrado)
        Me.vtab2.Controls.Add(Me.Label17)
        Me.vtab2.Controls.Add(Me.Label14)
        Me.vtab2.Controls.Add(Me.Label5)
        Me.vtab2.Controls.Add(Me.Label6)
        Me.vtab2.Controls.Add(Me.cmbIDPlano)
        Me.vtab2.Controls.Add(Me.ShapeContainer2)
        Me.vtab2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vtab2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab2.Location = New System.Drawing.Point(0, 38)
        Me.vtab2.Name = "vtab2"
        Me.vtab2.Padding = New System.Windows.Forms.Padding(0)
        Me.vtab2.SelectedTextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab2.Size = New System.Drawing.Size(1030, 462)
        Me.vtab2.TabIndex = 0
        Me.vtab2.Text = "Forma de Cobrança"
        Me.vtab2.TextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab2.TooltipText = "TabPage"
        Me.vtab2.UseDefaultTextFont = False
        Me.vtab2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.RETROBLUE
        Me.vtab2.Visible = False
        '
        'dgvAReceber
        '
        Me.dgvAReceber.AllowUserToAddRows = False
        Me.dgvAReceber.AllowUserToDeleteRows = False
        Me.dgvAReceber.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.dgvAReceber.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvAReceber.ColumnHeadersHeight = 30
        Me.dgvAReceber.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clnID, Me.clnReg, Me.clnVencimento, Me.clnPermanencia, Me.clnValor})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvAReceber.DefaultCellStyle = DataGridViewCellStyle6
        Me.dgvAReceber.EnableHeadersVisualStyles = False
        Me.dgvAReceber.Location = New System.Drawing.Point(30, 141)
        Me.dgvAReceber.MultiSelect = False
        Me.dgvAReceber.Name = "dgvAReceber"
        Me.dgvAReceber.ReadOnly = True
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvAReceber.RowHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.dgvAReceber.RowHeadersWidth = 30
        Me.dgvAReceber.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvAReceber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvAReceber.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvAReceber.Size = New System.Drawing.Size(447, 210)
        Me.dgvAReceber.TabIndex = 9
        '
        'clnID
        '
        Me.clnID.HeaderText = "IDAReceber"
        Me.clnID.Name = "clnID"
        Me.clnID.ReadOnly = True
        Me.clnID.Visible = False
        '
        'clnReg
        '
        Me.clnReg.HeaderText = "Reg."
        Me.clnReg.Name = "clnReg"
        Me.clnReg.ReadOnly = True
        Me.clnReg.Width = 70
        '
        'clnVencimento
        '
        Me.clnVencimento.HeaderText = "Vencimento"
        Me.clnVencimento.Name = "clnVencimento"
        Me.clnVencimento.ReadOnly = True
        '
        'clnPermanencia
        '
        Me.clnPermanencia.HeaderText = "Perm."
        Me.clnPermanencia.Name = "clnPermanencia"
        Me.clnPermanencia.ReadOnly = True
        Me.clnPermanencia.Width = 90
        '
        'clnValor
        '
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.clnValor.DefaultCellStyle = DataGridViewCellStyle5
        Me.clnValor.HeaderText = "Valor"
        Me.clnValor.Name = "clnValor"
        Me.clnValor.ReadOnly = True
        Me.clnValor.Width = 120
        '
        'txtObservacao
        '
        Me.txtObservacao.Location = New System.Drawing.Point(558, 63)
        Me.txtObservacao.Multiline = True
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(380, 64)
        Me.txtObservacao.TabIndex = 13
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(528, 27)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(234, 26)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Observações Importantes:"
        '
        'lblTotalCobrado
        '
        Me.lblTotalCobrado.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalCobrado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalCobrado.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCobrado.Location = New System.Drawing.Point(292, 363)
        Me.lblTotalCobrado.Name = "lblTotalCobrado"
        Me.lblTotalCobrado.Size = New System.Drawing.Size(151, 32)
        Me.lblTotalCobrado.TabIndex = 11
        Me.lblTotalCobrado.Text = "R$ 0,00"
        Me.lblTotalCobrado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(185, 368)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(103, 19)
        Me.Label17.TabIndex = 10
        Me.Label17.Text = "Total Cobrado:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(25, 27)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 26)
        Me.Label14.TabIndex = 1
        Me.Label14.Text = "Cobrança:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Location = New System.Drawing.Point(49, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(102, 19)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Plano de Pgto."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(29, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(202, 19)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Desbobramento das Parcelas:"
        '
        'cmbIDPlano
        '
        Me.cmbIDPlano.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbIDPlano.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbIDPlano.FormattingEnabled = True
        Me.cmbIDPlano.Location = New System.Drawing.Point(160, 63)
        Me.cmbIDPlano.Name = "cmbIDPlano"
        Me.cmbIDPlano.RestrictContentToListItems = True
        Me.cmbIDPlano.Size = New System.Drawing.Size(234, 27)
        Me.cmbIDPlano.TabIndex = 7
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(4, 4)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape3, Me.LineShape1})
        Me.ShapeContainer2.Size = New System.Drawing.Size(1022, 454)
        Me.ShapeContainer2.TabIndex = 0
        Me.ShapeContainer2.TabStop = False
        '
        'LineShape3
        '
        Me.LineShape3.BorderColor = System.Drawing.Color.SlateGray
        Me.LineShape3.BorderWidth = 3
        Me.LineShape3.Name = "LineShape3"
        Me.LineShape3.X1 = 757
        Me.LineShape3.X2 = 947
        Me.LineShape3.Y1 = 38
        Me.LineShape3.Y2 = 38
        '
        'LineShape1
        '
        Me.LineShape1.BorderColor = System.Drawing.Color.SlateGray
        Me.LineShape1.BorderWidth = 3
        Me.LineShape1.Name = "LineShape1"
        Me.LineShape1.X1 = 121
        Me.LineShape1.X2 = 446
        Me.LineShape1.Y1 = 39
        Me.LineShape1.Y2 = 39
        '
        'vtab3
        '
        Me.vtab3.Controls.Add(Me.Label11)
        Me.vtab3.Controls.Add(Me.dgvVendaNotas)
        Me.vtab3.Controls.Add(Me.ShapeContainer3)
        Me.vtab3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vtab3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab3.Location = New System.Drawing.Point(0, 38)
        Me.vtab3.Name = "vtab3"
        Me.vtab3.Padding = New System.Windows.Forms.Padding(0)
        Me.vtab3.SelectedTextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab3.Size = New System.Drawing.Size(1030, 462)
        Me.vtab3.TabIndex = 5
        Me.vtab3.Text = "Notas Fiscais"
        Me.vtab3.TextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab3.TooltipText = "TabPage"
        Me.vtab3.UseDefaultTextFont = False
        Me.vtab3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.RETROBLUE
        Me.vtab3.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Calibri", 15.75!)
        Me.Label11.Location = New System.Drawing.Point(20, 15)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(241, 26)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Notas e/ou Cupons Fiscais:"
        '
        'dgvVendaNotas
        '
        Me.dgvVendaNotas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvVendaNotas.Location = New System.Drawing.Point(37, 62)
        Me.dgvVendaNotas.Name = "dgvVendaNotas"
        Me.dgvVendaNotas.Size = New System.Drawing.Size(484, 277)
        Me.dgvVendaNotas.TabIndex = 4
        '
        'ShapeContainer3
        '
        Me.ShapeContainer3.Location = New System.Drawing.Point(4, 4)
        Me.ShapeContainer3.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer3.Name = "ShapeContainer3"
        Me.ShapeContainer3.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape5})
        Me.ShapeContainer3.Size = New System.Drawing.Size(1022, 454)
        Me.ShapeContainer3.TabIndex = 5
        Me.ShapeContainer3.TabStop = False
        '
        'LineShape5
        '
        Me.LineShape5.BorderColor = System.Drawing.Color.SlateGray
        Me.LineShape5.BorderWidth = 3
        Me.LineShape5.Name = "LineShape5"
        Me.LineShape5.X1 = 258
        Me.LineShape5.X2 = 538
        Me.LineShape5.Y1 = 26
        Me.LineShape5.Y2 = 26
        '
        'lblCli
        '
        Me.lblCli.AutoSize = True
        Me.lblCli.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCli.Location = New System.Drawing.Point(12, 65)
        Me.lblCli.Name = "lblCli"
        Me.lblCli.Size = New System.Drawing.Size(99, 19)
        Me.lblCli.TabIndex = 4
        Me.lblCli.Text = "Filial Destino:"
        '
        'lblPessoaDestino
        '
        Me.lblPessoaDestino.BackColor = System.Drawing.Color.White
        Me.lblPessoaDestino.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPessoaDestino.Location = New System.Drawing.Point(0, 0)
        Me.lblPessoaDestino.Name = "lblPessoaDestino"
        Me.lblPessoaDestino.Size = New System.Drawing.Size(346, 29)
        Me.lblPessoaDestino.TabIndex = 5
        Me.lblPessoaDestino.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnData
        '
        Me.btnData.AllowAnimations = True
        Me.btnData.ArrowDirection = System.Windows.Forms.ArrowDirection.Left
        Me.btnData.Location = New System.Drawing.Point(269, 24)
        Me.btnData.Maximum = 100
        Me.btnData.Minimum = 0
        Me.btnData.Name = "btnData"
        Me.btnData.Size = New System.Drawing.Size(16, 16)
        Me.btnData.TabIndex = 53
        Me.btnData.TabStop = False
        Me.btnData.Value = 0
        Me.btnData.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(166, 4)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 50
        Me.Label3.Text = "Data:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbl_IdTexto
        '
        Me.lbl_IdTexto.AutoSize = True
        Me.lbl_IdTexto.BackColor = System.Drawing.Color.Transparent
        Me.lbl_IdTexto.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_IdTexto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl_IdTexto.Location = New System.Drawing.Point(29, 4)
        Me.lbl_IdTexto.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lbl_IdTexto.Name = "lbl_IdTexto"
        Me.lbl_IdTexto.Size = New System.Drawing.Size(35, 13)
        Me.lbl_IdTexto.TabIndex = 51
        Me.lbl_IdTexto.Text = "Reg."
        Me.lbl_IdTexto.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblTransacaoData
        '
        Me.lblTransacaoData.BackColor = System.Drawing.Color.Transparent
        Me.lblTransacaoData.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTransacaoData.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblTransacaoData.Location = New System.Drawing.Point(112, 16)
        Me.lblTransacaoData.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTransacaoData.Name = "lblTransacaoData"
        Me.lblTransacaoData.Size = New System.Drawing.Size(155, 30)
        Me.lblTransacaoData.TabIndex = 48
        Me.lblTransacaoData.Text = "01/01/2017"
        Me.lblTransacaoData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIDTransacao
        '
        Me.lblIDTransacao.BackColor = System.Drawing.Color.Transparent
        Me.lblIDTransacao.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDTransacao.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblIDTransacao.Location = New System.Drawing.Point(5, 16)
        Me.lblIDTransacao.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIDTransacao.Name = "lblIDTransacao"
        Me.lblIDTransacao.Size = New System.Drawing.Size(85, 30)
        Me.lblIDTransacao.TabIndex = 49
        Me.lblIDTransacao.Text = "0001"
        Me.lblIDTransacao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClose
        '
        Me.btnClose.AllowAnimations = True
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.ButtonType = VIBlend.WinForms.Controls.vFormButtonType.CloseButton
        Me.btnClose.ForeColor = System.Drawing.Color.Firebrick
        Me.btnClose.Location = New System.Drawing.Point(1018, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RibbonStyle = False
        Me.btnClose.RoundedCornersMask = CType(15, Byte)
        Me.btnClose.ShowFocusRectangle = False
        Me.btnClose.Size = New System.Drawing.Size(20, 20)
        Me.btnClose.TabIndex = 54
        Me.btnClose.TabStop = False
        Me.btnClose.UseVisualStyleBackColor = False
        Me.btnClose.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2003SILVER
        '
        'lblTotalGeral
        '
        Me.lblTotalGeral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTotalGeral.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lblTotalGeral.Location = New System.Drawing.Point(0, 0)
        Me.lblTotalGeral.Name = "lblTotalGeral"
        Me.lblTotalGeral.Size = New System.Drawing.Size(200, 37)
        Me.lblTotalGeral.TabIndex = 9
        Me.lblTotalGeral.Text = "R$ 0,00"
        Me.lblTotalGeral.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(786, 627)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 19)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "Total:"
        '
        'lblSituacao
        '
        Me.lblSituacao.BackColor = System.Drawing.Color.Transparent
        Me.lblSituacao.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSituacao.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblSituacao.Location = New System.Drawing.Point(320, 16)
        Me.lblSituacao.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSituacao.Name = "lblSituacao"
        Me.lblSituacao.Size = New System.Drawing.Size(145, 30)
        Me.lblSituacao.TabIndex = 49
        Me.lblSituacao.Text = "Em Aberto"
        Me.lblSituacao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(357, 4)
        Me.Label15.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(67, 13)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "Situação:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'mnuItens
        '
        Me.mnuItens.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemEditar, Me.MenuItemInserir, Me.MenuItem3, Me.MenuItemExcluir})
        '
        'MenuItemEditar
        '
        Me.MenuItemEditar.Index = 0
        Me.MenuItemEditar.Text = "Editar Item"
        '
        'MenuItemInserir
        '
        Me.MenuItemInserir.Index = 1
        Me.MenuItemInserir.Text = "Inserir Novo Item"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'MenuItemExcluir
        '
        Me.MenuItemExcluir.Index = 3
        Me.MenuItemExcluir.Text = "Excluir Item"
        '
        'VApplicationMenuItem2
        '
        Me.VApplicationMenuItem2.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VApplicationMenuItem2.ItemType = VIBlend.WinForms.Controls.vApplicationMenuItemType.MenuItem
        Me.VApplicationMenuItem2.Location = New System.Drawing.Point(0, 0)
        Me.VApplicationMenuItem2.Name = "VApplicationMenuItem2"
        Me.VApplicationMenuItem2.SelectedChildMenuItem = Nothing
        Me.VApplicationMenuItem2.Size = New System.Drawing.Size(200, 20)
        Me.VApplicationMenuItem2.TabIndex = 0
        Me.VApplicationMenuItem2.Text = "Salvar"
        Me.VApplicationMenuItem2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VApplicationMenuItem2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VApplicationMenuItem3
        '
        Me.VApplicationMenuItem3.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VApplicationMenuItem3.ItemType = VIBlend.WinForms.Controls.vApplicationMenuItemType.Separator
        Me.VApplicationMenuItem3.Location = New System.Drawing.Point(0, 20)
        Me.VApplicationMenuItem3.Name = "VApplicationMenuItem3"
        Me.VApplicationMenuItem3.SelectedChildMenuItem = Nothing
        Me.VApplicationMenuItem3.Size = New System.Drawing.Size(200, 20)
        Me.VApplicationMenuItem3.TabIndex = 1
        Me.VApplicationMenuItem3.Text = "Imprimir"
        Me.VApplicationMenuItem3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VApplicationMenuItem3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VApplicationMenuItem4
        '
        Me.VApplicationMenuItem4.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VApplicationMenuItem4.ItemType = VIBlend.WinForms.Controls.vApplicationMenuItemType.MenuItem
        Me.VApplicationMenuItem4.Location = New System.Drawing.Point(0, 40)
        Me.VApplicationMenuItem4.Name = "VApplicationMenuItem4"
        Me.VApplicationMenuItem4.SelectedChildMenuItem = Nothing
        Me.VApplicationMenuItem4.Size = New System.Drawing.Size(200, 20)
        Me.VApplicationMenuItem4.TabIndex = 2
        Me.VApplicationMenuItem4.Text = "vApplicationMenuItem"
        Me.VApplicationMenuItem4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VApplicationMenuItem4.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'btnFinalizar
        '
        Me.btnFinalizar.AllowAnimations = True
        Me.btnFinalizar.BackColor = System.Drawing.Color.Transparent
        Me.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinalizar.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinalizar.HoverEffectsEnabled = True
        Me.btnFinalizar.Image = Global.NovaSiao.My.Resources.Resources.accept
        Me.btnFinalizar.ImageAbsolutePosition = New System.Drawing.Point(15, 5)
        Me.btnFinalizar.Location = New System.Drawing.Point(455, 615)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Opacity = 0!
        Me.btnFinalizar.RoundedCornersMask = CType(15, Byte)
        Me.btnFinalizar.RoundedCornersRadius = 0
        Me.btnFinalizar.Size = New System.Drawing.Size(140, 42)
        Me.btnFinalizar.TabIndex = 10
        Me.btnFinalizar.Text = "&Finalizar"
        Me.btnFinalizar.TextAbsolutePosition = New System.Drawing.Point(20, 1)
        Me.btnFinalizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFinalizar.UseAbsoluteImagePositioning = True
        Me.btnFinalizar.UseAbsoluteTextPositioning = True
        Me.btnFinalizar.UseVisualStyleBackColor = False
        Me.btnFinalizar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(522, 4)
        Me.Label18.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 13)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "Filial:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblFilial
        '
        Me.lblFilial.BackColor = System.Drawing.Color.Transparent
        Me.lblFilial.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilial.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblFilial.Location = New System.Drawing.Point(473, 16)
        Me.lblFilial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilial.Name = "lblFilial"
        Me.lblFilial.Size = New System.Drawing.Size(145, 30)
        Me.lblFilial.TabIndex = 55
        Me.lblFilial.Text = "Filial"
        Me.lblFilial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'VPanel1
        '
        Me.VPanel1.AllowAnimations = True
        Me.VPanel1.BorderRadius = 0
        '
        'VPanel1.Content
        '
        Me.VPanel1.Content.AutoScroll = True
        Me.VPanel1.Content.BackColor = System.Drawing.SystemColors.Control
        Me.VPanel1.Content.Controls.Add(Me.lblTotalGeral)
        Me.VPanel1.Content.Location = New System.Drawing.Point(1, 1)
        Me.VPanel1.Content.Name = "Content"
        Me.VPanel1.Content.Size = New System.Drawing.Size(200, 37)
        Me.VPanel1.Content.TabIndex = 3
        Me.VPanel1.CustomScrollersIntersectionColor = System.Drawing.Color.Empty
        Me.VPanel1.Location = New System.Drawing.Point(837, 616)
        Me.VPanel1.Name = "VPanel1"
        Me.VPanel1.Opacity = 1.0!
        Me.VPanel1.PanelBorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.VPanel1.Size = New System.Drawing.Size(202, 39)
        Me.VPanel1.TabIndex = 12
        Me.VPanel1.Text = "VPanel1"
        Me.VPanel1.UsePanelBorderColor = True
        Me.VPanel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VPanel2
        '
        Me.VPanel2.AllowAnimations = True
        Me.VPanel2.BorderRadius = 0
        '
        'VPanel2.Content
        '
        Me.VPanel2.Content.AutoScroll = True
        Me.VPanel2.Content.BackColor = System.Drawing.SystemColors.Control
        Me.VPanel2.Content.Controls.Add(Me.lblPessoaDestino)
        Me.VPanel2.Content.Location = New System.Drawing.Point(1, 1)
        Me.VPanel2.Content.Name = "Content"
        Me.VPanel2.Content.Size = New System.Drawing.Size(346, 29)
        Me.VPanel2.Content.TabIndex = 3
        Me.VPanel2.CustomScrollersIntersectionColor = System.Drawing.Color.Empty
        Me.VPanel2.Location = New System.Drawing.Point(117, 61)
        Me.VPanel2.Name = "VPanel2"
        Me.VPanel2.Opacity = 1.0!
        Me.VPanel2.PanelBorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.VPanel2.Size = New System.Drawing.Size(348, 31)
        Me.VPanel2.TabIndex = 13
        Me.VPanel2.Text = "VPanel2"
        Me.VPanel2.UsePanelBorderColor = True
        Me.VPanel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'frmSimplesSaida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(1049, 667)
        Me.Controls.Add(Me.VPanel2)
        Me.Controls.Add(Me.VPanel1)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblCli)
        Me.Controls.Add(Me.tabPrincipal)
        Me.KeyPreview = True
        Me.Name = "frmSimplesSaida"
        Me.Controls.SetChildIndex(Me.tabPrincipal, 0)
        Me.Controls.SetChildIndex(Me.lblCli, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.btnFinalizar, 0)
        Me.Controls.SetChildIndex(Me.VPanel1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.VPanel2, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tabPrincipal.ResumeLayout(False)
        Me.vtab1.ResumeLayout(False)
        CType(Me.dgvItens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vtab2.ResumeLayout(False)
        Me.vtab2.PerformLayout()
        CType(Me.dgvAReceber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vtab3.ResumeLayout(False)
        Me.vtab3.PerformLayout()
        CType(Me.dgvVendaNotas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.VPanel1.Content.ResumeLayout(False)
        Me.VPanel1.ResumeLayout(False)
        Me.VPanel2.Content.ResumeLayout(False)
        Me.VPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tabPrincipal As VIBlend.WinForms.Controls.vTabControl
    Friend WithEvents vtab1 As VIBlend.WinForms.Controls.vTabPage
    Friend WithEvents dgvItens As DataGridView
    Friend WithEvents vtab2 As VIBlend.WinForms.Controls.vTabPage
    Friend WithEvents dgvAReceber As DataGridView
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents cmbIDPlano As Controles.ComboBox_OnlyValues
    Friend WithEvents vtab3 As VIBlend.WinForms.Controls.vTabPage
    Friend WithEvents Label11 As Label
    Friend WithEvents dgvVendaNotas As DataGridView
    Friend WithEvents lblCli As Label
    Friend WithEvents lblPessoaDestino As Label
    Friend WithEvents btnData As VIBlend.WinForms.Controls.vArrowButton
    Friend WithEvents Label3 As Label
    Friend WithEvents lblIDTransacao As Label
    Friend WithEvents lbl_IdTexto As Label
    Friend WithEvents lblTransacaoData As Label
    Friend WithEvents btnClose As VIBlend.WinForms.Controls.vFormButton
    Friend WithEvents lblTotalGeral As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents clnIDTransacaoItem As DataGridViewTextBoxColumn
    Friend WithEvents clnRGProduto As DataGridViewTextBoxColumn
    Friend WithEvents clnProduto As DataGridViewTextBoxColumn
    Friend WithEvents clnQuantidade As DataGridViewTextBoxColumn
    Friend WithEvents clnPreco As DataGridViewTextBoxColumn
    Friend WithEvents clnSubTotal As DataGridViewTextBoxColumn
    Friend WithEvents clnDesconto As DataGridViewTextBoxColumn
    Friend WithEvents clnTotal As DataGridViewTextBoxColumn
    Friend WithEvents Label15 As Label
    Friend WithEvents lblSituacao As Label
    Friend WithEvents mnuItens As VIBlend.WinForms.Controls.vContextMenu
    Friend WithEvents MenuItemEditar As MenuItem
    Friend WithEvents MenuItemInserir As MenuItem
    Friend WithEvents MenuItem3 As MenuItem
    Friend WithEvents MenuItemExcluir As MenuItem
    Friend WithEvents VApplicationMenuItem2 As VIBlend.WinForms.Controls.vApplicationMenuItem
    Friend WithEvents VApplicationMenuItem3 As VIBlend.WinForms.Controls.vApplicationMenuItem
    Friend WithEvents VApplicationMenuItem4 As VIBlend.WinForms.Controls.vApplicationMenuItem
    Friend WithEvents Label14 As Label
    Friend WithEvents ShapeContainer2 As PowerPacks.ShapeContainer
    Friend WithEvents LineShape1 As PowerPacks.LineShape
    Friend WithEvents lblTotalCobrado As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents txtObservacao As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents LineShape3 As PowerPacks.LineShape
    Friend WithEvents btnFinalizar As VIBlend.WinForms.Controls.vButton
    Friend WithEvents Label18 As Label
    Friend WithEvents lblFilial As Label
    Friend WithEvents ShapeContainer3 As PowerPacks.ShapeContainer
    Friend WithEvents LineShape5 As PowerPacks.LineShape
    Friend WithEvents clnID As DataGridViewTextBoxColumn
    Friend WithEvents clnReg As DataGridViewTextBoxColumn
    Friend WithEvents clnVencimento As DataGridViewTextBoxColumn
    Friend WithEvents clnPermanencia As DataGridViewTextBoxColumn
    Friend WithEvents clnValor As DataGridViewTextBoxColumn
    Friend WithEvents VPanel1 As VIBlend.WinForms.Controls.vPanel
    Friend WithEvents VPanel2 As VIBlend.WinForms.Controls.vPanel
End Class
