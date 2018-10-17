Imports CamadaBLL
'
Public Class frmContas
    Private dtConta As DataTable
    Private ImgInativo As Image = My.Resources.block
    Private ImgAtivo As Image = My.Resources.accept
    Private VerAlteracao As Boolean = False
    Private _formOrigem As Form
    Dim _Sit As Byte
    Dim _formEscolha As Boolean
    '
    Property IDContaEscolhida As Integer
    Property ContaEscolhida As String
    Property IDFilialEscolhida As Integer
    Property FilialEscolhida As String
    '
#Region "LOAD | PROPERTYS"
    '
    ' PROPRIEDADE SIT
    Private Property Sit As FlagEstado
        Get
            Sit = _Sit
        End Get
        Set(value As FlagEstado)
            _Sit = value
            Select Case _Sit
                Case FlagEstado.RegistroSalvo
                    '
                    If _formEscolha = False Then
                        btnNovo.Enabled = True
                    Else
                        btnNovo.Enabled = False
                    End If
                    '
                    If _formEscolha = True Then
                        btnSalvar.Enabled = True
                        btnSalvar.Text = "&Escolher"
                        btnSalvar.Image = My.Resources.accept
                        btnFechar.Text = "&Cancelar"
                    Else
                        btnSalvar.Enabled = False
                        btnSalvar.Text = "&Salvar"
                        btnSalvar.Image = My.Resources.save
                        btnFechar.Text = "&Fechar"
                    End If
                    '
                    cmbFilial.Enabled = True
                    '
                Case FlagEstado.Alterado
                    '
                    btnNovo.Enabled = True
                    btnSalvar.Enabled = True
                    btnSalvar.Text = "&Salvar"
                    btnSalvar.Image = My.Resources.save
                    btnFechar.Text = "&Cancelar"
                    cmbFilial.Enabled = False
                    '
                Case FlagEstado.NovoRegistro
                    '
                    btnNovo.Enabled = True
                    btnSalvar.Enabled = True
                    btnSalvar.Text = "&Salvar"
                    btnSalvar.Image = My.Resources.save
                    btnFechar.Text = "&Cancelar"
                    cmbFilial.Enabled = False
                    '
            End Select
        End Set
    End Property
    '
    Sub New(Optional Escolha As Boolean = False,
            Optional IDFilialDefault As Integer? = Nothing,
            Optional formOrigem As Form = Nothing)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        _formEscolha = Escolha
        _formOrigem = formOrigem
        '
        CarregaComboFilial()
        If IsNothing(IDFilialDefault) Then
            cmbFilial.SelectedValue = Obter_FilialPadrao()
        Else
            cmbFilial.SelectedValue = IDFilialDefault
        End If
        '
        PreencheListaContas()
        '
        If Escolha = True Then
            dgvContas.EditMode = DataGridViewEditMode.EditProgrammatically
            lblTitulo.Text = "Escolher Conta"
        Else
            Me.Width = 615

        End If
        '
        Sit = FlagEstado.RegistroSalvo
        VerAlteracao = True
        '
    End Sub
    '
#End Region
    '
#Region "DATAGRID"

    ' PREENCHE LISTAGEM
    Private Sub PreencheListaContas()
        Dim cBLL As New MovimentacaoBLL
        '
        dtConta = cBLL.Contas_GET_PorIDFilial(cmbFilial.SelectedValue)
        '
        dgvContas.Columns.Clear()
        dgvContas.AutoGenerateColumns = False
        dgvContas.RowHeadersWidth = 36
        '
        ' (1) COLUNA REG
        dgvContas.Columns.Add("IDConta", "Reg.")
        With dgvContas.Columns("IDConta")
            .DataPropertyName = "IDConta"
            .Width = 50
            .Visible = True
            .ReadOnly = True
            .Resizable = False
            .DefaultCellStyle.Format = "00"
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        '
        ' (2) COLUNA CONTA
        dgvContas.Columns.Add("Conta", "Descrição da Conta")
        With dgvContas.Columns("Conta")
            .DataPropertyName = "Conta"
            .Width = 200
            .Visible = True
            .ReadOnly = False
            .Resizable = False
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        '
        ' (3) COLUNA BANCARIA
        Dim colBancaria As New DataGridViewCheckBoxColumn
        With colBancaria
            .Name = "Bancaria"
            .HeaderText = "Bancaria"
            .DataPropertyName = "Bancaria"
            .Width = 80
            .Visible = True
            .ReadOnly = False
            .Resizable = False
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        dgvContas.Columns.Add(colBancaria)
        '
        ' (4) COLUNA DA IMAGEM
        Dim colImage As New DataGridViewImageColumn
        With colImage
            .Name = "Ativo"
            .Resizable = False
            .Width = 70
        End With
        dgvContas.Columns.Add(colImage)
        '
        ' (5) COLUNA ATIVO
        dgvContas.Columns.Add("FAtivo", "Ativo")
        With dgvContas.Columns("FAtivo")
            .DataPropertyName = "Ativo"
            .Width = 1
            .Visible = False
            .ReadOnly = True
            .Resizable = False
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        '
        If _formEscolha = False Then '--- COLUNAS QUE SO APARECEM NO FORM DE EDICÃO
            '
            ' (6) COLUNA DT BLOQUEIO
            dgvContas.Columns.Add("clnBloqueioData", "Bloqueio Até:")
            With dgvContas.Columns("clnBloqueioData")
                .DataPropertyName = "BloqueioData"
                .Width = 120
                .Visible = True
                .ReadOnly = True
                .Resizable = False
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            '
        End If
        '
        dgvContas.DataSource = dtConta
        '
    End Sub
    '
    ' CONTROLE DA LISTAGEM
    Private Sub dgvContas_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvContas.CellBeginEdit
        If Sit <> FlagEstado.NovoRegistro Then
            Sit = FlagEstado.Alterado
        End If
    End Sub
    '
    Private Sub dgvContas_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvContas.CellFormatting
        If e.ColumnIndex = 3 Then
            Dim dr As DataRowView = DirectCast(dgvContas.Rows(e.RowIndex).DataBoundItem, DataRowView)
            '
            If IsDBNull(dr("Ativo")) Then Exit Sub
            '
            If dr("Ativo") = True Then
                e.Value = ImgAtivo
            Else
                e.Value = ImgInativo
            End If
        End If
        '
        If _formEscolha = False AndAlso e.ColumnIndex = 5 Then
            If IsDBNull(e.Value) Then e.Value = "Desbloqueada"
        End If
        '
    End Sub
    '
    '--- SELECIONA O REGISTRO COM DOUBLE_CLIC
    Private Sub dgvContas_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContas.CellDoubleClick
        '
        If e.ColumnIndex = 1 Then Exit Sub
        '
        If _formEscolha = True And Sit = FlagEstado.RegistroSalvo Then
            EscolherRegistro()
        End If
        '
    End Sub
    '
#End Region
    '
#Region "MENU SUSPENSO"
    '
    ' CONTROLE DO MENU SUSPENSO
    Private Sub dgvContas_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvContas.MouseDown
        '
        If e.Button = MouseButtons.Right And _formEscolha = False Then
            '
            Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvContas.HitTest(e.X, e.Y)
            '
            dgvContas.ClearSelection()
            '
            '---VERIFICAÇÕES NECESSÁRIAS
            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
            '---se for um novo registro
            If Sit = FlagEstado.NovoRegistro Then
                MessageBox.Show("Salve o novo registro para depois alterar a ATIVAÇÃO de alguma Conta",
                                "Desativar/Ativar Conta", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                Exit Sub
            End If
            '
            ' seleciona o ROW
            dgvContas.Rows(hit.RowIndex).Cells(0).Selected = True
            dgvContas.Rows(hit.RowIndex).Selected = True
            '
            ' mostra o MENU ativar e desativar
            If dgvContas.Columns(hit.ColumnIndex).Name = "Ativo" Then
                If dgvContas.Rows(hit.RowIndex).Cells("FAtivo").Value = True Then
                    AtivarToolStripMenuItem.Enabled = False
                    DesativarToolStripMenuItem.Enabled = True
                Else
                    AtivarToolStripMenuItem.Enabled = True
                    DesativarToolStripMenuItem.Enabled = False
                End If
                ' revela menu
                MenuListagem.Show(c.PointToScreen(e.Location))
            End If
        End If
    End Sub
    '
    Private Sub AtivarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AtivarToolStripMenuItem.Click
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvContas.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        '--- altera o valor
        Dim r As DataRow = dtConta.Select("Conta = '" & dgvContas.CurrentRow.Cells("Conta").Value & "'").First
        r("Ativo") = True
        dtConta.AcceptChanges()
        If r.RowState = DataRowState.Unchanged Then
            r.SetModified()
        End If
        '
        '--- atualiza os botoes
        Sit = FlagEstado.Alterado
    End Sub
    '
    Private Sub DesativarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesativarToolStripMenuItem.Click
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvContas.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        '--- altera o valor
        Dim r As DataRow = dtConta.Select("Conta = '" & dgvContas.CurrentRow.Cells("Conta").Value & "'").First
        r("Ativo") = False
        dtConta.AcceptChanges()
        If r.RowState = DataRowState.Unchanged Then
            r.SetModified()
        End If
        '
        '--- atualiza os botoes
        Sit = FlagEstado.Alterado
    End Sub
    '
#End Region
    '
#Region "BUTONS"
    '
    ' BOTOES DO FORMULARIO
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        '
        '---adiciona novo ROW no datatable 
        dtConta.Rows.Add()
        Sit = FlagEstado.NovoRegistro
        '
        ' seleciona a cell
        dgvContas.Focus()
        dgvContas.CurrentCell = dgvContas.Rows(dgvContas.Rows.Count - 1).Cells(1)
        dgvContas.BeginEdit(True)
        '
        '---adiciona a imagem no NOVO ROW
        dgvContas.CurrentRow.Cells("Ativo").Value = My.Resources.NovoPeq
        '
    End Sub
    '
    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        '
        If Sit = FlagEstado.RegistroSalvo Then
            DialogResult = DialogResult.Cancel
            Close()
            If _formEscolha = False Then MostraMenuPrincipal()
            Exit Sub
        ElseIf Sit = FlagEstado.NovoRegistro Then
            If MessageBox.Show("Deseja cancelar todas as alterações realizadas?",
                               "Cancelar Alterações", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            dgvContas.Rows.Remove(dgvContas.CurrentRow)
            PreencheListaContas()
            Sit = FlagEstado.RegistroSalvo
        ElseIf Sit = FlagEstado.Alterado Then
            If MessageBox.Show("Deseja cancelar todas as alterações realizadas?",
                               "Cancelar Alterações", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
            dgvContas.CancelEdit()
            PreencheListaContas()
            Sit = FlagEstado.RegistroSalvo
        End If
        '
    End Sub
    '
#End Region
    '
#Region "SALVAR"
    '
    ' SALVAR REGISTRO
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        '
        If (_formEscolha = False) Or (_formEscolha = True And Sit <> FlagEstado.RegistroSalvo) Then
            SalvarAlteracoes()
        Else
            EscolherRegistro()
        End If
        '
    End Sub
    '
    Private Sub SalvarAlteracoes()
        '
        Dim SQL As New SQLControl
        '
        '-- variavel para informar o número de registros salvos
        Dim regSalvos As Integer = 0
        '
        For Each r As DataGridViewRow In dgvContas.Rows
            ' verfica se já existe valor igual
            If dtConta.Rows(r.Index).RowState <> DataRowState.Unchanged Then
                If VerificaDuplicado(r.Index, dgvContas.Rows(r.Index).Cells(1).Value) = True Then
                    MessageBox.Show("Já existe uma Conta com a mesma descrição:" & vbNewLine &
                                        CStr(dgvContas.Rows(r.Index).Cells(1).Value).ToUpper,
                                        "Valor Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Continue For
                End If
            End If
            '
            '---salva os registros
            If dtConta.Rows(r.Index).RowState = DataRowState.Modified Then ' registro ALTERADO
                '
                Dim strSQL As String = String.Format("UPDATE tblContas SET Conta = '{0}', Ativo = '{1}', Bancaria = '{2}', IDFilial = {3} WHERE IDConta = {4}",
                                                     dgvContas.Rows(r.Index).Cells("Conta").Value,
                                                     dgvContas.Rows(r.Index).Cells("FAtivo").Value,
                                                     dgvContas.Rows(r.Index).Cells("Bancaria").Value,
                                                     cmbFilial.SelectedValue,
                                                     dgvContas.Rows(r.Index).Cells(0).Value)

                SQL.ExecQuery(strSQL)
                regSalvos += 1
            ElseIf dtConta.Rows(r.Index).RowState = DataRowState.Added Then ' registro NOVO
                '
                Dim strSQL As String = String.Format("INSERT INTO tblContas (Conta, Bancaria, Ativo, IDFilial) " &
                                                     "VALUES ('{0}', '{1}', 'TRUE', {2})",
                                                    dgvContas.Rows(r.Index).Cells(1).Value,
                                                    dgvContas.Rows(r.Index).Cells("Bancaria").Value,
                                                    cmbFilial.SelectedValue)
                '
                SQL.ExecQuery(strSQL, True)
                regSalvos += 1
            End If
            '
            '---veridica se houve erro
            If SQL.HasException(True) Then
                MessageBox.Show("O seguinte registro não pôde ser salvo:" & vbNewLine &
                                CStr(dgvContas.Rows(r.Index).Cells(1).Value).ToUpper, "Erro ao Salvar",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                regSalvos -= 1
            End If
        Next
        '
        '--- mensagem de sucesso---
        If regSalvos > 0 Then
            MessageBox.Show("Sucesso ao salvar registro(s) de Contas" & vbNewLine &
                            "Foram salvo(s) com sucesso " & Format(regSalvos, "00") &
                            IIf(regSalvos > 1, " registros", " registro"),
                            "Registro(s) Salvo(s)", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        '
        '---preencher a listagem com os novos valores
        PreencheListaContas()
        '
        Sit = FlagEstado.RegistroSalvo
        '
    End Sub
    '
    ' VERIFICAR SE EXISTE UM REGISTRO COM A MESMA DESCRIÇÃO
    Private Function VerificaDuplicado(myRow As Integer, myNewValor As String) As Boolean
        '---se não houver nenhum registro, Exit
        If dtConta.Rows.Count = 0 Then
            VerificaDuplicado = False
            Exit Function
        End If
        '---verifica todos os ROWS procurando registro igual
        For i = 0 To dtConta.Rows.Count - 1
            If i = myRow Then Continue For '---se for a mesma ROW não verifica
            If dtConta.Rows(i).Item("Conta").ToString.ToUpper = myNewValor Then
                VerificaDuplicado = True
                Exit Function
            End If
        Next i
        '---se não encontrar retorna FALSE
        VerificaDuplicado = False
    End Function
    '
    Private Sub EscolherRegistro()
        '
        If dgvContas.SelectedCells.Count = 0 Then
            MessageBox.Show("Selecione uma Conta antes de pressionar Escolher...", "Selecionar", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvContas.Focus()
            Exit Sub
        End If
        '
        Dim dr As DataRowView = dgvContas.Rows(dgvContas.SelectedCells(0).RowIndex).DataBoundItem
        '
        If dr("Ativo") = False Then
            MessageBox.Show("Você não pode escolher uma CONTA INATIVA...", "Escolher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            dgvContas.Focus()
            Exit Sub
        End If
        '
        ContaEscolhida = dr("Conta")
        IDContaEscolhida = dr("IDConta")
        IDFilialEscolhida = dr("IDFilial")
        FilialEscolhida = cmbFilial.Text
        '
        DialogResult = DialogResult.OK
        '
    End Sub
    '
#End Region
    '
#Region "DATAGRID VALIDACAO"
    '
    Private Sub dgvContas_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles dgvContas.CellValidating
        ' Valida o entrada para a descrição não permitindo valores em branco
        If dgvContas.Columns(e.ColumnIndex).Name = "Conta" Then
            If String.IsNullOrEmpty(e.FormattedValue.ToString()) Then
                dgvContas.Rows(e.RowIndex).ErrorText = "A DESCRIÇÃO da Conta não pode ficar vazia..."
                e.Cancel = True
                Exit Sub
            End If
        End If
        '---procura por valores duplicados da descrição 
        If VerificaDuplicado(e.RowIndex, e.FormattedValue.ToString.ToUpper) Then
            MessageBox.Show("Já existe uma Conta com a mesma descrição:" & vbNewLine &
                    e.FormattedValue.ToString.ToUpper,
                    "Valor Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            dgvContas.Rows(e.RowIndex).ErrorText = "A DESCRIÇÃO da Conta precisa ser EXCLUSIVA..."
            e.Cancel = True
            Exit Sub
        End If
    End Sub
    '
    Private Sub dgvContas_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContas.CellEndEdit
        ' Limpa o erro da linha no caso do usuário pressionar ESC
        dgvContas.Rows(e.RowIndex).ErrorText = String.Empty
    End Sub

    Private Sub dgvContas_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvContas.EditingControlShowing
        '---restringe a entrada de dados para a coluna 'Forma de Cobrança'
        If Me.dgvContas.CurrentCell.ColumnIndex = 1 And Not e.Control Is Nothing Then
            Dim tb As TextBox = CType(e.Control, TextBox)
            '---inclui um tratamento de evento para o controle TextBox---
            AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
        End If
    End Sub
    '
    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)
        '---Se o usuario pressionou a tecla ESC na edição ---
        If e.KeyChar = Convert.ToChar(27) Then
            If Sit <> FlagEstado.RegistroSalvo Then
                e.Handled = True
                '---cancela a adição do registro
                If Sit = FlagEstado.NovoRegistro Then dgvContas.Rows.Remove(dgvContas.CurrentRow)
                PreencheListaContas()
                Sit = FlagEstado.RegistroSalvo
            End If
        End If
    End Sub
    '
#End Region
    '
#Region "COMBO CONTROLE"
    '----------------------------------------------------------------------------
    ' PREENCHE O COMBO DAS FILIAIS
    '----------------------------------------------------------------------------
    Private Sub CarregaComboFilial()
        Dim db As New FilialBLL
        '
        Try
            Dim dtFil As DataTable = db.GetFiliais(True)
            '
            cmbFilial.DataSource = dtFil
            cmbFilial.ValueMember = "IDFilial"
            cmbFilial.DisplayMember = "ApelidoFilial"
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção inesperada ocorreu ao obter a lsita de Filiais...", "Filiais",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    Private Sub cmbFilial_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbFilial.SelectedValueChanged
        If VerAlteracao = False Then Exit Sub
        PreencheListaContas()
    End Sub
    '
#End Region
    '
#Region "EFEITOS SUB-FORMULARIO PADRAO"
    '
    '-------------------------------------------------------------------------------------------------
    ' CONSTRUIR UMA BORDA NO FORMULÁRIO
    '-------------------------------------------------------------------------------------------------
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        MyBase.OnPaintBackground(e)

        Dim rect As New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
        Dim pen As New Pen(Color.DarkSlateGray, 3)

        e.Graphics.DrawRectangle(pen, rect)
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub frmAPagarItem_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If IsNothing(_formOrigem) Then Exit Sub
        '
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.Silver
    End Sub
    '
    Private Sub frmAPagarItem_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        If IsNothing(_formOrigem) Then Exit Sub
        '
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.SlateGray
    End Sub
    '
#End Region
    '
End Class
