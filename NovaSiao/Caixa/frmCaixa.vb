Imports CamadaBLL
Imports CamadaDTO

Public Class frmCaixa
    Private lstMov As List(Of clMovimentacao)
    Private dtSaldo As New DataTable
    Private _clCaixa As clCaixa
    Private _TEntradas As Double
    Private _TSaidas As Double
    Private _TTransf As Double
    Private WithEvents bindCaixa As New BindingSource

#Region "SUB NEW | PROPERTIES"
    '
    Sub New(myCaixa As clCaixa)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        propCaixa = myCaixa
        '
        obterMovimentacoes()
        PreencheDataBindings()
        FormataDTSaldo()
        ObterSaldos()
        '
        CalculaTotais()
        '
    End Sub
    '
    Public Property propCaixa() As clCaixa
        Get
            Return _clCaixa
        End Get
        Set(ByVal value As clCaixa)
            _clCaixa = value
            bindCaixa.DataSource = value
            propIDSituacao = _clCaixa.IDSituacao
        End Set
    End Property
    '
    'PROPERTY SITUACAO
    Private Property propIDSituacao As Byte
        Get
            Return _clCaixa.IDSituacao
        End Get
        Set(value As Byte)
            '
            Select Case _clCaixa.IDSituacao
                Case 1 ' INICIADO
                    btnAlterar.Enabled = True
                    btnFinalizar.Enabled = True
                    btnInserirDespesa.Enabled = True
                    btnExcluirCaixa.Enabled = True
                    '
                    txtObservacao.ReadOnly = False
                    '
                    btnFinalizar.Text = "Finalizar Caixa"
                    btnFinalizar.Image = My.Resources.accept
                    '
                Case 2 ' FINALIZADO
                    btnAlterar.Enabled = False
                    btnFinalizar.Enabled = True
                    btnInserirDespesa.Enabled = False
                    btnExcluirCaixa.Enabled = False
                    '
                    txtObservacao.ReadOnly = True
                    '
                    btnFinalizar.Text = "Desbloqueio"
                    btnFinalizar.Image = My.Resources.unlock
                    '
                Case 3 ' BLOQUEADO
                    btnAlterar.Enabled = False
                    btnFinalizar.Enabled = False
                    btnInserirDespesa.Enabled = False
                    btnExcluirCaixa.Enabled = False
                    '
                    txtObservacao.ReadOnly = True
                    '
                    btnFinalizar.Text = "Finalizar Caixa"
                    btnFinalizar.Image = My.Resources.accept
                    '
            End Select
            '
        End Set
    End Property
    '
#End Region
    '
#Region "BINDINGS"
    '
    Private Sub PreencheDataBindings()
        ' ADICIONANDO O DATABINDINGS AOS CONTROLES TEXT
        ' OS COMBOS JA SÃO ADICIONADOS DATABINDINGS QUANDO CARREGA

        lblIDProduto.DataBindings.Add("Text", bindCaixa, "IDCaixa")
        lblDataFinal.DataBindings.Add("Text", bindCaixa, "DataFinal", True, DataSourceUpdateMode.OnPropertyChanged)
        lblDataInicial.DataBindings.Add("Text", bindCaixa, "DataInicial", True, DataSourceUpdateMode.OnPropertyChanged)
        lblApelidoFilial.DataBindings.Add("Text", bindCaixa, "ApelidoFilial", True, DataSourceUpdateMode.OnPropertyChanged)
        lblConta.DataBindings.Add("Text", bindCaixa, "Conta", True, DataSourceUpdateMode.OnPropertyChanged)
        lblSituacao.DataBindings.Add("Text", bindCaixa, "Situacao")
        lblApelidoFuncionario.DataBindings.Add("Text", bindCaixa, "ApelidoFuncionario", True, DataSourceUpdateMode.OnPropertyChanged)
        lblSaldoAnterior.DataBindings.Add("Text", bindCaixa, "SaldoAnterior", True)
        txtObservacao.DataBindings.Add("Text", bindCaixa, "Observacao", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler lblIDProduto.DataBindings("Text").Format, AddressOf idFormatRG
        AddHandler lblSaldoAnterior.DataBindings("Text").Format, AddressOf idFormatCUR
        AddHandler lblApelidoFuncionario.DataBindings("Text").Format, AddressOf FormatNULLFuncionario
        '
    End Sub
    '
    ' FORMATA OS BINDINGS
    Private Sub idFormatRG(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "0000")
    End Sub
    '
    Private Sub idFormatCUR(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = FormatCurrency(e.Value, 2)
    End Sub
    '
    Private Sub FormatNULLFuncionario(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        '
        If IsNothing(e.Value) Or String.IsNullOrEmpty(e.Value) Then
            e.Value = "Não Determinado"
        End If
        '
    End Sub
    '
#End Region
    '
#Region "DATAGRID MOVIMENTAÇÃO"
    '
    Private Sub obterMovimentacoes()
        '
        Dim movBLL As New MovimentacaoBLL
        '
        Try
            '--- GET Lista de Movimentacoes
            lstMov = movBLL.GetMovimentos_IDCaixa(propCaixa.IDCaixa)
            '
            dgvListagem.DataSource = lstMov
            '
        Catch ex As Exception
            MessageBox.Show("Um exceção ocorreu ao tentar obter os Entradas | Saídas dessa Conta ..." & vbNewLine &
                            ex.Message, "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
        PreencheListagem()
        '
    End Sub
    '
    Private Sub PreencheListagem()
        '
        '--- limpa as colunas do datagrid
        dgvListagem.Columns.Clear()
        dgvListagem.AutoGenerateColumns = False
        '
        ' altera as propriedades importantes
        dgvListagem.MultiSelect = False
        dgvListagem.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvListagem.ColumnHeadersVisible = True
        dgvListagem.AllowUserToResizeRows = False
        dgvListagem.AllowUserToResizeColumns = False
        dgvListagem.RowHeadersVisible = True
        dgvListagem.RowHeadersWidth = 30
        dgvListagem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvListagem.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvListagem.StandardTab = True
        '
        FormataColunas()
        '
    End Sub
    '
    Private Sub FormataColunas()
        '
        ' (0) COLUNA MOV
        With clnMov
            .HeaderText = ""
            .DataPropertyName = "Mov"
            .Width = 30
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (1) COLUNA DATA
        With clnMovData
            .HeaderText = "Data"
            .DataPropertyName = "MovData"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
        '
        ' (2) COLUNA DESCRICAO 
        With clnDescricao
            .HeaderText = "Descrição"
            .DataPropertyName = "Descricao"
            .Width = 300
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (3) COLUNA FORMA 
        With clnMovForma
            .HeaderText = "Forma"
            .DataPropertyName = "MovForma"
            .Width = 200
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (4) COLUNA VALOR
        With clnValor
            .HeaderText = "Valor"
            .DataPropertyName = "MovValor"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .DefaultCellStyle.Format = "C"
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        '
        Me.dgvListagem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
                                        Me.clnMov,
                                        Me.clnMovData,
                                        Me.clnDescricao,
                                        Me.clnMovForma,
                                        Me.clnValor})
        '
    End Sub
    '
    '--- CONTROLA AS CORES DA LISTAGEM
    '=====================================================================================================
    Private Sub dgvListagem_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvListagem.CellFormatting
        '
        If e.ColumnIndex = 3 Then
            '
            Dim M As String = DirectCast(dgvListagem.Rows(e.RowIndex).DataBoundItem, clMovimentacao).Mov
            '
            Select Case M
                '
                Case "S"
                    '
                    dgvListagem.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
                    dgvListagem.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = Color.Firebrick
                    '
                    e.CellStyle.ForeColor = Color.Red
                Case "E"
                    '
                    dgvListagem.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Azure
                    dgvListagem.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
                    '
                    e.CellStyle.ForeColor = Color.Black
                Case "T"
                    '
                    dgvListagem.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.LightCyan
                    dgvListagem.Rows(e.RowIndex).DefaultCellStyle.SelectionBackColor = SystemColors.Highlight
                    '
                    e.CellStyle.ForeColor = Color.Blue
                    '
            End Select
            '
        End If
        '
    End Sub
    '
#End Region
    '
#Region "DATAGRID SALDOS"
    '
    Private Sub FormataDTSaldo()
        '
        '--- Adiciona as COLUNAS da DataTable: dtSaldos
        '--------------------------------------------------------------
        Dim IDMovTipo As New DataColumn
        '
        With IDMovTipo
            .ColumnName = "IDMovTipo"
            .DataType = GetType(Short)
            .Caption = "ID"
            .ReadOnly = False
            .Unique = True
        End With
        '
        dtSaldo.Columns.Add(IDMovTipo)
        Dim Keys(0) As DataColumn
        Keys(0) = IDMovTipo
        dtSaldo.PrimaryKey = Keys
        '
        dtSaldo.Columns.Add("MovTipo", GetType(String))
        dtSaldo.Columns.Add("IDConta", GetType(Short))
        dtSaldo.Columns.Add("Conta", GetType(String))
        dtSaldo.Columns.Add("SaldoAnterior", GetType(Decimal))
        dtSaldo.Columns.Add("SaldoFinal", GetType(Decimal))
        dtSaldo.Columns.Add("Nivelamento", GetType(Boolean))
        '
    End Sub
    '
    Private Sub ObterSaldos()
        Dim cxBLL As New CaixaBLL
        '
        '--- Limpa todas as ROWS do dtSaldo
        dtSaldo.Rows.Clear()
        '
        '--- Adiciona os DADOS da DataTable: dtSaldos
        '--------------------------------------------------------------
        Try
            '--- Adiciona os DADOS do SALDOANTERIOR
            Dim dtSaldoAnterior As DataTable = cxBLL.GetSaldo_ContaTipos_IDCaixa(_clCaixa.IDCaixa)
            '
            If dtSaldoAnterior.Rows.Count > 0 Then
                '
                For Each r As DataRow In dtSaldoAnterior.Rows
                    dtSaldo.Rows.Add({r("IDMovTipo"),
                                      r("MovTipo"),
                                      r("IDConta"),
                                      r("Conta"),
                                      r("MovValor"),
                                      r("MovValor")})
                Next
                '
            End If
            '
            '--- Calcula os DADOS do SALDOATUAL
            For Each c As clMovimentacao In lstMov
                Dim saldoFind As DataRow = dtSaldo.Rows.Find(c.IDMovTipo)
                '
                '--- Calcula valor real positivo para entrada e negativo para saída
                Dim MovValorReal As Double = c.MovValorReal
                '
                '--- adiciona os valores
                If IsNothing(saldoFind) Then
                    If c.Descricao.ToString.Contains("Nivelamento") Then
                        dtSaldo.Rows.Add({c.IDMovTipo,
                                         c.MovTipo,
                                         c.IDConta,
                                         c.Conta,
                                         0,
                                         MovValorReal,
                                         True})
                    Else
                        dtSaldo.Rows.Add({c.IDMovTipo,
                                         c.MovTipo,
                                         c.IDConta,
                                         c.Conta,
                                         0,
                                         MovValorReal,
                                         False})
                    End If
                Else
                    saldoFind.BeginEdit()
                    '
                    If c.Descricao.ToString.Contains("Nivelamento") Then
                        saldoFind("Nivelamento") = True
                    End If
                    '
                    saldoFind("SaldoFinal") += MovValorReal
                    '
                    saldoFind.AcceptChanges()
                End If
                '
            Next
            '
            dgvSaldos.DataSource = dtSaldo
            '
            PreencheDgvSaldos()
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreui ao obter Saldos Anteriores..." & vbNewLine &
            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        '
    End Sub
    '
    Private Sub PreencheDgvSaldos()
        '
        '--- limpa as colunas do datagrid
        dgvSaldos.Columns.Clear()
        dgvSaldos.AutoGenerateColumns = False
        '
        ' altera as propriedades importantes
        dgvSaldos.MultiSelect = False
        dgvSaldos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSaldos.ColumnHeadersVisible = True
        dgvSaldos.AllowUserToResizeRows = False
        dgvSaldos.AllowUserToResizeColumns = False
        dgvSaldos.RowHeadersVisible = True
        dgvSaldos.RowHeadersWidth = 30
        dgvSaldos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvSaldos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvSaldos.StandardTab = True
        '
        FormataDgvSaldos()
        '
    End Sub
    '
    Private Sub FormataDgvSaldos()
        '
        ' (1) COLUNA MOVFORMA
        With clnTipo
            .HeaderText = "Tipo"
            .DataPropertyName = "MovTipo"
            .Width = 140
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.Font = New Font("Arial Narrow", 10)
        End With
        '
        ' (2) COLUNA SALDO ANTERIOR
        With clnSaldoAnterior
            .HeaderText = "Sd Anterior"
            .DataPropertyName = "SaldoAnterior"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .DefaultCellStyle.Format = "C"
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft

        End With
        '
        ' (3) COLUNA SALDO FINAL 
        With clnSaldoFinal
            .HeaderText = "Sd Final"
            .DataPropertyName = "SaldoFinal"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .DefaultCellStyle.Format = "C"
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        '
        Me.dgvSaldos.Columns.AddRange(New DataGridViewColumn() {
                                      Me.clnTipo,
                                      Me.clnSaldoAnterior,
                                      Me.clnSaldoFinal})
        '
    End Sub
    '
    Private Sub dgvSaldos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvSaldos.CellFormatting
        '
        If e.ColumnIndex = 3 Then
            If e.Value >= 0 Then
                e.CellStyle.ForeColor = Color.Blue
                e.CellStyle.SelectionForeColor = Color.Blue
            Else
                e.CellStyle.ForeColor = Color.Red
                e.CellStyle.SelectionForeColor = Color.Red
            End If
        End If
        '
    End Sub
#End Region
    '
#Region "BUTTONS FUNCTION"
    '
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click, btnFechar.Click
        Close()
        MostraMenuPrincipal()
    End Sub
    '
    Private Sub btnInserirDespesa_Click(sender As Object, e As EventArgs) Handles btnInserirDespesa.Click
        '
        '--- abre o form de DespesaSimples
        Dim f As New frmDespesaSimples(_clCaixa.DataInicial,
                                       _clCaixa.DataFinal,
                                       _clCaixa.IDConta,
                                       _clCaixa.IDFilial,
                                       _clCaixa.ApelidoFilial,
                                       _clCaixa.IDCaixa,
                                       Me)
        '
        f.ShowDialog()
        '
        '--- se a resultado for cancel então exit
        If f.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        Dim newMov As clMovimentacao = f.propMovSaida
        '
        '--- retorna os valores e insere na listagem
        lstMov.Add(newMov)
        dgvListagem.DataSource = Nothing
        dgvListagem.DataSource = lstMov
        ObterSaldos()
        CalculaTotais()
        '
        '--- seleciona a row com o NOVO nivelamento
        Dim i As Integer = lstMov.FindIndex(Function(x) x.Descricao = newMov.Descricao)
        dgvListagem.CurrentCell = dgvListagem.Rows(i).Cells(0)
        '
    End Sub
    '
    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click
        Dim f As New frmCaixaInserir(_clCaixa, Me)
        '
        f.ShowDialog()
        If f.DialogResult <> DialogResult.OK Then Exit Sub
        '
        _clCaixa.DataFinal = f.propMaxDate
        lblDataFinal.DataBindings("Text").ReadValue()
        '
        Dim cxBLL As New CaixaBLL
        '
        Try
            cxBLL.Caixa_AlteraPeriodo(_clCaixa.IDCaixa, _clCaixa.DataFinal)
            MessageBox.Show("Período do caixa alterado com sucesso!", "Alteração do Período",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            '
            '--- Atualiza a listagem de Movimentacao
            obterMovimentacoes()
            dgvListagem.DataSource = lstMov
            CalculaTotais()
            ObterSaldos()
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção inesperada ocorreu ao alterar o Período do caixa:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        '
        If propIDSituacao = 1 Then
            FinalizarCaixa()
        ElseIf propIDSituacao = 2 Then
            DesbloquearCaixa()
        End If
        '
    End Sub
    '
    '--- FUNCAO DE FINALIZAR O CAIXA
    Private Sub FinalizarCaixa()
        '
        If MessageBox.Show("Deseja realmente finalizar esse Caixa?",
                           "Finalizar Caixa",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
        '
        Dim cxBLL As New CaixaBLL
        '
        Try
            cxBLL.CaixaFinalizar(_clCaixa)
            '
            MessageBox.Show("Caixa Finalizado com sucesso...", "Finalizar Caixa",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
            MostraMenuPrincipal()
            '
        Catch ex As Exception
            MessageBox.Show("Um exceção ocorreu ao Finalizar esse Caixa..." & vbNewLine &
                            ex.Message, "Exceção Inseperada", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- FUNCAO DE DESBLOQUEAR O CAIXA
    Private Sub DesbloquearCaixa()
        '
        If MessageBox.Show("Deseja realmente DESBLOQUEAR esse Caixa?",
                           "Desbloquear Caixa",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then Exit Sub
        '
        '--- verifica se existe outro caixa na mesma conta depois desse
        '-----------------------------------------------------------------------------------------------
        If VerificaUltimoCaixa() = False Then
            MessageBox.Show("Esse caixa NÃO poderá ser DESBLOQUEADO já que existe um ou mais outros caixas " &
                            "na mesma conta que foram efetuados após esse caixa...", "Desbloqueio",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim db As New CaixaBLL
            Try
                db.DesbloquearCaixa(propCaixa.IDCaixa)
                '
                propCaixa.IDSituacao = 1
                propIDSituacao = 1
            Catch ex As Exception
                MessageBox.Show("Uma exceção ocorreu ao tentar DESBLOQUEAR o Caixa ..." & vbNewLine &
                                ex.Message, "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        '
    End Sub
    '
    Private Sub btnExcluirCaixa_Click(sender As Object, e As EventArgs) Handles btnExcluirCaixa.Click
        '
        If MessageBox.Show("Deseja realmente EXCLUIR esse Caixa?" & vbNewLine & vbNewLine &
                           "Essa operação não tem como VOLTAR atrás...",
                           "Excluir Caixa",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
        '
        '--- verifica se existe outro caixa na mesma conta depois desse
        '-----------------------------------------------------------------------------------------------
        If VerificaUltimoCaixa() = False Then
            MessageBox.Show("Esse caixa NÃO poderá ser EXCLUÍDO já que existe um ou mais caixas " &
                            "após esse caixa, dessa mesma conta...", "Desbloqueio",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Dim db As New CaixaBLL
            Try
                db.ExcluirCaixa(propCaixa.IDCaixa)
                Close()
                MostraMenuPrincipal()
            Catch ex As Exception
                MessageBox.Show("Uma exceção ocorreu ao tentar EXCLUIR o Caixa ..." & vbNewLine &
                                ex.Message, "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        '
    End Sub
    '
    '--- SALVA A OBSERVACAO DO CAIXA
    Private Sub btnSalvarObservacao_Click(sender As Object, e As EventArgs) Handles btnSalvarObservacao.Click
        '
        Try
            '
            If SalvarCaixa() Then
                MessageBox.Show("Observação salva com sucesso!", "Caixa",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtObservacao.Focus()
                btnSalvarObservacao.Visible = False
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao atualizar a observação do Caixa..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- SALVA FUNCIONARIO OPERADOR DE CAIXA
    Private Sub btnFuncionarioAlterar_Click(sender As Object, e As EventArgs) Handles btnFuncionarioAlterar.Click
        '
        '--- Ampulheta ON
        Cursor = Cursors.WaitCursor
        Dim fFunc As New frmFuncionarioProcurar(False, Me)
        Cursor = Cursors.Default
        '
        fFunc.ShowDialog()
        If fFunc.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        Try
            '
            Dim newID As Integer = fFunc.IDEscolhido
            '
            '--- se for o mesmo Funcionario
            If newID = If(_clCaixa.IDFuncionario, 0) Then Return
            '
            '--- define o novo operador de caixa
            _clCaixa.IDFuncionario = newID
            lblApelidoFuncionario.Text = fFunc.NomeEscolhido
            '
            If SalvarCaixa() Then
                MessageBox.Show("Operador de Caixa salvo com sucesso!", "Caixa",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao atualizar o Operador de Caixa..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    Private Function SalvarCaixa() As Boolean
        '
        Dim db As New CaixaBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            If db.UpdateCaixa(_clCaixa) Then
                Return True
            Else
                Return False
            End If
            '
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Function
    '
#End Region
    '
#Region "FUNCOES IMPORTANTES"
    '
    Private Sub CalculaTotais()
        '
        Dim E As Double = 0
        Dim S As Double = 0
        Dim Transf As Double = 0
        '
        For Each cl As clMovimentacao In lstMov
            '
            If cl.Mov = "E" Then
                E = E + cl.MovValor
            ElseIf cl.Mov = "S" Then
                S = S + cl.MovValor
            Else
                Transf = Transf + cl.MovValorReal
            End If
            '
            _TEntradas = E
            lblTEntradas.Text = Format(E, "C")
            _TSaidas = S
            lblTSaidas.Text = Format(S, "C")
            _TTransf = Transf
            lblTTransf.Text = Format(Transf, "C")
            '
            '--- Calcula SALDOS
            _clCaixa.SaldoFinal = _TEntradas + _TTransf - _TSaidas
            lblSaldoFinal.Text = Format(_clCaixa.SaldoFinal, "C")
            '
            If _clCaixa.SaldoFinal >= 0 Then
                lblSaldoFinal.ForeColor = Color.Blue
            Else
                lblSaldoFinal.ForeColor = Color.Red
            End If
            '
        Next
        '
    End Sub
    '
    '--- TROCAR A COR DO CAMPO QUANDO FOCUS
    Private Sub txtObservacao_GotFocus(sender As Object, e As EventArgs) Handles txtObservacao.GotFocus
        txtObservacao.BackColor = Color.White
    End Sub
    '
    Private Sub txtObservacao_LostFocus(sender As Object, e As EventArgs) Handles txtObservacao.LostFocus
        txtObservacao.BackColor = SystemColors.Control
    End Sub
    '
    '--- MOSTRA BTN SALVAR OBSERVACAO
    Private Sub txtObservacao_TextChanged(sender As Object, e As EventArgs) Handles txtObservacao.TextChanged
        '
        If Not CanFocus Then Exit Sub
        '
        If txtObservacao.Text.Length > 0 Then
            btnSalvarObservacao.Visible = True
        Else
            btnSalvarObservacao.Visible = False
        End If
        '
    End Sub
    '
    '--- EXECUTAR A FUNCAO DO BOTAO QUANDO PRESSIONA A TECLA (+) NO CONTROLE
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) Handles txtObservacao.KeyDown
        '
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True
            e.Handled = True
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
    '--- VERIFICA SE EXISTE OUTRO CAIXA INICIADO APOS O CAIXA INFORMADO
    Private Function VerificaUltimoCaixa() As Boolean
        '
        '--- verifica se o caixa atual é o ultimo caixa dessa conta
        '--- retorna TRUE se realmente for o ultimo caixa retorna TRUE
        '-----------------------------------------------------------------------------------------------
        Dim db As New CaixaBLL
        '
        Try
            '--- GET datas inicial e Final
            Dim dt As DataTable = db.GetLastDados_IDConta(propCaixa.IDConta)
            Dim r As DataRow = dt.Rows(0)
            '
            Dim LastIDCaixa As Integer = IIf(IsDBNull(r("LastIDCaixa")), Nothing, r("LastIDCaixa"))
            '
            If Not IsNothing(LastIDCaixa) AndAlso LastIDCaixa <> propCaixa.IDCaixa Then
                Return False
            Else
                Return True
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao tentar obter o ultimo Caixa da Conta ..." & vbNewLine &
                            ex.Message, "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
    End Function
    '
#End Region
    '
#Region "MENU SUSPENSO SALDOS"
    '
    ' CONTROLE DO MENU SUSPENSO
    Private Sub dgvSaldos_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvSaldos.MouseDown
        '
        If e.Button = MouseButtons.Right Then
            '
            Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvSaldos.HitTest(e.X, e.Y)
            dgvSaldos.ClearSelection()
            '
            '---VERIFICAÇÕES NECESSÁRIAS
            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
            '
            ' seleciona o ROW
            dgvSaldos.Rows(hit.RowIndex).Cells(0).Selected = True
            dgvSaldos.Rows(hit.RowIndex).Selected = True
            '
            ' mostra o menu nivelamento
            Dim r As DataRowView = dgvSaldos.Rows(hit.RowIndex).DataBoundItem
            '
            If propIDSituacao = 1 Then ' AndAlso r("IDOperadora") = 1
                '
                If r("Nivelamento") = True Then
                    miExcluirNivelamento.Enabled = True
                    miInserirNivelamento.Enabled = True
                Else
                    miExcluirNivelamento.Enabled = False
                    miInserirNivelamento.Enabled = True
                End If
                '
            Else
                miExcluirNivelamento.Enabled = False
                miInserirNivelamento.Enabled = False
            End If
            '
            ' revela menu
            MenuSaldos.Show(c.PointToScreen(e.Location))
            '
        End If
        '
    End Sub
    '
    Private Sub miInserirNivelamento_Click(sender As Object, e As EventArgs) Handles miInserirNivelamento.Click
        '
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvSaldos.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        '--- abre o frmNivelamento
        Dim r As DataRowView = dgvSaldos.CurrentRow.DataBoundItem
        Dim frmN As New frmNivelamento(r("SaldoFinal"), r("Conta"), r("MovTipo"))
        '
        frmN.ShowDialog()
        '
        '--- verifica o resultado do form
        If frmN.DialogResult <> DialogResult.OK Then
            Exit Sub
        End If
        '
        '--- recupera os valores
        Dim myNivValor As Decimal = frmN.NivValor_Escolhido
        '
        '--- Verifica se já houve um nivelamento efetuado com o mesmo IDMovForma
        If r("Nivelamento") = True Then
            '
            '--- percorre pela lista de movimentacoes do caixa
            For Each c As clMovimentacao In lstMov
                If c.Descricao.ToString.Contains("Nivelamento") Then
                    If c.IDMovTipo = r("IDMovTipo") Then
                        MessageBox.Show("Já existe um Nivelamento efetuado nesse mesmo Tipo de Movimentação." & vbNewLine &
                                        "Se deseja realizar Novo Nivelamento, exclua todos os outros anteriores...",
                                        "Nivelamento Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '
                        '--- selçeciona a row com o nivelamento duplicado
                        Dim i As Integer = lstMov.IndexOf(c)

                        dgvListagem.CurrentCell = dgvListagem.Rows(i).Cells(0)
                        '
                        Exit Sub
                    End If
                End If
            Next
            '
        End If
        '
        '--- salva o nivelamento
        Try
            Dim cxBLL As New CaixaBLL
            '
            Dim newMov As clMovimentacao = cxBLL.InserirNivelamento(_clCaixa.IDCaixa, r("IDMovTipo"), myNivValor)
            '
            '--- retorna os valores e insere na listagem
            lstMov.Add(newMov)
            ' ????  dgvListagem.DataSource = Nothing
            ' ????  dgvListagem.DataSource = lstMov
            ObterSaldos()
            CalculaTotais()
            '
            '--- seleciona a row com o NOVO nivelamento
            Dim i As Integer = lstMov.FindIndex(Function(x) x.Descricao = newMov.Descricao)
            dgvListagem.CurrentCell = dgvListagem.Rows(i).Cells(0)
            '
            r("Nivelamento") = True
            '
            '--- seleciona o nivelamento no datagrid
            dgvSaldos.CurrentCell = dgvSaldos.Rows(dgvSaldos.Rows.Count - 1).Cells(0)

        Catch ex As Exception
            MessageBox.Show("Uma execeção ocorreu ao Inserir Nivelamento..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    Private Sub miExcluirNivelamento_Click(sender As Object, e As EventArgs) Handles miExcluirNivelamento.Click
        '
        '--- verifica se existe alguma cell 
        If IsDBNull(dgvSaldos.CurrentRow.Cells(0).Value) Then Exit Sub
        '
        '--- pergunta ao usuário
        '
        Dim r As DataRowView = dgvSaldos.CurrentRow.DataBoundItem
        '
        If MessageBox.Show("Você deseja excluir todos os nivelamentos desse caixa da operadora: " & vbNewLine &
                           r("Operadora").ToString.ToUpper, "Excluir Nivelamentos",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) = vbNo Then Exit Sub
        '
        '--- salva o nivelamento
        Try
            Dim cxBLL As New CaixaBLL
            '
            Dim resp As Boolean = cxBLL.ExcluirNivelamentos(_clCaixa.IDCaixa)
            '
            '--- retorna os valores e insere na listagem
            If resp = False Then Exit Sub
            '
            obterMovimentacoes()
            dgvListagem.DataSource = lstMov
            CalculaTotais()
            ObterSaldos()
            '
        Catch ex As Exception
            MessageBox.Show("Uma execeção ocorreu ao Excluir Nivelamentos..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
#End Region
    '
End Class
