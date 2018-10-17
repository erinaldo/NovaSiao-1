Imports CamadaDTO
Imports CamadaDAL
Imports System.Data.SqlClient
'
Public Class FuncionarioBLL
    '
    '-----------------------------------------------------------------------------------------------------
    ' GET LIST OF
    '-----------------------------------------------------------------------------------------------------
    Public Function GetFuncionarios() As List(Of clFuncionario)
        Dim objdb As New AcessoDados
        Dim strSql As String = ""

        strSql = "SELECT * FROM qryFuncionario"

        Dim dr As SqlDataReader = objdb.ExecuteAndGetReader(strSql)
        Dim lista As New List(Of clFuncionario)
        While dr.Read
            Dim func As clFuncionario = New clFuncionario
            '
            func.IDPessoa = IIf(IsDBNull(dr("IDFuncionario")), Nothing, dr("IDFuncionario")) '1
            func.Cadastro = IIf(IsDBNull(dr("Cadastro")), String.Empty, dr("Cadastro")) '2
            func.CPF = IIf(IsDBNull(dr("CPF")), String.Empty, dr("CPF")) '4
            func.NascimentoData = IIf(IsDBNull(dr("NascimentoData")), Nothing, dr("NascimentoData")) '3
            func.Sexo = IIf(IsDBNull(dr("Sexo")), Nothing, dr("Sexo")) '3
            func.Identidade = IIf(IsDBNull(dr("Identidade")), String.Empty, dr("Identidade")) '3
            func.IdentidadeOrgao = IIf(IsDBNull(dr("IdentidadeOrgao")), String.Empty, dr("IdentidadeOrgao")) '3
            func.IdentidadeData = IIf(IsDBNull(dr("IdentidadeData")), Nothing, dr("IdentidadeData")) '3
            func.Endereco = IIf(IsDBNull(dr("Endereco")), String.Empty, dr("Endereco")) '5
            func.Bairro = IIf(IsDBNull(dr("Bairro")), String.Empty, dr("Bairro")) '6 
            func.Cidade = IIf(IsDBNull(dr("Cidade")), String.Empty, dr("Cidade")) '7 
            func.UF = IIf(IsDBNull(dr("UF")), String.Empty, dr("UF")) '8
            func.CEP = IIf(IsDBNull(dr("CEP")), String.Empty, dr("CEP")) '9
            func.TelefoneA = IIf(IsDBNull(dr("TelefoneA")), String.Empty, dr("TelefoneA")) '10
            func.TelefoneB = IIf(IsDBNull(dr("TelefoneB")), String.Empty, dr("TelefoneB")) '11
            func.Email = IIf(IsDBNull(dr("Email")), String.Empty, dr("Email")) '12
            func.AdmissaoData = IIf(IsDBNull(dr("AdmissaoData")), Nothing, dr("AdmissaoData")) '13
            func.Ativo = IIf(IsDBNull(dr("Ativo")), 0, dr("Ativo")) '14
            func.Vendedor = IIf(IsDBNull(dr("Vendedor")), Nothing, dr("Vendedor")) '15
            func.ApelidoFuncionario = IIf(IsDBNull(dr("ApelidoFuncionario")), String.Empty, dr("ApelidoFuncionario")) '16
            func.Comissao = IIf(IsDBNull(dr("Comissao")), Nothing, dr("Comissao")) '17
            func.VendaTipo = IIf(IsDBNull(dr("VendaTipo")), Nothing, dr("VendaTipo")) '18
            func.VendedorAtivo = IIf(IsDBNull(dr("VendedorAtivo")), Nothing, dr("VendedorAtivo")) '14
            lista.Add(func)
            '
        End While
        dr.Close()
        Return lista
    End Function
    '
    '-----------------------------------------------------------------------------------------------------
    ' GET DATATABLE
    '-----------------------------------------------------------------------------------------------------
    Public Function GetFuncionarios_DT(Optional myWhere As String = "") As DataTable
        Dim objdb As New AcessoDados
        Dim strSql As String = ""
        '
        If Len(myWhere) = 0 Then
            strSql = "SELECT * FROM qryFuncionario"
        Else
            If Left(myWhere, 8) = "ORDER BY" Then
                strSql = "SELECT * FROM qryFuncionario " & myWhere
            Else
                strSql = "SELECT * FROM qryFuncionario WHERE " & myWhere
            End If
        End If
        '
        Try
            Return objdb.ExecuteConsultaSQL_DataTable(strSql)
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------
    ' UPDATE
    '-----------------------------------------------------------------------------------------------------
    Public Function AtualizaFuncionario_Procedure_ID(ByVal _func As clFuncionario) As Long
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand

        'Adiciona os Parâmetros
        Conn.Parameters.Add(New SqlParameter("@IDPessoa", _func.IDPessoa))
        Conn.Parameters.Add(New SqlParameter("@Cadastro", _func.Cadastro))
        Conn.Parameters.Add(New SqlParameter("@CPF", _func.CPF))
        Conn.Parameters.Add(New SqlParameter("@Sexo", _func.Sexo))
        Conn.Parameters.Add(New SqlParameter("@NascimentoData", _func.NascimentoData))
        Conn.Parameters.Add(New SqlParameter("@Identidade", _func.Identidade))
        Conn.Parameters.Add(New SqlParameter("@IdentidadeOrgao", _func.IdentidadeOrgao))
        Conn.Parameters.Add(New SqlParameter("@IdentidadeData", _func.IdentidadeData))
        Conn.Parameters.Add(New SqlParameter("@Endereco", _func.Endereco))
        Conn.Parameters.Add(New SqlParameter("@Bairro", _func.Bairro))
        Conn.Parameters.Add(New SqlParameter("@Cidade", _func.Cidade))
        Conn.Parameters.Add(New SqlParameter("@UF", _func.UF))
        Conn.Parameters.Add(New SqlParameter("@CEP", _func.CEP))
        Conn.Parameters.Add(New SqlParameter("@TelefoneA", _func.TelefoneA))
        Conn.Parameters.Add(New SqlParameter("@TelefoneB", _func.TelefoneB))
        Conn.Parameters.Add(New SqlParameter("@Email", _func.Email))
        Conn.Parameters.Add(New SqlParameter("@AdmissaoData", _func.AdmissaoData))
        Conn.Parameters.Add(New SqlParameter("@Ativo", _func.Ativo))
        Conn.Parameters.Add(New SqlParameter("@Vendedor", _func.Vendedor))
        Conn.Parameters.Add(New SqlParameter("@ApelidoFuncionario", _func.ApelidoFuncionario))
        Conn.Parameters.Add(New SqlParameter("@Comissao", _func.Comissao))
        Conn.Parameters.Add(New SqlParameter("@VendaTipo", _func.VendaTipo))
        Conn.Parameters.Add(New SqlParameter("@VendedorAtivo", _func.VendedorAtivo))
        '
        Try
            Return objDB.ExecuteProcedureID("uspFuncionario_Alterar", Conn.Parameters)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------
    ' INSERT
    '-----------------------------------------------------------------------------------------------------
    Public Function SalvaNovoFuncionario_Procedure_ID(ByVal _func As clFuncionario) As Long
        Dim objDB As New AcessoDados
        Dim Conn As New SqlCommand

        'Adiciona os Parâmetros
        Conn.Parameters.Add(New SqlParameter("@Cadastro", _func.Cadastro))
        Conn.Parameters.Add(New SqlParameter("@CPF", _func.CPF))
        Conn.Parameters.Add(New SqlParameter("@Sexo", _func.Sexo))
        Conn.Parameters.Add(New SqlParameter("@NascimentoData", _func.NascimentoData))
        Conn.Parameters.Add(New SqlParameter("@Identidade", _func.Identidade))
        Conn.Parameters.Add(New SqlParameter("@IdentidadeOrgao", _func.IdentidadeOrgao))
        Conn.Parameters.Add(New SqlParameter("@IdentidadeData", _func.IdentidadeData))
        Conn.Parameters.Add(New SqlParameter("@Endereco", _func.Endereco))
        Conn.Parameters.Add(New SqlParameter("@Bairro", _func.Bairro))
        Conn.Parameters.Add(New SqlParameter("@Cidade", _func.Cidade))
        Conn.Parameters.Add(New SqlParameter("@UF", _func.UF))
        Conn.Parameters.Add(New SqlParameter("@CEP", _func.CEP))
        Conn.Parameters.Add(New SqlParameter("@TelefoneA", _func.TelefoneA))
        Conn.Parameters.Add(New SqlParameter("@TelefoneB", _func.TelefoneB))
        Conn.Parameters.Add(New SqlParameter("@Email", _func.Email))
        Conn.Parameters.Add(New SqlParameter("@AdmissaoData", _func.AdmissaoData))
        Conn.Parameters.Add(New SqlParameter("@Ativo", _func.Ativo))
        Conn.Parameters.Add(New SqlParameter("@Vendedor", _func.Vendedor))
        Conn.Parameters.Add(New SqlParameter("@ApelidoFuncionario", _func.ApelidoFuncionario))
        Conn.Parameters.Add(New SqlParameter("@Comissao", _func.Comissao))
        Conn.Parameters.Add(New SqlParameter("@VendaTipo", _func.VendaTipo))
        'Conn.Parameters.Add(New SqlParameter("@VendedorAtivo", _func.VendedorAtivo)) 'PADRAO 'TRUE'
        '
        Try
            Return objDB.ExecuteProcedureID("uspFuncionario_Inserir", Conn.Parameters)
        Catch ex As Exception
            Throw ex
            Return Nothing
        End Try
        '
    End Function
    '
    '-----------------------------------------------------------------------------------------------------
    ' DELETE
    '-----------------------------------------------------------------------------------------------------
    Public Function DeletaFuncionario_PorID(ByVal _IDFuncionario As Long) As Boolean
        MsgBox("Ainda ão implementado")
        Return False
    End Function
    '
    '-----------------------------------------------------------------------------------------------------
    ' GET QTDE DE VENDAS POR VENDEDOR 
    '-----------------------------------------------------------------------------------------------------
    Public Function FuncionarioVendedor_GetVendas(myID As Integer) As Integer?
        Dim SQL As New SQLControl
        Dim r As DataRow
        '
        Try
            SQL.ExecQuery("SELECT COUNT(IDVendedor) AS RETORNO FROM tblVenda WHERE IDVendedor = 1")
            '
            If SQL.HasException(True) Then
                Return Nothing
            End If
            '
            r = SQL.DBDT.Rows(0)
            '
            Return CInt(r("Retorno"))
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function

End Class
