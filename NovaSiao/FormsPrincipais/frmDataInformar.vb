Public Class frmDataInformar
    '
    Private _DataTipo As DataTipo
    Private _formOrigem As Form
    Property propDataInfo As Date? = Nothing
    '
#Region "NEW | LOAD"
    '
    Sub New(SubTitulo As String, myDataTipo As DataTipo, DataPadrao As Date, formOrigem As Form)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        lblSubTitulo.Text = SubTitulo
        DefinirDataLimite(myDataTipo)
        '
        If DataPadrao > dtpDateInfo.MaxDate Then
            dtpDateInfo.Value = dtpDateInfo.MaxDate
        ElseIf DataPadrao < dtpDateInfo.MinDate Then
            dtpDateInfo.Value = dtpDateInfo.MinDate
        Else
            dtpDateInfo.Value = DataPadrao
        End If
        '
        _DataTipo = myDataTipo
        _formOrigem = formOrigem
        '
    End Sub
    '
    '--- DEFINIR AS DATAS LIMITES PELO DataTipo
    Private Sub DefinirDataLimite(dataTipo As DataTipo)
        '
        Select Case dataTipo
            Case DataTipo.Passado
                dtpDateInfo.MaxDate = Now.AddDays(-1)
            Case DataTipo.PassadoPresente
                dtpDateInfo.MaxDate = Now
            Case DataTipo.Futuro
                dtpDateInfo.MinDate = Now.AddDays(1)
            Case DataTipo.FuturoPresente
                dtpDateInfo.MinDate = Now
        End Select
        '
    End Sub
    '
#End Region
    '
#Region "BUTTONS FUNCTION"
    '
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        '
        propDataInfo = dtpDateInfo.Value
        DialogResult = DialogResult.OK
        '
    End Sub
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        '
        propDataInfo = Nothing
        DialogResult = DialogResult.Cancel
        '
    End Sub
    '
#End Region
    '
#Region "VISUAL EFFECTS"
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If Not IsNothing(_formOrigem) Then
            Dim pnl As Panel = _formOrigem.Controls("Panel1")
            pnl.BackColor = Color.Silver
        End If
    End Sub
    '
    Private Sub frmProdutoProcurar_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If Not IsNothing(_formOrigem) Then
            Dim pnl As Panel = _formOrigem.Controls("Panel1")
            pnl.BackColor = Color.SlateGray
        End If
    End Sub
    '
#End Region
    '
End Class
