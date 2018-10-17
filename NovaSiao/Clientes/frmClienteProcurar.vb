Imports CamadaBLL
Imports ComponentOwl.BetterListView

Public Class frmClienteProcurar
    '
    Private dtClientes As DataTable
    Dim Px, Py As Integer
    Dim Mover As Boolean
    Private ClienteAtivo As Image = My.Resources.Cliente_Ativo
    Private ClienteInativo As Image = My.Resources.Cliente_Inativo
    Private _formOrigem As Form = Nothing
    '
#Region "PROPRIEDADES, NEW E LOAD"
    '--- PROPRIEDADES
    Public Property propClienteID As Integer? '--- Define o IDPessoa do Cliente Selecionado
    Public Property propClienteRG() As Integer? '--- Define o RGCliente do Cliente Selecionado
    Public Property propClienteTipo() As Byte? '--- Define o Tipo do Cliente Selecionado (JURIDICA OU FISICA)
    Public Property propClienteUF() As String '--- Define o UF do Cliente Selecionado
    '
    Private WriteOnly Property propFormOrigem As Form
        Set(ByVal value As Form)
            _formOrigem = value
        End Set
    End Property
    '
    '--- SUB NEW
    Sub New(Optional formOrigem As Form = Nothing)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        If Not IsNothing(formOrigem) Then
            propFormOrigem = formOrigem
        End If
        '
    End Sub
    '
    '--- FORM LOAD
    Private Sub frmClienteProcurar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        '--- Carrega o Combo de Atividades
        CarregaAtividade()
        '
        If cmbRGAtividade.Items.Count > 0 Then
            cmbRGAtividade.SelectedIndex = 0
        Else
            MessageBox.Show("Não há nenhum TIPO de Cliente Cadastrado no sistema..." & vbNewLine &
                            "Favor inserir ao menos uma ATIVIDADE DE CLIENTE",
                            "Não Há TIPOS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End If
        '
        '--- Carrega Combo Ativo
        CarregaAtivo()
        cmbAtivo.SelectedIndex = 0
        '
        PreencheListagem()
        FormataListagem()
    End Sub
    '
#End Region
    '
#Region "PREENCHER COMBOS"
    ' PREENCHE COMBO ATIVIDADES
    Private Sub CarregaAtividade()
        Dim cliBLL As New ClienteBLL
        '
        Try
            Dim dtAtiv As DataTable = cliBLL.GetClienteAtividades()
            '
            With cmbRGAtividade
                .DataSource = dtAtiv
                .ValueMember = "RGAtividade"
                .DisplayMember = "Atividade"
            End With
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- PREENCHE COMBO ATIVO
    Private Sub CarregaAtivo()
        Dim cliBLL As New ClienteBLL
        '
        Try
            Dim dtSit As DataTable = cliBLL.GetClienteSituacao()
            '
            With cmbAtivo
                .DataSource = dtSit
                .ValueMember = "IDSituacao"
                .DisplayMember = "Situacao"
            End With
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
#End Region
    '
#Region "LISTAGEM"
    '--- PREENCHE LISTAGEM
    Private Sub PreencheListagem()
        '--- Preenche o DataTable
        Dim SQL As New SQLControl
        '
        Try
            SQL.ExecQuery(mySQLWhere)
            If SQL.HasException(True) Then Exit Sub
            dtClientes = SQL.DBDT
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        Finally
            SQL = Nothing
        End Try
        '
        lstListagem.DataSource = dtClientes
        '
    End Sub
    '
    '--- FORMATA LISTAGEM DE CLIENTE
    Private Sub FormataListagem()
        clnRGCadastro.DisplayMember = "RGCliente"
        clnRGCadastro.Width = 50
        '
        clnCadastroNome.DisplayMember = "Cadastro"
        clnCadastroNome.Width = 300
        '
        clnCNP.DisplayMember = "CNP"
        clnCNP.Width = 190
        '
        clnAtivoImagem.Width = 60 ' imagem de ativo ou inativo
        '
        clnIDSituacao.DisplayMember = "IDSituacao"
        clnIDSituacao.Width = 0
        '
        clnRGAtividade.DisplayMember = "RGAtividade"
        clnRGAtividade.Width = 0
        '
        clnPessoaTipo.DisplayMember = "PessoaTipo"
        clnPessoaTipo.Width = 0
        '
        clnUF.DisplayMember = "UF"
        clnUF.Width = 0
        '
        clnID.DisplayMember = "IDPessoa"
        clnID.Width = 0
        '
        '
        lstListagem.SearchSettings = New BetterListViewSearchSettings(BetterListViewSearchMode.PrefixOrSubstring,
                                                                      BetterListViewSearchOptions.UpdateSearchHighlight,
                                                                      New Integer() {0, 1, 2})
    End Sub
    '
    '--- DESIGN DA LISTAGEM
    Private Sub lstListagem_DrawColumnHeader(sender As Object, eventArgs As BetterListViewDrawColumnHeaderEventArgs) Handles lstListagem.DrawColumnHeader
        '
        If eventArgs.ColumnHeaderBounds.BoundsOuter.Width > 0 AndAlso eventArgs.ColumnHeaderBounds.BoundsOuter.Height > 0 Then
            Dim brush As Brush = New SolidBrush(Color.LightSteelBlue)
            '
            eventArgs.Graphics.FillRectangle(brush, eventArgs.ColumnHeaderBounds.BoundsOuter)
            eventArgs.Graphics.DrawString(eventArgs.ColumnHeader.Text, New Font("Verdana", 12, FontStyle.Regular), New SolidBrush(Color.Black), eventArgs.ColumnHeaderBounds.BoundsText)
            '
            brush.Dispose()
        End If
        '
    End Sub
    '
    Private Sub lstListagem_DrawItem(sender As Object, eventArgs As BetterListViewDrawItemEventArgs) Handles lstListagem.DrawItem
        '
        If IsNumeric(eventArgs.Item.Text) Then
            eventArgs.Item.Text = Format(CInt(eventArgs.Item.Text), "0000")
        End If
        '
        eventArgs.Item.SubItems(3).AlignHorizontalImage = BetterListViewImageAlignmentHorizontal.OverlayCenter
        '
        If eventArgs.Item.SubItems(4).Text = "1" Then
            eventArgs.Item.SubItems(3).Image = ClienteAtivo
        ElseIf eventArgs.Item.SubItems(4).Text = "2" Then
            eventArgs.Item.SubItems(3).Image = ClienteInativo
        End If
        '
    End Sub
    '
    Private Sub lstListagem_ItemActivate(sender As Object, eventArgs As BetterListViewItemActivateEventArgs) Handles lstListagem.ItemActivate
        btnEscolher_Click(New Object, New System.EventArgs)
    End Sub
    '
#End Region
    '
#Region "BUTONS FUNCTION"
    ' BOTÃO FECHAR
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
    '
    ' ESCOLHER O CLIENTE
    Private Sub btnEscolher_Click(sender As Object, e As EventArgs) Handles btnEscolher.Click
        '
        If lstListagem.SelectedItems.Count = 0 Then
            MessageBox.Show("Não nenhum Cliente selecionado..." & vbNewLine &
                            "Favor antes selecione um cliente!",
                            "Escolher Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        propClienteRG = lstListagem.SelectedItems(0).Text 'Número de Registro do Cliente RGCliente
        propClienteID = lstListagem.SelectedItems(0).SubItems(8).Text 'ID de Registro do Cliente IDPessoa
        propClienteTipo = lstListagem.SelectedItems(0).SubItems(5).Text 'Se é PF ou PJ
        propClienteUF = lstListagem.SelectedItems(0).SubItems(7).Text 'UF do Cliente
        '
        Me.DialogResult = DialogResult.OK
        Me.Close()
        '
    End Sub
    '
#End Region
    '
#Region "FUNCAO PROCURAR CLIENTE"
    ' PROCURAR CLIENTE
    Private Sub ProcurarCliente()
        '
        Dim dvClientes As DataView = dtClientes.DefaultView
        '
        If txtProcurar.TextLength > 0 AndAlso IsNumeric(txtProcurar.Text.Substring(0, 1)) Then
            If Integer.TryParse(txtProcurar.Text, Nothing) Then ' PROCURA PELO RG
                dvClientes.RowFilter = "RGCliente = " & txtProcurar.Text ' & " OR " & "CNP LIKE '*" & txtProcurar.Text & "*'"
            Else
                dvClientes.RowFilter = "CNP LIKE '*" & txtProcurar.Text & "*'" ' PROCURA PELO CNP
            End If
        Else
            dvClientes.RowFilter = "Cadastro LIKE '*" & txtProcurar.Text & "*'" ' PROCURA PELO NOME
        End If
        '
    End Sub
    '
    Private Sub txtProcurar_TextChanged(sender As Object, e As EventArgs) Handles txtProcurar.TextChanged
        '
        ProcurarCliente()
        '
        Dim itemsFound As BetterListViewItemCollection
        '
        If txtProcurar.Text.Length > 0 Then
            itemsFound = lstListagem.FindItemsWithText(txtProcurar.Text)
            ' select found items
            lstListagem.SelectedItems.[Set](itemsFound)
        Else
            lstListagem.FindItemsWithText("?")
            lstListagem.SelectedItems.Clear()
        End If
        '
    End Sub
    '
    Private Sub cmbRGAtividade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRGAtividade.SelectedIndexChanged, cmbAtivo.SelectedIndexChanged
        '
        If IsNumeric(cmbRGAtividade.SelectedValue) AndAlso Not IsNothing(dtClientes) Then
            txtProcurar.Clear()
            lstListagem.FindItemsWithText("?")
            lstListagem.SelectedItems.Clear()
            PreencheListagem()
        End If
        '
    End Sub
    '
    Private Function mySQLWhere() As String
        '
        If Not IsNumeric(cmbRGAtividade.SelectedValue) Then
            mySQLWhere = $"SELECT * FROM qryClienteAll WHERE RGAtividade = 1 AND IDSituacao = 1"
        Else
            mySQLWhere = $"SELECT * FROM qryClienteAll WHERE RGAtividade = {cmbRGAtividade.SelectedValue} AND IDSituacao = {cmbAtivo.SelectedValue}"
        End If
        '
    End Function
    '
    '------------------------------------------------------------------------------------------------------------
    ' FAZER COM PGDOWN E PGUP SE TORNEM ATALHOS
    '------------------------------------------------------------------------------------------------------------
    Private Sub frmClienteProcurar_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.PageDown Then
            Dim maxIndex As Byte = cmbRGAtividade.Items.Count - 1
            '
            If cmbRGAtividade.SelectedIndex < maxIndex Then
                cmbRGAtividade.SelectedIndex += 1
            Else
                cmbRGAtividade.SelectedIndex = 0
            End If
            e.Handled = True
        ElseIf e.KeyCode = Keys.PageUp Then
            If cmbRGAtividade.SelectedIndex > 0 Then
                cmbRGAtividade.SelectedIndex -= 1
            Else
                cmbRGAtividade.SelectedIndex = cmbRGAtividade.Items.Count - 1
            End If
            e.Handled = True
        End If
    End Sub
    '
#End Region
    '
#Region "EFEITOS VISUAIS"
    '
    ' MOVER O FORMULÁRIO SEM BORDA
    Private Sub Painel_MouseDown(sender As Object, e As MouseEventArgs) Handles Painel.MouseDown
        Px = e.X
        Py = e.Y
        Mover = True
    End Sub
    '
    Private Sub Painel_MouseUp(sender As Object, e As MouseEventArgs) Handles Painel.MouseUp
        Mover = False
    End Sub
    '
    Private Sub Painel_MouseMove(sender As Object, e As MouseEventArgs) Handles Painel.MouseMove
        If Mover = True Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - Px,
                                                     MousePosition.Y - Me.Location.Y - Py))
        End If
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CONSTRUIR UMA BORDA NO FORMULÁRIO
    '-------------------------------------------------------------------------------------------------
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        MyBase.OnPaintBackground(e)

        Dim rect As New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
        Dim pen As New Pen(Color.SlateGray, 3)

        e.Graphics.DrawRectangle(pen, rect)
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Not IsNothing(_formOrigem) Then
            Dim pnl As Panel = _formOrigem.Controls("Panel1")
            pnl.BackColor = Color.Silver
        End If
    End Sub
    '
    Private Sub frmProdutoProcurar_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If Not IsNothing(_formOrigem) Then
            Dim pnl As Panel = _formOrigem.Controls("Panel1")
            pnl.BackColor = Color.SlateGray
        End If
    End Sub
    '
#End Region
    '
End Class