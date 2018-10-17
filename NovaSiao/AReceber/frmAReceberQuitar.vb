Imports CamadaBLL
Imports CamadaDTO

Public Class frmAReceberQuitar
    Private _formOrigem As Form
    Private bindEntrada As New BindingSource
    Private _vlMax As Decimal = 0
    Private _VerAlteracao As Boolean = False
    Property propEntrada As clEntradas
    Property prop_vlPagoDoValor As Double '--- retorna o valor pago sem o acrescimo
    Property prop_vlPagoJuros As Double '--- retorna o valor pago do Acrescimo / Juros
    '
    Public Sub New(formOrigem As Form, vlMax As Decimal, IDOrigem As Integer, Optional Acrescimo As Double = 0)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        _formOrigem = formOrigem
        _vlMax = vlMax
        propEntrada = New clEntradas
        bindEntrada.DataSource = propEntrada
        PreencheDataBinding()
        '
        propEntrada.EntradaData = Obter_DataPadrao()
        propEntrada.IDConta = Obter_ContaPadrao()
        propEntrada.Origem = 2 '--- ORIGEM PARCELAMENTO
        propEntrada.IDOrigem = IDOrigem
        propEntrada.Creditar = False
        txtDoValor.Text = FormatCurrency(vlMax, 2)
        txtAcrescimo.Text = FormatCurrency(Acrescimo, 2)
        CalculaValorPago()
        _VerAlteracao = True
        '
    End Sub
    '
#Region "DATA BINDINGS"
    '------------------------------------------------------------------------------------------
    ' PREENCHE O DATABINDING
    '------------------------------------------------------------------------------------------
    Private Sub PreencheDataBinding()
        '
        lblEntradaValor.DataBindings.Add("Text", bindEntrada, "EntradaValor", True, DataSourceUpdateMode.OnPropertyChanged)
        txtEntradaData.DataBindings.Add("Text", bindEntrada, "EntradaData", True, DataSourceUpdateMode.OnPropertyChanged)
        txtObservacao.DataBindings.Add("Text", bindEntrada, "Observacao", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' CARREGA OS COMBOBOX
        CarregaCmbTipo()
        CarregaCmbForma()
        CarregaCmbConta()
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler lblEntradaValor.DataBindings("Text").Format, AddressOf FormatCUR
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
    Private Sub CarregaCmbTipo()
        Dim TipoBLL As New MovimentacaoBLL
        Dim dtTipo As DataTable = TipoBLL.MovTipo_GET
        '
        With cmbIDMovTipo
            .DataSource = dtTipo
            .DisplayMember = "MovTipo"
            .ValueMember = "IDMovTipo"
            .DataBindings.Add("SelectedValue", bindEntrada, "IDMovTipo", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
        '
    End Sub
    '
    Private Sub CarregaCmbForma()
        Dim TipoBLL As New MovimentacaoBLL
        Dim dtForma As DataTable = TipoBLL.MovForma_GET
        '
        With cmbIDMovForma
            .DataSource = dtForma
            .DisplayMember = "MovForma"
            .ValueMember = "IDMovForma"
            .DataBindings.Add("SelectedValue", bindEntrada, "IDMovForma", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
        '
    End Sub
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
            Dim dt As DataTable = EntBLL.Contas_GET_PorIDFilial
            '
            With cmbIDConta
                .DataSource = dt
                .DisplayMember = "Conta"
                .ValueMember = "IDConta"
                .DataBindings.Add("SelectedValue", bindEntrada, "IDConta", True, DataSourceUpdateMode.OnPropertyChanged)
            End With
        Catch ex As Exception
            MessageBox.Show("Ocorreu uma exceção inesperada ao obter as lista de conta:" & vbNewLine &
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
        If DataPermitida(propEntrada.EntradaData, Obter_FilialPadrao, propEntrada.IDConta) = False Then
            Exit Sub
        End If
        '
        '--- Insere a Entrada
        Dim eBLL As New EntradaBLL
        Dim newID As Integer?
        '
        Try
            newID = eBLL.Entrada_Inserir(propEntrada)
            '
            If Not IsNumeric(newID) Then
                Throw New Exception(newID.ToString)
            Else
                propEntrada.IDEntrada = newID
                propEntrada.Conta = cmbIDConta.Text
                propEntrada.MovForma = cmbIDMovForma.Text
            End If
            '
            prop_vlPagoDoValor = CDbl(txtDoValor.Text)
            prop_vlPagoJuros = CDbl(txtAcrescimo.Text)
            DialogResult = DialogResult.OK
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção ao inserir Nova Entrada..." & vbNewLine &
                            "Comunique ao administrador do sistema.", "Exceção Inesperada",
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
        If f.VerificaControlesForm(txtEntradaData, "Data da Entrada") = False Then
            Return False
        End If
        '
        '--- Campo DoValor
        If f.VerificaControlesForm(txtDoValor, "Valor da Entrada") = False Then
            Return False
        End If
        '
        If IsNumeric(txtDoValor.Text) AndAlso CDec(txtDoValor.Text) = 0 Then
            MessageBox.Show("O Valor da Entrada não pode ser igual a ZERO", "Valor Inválido",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtDoValor.Focus()
            Return False
        End If
        '
        '--- Campo Tipo de Entrada
        If f.VerificaControlesForm(cmbIDMovTipo, "Tipo de Pagamento da Entrada") = False Then
            Return False
        End If
        '
        '--- Campo Forma da Entrada
        If f.VerificaControlesForm(cmbIDMovForma, "Forma de Pagamento da Entrada") = False Then
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
    Private Sub cmbFormaTipo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbIDMovTipo.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            '
            Dim dt As DataTable = cmbIDMovTipo.DataSource
            Dim rCount As Integer = dt.Rows.Count
            Dim item As Integer = e.KeyChar.ToString
            '
            If item <= rCount And item > 0 Then
                Dim Valor As Integer = dt.Rows(e.KeyChar.ToString - 1)("IDMovTipo")
                '
                propEntrada.IDMovTipo = Valor
                cmbIDMovTipo.SelectedValue = Valor
                '
            End If
        End If
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' SELECIONA A FORMA DE PAGAMENTO CRIANDO ATALHO NUMERICO
    '-------------------------------------------------------------------------------------------------
    Private Sub cmbForma_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbIDMovForma.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            '
            Dim num As Integer = e.KeyChar.ToString
            Dim dtF As DataTable = cmbIDMovForma.DataSource
            Dim i As Integer = 1
            '
            For Each r As DataRow In dtF.Rows
                If r("IDMovTipo") = cmbIDMovTipo.SelectedValue Then
                    If num = i Then
                        propEntrada.IDMovForma = r("IDMovForma")
                        cmbIDMovForma.DataBindings("SelectedValue").ReadValue()
                        Exit For
                    Else
                        i += 1
                    End If
                End If
            Next
        End If
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' USAR TAB AO KEYPRESS ENTER
    '------------------------------------------------------------------------------------------
    Private Sub txtValor_KeyDown(sender As Object, e As KeyEventArgs) Handles rbtEntrada.KeyDown,
            txtDoValor.KeyDown, txtAcrescimo.KeyDown, txtObservacao.KeyDown
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
    ' CALCULA O VALOR DO CAMPO VALORPAGO = DOVALOR + ACRESCIMO
    '------------------------------------------------------------------------------------------
    Private Sub CalculaValorPago()
        Dim DoValor As Decimal = 0
        Dim Acresc As Decimal = 0
        '
        If IsNumeric(txtDoValor.Text) Then
            DoValor = CDec(txtDoValor.Text)
            '
            If DoValor > _vlMax Then
                If MessageBox.Show("O valor da Entrada não pode ser maior que R$ " &
                                Format(_vlMax, "#,##0.00") & vbNewLine & vbNewLine &
                                "Deseja inserir a diferença no valor do ACRÉSCIMO?",
                                "Inserir Acréscimo", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    txtAcrescimo.Text = Format(DoValor - _vlMax, "C")
                Else
                    txtDoValor.Focus()
                End If
                DoValor = _vlMax
                txtDoValor.Text = FormatCurrency(_vlMax, 2)
            End If
        End If
        '
        If IsNumeric(txtAcrescimo.Text) Then
            Acresc = CDec(txtAcrescimo.Text)
        End If
        '
        propEntrada.EntradaValor = DoValor + Acresc
        lblEntradaValor.DataBindings("text").ReadValue()
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' RECALCULA O VALOR PAGO QUANDO ALTERA TXTDOVALOR OU TXTACRESCIMO
    '------------------------------------------------------------------------------------------
    Private Sub txtValor_Validated(sender As Object, e As EventArgs) Handles txtDoValor.Validated, txtAcrescimo.Validated
        CalculaValorPago()
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' ALTERA A FORMA DE PAGAMENTO PELA ALTERACAO DO TIPO DE PAGAMENTO
    '------------------------------------------------------------------------------------------
    Private Sub cmbIDMovTipo_Validated(sender As Object, e As EventArgs) Handles cmbIDMovTipo.Validated
        If _VerAlteracao = False Then Exit Sub
        If IsNothing(cmbIDMovTipo.SelectedValue) Then Exit Sub
        '
        Dim dtForma As DataTable = cmbIDMovForma.DataSource
        dtForma.DefaultView.RowFilter = "IDMovTipo = " & cmbIDMovTipo.SelectedValue
        '
        _VerAlteracao = False
        If cmbIDMovForma.Items.Count = 1 Then
            cmbIDMovForma.SelectedIndex = 0
            propEntrada.IDMovForma = dtForma.Rows(0).Item("IDMovForma")
        Else
            cmbIDMovForma.SelectedIndex = -1
        End If
        _VerAlteracao = True
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' ALTERA A FORMA DE ENTRADA PELA ALTERACAO DOS RADIOBUTTONS PANEL2
    '------------------------------------------------------------------------------------------
    Private Sub radiobt_CheckedChanged(sender As Object, e As EventArgs) Handles rbtEntrada.CheckedChanged, rbtCreditar.CheckedChanged
        '
        '--- verificar somente após a abertura completa
        If _VerAlteracao = False Then Exit Sub
        '
        If rbtEntrada.Checked = True Then
            propEntrada.Creditar = False
        ElseIf rbtCreditar.Checked = True Then
            '--- Verifica a autoridade do usuário
            If UsuarioAcesso(1) <> 1 Then '--- Se não for ADMINISTRADOR
                MessageBox.Show("Não é possível creditar essa entrada." & vbNewLine &
                                "Você ainda não tem autorização para CREDITAR....",
                                "Permissão de Usuário", MessageBoxButtons.OK, MessageBoxIcon.Information)
                rbtCreditar.Checked = False
            Else
                propEntrada.Creditar = True
            End If
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
