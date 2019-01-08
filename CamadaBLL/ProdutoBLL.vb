Imports CamadaDTO
Imports CamadaDAL
Imports System.Data.SqlClient
'
'=================================================================================================================
' PRODUTO BLL
'=================================================================================================================
Public Class ProdutoBLL
    Implements IDisposable
    '
    '---------------------------------------------------------------------------------------------------------
    ' GET (LIST OF) COM FILTRO WHERE
    '---------------------------------------------------------------------------------------------------------
    Public Function GetProdutos_Where(Optional myWhere As String = "") As List(Of clProduto)
        '
        Dim objdb As New AcessoDados
        Dim strSql As String = ""
        '
        If Len(myWhere) = 0 Then
            strSql = "SELECT * FROM qryProdutos"
        Else
            strSql = "SELECT * FROM qryProdutos WHERE " & myWhere
        End If
        '
        Dim dr As SqlDataReader = objdb.ExecuteAndGetReader(strSql)
        Dim lista As New List(Of clProduto)
        '
        While dr.Read
            '
            '--- converte dr em clProduto
            Dim prod As clProduto = ConvertDR_To_clProduto(dr)
            '
            '--- adiciona produto na lista
            lista.Add(prod)
            '
        End While
        '
        dr.Close()
        Return lista
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' GET (LIST OF) COM ESTOQUE + FILTRO WHERE PELA FILIAL
    '---------------------------------------------------------------------------------------------------------
    Public Function GetProdutosWithEstoque_Where(myFilial As Integer, Optional myWhere As String = "") As List(Of clProduto)
        '
        Dim objdb As New AcessoDados
        Dim strSql As String = "SELECT * FROM qryProdutosEstoque WHERE IDFilial = " & myFilial
        '
        If Len(myWhere) > 0 Then
            strSql = strSql & " AND " & myWhere
        End If
        '
        Try
            '
            Dim dr As SqlDataReader = objdb.ExecuteAndGetReader(strSql)
            Dim lista As New List(Of clProduto)
            '
            While dr.Read
                '
                '--- converte dr em clProduto
                Dim prod As clProduto = ConvertDR_To_clProduto(dr)
                '
                '--- adiciona os campo do qryProdutosEstoque
                prod.Estoque = IIf(IsDBNull(dr("Estoque")), 0, dr("Estoque"))
                prod.EstoqueNivel = IIf(IsDBNull(dr("EstoqueNivel")), 0, dr("EstoqueNivel"))
                prod.EstoqueIdeal = IIf(IsDBNull(dr("EstoqueIdeal")), 0, dr("EstoqueIdeal"))
                '
                lista.Add(prod)
                '
            End While
            '
            dr.Close()
            Return lista
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' COUNT PRODUTOS FILTRO WHERE PELA FILIAL
    '---------------------------------------------------------------------------------------------------------
    Public Function CountProdutos_Where(myFilial As Integer, Optional myWhere As String = "") As Integer
        '
        Dim objdb As New AcessoDados
        Dim strSql As String = "SELECT COUNT(*) FROM qryProdutosEstoque WHERE IDFilial = " & myFilial
        '
        If Len(myWhere) > 0 Then
            strSql = strSql & " AND " & myWhere
        End If
        '
        Try
            '
            Dim DT As DataTable = objdb.ExecutarConsulta(CommandType.Text, strSql)
            '
            If DT.Rows.Count > 0 Then
                Return DT.Rows(0).Item(0)
            Else
                Return 0
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' GET DATA READER E RETURN CLASSE CLPRODUTO
    '---------------------------------------------------------------------------------------------------------
    Public Function ConvertDR_To_clProduto(dr As SqlDataReader) As clProduto
        '
        Dim prod As New clProduto With {
            .IDProduto = IIf(IsDBNull(dr("IDProduto")), 0, dr("IDProduto")),
            .RGProduto = IIf(IsDBNull(dr("RGProduto")), Nothing, dr("RGProduto")),
            .Produto = IIf(IsDBNull(dr("Produto")), String.Empty, dr("Produto")),
            .IDFabricante = IIf(IsDBNull(dr("IDFabricante")), Nothing, dr("IDFabricante")),
            .Fabricante = IIf(IsDBNull(dr("Fabricante")), String.Empty, dr("Fabricante")),
            .IDProdutoTipo = IIf(IsDBNull(dr("IDProdutoTipo")), Nothing, dr("IDProdutoTipo")),
            .ProdutoTipo = IIf(IsDBNull(dr("ProdutoTipo")), String.Empty, dr("ProdutoTipo")),
            .IDProdutoSubTipo = IIf(IsDBNull(dr("IDProdutoSubTipo")), Nothing, dr("IDProdutoSubTipo")),
            .ProdutoSubTipo = IIf(IsDBNull(dr("ProdutoSubTipo")), String.Empty, dr("ProdutoSubTipo")),
            .IDCategoria = IIf(IsDBNull(dr("IDCategoria")), Nothing, dr("IDCategoria")),
            .ProdutoCategoria = IIf(IsDBNull(dr("ProdutoCategoria")), String.Empty, dr("ProdutoCategoria")),
            .Autor = IIf(IsDBNull(dr("Autor")), String.Empty, dr("Autor")),
            .Unidade = IIf(IsDBNull(dr("Unidade")), 1, dr("Unidade")),
            .PCompra = IIf(IsDBNull(dr("PCompra")), 0, dr("PCompra")),
            .DescontoCompra = IIf(IsDBNull(dr("DescontoCompra")), Nothing, dr("DescontoCompra")),
            .PVenda = IIf(IsDBNull(dr("PVenda")), 0, dr("PVenda")),
            .ProdutoAtivo = IIf(IsDBNull(dr("ProdutoAtivo")), Nothing, dr("ProdutoAtivo")),
            .SitTributaria = IIf(IsDBNull(dr("SitTributaria")), Nothing, dr("SitTributaria")),
            .SituacaoTributaria = IIf(IsDBNull(dr("SituacaoTributaria")), String.Empty, dr("SituacaoTributaria")),
            .NCM = IIf(IsDBNull(dr("NCM")), String.Empty, dr("NCM")),
            .UltAltera = IIf(IsDBNull(dr("UltAltera")), Nothing, dr("UltAltera")),
            .EntradaData = IIf(IsDBNull(dr("EntradaData")), Nothing, dr("EntradaData")),
            .CodBarrasA = IIf(IsDBNull(dr("CodBarrasA")), String.Empty, dr("CodBarrasA")),
            .Movimento = IIf(IsDBNull(dr("Movimento")), Nothing, dr("Movimento")),
            .MovimentoDescricao = IIf(IsDBNull(dr("MovimentoDescricao")), String.Empty, dr("MovimentoDescricao"))
        }
        '
        Return prod
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' GET REGISTRO POR ID/RG
    '---------------------------------------------------------------------------------------------------------
    Public Function GetProduto_PorID(myIDProd As Integer, myFilial As Integer) As clProduto
        Dim objdb As New AcessoDados
        Dim dt As DataTable
        '
        objdb.LimparParametros()
        objdb.AdicionarParametros("@IDProduto", myIDProd)
        objdb.AdicionarParametros("@IDFilial", myFilial)
        '
        Try
            dt = objdb.ExecutarConsulta(CommandType.StoredProcedure, "uspProduto_GET_PorID")
            '
            If dt.Rows.Count = 0 Then Return New clProduto
            '
            Dim r As DataRow = dt.Rows(0)
            Dim prod As New clProduto
            '
            prod.IDProduto = IIf(IsDBNull(r("IDProduto")), 0, r("IDProduto"))
            prod.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
            prod.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
            prod.IDFabricante = IIf(IsDBNull(r("IDFabricante")), Nothing, r("IDFabricante"))
            prod.Fabricante = IIf(IsDBNull(r("Fabricante")), String.Empty, r("Fabricante"))
            prod.IDProdutoTipo = IIf(IsDBNull(r("IDProdutoTipo")), Nothing, r("IDProdutoTipo"))
            prod.ProdutoTipo = IIf(IsDBNull(r("ProdutoTipo")), String.Empty, r("ProdutoTipo"))
            prod.IDProdutoSubTipo = IIf(IsDBNull(r("IDProdutoSubTipo")), Nothing, r("IDProdutoSubTipo"))
            prod.ProdutoSubTipo = IIf(IsDBNull(r("ProdutoSubTipo")), String.Empty, r("ProdutoSubTipo"))
            prod.IDCategoria = IIf(IsDBNull(r("IDCategoria")), Nothing, r("IDCategoria"))
            prod.ProdutoCategoria = IIf(IsDBNull(r("ProdutoCategoria")), String.Empty, r("ProdutoCategoria"))
            prod.Autor = IIf(IsDBNull(r("Autor")), String.Empty, r("Autor"))
            prod.EstoqueIdeal = IIf(IsDBNull(r("EstoqueIdeal")), 0, r("EstoqueIdeal"))
            prod.EstoqueNivel = IIf(IsDBNull(r("EstoqueNivel")), 0, r("EstoqueNivel"))
            prod.Unidade = IIf(IsDBNull(r("Unidade")), 1, r("Unidade"))
            prod.PCompra = IIf(IsDBNull(r("PCompra")), 0, r("PCompra"))
            prod.DescontoCompra = IIf(IsDBNull(r("DescontoCompra")), Nothing, r("DescontoCompra"))
            prod.PVenda = IIf(IsDBNull(r("PVenda")), 0, r("PVenda"))
            prod.Estoque = IIf(IsDBNull(r("Estoque")), 0, r("Estoque"))
            prod.ProdutoAtivo = IIf(IsDBNull(r("ProdutoAtivo")), Nothing, r("ProdutoAtivo"))
            prod.SitTributaria = IIf(IsDBNull(r("SitTributaria")), Nothing, r("SitTributaria"))
            prod.SituacaoTributaria = IIf(IsDBNull(r("SituacaoTributaria")), String.Empty, r("SituacaoTributaria"))
            prod.NCM = IIf(IsDBNull(r("NCM")), String.Empty, r("NCM"))
            prod.UltAltera = IIf(IsDBNull(r("UltAltera")), Nothing, r("UltAltera"))
            prod.EntradaData = IIf(IsDBNull(r("EntradaData")), Nothing, r("EntradaData"))
            prod.CodBarrasA = IIf(IsDBNull(r("CodBarrasA")), String.Empty, r("CodBarrasA"))
            prod.Movimento = IIf(IsDBNull(r("Movimento")), Nothing, r("Movimento"))
            prod.MovimentoDescricao = IIf(IsDBNull(r("MovimentoDescricao")), String.Empty, r("MovimentoDescricao"))
            '
            Return prod
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' GET LISTA PRODUTOS DE UMA TRANSACAO
    '---------------------------------------------------------------------------------------------------------
    Public Function GetProdutosLista_Transacao(IDTransacao As Integer) As List(Of clProduto)
        '
        Return GetProdutos_Where("IDProduto IN (SELECT IDProduto FROM tblTransacaoItens WHERE IDTransacao = " & IDTransacao & ")")
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' UPDATE
    '---------------------------------------------------------------------------------------------------------
    Public Function AtualizaProduto_Procedure_ID(ByVal _prod As clProduto, _filial As Integer) As Integer
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        'PARAMETROS DA TBLPRODUTO
        Conn.Parameters.Add(New SqlParameter("@IDProduto", _prod.IDProduto))
        Conn.Parameters.Add(New SqlParameter("@RGProduto", _prod.RGProduto))
        Conn.Parameters.Add(New SqlParameter("@Produto", _prod.Produto))
        Conn.Parameters.Add(New SqlParameter("@CodBarrasA", _prod.CodBarrasA))
        Conn.Parameters.Add(New SqlParameter("@IDFabricante", _prod.IDFabricante))
        Conn.Parameters.Add(New SqlParameter("@IDProdutoTipo", _prod.IDProdutoTipo))
        Conn.Parameters.Add(New SqlParameter("@IDProdutoSubTipo", _prod.IDProdutoSubTipo))
        Conn.Parameters.Add(New SqlParameter("@IDCategoria", _prod.IDCategoria))
        Conn.Parameters.Add(New SqlParameter("@Autor", _prod.Autor))
        Conn.Parameters.Add(New SqlParameter("@Unidade", _prod.Unidade))
        Conn.Parameters.Add(New SqlParameter("@PCompra", _prod.PCompra))
        Conn.Parameters.Add(New SqlParameter("@DescontoCompra", _prod.DescontoCompra))
        Conn.Parameters.Add(New SqlParameter("@PVenda", _prod.PVenda))
        Conn.Parameters.Add(New SqlParameter("@ProdutoAtivo", _prod.ProdutoAtivo))
        Conn.Parameters.Add(New SqlParameter("@SitTributaria", _prod.SitTributaria))
        Conn.Parameters.Add(New SqlParameter("@NCM", _prod.NCM))
        Conn.Parameters.Add(New SqlParameter("@UltAltera", _prod.UltAltera))
        Conn.Parameters.Add(New SqlParameter("@EntradaData", _prod.EntradaData))
        Conn.Parameters.Add(New SqlParameter("@Movimento", _prod.Movimento))
        '
        ' PARAMETROS DA TBLESTOQUE
        Conn.Parameters.Add(New SqlParameter("@EstoqueNivel", _prod.EstoqueNivel))
        Conn.Parameters.Add(New SqlParameter("@EstoqueIdeal", _prod.EstoqueIdeal))
        Conn.Parameters.Add(New SqlParameter("@IDFilial", _filial))
        '
        Try
            Dim strReturn As String = objDB.ExecuteProcedureID("uspProduto_Alterar", Conn.Parameters)
            If IsNumeric(strReturn) Then
                Return CInt(strReturn)
            Else
                Throw New Exception(strReturn)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' INSERT
    '---------------------------------------------------------------------------------------------------------
    Public Function SalvaNovoProduto_Procedure_ID(ByVal _prod As clProduto, _filial As Integer) As Long
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand

        'Adiciona os Parâmetros
        Conn.Parameters.Add(New SqlParameter("@RGProduto", _prod.RGProduto))
        Conn.Parameters.Add(New SqlParameter("@Produto", _prod.Produto))
        Conn.Parameters.Add(New SqlParameter("@CodBarrasA", _prod.CodBarrasA))
        Conn.Parameters.Add(New SqlParameter("@IDFabricante", _prod.IDFabricante))
        Conn.Parameters.Add(New SqlParameter("@IDProdutoTipo", _prod.IDProdutoTipo))
        Conn.Parameters.Add(New SqlParameter("@IDProdutoSubTipo", _prod.IDProdutoSubTipo))
        Conn.Parameters.Add(New SqlParameter("@IDCategoria", _prod.IDCategoria))
        Conn.Parameters.Add(New SqlParameter("@Autor", _prod.Autor))
        Conn.Parameters.Add(New SqlParameter("@Unidade", _prod.Unidade))
        Conn.Parameters.Add(New SqlParameter("@PCompra", _prod.PCompra))
        Conn.Parameters.Add(New SqlParameter("@DescontoCompra", _prod.DescontoCompra))
        Conn.Parameters.Add(New SqlParameter("@PVenda", _prod.PVenda))
        Conn.Parameters.Add(New SqlParameter("@ProdutoAtivo", _prod.ProdutoAtivo))
        Conn.Parameters.Add(New SqlParameter("@SitTributaria", _prod.SitTributaria))
        Conn.Parameters.Add(New SqlParameter("@NCM", _prod.NCM))
        Conn.Parameters.Add(New SqlParameter("@UltAltera", _prod.UltAltera))
        Conn.Parameters.Add(New SqlParameter("@EntradaData", _prod.EntradaData))
        Conn.Parameters.Add(New SqlParameter("@Movimento", _prod.Movimento))
        '
        ' PARAMETROS DA TBLESTOQUE
        Conn.Parameters.Add(New SqlParameter("@EstoqueNivel", _prod.EstoqueNivel))
        Conn.Parameters.Add(New SqlParameter("@EstoqueIdeal", _prod.EstoqueIdeal))
        Conn.Parameters.Add(New SqlParameter("@IDFilial", _filial))
        '
        Try
            Return objDB.ExecuteProcedureID("uspProduto_Inserir", Conn.Parameters)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' PROCURAR PRODUTO DO FRMPROCURAR PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoProcurar(myFilial As Integer,
                                    myTipo As Integer?,
                                    mySubTipo As Integer?,
                                    myAtivo As Boolean?,
                                    myComEstoque As Boolean?,
                                    myDescricao As String) As List(Of clProduto)
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        db.AdicionarParametros("IDFilial", myFilial)
        '
        If Not IsNothing(myTipo) Then
            db.AdicionarParametros("ProdutoTipo", myTipo)
        End If
        '
        If Not IsNothing(mySubTipo) Then
            db.AdicionarParametros("SubTipo", mySubTipo)
        End If
        '
        If Not IsNothing(myAtivo) Then
            db.AdicionarParametros("SomenteAtivo", myAtivo)
        End If
        '
        If Not IsNothing(myComEstoque) Then
            db.AdicionarParametros("ComEstoque", myComEstoque)
        End If
        '
        If myDescricao.Trim.Length > 0 Then
            db.AdicionarParametros("Descricao", myDescricao.Trim)
        End If
        '
        Try
            Dim pList As New List(Of clProduto)
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspProduto_Procura")
            '
            For Each r As DataRow In dt.Rows
                Dim p As New clProduto
                '
                p.IDProduto = IIf(IsDBNull(r("IDProduto")), Nothing, r("IDProduto"))
                p.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
                p.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
                p.Autor = IIf(IsDBNull(r("Autor")), String.Empty, r("Autor"))
                p.IDProdutoTipo = IIf(IsDBNull(r("IDProdutoTipo")), Nothing, r("IDProdutoTipo"))
                p.IDProdutoSubTipo = IIf(IsDBNull(r("IDProdutoSubTipo")), Nothing, r("IDProdutoSubTipo"))
                p.PVenda = IIf(IsDBNull(r("PVenda")), Nothing, r("PVenda"))
                p.PCompra = IIf(IsDBNull(r("PCompra")), Nothing, r("PCompra"))
                p.DescontoCompra = IIf(IsDBNull(r("DescontoCompra")), Nothing, r("DescontoCompra"))
                p.ProdutoAtivo = IIf(IsDBNull(r("ProdutoAtivo")), Nothing, r("ProdutoAtivo"))
                p.Estoque = IIf(IsDBNull(r("Estoque")), Nothing, r("Estoque"))
                '
                pList.Add(p)
            Next
            '
            Return pList
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' RETORNA UMA DATATABLE DE AUTORES DE PRODUTOS
    '-----------------------------------------------------------------------------------------------------------------
    Public Function GetAutoresLista() As DataTable
        Dim db As New AcessoDados
        Dim sql As String
        '
        sql = "SELECT AUTOR, COUNT(Autor) AS Quantidade FROM tblProduto WHERE Autor <> '' GROUP BY Autor ORDER BY Autor "
        '
        Try
            Return db.ExecuteConsultaSQL_DataTable(sql)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' RETORNA UMA DATATABLE DE FABRICANTES DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function GetFabricantes(Optional Ativo As Boolean? = Nothing) As DataTable
        '
        Dim SQL As New SQLControl
        '
        Dim strSQL As String = "SELECT * FROM tblProdutoFabricante"
        '
        If Not Ativo Is Nothing Then
            strSQL = strSQL & " WHERE FabricanteAtivo = '" & CBool(Ativo).ToString & "'"
        End If
        '
        Try
            SQL.ExecQuery(strSQL)
            '
            If SQL.HasException(True) Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' ALTERA O ESTOQUE NIVEL OU O ESTOQUE IDEAL DO PRODUTO
    '---------------------------------------------------------------------------------------------------------
    Public Function AtualizaProduto_EstoqueNivel(IDProduto As Integer, Nivel As Integer, Ideal As Integer, _filial As Integer) As Integer
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand
        '
        ' PARAMETROS DA TBLESTOQUE
        Conn.Parameters.Add(New SqlParameter("@IDProduto", IDProduto))
        Conn.Parameters.Add(New SqlParameter("@EstoqueNivel", Nivel))
        Conn.Parameters.Add(New SqlParameter("@EstoqueIdeal", Ideal))
        Conn.Parameters.Add(New SqlParameter("@IDFilial", _filial))
        '
        Try
            Dim strReturn As String = objDB.ExecuteProcedureID("uspProduto_Alterar_EstoqueNivel", Conn.Parameters)
            If IsNumeric(strReturn) Then
                Return CInt(strReturn)
            Else
                Throw New Exception(strReturn)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' BUSCA UM NOVO NUMERO DE REGISTRO RGPRODUTO VALIDO NA BASE DE DADOS
    '---------------------------------------------------------------------------------------------------------
    Public Function ProcuraMaxRGProduto() As Integer
        '
        Dim SQL As New SQLControl
        '
        Try
            '--- verifica se existe a X_tblProdutos (TABELA ANTIGA)
            Dim sqlstr As String = "SELECT OBJECT_ID('X_tblProdutos') AS RETORNO"
            SQL.ExecQuery(sqlstr)
            '
            If SQL.HasException(True) Then
                Throw New Exception(SQL.Exception)
            End If
            '
            If Not IsDBNull(SQL.DBDT.Rows(0)("RETORNO")) Then '--- procura na X_TBLPRODUTOS
                SQL.ExecQuery("SELECT MAX(RGProduto) FROM X_tblProdutos")
                '
                If SQL.HasException(True) Then
                    Throw New Exception(SQL.Exception)
                End If
                '
                If SQL.RecordCount > 0 Then
                    Dim r As DataRow = SQL.DBDT.Rows(0)
                    Return r(0)
                Else
                    Return 0
                End If
            Else '--- procura na TBLPRODUTO (TABELA NOVA)
                SQL.ExecQuery("SELECT MAX(RGProduto) FROM tblProduto")
                '
                If SQL.HasException(True) Then
                    Return 0
                    Exit Function
                End If
                '
                If SQL.RecordCount > 0 Then
                    Dim r As DataRow = SQL.DBDT.Rows(0)
                    Return r(0)
                Else
                    Return 0
                End If
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' BUSCA OS DADOS DO PRODUTO ANTERIOR NA X_TBLPRODUTOS
    '---------------------------------------------------------------------------------------------------------
    Public Function ProcuraProduto_CadastroAntigo(RGProduto As Integer) As clProduto
        '
        Dim SQL As New SQLControl
        '
        Try
            '--- verifica se existe a X_tblProdutos
            Dim sqlstr As String = "SELECT OBJECT_ID('X_tblProdutos') AS RETORNO"
            SQL.ExecQuery(sqlstr)
            '
            If SQL.HasException(True) Then
                Throw New Exception(SQL.Exception)
            End If
            '
            If Not IsDBNull(SQL.DBDT.Rows(0)("RETORNO")) Then '--- procura na X_TBLPRODUTOS
                SQL.AddParam("@RGProduto", RGProduto)
                SQL.ExecQuery("SELECT * FROM X_tblProdutos WHERE RGProduto = @RGProduto")
                '
                If SQL.HasException(True) Then
                    Throw New Exception(SQL.Exception)
                End If
                '
                If SQL.RecordCount > 0 Then
                    Dim r As DataRow = SQL.DBDT.Rows(0)
                    Dim myProd As New clProduto
                    '
                    myProd.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
                    myProd.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
                    myProd.Autor = IIf(IsDBNull(r("Autor")), String.Empty, r("Autor"))
                    myProd.PVenda = IIf(IsDBNull(r("Venda")), Nothing, r("Venda"))
                    myProd.PCompra = IIf(IsDBNull(r("Compra")), Nothing, r("Compra"))
                    myProd.EstoqueNivel = IIf(IsDBNull(r("EstoqueNivel")), Nothing, r("EstoqueNivel"))
                    myProd.EstoqueIdeal = IIf(IsDBNull(r("EstoqueIdeal")), Nothing, r("EstoqueIdeal"))
                    myProd.DescontoCompra = 0
                    '
                    Return myProd
                    '
                Else
                    Return Nothing
                End If
            Else '--- não encontrou a X_tblProdutos
                Return Nothing
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' ATIVA | DESATIVA O PRODUTO INFORMADO
    '---------------------------------------------------------------------------------------------------------
    Public Function ProdutoAtivarDesativar(IDProduto As Integer, Ativar As Boolean) As Object
        '
        Dim SQL As New SQLControl
        Dim mySQL As String = ""
        '
        If Ativar = True Then
            mySQL = "UPDATE tblProduto SET ProdutoAtivo = 'TRUE' WHERE IDProduto = " & IDProduto
        Else
            mySQL = "UPDATE tblProduto SET ProdutoAtivo = 'FALSE' WHERE IDProduto = " & IDProduto
        End If
        '
        Try
            SQL.ExecQuery(mySQL)
            '
            If SQL.HasException(True) Then
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
    '---------------------------------------------------------------------------------------------------------
    ' ALTERA O FABRICANTE DO PRODUTO INFORMADO
    '---------------------------------------------------------------------------------------------------------
    Public Function ProdutoAlterarFabricante(IDProduto As Integer, newIDFabricante As Integer) As Object
        '
        Dim SQL As New SQLControl
        Dim mySQL As String = ""
        '
        mySQL = "UPDATE tblProduto SET IDFabricante = " & newIDFabricante & " WHERE IDProduto = " & IDProduto
        '
        Try
            SQL.ExecQuery(mySQL)
            '
            If SQL.HasException(True) Then
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
    '---------------------------------------------------------------------------------------------------------
    ' ALTERA O TIPO | SUBTIPO DO PRODUTO INFORMADO
    '---------------------------------------------------------------------------------------------------------
    Public Function ProdutoAlterarTipoSubTipo(IDProduto As Integer,
                                              newIDTipo As Integer,
                                              newIDSubTipo As Integer,
                                              LimparCategoria As Boolean) As Object
        '
        Dim SQL As New SQLControl
        Dim mySQL As String = ""
        '
        mySQL = "UPDATE tblProduto SET IDProdutoTipo = " & newIDTipo & ", IDProdutoSubTipo = " & newIDSubTipo
        '
        If LimparCategoria = True Then
            mySQL = mySQL & ", IDCategoria = NULL"
        End If
        '
        mySQL = mySQL & " WHERE IDProduto = " & IDProduto

        '
        Try
            SQL.ExecQuery(mySQL)
            '
            If SQL.HasException(True) Then
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
    '---------------------------------------------------------------------------------------------------------
    ' ALTERA A CATEGORIA DO PRODUTO INFORMADO
    '---------------------------------------------------------------------------------------------------------
    Public Function ProdutoAlterarCategoria(IDProduto As Integer, newIDCategoria As Integer) As Object
        '
        Dim SQL As New SQLControl
        Dim mySQL As String = ""
        '
        mySQL = "UPDATE tblProduto SET IDCategoria = " & newIDCategoria & " WHERE IDProduto = " & IDProduto
        '
        Try
            SQL.ExecQuery(mySQL)
            '
            If SQL.HasException(True) Then
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
    '---------------------------------------------------------------------------------------------------------
    ' ALTERA O ESTOQUE MINIMO E IDEAL DO PRODUTO E FILIAL INFORMADOS
    '---------------------------------------------------------------------------------------------------------
    Public Function ProdutoAlterarEstoqueMinimoIdeal(IDProduto As Integer,
                                                     IDFilial As Integer,
                                                     newEstMinimo As Integer?,
                                                     newEstIdeal As Integer?) As Object
        '
        Dim SQL As New SQLControl
        Dim mySQL As String = ""
        '
        If Not IsNothing(newEstIdeal) AndAlso Not IsNothing(newEstMinimo) Then
            mySQL = "UPDATE tblEstoque SET" &
                " EstoqueNivel = " & newEstMinimo &
                ", EstoqueIdeal = " & newEstIdeal &
                " WHERE IDProduto = " & IDProduto &
                " AND IDFilial = " & IDFilial
        ElseIf IsNothing(newEstIdeal) Then
            mySQL = "UPDATE tblEstoque SET" &
                " EstoqueNivel = " & newEstMinimo &
                " WHERE IDProduto = " & IDProduto &
                " AND IDFilial = " & IDFilial
        ElseIf IsNothing(newEstMinimo) Then
            mySQL = "UPDATE tblEstoque SET" &
                " EstoqueIdeal = " & newEstIdeal &
                " WHERE IDProduto = " & IDProduto &
                " AND IDFilial = " & IDFilial
        End If
        '
        Try
            SQL.ExecQuery(mySQL)
            '
            If SQL.HasException(True) Then
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
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' dispose managed state (managed objects).
            End If

            ' free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' uncomment the following line if Finalize() is overridden above.
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    '
End Class
'
'=================================================================================================================
' PRODUTO ETIQUETA BLL
'=================================================================================================================
Public Class ProdutoEtiquetaBLL
    '
    Private db As AcessoDados
    '
    Sub New()
        db = New AcessoDados
    End Sub
    '
    '---------------------------------------------------------------------------------------------------------
    ' GET LISTA DE ETIQUETAS DO BD | RETURN LIST
    '---------------------------------------------------------------------------------------------------------
    Public Function Get_Etiquetas() As List(Of clProdutoEtiqueta)
        Dim lstEtiq As New List(Of clProdutoEtiqueta)
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.Text, "SELECT * FROM qryProdutoEtiqueta")
            '
            For Each r As DataRow In dt.Rows
                Dim e As New clProdutoEtiqueta
                '
                e.IDEtiqueta = IIf(IsDBNull(r("IDEtiqueta")), Nothing, r("IDEtiqueta"))
                e.IDProduto = IIf(IsDBNull(r("IDProduto")), Nothing, r("IDProduto"))
                e.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
                e.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
                e.ProdutoTipo = IIf(IsDBNull(r("ProdutoTipo")), String.Empty, r("ProdutoTipo"))
                e.ProdutoSubTipo = IIf(IsDBNull(r("ProdutoSubTipo")), String.Empty, r("ProdutoSubTipo"))
                e.PVenda = IIf(IsDBNull(r("PVenda")), Nothing, r("PVenda"))
                e.Quantidade = IIf(IsDBNull(r("Quantidade")), Nothing, r("Quantidade"))
                e.PrecoVenda = IIf(IsDBNull(r("PrecoVenda")), Nothing, r("PrecoVenda"))
                e.PrecoPromocao = IIf(IsDBNull(r("PrecoPromocao")), Nothing, r("PrecoPromocao"))
                e.CodBarrasA = IIf(IsDBNull(r("CodBarrasA")), String.Empty, r("CodBarrasA"))
                '
                lstEtiq.Add(e)
            Next
            '
            Return lstEtiq
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' INSERT NOVO ITEM OU ALTERAR A ETIQUETA NO BD
    '---------------------------------------------------------------------------------------------------------
    Public Function InserirAlterar_EtiquetaItem(myEtiq As clProdutoEtiqueta) As clProdutoEtiqueta
        '
        '-- limpa e adiciona os parametros
        db.LimparParametros()
        '
        db.AdicionarParametros("@RGProduto", myEtiq.RGProduto)
        db.AdicionarParametros("@Quantidade", IIf(IsNothing(myEtiq.Quantidade), 1, myEtiq.Quantidade))
        '
        If Not IsNothing(myEtiq.PrecoPromocao) Then
            db.AdicionarParametros("@PrecoPromocao", myEtiq.PrecoPromocao)
        End If
        '
        If Not IsNothing(myEtiq.IDEtiqueta) Then
            db.AdicionarParametros("@IDEtiqueta", myEtiq.IDEtiqueta)
        End If
        '
        '-- insere altera a etiqueta retorna um clEtiqueta
        Try
            '
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspProdutoEtiqueta_InserirAlterarItem")
            '
            If dt.Rows.Count = 0 Then
                Throw New Exception("Não retornou nenhum valor")
            End If
            '
            Dim e As New clProdutoEtiqueta
            Dim r As DataRow = dt.Rows(0)
            '
            If Not IsNumeric(r.Item(0)) Then
                Return e
            End If
            '
            e.IDEtiqueta = IIf(IsDBNull(r("IDEtiqueta")), Nothing, r("IDEtiqueta"))
            e.IDProduto = IIf(IsDBNull(r("IDProduto")), Nothing, r("IDProduto"))
            e.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
            e.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
            e.ProdutoTipo = IIf(IsDBNull(r("ProdutoTipo")), String.Empty, r("ProdutoTipo"))
            e.ProdutoSubTipo = IIf(IsDBNull(r("ProdutoSubTipo")), String.Empty, r("ProdutoSubTipo"))
            e.PVenda = IIf(IsDBNull(r("PVenda")), Nothing, r("PVenda"))
            e.Quantidade = IIf(IsDBNull(r("Quantidade")), Nothing, r("Quantidade"))
            e.PrecoVenda = IIf(IsDBNull(r("PrecoVenda")), Nothing, r("PrecoVenda"))
            e.PrecoPromocao = IIf(IsDBNull(r("PrecoPromocao")), Nothing, r("PrecoPromocao"))
            e.CodBarrasA = IIf(IsDBNull(r("CodBarrasA")), String.Empty, r("CodBarrasA"))
            '
            '-- retorna clEtiqueta
            Return e
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '---------------------------------------------------------------------------------------------------------
    ' INSERT ETIQUETAS DE UMA TRANSACAO | COMPRA NO BD
    '---------------------------------------------------------------------------------------------------------
    Public Sub Insert_EtiquetasTransacao(IDTransacao As Integer)
        Dim obj As Object
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDTransacao", IDTransacao)
        '
        Try
            obj = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspProdutoEtiqueta_InserirTransacao")
            '
            If Not IsNumeric(obj) Then
                Throw New Exception(obj.ToString)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Sub
    '
    '---------------------------------------------------------------------------------------------------------
    ' DELETE ITEM DE ETIQUETA NO BD
    '---------------------------------------------------------------------------------------------------------
    Public Sub Delete_Etiquetas(IDProduto As Integer)
        Try
            Dim strSQL As String = "DELETE tblProdutoEtiqueta WHERE IDProduto = " & IDProduto
            '
            db.ExecutarManipulacao(CommandType.Text, strSQL)
            '
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '
    '---------------------------------------------------------------------------------------------------------
    ' DELETE LIMPA TODA LISTAGEM DE ETIQUETAS DO BD
    '---------------------------------------------------------------------------------------------------------
    Public Sub Delete_Etiquetas()
        Try
            Dim strSQL As String = "DELETE tblProdutoEtiqueta"
            '
            db.ExecutarManipulacao(CommandType.Text, strSQL)
            '
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    '
    '---------------------------------------------------------------------------------------------------------
    ' OBTER A LISTA DOS RELATORIOS DE ETIQUETA
    '---------------------------------------------------------------------------------------------------------
    Public Function Get_ProdutoEtiquetaRelatorios() As DataTable
        Try
            Dim strSQL As String = "SELECT * FROM tblProdutoEtiquetaRelatorio"
            '
            Return db.ExecutarConsulta(CommandType.Text, strSQL)
            '
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
End Class
'
'
'=================================================================================================================
' PRODUTO TIPO SUBTIPO CATEGORIA
'=================================================================================================================
Public Class TipoSubTipoCategoriaBLL
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' GET TIPOS WITH WHERE DATATABLE
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoTipo_GET_WithWhere(Optional myWhere As String = "") As DataTable
        '
        Dim SQL As New SQLControl
        Dim myQuery As String = "SELECT * FROM tblProdutoTipo "
        '
        If myWhere.Length > 0 Then
            myQuery = myQuery & " WHERE " & myWhere
        End If
        '
        Try
            SQL.ExecQuery(myQuery, True)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' INSERE NOVO TIPO DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoTipo_Insert(ProdutoTipo As String) As Integer
        '
        Dim SQL As New SQLControl
        Dim newID As Integer
        Dim myQuery As String = "INSERT INTO tblProdutoTipo" &
                                " (ProdutoTipo, Ativo) VALUES ('@ProdutoTipo', 'TRUE')"
        '
        SQL.AddParam("@ProdutoTipo", ProdutoTipo)
        '
        Try
            SQL.ExecQuery(myQuery, True)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            If SQL.RecordCount > 0 Then
                newID = SQL.DBDT.Rows(0).Item(0)
                Return newID
            Else
                Return Nothing
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' ALTERA TIPO DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoTipo_Update(IDProdutoTipo As Integer, ProdutoTipo As String, Ativo As Boolean) As Boolean
        '
        Dim SQL As New SQLControl
        Dim myQuery As String = "UPDATE tblProdutoTipo " &
                                "SET ProdutoTipo = @ProdutoTipo, " &
                                " Ativo = @Ativo" &
                                " WHERE IDProdutoTipo = @IDProdutoTipo"
        '
        SQL.AddParam("@ProdutoTipo", ProdutoTipo)
        SQL.AddParam("@Ativo", Ativo)
        SQL.AddParam("@IDProdutoTipo", IDProdutoTipo)
        '
        Try
            SQL.ExecQuery(myQuery)
            '
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
    '-----------------------------------------------------------------------------------------------------------------
    ' GET SUBTIPOS WITH WHERE DATATABLE
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoSubTipo_GET_WithWhere(Optional myWhere As String = "") As DataTable
        '
        Dim SQL As New SQLControl
        Dim myQuery As String = "SELECT * FROM tblProdutoSubTipo "
        '
        If myWhere.Length > 0 Then
            myQuery = myQuery & " WHERE " & myWhere
        End If
        '
        Try
            SQL.ExecQuery(myQuery, True)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' INSERE NOVO SUBTIPO DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoSubTipo_Insert(SubTipo As String, IDTipo As Integer) As Integer
        '
        Dim SQL As New SQLControl
        Dim newID As Integer
        Dim myQuery As String = "INSERT INTO tblProdutoSubTipo (ProdutoSubTipo, IDProdutoTipo, Ativo) " &
                                "VALUES ('@SubTipo', @IDTipo, 'TRUE')"
        '
        SQL.AddParam("@IDTipo", IDTipo)
        SQL.AddParam("@SubTipo", SubTipo)
        '
        Try
            SQL.ExecQuery(myQuery, True)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            If SQL.RecordCount > 0 Then
                newID = SQL.DBDT.Rows(0).Item(0)
                Return newID
            Else
                Return Nothing
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' ALTERA SUBTIPO DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoSubTipo_Update(IDSubTipo As Integer, SubTipo As String, Ativo As Boolean) As Boolean
        '
        Dim SQL As New SQLControl
        Dim myQuery As String = "UPDATE tblProdutoSubTipo " &
                                "SET ProdutoSubTipo = '@SubTipo', " &
                                " Ativo = @Ativo" &
                                " WHERE IDSubTipo = @IDSubTipo"
        '
        SQL.AddParam("@SubTipo", SubTipo)
        SQL.AddParam("@Ativo", Ativo)
        SQL.AddParam("@IDSubTipo", IDSubTipo)
        '
        Try
            SQL.ExecQuery(myQuery)
            '
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
    '-----------------------------------------------------------------------------------------------------------------
    ' GET CATEGORIAS WITH WHERE DATATABLE
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoCategoria_GET_WithWhere(Optional myWhere As String = "") As DataTable
        '
        Dim SQL As New SQLControl
        Dim myQuery As String = "SELECT * FROM tblProdutoCategoria "
        '
        If myWhere.Length > 0 Then
            myQuery = myQuery & " WHERE " & myWhere
        End If
        '
        Try
            SQL.ExecQuery(myQuery, True)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' INSERE NOVA CATEGORIA DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoCategoria_Insert(Categoria As String, IDTipo As Integer) As Integer
        '
        Dim SQL As New SQLControl
        Dim newID As Integer
        Dim myQuery As String = "INSERT INTO tblProdutoCategoria (ProdutoSubTipo, IDProdutoTipo, Ativo) " &
                                "VALUES ('@Categoria', @IDTipo, 'TRUE')"
        '
        SQL.AddParam("@IDTipo", IDTipo)
        SQL.AddParam("@Categoria", Categoria)
        '
        Try
            SQL.ExecQuery(myQuery, True)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            If SQL.RecordCount > 0 Then
                newID = SQL.DBDT.Rows(0).Item(0)
                Return newID
            Else
                Return Nothing
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' ALTERA CATEGORIA DE PRODUTO
    '-----------------------------------------------------------------------------------------------------------------
    Public Function ProdutoCategoria_Update(IDCategoria As Integer, Categoria As String, Ativo As Boolean) As Boolean
        '
        Dim SQL As New SQLControl
        Dim myQuery As String = "UPDATE tblProdutoCategoria" &
                                " SET ProdutoCategoria = '@Categoria', " &
                                " Ativo = @Ativo" &
                                " WHERE IDCategoria = @IDCategoria"
        '
        SQL.AddParam("@Categoria", Categoria)
        SQL.AddParam("@Ativo", Ativo)
        SQL.AddParam("@IDCategoria", IDCategoria)
        '
        Try
            SQL.ExecQuery(myQuery)
            '
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
    '-----------------------------------------------------------------------------------------------------------------
    ' RETORNA UMA DATATABLE DE TIPOS DE PRODUTOS
    '-----------------------------------------------------------------------------------------------------------------
    Public Function GetTipos(Optional Ativo As Boolean? = Nothing) As DataTable
        Dim SQL As New SQLControl
        '
        Dim strSQL As String = "SELECT * FROM tblProdutoTipo"
        '
        If Not Ativo Is Nothing Then
            strSQL = strSQL & " WHERE Ativo = '" & CBool(Ativo).ToString & "'"
        End If
        '
        Try
            SQL.ExecQuery(strSQL)
            '
            If SQL.HasException(True) Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' RETORNA UMA DATATABLE DE SUBTIPOS DE PRODUTOS
    '-----------------------------------------------------------------------------------------------------------------
    Public Function GetSubTipos(Optional IDTipo As Integer? = Nothing,
                                Optional Ativo As Boolean? = Nothing) As DataTable
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = "SELECT * FROM tblProdutoSubTipo"
        '
        '--- determina as possibiliadades da clausula WHERE
        If Not IDTipo Is Nothing AndAlso Not Ativo Is Nothing Then
            strSQL = strSQL & " WHERE IDProdutoTipo = " & IDTipo & " Ativo = '" & CBool(Ativo).ToString & "'"
        ElseIf IDTipo Is Nothing AndAlso Not Ativo Is Nothing Then
            strSQL = strSQL & " WHERE Ativo = '" & CBool(Ativo).ToString & "'"
        ElseIf Not IDTipo Is Nothing AndAlso Ativo Is Nothing Then
            strSQL = strSQL & " WHERE IDProdutoTipo = " & IDTipo
        End If
        '
        '--- executa o comando
        Try
            SQL.ExecQuery(strSQL)
            '
            If SQL.HasException(True) Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------------------
    ' RETORNA UMA DATATABLE DE CATEGORIAS DE PRODUTOS
    '-----------------------------------------------------------------------------------------------------------------
    Public Function GetCategorias(Optional IDTipo As Integer? = Nothing,
                                  Optional Ativo As Boolean? = Nothing) As DataTable
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = "SELECT * FROM tblProdutoCategoria"
        '
        '--- determina as possibiliadades da clausula WHERE
        If Not IDTipo Is Nothing AndAlso Not Ativo Is Nothing Then
            strSQL = strSQL & " WHERE IDProdutoTipo = " & IDTipo & " Ativo = '" & CBool(Ativo).ToString & "'"
        ElseIf IDTipo Is Nothing AndAlso Not Ativo Is Nothing Then
            strSQL = strSQL & " WHERE Ativo = '" & CBool(Ativo).ToString & "'"
        ElseIf Not IDTipo Is Nothing AndAlso Ativo Is Nothing Then
            strSQL = strSQL & " WHERE IDProdutoTipo = " & IDTipo
        End If
        '
        '--- executa o comando
        Try
            SQL.ExecQuery(strSQL)
            '
            If SQL.HasException(True) Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Return SQL.DBDT
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
End Class