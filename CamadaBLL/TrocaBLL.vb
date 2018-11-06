Imports CamadaDTO
Imports CamadaDAL
Imports System.Data.SqlClient
'
Public Class TrocaBLL
    '
    '--------------------------------------------------------------------------------------------
    ' GET LIST COM WHERE
    '--------------------------------------------------------------------------------------------
    Public Function GetTroca_List(Optional myWhere As String = "") As List(Of clTroca)
        '
        Dim objdb As New AcessoDados
        Dim strSql As String = ""
        '
        If Len(myWhere) = 0 Then
            strSql = "SELECT * FROM qryTroca"
        Else
            strSql = "SELECT * FROM qryTroca WHERE " & myWhere
        End If
        '
        Try
            Dim dt As DataTable = objdb.ExecuteConsultaSQL_DataTable(strSql)
            Dim lista As New List(Of clTroca)
            '
            If dt.Rows.Count = 0 Then Return lista
            '
            For Each r As DataRow In dt.Rows
                Dim trc As clTroca
                trc = ConvertDtRow_clTroca(r)
                lista.Add(trc)

            Next
            '
            Return lista
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' GET REGISTRO POR ID/RG
    '--------------------------------------------------------------------------------------------
    Public Function GetTroca_PorID_OBJ(ByVal myIDTroca As Integer) As clTroca
        Dim objdb As New AcessoDados
        Dim strSql As String = ""
        '
        strSql = "SELECT * FROM qryTroca WHERE IDTroca = " & myIDTroca
        '
        Try
            Dim dt As DataTable = objdb.ExecuteConsultaSQL_DataTable(strSql)
            Dim r As DataRow = Nothing
            '
            If dt.Rows.Count > 0 Then
                r = dt.Rows(0)
                Return ConvertDtRow_clTroca(r)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' UPDATE
    '--------------------------------------------------------------------------------------------
    Public Function AtualizaTroca_Procedure_ID(ByVal _troca As clTroca) As String
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'Limpa os Parâmetros
        objDB.LimparParametros()
        '
        '-- PARAMETROS DA TBLTRANSACAO
        ''@IDVenda AS INT
        'objDB.AdicionarParametros("@IDVenda", _troca.IDVenda)
        ''@IDPessoaDestino AS INT, 
        'objDB.AdicionarParametros("@IDPessoaDestino", _troca.IDPessoaDestino)
        ''@IDPessoaOrigem AS INT, 
        'objDB.AdicionarParametros("@IDPessoaOrigem", _troca.IDPessoaOrigem)
        ''@IDOperacao AS BYTE, 
        'objDB.AdicionarParametros("@IDOperacao", _troca.IDOperacao)
        ''@IDSituacao AS TINYINT = 0, --0|INSERIDA ; 1|VERIFICADA ; 2|FECHADA 
        'objDB.AdicionarParametros("@IDSituacao", _troca.IDSituacao)
        ''@IDUser AS INT,
        'objDB.AdicionarParametros("@IDUser", _troca.IDUser)
        ''@CFOP AS INT(16), 
        'objDB.AdicionarParametros("@CFOP", _troca.CFOP)
        ''@VendaData AS SMALLDATETIME, 
        'objDB.AdicionarParametros("@TransacaoData", _troca.TransacaoData)
        ''
        ''-- PARAMETROS DA TBLVENDA
        ''@IDDepartamento AS SMALLINT = 1,
        'objDB.AdicionarParametros("@IDDepartamento", _troca.IDDepartamento)
        ''@IDVendedor AS INT,
        'objDB.AdicionarParametros("@IDVendedor", _troca.IDVendedor)
        ''@CobrancaTipo AS TINYINT, 
        'objDB.AdicionarParametros("@CobrancaTipo", _troca.CobrancaTipo)
        ''@ValorProdutos AS MONEY
        'objDB.AdicionarParametros("@ValorProdutos", _troca.ValorProdutos)
        ''@ValorFrete AS MONEY -- Valor do Frete a ser cobrado na Venda
        'objDB.AdicionarParametros("@ValorFrete", _troca.ValorFrete)
        ''@ValorImpostos AS MONEY -- Valor dos Impostos a ser cobrados
        'objDB.AdicionarParametros("@ValorImpostos", _troca.ValorImpostos)
        ''@ValorAcrescimos AS MONEY -- Valor dos outros acrescimos
        'objDB.AdicionarParametros("@ValorAcrescimos", _troca.ValorAcrescimos)
        ''@JurosMes AS DECIMAL(6,2), 
        'objDB.AdicionarParametros("@JurosMes", _troca.JurosMes)
        ''@Observacao AS VARCHAR(max) = null, 
        'objDB.AdicionarParametros("@Observacao", _troca.Observacao)
        ''@VendaTipo AS TINYINT = 0, --0|VAREJO ; 1|ATACADO
        'objDB.AdicionarParametros("@VendaTipo", _troca.VendaTipo)
        ''
        ''-- PARAMETROS DA TBLARECEBER
        ''@IDCobrancaForma AS SMALLINT, 
        'objDB.AdicionarParametros("@IDCobrancaForma", _troca.IDCobrancaForma)
        ''@IDPlano SMALLINT = NULL, 
        'objDB.AdicionarParametros("@IDPlano", _troca.IDPlano)
        ''
        ''-- PARAMETROS DA TBLVENDAFRETE
        ''@IDTransportadora AS INT = NULL,
        'objDB.AdicionarParametros("@IDTransportadora", _troca.IDTransportadora)
        ''@FreteTipo AS TINYINT = 0, -- 1|EMITENTE; 2|DESTINATARIO
        'objDB.AdicionarParametros("@FreteTipo", _troca.FreteTipo)
        ''@FreteValor AS MONEY = 0,
        'objDB.AdicionarParametros("@FreteValor", _troca.FreteValor)
        ''@Volumes AS SMALLINT = 1,
        'objDB.AdicionarParametros("@Volumes", _troca.Volumes)
        ''@IDAPagar AS INT = NULL
        'objDB.AdicionarParametros("@IDAPagar", _troca.IDApagar)
        '
        '
        Try
            Return objDB.ExecutarManipulacao(CommandType.StoredProcedure, "")
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' INSERT NOVA TROCA E RETORNA UMA CLTROCA
    '--------------------------------------------------------------------------------------------
    Public Function InsereNovaTroca_Procedure(ByVal _troca As clTroca, _IDUser As Integer) As clTroca
        '
        Try
            Dim dtTroca As DataTable
            '
            dtTroca = InsereNovaTroca_Procedure_DT(_troca, _IDUser)
            If dtTroca.Rows.Count > 0 Then
                Dim r As DataRow = dtTroca(0)
                '
                Return ConvertDtRow_clTroca(r)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' INSERT NOVA TROCA E RETORNA UM DATATABLE
    '--------------------------------------------------------------------------------------------
    Public Function InsereNovaTroca_Procedure_DT(ByVal _troca As clTroca, _IDUser As Integer) As DataTable
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'Adiciona os Parâmetros
        objDB.LimparParametros()
        '
        '@IDUser AS INT
        objDB.AdicionarParametros("@IDUser", _IDUser)
        '
        '@TrocaData AS DATE
        objDB.AdicionarParametros("@TrocaData", _troca.TrocaData)
        '
        '@IDPessoaTroca AS INT
        objDB.AdicionarParametros("@IDPessoaTroca", _troca.IDPessoaTroca)
        '
        '@IDFilial AS INT
        objDB.AdicionarParametros("@IDFilial", _troca.IDFilial)
        '
        '@IDVendedor AS INT
        objDB.AdicionarParametros("@IDVendedor", _troca.IDVendedor)
        '
        '
        Try
            Dim dtV As DataTable = objDB.ExecutarConsulta(CommandType.StoredProcedure, "uspTroca_Inserir")
            '
            If dtV.Rows.Count = 0 Then
                Throw New Exception("Um erro ineperado ocorreu ao INSERIR NOVA TROCA")
            End If
            '
            If IsNumeric(dtV.Rows(0).Item(0)) Then
                Return dtV
            Else
                Throw New Exception(dtV.Rows(0).Item(0))
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' DELETE TROCA POR IDTROCA
    '--------------------------------------------------------------------------------------------
    Public Function DeletaTrocaPorID(ByVal IDTroca As Integer)

        Dim strSql As String
        Dim objDB As AcessoDados
        '--- Verificar se a venda já foi incluída no Caixa Geral

        '--- Apagar todos os itens da Venda

        '--- Apagar todos os AReceber relacionadas à Venda

        '--- Apagar o tblVendaFrete associado

        '--- Apagar a Venda em si
        strSql = "" '"DELETE FROM tblVenda where IDVenda=" & _IDVenda
        '
        objDB = New AcessoDados
        Try
            objDB.ExecuteQuery(strSql)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try

        Return True
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' CONVERT DATAROW DA DATATABLE TROCA EM UM CLTROCA
    '--------------------------------------------------------------------------------------------
    Private Function ConvertDtRow_clTroca(r As DataRow) As clTroca
        '
        Dim t As clTroca = New clTroca
        '
        t.IDTroca = IIf(IsDBNull(r("IDTroca")), Nothing, r("IDTroca"))
        t.TrocaData = IIf(IsDBNull(r("TrocaData")), Nothing, r("TrocaData"))
        t.IDFilial = IIf(IsDBNull(r("IDFilial")), Nothing, r("IDFilial"))
        t.ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
        t.IDPessoaTroca = IIf(IsDBNull(r("IDPessoaTroca")), Nothing, r("IDPessoaTroca"))
        t.PessoaTroca = IIf(IsDBNull(r("PessoaTroca")), String.Empty, r("PessoaTroca"))
        t.IDVendedor = IIf(IsDBNull(r("IDVendedor")), Nothing, r("IDVendedor"))
        t.ApelidoVenda = IIf(IsDBNull(r("ApelidoVenda")), String.Empty, r("ApelidoVenda"))
        t.IDTransacaoEntrada = IIf(IsDBNull(r("IDTransacaoEntrada")), Nothing, r("IDTransacaoEntrada"))
        t.IDTransacaoSaida = IIf(IsDBNull(r("IDTransacaoSaida")), Nothing, r("IDTransacaoSaida"))
        t.CobrancaTipo = IIf(IsDBNull(r("CobrancaTipo")), Nothing, r("CobrancaTipo"))
        t.CobrancaTipoTexto = IIf(IsDBNull(r("CobrancaTipoTexto")), String.Empty, r("CobrancaTipoTexto"))
        t.ValorEntrada = IIf(IsDBNull(r("ValorEntrada")), Nothing, r("ValorEntrada"))
        t.ValorSaida = IIf(IsDBNull(r("ValorSaida")), Nothing, r("ValorSaida"))
        t.ValorAcrescimos = IIf(IsDBNull(r("ValorAcrescimos")), Nothing, r("ValorAcrescimos"))
        't.TotalTroca = IIf(IsDBNull(r("TotalTroca")), Nothing, r("TotalTroca"))
        t.JurosMes = IIf(IsDBNull(r("JurosMes")), Nothing, r("JurosMes"))
        t.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
        t.IDSituacao = IIf(IsDBNull(r("IDSituacao")), Nothing, r("IDSituacao"))
        t.Situacao = IIf(IsDBNull(r("Situacao")), String.Empty, r("Situacao"))
        '
        '--- Dados do tblAReceber
        t.IDAReceber = IIf(IsDBNull(r("IDAReceber")), Nothing, r("IDAReceber"))
        t.SituacaoAReceber = IIf(IsDBNull(r("SituacaoAReceber")), Nothing, r("SituacaoAReceber"))
        t.IDPlano = IIf(IsDBNull(r("IDPlano")), Nothing, r("IDPlano"))
        t.IDCobrancaForma = IIf(IsDBNull(r("IDCobrancaForma")), Nothing, r("IDCobrancaForma"))
        t.CobrancaForma = IIf(IsDBNull(r("CobrancaForma")), String.Empty, r("CobrancaForma"))
        t.ValorPagoTotal = IIf(IsDBNull(r("ValorPagoTotal")), Nothing, r("ValorPagoTotal"))
        '
        Return t
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' GET LISTA TROCA PARA FRMPROCURA RETORNA LIST OF CLTROCA
    '--------------------------------------------------------------------------------------------
    Public Function GetTrocaLista_Procura(Optional dtInicial As Date? = Nothing,
                                          Optional dtFinal As Date? = Nothing) As List(Of clTroca)
        '
        Dim sql As New SQLControl
        '
        Dim myQuery As String = "SELECT * FROM qryTroca"
        '
        If Not IsNothing(dtInicial) OrElse Not IsNothing(dtFinal) Then
            '
            myQuery = myQuery & " WHERE "
            '
            If Not IsNothing(dtInicial) Then
                sql.AddParam("@DataInicial", dtInicial)
                myQuery = myQuery & " TrocaData >= @DataInicial"
            End If
            '
            If Not IsNothing(dtFinal) Then
                sql.AddParam("@DataFinal", dtFinal)
                '
                If Not IsNothing(dtInicial) Then myQuery = myQuery & " AND "
                '
                myQuery = myQuery & " TrocaData <= @DataFinal"
            End If
            '
        End If
        '
        Try
            Dim tList As New List(Of clTroca)
            '
            sql.ExecQuery(myQuery)
            '
            If sql.HasException Then
                Throw New Exception(sql.Exception)
            End If
            '
            If sql.DBDT.Rows.Count = 0 Then Return tList
            '
            For Each r As DataRow In sql.DBDT.Rows
                Dim trc As New clTroca
                '
                trc = ConvertDtRow_clTroca(r)
                '
                tList.Add(trc)
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
End Class
