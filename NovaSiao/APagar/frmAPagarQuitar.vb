Imports CamadaBLL
Imports CamadaDTO

Public Class frmAPagarQuitar
    Private _formOrigem As Form
    Private bindSaida As New BindingSource
    Private _vlMax As Decimal = 0
    Private _VerAlteracao As Boolean = False
    Property propSaida As clSaidas
    Property propPagOrigem As clAPagar
    '
#Region "OPEN | LOAD | PROPRIEDADES"
    '
    Public Sub New(formOrigem As Form, _PagOrigem As clAPagar)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        propPagOrigem = _PagOrigem
        _formOrigem = formOrigem
        _vlMax = _PagOrigem.APagarValor - _PagOrigem.ValorPago - _PagOrigem.Desconto
        '
        '--- PREENCHE A OBJETO CLSAIDAS
        propSaida = New clSaidas
        bindSaida.DataSource = propSaida
        propSaida.IDOrigem = propPagOrigem.IDAPagar
        propSaida.SaidaData = Obter_DataPadrao()
        propSaida.IDConta = Obter_ContaPadrao()
        propSaida.Origem = 1 '--- ORIGEM IDAPAGAR
        propSaida.Creditar = False
        PreencheDataBinding()
        '
        '--- PREENCHE OS CAMPOS DOS CONTROLES
        txtValorPago.Text = FormatCurrency(_vlMax, 2)
        txtAcrescimo.Text = FormatCurrency(0, 2)
        CalculaValorPago()
        _VerAlteracao = True
        '
        '
    End Sub
    '
#End Region
    '
#Region "DATA BINDINGS"
    '------------------------------------------------------------------------------------------
    ' PREENCHE O DATABINDING
    '------------------------------------------------------------------------------------------
    Private Sub PreencheDataBinding()
        '
        lblSaidaValor.DataBindings.Add("Text", bindSaida, "SaidaValor", True, DataSourceUpdateMode.OnPropertyChanged)
        txtSaidaData.DataBindings.Add("Text", bindSaida, "SaidaData", True, DataSourceUpdateMode.OnPropertyChanged)
        txtObservacao.DataBindings.Add("Text", bindSaida, "Observacao", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' CARREGA OS COMBOBOX
        CarregaCmbConta()
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler lblSaidaValor.DataBindings("Text").Format, AddressOf FormatCUR
        '
    End Sub
    '
    Private Sub FormatCUR(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = FormatCurrency(e.Value, 2)
    End Sub
    '
#End Region
    '
#Region "PREENCHE COMBOS"
    '
    '------------------------------------------------------------------------------------------
    ' CARREGAR OS COMBOBOX
    '------------------------------------------------------------------------------------------
    '
    Private Sub CarregaCmbConta()
        Dim EntBLL As New MovimentacaoBLL
        Dim Filial As Integer
        '
        Try
            Filial = Obter_FilialPadrao()
        Catch ex As Exception
            MessageBox.Show("Ocorreu uma exceção inesperada ao obter a Filial Padrão:" & vbNewLine &
                ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        '
        Try
            Dim dt As DataTable = EntBLL.Contas_GET_PorIDFilial(Filial)
            '
            With cmbIDConta
                .DataSource = dt
                .DisplayMember = "Conta"
                .ValueMember = "IDConta"
                .DataBindings.Add("SelectedValue", bindSaida, "IDConta", True, DataSourceUpdateMode.OnPropertyChanged)
            End With
        Catch ex As Exception
            MessageBox.Show("Ocorreu uma exceção inesperada ao obter as lista de contas:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        '--- Verifica se a DATA DA ENTRADA é permitida pelo sistema
        If DataPermitida(propSaida.SaidaData, Obter_FilialPadrao, propSaida.IDConta) = False Then
            Exit Sub
        End If
        '
        '--- Veridica se o APAGAR é REAL ou PERIODICA
        '--- SE for PERIODICA torna a despesa em REAL
        If propPagOrigem.Origem = 3 Then
            '
            Dim dBLL As New DespesaPeriodicaBLL
            '
            Try
                propPagOrigem.IDAPagar = dBLL.DespesaPeriodica_TornarReal(propPagOrigem)
                propSaida.IDOrigem = propPagOrigem.IDAPagar
            Catch ex As Exception
                MessageBox.Show("Houve uma exceção ao TORNAR REAL o APagar Periodico:" & vbNewLine &
                                ex.Message, "Exceção Inesperada",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            '
        End If
        '
        '--- Insere a SAIDA
        Dim pBLL As New APagarBLL
        Dim newID As Integer?
        '
        Try
            newID = pBLL.Quitar_APagar(propSaida.IDOrigem, propSaida.SaidaValor,
                                       IIf(IsNumeric(txtAcrescimo.Text), CDbl(txtAcrescimo.Text), 0),
                                       propSaida.SaidaData, propSaida.IDConta, propSaida.Observacao)

            '
            If Not IsNumeric(newID) Then
                Throw New Exception(newID.ToString)
            Else
                propSaida.IDSaida = newID
                propSaida.Conta = cmbIDConta.Text
            End If
            '
            DialogResult = DialogResult.OK
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção ao QUITAR o APagaR:" & vbNewLine &
                            ex.Message, "Exceção Inesperada",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' VERIFICACAO DOS CAMPOS NECESSARIOS
    '------------------------------------------------------------------------------------------
    Private Function VerificarValores() As Boolean
        Dim f As New FuncoesUtilitarias
        '
        '--- Campo Conta de Entrada
        If f.VerificaControlesForm(cmbIDConta, "Conta da Entrada") = False Then
            Return False
        End If
        '
        '--- Campo Data da Entrada
        If f.VerificaControlesForm(txtSaidaData, "Data da Entrada") = False Then
            Return False
        End If
        '
        '--- Campo DoValor
        If f.VerificaControlesForm(txtValorPago, "Valor da Entrada") = False Then
            Return False
        End If
        '
        If IsNumeric(txtValorPago.Text) AndAlso CDec(txtValorPago.Text) = 0 Then
            MessageBox.Show("O Valor da Saída/Pagamento não pode ser igual a ZERO", "Valor Inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtValorPago.Focus()
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
    '
#End Region
    '
#Region "ATALHOS - NAVEGAÇÃO"
    '
    '------------------------------------------------------------------------------------------
    ' CRIA UM ATALHO PARA OS COMBO BOX
    '------------------------------------------------------------------------------------------
    Private Sub cmbFormaTipo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbIDConta.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            '
            Dim dt As DataTable = cmbIDConta.DataSource
            Dim rCount As Integer = dt.Rows.Count
            Dim item As Integer = e.KeyChar.ToString
            '
            If item <= rCount And item > 0 Then
                Dim Valor As Integer = dt.Rows(e.KeyChar.ToString - 1)("IDConta")
                '
                propSaida.IDConta = Valor
                cmbIDConta.SelectedValue = Valor
                '
            End If
        End If
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' USAR TAB AO KEYPRESS ENTER
    '------------------------------------------------------------------------------------------
    Private Sub txtValor_KeyDown(sender As Object, e As KeyEventArgs) Handles rbtTotal.KeyDown,
            txtValorPago.KeyDown, txtAcrescimo.KeyDown, txtObservacao.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
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
#Region "FUNCOES NECESSARIAS - CALCULOS"
    '
    '------------------------------------------------------------------------------------------
    ' CALCULA O VALOR DO CAMPO VALORPAGO = DOVALOR + ACRESCIMO - DESCONTO
    '------------------------------------------------------------------------------------------
    Private Sub CalculaValorPago()
        '
        Dim DoValor As Double = 0
        Dim Acresc As Double = 0
        Dim Desc As Double = 0
        '
        '--- Verifica o campo VALORPAGO
        If IsNumeric(txtValorPago.Text) Then
            DoValor = CDbl(txtValorPago.Text)
            '
            '--- verifica se o valor é maior para calcular o ACRESCIMO
            If DoValor > _vlMax Then
                '
                Dim dlgQ As New dlgAPagarQuitarAlteraValor(_vlMax, IIf(propPagOrigem.Origem = 3, True, False))
                '
                dlgQ.ShowDialog()
                '
                If dlgQ.DialogResult = 1 Then
                    '
                    '--- verifica qual o resultado da resposta
                    Select Case dlgQ.Resposta
                        Case 1 '--- Acresccentar no ACRESCIMO
                            txtAcrescimo.Text = Format(DoValor - _vlMax, "C")
                        Case 2 '--- ALTERAR o valor
                            propPagOrigem.APagarValor = DoValor + propPagOrigem.Desconto
                            _vlMax = propPagOrigem.APagarValor - propPagOrigem.ValorPago - propPagOrigem.Desconto
                        Case 3 '--- DESCARTAR a diferença
                            txtAcrescimo.Text = _vlMax
                    End Select
                    '
                Else
                    txtValorPago.Focus()
                End If
                '
                txtValorPago.Text = FormatCurrency(_vlMax, 2)
                DoValor = _vlMax
                '
            End If
            '
        End If
        '
        '--- Verifica o campo ACRESCIMO
        If IsNumeric(txtAcrescimo.Text) Then
            Acresc = CDbl(txtAcrescimo.Text)
        End If
        '
        '--- RETORNA O VALOR TOTAL
        propSaida.SaidaValor = DoValor + Acresc - Desc
        lblSaidaValor.DataBindings("text").ReadValue()
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' RECALCULA O VALOR PAGO QUANDO ALTERA TXTDOVALOR OU TXTACRESCIMO
    '------------------------------------------------------------------------------------------
    Private Sub txtValor_Validated(sender As Object, e As EventArgs) Handles txtValorPago.Validated, txtAcrescimo.Validated
        CalculaValorPago()
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
