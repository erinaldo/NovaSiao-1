Imports System.Data.SqlClient
Imports System.Reflection
'
Public Class AcessoDados
    '-------------------------------------------------------------------------------------------------------
    'DECLARAÇÃO DAS VARIÁVEIS
    '-------------------------------------------------------------------------------------------------------
    Dim conn As SqlConnection
    Dim cmd As SqlCommand
    Dim connStr As String
    Public ParamList As New List(Of SqlParameter)
    '
    '-------------------------------------------------------------------------------------------------------
    'SUB NEW
    '-------------------------------------------------------------------------------------------------------
    Public Sub New()
        If Not Connect() Then
            Exit Sub
        End If
    End Sub
    '
    '-------------------------------------------------------------------------------------------------------
    ' RETORNAR A CONECTION STRING
    '-------------------------------------------------------------------------------------------------------
    Public Shared Function GetConnectionString() As String
        Dim retorno As New String("")
        '
        Try
            retorno = My.MySettings.Default.ConexaoString
        Catch ex As Exception
            Throw ex
        End Try
        '
        '--- veriica se há retorno da |DataDirectory|
        '--- substitui o |DataDirectory| pelo "CamadaDAL\Dados"
        If retorno.Contains("|DataDirectory|") Then
            Dim BaseDir As String = AppDomain.CurrentDomain.BaseDirectory
            '
            Dim findI As Integer
            '
            findI = BaseDir.IndexOf("\", 0)
            findI = BaseDir.IndexOf("\", findI + 1)
            findI = BaseDir.IndexOf("\", findI + 1)
            '
            BaseDir = BaseDir.Substring(0, findI) + "\CamadaDAL"
            '
            retorno = retorno.Replace("|DataDirectory|", BaseDir)
            '
        End If
        '
        Return retorno
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    'CONCETAR ABRIR A CONEXÃO
    '-------------------------------------------------------------------------------------------------------
    Private Function Connect() As Boolean
        '
        Dim connstr As String
        Dim bln As Boolean
        '
        If conn Is Nothing Then
            connstr = GetConnectionString()
            If connstr <> String.Empty Then
                bln = True
                conn = New SqlConnection(connstr)
            Else
                bln = False
            End If
        End If
        '
        If conn.State = ConnectionState.Closed Then
            Try
                conn.Open()
            Catch ex As Exception
                Throw ex
            End Try
        End If
        '
        Return bln
        '
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' FECHAR A CONEXÃO
    '-------------------------------------------------------------------------------------------------------
    Public Sub CloseConn()
        '
        If Not conn Is Nothing Then
            If Not conn.State = ConnectionState.Closed Then
                conn.Close()
            End If
        End If
        '
    End Sub
    '
    '-------------------------------------------------------------------------------------------------------
    '--- LIMPAR PARAMETROS
    '-------------------------------------------------------------------------------------------------------
    Public Sub LimparParametros()
        ParamList.Clear()
    End Sub
    '
    '-------------------------------------------------------------------------------------------------------
    '--- ADICIONAR PARAMETROS
    '-------------------------------------------------------------------------------------------------------
    Public Sub AdicionarParametros(nomeParametro As String, valorParametro As Object)
        ParamList.Add(New SqlParameter(nomeParametro, valorParametro))
    End Sub
    '
    '-------------------------------------------------------------------------------------------------------
    '--- EXECUTAR INSERT, UPDATE, DELETE
    '-------------------------------------------------------------------------------------------------------
    Public Function ExecutarManipulacao(comandType As CommandType, nomeStoredProcedureOuTextoSQL As String) As Object
        '
        Try
            '
            If conn.State = ConnectionState.Closed Then Connect()
            '
            cmd = New SqlConnection().CreateCommand
            cmd.Connection = conn
            cmd.CommandType = comandType
            cmd.CommandText = nomeStoredProcedureOuTextoSQL
            cmd.CommandTimeout = 7200
            '
            ParamList.ForEach(Sub(p) cmd.Parameters.Add(p))
            '
            Return cmd.ExecuteScalar
            '
        Catch ex As Exception
            '
            Throw ex
            '
        End Try
        '
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    '--- EXECUTAR CONSULTA
    '-------------------------------------------------------------------------------------------------------
    Public Function ExecutarConsulta(comandType As CommandType, nomeStoredProcedureOuTextoSQL As String) As DataTable
        Try
            '
            If conn.State = ConnectionState.Closed Then Connect()
            '
            cmd = New SqlConnection().CreateCommand
            cmd.Connection = conn
            cmd.CommandType = comandType
            cmd.CommandText = nomeStoredProcedureOuTextoSQL
            cmd.CommandTimeout = 7200
            '
            ParamList.ForEach(Sub(p) cmd.Parameters.Add(p))

            Dim sqlDA As SqlDataAdapter = New SqlDataAdapter(cmd)
            '
            Dim dtTable As DataTable = New DataTable()
            '
            sqlDA.Fill(dtTable)
            '
            Return dtTable
            '
        Catch ex As Exception
            '
            Throw ex
            '
        End Try
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' EXECUTAR UMA QUERY
    '-------------------------------------------------------------------------------------------------------
    Public Function ExecuteQuery(ByVal strCmdTxt As String) As Boolean
        Dim intRows As Integer
        '
        Try
            If conn.State = ConnectionState.Closed Then Connect()
            '
            ' VARIÁVEIS DO COMMAND
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandText = strCmdTxt
            cmd.CommandType = CommandType.Text
            '
            'EXECUTA QUERY
            intRows = cmd.ExecuteNonQuery()

        Catch ex As Exception
            Throw ex
        Finally
            CloseConn()
        End Try
        '
        ' RETORNA A QUANTIDADE DE REGISTROS
        If intRows > 0 Then
            ExecuteQuery = True
        Else
            ExecuteQuery = False
        End If
        '
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' EXECUTAR UM COMMAND E OBTER DATAREADER
    '----------------------------------------------------------------------------
    Public Function ExecuteAndGetReader(ByVal strCmdTxt As String) As SqlDataReader
        '
        If conn.State = ConnectionState.Closed Then
            Try
                Connect()
            Catch ex As Exception
                Throw ex
            End Try
        End If
        '
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandText = strCmdTxt
        cmd.CommandType = CommandType.Text
        '
        Try
            Return cmd.ExecuteReader
            CloseConn()
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' EXECUTAR UM PROCEDURE COM PARÂMETROS
    '----------------------------------------------------------------------------
    Public Function ExecuteProcedure(ByVal myProcedureName As String, Optional ByVal myParam As SqlParameterCollection = Nothing) As String
        If conn.State = ConnectionState.Closed Then
            Try
                Connect()
            Catch ex As Exception
                Throw ex
            End Try
        End If
        '
        cmd = New SqlCommand
        cmd.Connection = conn
        cmd.CommandText = myProcedureName
        cmd.CommandType = CommandType.StoredProcedure
        '
        If Not IsNothing(myParam) Then
            For i As Integer = 0 To myParam.Count - 1
                cmd.Parameters.AddWithValue(myParam(i).ParameterName, myParam(i).Value)
            Next i
        End If
        '
        Try
            ExecuteProcedure = cmd.ExecuteScalar
        Catch ex As SqlException
            Throw ex
        Finally
            conn.Close()
            ExecuteProcedure = ""
        End Try
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' EXECUTAR UM PROCEDURE COM PARÂMETROS E OBTER O IDENTITY
    '----------------------------------------------------------------------------
    Public Function ExecuteProcedureID(ByVal myProcedureName As String, Optional ByVal myParam As SqlParameterCollection = Nothing) As String
        Try
            If conn.State = ConnectionState.Closed Then Connect()
            '
            cmd = New SqlCommand
            cmd.Connection = conn
            cmd.CommandText = myProcedureName
            cmd.CommandType = CommandType.StoredProcedure
            '
            If Not IsNothing(myParam) Then
                For i As Integer = 0 To myParam.Count - 1
                    cmd.Parameters.AddWithValue(myParam(i).ParameterName, myParam(i).Value)
                Next i
            End If
            '
            Dim obj = cmd.ExecuteScalar

            Return obj

            'Return cmd.ExecuteScalar
        Catch ex As Exception
            Throw ex
        Finally
            CloseConn()
        End Try
        '
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' EXECUTAR UM COMMAND E OBTER DATATABLE
    '----------------------------------------------------------------------------
    Public Function ExecuteConsultaSQL_DataTable(nomeStoredProcedureOuTextoSQL As String,
                                                 Optional ByVal myParam As SqlParameterCollection = Nothing) As DataTable
        Try
            '--- Abre a conexão
            If conn.State = ConnectionState.Closed Then Connect()
            '
            '--- cria o commando
            cmd = conn.CreateCommand
            If IsNothing(myParam) Then
                cmd.CommandType = CommandType.Text
            Else
                cmd.CommandType = CommandType.StoredProcedure
                For Each sqlparameter As SqlParameter In myParam
                    'cmd.Parameters.Add(New SqlParameter(sqlparameter.ParameterName, sqlparameter.Value))
                    cmd.Parameters.AddWithValue(sqlparameter.ParameterName, sqlparameter.Value)
                Next
            End If
            cmd.CommandText = nomeStoredProcedureOuTextoSQL
            cmd.CommandTimeout = 1800
            '
            '--- cria o Adapter e o DataTable
            Dim sqlDtAdapter As New SqlDataAdapter(cmd)
            Dim dtTable As New DataTable
            '
            '--- preenche o DataTable
            sqlDtAdapter.Fill(dtTable)
            '
            Return dtTable
        Catch ex As Exception
            Throw ex
        Finally
            CloseConn()
        End Try
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' EXECUTA UM DATAREADER E RETORNA UM DATASET
    '----------------------------------------------------------------------------
    Public Sub PreencheDataSetDeDataReader(ByVal ds As DataSet, ByVal table As String, ByVal dr As IDataReader)
        '
        ' Cria um  xxxDataAdapter do mesmo tipo de um DataReader
        Dim tipoDataReader As Type = CObj(dr).GetType
        Dim nomeTipo As String = tipoDataReader.FullName.Replace("DataReader", "DataAdapter")
        Dim tipoDataAdapter As Type = tipoDataReader.Assembly.GetType(nomeTipo)
        Dim da As Object = Activator.CreateInstance(tipoDataAdapter)

        ' invoca o método protegido Fill que toma um objeto IDataReader
        Dim args() As Object = {ds, table, dr, 0, 999999}
        tipoDataAdapter.InvokeMember("Fill", BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, da, args)

        ' fecha o DataReader
        dr.Close()
        '
    End Sub
    '
End Class
