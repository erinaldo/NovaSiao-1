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
        db.AdicionarParametros("@CaixaFinalDia", myCaixa.CaixaFinalDia)
        db.AdicionarParametros("@IDFuncionario", If(myCaixa.IDFuncionario, DBNull.Value))
        '
        Try
            Dim dt As DataTable
            '
            dt = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_Inserir")
            '
            If dt.Rows.Count > 0 Then
                '
                Dim r As DataRow = dt(0)
                Return Convert_DtRow_clCaixa(r)
                '
            Else
                Throw New Exception("Operação de salvamento não retornou nenhum dado...")
            End If
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    '  CONVERT DT ROW EM CLCAIXA
    '============================================================================================
    Private Function Convert_DtRow_clCaixa(r As DataRow) As clCaixa
        '
        Dim clC As New clCaixa
        '
        Try
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
            clC.CaixaFinalDia = IIf(IsDBNull(r("CaixaFinalDia")), Nothing, r("CaixaFinalDia"))
            clC.IDFuncionario = IIf(IsDBNull(r("IDFuncionario")), Nothing, r("IDFuncionario"))
            clC.ApelidoFuncionario = IIf(IsDBNull(r("ApelidoFuncionario")), String.Empty, r("ApelidoFuncionario"))
            '
            Return clC
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
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
                '
                Dim r As DataRow = dt(0)
                Return Convert_DtRow_clCaixa(r)
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
    ' GET SALDO DAS CONTAS DO CAIXA ANTERIOR PELO IDCAIXA
    '============================================================================================
    Public Function GetSaldo_ContaTipos_IDCaixa(myIDCaixa As Integer) As DataTable
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", myIDCaixa)
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspCaixa_GetSaldoContaTipos")
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
    Public Function InserirNivelamento(IDCaixa As Integer, IDMeio As Int16, myValor As Decimal) As clMovimentacao
        '
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
        db.AdicionarParametros("@IDMeio", IDMeio)
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
            Dim cx As New clMovimentacao
            '
            cx.IDMovimentacao = IIf(IsDBNull(r("IDMovimentacao")), Nothing, r("IDMovimentacao"))
            cx.IDOrigem = IIf(IsDBNull(r("IDOrigem")), Nothing, r("IDOrigem"))
            cx.Origem = IIf(IsDBNull(r("Origem")), Nothing, r("Origem"))
            cx.Mov = IIf(IsDBNull(r("Mov")), String.Empty, r("Mov"))
            cx.MovValor = IIf(IsDBNull(r("MovValor")), Nothing, r("MovValor"))
            cx.MovData = IIf(IsDBNull(r("MovData")), Nothing, r("MovData"))
            cx.IDMovForma = IIf(IsDBNull(r("IDMovForma")), Nothing, r("IDMovForma"))
            cx.MovForma = IIf(IsDBNull(r("MovForma")), String.Empty, r("MovForma"))
            cx.IDConta = IIf(IsDBNull(r("IDConta")), Nothing, r("IDConta"))
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
    ' EXCLUIR TODOS NIVELAMENTOS NO CAIXA
    '============================================================================================
    Public Function ExcluirNivelamentos(IDCaixa As Integer) As Boolean
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", IDCaixa)
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
    Public Function GetCaixaLista_Procura(myIDConta As Int16,
                                          Optional dtInicial As Date? = Nothing,
                                          Optional dtFinal As Date? = Nothing) As List(Of clCaixa)
        '
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        '
        db.AdicionarParametros("@IDConta", myIDConta)
        Dim myQuery As String = "SELECT * FROM qryCaixa WHERE IDConta = @IDConta"
        '
        If Not IsNothing(dtInicial) Then
            '
            db.AdicionarParametros("@DataInicial", dtInicial)
            myQuery = myQuery & " AND DataInicial >= @DataInicial"
            '
        End If
        '
        If Not IsNothing(dtFinal) Then
            '
            db.AdicionarParametros("@DataFinal", dtFinal)
            myQuery = myQuery & " AND DataFinal <= @DataFinal"
            '
        End If
        '
        Try
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.Text, myQuery)
            Dim cxList As New List(Of clCaixa)
            '
            If dt.Rows.Count = 0 Then Return cxList
            '
            For Each r In dt.Rows
                '
                cxList.Add(Convert_DtRow_clCaixa(r))
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
    ' FIANLIZA O CAIXA
    '============================================================================================
    Public Function CaixaFinalizar(_caixa As clCaixa) As Integer
        '
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDCaixa", _caixa.IDCaixa)
        db.AdicionarParametros("@CaixaFinalDia", _caixa.CaixaFinalDia)
        db.AdicionarParametros("@IDFuncionario", If(_caixa.IDFuncionario, DBNull.Value))
        '
        If _caixa.Observacao.Trim.Length > 0 Then
            db.AdicionarParametros("@Observacao", _caixa.Observacao)
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
            Return ID
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
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
    ' SALVA CAIXA
    '============================================================================================
    Public Function UpdateCaixa(_caixa As clCaixa) As Boolean
        '
        Dim db As New AcessoDados
        '
        '--- inicia uma TRANSACTION 
        db.BeginTransaction()
        '
        Try
            '
            '--- SALVA a observacao
            '-------------------------------------------------------------------------------
            Dim obs As New ObservacaoBLL
            '
            If Not obs.SaveObservacao(5, _caixa.IDCaixa, _caixa.Observacao, db) Then
                Throw New Exception("Não foi possível salvar a observação do caixa...")
            End If
            '
            '--- SALVA o caixa
            '-------------------------------------------------------------------------------
            db.LimparParametros()
            db.AdicionarParametros("@IDCaixa", _caixa.IDCaixa)
            db.AdicionarParametros("@IDSituacao", _caixa.IDSituacao)
            db.AdicionarParametros("@DataInicial", _caixa.DataInicial)
            db.AdicionarParametros("@DataFinal", _caixa.DataFinal)
            db.AdicionarParametros("@SaldoFinal", _caixa.SaldoFinal)
            db.AdicionarParametros("@SaldoAnterior", _caixa.SaldoAnterior)
            db.AdicionarParametros("@Observacao", _caixa.Observacao)
            db.AdicionarParametros("@CaixaFinalDia", _caixa.CaixaFinalDia)
            db.AdicionarParametros("@IDFuncionario", If(_caixa.IDFuncionario, DBNull.Value))
            '
            Dim myQuery As String = "UPDATE tblCaixa SET " &
                                    "IDSituacao = @IDSituacao, DataInicial = @DataInicial, DataFinal = @DataFinal, " &
                                    "SaldoFinal = @SaldoFinal, SaldoAnterior = @SaldoAnterior, " &
                                    "CaixaFinalDia = @CaixaFinalDia, IDFuncionario = @IDFuncionario " &
                                    "WHERE IDCaixa = @IDCaixa"
            '
            db.ExecutarManipulacao(CommandType.Text, myQuery)
            '
            db.CommitTransaction()
            Return True
            '
        Catch ex As Exception
            db.RollBackTransaction()
            Throw ex
        End Try
        '
    End Function
    '
End Class

