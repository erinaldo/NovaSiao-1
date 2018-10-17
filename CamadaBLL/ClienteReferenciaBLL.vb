Imports CamadaDAL
'Imports System.Data.SqlClient

Public Class ClienteReferenciaBLL
    '
    '-------------------------------------------------------------------------------------------------------
    ' GET REFERENCIAS DO CLIENTE PELO IDPESSOA
    '-------------------------------------------------------------------------------------------------------
    Public Function ClienteReferencia_GET_PorID(myID As Integer) As DataTable
        Dim db As New AcessoDados
        '
        db.LimparParametros()
        db.AdicionarParametros("@IDPessoa", myID)
        '
        Try
            Return db.ExecutarConsulta(CommandType.StoredProcedure, "uspCliente_Referencia_GET_PorID")
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-------------------------------------------------------------------------------------------------------
    ' SALVA REFERENCIAS DO CLIENTE
    '-------------------------------------------------------------------------------------------------------
    Public Function ClienteReferencia_INSERT(myID As Integer, tblRefs As DataTable) As Boolean
        '--- Apaga todas as referências anteriores do Cliente
        Dim db As New AcessoDados
        '
        Try
            db.LimparParametros()
            db.AdicionarParametros("@IDPessoa", myID)
            db.ExecutarManipulacao(CommandType.StoredProcedure, "uspCliente_Referencia_DELETE_PorID")
            '
            '--- verifica se existe algum ROW no tblRefs
            If tblRefs.Rows.Count < 1 Then
                Return True
                Exit Function
            End If
            '
            ' se houver pelo menos um ROW então insere na tabela
            For Each r As DataRow In tblRefs.Rows
                db.LimparParametros()
                ' ADICIONA OS PARÂMETROS
                db.AdicionarParametros("@IDPessoa", myID)
                db.AdicionarParametros("@ReferenciaNome", r("ReferenciaNome"))
                db.AdicionarParametros("@Afinidade", r("Afinidade"))
                db.AdicionarParametros("@ReferenciaTelefone", r("ReferenciaTelefone"))

                'PARA UM REGISTRO NOVO - INSERT
                db.ExecutarManipulacao(CommandType.StoredProcedure, "uspCliente_Referencia_Inserir")
            Next
            '
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
