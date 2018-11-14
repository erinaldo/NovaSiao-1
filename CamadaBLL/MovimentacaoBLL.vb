Imports CamadaDAL
Imports CamadaDTO

Public Class MovimentacaoBLL
    '
    '============================================================================================
    ' GET CONTA DADOS INICIAIS | ABERTURA
    '============================================================================================
    Public Function Conta_GetDados_Inicial(IDConta As Int16) As DataTable
        Dim db As New AcessoDados
        '
        '--- DETERMINA OS PARAMETROS
        db.AdicionarParametros("@IDConta", IDConta)
        '
        Try
            '
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspContaPadrao_GET")
            '
            Return dt
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '============================================================================================
    ' GET DATA DE BLOQUEIO DA CONTA INFORMADA 
    '============================================================================================
    Public Function Conta_GetDataBloqueio(IDConta As Int16) As Date?
        '
        Dim SQL As New SQLControl
        SQL.AddParam("@IDConta", IDConta)
        '
        Dim strSQL As String = "SELECT BloqueioData FROM tblContas WHERE IDConta = @IDConta"
        '
        Try
            SQL.ExecQuery(strSQL)
            '
            '--- verifica erro
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
            End If
            '
            '--- verifica retorno
            If SQL.DBDT.Rows.Count > 0 Then
                '
                Dim r As DataRow = SQL.DBDT.Rows(0)
                Dim dtBloq As Date? = If(IsDBNull(r.Item("BloqueioData")), Nothing, r.Item("BloqueioData"))
                '
                Return dtBloq
                '
            Else
                Throw New Exception("Não foi encontrada nenhuma conta com esse ID...")
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '===================================================================================================
    ' OBTER LISTA DAS FORMAS DE PAGAMENTO
    '===================================================================================================
    Public Function MovForma_GET(Optional Ativo As Boolean? = Nothing) As DataTable
        Dim db As New AcessoDados
        Dim dtFormas As DataTable
        '
        If IsNothing(Ativo) Then
            dtFormas = db.ExecutarConsulta(CommandType.Text, "SELECT * FROM tblMovForma")
        Else
            dtFormas = db.ExecutarConsulta(CommandType.Text, "SELECT * FROM tblMovForma WHERE Ativo = '" & Ativo & "'")
        End If
        '
        Return dtFormas
        '
    End Function
    '
    '===================================================================================================
    ' OBTER LISTA DOS TIPOS DE PAGAMENTO
    '===================================================================================================
    Public Function MovTipo_GET(Optional Ativo As Boolean? = Nothing) As DataTable
        Dim db As New AcessoDados
        Dim dtTipos As DataTable
        '
        If IsNothing(Ativo) Then
            dtTipos = db.ExecutarConsulta(CommandType.Text, "SELECT * FROM tblMovTipo")
        Else
            dtTipos = db.ExecutarConsulta(CommandType.Text, "SELECT * FROM tblMovTipo WHERE Ativo = '" & Ativo & "'")
        End If
        '
        Return dtTipos
        '
    End Function
    '
    '===================================================================================================
    ' OBTER LISTA DAS CONTAS PELO IDFILIAL
    '===================================================================================================
    Public Function Contas_GET_PorIDFilial(Optional IDFilial As Integer? = Nothing) As DataTable
        Dim db As New AcessoDados
        Dim dtConta As DataTable
        '
        db.LimparParametros()
        '
        If Not IsNothing(IDFilial) Then
            '--- adiciona os parametros
            db.AdicionarParametros("@IDFilial", IDFilial)
        End If
        '
        Try
            '--- executa o procedure
            dtConta = db.ExecutarConsulta(CommandType.StoredProcedure, "uspContas_GET_PorIDFilial")
            Return dtConta
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '
    '=========================================================================================
    ' OBTER LISTA DAS FORMAS DE MOVIMENTACAO
    '=========================================================================================
    ' --- OBTER LISTA
    Public Function MovForma_GET_List() As List(Of clMovForma)
        Dim db As New AcessoDados
        '
        Dim mySQL As String = "SELECT * FROM qryMovForma"
        Dim dt As DataTable = db.ExecutarConsulta(CommandType.Text, mySQL)
        '
        Dim list As New List(Of clMovForma)
        '
        If dt.Rows.Count = 0 Then Return list
        '
        For Each r As DataRow In dt.Rows
            Dim clF As New clMovForma
            '
            clF.IDMovForma = IIf(IsDBNull(r("IDMovForma")), Nothing, r("IDMovForma"))
            clF.MovForma = IIf(IsDBNull(r("MovForma")), String.Empty, r("MovForma"))
            clF.IDMovTipo = IIf(IsDBNull(r("IDMovTipo")), Nothing, r("IDMovTipo"))
            clF.MovTipo = IIf(IsDBNull(r("MovTipo")), String.Empty, r("MovTipo"))
            clF.IDOperadora = IIf(IsDBNull(r("IDOperadora")), Nothing, r("IDOperadora"))
            clF.Operadora = IIf(IsDBNull(r("Operadora")), String.Empty, r("Operadora"))
            clF.IDCartao = IIf(IsDBNull(r("IDCartao")), Nothing, r("IDCartao"))
            clF.Cartao = IIf(IsDBNull(r("Cartao")), String.Empty, r("Cartao"))
            clF.Parcelas = IIf(IsDBNull(r("Parcelas")), Nothing, r("Parcelas"))
            clF.Comissao = IIf(IsDBNull(r("Comissao")), Nothing, r("Comissao"))
            clF.NoDias = IIf(IsDBNull(r("NoDias")), Nothing, r("NoDias"))
            clF.Ativo = IIf(IsDBNull(r("Ativo")), Nothing, r("Ativo"))
            '
            list.Add(clF)
            '
        Next
        '
        Return list
        '
    End Function
    '
    '--- SALVAR REGISTRO
    Public Function MovForma_Inserir(myMovForma As clMovForma) As Int16?
        '
        Dim mySQL As New SQLControl
        '
        '---adiciona os parâmentros
        mySQL.AddParam("@MovForma", myMovForma.MovForma)
        mySQL.AddParam("@IDMovTipo", myMovForma.IDMovTipo)
        mySQL.AddParam("@IDOperadora", IIf(IsNothing(myMovForma.IDOperadora), DBNull.Value, myMovForma.IDOperadora))
        mySQL.AddParam("@IDCartao", IIf(IsNothing(myMovForma.IDCartao), DBNull.Value, myMovForma.IDCartao))
        mySQL.AddParam("@Parcelas", IIf(IsNothing(myMovForma.Parcelas), DBNull.Value, myMovForma.Parcelas))
        mySQL.AddParam("@Comissao", IIf(IsNothing(myMovForma.Comissao), DBNull.Value, myMovForma.Comissao))
        mySQL.AddParam("@NoDias", IIf(IsNothing(myMovForma.NoDias), DBNull.Value, myMovForma.NoDias))
        mySQL.AddParam("@Ativo", True)
        '
        Try
            '---salva o NOVO registro
            mySQL.ExecQuery("INSERT INTO tblMovForma " &
                            "(IDMovTipo, MovForma, IDOperadora, IDCartao, Parcelas, Comissao, NoDias, Ativo) " &
                            "VALUES (@IDMovTipo, @MovForma, @IDOperadora, @IDCartao, @Parcelas, @Comissao, @NoDias, @Ativo);", True)
            '
            If mySQL.HasException() Then Throw New Exception(mySQL.Exception)
            '
            Dim newID As Int16? = Convert.ToInt16(mySQL.DBDT.Rows(0)("LastID"))
            '
            Return newID
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--- ATUALIZAR REGISTRO
    Public Function MovForma_Update(myMovForma As clMovForma) As Int16?
        '
        Dim mySQL As New SQLControl
        '
        '---adiciona os parâmentros
        mySQL.AddParam("@IDMovForma", myMovForma.IDMovForma)
        mySQL.AddParam("@MovForma", myMovForma.MovForma)
        mySQL.AddParam("@IDMovTipo", myMovForma.IDMovTipo)
        mySQL.AddParam("@IDOperadora", IIf(IsNothing(myMovForma.IDOperadora), DBNull.Value, myMovForma.IDOperadora))
        mySQL.AddParam("@IDCartao", IIf(IsNothing(myMovForma.IDCartao), DBNull.Value, myMovForma.IDCartao))
        mySQL.AddParam("@Parcelas", IIf(IsNothing(myMovForma.Parcelas), DBNull.Value, myMovForma.Parcelas))
        mySQL.AddParam("@Comissao", IIf(IsNothing(myMovForma.Comissao), DBNull.Value, myMovForma.Comissao))
        mySQL.AddParam("@NoDias", IIf(IsNothing(myMovForma.NoDias), DBNull.Value, myMovForma.NoDias))
        mySQL.AddParam("@Ativo", True)
        '
        Try
            '---atualiza o registro ALTERADO
            mySQL.ExecQuery("UPDATE tblMovForma " &
                            "SET IDMovTipo=@IDMovTipo, MovForma=@MovForma, IDOperadora=@IDOperadora, " &
                            "IDCartao=@IDCartao, Parcelas=@Parcelas, Comissao=@Comissao, NoDias=@NoDias, Ativo=@Ativo " &
                            "WHERE IDMovForma=@IDMovForma;")
            '
            If mySQL.HasException() Then Throw New Exception(mySQL.Exception)
            '
            Return myMovForma.IDMovForma
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '*******************************************************************************************************************



    '=========================================================================================
    ' TIPOS DE MOVIMENTACAO
    '=========================================================================================
    ' --- OBTER LISTA
    Public Function MovTipo_GET_Dt() As DataTable
        Dim SQL As New SQLControl
        '
        Try
            '
            SQL.ExecQuery("SELECT * FROM tblMovTipo")
            Dim dtTipo As DataTable = SQL.DBDT
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
                Exit Function
            End If
            '
            Return dtTipo
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    ' --- SALVAR REGISTRO
    Public Function MovTipo_Inserir(MovTipo As String) As Integer
        '
        Dim lng As Integer = MovTipo.Trim.Length
        '
        If lng = 0 Then
            Throw New Exception("A descrição do Tipo de Movimentação está vazia...")
        ElseIf lng > 30 Then
            Throw New Exception("A descrição do Tipo de Movimentação não pode ser maior de 30 caracteres")
        End If
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = String.Format("INSERT INTO tblMovTipo (MovTipo, Ativo) VALUES ('{0}', 'TRUE')", MovTipo)
        '
        Try
            '
            SQL.ExecQuery(strSQL, True)
            '
            If SQL.HasException Then Throw New Exception(SQL.Exception)
            '
            If SQL.RecordCount > 0 Then
                Dim r As DataRow = SQL.DBDT.Rows(0)
                '
                Return r("LastID")
            Else
                Throw New Exception("Não foi retornado o novo ID...")
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    ' --- ATUALIZAR REGISTRO
    Public Function MovTipo_Update(IDMovTipo As Integer, MovTipo As String, Ativo As Boolean) As Integer
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = String.Format("UPDATE tblMovTipo SET MovTipo = '{1}', Ativo = '{2}' WHERE IDMovTipo = {0}",
                                             IDMovTipo, MovTipo, Ativo.ToString)
        '
        Try
            '
            SQL.ExecQuery(strSQL, False)
            '
            If Not SQL.HasException Then
                Return IDMovTipo
            Else
                Throw New Exception(SQL.Exception)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--- VERIFICA ATIVIDADE (Se esta sendo utilizado)
    '--- retorna a quantidade de MovForma que se utiliza desse MOVTIPO
    Public Function MovTipo_Atividade(IDMovTipo As Integer) As Integer
        Dim mySQL As New SQLControl
        '
        Try
            '
            mySQL.ExecQuery("SELECT COUNT(IDMovTipo) AS numReg FROM tblMovForma WHERE IDMovTipo = " & IDMovTipo)
            '
            If mySQL.HasException Then Throw New Exception(mySQL.Exception)
            '
            If mySQL.RecordCount > 0 Then
                Return mySQL.DBDT.Rows(0)("numReg")
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
    '*******************************************************************************************************************



    '=========================================================================================
    ' TIPOS DE CARTAO DE CREDITO
    '=========================================================================================
    ' --- OBTER LISTA
    Public Function MovCartao_GET_Dt() As DataTable
        Dim SQL As New SQLControl
        '
        Try
            '
            SQL.ExecQuery("SELECT * FROM tblMovCartao")
            Dim dtC As DataTable = SQL.DBDT
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
                Exit Function
            End If
            '
            Return dtC
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    ' --- SALVAR REGISTRO
    Public Function Cartao_Inserir(Cartao As String) As Integer
        '
        Dim lng As Integer = Cartao.Trim.Length
        '
        If lng = 0 Then
            Throw New Exception("A descrição do Tipo de Cartão está vazia...")
        ElseIf lng > 30 Then
            Throw New Exception("A descrição do Tipo de Cartão não pode ser maior de 30 caracteres")
        End If
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = String.Format("INSERT INTO tblMovCartao (Cartao, Ativo) VALUES ('{0}', 'TRUE')", Cartao)
        '
        Try
            '
            SQL.ExecQuery(strSQL, True)
            '
            If SQL.HasException Then Throw New Exception(SQL.Exception)
            '
            If SQL.RecordCount > 0 Then
                Dim r As DataRow = SQL.DBDT.Rows(0)
                '
                Return r("LastID")
            Else
                Throw New Exception("Não foi retornado o novo ID...")
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    ' --- ATUALIZAR REGISTRO
    Public Function Cartao_Update(IDCartao As Integer, Cartao As String, Ativo As Boolean) As Integer
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = String.Format("UPDATE tblMovCartao SET Cartao = '{1}', Ativo = '{2}' WHERE IDCartao = {0}",
                                             IDCartao, Cartao, Ativo.ToString)
        '
        Try
            '
            SQL.ExecQuery(strSQL, False)
            '
            If Not SQL.HasException Then
                Return IDCartao
            Else
                Throw New Exception(SQL.Exception)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '*******************************************************************************************************************



    '=========================================================================================
    ' OPERADORAS DE MOVIMENTO
    '=========================================================================================
    ' --- OBTER LISTA
    Public Function MovOperadora_GET_Dt() As DataTable
        Dim SQL As New SQLControl
        '
        Try
            '
            SQL.ExecQuery("SELECT * FROM tblMovOperadora")
            Dim dtO As DataTable = SQL.DBDT
            '
            If SQL.HasException Then
                Throw New Exception(SQL.Exception)
                Exit Function
            End If
            '
            Return dtO
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    ' --- SALVAR REGISTRO
    Public Function Operadora_Inserir(Operadora As String) As Integer
        '
        Dim lng As Integer = Operadora.Trim.Length
        '
        If lng = 0 Then
            Throw New Exception("A descrição da Operadora de Movimentação está vazia...")
        ElseIf lng > 30 Then
            Throw New Exception("A descrição do Operadora de Movimentação não pode ser maior de 30 caracteres")
        End If
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = String.Format("INSERT INTO tblMovOperadora (Operadora, Ativo) VALUES ('{0}', 'TRUE')", Operadora)
        '
        Try
            '
            SQL.ExecQuery(strSQL, True)
            '
            If SQL.HasException Then Throw New Exception(SQL.Exception)
            '
            If SQL.RecordCount > 0 Then
                Dim r As DataRow = SQL.DBDT.Rows(0)
                '
                Return r("LastID")
            Else
                Throw New Exception("Não foi retornado o novo ID...")
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    ' --- ATUALIZAR REGISTRO
    Public Function Operadora_Update(IDOperadora As Integer, Operadora As String, Ativo As Boolean) As Integer
        '
        Dim SQL As New SQLControl
        Dim strSQL As String = String.Format("UPDATE tblMovOperadora SET Operadora = '{1}', Ativo = '{2}' WHERE IDOperadora = {0}",
                                             IDOperadora, Operadora, Ativo.ToString)
        '
        Try
            '
            SQL.ExecQuery(strSQL, False)
            '
            If Not SQL.HasException Then
                Return IDOperadora
            Else
                Throw New Exception(SQL.Exception)
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '*******************************************************************************************************************
    '
    '
End Class
