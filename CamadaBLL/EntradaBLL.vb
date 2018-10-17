Imports CamadaDAL
Imports CamadaDTO
'
Public Enum EntradaOrigem
    Transacao = 1 ' IDOrigem: tblTransacao
    AReceberParcela = 2 ' IDOrigem : tblAReceberParcela
    Creditos = 3 ' IDOrigem : tblCreditos
End Enum
'
Public Class EntradaBLL
    '===================================================================================================
    ' OBTER LISTA DE TODAS ENTRADAS INFORMANDO O TIPO DE ORIGEM E PELO IDORIGEM 
    '===================================================================================================
    Public Function Entrada_GET_PorOrigemID(Origem As EntradaOrigem, IDOrigem As Integer) As List(Of clEntradas)
        Dim db As New AcessoDados
        '
        '--- limpa os paramentros
        db.LimparParametros()
        '
        '--- adiciona os parametros
        db.AdicionarParametros("@Origem", CByte(Origem)) ' ORIGEM TRANSACAO/VENDA
        db.AdicionarParametros("@IDOrigem", IDOrigem)
        '
        Try
            Dim listE As New List(Of clEntradas)
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspEntrada_GET_PorOrigemID")
            '
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    Dim clE As New clEntradas
                    '
                    With clE
                        .IDEntrada = IIf(IsDBNull(r("IDEntrada")), Nothing, r("IDEntrada"))
                        .Origem = 1
                        .IDOrigem = IDOrigem
                        .IDConta = IIf(IsDBNull(r("IDConta")), Nothing, r("IDConta"))
                        .Conta = IIf(IsDBNull(r("Conta")), String.Empty, r("Conta"))
                        .EntradaData = IIf(IsDBNull(r("EntradaData")), Nothing, r("EntradaData"))
                        .EntradaValor = IIf(IsDBNull(r("EntradaValor")), 0, r("EntradaValor"))
                        .IDMovForma = IIf(IsDBNull(r("IDMovForma")), Nothing, r("IDMovForma"))
                        .Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
                        .MovForma = IIf(IsDBNull(r("MovForma")), String.Empty, r("MovForma"))
                        .Operadora = IIf(IsDBNull(r("Operadora")), String.Empty, r("Operadora"))
                        .IDOperadora = IIf(IsDBNull(r("IDOperadora")), Nothing, r("IDOperadora"))
                        .Cartao = IIf(IsDBNull(r("Cartao")), String.Empty, r("Cartao"))
                        .IDMovTipo = IIf(IsDBNull(r("IDMovTipo")), Nothing, r("IDMovTipo"))
                        .IDCaixa = IIf(IsDBNull(r("IDCaixa")), Nothing, r("IDCaixa"))
                    End With
                    '
                    '--- Adiciona o ROW na listagem
                    listE.Add(clE)
                    '
                Next
            End If
            '
            Return listE
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '===================================================================================================
    ' SALVA NOVA ENTRADA/PAGAMENTO NO BD
    '===================================================================================================
    Public Function Entrada_Inserir(Entrada As clEntradas) As Integer
        Dim db As New AcessoDados
        '
        '--- limpa os parametros
        db.LimparParametros()
        '
        '--- adiciona o parametros
        db.AdicionarParametros("@Origem", Entrada.Origem)
        db.AdicionarParametros("@IDOrigem", Entrada.IDOrigem)
        db.AdicionarParametros("@IDConta", Entrada.IDConta)
        db.AdicionarParametros("@EntradaData", Entrada.EntradaData)
        db.AdicionarParametros("@EntradaValor", Entrada.EntradaValor)
        db.AdicionarParametros("@IDMovForma", Entrada.IDMovForma)
        db.AdicionarParametros("@Observacao", Entrada.Observacao)
        db.AdicionarParametros("@Creditar", Entrada.Creditar)
        db.AdicionarParametros("@Descricao", Entrada.Descricao)
        '
        Try
            Return db.ExecutarManipulacao(CommandType.StoredProcedure, "uspEntrada_Inserir")
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '===================================================================================================
    ' LIMPA/EXCLUI TODOS OS REGISTROS DE PAGAMENTOS/ENTRADAS DA TRANSACAO
    '===================================================================================================
    Public Function Entrada_ExcluirTodas_PorTransacao(IDTransacao As Integer) As Boolean
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDTransacao", IDTransacao)
        '
        Try
            db.ExecutarManipulacao(CommandType.StoredProcedure, "uspEntrada_ExcluirTodas_IDTransacao")
            Return True
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
End Class
