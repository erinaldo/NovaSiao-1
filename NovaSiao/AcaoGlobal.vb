Imports CamadaBLL
Imports CamadaDTO

Public Class AcaoGlobal
    '
    '================================================================================
    ' EFETUA NOVA VENDA A PRAZO
    '================================================================================
    Public Function VendaPrazo_Nova() As clVenda
        '
        '--- Questiona o VENDEDOR
        Dim fFunc As New frmFuncionarioProcurar(True)
        fFunc.ShowDialog()
        If fFunc.DialogResult = DialogResult.Cancel Then Return Nothing
        '
        Dim IDFunc As Integer = fFunc.IDEscolhido
        '
        '--- Questiona o CLIENTE
        Dim fCli As New frmClienteProcurar()
        fCli.ShowDialog()
        If fCli.DialogResult = DialogResult.Cancel Then Return Nothing
        '
        Dim IDCli As Integer = fCli.propClienteID
        Dim UFCli As String = fCli.propClienteUF
        '
        '--- Pergunta ao Usuário se Deseja inserir nova venda
        If MessageBox.Show("Você deseja realmente inserir uma nova Venda à Prazo?",
                           "Inserir Nova Venda", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) = DialogResult.Cancel Then
            Return Nothing
        End If
        '
        '--- Insere um novo Registro de Venda à Prazo
        '---------------------------------------------------------------------------------------
        Dim vndBLL As New VendaBLL
        Dim newVenda As New clVenda
        Dim tranBLL As New TransacaoBLL
        '
        Try
            '--- Define os valores iniciais
            With newVenda
                .IDPessoaDestino = IDCli
                .IDPessoaOrigem = Obter_FilialPadrao()
                .IDOperacao = 1
                .IDSituacao = TransacaoBLL.TransacaoSituacao.INICIADA
                .IDUser = UsuarioAcesso(0)
                If UFCli = ObterDefault("UF") Then
                    .CFOP = tranBLL.ObterCFOP(TransacaoBLL.OperacaoEnum.Venda, TransacaoBLL.CFOPUFDestino.DentroDaUF)
                Else
                    .CFOP = tranBLL.ObterCFOP(TransacaoBLL.OperacaoEnum.Venda, TransacaoBLL.CFOPUFDestino.ForaDaUF)
                End If
                .TransacaoData = ObterDefault("DataPadrao")
                .IDDepartamento = 1
                .IDVendedor = IDFunc
                .CobrancaTipo = 2 '--- venda à prazo
                .FreteTipo = 0
                .ValorProdutos = 0
                .ValorFrete = 0
                .ValorImpostos = 0
                .ValorAcrescimos = 0
                .JurosMes = 0
                .VendaTipo = 0 '--- varejo
            End With
            '
            newVenda = vndBLL.SalvaNovaVenda_Procedure_Venda(newVenda)
            If IsNothing(newVenda) Then
                MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Venda",
                                "Inserir Nova Venda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Venda" & vbNewLine &
                            vbNewLine & ex.Message,
                            "Inserir Nova Venda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End Try
        '
        Return newVenda
        '
    End Function
    '
    '================================================================================
    ' EFETUA NOVA VENDA A VISTA
    '================================================================================
    Public Function VendaAVista_Nova() As clVenda
        '
        '--- Questiona o VENDEDOR
        Dim fFunc As New frmFuncionarioProcurar(True)
        fFunc.ShowDialog()
        If fFunc.DialogResult = DialogResult.Cancel Then Return Nothing
        '
        Dim IDFunc As Integer = fFunc.IDEscolhido
        '
        '--- Determina o CLIENTE VAREJO
        Dim IDCli As Integer = 0 '--- Cliente Varejo
        '
        '--- Pergunta ao Usuário se Deseja inserir nova venda
        If MessageBox.Show("Você deseja realmente inserir uma NOVA VENDA À VISTA?",
                           "Inserir Nova Venda", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) = DialogResult.Cancel Then
            Return Nothing
        End If
        '
        '--- Insere um novo Registro de Venda
        '---------------------------------------------------------------------------------------
        Dim vndBLL As New VendaBLL
        Dim newVenda As New clVenda
        Dim tranBLL As New TransacaoBLL
        '
        Try
            '--- Define os valores iniciais
            With newVenda
                .IDPessoaDestino = IDCli
                .IDPessoaOrigem = Obter_FilialPadrao()
                .IDOperacao = 1
                .IDSituacao = TransacaoBLL.TransacaoSituacao.INICIADA
                .IDUser = UsuarioAcesso(0)
                .CFOP = tranBLL.ObterCFOP(TransacaoBLL.OperacaoEnum.Venda, TransacaoBLL.CFOPUFDestino.DentroDaUF)
                .TransacaoData = ObterDefault("DataPadrao")
                .IDDepartamento = 1
                .IDVendedor = IDFunc
                .CobrancaTipo = 1 '--- venda à prazo
                .FreteTipo = 0
                .ValorProdutos = 0
                .ValorFrete = 0
                .ValorImpostos = 0
                .ValorAcrescimos = 0
                .JurosMes = 0
                .VendaTipo = 0 '--- varejo
            End With
            '
            newVenda = vndBLL.SalvaNovaVenda_Procedure_Venda(newVenda)
            If IsNothing(newVenda) Then
                MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Venda",
                                "Inserir Nova Venda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Venda" & vbNewLine &
                            vbNewLine & ex.Message,
                            "Inserir Nova Venda", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End Try
        '
        Return newVenda
        '
    End Function
    '
    '================================================================================
    ' EFETUA NOVA COMPRA
    '================================================================================
    Public Function Compra_Nova() As clCompra
        '
        '--- Questiona o FORNECEDOR
        Dim fForn As New frmFornecedorProcurar(True)
        fForn.ShowDialog()
        If fForn.DialogResult = DialogResult.Cancel Then Return Nothing
        '
        Dim IDFornecedor As Integer = fForn.propFornecedor_Escolha.IDPessoa
        Dim FornecedorUF As String = fForn.propFornecedor_Escolha.UF
        '
        '--- Pergunta ao Usuário se Deseja inserir nova COMPRA
        If MessageBox.Show("Você deseja realmente inserir uma NOVA COMPRA?",
                           "Inserir Nova Venda", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) = DialogResult.Cancel Then
            Return Nothing
        End If
        '
        '--- Insere um novo Registro de COMPRA
        '---------------------------------------------------------------------------------------
        Dim cmpBLL As New CompraBLL
        Dim newCompra As New clCompra
        Dim tranBLL As New TransacaoBLL
        '
        Try
            '--- Define os valores iniciais
            With newCompra
                .IDPessoaDestino = Obter_FilialPadrao()
                .IDPessoaOrigem = IDFornecedor
                .IDOperacao = 2
                .IDSituacao = TransacaoBLL.TransacaoSituacao.INICIADA
                .IDUser = UsuarioAcesso(0)
                If FornecedorUF = ObterDefault("UF") Then
                    .CFOP = tranBLL.ObterCFOP(TransacaoBLL.OperacaoEnum.Compra, TransacaoBLL.CFOPUFDestino.DentroDaUF)
                Else
                    .CFOP = tranBLL.ObterCFOP(TransacaoBLL.OperacaoEnum.Compra, TransacaoBLL.CFOPUFDestino.ForaDaUF)
                End If
                .TransacaoData = ObterDefault("DataPadrao")
                .CobrancaTipo = 0 '--- sem Cobrança
                .FreteTipo = 0 '--- sem frete
                .FreteCobrado = 0
                .ICMSValor = 0
                .Despesas = 0
                .Descontos = 0
                .TotalCompra = 0
                .Observacao = ""
            End With
            '
            newCompra = cmpBLL.SalvaNovaCompra_Procedure_Compra(newCompra)
            If IsNothing(newCompra) Then
                MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Compra",
                                "Inserir Nova Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Compra" & vbNewLine &
                            vbNewLine & ex.Message,
                            "Inserir Nova Compra", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End Try
        '
        Return newCompra
        '
    End Function
    '
    '================================================================================
    ' EFETUA NOVA TROCA SIMPLES
    '================================================================================
    Public Function TrocaSimples_Nova() As clTroca
        '
        '--- Questiona o VENDEDOR
        Dim fFunc As New frmFuncionarioProcurar(True)
        fFunc.ShowDialog()
        If fFunc.DialogResult = DialogResult.Cancel Then Return Nothing
        '
        Dim IDFunc As Integer = fFunc.IDEscolhido
        Dim IDCli As Integer
        '
        '--- Questiona CLIENTE DEFINIDO
        Dim respCliente As DialogResult
        respCliente = MessageBox.Show("Você deseja uma troca no nome de um Cliente Cadastrado?",
                      "Inserir Nova Troca", MessageBoxButtons.YesNoCancel,
                      MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
        '
        If respCliente = DialogResult.Cancel Then
            Return Nothing
        ElseIf respCliente = DialogResult.yes Then
            '
            '--- Questiona o CLIENTE
            Dim fCli As New frmClienteProcurar()
            fCli.ShowDialog()
            If fCli.DialogResult = DialogResult.Cancel Then Return Nothing
            '
            IDCli = fCli.propClienteID
            '
        Else
            '
            IDCli = 0 '=> Cliente Varejo
            '
        End If
        '
        '--- Pergunta ao Usuário se Deseja inserir nova TROCA
        If MessageBox.Show("Você deseja realmente inserir uma nova Troca Simples?",
                           "Inserir Nova Troca", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) = DialogResult.Cancel Then
            Return Nothing
        End If
        '
        '--- Insere um novo Registro de Venda à Prazo
        '---------------------------------------------------------------------------------------
        Dim tBLL As New TrocaBLL
        Dim newTroca As New clTroca
        Dim tranBLL As New TransacaoBLL
        '
        Try
            '--- Define os valores iniciais
            With newTroca
                .IDPessoaTroca = IDCli
                .IDFilial = Obter_FilialPadrao()
                .IDSituacao = TransacaoBLL.TransacaoSituacao.INICIADA
                .TrocaData = ObterDefault("DataPadrao")
                .IDVendedor = IDFunc
                .CobrancaTipo = 2 '--- venda à prazo
                .ValorEntrada = 0
                .ValorSaida = 0
                .ValorAcrescimos = 0
                .JurosMes = 0
                '.TotalTroca = 0
            End With
            '
            newTroca = tBLL.InsereNovaTroca_Procedure(newTroca, UsuarioAcesso(0))
            '
            If IsNothing(newTroca) Then
                MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Troca",
                                "Inserir Nova Troca", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Um erro ocorreu ao salvar ao Inserir Nova Troca" & vbNewLine &
                            vbNewLine & ex.Message,
                            "Inserir Nova Troca", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return Nothing
        End Try
        '
        Return newTroca
        '
    End Function
    '
End Class
