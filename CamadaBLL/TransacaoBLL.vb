Imports CamadaDAL

Public Class TransacaoBLL
    '
    Public Enum OperacaoEnum
        Venda = 1 '--- Vendas
        Compra = 2 '--- Compras
        SimplesEntrada = 3 '--- Simples Entrada
        SimplesSaida = 4 '--- Simples Saída
        DevolucaoDeEntrada = 5 '--- Quando o Cliente devolve uma venda
        DevolucaoDeSaida = 6 '--- Quando a Filial Devolve uma Compra
        ConsignacaoEntrada = 7 '--- Quando a Filial recebe uma Consignação
        ConsignacaoSaida = 8 '--- Quando a Filial devolve uma Consignação
    End Enum
    '
    Public Enum CFOPUFDestino
        DentroDaUF = 0
        ForaDaUF = 1
    End Enum
    '
    Public Enum TransacaoSituacao
        INICIADA = 1
        CONCLUIDA = 2
        BLOQUEADA = 3
    End Enum
    '
    Public Enum MovimentoEstoque
        ENTRADA
        SAIDA
    End Enum
    '
    ' OBTER O VALORES CFOP DO CONFIG
    Public Function ObterCFOP(Operacao As OperacaoEnum, UFDestino As CFOPUFDestino) As String
        '
        Try
            Dim dt As DataTable = Get_Operacao_PeloID(Operacao)
            '
            If dt.Rows.Count = 0 Then
                Throw New Exception("Não foi encontradada nenhuma operação comercial")
            End If
            '
            Dim r As DataRow = dt.Rows(0)
            '
            If UFDestino = CFOPUFDestino.DentroDaUF Then
                If IsDBNull(r("CFOPDentro")) OrElse String.IsNullOrEmpty(r("CFOPDentro")) Then
                    Throw New Exception("Não foi encontradado nenhum CFOP Dentro nessa operação comercial")
                End If
                Return r("CFOPDentro")
            Else
                If IsDBNull(r("CFOPFora")) OrElse String.IsNullOrEmpty(r("CFOPFora")) Then
                    Throw New Exception("Não foi encontradado nenhum CFOP Fora nessa operação comercial")
                End If
                Return r("CFOPFora")
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    ' OBTER DATATABLE DA OPERAÇÃO
    Public Function Get_Operacao_PeloID(myOperacao As OperacaoEnum) As DataTable
        Dim db As New AcessoDados
        Try
            Dim sql As String = "SELECT * FROM tblOperacao WHERE IDOperacao = " & CInt(myOperacao)
            '
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.Text, sql)
            '
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    ' OBTER UMA LISTA DE OPERACAO
    Public Function Get_Operacao_DT(Optional _movimento As MovimentoEstoque? = Nothing) As DataTable
        Dim db As New AcessoDados
        Dim sql As String

        If IsNothing(_movimento) Then
            sql = "SELECT * FROM tblOperacao"
        Else
            If _movimento = MovimentoEstoque.ENTRADA Then
                sql = "SELECT * FROM tblOperacao WHERE MovimentoEstoque = 'E'"
            Else
                sql = "SELECT * FROM tblOperacao WHERE MovimentoEstoque = 'S'"
            End If
        End If
        '
        Try
            Return db.ExecutarConsulta(CommandType.Text, sql)
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--- SALVA O CFOP DE DETERMINADA OPERAÇAO
    Public Function SalvaOperacao_CFOP(myIDOperacao As Integer, myCFOPDentro As Object, myCFOPFora As Object) As Boolean
        Dim db As New AcessoDados
        Dim sql As String = String.Format("UPDATE tblOperacao SET CFOPDentro = {1}, CFOPFora = {2} WHERE IDOperacao = {0}",
                                          myIDOperacao,
                                          IIf(IsDBNull(myCFOPDentro), "NULL", myCFOPDentro),
                                          IIf(IsDBNull(myCFOPFora), "NULL", myCFOPFora))
        '
        Try
            db.ExecutarConsulta(CommandType.Text, sql)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' UPDATE A DATA DA TRANSACAO
    '--------------------------------------------------------------------------------------------
    Public Function AtualizaTransacaoData(myIDTransacao As Integer, myNewDate As Date) As Boolean
        '
        Dim SQL As New SQLControl
        SQL.AddParam("@IDTransacao", myIDTransacao)
        SQL.AddParam("@TransacaoData", myNewDate)
        '
        Dim myQuery As String = "UPDATE tblTransacao SET TransacaoData = @TransacaoData WHERE IDTransacao = @IDTransacao"
        '
        Try
            SQL.ExecQuery(myQuery)
            '
            '--- verifica erro
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            Else
                Return True
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
End Class
