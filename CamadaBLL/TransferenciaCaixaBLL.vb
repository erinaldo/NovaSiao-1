Imports CamadaDAL
Imports CamadaDTO

Public Class TransferenciaCaixaBLL
    '
    '============================================================================================
    ' INSERT NOVA TRANSFERENCIA E RETORNA UM CLTRANSFERENCIA
    '============================================================================================
    Public Function InserirNova_Transferencia(Transf As clTransferenciaCaixa) As clTransferenciaCaixa

        Dim db As New AcessoDados
        Dim myQuery As String = ""
        '
        '--- INICIA UMA TRANSACAO
        '---------------------------------------------------------------------------------------------
        db.BeginTransaction()
        '
        '--- INSERT A TRANSFERENCIA DE CAIXA
        '---------------------------------------------------------------------------------------------
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDContaDebito", Transf.IDContaDebito)
        db.AdicionarParametros("@IDContaCredito", Transf.IDContaCredito)
        db.AdicionarParametros("@TransferenciaData", Transf.TransferenciaData)
        db.AdicionarParametros("@TransferenciaValor", Transf.TransferenciaValor)
        db.AdicionarParametros("@ComissaoValor", Transf.ComissaoValor)
        '
        myQuery = "INSERT INTO tblCaixaTransferencia" &
                  "(IDContaDebito, IDContaCredito, TransferenciaData, TransferenciaValor, ComissaoValor)" &
                  "VALUES(@IDContaDebito, @IDContaCredito, @TransferenciaData, @TransferenciaValor, @ComissaoValor)"
        '
        Try
            db.ExecutarManipulacao(CommandType.Text, myQuery)
            '
            '--- Get IDENTITY
            db.LimparParametros()
            Transf.IDTransferencia = db.ExecutarConsulta(CommandType.Text, "SELECT @@IDENTITY As LastID;").Rows(0)(0)
            '
        Catch ex As Exception
            db.RollBackTransaction()
            Throw New Exception("Uma exceção ocorreu ao Inserir uma Nova Tranferência..." & vbNewLine & vbNewLine &
                                ex.Message)
        End Try
        '
        '--- INSERT A OBSERVACAO IF NECESSARY
        '---------------------------------------------------------------------------------------------
        If Not IsNothing(Transf.Observacao) AndAlso Transf.Observacao.Trim.Length > 0 Then
            '
            Dim oBLL As New ObservacaoBLL
            '
            Try
                oBLL.SaveObservacao(13, Transf.IDTransferencia, Transf.Observacao, db)
            Catch ex As Exception
                db.RollBackTransaction()
                Throw New Exception("Uma exceção ocorreu ao Inserir uma Nova Observação..." & vbNewLine & vbNewLine &
                                    ex.Message)
            End Try
            '
        End If
        '
        '--- INSERT A MOVIMENTACAO DE SAIDA
        '---------------------------------------------------------------------------------------------
        Dim mBLL As New MovimentacaoBLL
        Dim movSaida As New clMovimentacao(EnumMovimentacaoOrigem.Transferencia, EnumMovimento.Transferencia)
        '
        movSaida.IDConta = Transf.IDContaDebito
        movSaida.Creditar = False
        movSaida.Descricao = "Débito de Transferencia para Conta: " & Transf.ContaDebito
        movSaida.IDFilial = Transf.IDFilial
        movSaida.IDMeio = Transf.IDMeio
        movSaida.MovData = Transf.TransferenciaData
        movSaida.Movimento = 3 '--- transferencia
        movSaida.MovValor = Transf.TransferenciaValor * (-1)
        movSaida.IDOrigem = Transf.IDTransferencia '--- IDTranferencia
        movSaida.Origem = 11 '--- tblCaixaTransferencia
        '
        Try
            movSaida = mBLL.Movimentacao_Inserir(movSaida, db)
        Catch ex As Exception
            db.RollBackTransaction()
            Throw New Exception("Uma exceção ocorreu ao Inserir uma Nova Movimentação..." & vbNewLine & vbNewLine &
                                ex.Message)
        End Try
        '
        '--- INSERT A MOVIMENTACAO DE CREDITO
        '---------------------------------------------------------------------------------------------
        Dim movEntrada As New clMovimentacao(EnumMovimentacaoOrigem.Transferencia, EnumMovimento.Transferencia)
        '
        movEntrada.IDConta = Transf.IDContaCredito
        movEntrada.Creditar = False
        movEntrada.Descricao = "Crédito de Transferencia da Conta: " & Transf.ContaDebito
        movEntrada.IDFilial = Transf.IDFilial
        movEntrada.IDMeio = Transf.IDMeio
        movEntrada.MovData = Transf.TransferenciaData
        movEntrada.Movimento = 3 '--- transferencia
        movEntrada.MovValor = Transf.TransferenciaValor - Transf.ComissaoValor
        movEntrada.IDOrigem = Transf.IDTransferencia '--- IDTranferencia
        movEntrada.Origem = 11 '--- tblCaixaTransferencia
        '
        Try
            movEntrada = mBLL.Movimentacao_Inserir(movEntrada, db)
        Catch ex As Exception
            db.RollBackTransaction()
            Throw New Exception("Uma exceção ocorreu ao Inserir uma Nova Movimentação..." & vbNewLine & vbNewLine &
                                ex.Message)
        End Try
        '
        '--- UPDATE TBLTRANSFERENCIA WITH IDMOVCREDITO AND IDMOVDEBITO
        '---------------------------------------------------------------------------------------------
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDTransferencia", Transf.IDTransferencia)
        db.AdicionarParametros("@IDMovDebito", movSaida.IDMovimentacao)
        db.AdicionarParametros("@IDMovCredito", movEntrada.IDMovimentacao)
        '
        myQuery = "UPDATE tblCaixaTransferencia SET " &
                  "IDMovDebito = @IDMovDebito, IDMovCredito = @IDMovCredito " &
                  "WHERE IDTransferencia = @IDTransferencia"
        '
        Try
            '
            db.ExecutarManipulacao(CommandType.Text, myQuery)
            '
            db.CommitTransaction()
            Return Transf
            '
        Catch ex As Exception
            db.RollBackTransaction()
            Throw New Exception("Uma exceção ocorreu ao Inserir uma Nova Tranferência..." & vbNewLine & vbNewLine &
                                ex.Message)
        End Try
        '
    End Function
    '
    '==========================================================================================
    ' GET LIST OF TRANSFERENCIAS
    '==========================================================================================
    Public Function GetTransferenciasLista_Procura(myIDContaDebito As Int16,
                                                   Optional dtInicial As Date? = Nothing,
                                                   Optional dtFinal As Date? = Nothing) As List(Of clTransferenciaCaixa)
        '
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDContaDebito", myIDContaDebito)
        Dim myQuery As String = "SELECT * FROM qryCaixaTransferencia WHERE IDContaDebito = @IDContaDebito"
        '
        If Not IsNothing(dtInicial) Then
            '
            db.AdicionarParametros("@DataInicial", dtInicial)
            myQuery = myQuery & " AND TransferenciaData >= @DataInicial"
            '
        End If
        '
        If Not IsNothing(dtFinal) Then
            '
            db.AdicionarParametros("@DataFinal", dtFinal)
            myQuery = myQuery & " AND TransferenciaData <= @DataFinal"
            '
        End If
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.Text, myQuery)
            Dim tList As New List(Of clTransferenciaCaixa)
            '
            If dt.Rows.Count = 0 Then Return tList
            '
            For Each r In dt.Rows
                '
                tList.Add(Convert_DtRow_clTransferencia(r))
                '
            Next
            '
            Return tList
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '==========================================================================================
    ' CONVERTER DTROW EM CLTRANSFERENCIA
    '==========================================================================================
    Private Function Convert_DtRow_clTransferencia(r As DataRow) As clTransferenciaCaixa
        '
        Dim clT As New clTransferenciaCaixa
        '
        Try
            '
            clT.IDTransferencia = IIf(IsDBNull(r("IDTransferencia")), Nothing, r("IDTransferencia"))
            clT.IDFilial = IIf(IsDBNull(r("IDFilial")), Nothing, r("IDFilial"))
            clT.ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
            clT.TransferenciaData = IIf(IsDBNull(r("TransferenciaData")), Nothing, r("TransferenciaData"))
            clT.TransferenciaValor = IIf(IsDBNull(r("TransferenciaValor")), Nothing, r("TransferenciaValor"))
            clT.ComissaoValor = IIf(IsDBNull(r("ComissaoValor")), Nothing, r("ComissaoValor"))
            clT.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
            clT.IDMeio = IIf(IsDBNull(r("IDMeio")), Nothing, r("IDMeio"))
            clT.Meio = IIf(IsDBNull(r("Meio")), String.Empty, r("Meio"))
            '
            clT.IDContaDebito = IIf(IsDBNull(r("IDContaDebito")), Nothing, r("IDContaDebito"))
            clT.ContaDebito = IIf(IsDBNull(r("ContaDebito")), String.Empty, r("ContaDebito"))
            clT.IDMovDebito = IIf(IsDBNull(r("IDMovDebito")), Nothing, r("IDMovDebito"))
            '
            clT.IDContaCredito = IIf(IsDBNull(r("IDContaCredito")), Nothing, r("IDContaCredito"))
            clT.ContaCredito = IIf(IsDBNull(r("ContaCredito")), String.Empty, r("ContaCredito"))
            clT.IDMovCredito = IIf(IsDBNull(r("IDMovCredito")), Nothing, r("IDMovCredito"))
            '
            Return clT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '==========================================================================================
    ' EXCLUIR TRANSFERENCIA
    '==========================================================================================
    Public Function Excluir_Transferencia(Transf As clTransferenciaCaixa,
                                          Optional dbTran As Object = Nothing) As Boolean
        '
        Dim db As AcessoDados = If(dbTran, New AcessoDados)
        '
        '--- create TRANSACAO IF NECESSARY
        '----------------------------------------------------------------------------------
        Dim tranInterna As Boolean = False
        '
        If Not db.isTransaction Then
            tranInterna = True
            db.BeginTransaction()
        End If
        '
        '--- get MOVIMENTACAO
        '----------------------------------------------------------------------------------
        Dim lstMovs As List(Of clMovimentacao) = Nothing
        Dim movBLL As New MovimentacaoBLL
        lstMovs = movBLL.Movimentacao_GET_PorOrigemID(EnumMovimentacaoOrigem.Transferencia, Transf.IDTransferencia)
        '
        '--- DELETE OBSERVACAO
        '----------------------------------------------------------------------------------
        Try
            Dim oBLL As New ObservacaoBLL
            '
            oBLL.DeleteObservacao(13, Transf.IDTransferencia)
            '
        Catch ex As Exception
            '
            If tranInterna Then db.RollBackTransaction()
            Throw ex
            Return False
            '
        End Try
        '
        '--- DELETE TRANSFERENCIA
        '----------------------------------------------------------------------------------
        Try
            '
            '--- DELETE OBSERVACAO
            db.LimparParametros()
            db.AdicionarParametros("@IDTransferencia", Transf.IDTransferencia)
            '
            Dim myQuery As String = "DELETE tblCaixaTransferencia WHERE IDTransferencia = @IDTransferencia"
            '
            db.ExecutarManipulacao(CommandType.Text, myQuery)
            '
        Catch ex As Exception
            '
            If tranInterna Then db.RollBackTransaction()
            Throw ex
            Return False
            '
        End Try
        '
        '--- DELETE MOVIMENTACAO CAIXA
        Try
            '
            If lstMovs.Count > 0 Then
                '
                If lstMovs.Where(Function(a) Not IsNothing(a.IDCaixa)).Count > 0 Then
                    Throw New Exception("Não é possível excluir parcelas que tem movimentações incluídas no Caixa...")
                End If
                '
                '--- exclui as movimentacoes
                For Each mov In lstMovs
                    movBLL.Movimentacao_Excluir(mov, db)
                Next
                '
            End If
            '
            If tranInterna Then db.CommitTransaction()
            Return True
            '
        Catch ex As Exception
            '
            If tranInterna Then db.RollBackTransaction()
            Throw ex
            Return False
            '
        End Try
        '
    End Function
    '
End Class
