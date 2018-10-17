Imports CamadaBLL

Public Class frmContaDataPadrao
    Private _IDFilial As Integer?
    Private _Filial As String
    Private _IDConta As Int16?
    Private _Conta As String
    Private _DataInicial As Date?
    '
    Private _formOrigem As Form
    '
    Private controlaAlteracao As Boolean = False
    '
#Region "SUB NEW | PROPERTY"
    '
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '
        '--- verifica a conta padrao
        _Conta = ObterDefault("ContaDescricao")
        _IDConta = Obter_ContaPadrao()
        _IDFilial = Obter_FilialPadrao()
        _Filial = ObterDefault("FilialDescricao")
        txtConta.Text = _Conta
        txtFilial.Text = _Filial
        '
        _DataInicial = Obter_DataPadrao()
        calDataPadrao.SetDate(_DataInicial)
        '
        VerficaBloqueioDataConta()
        '
        controlaAlteracao = True
        '
    End Sub
    '
#End Region
    '
#Region "BUTTONS FUNCTIONS"
    '
    Private Sub btnDefinir_Click(sender As Object, e As EventArgs) Handles btnDefinir.Click
        '
        '--- verifica os controles
        If VerificaCampos() = False Then Exit Sub
        '
        DirectCast(ParentForm, frmPrincipal).propDataPadrao = calDataPadrao.SelectionStart
        DirectCast(ParentForm, frmPrincipal).propContaPadrao = txtConta.Text
        DirectCast(ParentForm, frmPrincipal).propFilialPadrao = txtFilial.Text
        SetarDefault("FilialPadrao", _IDFilial)
        SetarDefault("ContaPadrao", _IDConta)
        '
        Me.Close()
        If IsNothing(_formOrigem) Then MostraMenuPrincipal()
        '
    End Sub
    '
    '--- VERIFICA OS VALORES ANTES DE INSERIR
    Private Function VerificaCampos() As Boolean
        '
        '--- Verifica os controles
        '---------------------------------------------------------------------------------------
        If IsNothing(_IDFilial) Or txtFilial.Text.Trim.Length = 0 Then
            MessageBox.Show("Nenhuma FILIAL padrão foi escolhida...",
                            "Filial Vazia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtFilial.Focus()
            Return False
        End If
        '
        If IsNothing(_IDConta) Or txtConta.Text.Trim.Length = 0 Then
            MessageBox.Show("Nenhuma CONTA padrão foi escolhida...",
                            "Conta Vazia", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtConta.Focus()
            Return False
        End If
        '
        If IsNothing(calDataPadrao.SelectionStart) Then
            MessageBox.Show("Escolha a DATA na qual você deseja que seja a data padrão do sistema...",
                            "Escolha a Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
            calDataPadrao.Focus()
            Return False
        End If
        '
        Return True
        '
    End Function
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click, btnClose.Click
        '
        DialogResult = DialogResult.Cancel
        Me.Close()
        If IsNothing(_formOrigem) Then MostraMenuPrincipal()
        '
    End Sub
    '
    Private Sub btnFilialEscolher_Click(sender As Object, e As EventArgs) Handles btnFilialEscolher.Click
        '
        '--- Abre o frmFilial
        Dim fFil As New frmFilial(True, Me)
        '
        fFil.ShowDialog()
        '
        If fFil.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        If fFil.IDFilialEscolhida <> _IDFilial Then
            _IDConta = Nothing
            txtConta.Clear()
        End If
        '
        _IDFilial = fFil.IDFilialEscolhida
        _Filial = fFil.FilialEscolhida
        txtFilial.Text = fFil.FilialEscolhida
        '
    End Sub

    Private Sub btnContaEscolher_Click(sender As Object, e As EventArgs) Handles btnContaEscolher.Click
        '
        '--- Abre o frmContas
        Dim frmConta As New frmContas(True, _IDFilial, Me)
        '
        frmConta.ShowDialog()
        '
        If frmConta.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        _Conta = frmConta.ContaEscolhida
        txtConta.Text = frmConta.ContaEscolhida
        _IDConta = frmConta.IDContaEscolhida
        '
        _IDFilial = frmConta.IDFilialEscolhida
        _Filial = frmConta.FilialEscolhida
        txtFilial.Text = frmConta.FilialEscolhida
        '
        VerficaBloqueioDataConta()
        '
    End Sub
    '
#End Region
    '
#Region "OUTRAS FUNCOES"
    '
    '--- VERIFICA BLOQUEIO DA DATA INICIAL DA CONTA ESCOLHIDA
    Private Sub VerficaBloqueioDataConta()
        '
        Dim mBLL As New MovimentacaoBLL
        '
        Try
            Dim blData As Date? = mBLL.Conta_GetDataBloqueio(_IDConta)
            '
            If Not IsNothing(blData) Then
                blData = CDate(blData).AddDays(1)
                '
                '-- verifica se a data adicionada é DOMINGO, sendo adiciona um dia
                If CDate(blData).DayOfWeek = DayOfWeek.Sunday Then CDate(blData).AddDays(1)
                '
                '-- define a propriedade DATA PADRAO
                calDataPadrao.MinDate = blData
                calDataPadrao.MaxDate = Today
                calDataPadrao.SetDate(blData)
            Else '-- Se não houver DataBloqueio definida escolhe o dia de HOJE
                MessageBox.Show("A CONTA PADRÃO escolhida: " & _Conta.ToUpper & vbNewLine &
                                "ainda não tem data de bloqueio definida..." & vbNewLine &
                                "A DATA PADRÃO do sistema será escolhida como" & vbNewLine &
                                "DATA ATUAL: " & Now.ToLongDateString, "Data Padrão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '
                calDataPadrao.MinDate = Today.AddYears(-10)
                calDataPadrao.SetDate(Today.ToShortDateString)
                calDataPadrao.MaxDate = Today
                '
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Ocorreu uma exceção ao obter a Data Padrão da Conta Escolhida..." & vbNewLine &
            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    '--- EXECUTAR A FUNCAO DO BOTAO QUANDO PRESSIONA A TECLA (+) NO CONTROLE
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilial.KeyDown, txtConta.KeyDown
        '
        Dim ctr As Control = DirectCast(sender, Control)
        '
        If e.KeyCode = Keys.Add Then
            e.Handled = True
            Select Case ctr.Name
                Case "txtFilial"
                    btnFilialEscolher_Click(New Object, New EventArgs)
                    txtFilial.Text = txtFilial.Text.Replace("+", "")
                    txtFilial.SelectAll()
                Case "txtConta"
                    btnContaEscolher_Click(New Object, New EventArgs)
                    txtConta.Text = txtConta.Text.Replace("+", "")
                    txtConta.SelectAll()
            End Select
        ElseIf e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Tab Then
            e.Handled = True
            SendKeys.Send("{Tab}")
        Else
            e.Handled = True
            e.SuppressKeyPress = True
        End If
        '
    End Sub
    '
#End Region
    '
#Region "EFEITOS SUB-FORMULARIO PADRAO"
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub frmAPagarItem_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        '
        If IsNothing(_formOrigem) Then Exit Sub
        '
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.Silver
        '
    End Sub
    '
    Private Sub frmDespesaTipo_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        '
        If IsNothing(_formOrigem) Then Exit Sub
        '
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.SlateGray
        '
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CONSTRUIR UMA BORDA NO FORMULÁRIO
    '-------------------------------------------------------------------------------------------------
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        MyBase.OnPaintBackground(e)

        Dim rect As New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
        Dim pen As New Pen(Color.SlateGray, 3)

        e.Graphics.DrawRectangle(pen, rect)
    End Sub
    '
#End Region
    '
End Class
