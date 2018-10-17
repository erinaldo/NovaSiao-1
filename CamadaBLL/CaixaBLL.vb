Imports CamadaDAL
Imports CamadaDTO
'
Public Class CaixaBLL
    '
    '============================================================================================
    ' INSERT NOVO CAIXA E RETORNA UM CLCAIXA
    '============================================================================================
    Public Function InserirNovo_Caixa(ByVal myCaixa As clCaixa) As clCaixa
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDConta", myCaixa.IDConta)
        db.AdicionarParametros("@IDFilial", myCaixa.IDFilial)
        db.AdicionarParametros("@FechamentoData", myCaixa.FechamentoData)
        db.AdicionarParametros("@IDSituacao", myCaixa.IDSituacao)
        db.AdicionarParametros("@DataInicial", myCaixa.DataInicial)
        db.AdicionarParametros("@DataFinal", myCaixa.DataFinal)
        db.AdicionarParametros("@SaldoFinal", myCaixa.SaldoFinal)
        db.AdicionarParametros("@SaldoAnterior", myCaixa.SaldoAnterior)
        db.AdicionarParametros("@Observacao", myCaixa.Observacao)
        '
        Try
            Dim dt As DataTable
            '
            dt = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_Inserir")
            '
            If dt.Rows.Count > 0 Then
                Dim clC As New clCaixa
                Dim r As DataRow = dt(0)
                '
                clC.IDCaixa = IIf(IsDBNull(r("IDCaixa")), Nothing, r("IDCaixa"))
                clC.IDConta = IIf(IsDBNull(r("IDConta")), Nothing, r("IDConta"))
                clC.Conta = IIf(IsDBNull(r("Conta")), String.Empty, r("Conta"))
                clC.IDFilial = IIf(IsDBNull(r("IDFilial")), Nothing, r("IDFilial"))
                clC.ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
                clC.FechamentoData = IIf(IsDBNull(r("FechamentoData")), Nothing, r("FechamentoData"))
                clC.IDSituacao = IIf(IsDBNull(r("IDSituacao")), Nothing, r("IDSituacao"))
                clC.Situacao = IIf(IsDBNull(r("Situacao")), String.Empty, r("Situacao"))
                clC.DataInicial = IIf(IsDBNull(r("DataInicial")), Nothing, r("DataInicial"))
                clC.DataFinal = IIf(IsDBNull(r("DataFinal")), Nothing, r("DataFinal"))
                clC.SaldoFinal = IIf(IsDBNull(r("SaldoFinal")), Nothing, r("SaldoFinal"))
                clC.SaldoAnterior = IIf(IsDBNull(r("SaldoAnterior")), Nothing, r("SaldoAnterior"))
                clC.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
                '
                '--- Retorna o resultado clCaixa
                Return clC
                '
            Else
                Throw New Exception("Operação de salvamento não retornou nenhum dado...")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    '
    '============================================================================================
    '  GET CAIXA PELO ID
    '============================================================================================
    Public Function GetCaixa_PeloID(myIDCaixa As Integer) As clCaixa
        '
        Dim SQL As New SQLControl
        Dim str As String = "SELECT * FROM qryCaixa WHERE IDCaixa = " & myIDCaixa
        '
        Try
            SQL.ExecQuery(str)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            Dim dt As DataTable = SQL.DBDT
            '
            If dt.Rows.Count > 0 Then
                Dim clC As New clCaixa
                Dim r As DataRow = dt(0)
                '
                clC.IDCaixa = IIf(IsDBNull(r("IDCaixa")), Nothing, r("IDCaixa"))
                clC.IDConta = IIf(IsDBNull(r("IDConta")), Nothing, r("IDConta"))
                clC.Conta = IIf(IsDBNull(r("Conta")), String.Empty, r("Conta"))
                clC.IDFilial = IIf(IsDBNull(r("IDFilial")), Nothing, r("IDFilial"))
                clC.ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
                clC.FechamentoData = IIf(IsDBNull(r("FechamentoData")), Nothing, r("FechamentoData"))
                clC.IDSituacao = IIf(IsDBNull(r("IDSituacao")), Nothing, r("IDSituacao"))
                clC.Situacao = IIf(IsDBNull(r("Situacao")), String.Empty, r("Situacao"))
                clC.DataInicial = IIf(IsDBNull(r("DataInicial")), Nothing, r("DataInicial"))
                clC.DataFinal = IIf(IsDBNull(r("DataFinal")), Nothing, r("DataFinal"))
                clC.SaldoFinal = IIf(IsDBNull(r("SaldoFinal")), Nothing, r("SaldoFinal"))
                clC.SaldoAnterior = IIf(IsDBNull(r("SaldoAnterior")), Nothing, r("SaldoAnterior"))
                clC.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
                '
                Return clC
                '
            Else
                Throw New Exception("Não foi encontrado nenhum registro...")
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' GET PRIMEIRA E ULTIMA DATAS DOS MOVIMENTOS DE ENTRADA E SAIDA PELO IDCONTA
    '============================================================================================
    Public Function GetLastDados_IDConta(myIDConta As Byte) As DataTable
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDConta", myIDConta)
        '
        Try
            Return db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_GetLastDados_IDConta")
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' GET MOVIMENTACOES DE CAIXA PELO ID CAIXA
    '============================================================================================
    Public Function GetMovimentos_IDCaixa(myIDCaixa) As List(Of clCaixaMovimentacao)
        '
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", myIDCaixa)
        '
        Try
            Dim listMov As New List(Of clCaixaMovimentacao)
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_GetMovimentos_IDCaixa")
            '
            If dt.Rows.Count = 0 Then
                Return listMov
            End If
            '
            For Each r As DataRow In dt.Rows
                Dim cx As New clCaixaMovimentacao
                '
                cx.IDMov = IIf(IsDBNull(r("ID")), Nothing, r("ID"))
                cx.IDOrigem = IIf(IsDBNull(r("IDOrigem")), Nothing, r("IDOrigem"))
                cx.Origem = IIf(IsDBNull(r("Origem")), Nothing, r("Origem"))
                cx.Movimentacao = IIf(IsDBNull(r("Mov")), String.Empty, r("Mov"))
                cx.MovValor = IIf(IsDBNull(r("MovValor")), Nothing, r("MovValor"))
                cx.MovData = IIf(IsDBNull(r("MovData")), Nothing, r("MovData"))
                cx.IDMovForma = IIf(IsDBNull(r("IDMovForma")), Nothing, r("IDMovForma"))
                cx.MovForma = IIf(IsDBNull(r("MovForma")), String.Empty, r("MovForma"))
                cx.IDOperadora = IIf(IsDBNull(r("IDOperadora")), Nothing, r("IDOperadora"))
                cx.Operadora = IIf(IsDBNull(r("Operadora")), String.Empty, r("Operadora"))
                cx.Descricao = IIf(IsDBNull(r("Descricao")), String.Empty, r("Descricao"))
                '
                listMov.Add(cx)
                '
            Next
            '
            Return listMov
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' GET SALDO DAS OPERADORAS DO CAIXA ANTERIOR PELO IDCONTA
    '============================================================================================
    Public Function GetSaldo_FormaOperadoras_IDCaixa(myIDCaixa As Int16) As DataTable
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", myIDCaixa)
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_GetSaldoFormasOperadoras")
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' INSERE NIVELAMENTO NO CAIXA E RETORNA UMA NOVA MOVIMENTACAO DE CAIXA
    '============================================================================================
    Public Function InserirNivelamento(IDCaixa As Integer, IDMovForma As Int16, myValor As Decimal) As clCaixaMovimentacao
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
        db.AdicionarParametros("@IDMovForma", IDMovForma)
        db.AdicionarParametros("@Valor", myValor)
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_InserirNivelamento")
            '
            If dt.Rows.Count = 0 Then
                Throw New Exception("ERRO: Não foi adicionado nivelamento...")
            End If
            '
            Dim r As DataRow = dt.Rows(dt.Rows.Count - 1)
            Dim cx As New clCaixaMovimentacao
            '
            cx.IDMov = IIf(IsDBNull(r("ID")), Nothing, r("ID"))
            cx.IDOrigem = IIf(IsDBNull(r("IDOrigem")), Nothing, r("IDOrigem"))
            cx.Origem = IIf(IsDBNull(r("Origem")), Nothing, r("Origem"))
            cx.Movimentacao = IIf(IsDBNull(r("Mov")), String.Empty, r("Mov"))
            cx.MovValor = IIf(IsDBNull(r("MovValor")), Nothing, r("MovValor"))
            cx.MovData = IIf(IsDBNull(r("MovData")), Nothing, r("MovData"))
            cx.IDMovForma = IIf(IsDBNull(r("IDMovForma")), Nothing, r("IDMovForma"))
            cx.MovForma = IIf(IsDBNull(r("MovForma")), String.Empty, r("MovForma"))
            cx.IDOperadora = IIf(IsDBNull(r("IDOperadora")), Nothing, r("IDOperadora"))
            cx.Operadora = IIf(IsDBNull(r("Operadora")), String.Empty, r("Operadora"))
            cx.Descricao = IIf(IsDBNull(r("Descricao")), String.Empty, r("Descricao"))
            '
            Return cx
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' EXCLUIR TODOS NIVELAMENTOS NO CAIXA DE UMA OPERADORA DE CAIXA
    '============================================================================================
    Public Function ExcluirNivelamentos(IDCaixa As Integer, IDOperadora As Int16) As Boolean
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
        db.AdicionarParametros("@IDOperadora", IDOperadora)
        '
        Try
            db.ExecutarManipulacao(CommandType.StoredProcedure, "uspCaixa_ExcluirNivelamentos")
            '
            Return True
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' ALTERA O PEÍRODO DA DATA FINAL DO CAIXA PELO ID CAIXA
    '============================================================================================
    Public Sub Caixa_AlteraPeriodo(IDCaixa As Integer, dtFinal As Date)
        '
        Dim SQL As New SQLControl
        SQL.AddParam("@dtFinal", dtFinal)
        SQL.AddParam("@IDCaixa", IDCaixa)
        '
        Dim str As String = "UPDATE tblCaixa SET DataFinal = @dtFinal WHERE IDCaixa = @IDCaixa"
        '
        Try
            SQL.ExecQuery(str)
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Sub
    '
    '============================================================================================
    ' GET LISTAGEM DE CAIXAS PARA PROCURA
    '============================================================================================
    Public Function GetCaixaLista_Procura(myIDConta As Int16?,
                                          Optional dtInicial As Date? = Nothing,
                                          Optional dtFinal As Date? = Nothing) As List(Of clCaixa)
        '
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDConta", myIDConta)
        If Not IsNothing(dtInicial) Then
            db.AdicionarParametros("@DataInicial", dtInicial)
        End If
        '
        If Not IsNothing(dtFinal) Then
            db.AdicionarParametros("@DataFinal", dtFinal)
        End If
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_ProcurarLista")
            Dim cxList As New List(Of clCaixa)
            '
            For Each r In dt.Rows
                Dim c As New clCaixa
                '
                c.IDCaixa = IIf(IsDBNull(r("IDCaixa")), Nothing, r("IDCaixa"))
                c.IDConta = IIf(IsDBNull(r("IDConta")), Nothing, r("IDConta"))
                c.Conta = IIf(IsDBNull(r("Conta")), String.Empty, r("Conta"))
                c.IDFilial = IIf(IsDBNull(r("IDFilial")), Nothing, r("IDFilial"))
                c.ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
                c.FechamentoData = IIf(IsDBNull(r("FechamentoData")), Nothing, r("FechamentoData"))
                c.DataInicial = IIf(IsDBNull(r("DataInicial")), Nothing, r("DataInicial"))
                c.DataFinal = IIf(IsDBNull(r("DataFinal")), Nothing, r("DataFinal"))
                c.IDSituacao = IIf(IsDBNull(r("IDSituacao")), Nothing, r("IDSituacao"))
                c.Situacao = IIf(IsDBNull(r("Situacao")), String.Empty, r("Situacao"))
                c.SaldoFinal = IIf(IsDBNull(r("SaldoFinal")), Nothing, r("SaldoFinal"))
                c.SaldoAnterior = IIf(IsDBNull(r("SaldoAnterior")), Nothing, r("SaldoAnterior"))
                c.Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
                '
                cxList.Add(c)
                '
            Next
            '
            Return cxList
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' GET LISTAGEM DE CAIXAS PARA PROCURA
    '============================================================================================
    Public Sub CaixaFinalizar(IDCaixa As Integer, Observacao As String)
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
        '
        If Observacao.Trim.Length > 0 Then
            db.AdicionarParametros("@Observacao", Observacao)
        End If
        '
        Try
            '
            Dim ID As Object = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspCaixa_Finalizar")
            '
            If Not IsNumeric(ID) Then
                Throw New Exception(ID.ToString)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Sub
    '
    '============================================================================================
    ' DESBLOQUEAR CAIXA E CONTA PELO ID CAIXA
    '============================================================================================
    Public Sub DesbloquearCaixa(IDCaixa As Integer)
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
        '
        Try
            Dim ID As Object = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspCaixa_Desbloquear")
            '
            If Not IsNumeric(ID) Then
                Throw New Exception(ID.ToString)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Sub
    '
    '============================================================================================
    ' EXCLUIR CAIXA PELO ID CAIXA
    '============================================================================================
    Public Sub ExcluirCaixa(IDCaixa As Integer)
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
        '
        Try
            Dim ID As Object = db.ExecutarManipulacao(CommandType.StoredProcedure, "uspCaixa_Excluir")
            '
            If Not ID = True Then
                Throw New Exception(ID.ToString)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Sub
    '
    '============================================================================================
    ' SALVA OBSERVACAO PELO IDCAIXA
    '============================================================================================
    Public Sub Caixa_SalvarObservacao(IDCaixa As Integer, Observacao As String)
        Dim SQL As New SQLControl
        '
        '--- DETERMINA OS PARAMETROS
        SQL.AddParam("@IDCaixa", IDCaixa)
        '
        Try
            '--- EXCLUI A OBSERVACAO ANTERIOR SE HOUVER
            SQL.ExecQuery("DELETE tblObservacao WHERE Origem = 5 AND IDOrigem = @IDCaixa")
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
                Exit Sub
            End If
            '
            '--- DETERMINA OS PARAMETROS
            SQL.AddParam("@IDCaixa", IDCaixa)
            SQL.AddParam("@Observacao", Observacao)
            '
            '--- INSERE A NOVA OBSERVACAO
            SQL.ExecQuery("INSERT INTO tblObservacao (IDOrigem, Origem, Observacao) VALUES (@IDCaixa, 5, @Observacao);")
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
                Exit Sub
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Sub
    '
End Class

