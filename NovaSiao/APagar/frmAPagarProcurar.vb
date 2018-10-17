Imports CamadaBLL
Imports CamadaDTO
Imports System.Drawing.Drawing2D
'
Public Class frmAPagarProcurar
    Private pagLista As New List(Of clAPagar)
    Private _myMes As Date
    Private SituacaoAtual As Byte? '--- property
    Private _Sit As FlagEstado
    '
#Region "PROPERTY E EVENTO LOAD"
    '
    Private Property myMes() As DateTime
        Get
            Return _myMes
        End Get
        Set(ByVal value As DateTime)
            If CDate(value.ToShortDateString) > CDate(Now.ToShortDateString) Then
                value = Now.ToShortDateString
                btnPeriodoPosterior.Enabled = False
            Else
                btnPeriodoPosterior.Enabled = True
            End If
            _myMes = value
            lblPeriodo.Text = Format(_myMes, "MMMM | yyyy")
        End Set
    End Property
    '
    Public Property Sit() As FlagEstado
        Get
            Return _Sit
        End Get
        Set(ByVal value As FlagEstado)
            _Sit = value
            '
            If value = FlagEstado.Alterado Then
                btnProcurar.Enabled = True
                dgvListagem.Columns.Clear()
            ElseIf value = FlagEstado.RegistroSalvo Then
                btnProcurar.Enabled = False
            End If
            '
        End Set
    End Property
    '
    Public Property propSituacaoAtual() As Byte?
        Get
            If rbtEmAberto.Checked = True Then
                SituacaoAtual = 0 '--- em aberto
            ElseIf rbtQuitadas.Checked = True Then
                SituacaoAtual = 1 '--- pagas
            ElseIf rbtCanceladas.Checked = True Then
                SituacaoAtual = 2 '--- canceladas
            ElseIf rbtNegativadas.Checked = True Then
                SituacaoAtual = 3 '--- negativadas
            ElseIf rbtNegociadas.Checked = True Then
                SituacaoAtual = 4 '--- Negociadas
            ElseIf rbtTodas.Checked = True Then
                SituacaoAtual = Nothing '--- Todas
            Else
                SituacaoAtual = Nothing
            End If
            '
            Return SituacaoAtual
            '
        End Get
        '
        Set(ByVal value As Byte?)
            SituacaoAtual = value
            '
            RemoverHandler()
            '
            Select Case SituacaoAtual
                Case 0 '--- Em aberto
                    rbtEmAberto.Checked = True
                    rbtQuitadas.Checked = False
                    rbtCanceladas.Checked = False
                    rbtNegativadas.Checked = False
                    rbtNegociadas.Checked = False
                    rbtTodas.Checked = False
                Case 1 '--- Pagas
                    rbtEmAberto.Checked = False
                    rbtQuitadas.Checked = True
                    rbtCanceladas.Checked = False
                    rbtNegativadas.Checked = False
                    rbtNegociadas.Checked = False
                    rbtTodas.Checked = False
                Case 2 '--- Canceladas
                    rbtEmAberto.Checked = False
                    rbtQuitadas.Checked = False
                    rbtCanceladas.Checked = True
                    rbtNegativadas.Checked = False
                    rbtNegociadas.Checked = False
                    rbtTodas.Checked = False
                Case 3 '--- Negativadas
                    rbtEmAberto.Checked = False
                    rbtQuitadas.Checked = False
                    rbtCanceladas.Checked = False
                    rbtNegativadas.Checked = True
                    rbtNegociadas.Checked = False
                    rbtTodas.Checked = False
                Case 4 '--- Negociadas
                    rbtEmAberto.Checked = False
                    rbtQuitadas.Checked = False
                    rbtCanceladas.Checked = False
                    rbtNegativadas.Checked = False
                    rbtNegociadas.Checked = True
                    rbtTodas.Checked = False
                Case Else '--- Todas
                    rbtEmAberto.Checked = False
                    rbtQuitadas.Checked = False
                    rbtCanceladas.Checked = False
                    rbtNegativadas.Checked = False
                    rbtNegociadas.Checked = False
                    rbtTodas.Checked = True
            End Select
            '
            AdicionaHandler()
            '
        End Set
    End Property
    '
    Private Sub frmAPagarProcurar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        myMes = ObterDefault("DataPadrao")
        lblPeriodo.Text = Format(myMes, "MMMM | yyyy")
        propSituacaoAtual = 0
        FormataListagem()
        '
        AddHandler dtMes.DateChanged, AddressOf dtMes_DateChanged
        '
        pnlMes.Location() = New Point(636, 130)
        '
        CarregaCmbCobrancaForma()
        AddHandler cmbCobrancaForma.SelectedIndexChanged, AddressOf cmbCobrancaForma_SelectedIndexChanged
        '
    End Sub
    '
#End Region
    '
#Region "PREENCHE TRATA COMBO"
    '-------------------------------------------------------------------------------------------------
    ' PREENCHE COMBO
    '-------------------------------------------------------------------------------------------------
    Private Sub CarregaCmbCobrancaForma()
        Dim pg As New APagarBLL
        '
        Try
            Dim dtForma As DataTable = pg.CobrancaFormas_APagar_GET
            dtForma.Rows.Add({0, "TODAS FORMAS"})
            '
            With cmbCobrancaForma
                .DataSource = dtForma
                .ValueMember = "IDCobrancaForma"
                .DisplayMember = "CobrancaForma"
                .SelectedValue = 0
            End With
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao obter a lista de Formas Pagamento:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
#End Region
    '
#Region "LISTAGEM CONFIGURAÇÃO"
    Private Sub FormataListagem()
        '
        '--- limpa as colunas do datagrid
        dgvListagem.Columns.Clear()
        dgvListagem.AutoGenerateColumns = False
        '
        ' altera as propriedades importantes
        dgvListagem.MultiSelect = False
        dgvListagem.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvListagem.ColumnHeadersVisible = False
        dgvListagem.AllowUserToResizeRows = False
        dgvListagem.AllowUserToResizeColumns = False
        dgvListagem.RowHeadersVisible = True
        dgvListagem.RowHeadersWidth = 30
        dgvListagem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvListagem.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvListagem.StandardTab = True
        '
        Get_Dados()
        PreencheListagem()
        '
    End Sub
    '
    Private Sub Get_Dados()
        '
        Dim pagBLL As New APagarBLL
        '--- Verifica o IDCobrancaForma
        Dim myIDCobrancaForma As Int16?
        '
        If cmbCobrancaForma.SelectedIndex = -1 OrElse cmbCobrancaForma.SelectedValue = 0 Then
            myIDCobrancaForma = Nothing
        Else
            myIDCobrancaForma = cmbCobrancaForma.SelectedValue
        End If
        '
        '--- consulta o banco de dados
        Try
            '--- verifica o filtro das datas
            If chkPeriodoTodos.Checked = True Then
                pagLista = pagBLL.GetAPagarLista_Procura(myIDCobrancaForma, txtCredorCadastro.Text)
            Else
                Dim f As New FuncoesUtilitarias
                Dim dtInicial As Date = f.FirstDayOfMonth(myMes)
                Dim dtFinal As Date = f.LastDayOfMonth(myMes)
                '
                pagLista = pagBLL.GetAPagarLista_Procura(myIDCobrancaForma, txtCredorCadastro.Text, dtInicial, dtFinal)
            End If
            '
            FiltraSituacao()
            '
            Sit = FlagEstado.RegistroSalvo
            '
        Catch ex As Exception
            MessageBox.Show("Ocorreu exceção ao obter a lista de A Pagar..." & vbNewLine &
            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    Private Sub PreencheListagem()
        '
        ' (0) COLUNA VENCIMENTO
        dgvListagem.Columns.Add("clnVencimento", "Vencto")
        With dgvListagem.Columns("clnVencimento")
            .DataPropertyName = "Vencimento"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.DefaultCellStyle.Font = New Font("Arial Narrow", 12)
        End With
        '
        ' (1) COLUNA CREDOR
        dgvListagem.Columns.Add("clnCadastro", "Credor/Fornecedor")
        With dgvListagem.Columns("clnCadastro")
            .DataPropertyName = "Cadastro"
            .Width = 280
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (2) COLUNA ORIGEM
        dgvListagem.Columns.Add("clnOrigemTexto", "Origem")
        With dgvListagem.Columns("clnOrigemTexto")
            .DataPropertyName = "OrigemTexto"
            .Width = 150
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (3) COLUNA FORMA
        dgvListagem.Columns.Add("clnCobrancaForma", "Forma de Cobranca")
        With dgvListagem.Columns("clnCobrancaForma")
            .DataPropertyName = "CobrancaForma"
            .Width = 150
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (4) COLUNA VALOR
        dgvListagem.Columns.Add("clnAPagarValor", "Valor")
        With dgvListagem.Columns("clnAPagarValor")
            .DataPropertyName = "APagarValor"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "C"
        End With
        '
        ' (5) COLUNA SITUAÇÃO
        dgvListagem.Columns.Add("clnSituacao", "Situacao")
        With dgvListagem.Columns("clnSituacao")
            .DataPropertyName = "SituacaoDescricao"
            .Width = 80
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        '
    End Sub
    '
    Private Sub dgvListagem_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvListagem.CellFormatting
        '
        If DirectCast(dgvListagem.Rows(e.RowIndex).DataBoundItem, clAPagar).Origem = 3 Then
            dgvListagem.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Blue
            dgvListagem.Rows(e.RowIndex).DefaultCellStyle.SelectionForeColor = Color.Yellow
        Else
            dgvListagem.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            dgvListagem.Rows(e.RowIndex).DefaultCellStyle.SelectionForeColor = Color.White
        End If
        '
        '--- Define o texto do APAGAR quando esta VENCIDO
        If e.ColumnIndex = 5 AndAlso e.Value = "Em Aberto" Then
            Dim dtVenc As Date = dgvListagem.Rows(e.RowIndex).Cells(0).Value
            If dtVenc < Date.Today Then
                e.Value = "Vencida"
                e.CellStyle.ForeColor = Color.Red
            End If
        End If
        '
    End Sub
    '
#End Region
    '
#Region "ACAO DOS BOTOES"
    '
    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        If dgvListagem.SelectedRows.Count = 0 Then
            MessageBox.Show("Não existe nenhum A Pagar selecionado na listagem", "Escolher",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        Dim clP As clAPagar = dgvListagem.SelectedRows(0).DataBoundItem
        '
        Dim f As New frmAPagarSaidas(clP, Me)
        '
        f.ShowDialog()
        '
        '--- Se retornar DIALOGRESULT = OK THEN
        If f.DialogResult <> DialogResult.Cancel Then
            FormataListagem()
        End If
        '
    End Sub
    '
    Private Sub btnProcurar_Click(sender As Object, e As EventArgs) Handles btnProcurar.Click
        Get_Dados()
        PreencheListagem()
        dgvListagem.Focus()
        '
        If pagLista.Count = 0 Then
            MessageBox.Show("Nenhuma A Pagar encontrado nessas condições...", "Procurar",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        '
    End Sub
    '
    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click

        If dgvListagem.SelectedRows.Count = 0 Then
            MessageBox.Show("Não existe nenhum A Pagar selecionado na listagem", "Escolher",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        Dim clP As clAPagar = dgvListagem.SelectedRows(0).DataBoundItem
        '
        If clP.Origem = 1 Then '--- A Origem é transacao de compras
            Dim cBLL As New CompraBLL
            '
            Dim clC As clCompra = cBLL.GetCompra_PorID_OBJ(clP.IDOrigem)
            '
            Dim frm = New frmCompra(clC)
            frm.MdiParent = frmPrincipal
            frm.StartPosition = FormStartPosition.CenterScreen
            Close()
            frm.Show()
        ElseIf clP.Origem = 2 Then '--- A Origem é Despesa
            Dim dBLL As New DespesaBLL
            '
            Dim clD As clDespesa = dBLL.GetDespesa_PorID(clP.IDOrigem)
            '
            Dim frm As New frmDespesa(clD)
            frm.MdiParent = frmPrincipal
            frm.StartPosition = FormStartPosition.CenterScreen
            Close()
            frm.Show()
        ElseIf clP.Origem = 3 Then '--- A Origem é DespesaPeriodica
            Dim dBLL As New DespesaPeriodicaBLL
            '
            Dim clD As clDespesaPeriodica = dBLL.GetDespesaPeriodica_PorID(clP.IDOrigem)
            '
            Dim frm As New frmDespesaPeriodica(clD)
            frm.MdiParent = frmPrincipal
            frm.StartPosition = FormStartPosition.CenterScreen
            Close()
            frm.Show()
        End If
        '
    End Sub
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click, btnClose.Click
        Close()
        MostraMenuPrincipal()
    End Sub
    '
    Private Sub dgvListagem_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListagem.CellDoubleClick
        btnQuitar_Click(New Object, New EventArgs)
    End Sub
#End Region
    '
#Region "CONTROLES GERAIS | FLUXO"
    '
    '--- SELECIONAR ITEM QUANDO PRESSIONA A TECLA (ENTER)
    Private Sub dgvListagem_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvListagem.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            '
            btnQuitar_Click(New Object, New EventArgs)
        End If
    End Sub
    '
    '--- AO PRESSIONAR A TECLA (ENTER) ENVIAR (TAB)
    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCredorCadastro.KeyDown
        '
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PreencheListagem()
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
    Private Sub txtDescricao_TextChanged(sender As Object, e As EventArgs) Handles txtCredorCadastro.TextChanged
        '
        If Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
    Private Sub cmbCobrancaForma_SelectedIndexChanged(sender As Object, e As EventArgs)
        '
        If Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
#End Region
    '
#Region "CONTROLE DO PERÍODO"
    Private Sub btnPeriodoAnterior_Click(sender As Object, e As EventArgs) Handles btnPeriodoAnterior.Click
        myMes = myMes.AddMonths(-1)
        dtMes.SelectionStart = myMes
    End Sub
    '
    Private Sub btnPeriodoPosterior_Click(sender As Object, e As EventArgs) Handles btnPeriodoPosterior.Click
        myMes = myMes.AddMonths(1)
        dtMes.SelectionStart = myMes
        '
        If Year(myMes) = Year(Now) AndAlso Month(myMes) = Month(Now) Then
            btnPeriodoPosterior.Enabled = False
        Else
            btnPeriodoPosterior.Enabled = True
        End If
        '
    End Sub
    '
    Private Sub btnMesAtual_Click(sender As Object, e As EventArgs) Handles btnMesAtual.Click
        myMes = Now
        dtMes.SelectionStart = myMes
        btnPeriodoPosterior.Enabled = False
    End Sub
    '
    Private Sub dtMes_DateChanged(sender As Object, e As DateRangeEventArgs)
        '
        If CDate(e.Start.ToShortDateString) <= CDate(Today.ToShortDateString) Then
            myMes = e.Start
            lblPeriodo.Text = Format(myMes, "MMMM | yyyy")
            FormataListagem()
        Else
            MessageBox.Show("Escolha um mês anterior ou igual ao mês atual...",
                "Período", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtMes.SelectionStart = Now
            myMes = Now
        End If
        '
    End Sub
    '
    Private Sub chkPeriodoTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkPeriodoTodos.CheckedChanged
        If chkPeriodoTodos.Checked = False Then
            btnPeriodoAnterior.Enabled = True
            btnPeriodoPosterior.Enabled = True
            btnMesAtual.Enabled = True
            lblPeriodo.Visible = True
        Else
            btnPeriodoAnterior.Enabled = False
            btnPeriodoPosterior.Enabled = False
            btnMesAtual.Enabled = False
            lblPeriodo.Visible = False
        End If
        '
        FormataListagem()
        '
    End Sub
    '
    Private Sub lblPerido_Click(sender As Object, e As EventArgs) Handles lblPeriodo.Click
        pnlMes.Visible = True
        dtMes.Focus()
    End Sub
    '
    Private Sub pnlMes_Leave(sender As Object, e As EventArgs) Handles pnlMes.MouseLeave, dtMes.LostFocus
        pnlMes.Visible = False
    End Sub
    '
#End Region
    '
#Region "CONTROLE DOS BTN PESQUISA"
    Private Sub AdicionaHandler()
        AddHandler rbtEmAberto.CheckedChanged, AddressOf Butons_CheckedChanged
        AddHandler rbtCanceladas.CheckedChanged, AddressOf Butons_CheckedChanged
        AddHandler rbtNegativadas.CheckedChanged, AddressOf Butons_CheckedChanged
        AddHandler rbtQuitadas.CheckedChanged, AddressOf Butons_CheckedChanged
        AddHandler rbtNegociadas.CheckedChanged, AddressOf Butons_CheckedChanged
        AddHandler rbtTodas.CheckedChanged, AddressOf Butons_CheckedChanged
    End Sub
    '
    Private Sub RemoverHandler()
        RemoveHandler rbtEmAberto.CheckedChanged, AddressOf Butons_CheckedChanged
        RemoveHandler rbtCanceladas.CheckedChanged, AddressOf Butons_CheckedChanged
        RemoveHandler rbtNegativadas.CheckedChanged, AddressOf Butons_CheckedChanged
        RemoveHandler rbtQuitadas.CheckedChanged, AddressOf Butons_CheckedChanged
        RemoveHandler rbtNegociadas.CheckedChanged, AddressOf Butons_CheckedChanged
        RemoveHandler rbtTodas.CheckedChanged, AddressOf Butons_CheckedChanged
    End Sub
    '
    Private Sub Butons_CheckedChanged(sender As Object, e As EventArgs)
        FiltraSituacao()
    End Sub
    '
    '--- FILTRAGEM DA LISTA PELA SITUACAO (EM ABERTO, QUITADA, CANCELADA, NEGATIVADA)
    Private Sub FiltraSituacao()
        '
        If Not IsNothing(propSituacaoAtual) Then
            Dim query = From P In pagLista
            query = pagLista.Where(Function(p) p.Situacao = propSituacaoAtual).ToList
            '
            dgvListagem.DataSource = query
        Else
            dgvListagem.DataSource = pagLista
        End If
        '
    End Sub
    '
#End Region
    '
#Region "MENU SUSPENSO"
    '
    Private Sub dgvListagem_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvListagem.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvListagem.HitTest(e.X, e.Y)
            dgvListagem.ClearSelection()

            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub

            ' seleciona o ROW
            dgvListagem.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            dgvListagem.CurrentCell = dgvListagem.Rows(hit.RowIndex).Cells(2)
            dgvListagem.Rows(hit.RowIndex).Selected = True

            ' mostra o MENU ativar e desativar
            Dim pagItem As clAPagar = DirectCast(dgvListagem.Rows(hit.RowIndex).DataBoundItem, clAPagar)
            '
            If pagItem.Situacao = 0 Then '--- EmAberto
                QuitarToolStripMenuItem.Enabled = True
                CancelarToolStripMenuItem.Enabled = True
                NegativarToolStripMenuItem.Enabled = True
                NormalizarToolStripMenuItem.Enabled = False

            ElseIf pagItem.Situacao = 1 Then '--- Pagas
                QuitarToolStripMenuItem.Enabled = False
                CancelarToolStripMenuItem.Enabled = False
                NegativarToolStripMenuItem.Enabled = False
                NormalizarToolStripMenuItem.Enabled = False

            ElseIf pagItem.Situacao = 2 Then '--- Canceladas
                QuitarToolStripMenuItem.Enabled = True
                CancelarToolStripMenuItem.Enabled = False
                NegativarToolStripMenuItem.Enabled = True
                NormalizarToolStripMenuItem.Enabled = True

            ElseIf pagItem.Situacao = 3 Then '--- Negativadas
                QuitarToolStripMenuItem.Enabled = False
                CancelarToolStripMenuItem.Enabled = False
                NegativarToolStripMenuItem.Enabled = True
                NormalizarToolStripMenuItem.Enabled = True

            ElseIf pagItem.Situacao = 4 Then '--- Negociadas
                QuitarToolStripMenuItem.Enabled = False
                CancelarToolStripMenuItem.Enabled = False
                NegativarToolStripMenuItem.Enabled = False
                NormalizarToolStripMenuItem.Enabled = False

            End If
            '
            ' revela menu
            mnuOperacoes.Show(c.PointToScreen(e.Location))
        End If
    End Sub
    ' 
    '--- QUITAR A PAGAR
    '-------------------------------------------------------------------------------------------------------
    Private Sub QuitarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitarToolStripMenuItem.Click
        '
        btnQuitar_Click(New Object, New EventArgs)
        '
    End Sub
    '
    '--- CANCELAR A PAGAR
    '-------------------------------------------------------------------------------------------------------
    Private Sub CancelarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelarToolStripMenuItem.Click
        '
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvListagem.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        '--- verifica USER PERMIT
        If UsuarioAcesso(1) <> 1 Then '--- Usuario Administrador
            MessageBox.Show("Solicite autorização Administrativa...")
            Exit Sub
        End If
        '
        '--- Pergunta ao USER
        If MessageBox.Show("Você deseja realmente CANCELAR esse APagar selecionado?",
                           "Cancelar A Pagar", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If
        '
        '--- Cancela a APagar selecionado
        Dim pBLL As New APagarBLL
        Dim resp As Integer
        Dim IDAPagar As Integer = DirectCast(dgvListagem.Rows(dgvListagem.CurrentCell.RowIndex).DataBoundItem, clAPagar).IDAPagar
        '
        Try
            resp = pBLL.Cancelar_APagar(IDAPagar)
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao Cancelar o APagar..." & vbNewLine &
                            ex.Message, "Execeção Inesperada", MessageBoxButtons.OK)
            Exit Sub
        End Try
        '
        '--- atualiza a listagem
        DirectCast(dgvListagem.Rows(dgvListagem.CurrentCell.RowIndex).DataBoundItem, clAPagar).Situacao = 2 '-- 2 : Situacao CANCELADA
        dgvListagem.EndEdit()
        FiltraSituacao()
        '
    End Sub
    '
    '--- NORMALIZAR A PAGAR
    '-------------------------------------------------------------------------------------------------------
    Private Sub NormalizarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalizarToolStripMenuItem.Click
        '
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvListagem.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        '--- verifica USER PERMIT
        If UsuarioAcesso(1) <> 1 Then '--- Usuario Administrador
            MessageBox.Show("Solicite autorização Administrativa...")
            Exit Sub
        End If
        '
        '--- Pergunta ao USER
        If MessageBox.Show("Você deseja realmente NORMALIZAR esse APagar selecionado?",
                           "Normalizar A Pagar", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If
        '
        '--- Normaliza a APagar selecionado
        Dim pBLL As New APagarBLL
        Dim resp As Integer
        Dim IDAPagar As Integer = DirectCast(dgvListagem.Rows(dgvListagem.CurrentCell.RowIndex).DataBoundItem, clAPagar).IDAPagar
        '
        Try
            resp = pBLL.Normalizar_APagar(IDAPagar)
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao Normalizar o A Pagar..." & vbNewLine &
                            ex.Message, "Execeção Inesperada", MessageBoxButtons.OK)
            Exit Sub
        End Try
        '
        '--- atualiza a listagem
        '-- 2 : Situacao EM ABERTO
        DirectCast(dgvListagem.Rows(dgvListagem.CurrentCell.RowIndex).DataBoundItem, clAPagar).Situacao = 0
        dgvListagem.EndEdit()
        FiltraSituacao()
        '
    End Sub
    '
    '--- NEGATIVAR APAGAR
    '-------------------------------------------------------------------------------------------------------
    Private Sub NegativarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NegativarToolStripMenuItem.Click
        MsgBox("Em Implementação")
    End Sub
    '
    '--- VER ORIGEM DO APAGAR
    '-------------------------------------------------------------------------------------------------------
    Private Sub VerOrigemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerOrigemToolStripMenuItem.Click
        '
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvListagem.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        btnEditar_Click(New Object, New EventArgs)
        '
    End Sub
    '
#End Region
    '
#Region "TRATAMENTO VISUAL"
    Private Sub pnlVenda_Paint(sender As Object, e As PaintEventArgs) Handles pnlVenda.Paint, pnlSituacao.Paint
        '
        Dim brush As Brush = New LinearGradientBrush(e.ClipRectangle, Color.LightSteelBlue, Color.FromArgb(100, Color.SlateGray), LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(brush, e.ClipRectangle)
        '
    End Sub
    '
#End Region
    '
End Class
