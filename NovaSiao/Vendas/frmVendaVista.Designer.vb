<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmVendaVista
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
        Me.txtObservacao = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotalPago = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dgvPagamentos = New System.Windows.Forms.DataGridView()
        Me.clnID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnMovForma = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnValor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ShapeContainer2 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.LineShape3 = New Microsoft.VisualBasic.PowerPacks.LineShape()
        Me.btnVendedorAlterar = New System.Windows.Forms.Button()
        Me.lblVendedor = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RectangleShape1 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.btnDataSuperior = New VIBlend.WinForms.Controls.vArrowButton()
        Me.btnDataAnterior = New VIBlend.WinForms.Controls.vArrowButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbl_IdTexto = New System.Windows.Forms.Label()
        Me.lblVendaData = New System.Windows.Forms.Label()
        Me.lblIDVenda = New System.Windows.Forms.Label()
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
        Me.lblFilial = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.tabPrincipal.SuspendLayout()
        Me.vtab1.SuspendLayout()
        CType(Me.dgvItens, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.vtab2.SuspendLayout()
        CType(Me.dgvPagamentos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.lbl_IdTexto)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnDataSuperior)
        Me.Panel1.Controls.Add(Me.btnDataAnterior)
        Me.Panel1.Controls.Add(Me.lblFilial)
        Me.Panel1.Controls.Add(Me.lblSituacao)
        Me.Panel1.Controls.Add(Me.lblIDVenda)
        Me.Panel1.Controls.Add(Me.lblVendaData)
        Me.Panel1.Size = New System.Drawing.Size(1049, 50)
        Me.Panel1.Controls.SetChildIndex(Me.lblVendaData, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblIDVenda, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblSituacao, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblFilial, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnDataAnterior, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblTitulo, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnDataSuperior, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnClose, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lbl_IdTexto, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label15, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label4, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label3, 0)
        '
        'lblTitulo
        '
        Me.lblTitulo.Font = New System.Drawing.Font("Calibri", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Location = New System.Drawing.Point(830, 0)
        Me.lblTitulo.Size = New System.Drawing.Size(219, 50)
        Me.lblTitulo.Text = "Venda A Vista"
        '
        'tabPrincipal
        '
        Me.tabPrincipal.AllowAnimations = True
        Me.tabPrincipal.AllPagesHeight = 28
        Me.tabPrincipal.Controls.Add(Me.vtab1)
        Me.tabPrincipal.Controls.Add(Me.vtab2)
        Me.tabPrincipal.ForeColor = System.Drawing.Color.Black
        Me.tabPrincipal.Location = New System.Drawing.Point(9, 106)
        Me.tabPrincipal.Name = "tabPrincipal"
        Me.tabPrincipal.Padding = New System.Windows.Forms.Padding(0, 38, 0, 0)
        Me.tabPrincipal.Size = New System.Drawing.Size(1030, 500)
        Me.tabPrincipal.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top
        Me.tabPrincipal.TabIndex = 3
        Me.tabPrincipal.TabPages.Add(Me.vtab1)
        Me.tabPrincipal.TabPages.Add(Me.vtab2)
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
        Me.vtab2.Controls.Add(Me.txtObservacao)
        Me.vtab2.Controls.Add(Me.Label12)
        Me.vtab2.Controls.Add(Me.lblTotalPago)
        Me.vtab2.Controls.Add(Me.Label17)
        Me.vtab2.Controls.Add(Me.dgvPagamentos)
        Me.vtab2.Controls.Add(Me.Label6)
        Me.vtab2.Controls.Add(Me.ShapeContainer2)
        Me.vtab2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.vtab2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab2.Location = New System.Drawing.Point(0, 38)
        Me.vtab2.Name = "vtab2"
        Me.vtab2.Padding = New System.Windows.Forms.Padding(0)
        Me.vtab2.SelectedTextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab2.Size = New System.Drawing.Size(1030, 462)
        Me.vtab2.TabIndex = 4
        Me.vtab2.Text = "Pagamentos"
        Me.vtab2.TextFont = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vtab2.TooltipText = "TabPage"
        Me.vtab2.UseDefaultTextFont = False
        Me.vtab2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.RETROBLUE
        Me.vtab2.Visible = False
        '
        'txtObservacao
        '
        Me.txtObservacao.Location = New System.Drawing.Point(529, 52)
        Me.txtObservacao.Multiline = True
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(476, 132)
        Me.txtObservacao.TabIndex = 16
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(529, 19)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(234, 26)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Observações Importantes:"
        '
        'lblTotalPago
        '
        Me.lblTotalPago.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalPago.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPago.Location = New System.Drawing.Point(310, 403)
        Me.lblTotalPago.Name = "lblTotalPago"
        Me.lblTotalPago.Size = New System.Drawing.Size(151, 32)
        Me.lblTotalPago.TabIndex = 12
        Me.lblTotalPago.Text = "R$ 0,00"
        Me.lblTotalPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Location = New System.Drawing.Point(195, 403)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(109, 19)
        Me.Label17.TabIndex = 13
        Me.Label17.Text = "Total Recebido:"
        '
        'dgvPagamentos
        '
        Me.dgvPagamentos.AllowUserToAddRows = False
        Me.dgvPagamentos.AllowUserToDeleteRows = False
        Me.dgvPagamentos.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.dgvPagamentos.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvPagamentos.ColumnHeadersHeight = 30
        Me.dgvPagamentos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clnID, Me.clnMovForma, Me.clnValor})
        Me.dgvPagamentos.EnableHeadersVisualStyles = False
        Me.dgvPagamentos.Location = New System.Drawing.Point(23, 52)
        Me.dgvPagamentos.MultiSelect = False
        Me.dgvPagamentos.Name = "dgvPagamentos"
        Me.dgvPagamentos.ReadOnly = True
        Me.dgvPagamentos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvPagamentos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvPagamentos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPagamentos.Size = New System.Drawing.Size(456, 347)
        Me.dgvPagamentos.TabIndex = 3
        '
        'clnID
        '
        Me.clnID.HeaderText = "Reg."
        Me.clnID.Name = "clnID"
        Me.clnID.ReadOnly = True
        Me.clnID.Width = 90
        '
        'clnMovForma
        '
        Me.clnMovForma.HeaderText = "Forma"
        Me.clnMovForma.Name = "clnMovForma"
        Me.clnMovForma.ReadOnly = True
        Me.clnMovForma.Width = 200
        '
        'clnValor
        '
        DataGridViewCellStyle5.Format = "C2"
        DataGridViewCellStyle5.NullValue = "0"
        Me.clnValor.DefaultCellStyle = DataGridViewCellStyle5
        Me.clnValor.HeaderText = "Valor"
        Me.clnValor.Name = "clnValor"
        Me.clnValor.ReadOnly = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(19, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(157, 19)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Pagamentos da Venda:"
        '
        'ShapeContainer2
        '
        Me.ShapeContainer2.Location = New System.Drawing.Point(4, 4)
        Me.ShapeContainer2.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer2.Name = "ShapeContainer2"
        Me.ShapeContainer2.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.LineShape3})
        Me.ShapeContainer2.Size = New System.Drawing.Size(1022, 454)
        Me.ShapeContainer2.TabIndex = 14
        Me.ShapeContainer2.TabStop = False
        '
        'LineShape3
        '
        Me.LineShape3.BorderColor = System.Drawing.Color.SlateGray
        Me.LineShape3.BorderWidth = 3
        Me.LineShape3.Name = "LineShape3"
        Me.LineShape3.X1 = 760
        Me.LineShape3.X2 = 1000
        Me.LineShape3.Y1 = 30
        Me.LineShape3.Y2 = 30
        '
        'btnVendedorAlterar
        '
        Me.btnVendedorAlterar.BackgroundImage = Global.NovaSiao.My.Resources.Resources.refresh
        Me.btnVendedorAlterar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVendedorAlterar.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.btnVendedorAlterar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVendedorAlterar.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnVendedorAlterar.Location = New System.Drawing.Point(301, 64)
        Me.btnVendedorAlterar.Name = "btnVendedorAlterar"
        Me.btnVendedorAlterar.Size = New System.Drawing.Size(23, 23)
        Me.btnVendedorAlterar.TabIndex = 10
        Me.btnVendedorAlterar.TabStop = False
        Me.btnVendedorAlterar.UseVisualStyleBackColor = True
        '
        'lblVendedor
        '
        Me.lblVendedor.BackColor = System.Drawing.Color.White
        Me.lblVendedor.Location = New System.Drawing.Point(97, 63)
        Me.lblVendedor.Name = "lblVendedor"
        Me.lblVendedor.Size = New System.Drawing.Size(192, 25)
        Me.lblVendedor.TabIndex = 8
        Me.lblVendedor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 19)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Vendedor:"
        '
        'RectangleShape1
        '
        Me.RectangleShape1.BackColor = System.Drawing.Color.White
        Me.RectangleShape1.BackStyle = Microsoft.VisualBasic.PowerPacks.BackStyle.Opaque
        Me.RectangleShape1.BorderColor = System.Drawing.SystemColors.ActiveCaption
        Me.RectangleShape1.FillGradientStyle = Microsoft.VisualBasic.PowerPacks.FillGradientStyle.Horizontal
        Me.RectangleShape1.Location = New System.Drawing.Point(93, 60)
        Me.RectangleShape1.Name = "RectangleShape1"
        Me.RectangleShape1.Size = New System.Drawing.Size(235, 30)
        '
        'ShapeContainer1
        '
        Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer1.Name = "ShapeContainer1"
        Me.ShapeContainer1.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape1})
        Me.ShapeContainer1.Size = New System.Drawing.Size(1049, 667)
        Me.ShapeContainer1.TabIndex = 11
        Me.ShapeContainer1.TabStop = False
        '
        'btnDataSuperior
        '
        Me.btnDataSuperior.AllowAnimations = True
        Me.btnDataSuperior.ArrowDirection = System.Windows.Forms.ArrowDirection.Up
        Me.btnDataSuperior.Location = New System.Drawing.Point(271, 10)
        Me.btnDataSuperior.Maximum = 100
        Me.btnDataSuperior.Minimum = 0
        Me.btnDataSuperior.Name = "btnDataSuperior"
        Me.btnDataSuperior.Size = New System.Drawing.Size(16, 16)
        Me.btnDataSuperior.TabIndex = 52
        Me.btnDataSuperior.TabStop = False
        Me.btnDataSuperior.Value = 0
        Me.btnDataSuperior.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'btnDataAnterior
        '
        Me.btnDataAnterior.AllowAnimations = True
        Me.btnDataAnterior.ArrowDirection = System.Windows.Forms.ArrowDirection.Down
        Me.btnDataAnterior.Location = New System.Drawing.Point(271, 26)
        Me.btnDataAnterior.Maximum = 100
        Me.btnDataAnterior.Minimum = 0
        Me.btnDataAnterior.Name = "btnDataAnterior"
        Me.btnDataAnterior.Size = New System.Drawing.Size(16, 16)
        Me.btnDataAnterior.TabIndex = 53
        Me.btnDataAnterior.TabStop = False
        Me.btnDataAnterior.Value = 0
        Me.btnDataAnterior.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        'lblVendaData
        '
        Me.lblVendaData.BackColor = System.Drawing.Color.Transparent
        Me.lblVendaData.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVendaData.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblVendaData.Location = New System.Drawing.Point(112, 16)
        Me.lblVendaData.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVendaData.Name = "lblVendaData"
        Me.lblVendaData.Size = New System.Drawing.Size(155, 30)
        Me.lblVendaData.TabIndex = 48
        Me.lblVendaData.Text = "01/01/2017"
        Me.lblVendaData.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIDVenda
        '
        Me.lblIDVenda.BackColor = System.Drawing.Color.Transparent
        Me.lblIDVenda.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIDVenda.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblIDVenda.Location = New System.Drawing.Point(5, 16)
        Me.lblIDVenda.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblIDVenda.Name = "lblIDVenda"
        Me.lblIDVenda.Size = New System.Drawing.Size(85, 30)
        Me.lblIDVenda.TabIndex = 49
        Me.lblIDVenda.Text = "0001"
        Me.lblIDVenda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.lblTotalGeral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalGeral.Font = New System.Drawing.Font("Calibri", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalGeral.Location = New System.Drawing.Point(888, 613)
        Me.lblTotalGeral.Name = "lblTotalGeral"
        Me.lblTotalGeral.Size = New System.Drawing.Size(151, 41)
        Me.lblTotalGeral.TabIndex = 12
        Me.lblTotalGeral.Text = "R$ 0,00"
        Me.lblTotalGeral.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(800, 615)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(84, 19)
        Me.Label13.TabIndex = 13
        Me.Label13.Text = "Total Geral:"
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
        Me.btnFinalizar.TabIndex = 15
        Me.btnFinalizar.Text = "&Finalizar"
        Me.btnFinalizar.TextAbsolutePosition = New System.Drawing.Point(20, 1)
        Me.btnFinalizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFinalizar.UseAbsoluteImagePositioning = True
        Me.btnFinalizar.UseAbsoluteTextPositioning = True
        Me.btnFinalizar.UseVisualStyleBackColor = False
        Me.btnFinalizar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        '
        'lblFilial
        '
        Me.lblFilial.BackColor = System.Drawing.Color.Transparent
        Me.lblFilial.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilial.ForeColor = System.Drawing.Color.AliceBlue
        Me.lblFilial.Location = New System.Drawing.Point(486, 16)
        Me.lblFilial.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilial.Name = "lblFilial"
        Me.lblFilial.Size = New System.Drawing.Size(145, 30)
        Me.lblFilial.TabIndex = 49
        Me.lblFilial.Text = "Filial"
        Me.lblFilial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(535, 4)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "Filial:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'frmVendaVista
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(1049, 667)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.lblTotalGeral)
        Me.Controls.Add(Me.btnVendedorAlterar)
        Me.Controls.Add(Me.lblVendedor)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tabPrincipal)
        Me.Controls.Add(Me.ShapeContainer1)
        Me.KeyPreview = True
        Me.Name = "frmVendaVista"
        Me.Controls.SetChildIndex(Me.ShapeContainer1, 0)
        Me.Controls.SetChildIndex(Me.tabPrincipal, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.lblVendedor, 0)
        Me.Controls.SetChildIndex(Me.btnVendedorAlterar, 0)
        Me.Controls.SetChildIndex(Me.lblTotalGeral, 0)
        Me.Controls.SetChildIndex(Me.Label13, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.btnFinalizar, 0)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tabPrincipal.ResumeLayout(False)
        Me.vtab1.ResumeLayout(False)
        CType(Me.dgvItens, System.ComponentModel.ISupportInitialize).EndInit()
        Me.vtab2.ResumeLayout(False)
        Me.vtab2.PerformLayout()
        CType(Me.dgvPagamentos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tabPrincipal As VIBlend.WinForms.Controls.vTabControl
    Friend WithEvents vtab1 As VIBlend.WinForms.Controls.vTabPage
    Friend WithEvents dgvItens As DataGridView
    Friend WithEvents vtab2 As VIBlend.WinForms.Controls.vTabPage
    Friend WithEvents dgvPagamentos As DataGridView
    Friend WithEvents Label6 As Label
    Friend WithEvents btnVendedorAlterar As Button
    Friend WithEvents lblVendedor As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents RectangleShape1 As PowerPacks.RectangleShape
    Friend WithEvents ShapeContainer1 As PowerPacks.ShapeContainer
    Friend WithEvents btnDataSuperior As VIBlend.WinForms.Controls.vArrowButton
    Friend WithEvents btnDataAnterior As VIBlend.WinForms.Controls.vArrowButton
    Friend WithEvents Label3 As Label
    Friend WithEvents lblIDVenda As Label
    Friend WithEvents lbl_IdTexto As Label
    Friend WithEvents lblVendaData As Label
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
    Friend WithEvents ShapeContainer2 As PowerPacks.ShapeContainer
    Friend WithEvents lblTotalPago As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents txtObservacao As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents LineShape3 As PowerPacks.LineShape
    Friend WithEvents btnFinalizar As VIBlend.WinForms.Controls.vButton
    Friend WithEvents Label4 As Label
    Friend WithEvents lblFilial As Label
    Friend WithEvents clnID As DataGridViewTextBoxColumn
    Friend WithEvents clnMovForma As DataGridViewTextBoxColumn
    Friend WithEvents clnValor As DataGridViewTextBoxColumn
End Class
