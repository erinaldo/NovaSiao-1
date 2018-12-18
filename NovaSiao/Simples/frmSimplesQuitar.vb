Imports CamadaBLL

Public Class frmSimplesQuitar
    '
    Private _formOrigem As Form
    Private _IDFilialDestino As Integer?
    Private _IDFilialOrigem As Integer
    Private _FilialOrigem As String
    Private _EntradaSaida As Boolean
    '
    Public Sub New(formOrigem As Form,
                   EntradaSaida As Boolean,
                   IDFilialOrigem As Integer,
                   FilialOrigem As String,
                   Optional IDFilialDestino As Integer? = Nothing,
                   Optional FilialDestino As String = "")
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        _formOrigem = formOrigem
        _IDFilialOrigem = IDFilialOrigem
        _EntradaSaida = EntradaSaida
        lblFilial.Text = FilialOrigem
        CarregaCmbContaOrigem()
        '
        ' obtem a data padrao
        txtEntradaData.Text = If(Obter_DataPadrao(), "")
        '
        If Not IsNothing(IDFilialDestino) Then
            _IDFilialDestino = IDFilialDestino
            txtFilialDestino.Text = FilialDestino
            CarregaCmbContaDestino()
        End If
        '
        If EntradaSaida = True Then '--- RECEBER | SIMPLES SAIDA
            lblTitulo.Text = "Simples Receber"
            lblContaOrigem.Text = "Conta Creditada"
            lblFilialDestino.Text = "Filial | Origem"
            lblContaDestino.Text = "Conta | Origem"
            lblData.Text = "Entrada Data"
            lblValor.Text = "Valor Recebido"

        ElseIf EntradaSaida = False Then '--- PAGAR | SIMPLES ENTRADA
            lblTitulo.Text = "Simples Quitar"
            lblContaOrigem.Text = "Conta Debitada"
            lblFilialDestino.Text = "Filial | Destino"
            lblContaDestino.Text = "Conta | Destino"
            lblData.Text = "Pag. Data"
            lblValor.Text = "Valor Pago"

        End If
        '
    End Sub
    '
    '
#Region "PREENCHE COMBOS"
    '
    Private Sub CarregaCmbContaOrigem()
        '
        Dim EntBLL As New MovimentacaoBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            Dim dt As DataTable = EntBLL.Contas_GET_PorIDFilial_DT(_IDFilialOrigem)
            '
            With cmbIDContaOrigem
                .DataSource = dt
                .DisplayMember = "Conta"
                .ValueMember = "IDConta"
            End With
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao obter a listagem das Contas da Filial..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Sub
    '
    Private Sub CarregaCmbContaDestino()
        '
        '--- verifica se existe FilialDestino
        If IsNothing(_IDFilialDestino) Then
            cmbIDContaDestino.DataSource = Nothing
            cmbIDContaDestino.Items.Clear()
        End If
        '
        Dim EntBLL As New MovimentacaoBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            Dim dt As DataTable = EntBLL.Contas_GET_PorIDFilial_DT(_IDFilialDestino)
            '
            With cmbIDContaDestino
                .DataSource = dt
                .DisplayMember = "Conta"
                .ValueMember = "IDConta"
            End With
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao obter a listagem das Contas da Filial..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Sub
    '
#End Region
    '
#Region "BUTONS FUNCTION"
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub
    '
    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        '
        '--- Verifica se os Valores estão preenchidos e corretos
        If VerificarValores() = False Then
            Exit Sub
        End If
        '
        Dim simplesBLL As New SimplesMovimentacaoBLL
        '
        If _EntradaSaida Then '-- A RECEBER ================================================
            '
            '--- Verifica o valor do EmAberto da FilialOrigem
            '--------------------------------------------------------------------------------------------------------
            Try
                '--- Ampulheta ON
                Cursor = Cursors.WaitCursor
                '
                Dim totalAReceber As Double = simplesBLL.Simples_AReceberTotal_Filial(_IDFilialOrigem, _IDFilialDestino)
                '
                If totalAReceber < txtValor.Text Then
                    MessageBox.Show("O Valor que você deseja quitar é maior que o Valor Total do A Receber que há entre as Filiais: " &
                                    vbNewLine &
                                    lblFilial.Text.ToUpper & " e " & txtFilialDestino.Text.ToUpper _
                                    & vbNewLine & "Valor Total A Receber: " & Format(totalAReceber, "C"),
                                    "Valor Total",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return
                End If
                '
            Catch ex As Exception
                MessageBox.Show("Uma exceção ocorreu ao Verificar Valor de A Receber..." & vbNewLine &
                                ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                '--- Ampulheta OFF
                Cursor = Cursors.Default
            End Try
            '

            '
        Else '--- A PAGAR ===================================================================

        End If




        '--- Verifica se a DATA DA ENTRADA é permitida pelo sistema
        'If DataBloqueada(propEntrada.EntradaData, propEntrada.IDConta) Then
        '    Exit Sub
        'End If
        '
        '--- Insere o Pagamento
        '
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Efetuar Pagamento..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Sub
    '
    '--- BOTAO PROCURAR FILIAL
    Private Sub btnProcurarFilial_Click(sender As Object, e As EventArgs) Handles btnProcurarFilial.Click
        ProcurarEscolherFilial(txtFilialDestino)
    End Sub
    '
    '--- ESCOLHER FILIAL
    Private Sub ProcurarEscolherFilial(sender As Control)
        '
        Dim frmT As Form = Nothing
        Dim oldID As Integer? = _IDFilialDestino
        frmT = New frmFilialEscolher(Me, oldID)
        '
        ' revela o formulario dependendo do controle acionado
        frmT.ShowDialog()
        '
        ' ao fechar dialog verifica o resultado
        If frmT.DialogResult <> DialogResult.Cancel Then
            '
            '--- verify if is same Filial
            Dim IDFilialEscolha As Integer = DirectCast(frmT, frmFilialEscolher).propIdFilial_Escolha
            '
            If _IDFilialOrigem = IDFilialEscolha Then
                MessageBox.Show("Você escolheu a mesma Filial Atual:" & vbNewLine &
                                lblFilial.Text.ToUpper & vbNewLine &
                                "Não é possível possuir A Receber ou A Pagar para mesma filial." & vbNewLine &
                                "Favor escolher uma Filial diferente...",
                                "Mesma Filial Padrão",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtFilialDestino.Focus()
                Return
            End If
            '
            txtFilialDestino.Text = DirectCast(frmT, frmFilialEscolher).propFilial_Escolha
            _IDFilialDestino = IDFilialEscolha
            '
            ' Get CONTAS da Filial escolhida
            If If(oldID, 0) <> If(_IDFilialDestino, 0) Then
                '
                '--- Preenche o Combo ContaDestino
                CarregaCmbContaDestino()
                '
            End If
            '
            ' move focus
            txtFilialDestino.Focus()
            '
        End If
        '
    End Sub
    '
#End Region
    '
#Region "OUTRAS FUNCOES"
    '
    '------------------------------------------------------------------------------------------
    ' VERIFICACAO DOS CAMPOS NECESSARIOS
    '------------------------------------------------------------------------------------------
    Private Function VerificarValores() As Boolean
        Dim f As New FuncoesUtilitarias
        '
        '--- Campo Conta de Entrada
        If f.VerificaControlesForm(cmbIDContaOrigem, "Conta da Entrada") = False Then
            Return False
        End If
        '
        '--- Campo Data da Entrada
        If f.VerificaControlesForm(txtEntradaData, "Data da Entrada") = False Then
            Return False
        End If
        '
        '--- Campo DoValor
        If f.VerificaControlesForm(txtValor, "Valor da Entrada") = False Then
            Return False
        End If
        '
        If IsNumeric(txtValor.Text) AndAlso CDec(txtValor.Text) = 0 Then
            MessageBox.Show("O Valor da Entrada não pode ser igual a ZERO", "Valor Inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtValor.Focus()
            Return False
        End If
        '
        '--- Campo Observacao
        If txtObservacao.Text.Trim.Length = 0 Then
            txtObservacao.Text = ""
        Else
            txtObservacao.Text = txtObservacao.Text.Trim
        End If
        '
        '--- RETORNA TRUE
        Return True
        '
    End Function
#End Region '/ OUTRAS FUNCOES
    '
#Region "ATALHOS - NAVEGAÇÃO"
    '
    '------------------------------------------------------------------------------------------
    ' USAR TAB AO KEYPRESS ENTER
    '------------------------------------------------------------------------------------------
    Private Sub txtValor_KeyDown(sender As Object, e As KeyEventArgs) Handles _
            rbtTransferencia.KeyDown,
            txtValor.KeyDown,
            txtObservacao.KeyDown
        '
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    '-- CONVERTE A TECLA ESC EM CANCELAR
    '------------------------------------------------------------------------------------------
    Private Sub frm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            e.Handled = True
            '
            btnCancelar_Click(New Object, New EventArgs)
        End If
    End Sub
    '
#End Region
    '
#Region "EFEITOS SUB-FORMULARIO PADRAO"
    '
    '-------------------------------------------------------------------------------------------------
    ' CONSTRUIR UMA BORDA NO FORMULÁRIO
    '-------------------------------------------------------------------------------------------------
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        MyBase.OnPaintBackground(e)

        Dim rect As New Rectangle(0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
        Dim pen As New Pen(Color.DarkSlateGray, 3)

        e.Graphics.DrawRectangle(pen, rect)
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub frmAPagarItem_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.Silver
    End Sub
    '
    Private Sub frmAPagarItem_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.SlateGray
    End Sub
    '
#End Region
    '
End Class
