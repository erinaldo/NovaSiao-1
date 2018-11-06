Imports CamadaBLL
Imports CamadaDTO
'
Public Class frmTrocaSimples
    '
    Private Shared _Troca As clTroca
    Private ItensGroupEntrada As ItensGroup
    Private ItensGroupSaida As ItensGroup
    Private _ParcelaList As New List(Of clAReceberParcela)
    '
    Private bindTroca As New BindingSource
    Private bindParc As New BindingSource
    '
    Private _Sit As FlagEstado '= 1:Registro Salvo; 2:Registro Alterado; 3:Novo registro
    Private Shared _Filial As Integer

    Private VerificaAlteracao As Boolean
    Private Taxa As Double?
    '
#Region "LOAD"
    '
    Private Property Sit As FlagEstado
        '
        Get
            Return _Sit
        End Get
        '
        Set(value As FlagEstado)
            _Sit = value
            Select Case _Sit
                Case FlagEstado.RegistroSalvo '--- REGISTRO FINALIZADO | NÃO BLOQUEADO
                    lblSituacao.Text = "Finalizada"
                    btnFinalizar.Text = "&Fechar"
                    '
                    btnVendedorAlterar.Enabled = True
                    btnDataAnterior.Enabled = True
                    btnDataPosterior.Enabled = True
                    txtObservacao.ReadOnly = False
                    '
                    txtValorAcrescimos.ReadOnly = False
                    '
                Case FlagEstado.Alterado '--- REGISTRO FINALIZADO ALTERADO
                    lblSituacao.Text = "Alterada"
                    btnFinalizar.Text = "&Finalizar"
                    '
                    btnVendedorAlterar.Enabled = True
                    btnDataAnterior.Enabled = True
                    btnDataPosterior.Enabled = True
                    txtObservacao.ReadOnly = False
                    '
                    txtValorAcrescimos.ReadOnly = False
                    '
                Case FlagEstado.NovoRegistro '--- REGISTRO NÃO FINALIZADO
                    lblSituacao.Text = "Em Aberto"
                    btnFinalizar.Text = "&Finalizar"
                    '
                    btnVendedorAlterar.Enabled = True
                    btnDataAnterior.Enabled = True
                    btnDataPosterior.Enabled = True
                    txtObservacao.ReadOnly = False
                    '
                    txtValorAcrescimos.ReadOnly = False
                    '
                Case FlagEstado.RegistroBloqueado '--- REGISTRO BLOQUEADO PARA ALTERACOES
                    lblSituacao.Text = "Bloqueada"
                    btnFinalizar.Text = "&Fechar"
                    '
                    btnVendedorAlterar.Enabled = False
                    btnDataAnterior.Enabled = False
                    btnDataPosterior.Enabled = False
                    txtObservacao.ReadOnly = True
                    '
                    txtValorAcrescimos.ReadOnly = True
                    '
            End Select
            '
        End Set
        '
    End Property
    '
    Property propTroca As clTroca
        '
        Get
            Return _Troca
        End Get
        '
        Set(value As clTroca)
            VerificaAlteracao = False '--- Inibe a verificacao dos campos IDPlano
            '
            '--- define a Troca Atual
            _Troca = value
            _Filial = _Troca.IDFilial
            '
            '--- obtem os produtos da listagem
            getItensList()
            getParcelasList()
            '
            '--- preenche os BINDS
            'ItensGroupEntrada.BindItem.DataSource = ItensGroupEntrada.ItensList
            'ItensGroupSaida.BindItem.DataSource = ItensGroupSaida.ItensList
            bindParc.DataSource = _ParcelaList
            '
            If IsNothing(bindTroca.DataSource) Then
                bindTroca.DataSource = _Troca
                PreencheDataBinding()
            Else
                bindTroca.Clear()
                bindTroca.DataSource = _Troca
                bindTroca.ResetBindings(True)
            End If
            '
            '--- Preenche e formata os Datagrid de Itens da Troca
            ItensGroupEntrada.PreencheItens()
            ItensGroupSaida.PreencheItens()
            '
            '--- Preenche Itens do A Receber (parcelas)
            Preenche_AReceber()
            cmbIDPlano.SelectedValue = IIf(IsNothing(_Troca.IDPlano), -1, _Troca.IDPlano)
            cmbIDCobrancaForma.SelectedValue = IIf(IsNothing(_Troca.IDCobrancaForma), -1, _Troca.IDCobrancaForma)
            '
            '--- Atualiza o estado da Situacao: FLAGESTADO
            Select Case _Troca.IDSituacao
                Case 1 ' VENDA INICIADA
                    Sit = FlagEstado.NovoRegistro
                Case 2 ' VENDA FINALIZADA
                    Sit = FlagEstado.RegistroSalvo
                Case 3 ' VENDA BLOQUEADA
                    Sit = FlagEstado.RegistroBloqueado
                Case Else
            End Select
            '
            '--- Permite a verificacao dos campos IDPlano
            VerificaAlteracao = True
            '
        End Set
        '
    End Property
    '
    Public Sub New(myTroca As clTroca)
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        ItensGroupEntrada = New ItensGroup(dgvEntradas, TransacaoItemBLL.EnumMovimento.ENTRADA, Me)
        ItensGroupSaida = New ItensGroup(dgvSaidas, TransacaoItemBLL.EnumMovimento.SAIDA, Me)
        propTroca = myTroca
        '
        '--- Define a TAXA PERMANENCIA padrao
        Try
            Taxa = Convert.ToDouble(ObterDefault("TaxaPermanencia"))
        Catch ex As Exception
            Taxa = 0
        End Try
        '
    End Sub
    '
#End Region '// LOAD
    '
#Region "DATABINDING"
    '
    Private Sub PreencheDataBinding()
        '
        lblCliente.DataBindings.Add("Text", bindTroca, "PessoaTroca", True, DataSourceUpdateMode.OnPropertyChanged)
        lblIDTroca.DataBindings.Add("Text", bindTroca, "IDTroca", True, DataSourceUpdateMode.OnPropertyChanged)
        lblFilial.DataBindings.Add("Text", bindTroca, "ApelidoFilial", True, DataSourceUpdateMode.OnPropertyChanged)
        lblTrocaData.DataBindings.Add("Text", bindTroca, "TrocaData", True, DataSourceUpdateMode.OnPropertyChanged)
        lblVendedor.DataBindings.Add("Text", bindTroca, "ApelidoVenda", True, DataSourceUpdateMode.OnPropertyChanged)
        txtObservacao.DataBindings.Add("Text", bindTroca, "Observacao", True, DataSourceUpdateMode.OnPropertyChanged)
        lblTotalTroca.DataBindings.Add("Text", bindTroca, "TotalTroca", True, DataSourceUpdateMode.OnPropertyChanged)
        txtValorAcrescimos.DataBindings.Add("Text", bindTroca, "ValorAcrescimos", True, DataSourceUpdateMode.OnPropertyChanged)
        lblValorEntrada.DataBindings.Add("Text", bindTroca, "ValorEntrada", True, DataSourceUpdateMode.OnPropertyChanged)
        lblValorSaida.DataBindings.Add("Text", bindTroca, "ValorSaida", True, DataSourceUpdateMode.OnPropertyChanged)
        lblTotalGeral.DataBindings.Add("Text", bindTroca, "TotalTroca")
        '
        'dgvItens.DataBindings.Add("Tag", bindItem, "IDProduto", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler lblIDTroca.DataBindings("Text").Format, AddressOf FormatRG
        AddHandler lblTotalTroca.DataBindings("text").Format, AddressOf FormatCUR
        AddHandler txtValorAcrescimos.DataBindings("text").Format, AddressOf FormatCUR
        AddHandler lblValorEntrada.DataBindings("text").Format, AddressOf FormatCUR
        AddHandler lblValorSaida.DataBindings("text").Format, AddressOf FormatCUR
        AddHandler lblTotalGeral.DataBindings("text").Format, AddressOf FormatCUR
        AddHandler lblTrocaData.DataBindings("text").Format, AddressOf FormatDT
        '
        ' CARREGA OS COMBOBOX
        CarregaCmbCobrancaForma()
        CarregaCmbPlano()
        '
        ' ADD HANDLER PARA DATABINGS
        AddHandler _Troca.AoAlterar, AddressOf HandlerAoAlterar
        '
    End Sub
    '
    Private Sub HandlerAoAlterar()
        If _Troca.RegistroAlterado = True And Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
    End Sub
    '
    ' FORMATA OS BINDINGS
    Private Sub FormatRG(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "0000")
    End Sub
    Private Sub FormatCUR(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = FormatCurrency(e.Value, 2)
    End Sub
    Private Sub FormatDT(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "dd/MM/yyyy")
    End Sub
    Private Sub Format00(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        If Not IsNothing(e.Value) Then
            e.Value = Format(CInt(e.Value), "00")
        End If
    End Sub
    '
#End Region '// DATABINDING
    '
#Region "CARREGA OS COMBOBOX"
    '
    ' CARREGA OS COMBOBOX
    '--------------------------------------------------------------------------------------------------------
    Private Sub CarregaCmbCobrancaForma()
        Dim sql As New SQLControl
        sql.ExecQuery("SELECT * FROM tblCobrancaForma WHERE Ativo = 'TRUE' AND Entradas = 'TRUE'")
        '
        If sql.HasException(True) Then
            Exit Sub
        End If
        '
        With cmbIDCobrancaForma
            .DataSource = sql.DBDT
            .ValueMember = "IDCobrancaForma"
            .DisplayMember = "CobrancaForma"
            .DataBindings.Add("SelectedValue", bindTroca, "IDCobrancaForma", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
        '
    End Sub
    '
    Private Sub CarregaCmbPlano()
        Dim sql As New SQLControl
        sql.ExecQuery("SELECT * FROM tblVendaPlanos WHERE Ativo = 'TRUE'")
        '
        If sql.HasException(True) Then
            Exit Sub
        End If
        '
        With cmbIDPlano
            .DataSource = sql.DBDT
            .ValueMember = "IDPlano"
            .DisplayMember = "Plano"
            .DataBindings.Add("SelectedValue", bindTroca, "IDPlano", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
        '
    End Sub
    '
    '--------------------------------------------------------------------------------------------------------
#End Region '// CARREGA OS COMBOBOX
    '
#Region "GET ITENS"
    '
    '--- RETORNA TODOS OS ITENS DA VENDA
    Private Sub getItensList()
        Dim tBLL As New TransacaoItemBLL
        Try
            '
            '--- get itens da transacao entradas e saidas relacionadas a troca
            ItensGroupSaida.ItensList = tBLL.GetVendaItens_IDVenda_List(_Troca.IDTransacaoSaida, _Troca.IDFilial)
            ItensGroupEntrada.ItensList = tBLL.GetVendaItens_IDVenda_List(_Troca.IDTransacaoEntrada, _Troca.IDFilial)
            '
            '--- Atualiza o label TOTAL
            AtualizaTotalGeral()
            '
        Catch ex As Exception
            MessageBox.Show("Um execeção ocorreu ao obter Produtos de Entrada e Saída da Troca:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
#End Region '// GET ITENS
    '
#Region "BOTOES DE ACAO"
    '
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Sit = FlagEstado.NovoRegistro Or Sit = FlagEstado.Alterado Then
            If MessageBox.Show("ALTERAÇÕES DA TROCA NÃO SERÃO SALVAS!" & vbNewLine & vbNewLine &
                               "Se você fechar agora essa Troca," & vbNewLine &
                               "todas alterações serão perdidas," & vbNewLine &
                               "inclusive as alterações no Parcelamento..." & vbNewLine & vbNewLine &
                               "Deseja Fechar assim mesmo?", "Troca não Finalizada",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
                tabPrincipal.SelectedTab = vtab2
                '
                Exit Sub
            End If
            '
        End If
        '
        Close()
        MostraMenuPrincipal()
        '
    End Sub
    '
    Private Sub btnVendedorAlterar_Click(sender As Object, e As EventArgs) Handles btnVendedorAlterar.Click
        '
        Dim fFunc As New frmFuncionarioProcurar(True, Me)
        fFunc.ShowDialog()
        If fFunc.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        _Troca.IDVendedor = fFunc.IDEscolhido
        _Troca.ApelidoVenda = fFunc.NomeEscolhido
        lblVendedor.DataBindings("Text").ReadValue()
        '
    End Sub
    '
    Private Sub btnData_Click(sender As Object, e As EventArgs) Handles btnDataPosterior.Click, btnDataAnterior.Click
        '
        If DirectCast(sender, Button).Name = "btnDataPosterior" Then
            _Troca.TrocaData = _Troca.TrocaData.AddDays(1)
        ElseIf DirectCast(sender, Button).Name = "btnDataAnterior" Then
            _Troca.TrocaData = _Troca.TrocaData.AddDays(-1)
        End If
        '
        lblTrocaData.DataBindings("Text").ReadValue()
        '
    End Sub
    '
#End Region
    '
#Region "FORMATACAO E FLUXO"
    '
    ' CRIA TECLA DE ATALHO PARA O CONTROLE DE TABULACAO
    '---------------------------------------------------------------------------------------------------
    Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.D1 Then
            tabPrincipal.SelectedTab = vtab1
            tabPrincipal_SelectedIndexChanged(New Object, New System.EventArgs)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D2 Then
            tabPrincipal.SelectedTab = vtab2
            tabPrincipal_SelectedIndexChanged(New Object, New System.EventArgs)
        End If
    End Sub
    '
    Private Sub tabPrincipal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabPrincipal.SelectedIndexChanged
        If tabPrincipal.SelectedIndex = 0 Then
            dgvEntradas.Focus()
        ElseIf tabPrincipal.SelectedIndex = 1 Then
            txtValorAcrescimos.Focus()
        End If
    End Sub
    '
    ' CONVERTE ENTER EM TAB DE ALGUNS CONTROLES
    '---------------------------------------------------------------------------------------------------
    Private Sub Text_KeyDown(sender As Object, e As KeyEventArgs) Handles _
            txtValorAcrescimos.KeyDown,
            txtObservacao.KeyDown,
            cmbIDCobrancaForma.KeyDown,
            cmbIDPlano.KeyDown
        '
        '--- Se for o campo observacao, verifica se esta preenchido com algum texto
        '--- Se esta preenchido entao permite que o ENTER funcione como nova linha
        If DirectCast(sender, TextBox).Name = "txtObservacao" AndAlso txtObservacao.Text.Trim.Length > 0 Then
            Exit Sub
        End If
        '
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
#End Region
    '
#Region "CONTROLE DO A RECEBER"
    '============================================================================================================
    ' A RECEBER CONTROLES
    '============================================================================================================
    '
    '--- RETORNA TODOS AS PARCELAS DE A RECEBER
    Private Sub getParcelasList()
        '
        Dim rBLL As New ParcelaBLL
        Try
            _ParcelaList = rBLL.Parcela_GET_PorIDOrigem(1, _Troca.IDTroca)
            '--- Atualiza o label TOTAL
            AtualizaTotalAReceber()
        Catch ex As Exception
            MessageBox.Show("Um execeção ocorreu ao obter o a Receber da Troca:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- Preenche o DataGrid AReceber
    Private Sub Preenche_AReceber()
        With dgvAReceber
            '
            '--- limpa as colunas do datagrid
            .Columns.Clear()
            .AutoGenerateColumns = False
            '
            ' altera as propriedades importantes
            .MultiSelect = False
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .RowHeadersVisible = True
            .RowHeadersWidth = 30
            .RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .StandardTab = True
            '
            '--- configura o DataSource
            .DataSource = bindParc
            If .Rows.Count > 0 Then .CurrentCell = .Rows(.Rows.Count).Cells(1)
        End With
        '
        '--- formata as colunas do datagrid
        FormataGrid_AReceber()
        '
    End Sub
    '
    Private Sub FormataGrid_AReceber()
        '
        Dim cellStyleCur As New DataGridViewCellStyle
        cellStyleCur.Format = "c"
        cellStyleCur.NullValue = Nothing
        cellStyleCur.Alignment = DataGridViewContentAlignment.MiddleRight
        '
        ' (1) COLUNA IDAReceber
        dgvAReceber.Columns.Add("clnID", "ID")
        With dgvAReceber.Columns("clnID")
            .DataPropertyName = "IDAReceberParcela"
            .Width = 0
            .Resizable = DataGridViewTriState.False
            .Visible = False
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        '
        ' (2) COLUNA REG
        dgvAReceber.Columns.Add("clnReg", "Reg.")
        With dgvAReceber.Columns("clnReg")
            .DataPropertyName = "CodRegistro"
            .Width = 70
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            '.DefaultCellStyle.Font = New Font("Arial Narrow", 12)
        End With
        '
        ' (3) COLUNA VENCIMENTO
        dgvAReceber.Columns.Add("clnVencimento", "Vencimento")
        With dgvAReceber.Columns("clnVencimento")
            .HeaderText = "Vencimento"
            .DataPropertyName = "Vencimento"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        '
        ' (4) COLUNA PERMANENCIA
        dgvAReceber.Columns.Add("clnPermanencia", "Perm.(%)")
        With dgvAReceber.Columns("clnPermanencia")
            .DataPropertyName = "PermanenciaTaxa"
            .DefaultCellStyle.Format = "0.00"
            .Width = 90
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        '
        ' (5) COLUNA VALOR
        dgvAReceber.Columns.Add("clnValor", "Valor")
        With dgvAReceber.Columns("clnValor")
            .DefaultCellStyle = cellStyleCur
            .DataPropertyName = "ParcelaValor"
            .Width = 120
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        '
    End Sub
    '
    Private Sub dgvAReceber_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvAReceber.KeyDown
        '
        If e.KeyCode = Keys.Add Then
            e.Handled = True
            '
            If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            '
            Parcela_Adicionar()
        ElseIf e.KeyCode = Keys.Enter Then
            e.Handled = True
            '
            If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            '
            Parcela_Editar()
        ElseIf e.KeyCode = Keys.Delete Then
            e.Handled = True
            '
            If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            '
            Parcela_Excluir()
        End If
    End Sub
    '
    Private Sub Parcela_Adicionar()
        '
        '--- Atualiza o Valor do Total Geral
        Dim vl As Double = AtualizaTotalGeral()
        '
        '--- Verifica se o valor dos itens é maior que zero
        If vl = 0 Then
            MessageBox.Show("Não é necessário adicionar Parcelas de A Receber" & vbNewLine &
                            "Quando o valor da Troca é igual a Zero..." & vbNewLine &
                            "Adicione produtos primeiro e depois tente novamente.", "Nova Parcela",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        '--- posiciona o form
        Dim pos As Point = dgvAReceber.PointToScreen(Point.Empty)
        pos = New Point(pos.X - 10, pos.Y) ' aparecer no lado esquerdo
        '
        '--- cria novo AReceber
        Dim clPar As New clAReceberParcela
        Dim ParcelaCount As Integer = _ParcelaList.Count
        Dim myLetra As Char = Chr(ParcelaCount + 65)
        '
        clPar.PermanenciaTaxa = Taxa
        clPar.IDAReceber = _Troca.IDAReceber
        clPar.IDPessoa = _Troca.IDPessoaTroca
        clPar.ParcelaValor = vl - _ParcelaList.Sum(Function(x) x.ParcelaValor)
        clPar.Vencimento = _Troca.TrocaData
        clPar.Letra = myLetra.ToString
        '
        '--- abre o form frmAReceber
        Dim fRec As New frmAReceberItem(Me, vl, _Troca.TrocaData, clPar, FlagAcao.INSERIR, pos)
        fRec.ShowDialog()
        '
        If fRec.DialogResult = DialogResult.OK Then
            ' SE A RESPOSTA DO DIALOG FOR OK
            '----------------------------------------------------------------------------------------------
            '--- Insere o ITEM na lista
            _ParcelaList.Add(fRec.propAReceber)
            bindParc.ResetBindings(False)
            '--- Atualiza o DataGrid
            dgvAReceber.DataSource = bindParc
            bindParc.MoveLast()
            '
            '--- AtualizaTotalAReceber
            AtualizaTotalAReceber()
            Sit = FlagEstado.Alterado
            '
        End If
        '
    End Sub
    '
    Private Sub Parcela_Editar()
        '
        If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
        '
        '--- posiciona o form
        Dim pos As Point = dgvAReceber.PointToScreen(Point.Empty)
        pos = New Point(pos.X - 10, pos.Y) ' aparecer no lado esquerdo
        '
        '--- GET AReceber do DataGrid
        If dgvAReceber.SelectedRows.Count = 0 Then Exit Sub
        '
        Dim ParcAtual As clAReceberParcela = dgvAReceber.SelectedRows(0).DataBoundItem
        Dim fRec As New frmAReceberItem(Me, AtualizaTotalGeral(), _Troca.TrocaData, ParcAtual, FlagAcao.EDITAR, pos)
        fRec.ShowDialog()
        '
        '--- AtualizaTotalAReceber
        AtualizaTotalAReceber()
        Sit = FlagEstado.Alterado
        '
    End Sub
    '
    Private Sub Parcela_Excluir()
        '--- verifica se existe alguma parcela selecionada
        If dgvAReceber.SelectedRows.Count = 0 Then Exit Sub
        '
        '--- seleciona a parcela
        Dim ParcAtual As clAReceberParcela
        ParcAtual = dgvAReceber.SelectedRows(0).DataBoundItem
        '
        '--- pergunta ao usuário se deseja excluir o item
        If MessageBox.Show("Deseja realmente REMOVER essa parcela A Receber?" & vbNewLine & vbNewLine &
                           ParcAtual.CodRegistro, "Excluir Parcela",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If
        '
        '--- envia o comando para excluir a parcela
        '
        Dim i As Integer = _ParcelaList.FindIndex(Function(x) x.Letra = ParcAtual.Letra)
        '
        '--- Atualiza o ITEM da lista
        _ParcelaList.RemoveAt(i)
        bindParc.ResetBindings(False)
        '--- Atualiza o DataGrid
        dgvAReceber.DataSource = bindParc
        '--- AtualizaTotalAReceber
        AtualizaTotalAReceber()
        Sit = FlagEstado.Alterado
    End Sub
    '
    Private Sub cmbIDPlano_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbIDPlano.SelectedValueChanged
        '
        '--- Se não houver SelectedVaue então exit
        If Not IsNumeric(cmbIDPlano.SelectedValue) OrElse VerificaAlteracao = False Then Exit Sub
        '
        '--- Se o registro da venda esta bloqueado não permite alteracao
        If Sit = FlagEstado.RegistroBloqueado Then
            VerificaAlteracao = False
            cmbIDPlano.SelectedValue = IIf(IsNothing(_Troca.IDPlano), -1, _Troca.IDPlano)
            VerificaAlteracao = True
            RegistroBloqueado() '--- mensagem padrao
            Exit Sub
        End If
        '
        '--- Se Valor Total da Venda for igual a Zero então Avisa e Exit
        Dim vlTotal As Double = AtualizaTotalGeral()
        If vlTotal = 0 Then
            MessageBox.Show("Não é necessário adicionar Parcelas de A Receber" & vbNewLine &
                            "Quando o valor da Troca é igual a Zero..." & vbNewLine &
                            "Adicione produtos primeiro e depois tente novamente.", "Nova Parcela",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            VerificaAlteracao = False
            cmbIDPlano.SelectedValue = IIf(IsNothing(_Troca.IDPlano), -1, _Troca.IDPlano)
            VerificaAlteracao = True
            Exit Sub
        End If
        '
        '--- Pergunta ao usuario
        If Not IsNothing(_Troca.IDPlano) Then 'AndAlso cmbIDPlano.SelectedValue <> _Venda.IDPlano Then
            Dim a As DialogResult
            a = MessageBox.Show("Você deseja realmente alterar a forma de parcelamento dessa troca?",
                            "Alterar Parcelamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If a = DialogResult.No Then
                VerificaAlteracao = False
                cmbIDPlano.SelectedValue = IIf(IsNothing(_Troca.IDPlano), -1, _Troca.IDPlano)
                VerificaAlteracao = True
                Exit Sub
            End If
        End If
        '
        '--- Verifica se o parcelamento escolhido é o PERSONALIZADO = 0
        If cmbIDPlano.SelectedValue = 0 Then
            dgvAReceber.Focus()
            Exit Sub
        End If
        '
        '--- Obtem o DataTable do cmbPlano
        Dim dtPlano As DataTable = cmbIDPlano.DataSource
        '--- Procura pelo ROW no DataTable
        Dim r As DataRow = dtPlano.Select("IDPlano = " & cmbIDPlano.SelectedValue.ToString)(0)
        '
        '--- Determina os criterios do parcelamento
        Dim Meses As Byte = r("Meses")
        Dim Entrada As Boolean = r("Entrada")
        Dim ComJuros As Double = r("ComJuros")
        '
        '--- Limpa a lista de parcelas
        _ParcelaList.Clear()
        '
        '--- Define alguns Valores
        Dim Parcelas As Byte = Meses + IIf(Entrada = True, 1, 0)
        Dim vlParcela As Decimal = Math.Round(vlTotal / Parcelas, 2)
        '
        '--- Insere na listagem e no Grid
        For i = 0 To Parcelas + IIf(Entrada = True, -1, 0)
            If Entrada = False And i = 0 Then i += 1
            Dim _parc As New clAReceberParcela
            _parc.ParcelaValor = vlParcela
            _parc.Vencimento = _Troca.TrocaData.AddMonths(i)
            _parc.IDAReceber = _Troca.IDAReceber
            _parc.IDPessoa = _Troca.IDPessoaTroca
            _parc.SituacaoParcela = 0
            _parc.PermanenciaTaxa = Taxa
            If Entrada = True Then
                _parc.Letra = Chr(i + 65)
            Else
                _parc.Letra = Chr(i + 64)
            End If
            '
            _ParcelaList.Add(_parc)
        Next
        '
        '--- Atualiza a listagem
        bindParc.ResetBindings(False)
        '--- Atualiza o DataGrid
        dgvAReceber.DataSource = bindParc
        bindParc.MoveLast()
        '
        '--- Atualiza o Total do AReceber
        AtualizaTotalAReceber()
        Sit = FlagEstado.Alterado
    End Sub
    '
    ' ALTERA A COR DO DATAGRIDVIEW ARECEBER QUANDO GANHA OU PERDE O FOCO
    '-----------------------------------------------------------------------------------------------------
    Private Sub dgvAReceber_GotFocus(sender As Object, e As EventArgs) Handles _
        dgvAReceber.GotFocus,
        dgvEntradas.GotFocus,
        dgvSaidas.GotFocus
        '
        Dim c As Color = Color.FromArgb(209, 215, 220)
        sender.BackgroundColor = c
        DirectCast(sender, DataGridView).BorderStyle = BorderStyle.Fixed3D
        '
    End Sub
    '
    Private Sub dgvAReceber_LostFocus(sender As Object, e As EventArgs) Handles _
        dgvAReceber.LostFocus,
        dgvEntradas.LostFocus,
        dgvSaidas.LostFocus
        '
        Dim c As Color = Color.FromArgb(224, 232, 243)
        sender.BackgroundColor = c
        DirectCast(sender, DataGridView).BorderStyle = BorderStyle.None
        '
    End Sub
    '
    ' CONTROLA O MENU NO DATAGRID ARECEBER
    '-----------------------------------------------------------------------------------------------------
    Private Sub dgvAReceber_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvAReceber.MouseDown
        If e.Button = MouseButtons.Right Then
            'Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvAReceber.HitTest(e.X, e.Y)
            dgvAReceber.ClearSelection()
            '
            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
            '
            ' seleciona o ROW
            dgvAReceber.CurrentCell = dgvAReceber.Rows(hit.RowIndex).Cells(1)
            dgvAReceber.Rows(hit.RowIndex).Selected = True
            dgvAReceber.Focus()
            '
            mnuItens.Show(dgvAReceber, e.Location)
            '
        End If
    End Sub
    '
    Private Sub dgvAReceber_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAReceber.CellDoubleClick
        Parcela_Editar()
    End Sub
    '
    '============================================================================================================
#End Region
    '
#Region "FINALIZAR TROCA"
    '
    Private Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        '
        '--- Verifica se a SITUACAO do registro permite salvar
        If Sit = FlagEstado.Alterado OrElse Sit = FlagEstado.NovoRegistro Then
            '
            '--- Faz as VERIFICACOES DOS CAMPOS E VALORES
            If Verificar() = False Then Exit Sub
            '
            '--- PERGUNTA AO USUÁRIO
            If MessageBox.Show("Deseja realmente Finalizar essa Transação de TROCA?", "Finalizar TROCA",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
            '
            '--- SALVA O ARECEBER PARCELAS NO BD
            If Salvar_Parcelas() = False Then Exit Sub
            '
            '--- SALVA A TRANSACAO/VENDA NO BD
            Dim tBLL As New TrocaBLL
            Try
                '--- altera a situacao da transacao atual
                _Troca.IDSituacao = 2 'CONCLUÍDA
                '
                Dim obj As Object = tBLL.AtualizaTroca_Procedure_ID(_Troca)
                '
                If Not IsNumeric(obj) Then
                    Throw New Exception(obj.ToString)
                End If
                '
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            '
            '--- RECEBE/QUITA A PARCELA QUE ESTA VENCIDA (ENTRADA OU A VISTA)
            MessageBox.Show("PRECISA RECEBER O QUE ESTA VENCIDO?")
            '
            '--- ALTERA A SITUACAO DO REGISTRO ATUAL
            Sit = FlagEstado.RegistroSalvo
            '
        End If
        '
        '--- PERGUNTA O QUE O USUARIO DESEJA FAZER
        Dim fAcao As New frmAcaoDialog(Me, "Venda")
        '
        fAcao.ShowDialog()
        '
        If fAcao.DialogResult = DialogResult.Cancel Then
            Close()
            MostraMenuPrincipal()
        End If
        '
    End Sub
    '
    Private Function Verificar() As Boolean
        '--- Verifica se a Data não está BLOQUEADA pelo sistema
        MessageBox.Show("PRECISA VERIFICA SE A DATA ESTA BLOQUEADA PELO SISTEMA?")
        '
        '----------------------------------------------------------------------------------------------
        ' VERIFICA OS VALORES DA TROCA, DAS PARCELAS E DO FRETE
        '----------------------------------------------------------------------------------------------
        '
        '--- Verifica o valor da TROCA
        If AtualizaTotalGeral() = 0 Then
            MessageBox.Show("Não é possível finalizar uma troca cujo valor final é igual a Zero..." & vbNewLine &
                            "Favor incluir alguns produtos nessa troca.",
                            "Troca Nula", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            tabPrincipal.SelectedTab = vtab1
            dgvEntradas.Focus()
            Return False
        End If
        '
        '--- Verifica se o valor da troca é igual à soma das parcelas
        If Math.Abs(AtualizaTotalGeral() - AtualizaTotalAReceber()) > 1 Then
            MessageBox.Show("A soma das parcelas é diferente da soma dos produtos" & vbNewLine &
                            "Favor corrigir esse erro alterando o parcelamento.",
                            "Parcelamento com Diferença", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            tabPrincipal.SelectedTab = vtab2
            dgvAReceber.Focus()
            Return False
        End If
        '
        '
        '----------------------------------------------------------------------------------------------
        ' VERIFICA OS CAMPOS NECESSÁRIOS DA TROCA
        '----------------------------------------------------------------------------------------------
        '
        '--- Verifica se há FORMA DE COBRANCA
        If IsNothing(_Troca.IDCobrancaForma) Then
            MessageBox.Show("O campo FORMA DE COBRANÇA não pode ficar vazio...", "Campo Necessário",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            tabPrincipal.SelectedTab = vtab2
            cmbIDCobrancaForma.Focus()
            Return False
        End If
        '
        '--- Verifica se há PLANO DE PAGAMENTO
        If IsNothing(_Troca.IDPlano) Then
            MessageBox.Show("O campo PLANO DE PAGAMENTO não pode ficar vazio...", "Campo Necessário",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            tabPrincipal.SelectedTab = vtab2
            cmbIDPlano.Focus()
            Return False
        End If
        '
        Return True
        '
    End Function
    '
    Private Function Salvar_Parcelas() As Boolean
        If _ParcelaList.Count = 0 Then Return False
        '
        Dim parcBLL As New ParcelaBLL
        '
        '--- Exclui todos os registros de Parcela da Venda atual
        Try
            parcBLL.Excluir_Parcelas_AReceber(_Troca.IDAReceber)
            '
            '--- Insere cada um AReceber no BD
            For Each parc As clAReceberParcela In _ParcelaList
                parcBLL.InserirNova_Parcela(parc)
            Next
            '
            Return True
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao SALVAR as Parcelas..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
    End Function
    '
#End Region
    '
#Region "FUNCOES NECESSARIAS"
    '
    'ATUALIZA O TOTAL DO GERAL
    '-----------------------------------------------------------------------------------------------------
    Private Function AtualizaTotalGeral() As Double
        '
        Dim T As Double = 0
        T = AtualizaTotalProdutosSaida() - AtualizaTotalProdutosEntrada() - _Troca.ValorAcrescimos
        '_Troca.TotalTroca = T * -1
        '
        Return T
        '
    End Function
    '
    ' ATUALIZA O TOTAL DOS PRODUTOS ENTRADOS
    '-----------------------------------------------------------------------------------------------------
    Private Function AtualizaTotalProdutosEntrada() As Double

        If ItensGroupEntrada.ItensList.Count > 0 Then
            Dim T As Double = 0
            '
            For Each i As clTransacaoItem In ItensGroupEntrada.ItensList
                T = T + i.Total
            Next
            '
            _Troca.ValorEntrada = T
            Return T
        Else
            _Troca.ValorEntrada = 0
            Return 0
        End If
        '
    End Function
    '
    ' ATUALIZA O TOTAL DOS PRODUTOS SAIDOS
    '-----------------------------------------------------------------------------------------------------
    Private Function AtualizaTotalProdutosSaida() As Double

        If ItensGroupSaida.ItensList.Count > 0 Then
            Dim T As Double = 0
            '
            For Each i As clTransacaoItem In ItensGroupSaida.ItensList
                T = T + i.Total
            Next
            '
            _Troca.ValorSaida = T
            Return T
        Else
            _Troca.ValorSaida = 0
            Return 0
        End If
        '
    End Function
    '
    ' ATUALIZA O TOTAL DO ARECEBER
    '-----------------------------------------------------------------------------------------------------
    Private Function AtualizaTotalAReceber() As Double
        If _ParcelaList.Count > 0 Then
            Dim T As Double = 0
            '
            For Each p As clAReceberParcela In _ParcelaList
                T = T + p.ParcelaValor
            Next
            '
            lblTotalCobrado.Text = Format(T, "c")
            Return T
        Else
            lblTotalCobrado.Text = Format(0, "c")
            Return 0
        End If
    End Function
    '
    ' CRIA UMA NOVA TROCA
    '-----------------------------------------------------------------------------------------------------
    Public Sub NovaTroca()
        'Dim v As New AcaoGlobal
        'Dim newVenda As Object = v.VendaPrazo_Nova
        ''
        'If IsNothing(newVenda) Then Exit Sub
        ''
        '_Venda = newVenda
        ''
    End Sub
    '
    ' PROCURA VENDA
    '-----------------------------------------------------------------------------------------------------
    Public Sub ProcuraVenda()
        Me.Close()
        Dim frmP As New frmOperacaoSaidaProcurar
        OcultaMenuPrincipal()
        Dim fPrincipal As Form = Application.OpenForms.OfType(Of frmPrincipal)().First
        frmP.MdiParent = fPrincipal
        frmP.Show()
    End Sub
    '
    ' RECALCULA VALORES QUANDO ALTERA CONTROLES VALOR
    '-----------------------------------------------------------------------------------------------------
    Private Sub txtValorAcrescimos_Validated(sender As Object, e As EventArgs) Handles txtValorAcrescimos.Validated
        '
        AtualizaTotalGeral()
        '
    End Sub
    '
#End Region
    '
#Region "BLOQUEIO DE REGISTROS"
    '
    ' PROIBE EDICAO NOS COMBOBOX QUANDO VENDA BLOQUEADA
    '-----------------------------------------------------------------------------------------------------
    Private Sub ComboBox_SelectedValueChanged(sender As Object, e As EventArgs) _
        Handles cmbIDCobrancaForma.SelectedValueChanged
        '
        If Sit = FlagEstado.RegistroBloqueado AndAlso VerificaAlteracao = True Then
            Dim cmb As ComboBox = DirectCast(sender, ComboBox)
            '
            Select Case cmb.Name
                Case "cmbIDCobrancaForma"
                    VerificaAlteracao = False
                    cmbIDCobrancaForma.SelectedValue = IIf(IsNothing(_Troca.IDCobrancaForma), -1, _Troca.IDCobrancaForma)
                    VerificaAlteracao = True
            End Select
            '
            '--- emite mensagem padrao
            RegistroBloqueado()
            '
        End If
        '
    End Sub
    '
    ' FUNCAO QUE CONFERE REGISTRO BLOQUEADO E EMITE MENSAGEM PADRAO
    '-----------------------------------------------------------------------------------------------------
    Private Function RegistroBloqueado() As Boolean
        '
        If Sit = FlagEstado.RegistroBloqueado Then
            MessageBox.Show("Esse registro de Venda está BLOQUEADO para alterações..." & vbNewLine &
                            "Não é possível adicionar produtos ou alterar algum dado!",
                            "Registro Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            RegistroBloqueado = True
        Else
            RegistroBloqueado = False
        End If
        '
    End Function
    '
    ' FUNCAO QUE CONFERE REGISTRO FINALIZADO E PERGUNTA SE DESEJA ALTERAR
    '-----------------------------------------------------------------------------------------------------
    Private Function RegistroFinalizado() As Boolean
        '
        If Sit = FlagEstado.RegistroSalvo Then
            If MessageBox.Show("Esse registro de Troca já se encontra FINALIZADO..." & vbNewLine &
                               "Deseja realmente fazer alterações nesse registro?",
                               "Registro Finalizado", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                '--- Edita o registro e altera a situação
                _Troca.IDSituacao = 1
                '
                '--- SALVA A TRANSACAO/TROCA NO BD
                Dim tBLL As New TrocaBLL
                Try
                    Dim obj As Object = tBLL.AtualizaTroca_Procedure_ID(_Troca)
                    '
                    If Not IsNumeric(obj) Then
                        Throw New Exception(obj.ToString)
                    End If
                    '
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
                '
                RegistroFinalizado = False
            Else
                RegistroFinalizado = True
            End If
        Else
            RegistroFinalizado = False
        End If
        '
    End Function
    '
#End Region
    '
    '=================================================
    ' CLASSE ITENSGROUP
    '=================================================
    '
    Private Class ItensGroup
        '
        Private WithEvents _ItensList As List(Of clTransacaoItem)
        Private WithEvents _BindItem As BindingSource
        Private WithEvents _Dgv As DataGridView
        '
        Private WithEvents mnuItensAcao As New ContextMenuStrip
        Private WithEvents mnuItemEditar As New ToolStripMenuItem
        Private WithEvents mnuItemInserir As New ToolStripMenuItem
        Private WithEvents mnuItemExcluir As New ToolStripMenuItem
        Private ToolStripSeparator1 As New ToolStripSeparator
        '
        Private frmOrigem As frmTrocaSimples
        Property Movimento As TransacaoItemBLL.EnumMovimento
        '
        Sub New(_datagrid As DataGridView, _movimento As TransacaoItemBLL.EnumMovimento, _frmOrigem As frmTrocaSimples)
            '
            BindItem = New BindingSource
            Dgv = _datagrid
            ItensList = New List(Of clTransacaoItem)
            frmOrigem = _frmOrigem
            Movimento = _movimento
            InicializarMenuItem()
            '
        End Sub
        '
        Public Property Dgv() As DataGridView
            Get
                Return _Dgv
            End Get
            Set(ByVal value As DataGridView)
                _Dgv = value
            End Set
        End Property
        '
        Public Property BindItem() As BindingSource
            Get
                Return _BindItem
            End Get
            Set(ByVal value As BindingSource)
                _BindItem = value
            End Set
        End Property
        '
        Public Property ItensList() As List(Of clTransacaoItem)
            Get
                Return _ItensList
            End Get
            Set(ByVal value As List(Of clTransacaoItem))
                _ItensList = value
                BindItem.DataSource = _ItensList
                Dgv.DataSource = BindItem
            End Set
        End Property
        '
        Private Sub InicializarMenuItem()
            '
            ' verifica se já esta inicialzado
            If mnuItensAcao.Items.Count > 1 Then Exit Sub ' ja esta inicializado
            '
            'mnuItensAcao
            '
            mnuItensAcao.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuItemEditar, Me.mnuItemInserir, Me.ToolStripSeparator1, Me.mnuItemExcluir})
            mnuItensAcao.Name = "mnuItensAcao"
            mnuItensAcao.Size = New System.Drawing.Size(181, 98)
            '
            'mnuItemEditar
            '
            mnuItemEditar.Image = My.Resources.editar
            mnuItemEditar.Name = "mnuItemEditar"
            mnuItemEditar.Size = New System.Drawing.Size(180, 22)
            mnuItemEditar.Text = "Editar Item"
            '
            'mnuItemInserir
            '
            mnuItemInserir.Image = My.Resources.add
            mnuItemInserir.Name = "mnuItemInserir"
            mnuItemInserir.Size = New System.Drawing.Size(180, 22)
            mnuItemInserir.Text = "Inserir Produto"
            '
            'ToolStripSeparator1
            '
            ToolStripSeparator1.Name = "ToolStripSeparator1"
            ToolStripSeparator1.Size = New System.Drawing.Size(177, 6)
            '
            'mnuItemExcluir
            '
            mnuItemExcluir.Image = My.Resources.delete
            mnuItemExcluir.Name = "mnuItemExcluir"
            mnuItemExcluir.Size = New System.Drawing.Size(180, 22)
            mnuItemExcluir.Text = "Excluir Produto"
        End Sub
        '
#Region "PREENCHE FORMATA DATAGRID"
        '
        '--- FORMATA E PREENCHE OS ITENS
        Friend Sub PreencheItens()
            '
            '--- limpa as colunas do datagrid
            Dgv.Columns.Clear()
            Dgv.AutoGenerateColumns = False
            '
            ' altera as propriedades importantes
            Dgv.MultiSelect = False
            Dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Dgv.ColumnHeadersVisible = True
            Dgv.AllowUserToResizeRows = False
            Dgv.AllowUserToResizeColumns = False
            Dgv.RowHeadersVisible = True
            Dgv.RowHeadersWidth = 30
            Dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            Dgv.StandardTab = True
            '
            '--- configura o DataSource da listagem
            'dgv.DataSource = _ItensListEntrada
            '
            '--- define o currentcell da listagem
            If Dgv.Rows.Count > 0 Then Dgv.CurrentCell = Dgv.Rows(Dgv.Rows.Count).Cells(1)
            '
            '--- formata as colunas do datagrid
            FormataColunas_Itens()
            '
        End Sub
        '
        '--- FORMATA COLUNAS
        Private Sub FormataColunas_Itens()
            '
            ' (1) COLUNA IDItem
            Dgv.Columns.Add("clnIDTansacaoItem", "IDItem")
            With Dgv.Columns("clnIDTansacaoItem")
                .DataPropertyName = "IDTransacaoItem"
                .Width = 0
                .Resizable = DataGridViewTriState.False
                .Visible = False
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
            '
            ' (2) COLUNA RGProduto
            Dgv.Columns.Add("clnRGProduto", "Reg.")
            With Dgv.Columns("clnRGProduto")
                .DataPropertyName = "RGProduto"
                .Width = 70
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .DefaultCellStyle.Format = "0000"
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.DefaultCellStyle.Font = New Font("Arial Narrow", 12)
            End With
            '
            ' (3) COLUNA PRODUTO
            Dgv.Columns.Add("clnProduto", "Descrição")
            With Dgv.Columns("clnProduto")
                .DataPropertyName = "Produto"
                .Width = 430
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            '
            ' (4) COLUNA QUANTIDADE
            Dgv.Columns.Add("clnQuantidade", "Qtde.")
            With Dgv.Columns("clnQuantidade")
                .DataPropertyName = "Quantidade"
                .Width = 70
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Format = "00"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            '
            ' (5) COLUNA PRECO
            Dgv.Columns.Add("clnPreco", "Preço")
            With Dgv.Columns("clnPreco")
                .DataPropertyName = "Preco"
                .Width = 100
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "C"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            '
            ' (6) COLUNA SUB TOTAL
            Dgv.Columns.Add("clnSubTotal", "SubTotal")
            With Dgv.Columns("clnSubTotal")
                .DataPropertyName = "SubTotal"
                .Width = 100
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "C"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            '
            ' (7) COLUNA DESCONTO
            Dgv.Columns.Add("clnDesconto", "Desc.")
            With Dgv.Columns("clnDesconto")
                .DataPropertyName = "Desconto"
                .Width = 80
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.00"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            '
            ' (8) COLUNA TOTAL
            Dgv.Columns.Add("clnTotal", "Total")
            With Dgv.Columns("clnTotal")
                .DataPropertyName = "Total"
                .Width = 100
                .Resizable = DataGridViewTriState.False
                .Visible = True
                .ReadOnly = True
                .SortMode = DataGridViewColumnSortMode.NotSortable
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "C"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            '
        End Sub
        '
#End Region ' // PREENCHE FORMATA DATAGRID
        '
#Region "MOVIMENTA ITENS CRUD PRODUTOS"
        '
        '--- INSERE UM NOVO ITEM NA LISTA DE PRODUTOS
        '---------------------------------------------------------------------------------------------
        Private Sub Inserir_Item()
            '
            '--- Verifica se esta Bloqueado ou Finalizado
            If frmOrigem.RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            If frmOrigem.RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
            '
            '--- Abre o frmItem
            Dim newItem As New clTransacaoItem
            '
            Dim fItem As New frmVendaItem(frmOrigem, Movimento, _Filial, newItem)
            fItem.ShowDialog()
            '
            '--- Verifica o retorno do Dialog
            If Not fItem.DialogResult = DialogResult.OK Then Exit Sub
            '
            '--- Insere o novo Item
            Dim ItemBLL As New TransacaoItemBLL
            Dim myID As Long? = Nothing
            '
            '----------------------------------------------------------------------------------------------
            '--- Insere o novo ITEM no BD
            Try
                If Movimento = TransacaoItemBLL.EnumMovimento.ENTRADA Then
                    newItem.IDTransacao = _Troca.IDTransacaoEntrada
                ElseIf Movimento = TransacaoItemBLL.EnumMovimento.SAIDA Then
                    newItem.IDTransacao = _Troca.IDTransacaoSaida
                End If
                '
                myID = ItemBLL.InserirNovoItem(newItem, Movimento, _Troca.TrocaData)
                newItem.IDTransacaoItem = myID
                '
            Catch ex As Exception
                MessageBox.Show("Houve um exceção ao INSERIR o item no BD..." & vbNewLine & ex.Message, "Exceção Inesperada",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            '
            '--- Insere o ITEM na lista
            _ItensList.Add(newItem)
            BindItem.ResetBindings(False)
            '
            '--- Atualiza o DataGrid
            Dgv.DataSource = BindItem
            BindItem.MoveLast()
            '
            '--- Atualiza o label TOTAL
            frmOrigem.AtualizaTotalGeral()
            '
        End Sub
        '    
        '--- EDITA UM ITEM NA LISTA DE PRODUTOS
        '---------------------------------------------------------------------------------------------
        Private Sub Editar_Item()
            '
            '--- Verifica se esta Bloqueado ou Finalizado
            If frmOrigem.RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            If frmOrigem.RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
            '
            '--- Verifica se há um item selecionado
            If Dgv.SelectedRows.Count = 0 Then Exit Sub
            '
            '--- obtem o itemAtual
            Dim itmAtual As clTransacaoItem
            itmAtual = Dgv.SelectedRows(0).DataBoundItem
            '
            '--- Abre o frmItem
            Dim fitem As New frmVendaItem(frmOrigem, Movimento, _Filial, itmAtual)
            fitem.ShowDialog()
            '
            '--- Verifica o retorno do Dialog
            If Not fitem.DialogResult = DialogResult.OK Then Exit Sub
            '
            '--- Edita o novo Item
            Dim ItemBLL As New TransacaoItemBLL
            Dim myID As Long? = Nothing
            '
            'Dim i As Integer = _ItensList.FindIndex(Function(x) x.IDTransacaoItem = itmAtual.IDTransacaoItem)
            '
            '--- Altera o ITEM no BD e reforma o ESTOQUE
            Try
                itmAtual.IDTransacao = _Troca.IDTroca
                myID = ItemBLL.EditarItem(itmAtual, Movimento, _Troca.TrocaData)
                itmAtual.IDTransacaoItem = myID
            Catch ex As Exception
                MessageBox.Show("Houve um exceção ao ALTERAR o item no BD..." & vbNewLine & ex.Message,
                            "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            '
            '--- Atualiza o ITEM da lista
            '
            '_ItensList.Item(i) = Item
            BindItem.ResetBindings(False)
            '
            '--- Atualiza o DataGrid
            Dgv.DataSource = BindItem
            'bindItem.CurrencyManager.Position = i
            '
            '--- Atualiza o label TOTAL
            frmOrigem.AtualizaTotalGeral()
            '
        End Sub
        '    
        '--- EXCLUI UM ITEM NA LISTA DE PRODUTOS
        '---------------------------------------------------------------------------------------------
        Private Sub Excluir_Item()
            '
            '--- Verifica se esta Bloqueado ou Finalizado
            If frmOrigem.RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            If frmOrigem.RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
            '
            '--- Verifica se há um item selecionado
            If Dgv.SelectedRows.Count = 0 Then Exit Sub
            '
            '--- obtem o itemAtual
            Dim itmAtual As clTransacaoItem
            itmAtual = Dgv.SelectedRows(0).DataBoundItem
            '
            '--- pergunta ao usuário se deseja excluir o item
            If MessageBox.Show("Deseja realmente REMOVER esse item da Troca?" & vbNewLine & vbNewLine &
                               itmAtual.Produto.ToUpper, "Excluir Item",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
            '
            '--- Exclui o Item do BD
            Dim ItemBLL As New TransacaoItemBLL
            Dim myID As Long? = Nothing
            '
            Dim i As Integer = _ItensList.FindIndex(Function(x) x.IDTransacaoItem = itmAtual.IDTransacaoItem)
            '
            '--- Altera o ITEM no BD e reforma o ESTOQUE
            Try
                myID = ItemBLL.ExcluirItem(itmAtual, Movimento)
            Catch ex As Exception
                MessageBox.Show("Houve um exceção ao EXCLUIR o item no BD..." & vbNewLine & ex.Message,
                                "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
            '
            '--- Atualiza o ITEM da lista
            _ItensList.RemoveAt(i)
            BindItem.ResetBindings(False)
            '
            '--- Atualiza o DataGrid
            Dgv.DataSource = BindItem
            '
            '--- Atualiza o label TOTAL
            frmOrigem.AtualizaTotalGeral()
            '
        End Sub
        '
        '---------------------------------------------------------------------------------------------------
        ' CRIA TECLA DE ATALHO PARA INSERIR/EDITAR PRODUTO
        '---------------------------------------------------------------------------------------------------
        Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles _Dgv.KeyDown
            '
            If e.KeyCode = Keys.Add Then
                e.Handled = True
                Inserir_Item()
            ElseIf e.KeyCode = Keys.Enter Then
                e.Handled = True
                Editar_Item()
            ElseIf e.KeyCode = Keys.Delete Then
                e.Handled = True
                Excluir_Item()
            End If
            '
        End Sub
        '
        Private Sub dgv_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles _Dgv.CellDoubleClick
            Editar_Item()
        End Sub
        '
        Private Sub dgv_MouseDown(sender As Object, e As MouseEventArgs) Handles _Dgv.MouseDown
            '
            If e.Button = MouseButtons.Right Then
                'Dim c As Control = DirectCast(sender, Control)
                Dim hit As DataGridView.HitTestInfo = Dgv.HitTest(e.X, e.Y)
                Dgv.ClearSelection()
                '
                If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
                '
                ' seleciona o ROW
                Dgv.CurrentCell = Dgv.Rows(hit.RowIndex).Cells(1)
                Dgv.Rows(hit.RowIndex).Selected = True
                '
                'mnuItens.Show(dgvItens, c.PointToScreen(e.Location))
                mnuItensAcao.Show(Dgv, e.Location)
                '
            End If
            '
        End Sub
        '
        Private Sub MenuItemEditar_Click(sender As Object, e As EventArgs) Handles mnuItemEditar.Click
            Editar_Item()
        End Sub
        '
        Private Sub MenuItemInserir_Click(sender As Object, e As EventArgs) Handles mnuItemInserir.Click
            Inserir_Item()
        End Sub
        '
        Private Sub MenuItemExcluir_Click(sender As Object, e As EventArgs) Handles mnuItemExcluir.Click
            Excluir_Item()
        End Sub
        '
#End Region
        '
    End Class
    '
    '=================================================
    '
End Class
