Imports CamadaDAL
Imports CamadaDTO
'
Public Enum SaidaOrigem
    APagar = 1 ' IDOrigem: tblAPagar
    Creditos = 3 ' IDOrigem : tblCreditos
End Enum
'
Public Class SaidaBLL
    '===================================================================================================
    ' OBTER LISTA DE TODAS SAIDAS INFORMANDO O TIPO DE ORIGEM E PELO IDORIGEM 
    '===================================================================================================
    Public Function Saida_GET_PorOrigemID(Origem As SaidaOrigem, IDOrigem As Integer) As List(Of clSaidas)
        Dim db As New AcessoDados
        '
        '--- limpa os paramentros
        db.LimparParametros()
        '
        '--- adiciona os parametros
        db.AdicionarParametros("@Origem", CByte(Origem)) ' ORIGEM APAGAR / CREDITOS
        db.AdicionarParametros("@IDOrigem", IDOrigem)
        '
        Try
            Dim listS As New List(Of clSaidas)
            Dim dt As DataTable = db.ExecutarConsulta(CommandType.StoredProcedure, "uspSaida_GET_PorOrigemID")
            '
            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    Dim clS As New clSaidas
                    '
                    With clS
                        .IDSaida = IIf(IsDBNull(r("IDSaida")), Nothing, r("IDSaida"))
                        .Origem = Origem
                        .IDOrigem = IDOrigem
                        .IDConta = IIf(IsDBNull(r("IDConta")), Nothing, r("IDConta"))
                        .Conta = IIf(IsDBNull(r("Conta")), String.Empty, r("Conta"))
                        .SaidaData = IIf(IsDBNull(r("SaidaData")), Nothing, r("SaidaData"))
                        .SaidaValor = IIf(IsDBNull(r("SaidaValor")), 0, r("SaidaValor"))
                        .Creditar = IIf(IsDBNull(r("Creditar")), Nothing, r("Creditar"))
                        .IDCaixa = IIf(IsDBNull(r("IDCaixa")), Nothing, r("IDCaixa"))
                        .IDFilial = IIf(IsDBNull(r("IDFilial")), Nothing, r("IDFilial"))
                        .ApelidoFilial = IIf(IsDBNull(r("ApelidoFilial")), String.Empty, r("ApelidoFilial"))
                        .Observacao = IIf(IsDBNull(r("Observacao")), String.Empty, r("Observacao"))
                    End With
                    '
                    '--- Adiciona o ROW na listagem
                    listS.Add(clS)
                    '
                Next
            End If
            '
            Return listS
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
End Class
