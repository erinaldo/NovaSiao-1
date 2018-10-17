<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmContas
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvContas = New System.Windows.Forms.DataGridView()
        Me.btnFechar = New System.Windows.Forms.Button()
        Me.btnNovo = New System.Windows.Forms.Button()
        Me.btnSalvar = New System.Windows.Forms.Button()
        Me.MenuListagem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AtivarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesativarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbFilial = New Controles.ComboBox_OnlyValues()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.IDFabricante = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnConta = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clnBancaria = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.clnAtivo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvContas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuListagem.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Size = New System.Drawing.Size(499, 50)
        Me.Panel1.TabIndex = 0
        '
        'lblTitulo
        '
        Me.lblTitulo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.None
        Me.lblTitulo.Location = New System.Drawing.Point(226, 0)
        Me.lblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTitulo.Size = New System.Drawing.Size(273, 50)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Contas de Movimentação"
        '
        'dgvContas
        '
        Me.dgvContas.AllowUserToAddRows = False
        Me.dgvContas.AllowUserToDeleteRows = False
        Me.dgvContas.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvContas.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvContas.ColumnHeadersHeight = 27
        Me.dgvContas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvContas.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDFabricante, Me.clnConta, Me.clnBancaria, Me.clnAtivo})
        Me.dgvContas.EnableHeadersVisualStyles = False
        Me.dgvContas.Location = New System.Drawing.Point(12, 106)
        Me.dgvContas.MultiSelect = False
        Me.dgvContas.Name = "dgvContas"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvContas.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvContas.RowHeadersWidth = 36
        Me.dgvContas.RowTemplate.Height = 30
        Me.dgvContas.Size = New System.Drawing.Size(478, 345)
        Me.dgvContas.TabIndex = 3
        '
        'btnFechar
        '
        Me.btnFechar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFechar.CausesValidation = False
        Me.btnFechar.Image = Global.NovaSiao.My.Resources.Resources.Fechar_24x24
        Me.btnFechar.Location = New System.Drawing.Point(363, 459)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(127, 43)
        Me.btnFechar.TabIndex = 6
        Me.btnFechar.Text = "&Fechar"
        Me.btnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'btnNovo
        '
        Me.btnNovo.Image = Global.NovaSiao.My.Resources.Resources.add_24x24
        Me.btnNovo.Location = New System.Drawing.Point(11, 460)
        Me.btnNovo.Name = "btnNovo"
        Me.btnNovo.Size = New System.Drawing.Size(127, 43)
        Me.btnNovo.TabIndex = 4
        Me.btnNovo.Text = "&Novo"
        Me.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNovo.UseVisualStyleBackColor = True
        '
        'btnSalvar
        '
        Me.btnSalvar.Enabled = False
        Me.btnSalvar.Image = Global.NovaSiao.My.Resources.Resources.save
        Me.btnSalvar.Location = New System.Drawing.Point(144, 459)
        Me.btnSalvar.Name = "btnSalvar"
        Me.btnSalvar.Size = New System.Drawing.Size(127, 43)
        Me.btnSalvar.TabIndex = 5
        Me.btnSalvar.Text = "&Salvar"
        Me.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalvar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSalvar.UseVisualStyleBackColor = True
        '
        'MenuListagem
        '
        Me.MenuListagem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AtivarToolStripMenuItem, Me.DesativarToolStripMenuItem})
        Me.MenuListagem.Name = "MenuFab"
        Me.MenuListagem.Size = New System.Drawing.Size(158, 48)
        '
        'AtivarToolStripMenuItem
        '
        Me.AtivarToolStripMenuItem.Image = Global.NovaSiao.My.Resources.Resources.accept
        Me.AtivarToolStripMenuItem.Name = "AtivarToolStripMenuItem"
        Me.AtivarToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.AtivarToolStripMenuItem.Text = "Ativar Conta"
        '
        'DesativarToolStripMenuItem
        '
        Me.DesativarToolStripMenuItem.Image = Global.NovaSiao.My.Resources.Resources.block
        Me.DesativarToolStripMenuItem.Name = "DesativarToolStripMenuItem"
        Me.DesativarToolStripMenuItem.Size = New System.Drawing.Size(157, 22)
        Me.DesativarToolStripMenuItem.Text = "Desativar Conta"
        '
        'cmbFilial
        '
        Me.cmbFilial.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbFilial.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFilial.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFilial.FormattingEnabled = True
        Me.cmbFilial.Location = New System.Drawing.Point(137, 63)
        Me.cmbFilial.Name = "cmbFilial"
        Me.cmbFilial.RestrictContentToListItems = True
        Me.cmbFilial.Size = New System.Drawing.Size(212, 26)
        Me.cmbFilial.TabIndex = 2
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(12, 66)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(119, 18)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Filial Origem:"
        '
        'IDFabricante
        '
        Me.IDFabricante.HeaderText = "Reg."
        Me.IDFabricante.Name = "IDFabricante"
        Me.IDFabricante.ReadOnly = True
        Me.IDFabricante.Width = 50
        '
        'clnConta
        '
        Me.clnConta.HeaderText = "Conta"
        Me.clnConta.Name = "clnConta"
        Me.clnConta.ReadOnly = True
        Me.clnConta.Width = 200
        '
        'clnBancaria
        '
        Me.clnBancaria.HeaderText = "Bancaria"
        Me.clnBancaria.Name = "clnBancaria"
        Me.clnBancaria.ReadOnly = True
        Me.clnBancaria.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.clnBancaria.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.clnBancaria.Width = 80
        '
        'clnAtivo
        '
        Me.clnAtivo.HeaderText = "Ativo"
        Me.clnAtivo.Name = "clnAtivo"
        Me.clnAtivo.ReadOnly = True
        Me.clnAtivo.Width = 70
        '
        'frmContas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.ClientSize = New System.Drawing.Size(499, 511)
        Me.Controls.Add(Me.cmbFilial)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.btnNovo)
        Me.Controls.Add(Me.btnSalvar)
        Me.Controls.Add(Me.dgvContas)
        Me.Name = "frmContas"
        Me.Text = "Contas de Movimentação"
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.dgvContas, 0)
        Me.Controls.SetChildIndex(Me.btnSalvar, 0)
        Me.Controls.SetChildIndex(Me.btnNovo, 0)
        Me.Controls.SetChildIndex(Me.btnFechar, 0)
        Me.Controls.SetChildIndex(Me.Label15, 0)
        Me.Controls.SetChildIndex(Me.cmbFilial, 0)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvContas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuListagem.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dgvContas As DataGridView
    Friend WithEvents btnFechar As Button
    Friend WithEvents btnNovo As Button
    Friend WithEvents btnSalvar As Button
    Friend WithEvents MenuListagem As ContextMenuStrip
    Friend WithEvents AtivarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DesativarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmbFilial As Controles.ComboBox_OnlyValues
    Friend WithEvents Label15 As Label
    Friend WithEvents IDFabricante As DataGridViewTextBoxColumn
    Friend WithEvents clnConta As DataGridViewTextBoxColumn
    Friend WithEvents clnBancaria As DataGridViewCheckBoxColumn
    Friend WithEvents clnAtivo As DataGridViewTextBoxColumn
End Class
