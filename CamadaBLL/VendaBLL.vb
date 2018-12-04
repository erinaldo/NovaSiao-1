﻿Imports CamadaDTO
Imports CamadaDAL
Imports System.Data.SqlClient

Public Class VendaBLL
    '
    '--------------------------------------------------------------------------------------------
    ' GET DATATABLE COM WHERE
    '--------------------------------------------------------------------------------------------
    Public Function GetVendas_DT(Optional myWhere As String = "") As DataTable
        Dim objdb As New AcessoDados
        Dim dt As New DataTable
        Dim strSql As String
        '
        If Len(myWhere) = 0 Then
            strSql = "SELECT * FROM qryVendas"
        Else
            strSql = "SELECT * FROM qryVendas WHERE " & myWhere
        End If
        '
        Try
            dt = objdb.ExecuteConsultaSQL_DataTable(strSql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' GET LIST COM WHERE
    '--------------------------------------------------------------------------------------------
    Public Function GetVendas_List(Optional myWhere As String = "") As List(Of clVenda)
        Dim objdb As New AcessoDados
        Dim strSql As String = ""
        If Len(myWhere) = 0 Then
            strSql = "SELECT * FROM qryVenda"
        Else
            strSql = "SELECT * FROM qryVenda WHERE " & myWhere
        End If
        '
        Try
            Dim dt As DataTable = objdb.ExecuteConsultaSQL_DataTable(strSql)
            Dim lista As New List(Of clVenda)
            '
            If dt.Rows.Count = 0 Then Return lista
            '
            For Each r As DataRow In dt.Rows
                Dim vnd As clVenda = New clVenda
                vnd = ConvertDtRow_clVenda(r)
                lista.Add(vnd)

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
    Public Function GetVenda_PorID_OBJ(ByVal myIDVenda As Integer) As clVenda
        Dim objdb As New AcessoDados
        Dim strSql As String = ""
        strSql = "SELECT * FROM qryVenda WHERE IDVenda = " & myIDVenda
        '
        Try
            Dim dt As DataTable = objdb.ExecuteConsultaSQL_DataTable(strSql)
            Dim r As DataRow = Nothing
            '
            If dt.Rows.Count > 0 Then
                r = dt.Rows(0)
                Return ConvertDtRow_clVenda(r)
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
    Public Function AtualizaVenda_Procedure_ID(ByVal _vnd As clVenda) As String
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'Limpa os Parâmetros
        objDB.LimparParametros()
        '
        '-- PARAMETROS DA TBLTRANSACAO
        '@IDVenda AS INT
        objDB.AdicionarParametros("@IDVenda", _vnd.IDVenda)
        '@IDPessoaDestino AS INT, 
        objDB.AdicionarParametros("@IDPessoaDestino", _vnd.IDPessoaDestino)
        '@IDPessoaOrigem AS INT, 
        objDB.AdicionarParametros("@IDPessoaOrigem", _vnd.IDPessoaOrigem)
        '@IDOperacao AS BYTE, 
        objDB.AdicionarParametros("@IDOperacao", _vnd.IDOperacao)
        '@IDSituacao AS TINYINT = 0, --0|INSERIDA ; 1|VERIFICADA ; 2|FECHADA 
        objDB.AdicionarParametros("@IDSituacao", _vnd.IDSituacao)
        '@IDUser AS INT,
        objDB.AdicionarParametros("@IDUser", _vnd.IDUser)
        '@CFOP AS INT(16), 
        objDB.AdicionarParametros("@CFOP", _vnd.CFOP)
        '@VendaData AS SMALLDATETIME, 
        objDB.AdicionarParametros("@TransacaoData", _vnd.TransacaoData)
        '
        '-- PARAMETROS DA TBLVENDA
        '@IDDepartamento AS SMALLINT = 1,
        objDB.AdicionarParametros("@IDDepartamento", _vnd.IDDepartamento)
        '@IDVendedor AS INT,
        objDB.AdicionarParametros("@IDVendedor", _vnd.IDVendedor)
        '@CobrancaTipo AS TINYINT, 
        objDB.AdicionarParametros("@CobrancaTipo", _vnd.CobrancaTipo)
        '@AgregaDevolucao AS BIT, 
        objDB.AdicionarParametros("@AgregaDevolucao", _vnd.AgregaDevolucao)
        '@ValorProdutos AS MONEY
        objDB.AdicionarParametros("@ValorProdutos", _vnd.ValorProdutos)
        '@ValorFrete AS MONEY -- Valor do Frete a ser cobrado na Venda
        objDB.AdicionarParametros("@ValorFrete", _vnd.ValorFrete)
        '@ValorImpostos AS MONEY -- Valor dos Impostos a ser cobrados
        objDB.AdicionarParametros("@ValorImpostos", _vnd.ValorImpostos)
        '@ValorAcrescimos AS MONEY -- Valor dos outros acrescimos
        objDB.AdicionarParametros("@ValorAcrescimos", _vnd.ValorAcrescimos)
        '@ValorDevoucao AS MONEY -- Valor das devolucao a ser abatida
        objDB.AdicionarParametros("@ValorDevolucao", _vnd.ValorDevolucao)
        '@TotalVenda AS MONEY -- Valor total da Venda
        objDB.AdicionarParametros("@TotalVenda", _vnd.TotalVenda)
        '@JurosMes AS DECIMAL(6,2), 
        objDB.AdicionarParametros("@JurosMes", _vnd.JurosMes)
        '@Observacao AS VARCHAR(max) = null, 
        objDB.AdicionarParametros("@Observacao", _vnd.Observacao)
        '@VendaTipo AS TINYINT = 0, --0|VAREJO ; 1|ATACADO
        objDB.AdicionarParametros("@IDVendaTipo", _vnd.IDVendaTipo)
        '
        '-- PARAMETROS DA TBLARECEBER
        '@IDCobrancaForma AS SMALLINT, 
        objDB.AdicionarParametros("@IDCobrancaForma", _vnd.IDCobrancaForma)
        '@IDPlano SMALLINT = NULL, 
        objDB.AdicionarParametros("@IDPlano", _vnd.IDPlano)
        '
        '-- PARAMETROS DA TBLVENDAFRETE
        '@IDTransportadora AS INT = NULL,
        objDB.AdicionarParametros("@IDTransportadora", _vnd.IDTransportadora)
        '@FreteTipo AS TINYINT = 0, -- 1|EMITENTE; 2|DESTINATARIO
        objDB.AdicionarParametros("@FreteTipo", _vnd.FreteTipo)
        '@FreteValor AS MONEY = 0,
        objDB.AdicionarParametros("@FreteValor", _vnd.FreteValor)
        '@Volumes AS SMALLINT = 1,
        objDB.AdicionarParametros("@Volumes", _vnd.Volumes)
        '@IDAPagar AS INT = NULL
        objDB.AdicionarParametros("@IDAPagar", _vnd.IDApagar)
        '
        '
        Try
            Return objDB.ExecutarManipulacao(CommandType.StoredProcedure, "uspVenda_Alterar")
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' INSERT NOVA VENDA E RETORNA UMA CLVENDA
    '--------------------------------------------------------------------------------------------
    Public Function SalvaNovaVenda_Procedure_Venda(ByVal _venda As clVenda) As clVenda
        Try
            Dim dtVenda As DataTable
            '
            dtVenda = SalvaNovaVenda_Procedure_DT(_venda)
            If dtVenda.Rows.Count > 0 Then
                Dim r As DataRow = dtVenda(0)
                '
                Return ConvertDtRow_clVenda(r)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' INSERT NOVA VENDA E RETORNA UM DATATABLE
    '--------------------------------------------------------------------------------------------
    Public Function SalvaNovaVenda_Procedure_DT(ByVal _vnd As clVenda) As DataTable
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'Adiciona os Parâmetros
        objDB.LimparParametros()
        '
        '-- PARAMETROS DA TBLTRANSACAO
        '@IDPessoaDestino AS INT, x
        objDB.AdicionarParametros("@IDPessoaDestino", _vnd.IDPessoaDestino)
        '@IDPessoaOrigem AS INT, x
        objDB.AdicionarParametros("@IDPessoaOrigem", _vnd.IDPessoaOrigem)
        '@IDOperacao AS BYTE,  x
        objDB.AdicionarParametros("@IDOperacao", _vnd.IDOperacao)
        '@IDSituacao AS TINYINT = 0, --0|INSERIDA ; 1|VERIFICADA ; 2|FECHADA  x
        objDB.AdicionarParametros("@IDSituacao", _vnd.IDSituacao)
        '@IDUser AS INT, x
        objDB.AdicionarParametros("@IDUser", _vnd.IDUser)
        '@CFOP AS INT(16), x
        objDB.AdicionarParametros("@CFOP", _vnd.CFOP)
        '@VendaData AS SMALLDATETIME, x
        objDB.AdicionarParametros("@TransacaoData", _vnd.TransacaoData)
        '
        '-- PARAMETROS DA TBLVENDA
        '@IDDepartamento AS SMALLINT = 1, x
        objDB.AdicionarParametros("@IDDepartamento", _vnd.IDDepartamento)
        '@IDVendedor AS INT, x
        objDB.AdicionarParametros("@IDVendedor", _vnd.IDVendedor)
        '@CobrancaTipo AS TINYINT,  x
        objDB.AdicionarParametros("@CobrancaTipo", _vnd.CobrancaTipo)
        '@IDPlano SMALLINT = NULL, x
        objDB.AdicionarParametros("@IDPlano", _vnd.IDPlano)
        '@JurosMes AS DECIMAL(6,2), x
        objDB.AdicionarParametros("@JurosMes", _vnd.JurosMes)
        '@Observacao AS VARCHAR(max) = null, x
        objDB.AdicionarParametros("@Observacao", _vnd.Observacao)
        '@VendaTipo AS TINYINT = 0, --0|VAREJO ; 1|ATACADO x
        objDB.AdicionarParametros("@IDVendaTipo", _vnd.IDVendaTipo)
        '@ValorProdutos AS MONEY x
        objDB.AdicionarParametros("@ValorProdutos", _vnd.ValorProdutos)
        '@ValorFrete AS MONEY -- Valor do Frete a ser cobrado na Venda x
        objDB.AdicionarParametros("@ValorFrete", _vnd.ValorFrete)
        '@ValorImpostos AS MONEY -- Valor dos Impostos a ser cobrados x
        objDB.AdicionarParametros("@ValorImpostos", _vnd.ValorImpostos)
        '@ValorAcrescimos AS MONEY -- Valor dos outros acrescimos x
        objDB.AdicionarParametros("@ValorAcrescimos", _vnd.ValorAcrescimos)
        '@ValorDevolucao AS MONEY -- Valor da Troca|Devolucao x
        objDB.AdicionarParametros("@ValorDevolucao", _vnd.ValorDevolucao)
        '
        '-- PARAMETROS DA TBLVENDAFRETE
        '@IDTransportadora AS INT = NULL,
        objDB.AdicionarParametros("@IDTransportadora", _vnd.IDTransportadora)
        '@FreteValor AS MONEY = 0,
        objDB.AdicionarParametros("@FreteValor", _vnd.FreteValor)
        '@FreteTipo AS TINYINT = 0, -- 1|EMITENTE; 2|DESTINATARIO 
        objDB.AdicionarParametros("@FreteTipo", _vnd.FreteTipo)
        '@Volumes AS SMALLINT = 1,
        objDB.AdicionarParametros("@Volumes", _vnd.Volumes)
        '@IDAPagar AS INT = NULL
        objDB.AdicionarParametros("@IDAPagar", _vnd.IDApagar)
        '
        Try
            Dim dtV As DataTable = objDB.ExecutarConsulta(CommandType.StoredProcedure, "uspVenda_Inserir")
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
    ' DELETE VENDA POR IDVENDA
    '--------------------------------------------------------------------------------------------
    Public Function DeletaVendaPorID(ByVal IDVenda As Integer)

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
    ' CONVERT DATAROW DA DATATABLE VENDA EM UM CLVENDA 
    '--------------------------------------------------------------------------------------------
    Private Function ConvertDtRow_clVenda(r As DataRow) As clVenda
        '
        Dim vnd As clVenda = New clVenda
        '
        vnd.IDVenda = IIf(IsDBNull(r("IDVenda")), Nothing, r("IDVenda"))
        vnd.IDPessoaDestino = IIf(IsDBNull(r("IDPessoaDestino")), Nothing, r("IDPessoaDestino"))
        vnd.Cadastro = IIf(IsDBNull(r("Cadastro")), String.Empty, r("Cadastro"))
        vnd.CNP = IIf(IsDBNull(r("CNP")), String.Empty, r("CNP"))
        vnd.UF = IIf(IsDBNull(r("UF")), String.Empty, r("UF"))
        vnd.Cidade = IIf(IsDBNull(r("Cidade")), String.Empty, r("Cidade"))
        vnd.IDPessoaOrigem = IIf(IsDBNull(r("IDPessoaOrigem")), Nothing, r("IDPessoaOrigem"))
        vnd.ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
        vnd.IDOperacao = IIf(IsDBNull(r("IDOperacao")), Nothing, r("IDOperacao"))
        vnd.IDSituacao = IIf(IsDBNull(r("IDSituacao")), Nothing, r("IDSituacao"))
        vnd.Situacao = IIf(IsDBNull(r("Situacao")), String.Empty, r("Situacao"))
        vnd.IDUser = IIf(IsDBNull(r("IDUser")), Nothing, r("IDUser"))
        vnd.CFOP = IIf(IsDBNull(r("CFOP")), String.Empty, r("CFOP"))
        vnd.TransacaoData = IIf(IsDBNull(r("TransacaoData")), Nothing, r("TransacaoData"))
        vnd.IDDepartamento = IIf(IsDBNull(r("IDDepartamento")), Nothing, r("IDDepartamento"))
        vnd.IDVendedor = IIf(IsDBNull(r("IDVendedor")), Nothing, r("IDVendedor"))
        vnd.CobrancaTipo = IIf(IsDBNull(r("CobrancaTipo")), Nothing, r("CobrancaTipo"))
        vnd.AgregaDevolucao = IIf(IsDBNull(r("AgregaDevolucao")), False, r("AgregaDevolucao"))
        vnd.ValorProdutos = IIf(IsDBNull(r("ValorProdutos")), 0, r("ValorProdutos"))
        vnd.ValorFrete = IIf(IsDBNull(r("ValorFrete")), 0, r("ValorFrete"))
        vnd.ValorImpostos = IIf(IsDBNull(r("ValorImpostos")), 0, r("ValorImpostos"))
        vnd.ValorAcrescimos = IIf(IsDBNull(r("ValorAcrescimos")), 0, r("ValorAcrescimos"))
        vnd.ValorDevolucao = IIf(IsDBNull(r("ValorDevolucao")), 0, r("ValorDevolucao"))
        vnd.JurosMes = IIf(IsDBNull(r("JurosMes")), Nothing, r("JurosMes"))
        vnd.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
        vnd.IDVendaTipo = IIf(IsDBNull(r("IDVendaTipo")), Nothing, r("IDVendaTipo"))
        '--- Dados do tblAReceber
        vnd.IDAReceber = IIf(IsDBNull(r("IDAReceber")), Nothing, r("IDAReceber"))
        vnd.SituacaoAReceber = IIf(IsDBNull(r("SituacaoAReceber")), Nothing, r("SituacaoAReceber"))
        vnd.IDPlano = IIf(IsDBNull(r("IDPlano")), Nothing, r("IDPlano"))
        vnd.IDCobrancaForma = IIf(IsDBNull(r("IDCobrancaForma")), Nothing, r("IDCobrancaForma"))
        vnd.CobrancaForma = IIf(IsDBNull(r("CobrancaForma")), String.Empty, r("CobrancaForma"))
        vnd.ValorPagoTotal = IIf(IsDBNull(r("ValorPagoTotal")), Nothing, r("ValorPagoTotal"))
        '--- Dados da tblVendaFrete
        vnd.IDTransportadora = IIf(IsDBNull(r("IDTransportadora")), Nothing, r("IDTransportadora"))
        vnd.FreteTipo = IIf(IsDBNull(r("FreteTipo")), Nothing, r("FreteTipo"))
        vnd.FreteValor = IIf(IsDBNull(r("FreteValor")), Nothing, r("FreteValor"))
        vnd.Volumes = IIf(IsDBNull(r("Volumes")), Nothing, r("Volumes"))
        vnd.IDApagar = IIf(IsDBNull(r("IDApagar")), Nothing, r("IDApagar"))
        '--- Dados Adicionais
        vnd.ApelidoFuncionario = IIf(IsDBNull(r("ApelidoFuncionario")), String.Empty, r("ApelidoFuncionario"))
        '
        Return vnd
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' GET LISTA VENDAS PARA FRMPROCURA RETORNA LIST OF CLVENDA
    '--------------------------------------------------------------------------------------------
    Public Function GetVendaLista_Procura(IDFilial As Integer,
                                          Optional dtInicial As Date? = Nothing,
                                          Optional dtFinal As Date? = Nothing) As List(Of clVenda)
        '
        Dim sql As New SQLControl
        '
        sql.AddParam("@IDFilial", IDFilial)
        Dim myQuery As String = "SELECT * FROM qryVenda WHERE IDPessoaOrigem = @IDFilial"
        '

        If Not IsNothing(dtInicial) Then
            '
            sql.AddParam("@DataInicial", dtInicial)
            myQuery = myQuery & " AND TransacaoData >= @DataInicial"
            '
        End If
        '
        If Not IsNothing(dtFinal) Then
            '
            sql.AddParam("@DataFinal", dtFinal)
            myQuery = myQuery & " AND TransacaoData <= @DataFinal"
            '
        End If
        '
        Try
            Dim vndList As New List(Of clVenda)
            '
            sql.ExecQuery(myQuery)
            '
            If sql.HasException Then
                Throw New Exception(sql.Exception)
            End If
            '
            If sql.DBDT.Rows.Count = 0 Then Return vndList
            '
            For Each r As DataRow In sql.DBDT.Rows
                Dim vnd As New clVenda
                '
                vnd = ConvertDtRow_clVenda(r)
                '
                vndList.Add(vnd)
                '
            Next
            '
            Return vndList
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' GET VENDA TIPOS DATATABLE COM OPTIONAL IDVENDATIPO
    '--------------------------------------------------------------------------------------------
    Public Function GetVendaTipo_DT(Optional IDVendaTipo As Byte? = Nothing) As DataTable
        '
        Dim objdb As New AcessoDados
        Dim dt As New DataTable
        Dim strSql As String
        '
        strSql = "SELECT * FROM tblVendaTipo"
        '
        If Not IsNothing(IDVendaTipo) Then
            strSql = strSql & " WHERE IDVendaTipo = " & IDVendaTipo
        End If
        '
        Try
            dt = objdb.ExecuteConsultaSQL_DataTable(strSql)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--------------------------------------------------------------------------------------------
    ' UPDATE A FUNCIONARIO / VENDEDOR DA VENDA
    '--------------------------------------------------------------------------------------------
    Public Function AtualizaVendaVendedor(myIDVenda As Integer, NewIDVendedor As Integer) As Boolean
        '
        Dim SQL As New SQLControl
        SQL.AddParam("@IDVenda", myIDVenda)
        SQL.AddParam("@IDVendedor", NewIDVendedor)
        '
        Dim myQuery As String = "UPDATE tblVenda SET IDVendedor = @IDVendedor WHERE IDVenda = @IDVenda"
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