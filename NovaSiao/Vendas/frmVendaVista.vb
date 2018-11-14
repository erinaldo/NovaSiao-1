Imports CamadaBLL
Imports CamadaDTO
'
Public Class frmVendaVista
    '
    Private vBLL As New VendaBLL
    Private _Venda As clVenda
    Private _Troca As clTroca
    Private _ItensList As New List(Of clTransacaoItem)
    Private _EntradaList As New List(Of clEntradas)
    Private bindVenda As New BindingSource
    Private bindItem As New BindingSource
    Private bindEnt As New BindingSource
    Private _Sit As FlagEstado '= 1:Registro Salvo; 2:Registro Alterado; 3:Novo registro
    Private _Filial As Integer
    Private VerificaAlteracao As Boolean
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
            '
            _Sit = value
            Select Case _Sit
                Case FlagEstado.RegistroSalvo '--- REGISTRO FINALIZADO | NÃO BLOQUEADO
                    lblSituacao.Text = "Finalizada"
                    btnFinalizar.Text = "&Fechar"
                    btnTroca.Enabled = True
                    btnLimparPagamentos.Enabled = False
                    '
                    btnVendedorAlterar.Enabled = True
                    btnData.Enabled = True
                    txtObservacao.ReadOnly = False
                                        '
                Case FlagEstado.Alterado '--- REGISTRO FINALIZADO ALTERADO
                    lblSituacao.Text = "Alterada"
                    btnFinalizar.Text = "&Finalizar"
                    btnTroca.Enabled = True
                    btnLimparPagamentos.Enabled = True
                    '
                    btnVendedorAlterar.Enabled = True
                    btnData.Enabled = True
                    txtObservacao.ReadOnly = False
                    '
                Case FlagEstado.NovoRegistro '--- REGISTRO NÃO FINALIZADO
                    lblSituacao.Text = "Em Aberto"
                    btnFinalizar.Text = "&Finalizar"
                    btnTroca.Enabled = True
                    btnLimparPagamentos.Enabled = True
                    '
                    btnVendedorAlterar.Enabled = True
                    btnData.Enabled = True
                    txtObservacao.ReadOnly = False
                    '
                Case FlagEstado.RegistroBloqueado '--- REGISTRO BLOQUEADO PARA ALTERACOES
                    lblSituacao.Text = "Bloqueada"
                    btnFinalizar.Text = "&Fechar"
                    btnTroca.Enabled = False
                    btnLimparPagamentos.Enabled = False
                    '
                    btnVendedorAlterar.Enabled = False
                    btnData.Enabled = False
                    txtObservacao.ReadOnly = True
                    '
            End Select
            '
        End Set
        '
    End Property
    '
    Property propVenda As clVenda
        '
        Get
            Return _Venda
        End Get
        '
        Set(value As clVenda)
            '
            VerificaAlteracao = False '--- Inibe a verificacao dos campos IDPlano
            _Venda = value
            _Filial = _Venda.IDPessoaOrigem
            obterItens()
            obterPagamentos()
            bindItem.DataSource = _ItensList
            bindEnt.DataSource = _EntradaList
            dgvItens.DataSource = bindItem
            '
            '--- Verifica se existe TROCA anexada a venda e preenche o _troca (clTroca)
            VerificaTroca()
            '
            If IsNothing(bindVenda.DataSource) Then
                bindVenda.DataSource = _Venda
                PreencheDataBinding()
            Else
                bindVenda.Clear()
                bindVenda.DataSource = _Venda
                bindVenda.ResetBindings(True)
                'AddHandler _ClientePF.AoAlterar, AddressOf HandlerAoAlterar
            End If
            '
            '--- Preenche os Itens da Venda
            PreencheItens()
            '
            '--- Preenche Itens do A Receber (parcelas)
            Preenche_Pagamentos()
            '
            '--- Verifica se a FilialPadrão = PessoaOrigem se não bloqueia alterações
            If _Venda.IDPessoaOrigem <> Obter_FilialPadrao() Then
                MessageBox.Show("A configuração da Filial Padrão atual não coincide" & vbNewLine &
                                "com a Filial ao qual pertence essa Venda..." & vbNewLine &
                                "Assim, não é possível realizar alterações nessa venda.",
                                "Filial Divergente", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Sit = FlagEstado.RegistroBloqueado
            Else
                '--- Atualiza o estado da Situacao: FLAGESTADO
                Select Case _Venda.IDSituacao
                    Case 1 ' VENDA INICIADA
                        Sit = FlagEstado.NovoRegistro
                    Case 2 ' VENDA FINALIZADA
                        Sit = FlagEstado.RegistroSalvo
                    Case 3 ' VENDA BLOQUEADA
                        Sit = FlagEstado.RegistroBloqueado
                    Case Else
                End Select
            End If
            '
            '--- Verifica o TotalGeral
            AtualizaTotalGeral()
            '
            '--- Permite a verificacao dos campos IDPlano
            VerificaAlteracao = True
            '
        End Set
        '
    End Property
    '
    Public Sub New(myVenda As clVenda)
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        propVenda = myVenda
        '
    End Sub
    '
#End Region ' / LOAD
    '
#Region "DATABINDING"
    '
    Private Sub PreencheDataBinding()
        '
        lblIDVenda.DataBindings.Add("Text", bindVenda, "IDVenda", True, DataSourceUpdateMode.OnPropertyChanged)
        lblVendaData.DataBindings.Add("Text", bindVenda, "TransacaoData", True, DataSourceUpdateMode.OnPropertyChanged)
        lblVendedor.DataBindings.Add("Text", bindVenda, "ApelidoFuncionario", True, DataSourceUpdateMode.OnPropertyChanged)
        lblFilial.DataBindings.Add("Text", bindVenda, "ApelidoFilial", True, DataSourceUpdateMode.OnPropertyChanged)
        txtObservacao.DataBindings.Add("Text", bindVenda, "Observacao", True, DataSourceUpdateMode.OnPropertyChanged)
        lblTotalGeral.DataBindings.Add("Text", bindVenda, "TotalVenda", True, DataSourceUpdateMode.OnPropertyChanged)
        lblValorDevolucao.DataBindings.Add("Text", bindVenda, "ValorDevolucao", True, DataSourceUpdateMode.OnPropertyChanged)
        lblValorProdutos.DataBindings.Add("Text", bindVenda, "ValorProdutos", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler lblIDVenda.DataBindings("Text").Format, AddressOf FormatRG
        AddHandler lblVendaData.DataBindings("text").Format, AddressOf FormatDT
        AddHandler lblTotalGeral.DataBindings("Text").Format, AddressOf FormatCUR
        AddHandler lblValorDevolucao.DataBindings("Text").Format, AddressOf FormatCUR
        AddHandler lblValorProdutos.DataBindings("Text").Format, AddressOf FormatCUR
        '
        ' ADD HANDLER PARA DATABINGS
        AddHandler _Venda.AoAlterar, AddressOf HandlerAoAlterar
        '
    End Sub
    '
    Private Sub HandlerAoAlterar()
        If _Venda.RegistroAlterado = True And Sit = FlagEstado.RegistroSalvo Then
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
#End Region ' / DATABINDINGS
    '
#Region "CARREGA/INSERE ITENS"
    Private Sub PreencheItens()
        '
        '--- limpa as colunas do datagrid
        dgvItens.Columns.Clear()
        dgvItens.AutoGenerateColumns = False
        '
        ' altera as propriedades importantes
        dgvItens.MultiSelect = False
        dgvItens.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvItens.ColumnHeadersVisible = True
        dgvItens.AllowUserToResizeRows = False
        dgvItens.AllowUserToResizeColumns = False
        dgvItens.RowHeadersVisible = True
        dgvItens.RowHeadersWidth = 30
        dgvItens.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvItens.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvItens.StandardTab = True
        '
        '--- configura o DataSource
        dgvItens.DataSource = GetType(List(Of))
        dgvItens.DataSource = _ItensList
        If dgvItens.Rows.Count > 0 Then dgvItens.CurrentCell = dgvItens.Rows(dgvItens.Rows.Count).Cells(1)
        '--- formata as colunas do datagrid
        FormataColunas_Itens()
        '
    End Sub
    '
    Private Sub FormataColunas_Itens()
        '
        ' (1) COLUNA IDItem
        dgvItens.Columns.Add("clnIDTansacaoItem", "IDItem")
        With dgvItens.Columns("clnIDTansacaoItem")
            .DataPropertyName = "IDTransacaoItem"
            .Width = 0
            .Resizable = DataGridViewTriState.False
            .Visible = False
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        '
        ' (2) COLUNA RGProduto
        dgvItens.Columns.Add("clnRGProduto", "Reg.")
        With dgvItens.Columns("clnRGProduto")
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
        dgvItens.Columns.Add("clnProduto", "Descrição")
        With dgvItens.Columns("clnProduto")
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
        dgvItens.Columns.Add("clnQuantidade", "Qtde.")
        With dgvItens.Columns("clnQuantidade")
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
        dgvItens.Columns.Add("clnPreco", "Preço")
        With dgvItens.Columns("clnPreco")
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
        dgvItens.Columns.Add("clnSubTotal", "SubTotal")
        With dgvItens.Columns("clnSubTotal")
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
        dgvItens.Columns.Add("clnDesconto", "Desc.")
        With dgvItens.Columns("clnDesconto")
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
        dgvItens.Columns.Add("clnTotal", "Total")
        With dgvItens.Columns("clnTotal")
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
    '--- RETORNA TODOS OS ITENS DA VENDA
    Private Sub obterItens()
        '
        Dim tBLL As New TransacaoItemBLL
        Try
            _ItensList = tBLL.GetVendaItens_IDVenda_List(_Venda.IDVenda, _Filial)
        Catch ex As Exception
            MessageBox.Show("Um execeção ocorreu ao obter Itens da Venda:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '
    '--- INSERE UM NOVO ITEM NA LISTA DE PRODUTOS
    '---------------------------------------------------------------------------------------------
    Private Sub Inserir_Item()
        '
        '--- Verifica se esta Bloqueado ou Finalizado
        If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
        If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
        '
        '--- Abre o frmItem
        '
        Dim fItem As New frmVendaItem(Me, precoOrigem.PRECO_VENDA, _Filial, Nothing)
        fItem.ShowDialog()
        '
        '--- Verifica o retorno do Dialog
        If Not fItem.DialogResult = DialogResult.OK Then Exit Sub
        '
        Dim newItem As clTransacaoItem = fItem.propItem
        '
        '--- Insere o novo Item
        Dim ItemBLL As New TransacaoItemBLL
        Dim myID As Long? = Nothing
        '
        '----------------------------------------------------------------------------------------------
        '--- Insere o novo ITEM no BD
        Try
            newItem.IDTransacao = _Venda.IDVenda
            myID = ItemBLL.InserirNovoItem(newItem, TransacaoItemBLL.EnumMovimento.SAIDA, _Venda.TransacaoData)
            newItem.IDTransacaoItem = myID
        Catch ex As Exception
            MessageBox.Show("Houve um exceção ao INSERIR o item no BD..." & vbNewLine & ex.Message, "Exceção Inesperada",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '
        '--- Insere o ITEM na lista
        _ItensList.Add(newItem)
        bindItem.ResetBindings(False)
        '
        '--- Atualiza o DataGrid
        dgvItens.DataSource = bindItem
        bindItem.MoveLast()
        '
        '--- Atualiza o label TOTAL
        AtualizaTotalGeral()
        '
    End Sub
    '    
    '--- EDITA UM ITEM NA LISTA DE PRODUTOS
    '---------------------------------------------------------------------------------------------
    Private Sub Editar_Item()
        '
        '--- Verifica se esta Bloqueado ou Finalizado
        If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
        If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
        '
        '--- Verifica se há um item selecionado
        If dgvItens.SelectedRows.Count = 0 Then Exit Sub
        '
        '--- obtem o itemAtual
        Dim itmAtual As clTransacaoItem
        itmAtual = dgvItens.SelectedRows(0).DataBoundItem
        '
        '--- Abre o frmItem
        Dim fitem As New frmVendaItem(Me, precoOrigem.PRECO_VENDA, _Filial, itmAtual)
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
            itmAtual.IDTransacao = _Venda.IDVenda
            myID = ItemBLL.EditarItem(itmAtual, TransacaoItemBLL.EnumMovimento.SAIDA, _Venda.TransacaoData)
            itmAtual.IDTransacaoItem = myID
        Catch ex As Exception
            MessageBox.Show("Houve um exceção ao ALTERAR o item no BD..." & vbNewLine & ex.Message,
                            "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '
        '--- Atualiza o ITEM da lista
        '
        '_ItensList.Item(i) = Item
        bindItem.ResetBindings(False)
        '
        '--- Atualiza o DataGrid
        dgvItens.DataSource = bindItem
        'bindItem.CurrencyManager.Position = i
        '
        '--- Atualiza o label TOTAL
        AtualizaTotalGeral()
        '
    End Sub
    '    
    '--- EXCLUI UM ITEM NA LISTA DE PRODUTOS
    '---------------------------------------------------------------------------------------------
    Private Sub Excluir_Item()
        '
        '--- Verifica se esta Bloqueado ou Finalizado
        If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
        If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
        '
        '--- Verifica se há um item selecionado
        If dgvItens.SelectedRows.Count = 0 Then Exit Sub
        '
        '--- obtem o itemAtual
        Dim itmAtual As clTransacaoItem
        itmAtual = dgvItens.SelectedRows(0).DataBoundItem
        '
        '--- pergunta ao usuário se deseja excluir o item
        If MessageBox.Show("Deseja realmente REMOVER esse item da Venda?" & vbNewLine & vbNewLine &
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
            myID = ItemBLL.ExcluirItem(itmAtual, TransacaoItemBLL.EnumMovimento.SAIDA)
        Catch ex As Exception
            MessageBox.Show("Houve um exceção ao EXCLUIR o item no BD..." & vbNewLine & ex.Message,
                            "Exceção Inesperada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
        '
        '--- Atualiza o ITEM da lista
        _ItensList.RemoveAt(i)
        bindItem.ResetBindings(False)
        '
        '--- Atualiza o DataGrid
        dgvItens.DataSource = bindItem
        '
        '--- Atualiza o label TOTAL
        AtualizaTotalGeral()
        '
    End Sub
    '
    '---------------------------------------------------------------------------------------------------
    ' CRIA TECLA DE ATALHO PARA INSERIR/EDITAR PRODUTO
    '---------------------------------------------------------------------------------------------------
    Private Sub dgvItens_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvItens.KeyDown
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
    Private Sub dgvItens_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvItens.CellDoubleClick
        Editar_Item()
    End Sub
    '
    Private Sub dgvItens_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvItens.MouseDown
        If e.Button = MouseButtons.Right Then
            'Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvItens.HitTest(e.X, e.Y)
            dgvItens.ClearSelection()
            '
            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
            '
            ' seleciona o ROW
            dgvItens.CurrentCell = dgvItens.Rows(hit.RowIndex).Cells(1)
            dgvItens.Rows(hit.RowIndex).Selected = True
            '
            'mnuItens.Show(dgvItens, c.PointToScreen(e.Location))
            mnuItens.Show(dgvItens, e.Location)
            '
        End If
    End Sub
    '
    Private Sub MenuItemEditar_Click(sender As Object, e As EventArgs) Handles MenuItemEditar.Click
        Editar_Item()
    End Sub
    '
    Private Sub MenuItemInserir_Click(sender As Object, e As EventArgs) Handles MenuItemInserir.Click
        Inserir_Item()
    End Sub
    '
    Private Sub MenuItemExcluir_Click(sender As Object, e As EventArgs) Handles MenuItemExcluir.Click
        Excluir_Item()
    End Sub
    '
#End Region '/ CARREGA/INSERE ITENS
    '
#Region "BOTOES DE ACAO"
    '
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        If Sit = FlagEstado.NovoRegistro Or Sit = FlagEstado.Alterado Then
            Dim vlReceber As Double = AtualizaTotalPago()
            '
            If vlReceber > 0 Then
                '--- Verifica se o valor da venda é igual à soma das parcelas
                If Math.Abs(AtualizaTotalGeral() - AtualizaTotalPago()) > 1 Then
                    If MessageBox.Show("Se você fechar agora os pagamentos não serão salvos..." & vbNewLine &
                                       "Deseja Fechar a Venda assim mesmo?", "Fechar a Venda",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
                        tabPrincipal.SelectedTab = vtab2
                        dgvPagamentos.Focus()
                        Exit Sub
                    End If
                End If
            End If
        End If
        '
        Close()
        MostraMenuPrincipal()
    End Sub
    '
    '--- ALTERA O VENDEDOR E SALVA NO BD
    Private Sub btnVendedorAlterar_Click(sender As Object, e As EventArgs) Handles btnVendedorAlterar.Click
        '
        Dim fFunc As New frmFuncionarioProcurar(True, Me)
        fFunc.ShowDialog()
        If fFunc.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        Try
            Dim newID As Integer = fFunc.IDEscolhido
            '
            If vBLL.AtualizaVendaVendedor(_Venda.IDVenda, newID) Then
                _Venda.IDVendedor = newID
                _Venda.ApelidoFuncionario = fFunc.NomeEscolhido
                lblVendedor.DataBindings("Text").ReadValue()
            End If
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao atualizar o Vendedor..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- ALTERAR A DATA DA VENDA
    Private Sub lblVendaData_DoubleClick(sender As Object, e As EventArgs) _
        Handles lblVendaData.DoubleClick, btnData.Click
        '
        Dim frmDt As New frmDataInformar("Informe a data da Venda", DataTipo.PassadoPresente, _Venda.TransacaoData, Me)
        frmDt.ShowDialog()
        '
        If frmDt.DialogResult <> DialogResult.OK Then Exit Sub
        '
        Dim newDt As Date = frmDt.propDataInfo
        '
        '--- verifica a data bloqueada
        If DataBloqueada(newDt, Obter_ContaPadrao) Then Exit Sub
        '
        '--- altera a data da venda e salva no BD
        Dim tranBLL As New TransacaoBLL
        If tranBLL.AtualizaTransacaoData(_Venda.IDVenda, newDt) Then
            '
            _Venda.TransacaoData = frmDt.propDataInfo
            lblVendaData.DataBindings("Text").ReadValue()
            '
            '--- verifica se existe troca e altera a data da troca
            If Not IsNothing(_Troca) Then
                Dim tBLL As New TrocaBLL
                _Troca.TrocaData = newDt
                tBLL.AtualizaTroca_Procedure_ID(_Troca)
            End If
            '
        End If
        '
    End Sub
    '
    '--- LIMPAR TODOS OS PAGAMENTOS DA VENDA
    Private Sub btnLimparPagamentos_Click(sender As Object, e As EventArgs) Handles btnLimparPagamentos.Click
        '
        '--- Verifica se o registro esta bloqueado
        If RegistroBloqueado() Then Exit Sub
        '
        '--- Verifica se ha entrada para limpar
        If _EntradaList.Count = 0 Then
            MessageBox.Show("Não existem pagamentos de Entrada nessa Venda para serem removidos...",
                            "Limpar Pagamentos", MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            dgvPagamentos.Focus()
            Return
        End If
        '
        '--- Pergunta ao Usuário se Deseja inserir LIMPAR PAGAMENTOS
        If MessageBox.Show("Você deseja realmente LIMPAR todos os Pagamentos/Entradas dessa venda?",
                           "Limpar Pagamentos", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question) = DialogResult.No Then Return
        '
        '--- Faz a limpeza
        Limpa_Pagamentos()
        '
    End Sub
    '
#End Region '/ BOTOES ACAO
    '
#Region "FORMATACAO E FLUXO"
    ' CRIA TECLA DE ATALHO PARA O TAB
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
            dgvItens.Focus()
        ElseIf tabPrincipal.SelectedIndex = 1 Then
            dgvPagamentos.Focus()
        End If
    End Sub
    '
#End Region
    '
#Region "CONTROLE DO GRID PAGAMENTOS"
    '
    '============================================================================================================
    ' PAGAMENTOS CONTROLES
    '============================================================================================================
    '
    '--- RETORNA TODOS OS PAGAMENTOS
    Private Sub obterPagamentos()
        Dim pBLL As New EntradaBLL
        Try
            _EntradaList = pBLL.Entrada_GET_PorOrigemID(EntradaOrigem.Transacao, _Venda.IDVenda)
            '--- Atualiza o label TOTAL PAGO
            AtualizaTotalPago()
        Catch ex As Exception
            MessageBox.Show("Um execeção ocorreu ao obter os Pagamentos/Entradas da Venda:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- Preenche o DataGrid AReceber
    Private Sub Preenche_Pagamentos()
        With dgvPagamentos
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
            .DataSource = bindEnt
            If .Rows.Count > 0 Then .CurrentCell = .Rows(.Rows.Count).Cells(1)
        End With
        '
        '--- formata as colunas do datagrid
        FormataGrid_Pagamentos()
        '
    End Sub
    '
    Private Sub FormataGrid_Pagamentos()
        '
        Dim cellStyleCur As New DataGridViewCellStyle
        cellStyleCur.Format = "c"
        cellStyleCur.NullValue = Nothing
        cellStyleCur.Alignment = DataGridViewContentAlignment.MiddleRight
        '
        ' (1) COLUNA IDEntrada
        dgvPagamentos.Columns.Add("clnID", "ID")
        With dgvPagamentos.Columns("clnID")
            .DataPropertyName = "IDEntrada"
            .Width = 90
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .DefaultCellStyle.Format = "0000"
            .SortMode = DataGridViewColumnSortMode.NotSortable
        End With
        '
        ' (2) COLUNA FORMA
        dgvPagamentos.Columns.Add("clnMovForma", "Forma")
        With dgvPagamentos.Columns("clnMovForma")
            .DataPropertyName = "MovForma"
            .Width = 200
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            '.DefaultCellStyle.Font = New Font("Arial Narrow", 12)
        End With
        '
        ' (3) COLUNA VALOR
        dgvPagamentos.Columns.Add("clnValor", "Valor")
        With dgvPagamentos.Columns("clnValor")
            .DefaultCellStyle = cellStyleCur
            .DataPropertyName = "EntradaValor"
            .Width = 100
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
    Private Sub dgvPagamentos_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvPagamentos.KeyDown
        '
        If e.KeyCode = Keys.Add Then
            '
            e.Handled = True
            '
            'If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            'If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro nao esta finalizado
            '
            Pagamentos_Adicionar()
            '
        ElseIf e.KeyCode = Keys.Enter Then
            '
            e.Handled = True
            '
            'If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            'If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro nao esta finalizado
            '
            Pagamentos_Editar()
            '
        ElseIf e.KeyCode = Keys.Delete Then
            '
            e.Handled = True
            '
            If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
            If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro nao esta finalizado
            '
            Pagamentos_Excluir()
            '
        End If
    End Sub
    '
    Public Sub Pagamentos_Manipulacao(clPag As clEntradas, Acao As FlagAcao)
        '
        If Acao = FlagAcao.INSERIR Then
            ' SE ACAO FOR INSERIR
            '----------------------------------------------------------------------------------------------
            '--- Insere o ITEM na lista
            _EntradaList.Add(clPag)
            bindEnt.ResetBindings(False)
            '--- Atualiza o DataGrid
            dgvPagamentos.DataSource = bindEnt
            bindEnt.MoveLast()
            '
        End If
        '
    End Sub
    '
    Private Sub Pagamentos_Adicionar()
        '
        If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
        '
        '--- Atualiza o Valor do Total Geral
        Dim vl As Double = AtualizaTotalGeral()
        '
        '--- Verifica se o valor dos itens é maior que zero
        If vl = 0 Then
            MessageBox.Show("Não é possivel realizar pagamentos" & vbNewLine &
                            "Quando o valor da Venda ainda é igual a Zero..." & vbNewLine &
                            "Adicione produtos primeiro e depois tente novamente.", "Novo Pagamento",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        '--- Verifica se o ValorPago já é suficiente
        If AtualizaTotalPago() >= vl Then
            MessageBox.Show("Não é possivel adicionar novo pagamento," & vbNewLine &
                "porque o valor dos Pagamentos já é igual ao valor total da venda", "Novo Pagamento",
                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        '--- posiciona o form
        Dim pos As Point = dgvPagamentos.PointToScreen(Point.Empty)
        pos = New Point(pos.X + dgvPagamentos.Width - 10, pos.Y)
        '
        '--- cria nova Entrada
        Dim clPag As New clEntradas
        Dim vlMax As Double = vl - _EntradaList.Sum(Function(x) x.EntradaValor)
        '
        clPag.EntradaValor = vlMax
        clPag.Origem = 1
        clPag.IDOrigem = _Venda.IDVenda
        clPag.EntradaData = _Venda.TransacaoData
        clPag.IDConta = Obter_ContaPadrao()
        clPag.IDEntrada = _EntradaList.Count + 1
        '
        '--- abre o form frmPagamentos
        Dim fPag As New frmVendaEntrada(Me, vlMax, clPag, FlagAcao.INSERIR, pos)
        fPag.ShowDialog()
        '
        '--- AtualizaTotalPago
        AtualizaTotalPago()
        '
    End Sub
    '
    Private Sub Pagamentos_Editar()
        '
        If RegistroBloqueado() Then Exit Sub '--- Verifica se o registro nao esta bloqueado
        If RegistroFinalizado() Then Exit Sub '--- Verifica se o registro está Finalizado
        '
        '--- posiciona o form
        Dim pos As Point = dgvPagamentos.PointToScreen(Point.Empty)
        pos = New Point(pos.X + dgvPagamentos.Width - 10, pos.Y)
        '
        '--- GET Pagamentos do DataGrid
        If dgvPagamentos.SelectedRows.Count = 0 Then Exit Sub
        '
        Dim PagAtual As clEntradas = dgvPagamentos.SelectedRows(0).DataBoundItem
        '
        Dim fPag As New frmVendaEntrada(Me, AtualizaTotalGeral(), PagAtual, FlagAcao.EDITAR, pos)
        fPag.ShowDialog()
        '
        '--- AtualizaTotalPago
        AtualizaTotalPago()
    End Sub
    '
    Private Sub Pagamentos_Excluir()
        '
        '--- verifica se existe alguma parcela selecionada
        If dgvPagamentos.SelectedRows.Count = 0 Then Exit Sub
        '
        '--- seleciona a parcela
        Dim PagAtual As clEntradas
        PagAtual = dgvPagamentos.SelectedRows(0).DataBoundItem
        '
        '--- pergunta ao usuário se deseja excluir o item
        If MessageBox.Show("Deseja realmente REMOVER esse Pagamento?", "Excluir Pagamento",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If
        '
        '--- envia o comando para excluir a parcela
        '
        Dim i As Integer = dgvPagamentos.SelectedRows(0).Index
        '
        '--- Atualiza o ITEM da lista
        _EntradaList.RemoveAt(i)
        '
        '--- Atualiza a contagem dos itens
        i = 1
        '
        For Each pg As clEntradas In _EntradaList
            pg.IDEntrada = i
            i += 1
        Next
        '
        bindEnt.ResetBindings(False)
        '--- Atualiza o DataGrid
        dgvPagamentos.DataSource = bindEnt
        '--- AtualizaTotalPago
        AtualizaTotalPago()
        '
    End Sub
    '
    ' ALTERA A COR DO DATAGRIDVIEW QUANDO GANHA OU PERDE O FOCO
    '-----------------------------------------------------------------------------------------------------
    Private Sub dgv_GotFocus(sender As Object, e As EventArgs) Handles dgvPagamentos.GotFocus
        '
        Dim c As Color = Color.FromArgb(209, 215, 220)
        sender.BackgroundColor = c
        DirectCast(sender, DataGridView).BorderStyle = BorderStyle.Fixed3D
        '
    End Sub
    '
    Private Sub dgv_LostFocus(sender As Object, e As EventArgs) Handles dgvPagamentos.LostFocus
        '
        Dim c As Color = Color.FromArgb(224, 232, 243)
        sender.BackgroundColor = c
        DirectCast(sender, DataGridView).BorderStyle = BorderStyle.None
        '
    End Sub
    '
    ' CONTROLA O MENU NO DATAGRID PAGAMENTOS
    '-----------------------------------------------------------------------------------------------------
    Private Sub dgvPagamentos_MouseDown(sender As Object, e As MouseEventArgs) Handles dgvPagamentos.MouseDown
        If e.Button = MouseButtons.Right Then
            'Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvPagamentos.HitTest(e.X, e.Y)
            dgvPagamentos.ClearSelection()
            '
            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
            '
            ' seleciona o ROW
            dgvPagamentos.CurrentCell = dgvPagamentos.Rows(hit.RowIndex).Cells(1)
            dgvPagamentos.Rows(hit.RowIndex).Selected = True
            dgvPagamentos.Focus()
            '
            mnuItens.Show(dgvPagamentos, e.Location)
            '
        End If
    End Sub
    '
    Private Sub dgvAReceber_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPagamentos.CellDoubleClick
        Pagamentos_Editar()
    End Sub
    '
    '============================================================================================================
#End Region
    '
#Region "FINALIZAR VENDA"
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
            If MessageBox.Show("Deseja realmente Finalizar essa Transação de Venda?", "Finalizar Venda",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.No Then
                Exit Sub
            End If
            '
            '--- SALVA OS PAGAMENTOS NO BD
            If Salvar_Pagamentos() = False Then Exit Sub
            '
            '--- SALVA A TRANSACAO/VENDA NO BD
            Try
                '--- altera a situacao da transacao atual
                _Venda.IDSituacao = 2 'CONCLUÍDA
                '
                Dim obj As Object = vBLL.AtualizaVenda_Procedure_ID(_Venda)
                '
                If Not IsNumeric(obj) Then
                    Throw New Exception(obj.ToString)
                End If
                '
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
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
        '
        '----------------------------------------------------------------------------------------------
        ' VERIFICA OS VALORES DA VENDA, DAS PARCELAS E DO FRETE
        '----------------------------------------------------------------------------------------------
        '
        '--- Verifica o valor da venda
        If AtualizaTotalGeral() = 0 Then
            MessageBox.Show("Não é possível finalizar uma venda cujo valor final é igual a Zero..." & vbNewLine &
                            "Favor incluir alguns produtos nessa venda.",
                            "Venda Nula", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            tabPrincipal.SelectedTab = vtab1
            dgvItens.Focus()
            Return False
        End If
        '
        '--- Verifica se o valor da venda é igual à soma dos pagamentos
        If Math.Abs(AtualizaTotalGeral() - AtualizaTotalPago()) >= 1 Then
            tabPrincipal.SelectedTab = vtab2
            dgvPagamentos.Focus()
            If MessageBox.Show("A soma dos pagamentos é diferente da soma dos produtos" & vbNewLine &
                               "Deseja Inserir os pagamentos agora?",
                               "Inserir Pagamentos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                '
                Efetuar_Pagamentos()
                '
                '--- Verifica novamente se o valor da venda é igual à soma dos pagamentos
                If Math.Abs(AtualizaTotalGeral() - AtualizaTotalPago()) >= 1 Then
                    Return False
                Else
                    Return True
                End If
                '
            Else
                Return False
            End If
        End If
        '
        Return True
        '
    End Function
    '
    Private Function Salvar_Pagamentos() As Boolean
        '
        '--- verifica se existem pagamentos
        If _EntradaList.Count = 0 Then Return False
        '
        Dim recBLL As New AReceberBLL
        '
        '--- Exclui todos os registros de AReceber da Venda atual
        Try
            '
            recBLL.Excluir_AReceber_Transacao(_Venda.IDVenda)
            '
            '--- Insere um AReceber no BD
            Dim rec As New clAReceber
            '
            rec.IDOrigem = _Venda.IDVenda
            rec.Origem = clAReceber.AReceberOrigem.Transacao
            rec.IDPessoa = _Venda.IDPessoaDestino
            rec.AReceberValor = _Venda.TotalVenda
            '
            recBLL.InserirNovo_AReceber(rec)
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao SALVAR Registros de AReceber..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
        '--- Excluir todas as entradas da Venda Atual
        Try
            Dim entBLL As New EntradaBLL
            '
            entBLL.Entrada_ExcluirTodas_PorTransacao(_Venda.IDVenda)
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao LIMPAR Registros de Entrada..." & vbNewLine &
                ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
        Try
            '--- INSERE AS ENTRADAS NO BD
            '--- Agrupa as Entradas pelo IDMovForma (soma seus valores)
            '--- Cria uma nova lista de Entradas pelo agrupamento
            Dim PagGroupList As New List(Of clEntradas)
            Dim ContaPadrao As Integer = Obter_ContaPadrao()
            '
            For Each pagItem As clEntradas In _EntradaList
                If Not PagGroupList.Exists(Function(x) x.IDMovForma = pagItem.IDMovForma) Then
                    '--- cria nova Entrada
                    Dim clPag As New clEntradas
                    '
                    clPag.EntradaValor = 0
                    clPag.Origem = 1
                    clPag.IDOrigem = _Venda.IDVenda
                    clPag.EntradaData = _Venda.TransacaoData
                    clPag.IDConta = ContaPadrao
                    clPag.IDMovForma = pagItem.IDMovForma
                    clPag.MovForma = pagItem.MovForma
                    '
                    PagGroupList.Add(clPag)
                End If
                '
                Dim pg As clEntradas = PagGroupList.Find(Function(x) x.IDMovForma = pagItem.IDMovForma)
                pg.EntradaValor = pg.EntradaValor + pagItem.EntradaValor
            Next
            '
            '--- Remove os itens que foram agrupados
            _EntradaList.Clear()
            '
            '--- Atualiza a listagem
            bindEnt.ResetBindings(False)
            '--- Atualiza o DataGrid
            dgvPagamentos.DataSource = bindEnt
            '
            '
            '--- Adiciona a nova lista agrupada ao BD
            Dim pagBll As New EntradaBLL
            '
            For Each pag As clEntradas In PagGroupList
                pag.IDEntrada = pagBll.Entrada_Inserir(pag)
                '
                '--- Insere a nova lista DataGrid pagamentos
                _EntradaList.Add(pag)
                '
                '--- Atualiza a listagem
                bindEnt.ResetBindings(False)
                '--- Atualiza o DataGrid
                dgvPagamentos.DataSource = bindEnt
                '
            Next
            '
            '--- quita o ARECEBER da Venda
            recBLL.Quitar_AReceber_AVista(_Venda.IDVenda, PagGroupList.Sum(Function(x) x.EntradaValor))
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção inesperada ao SALVAR os pagamentos..." & vbNewLine &
                ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
        Return True
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------
    ' ADICIONA PAGAMENTOS EM LOOP ATE COMPLETAR O VALOR TOTAL DA VENDA
    '---------------------------------------------------------------------------------------------
    Private Sub Efetuar_Pagamentos()
        Dim vlTotal As Double = AtualizaTotalGeral()
        Dim vlPago As Double = AtualizaTotalPago()
        Dim qtdePag As Integer = _EntradaList.Count
        '
        Do While Math.Abs(vlTotal - vlPago) >= 1
            '
            '--- posiciona o form
            Dim pos As Point = dgvPagamentos.PointToScreen(Point.Empty)
            pos = New Point(pos.X + dgvPagamentos.Width - 10, pos.Y)
            '
            '--- cria nova Entrada/Pagamento
            Dim clPag As New clEntradas
            Dim vlMax As Double = vlTotal - _EntradaList.Sum(Function(x) x.EntradaValor)
            '
            clPag.EntradaValor = vlMax
            clPag.Origem = 1
            clPag.IDOrigem = _Venda.IDVenda
            clPag.EntradaData = _Venda.TransacaoData
            clPag.IDConta = Obter_ContaPadrao()
            clPag.IDEntrada = _EntradaList.Count + 1
            '
            '--- abre o form frmPagamentos
            Dim fPag As New frmVendaEntrada(Me, vlMax, clPag, FlagAcao.INSERIR, pos)
            fPag.ShowDialog()
            '
            '--- Verifica se foi adiciona algum novo pagamento
            If _EntradaList.Count - qtdePag = 0 Then ' nesse caso não foi adicionado nenhum pagamento
                Exit Do
            Else ' refaz o calculo da nova quantidade de pagamento
                qtdePag = _EntradaList.Count
            End If
            '
            '--- AtualizaTotalPago
            AtualizaTotalPago()
            vlPago = _EntradaList.Sum(Function(x) x.EntradaValor)
            '
        Loop
    End Sub
    '
    '--- LIMPA TODOS OS PAGAMENTOS DA VENDA
    Private Function Limpa_Pagamentos() As Boolean
        '
        '--- LIMPA TODOS OS PAGAMENTOS DA VENDA
        '----------------------------------------------------------------
        Dim eBLL As New EntradaBLL
        '
        Try
            eBLL.Entrada_ExcluirTodas_PorTransacao(_Venda.IDVenda)
            '
            For i = 0 To _EntradaList.Count - 1
                If _EntradaList.Item(i).IDEntrada <> 0 Then _EntradaList.RemoveAt(i)
            Next
            '
            '--- Atualiza a listagem
            bindEnt.ResetBindings(False)
            '
            '--- Atualiza o DataGrid
            dgvPagamentos.DataSource = bindEnt
            '
            '---Atualiza o valor pago
            AtualizaTotalPago()
            '
        Catch ex As Exception
            MessageBox.Show("Ocorreu uma nova exceção ao excluir os pagamentos da venda..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        End Try
        '
        Return True
        '
    End Function
    '
#End Region
    '
#Region "FUNCOES NECESSARIAS"
    '
    ' ATUALIZA O TOTAL DO GERAL
    '-----------------------------------------------------------------------------------------------------
    Private Function AtualizaTotalGeral() As Double
        '
        If _ItensList.Count > 0 Then
            Dim T As Double = 0
            '
            For Each i As clTransacaoItem In _ItensList
                T = T + i.Total
            Next
            '
            _Venda.ValorProdutos = T
            T = T - _Venda.ValorDevolucao
            If T < 0 Then T = 0
            Return T
        Else
            lblTotalGeral.Text = Format(0, "c")
            Return 0
        End If
        '
    End Function
    '
    ' ATUALIZA O TOTAL DOS PAGAMENTOS
    '-----------------------------------------------------------------------------------------------------
    Private Function AtualizaTotalPago() As Double
        If _EntradaList.Count > 0 Then
            Dim T As Double = 0
            '
            For Each p As clEntradas In _EntradaList
                T = T + p.EntradaValor
            Next
            '
            lblTotalPago.Text = Format(T, "c")
            Return T
        Else
            lblTotalPago.Text = Format(0, "c")
            Return 0
        End If
    End Function
    '
    ' CRIA UMA NOVA VENDA
    '-----------------------------------------------------------------------------------------------------
    Public Sub NovaVenda()
        Dim v As New AcaoGlobal
        Dim newVenda As Object = v.VendaAVista_Nova
        '
        If IsNothing(newVenda) Then Exit Sub
        '
        _Venda = newVenda
        '
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
#End Region
    '
#Region "BLOQUEIO DE REGISTRO"

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
        If Not Sit = FlagEstado.RegistroSalvo Then Return False
        '
        If MessageBox.Show("Esse registro de Venda já se encontra FINALIZADO..." & vbNewLine &
                           "Deseja realmente fazer alterações nesse registro?",
                           "Registro Finalizado", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                           MessageBoxDefaultButton.Button2) = DialogResult.No Then Return True
        '
        '--- SALVA A TRANSACAO/VENDA NO BD
        '-------------------------------------------------------------------
        _Venda.IDSituacao = 1 '--- Edita o registro e altera a situação
        '
        Try
            Dim obj As Object = vBLL.AtualizaVenda_Procedure_ID(_Venda)
            '
            If Not IsNumeric(obj) Then
                Throw New Exception(obj.ToString)
            End If
            '
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return True
        End Try
        '
    End Function
    '
#End Region
    '
#Region "TROCA FUNCOES"
    '
    '--- VERIFICA SE EXISTE TROCA E ALTERA O BTNTROCA
    Private Sub VerificaTroca()
        '
        Dim tBLL As New TrocaBLL
        '
        Try
            _Troca = tBLL.GetTroca_PorIDVenda_clTroca(_Venda.IDVenda)
            '
            If Not IsNothing(_Troca) Then
                '
                '--- atualiza a propriedade VALORDEVOLUCAO da Venda
                If _Venda.ValorDevolucao <> _Troca.ValorTotal Then _Venda.ValorDevolucao = _Troca.ValorTotal
                '
                '--- Se houver Troca atualiza o marca o AGREGA DEVOLUCAO
                If Not _Venda.AgregaDevolucao Then _Venda.AgregaDevolucao = True '--- varejo COM troca
                '
                '--- Verifica se o IDSitucao da Troca é igual ao da Venda
                If _Venda.IDSituacao <> _Troca.IDSituacao Then
                    '
                    '--- iguala o IDSituacao
                    _Troca.IDSituacao = _Venda.IDSituacao
                    '
                    Try '--- atualiza a Troca
                        tBLL.AtualizaTroca_Procedure_ID(_Troca)
                    Catch ex As Exception
                        MessageBox.Show("Uma exceção ocorreu ao Atualizar a Situacao da Troca..." & vbNewLine &
                                        ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    '
                End If
                '
            Else
                '--- atualializa a propriedade VALORDEVOLUCAO da Venda
                _Venda.ValorDevolucao = 0
                '
                '--- Se houver Troca atualiza o VendaTipo
                If _Venda.AgregaDevolucao Then _Venda.AgregaDevolucao = False '--- varejo SEM troca
                '
            End If
            '
            '--- Exibe ou esconde o Label do valor da troca
            Troca_EditaLabel()
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao verificar se existe TROCA anexada à Venda..." & vbNewLine &
                ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- INSERE DEVOLUCAO DE TROCA E ABRE O FORMULARIO DE TROCA
    Private Sub btnTroca_Click(sender As Object, e As EventArgs) Handles btnTroca.Click
        '
        If IsNothing(_Troca) Then
            '--- Pergunta ao Usuário se Deseja inserir nova TROCA
            If MessageBox.Show("Você deseja realmente anexar uma Troca Simples nessa venda?",
                               "Inserir Troca", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) = DialogResult.No Then Return
            '
        End If
        '
        '--- Verifica ou Cria uma nova TROCA
        Try
            If IsNothing(_Troca) Then '--- se a TROCA for nothing entao CRIA nova TROCA
                Dim tBLL As New TrocaBLL
                '
                '--- cria nova troca
                _Troca = tBLL.TrocaSimples_Nova(_Venda.IDVenda,
                                                _Venda.TransacaoData,
                                                _Venda.IDPessoaOrigem,
                                                _Venda.ApelidoFilial,
                                                _Venda.IDPessoaDestino,
                                                _Venda.Cadastro,
                                                _Venda.IDUser)
                '
            End If
            '
            '--- abre o frmTROCA
            Dim old_vlTroca = If(IsNothing(_Troca), 0, _Troca.ValorTotal)
            Dim fTroca As New frmTrocaSimples(_Troca, _Venda, Me)
            fTroca.ShowDialog()
            '
            '--- verifica se houve alteracao da troca pelo valor
            VerificaTroca()
            '
            '--- se houve alteracao
            If old_vlTroca <> If(IsNothing(_Troca), 0, _Troca.ValorTotal) Then
                AtualizaTotalGeral()
                Sit = FlagEstado.Alterado
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Verificar ou Inserir Troca..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- VERIFICA E ADICIONA OU ALTERA A TROCA NA LISTA DE ENTRADA
    Private Sub Troca_EditaLabel()
        '
        If Not IsNothing(_Troca) Then
            pnlTroca.Visible = True
            '
            '-- atualiza o btnTroca e o lblTroca
            btnTroca.Text = "Altera &Troca"
            '
            '-- atualiza o lbltitulo
            lblTitulo.Text = "Venda A Vista com Troca"
        Else
            '
            pnlTroca.Visible = True
            '
            '-- atualiza o btnTroca e o lblTroca
            btnTroca.Text = "Insere &Troca"
            '
            '-- atualiza o lbltitulo
            lblTitulo.Text = "Venda Vista"
            '
        End If
        '
    End Sub
    '
#End Region '/ TROCA FUNCOES
    '
End Class
