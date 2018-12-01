Imports CamadaDTO
Imports CamadaDAL
Imports System.Data.SqlClient
'
Public Class TransacaoItemBLL
    '
    '==========================================================================================
    ' ENUM MOVIMENTO de entrada ou saida
    '==========================================================================================
    Public Enum EnumMovimento
        ENTRADA '--- no caso de COMPRA, entrada de produto
        SAIDA '--- no caso de VENDA, saída de produto
    End Enum
    '
    '==========================================================================================
    ' RETORNA UMA LISTA DE ITEM TRANSACAO FILTRADO PELO IDVENDA
    '==========================================================================================
    Public Function GetVendaItens_IDVenda_List(myIDTransacao As Integer, myFilial As Integer) As List(Of clTransacaoItem)
        Dim objdb As New AcessoDados
        '
        '--- limpar os parâmetros
        objdb.LimparParametros()
        '--- adicionar os parâmetros
        objdb.AdicionarParametros("@IDTransacao", myIDTransacao)
        objdb.AdicionarParametros("@IDFilial", myFilial)
        '
        Try
            Dim dt As DataTable = objdb.ExecutarConsulta(CommandType.StoredProcedure, "uspTransacaoItem_GETItens_PorVenda")
            Dim lista As New List(Of clTransacaoItem)
            '
            If dt.Rows.Count = 0 Then Return lista
            '
            For Each r As DataRow In dt.Rows
                Dim itn As clTransacaoItem = New clTransacaoItem
                '--- Itens da tblTransacaoItens
                itn.IDTransacaoItem = IIf(IsDBNull(r("IDTransacaoItem")), Nothing, r("IDTransacaoItem"))
                itn.Preco = IIf(IsDBNull(r("Preco")), Nothing, r("Preco"))
                itn.IDTransacao = IIf(IsDBNull(r("IDTransacao")), Nothing, r("IDTransacao"))
                itn.IDProduto = IIf(IsDBNull(r("IDProduto")), Nothing, r("IDProduto"))
                itn.Quantidade = IIf(IsDBNull(r("Quantidade")), Nothing, r("Quantidade"))
                itn.Desconto = IIf(IsDBNull(r("Desconto")), Nothing, r("Desconto"))
                '--- Itens Importados tblProdutos
                itn.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
                itn.CodBarrasA = IIf(IsDBNull(r("CodBarrasA")), Nothing, r("CodBarrasA"))
                itn.PVenda = IIf(IsDBNull(r("PVenda")), Nothing, r("PVenda"))
                itn.DescontoCompra = IIf(IsDBNull(r("DescontoCompra")), 0, r("DescontoCompra"))
                itn.PCompra = IIf(IsDBNull(r("PCompra")), Nothing, r("PCompra"))
                itn.ProdutoAtivo = IIf(IsDBNull(r("ProdutoAtivo")), Nothing, r("ProdutoAtivo"))
                itn.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
                itn.Estoque = IIf(IsDBNull(r("Estoque")), Nothing, r("Estoque"))
                itn.Reservado = IIf(IsDBNull(r("Reservado")), Nothing, r("Reservado"))
                itn.ProdutoAtivo = IIf(IsDBNull(r("ProdutoAtivo")), Nothing, r("ProdutoAtivo"))
                '
                '--- Itens Estoque
                itn.IDFilial = myFilial
                '
                lista.Add(itn)
                '
            Next
            '
            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '==========================================================================================
    ' GET UM PRODUTO DO NOVO ITEM DA TRANSACAO
    '==========================================================================================
    Public Function ProdutoItem_GET(_CodProcurado As String, _IDFilial As Integer) As clTransacaoItem
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        '--- verifica se o cod informado é RG ou CODBARRAS
        If Len(_CodProcurado) < 7 Then
            db.AdicionarParametros("@RGProduto", _CodProcurado)
        Else
            db.AdicionarParametros("@CodBarras", _CodProcurado)
        End If
        '
        '--- Adico
        db.AdicionarParametros("@IDFilial", _IDFilial)
        '
        Try
            Dim clItem As New clTransacaoItem
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspProdutoItem_GET")
            '
            If dt.Rows.Count = 0 Then Return clItem '--- retorna um item vazio quando nao encontra
            '
            Dim r As DataRow = dt.Rows(0)
            '
            clItem.IDProduto = r("IDProduto")
            clItem.RGProduto = r("RGProduto")
            clItem.Produto = r("Produto")
            clItem.CodBarrasA = r("CodBarrasA")
            clItem.PVenda = r("PVenda")
            clItem.DescontoCompra = r("DescontoCompra")
            clItem.PCompra = r("PCompra")
            clItem.ProdutoAtivo = r("ProdutoAtivo")
            clItem.Estoque = r("Estoque")
            clItem.Reservado = r("Reservado")
            clItem.IDFilial = _IDFilial
            '
            Return clItem
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '==========================================================================================
    ' INSERE UM NOVO ITEM NA TRANSACAO
    '==========================================================================================
    Public Function InserirNovoItem(Item As clTransacaoItem,
                                    Movimento As EnumMovimento,
                                    MovData As Date,
                                    InsereCustos As Boolean) As Long
        Dim db As New AcessoDados
        '
        '--- limpa os parametros
        db.LimparParametros()
        '
        '--- adiciona os parametros
        db.AdicionarParametros("@IDTransacao", Item.IDTransacao)
        db.AdicionarParametros("@IDProduto", Item.IDProduto)
        db.AdicionarParametros("@Quantidade", Item.Quantidade)
        db.AdicionarParametros("@Preco", Item.Preco)
        db.AdicionarParametros("@Desconto", Item.Desconto)
        db.AdicionarParametros("@IDFilial", Item.IDFilial)
        db.AdicionarParametros("@Movimento", Movimento.ToString)
        db.AdicionarParametros("@MovData", MovData)
        db.AdicionarParametros("@InsereCustos", InsereCustos)
        '
        If InsereCustos Then ' SE InsereCustos ADICIONA OS CUSTOS NO tblTransacaoItensCustos
            db.AdicionarParametros("@ICMS", Item.ICMS)
            db.AdicionarParametros("@Substituicao", Item.Substituicao)
            db.AdicionarParametros("@FreteRateado", Item.FreteRateado)
            db.AdicionarParametros("@MVA", Item.MVA)
            db.AdicionarParametros("@IPI", Item.IPI)
        End If
        '
        Try
            '--- executa a procedure
            Dim obj As Object = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspTransacaoItem_Inserir")
            '
            '--- verifica o resultado
            If Not IsNothing(obj) AndAlso IsNumeric(obj) Then
                Return obj
            Else
                Throw New Exception(obj.ToString)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '==========================================================================================
    ' EDITA UM ITEM EXISTENTE NA TRANSACAO
    '==========================================================================================
    Public Function EditarItem(Item As clTransacaoItem,
                               Movimento As EnumMovimento,
                               MovData As Date,
                               InsereCustos As Boolean) As Long
        '
        Dim db As New AcessoDados
        '
        '--- limpa os parametros
        db.LimparParametros()
        '
        '--- adiciona os parametros
        db.AdicionarParametros("@IDTransacaoItem", Item.IDTransacaoItem)
        db.AdicionarParametros("@IDTransacao", Item.IDTransacao)
        db.AdicionarParametros("@IDProduto", Item.IDProduto)
        db.AdicionarParametros("@Quantidade", Item.Quantidade)
        db.AdicionarParametros("@Preco", Item.Preco)
        db.AdicionarParametros("@Desconto", Item.Desconto)
        db.AdicionarParametros("@IDFilial", Item.IDFilial)
        db.AdicionarParametros("@Movimento", Movimento.ToString)
        db.AdicionarParametros("@MovData", MovData)
        db.AdicionarParametros("@InsereCustos", InsereCustos)
        '
        If InsereCustos Then ' SE insereCustos EDITA OS CUSTOS NO tblTransacaoItensCustos
            db.AdicionarParametros("@ICMS", Item.ICMS)
            db.AdicionarParametros("@Substituicao", Item.Substituicao)
            db.AdicionarParametros("@FreteRateado", Item.FreteRateado)
            db.AdicionarParametros("@MVA", Item.MVA)
            db.AdicionarParametros("@IPI", Item.IPI)
        End If
        '
        Try
            '--- executa a procedure
            Dim obj As Object = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspTransacaoItem_Editar")
            '
            '--- verifica o resultado
            If Not IsNothing(obj) AndAlso IsNumeric(obj) Then
                Return obj
            Else
                Throw New Exception(obj.ToString)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '==========================================================================================
    ' EXCLUI UM ITEM EXISTENTE NA TRANSACAO
    '==========================================================================================
    Public Function ExcluirItem(Item As clTransacaoItem, Movimento As EnumMovimento) As Long
        Dim db As New AcessoDados
        '
        '--- limpa os parametros
        db.LimparParametros()
        '
        '--- adiciona os parametros
        db.AdicionarParametros("@IDTransacaoItem", Item.IDTransacaoItem)
        db.AdicionarParametros("@IDProduto", Item.IDProduto)
        db.AdicionarParametros("@Quantidade", Item.Quantidade)
        db.AdicionarParametros("@IDFilial", Item.IDFilial)
        db.AdicionarParametros("@Movimento", Movimento.ToString)
        '
        Try
            '--- executa a procedure
            Dim obj As Object = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspTransacaoItem_Excluir")
            '
            '--- verifica o resultado
            If Not IsNothing(obj) AndAlso IsNumeric(obj) Then
                Return obj
            Else
                Throw New Exception(obj.ToString)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '==========================================================================================
    ' RETORNA UMA LISTA DE ITENS TRANSACAO FILTRADO PELO IDTRANSACAO
    '==========================================================================================
    Public Function GetCompraItens_IDCompra_List(myIDTransacao As Integer, myFilial As Integer) As List(Of clTransacaoItem)
        Dim objdb As New AcessoDados
        '
        '--- limpar os parâmetros
        objdb.LimparParametros()
        '--- adicionar os parâmetros
        objdb.AdicionarParametros("@IDTransacao", myIDTransacao)
        objdb.AdicionarParametros("@IDFilial", myFilial)
        '
        Try
            Dim dt As DataTable = objdb.ExecutarConsulta(CommandType.StoredProcedure, "uspTransacaoItem_GETItens_PorCompra")
            Dim lista As New List(Of clTransacaoItem)
            '
            If dt.Rows.Count = 0 Then Return lista
            '
            For Each r As DataRow In dt.Rows
                Dim itn As clTransacaoItem = New clTransacaoItem
                '--- Itens da tblTransacaoItens
                itn.IDTransacaoItem = IIf(IsDBNull(r("IDTransacaoItem")), Nothing, r("IDTransacaoItem"))
                itn.Preco = IIf(IsDBNull(r("Preco")), Nothing, r("Preco"))
                itn.IDTransacao = IIf(IsDBNull(r("IDTransacao")), Nothing, r("IDTransacao"))
                itn.IDProduto = IIf(IsDBNull(r("IDProduto")), Nothing, r("IDProduto"))
                itn.Quantidade = IIf(IsDBNull(r("Quantidade")), Nothing, r("Quantidade"))
                itn.Desconto = IIf(IsDBNull(r("Desconto")), Nothing, r("Desconto"))
                '--- Itens Importados tblProdutos
                itn.RGProduto = IIf(IsDBNull(r("RGProduto")), Nothing, r("RGProduto"))
                itn.CodBarrasA = IIf(IsDBNull(r("CodBarrasA")), Nothing, r("CodBarrasA"))
                itn.PVenda = IIf(IsDBNull(r("PVenda")), Nothing, r("PVenda"))
                itn.DescontoCompra = IIf(IsDBNull(r("DescontoCompra")), Nothing, r("DescontoCompra"))
                itn.PCompra = IIf(IsDBNull(r("PCompra")), Nothing, r("PCompra"))
                itn.ProdutoAtivo = IIf(IsDBNull(r("ProdutoAtivo")), Nothing, r("ProdutoAtivo"))
                itn.Produto = IIf(IsDBNull(r("Produto")), String.Empty, r("Produto"))
                itn.Estoque = IIf(IsDBNull(r("Estoque")), Nothing, r("Estoque"))
                itn.Reservado = IIf(IsDBNull(r("Reservado")), Nothing, r("Reservado"))
                itn.ProdutoAtivo = IIf(IsDBNull(r("ProdutoAtivo")), Nothing, r("ProdutoAtivo"))
                '--- Itens Importados tblTransacaoItensCustos
                itn.FreteRateado = IIf(IsDBNull(r("FreteRateado")), Nothing, r("FreteRateado"))
                itn.ICMS = IIf(IsDBNull(r("ICMS")), Nothing, r("ICMS"))
                itn.Substituicao = IIf(IsDBNull(r("Substituicao")), Nothing, r("Substituicao"))
                itn.MVA = IIf(IsDBNull(r("MVA")), Nothing, r("MVA"))
                itn.IPI = IIf(IsDBNull(r("IPI")), Nothing, r("IPI"))
                '
                lista.Add(itn)
                '
            Next
            '
            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
End Class
