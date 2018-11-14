Imports CamadaDTO
Imports CamadaDAL
Imports System.Data.SqlClient

Public Class SimplesMovimentacaoBLL
    '
    '--------------------------------------------------------------------------------------------
    ' INSERT NOVA SIMPLES SAIDA E RETORNA UMA CLSIMPLESSAIDA
    '--------------------------------------------------------------------------------------------
    Public Function InsertSimplesSaida_Procedure_Classe(ByVal _simples As clSimplesSaida) As clSimplesSaida
        Try
            Dim dtVenda As DataTable
            '
            dtVenda = InsertSimplesSaida_Procedure_DT(_simples)
            If dtVenda.Rows.Count > 0 Then
                Dim r As DataRow = dtVenda(0)
                '
                Return ConvertDtRow_Classe(r)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' INSERT SIMPLES SAIDA E RETORNA UM DATATABLE
    '--------------------------------------------------------------------------------------------
    Public Function InsertSimplesSaida_Procedure_DT(ByVal _sim As clSimplesSaida) As DataTable
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'Adiciona os Parâmetros
        objDB.LimparParametros()
        '
        '-- PARAMETROS DA TBLTRANSACAO
        '@IDPessoaDestino AS INT, x
        objDB.AdicionarParametros("@IDPessoaDestino", _sim.IDPessoaDestino)
        '@IDPessoaOrigem AS INT, x
        objDB.AdicionarParametros("@IDPessoaOrigem", _sim.IDPessoaOrigem)
        '@IDUser AS INT, x
        objDB.AdicionarParametros("@IDUser", _sim.IDUser)
        '@VendaData AS SMALLDATETIME, x
        objDB.AdicionarParametros("@TransacaoData", _sim.TransacaoData)
        '
        Try
            Dim dtV As DataTable = objDB.ExecutarConsulta(CommandType.StoredProcedure, "uspSimplesSaida_Inserir")
            If dtV.Rows.Count = 0 Then
                Throw New Exception("Um erro ineperado ocorreu na uspVenda_Inserir")
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
    ' UPDATE SIMPLES SAIDA E RETORNA UMA CLSIMPLESSAIDA
    '--------------------------------------------------------------------------------------------
    Public Function AtualizaSimplesSaida_Procedure_DT(ByVal _sim As clSimplesSaida) As Integer
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'Adiciona os Parâmetros
        objDB.LimparParametros()
        '
        '-- PARAMETROS DA TBLTRANSACAO
        objDB.AdicionarParametros("@IDTransacao", _sim.IDTransacao)
        objDB.AdicionarParametros("@IDPessoaDestino", _sim.IDPessoaDestino)
        objDB.AdicionarParametros("@IDPessoaOrigem", _sim.IDPessoaOrigem)
        objDB.AdicionarParametros("@IDSituacao", _sim.IDSituacao)
        objDB.AdicionarParametros("@IDUser", _sim.IDUser)
        objDB.AdicionarParametros("@TransacaoData", _sim.TransacaoData)
        objDB.AdicionarParametros("@ValorTotal", _sim.ValorTotal)
        objDB.AdicionarParametros("@Observacao", _sim.Observacao)
        objDB.AdicionarParametros("@IDPlano", _sim.IDPlano)
        objDB.AdicionarParametros("@ArquivoGerado", _sim.ArquivoGerado)
        objDB.AdicionarParametros("@ArquivoRecebido", _sim.ArquivoRecebido)
        '
        Try
            Dim dtV As DataTable = objDB.ExecutarConsulta(CommandType.StoredProcedure, "uspSimplesSaida_Alterar")
            If dtV.Rows.Count = 0 Then
                Throw New Exception("Um erro ineperado ocorreu na uspVenda_Inserir")
            End If
            '
            Dim obj As Object = dtV.Rows(0).Item(0)
            '
            If IsNumeric(obj) Then
                Return obj
            Else
                Throw New Exception(obj)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' CONVERT DATAROW DA DATATABLE qrySimplesSaida EM UM clSimplesSaida
    '--------------------------------------------------------------------------------------------
    Private Function ConvertDtRow_Classe(r As DataRow) As clSimplesSaida
        '
        Dim sim As New clSimplesSaida
        '
        sim.IDTransacao = IIf(IsDBNull(r("IDTransacao")), Nothing, r("IDTransacao"))
        sim.ValorTotal = IIf(IsDBNull(r("ValorTotal")), 0, r("ValorTotal"))
        sim.ArquivoGerado = IIf(IsDBNull(r("ArquivoGerado")), Nothing, r("ArquivoGerado"))
        sim.ArquivoRecebido = IIf(IsDBNull(r("ArquivoRecebido")), Nothing, r("ArquivoRecebido"))
        sim.IDPessoaDestino = IIf(IsDBNull(r("IDPessoaDestino")), Nothing, r("IDPessoaDestino"))
        sim.PessoaDestino = IIf(IsDBNull(r("PessoaDestino")), String.Empty, r("PessoaDestino"))
        sim.IDPessoaOrigem = IIf(IsDBNull(r("IDPessoaOrigem")), Nothing, r("IDPessoaOrigem"))
        sim.PessoaOrigem = IIf(IsDBNull(r("PessoaOrigem")), String.Empty, r("PessoaOrigem"))
        sim.IDOperacao = IIf(IsDBNull(r("IDOperacao")), Nothing, r("IDOperacao"))
        sim.IDSituacao = IIf(IsDBNull(r("IDSituacao")), Nothing, r("IDSituacao"))
        sim.Situacao = IIf(IsDBNull(r("Situacao")), String.Empty, r("Situacao"))
        sim.IDUser = IIf(IsDBNull(r("IDUser")), Nothing, r("IDUser"))
        sim.CFOP = IIf(IsDBNull(r("CFOP")), String.Empty, r("CFOP"))
        sim.TransacaoData = IIf(IsDBNull(r("TransacaoData")), Nothing, r("TransacaoData"))
        sim.IDAReceber = IIf(IsDBNull(r("IDAReceber")), Nothing, r("IDAReceber"))
        sim.SituacaoAReceber = IIf(IsDBNull(r("SituacaoAReceber")), Nothing, r("SituacaoAReceber"))
        sim.ValorPagoTotal = IIf(IsDBNull(r("ValorPagoTotal")), 0, r("ValorPagoTotal"))
        sim.IDCobrancaForma = IIf(IsDBNull(r("IDCobrancaForma")), Nothing, r("IDCobrancaForma"))
        sim.IDPlano = IIf(IsDBNull(r("IDPlano")), Nothing, r("IDPlano"))
        sim.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
        '
        Return sim
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' GET LISTA SIMPLES SAIDA PARA FRMPROCURA RETORNA LIST OF clSimplesSaida
    '--------------------------------------------------------------------------------------------
    Public Function GetSimplesSaidaLista_Procura(Optional dtInicial As Date? = Nothing,
                                                 Optional dtFinal As Date? = Nothing) As List(Of clSimplesSaida)
        '
        Dim sql As New SQLControl
        '
        Dim myQuery As String = "SELECT * FROM qrySimplesSaida"
        '
        If Not IsNothing(dtInicial) Then
            sql.AddParam("@DataInicial", dtInicial)

            myQuery = myQuery & " WHERE TransacaoData >= @DataInicial"
        End If
        '
        If Not IsNothing(dtFinal) Then
            sql.AddParam("@DataFinal", dtFinal)

            If Not IsNothing(dtInicial) Then
                myQuery = myQuery & " AND TransacaoData <= @DataFinal"
            Else
                myQuery = myQuery & " WHERE TransacaoData <= @DataFinal"
            End If
            '
        End If
        '
        Try
            Dim sList As New List(Of clSimplesSaida)
            '
            sql.ExecQuery(myQuery)
            '
            If sql.HasException Then
                Throw New Exception(sql.Exception)
            End If
            '
            If sql.DBDT.Rows.Count = 0 Then Return sList
            '
            For Each r As DataRow In sql.DBDT.Rows
                Dim sim As New clSimplesSaida
                '
                sim = ConvertDtRow_Classe(r)
                '
                sList.Add(sim)
                '
            Next
            '
            Return sList
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
End Class
