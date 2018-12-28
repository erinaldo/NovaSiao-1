Imports CamadaBLL
Imports CamadaDTO

Public Class frmVendaEntrada
    Private _formOrigem As Form
    Private _vlMaximo As Double
    Private _VerAlteracao As Boolean
    Private _Pag As clMovimentacao
    Private _Acao As FlagAcao
    Private bindPag As New BindingSource
    '
    Private dtTipo As DataTable
    Private dtForma As DataTable
    '
    '-------------------------------------------------------------------------------------------------
    ' SUB NEW
    '-------------------------------------------------------------------------------------------------
    Sub New(fOrigem As Form,
            TranVlTotal As Double,
            Pag As clMovimentacao,
            Acao As FlagAcao,
            Optional Posicao As Point = Nothing)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        _formOrigem = fOrigem
        _VerAlteracao = False
        _vlMaximo = TranVlTotal
        _Pag = Pag
        _Acao = Acao
        '
        '--- Preenche a lsita de Formas e Tipos
        Dim TipoBLL As New MovimentacaoBLL
        dtForma = TipoBLL.MovForma_GET_DT(True)
        dtTipo = TipoBLL.MovTipo_GET_Dt(True)
        '
        bindPag.DataSource = _Pag
        PreencheDataBinding()
        '
        ' Verifica valor do Combo MovTipo pelo MovForma
        If IsNothing(_Pag.IDMovForma) Then
            '
            txtFormaTipo.Text = ""
            _Pag.IDMovTipo = Nothing
            '
        Else
            '
            For Each r As DataRow In dtForma.Rows
                If r("IDMovForma") = _Pag.IDMovForma Then
                    txtFormaTipo.Text = r("MovTipo")
                    _Pag.IDMovTipo = r("IDMovTipo")
                End If
            Next
            '
        End If
        '
        lblConta.Text = ObterDefault("ContaDescricao")
        lblFilial.Text = ObterDefault("FilialDescricao")
        '
        '--- Define a posicao do form
        If Not IsNothing(Posicao) Then
            StartPosition = FormStartPosition.Manual
            Location = Posicao
        End If
        '
        _VerAlteracao = True
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' PREENCHE O DATABINDING
    '------------------------------------------------------------------------------------------
    Private Sub PreencheDataBinding()
        '
        txtValor.DataBindings.Add("Text", bindPag, "MovValor", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' CARREGA OS COMBOBOX
        CarregaCmbForma()
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler txtValor.DataBindings("Text").Format, AddressOf FormatCUR
        '
    End Sub
    '
    Private Sub FormatCUR(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = FormatCurrency(e.Value, 2)
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' CARREGAR OS COMBOBOX
    '------------------------------------------------------------------------------------------
    '
    Private Sub CarregaCmbForma()
        '
        With cmbForma
            .DataSource = dtForma
            .DisplayMember = "MovForma"
            .ValueMember = "IDMovForma"
            .DataBindings.Add("SelectedValue", bindPag, "IDMovForma", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' CRIA UM ATALHO PARA OS COMBO BOX
    '------------------------------------------------------------------------------------------
    Private Sub txtFormaTipo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFormaTipo.KeyPress
        '
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            '
            Dim rCount As Integer = dtTipo.Rows.Count
            Dim item As Integer = e.KeyChar.ToString
            '
            If item <= rCount And item > 0 Then
                Dim Valor As Integer = dtTipo.Rows(e.KeyChar.ToString - 1)("IDMovTipo")
                '
                _Pag.IDMovTipo = Valor
                txtFormaTipo.Text = dtTipo.Rows(e.KeyChar.ToString - 1)("MovTipo")
                '
            End If
        Else
            e.Handled = True
        End If
        '
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' SELECIONA A FORMA DE PAGAMENTO CRIANDO ATALHO NUMERICO
    '-------------------------------------------------------------------------------------------------
    Private Sub cmbForma_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cmbForma.KeyPress
        '
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = True
            '
            Dim num As Integer = e.KeyChar.ToString
            Dim dtF As DataTable = cmbForma.DataSource
            Dim i As Integer = 1
            '
            For Each r As DataRow In dtF.Rows
                '
                If r("IDMovTipo") = _Pag.IDMovTipo Then
                    If num = i Then
                        _Pag.IDMovForma = r("IDMovForma")
                        cmbForma.DataBindings("SelectedValue").ReadValue()
                        Exit For
                    Else
                        i += 1
                    End If
                End If
                '
            Next
        End If
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
    Private Sub Me_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            e.Handled = True
            '
            btnCancelar_Click(New Object, New EventArgs)
        End If
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' ACAO DOS BOTOES
    '------------------------------------------------------------------------------------------
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        '
        '--- Verifica campos/valores
        If IsNothing(_Pag.IDMovTipo) Then
            MessageBox.Show("O campo TIPO de Entrada não pode ficar vazio..." & vbNewLine &
                            "Favor escolher um valor para esse campo.", "Campo Vazio",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtFormaTipo.Focus()
            Exit Sub
        End If
        '
        If IsNothing(_Pag.IDMovForma) Then
            MessageBox.Show("O campo FORMA de Entrada não pode ficar vazio..." & vbNewLine &
                            "Favor escolher um valor para esse campo.", "Campo Vazio",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            cmbForma.Focus()
            cmbForma.DroppedDown = True
            Exit Sub
        End If
        '
        If IsNothing(_Pag.MovValor) OrElse _Pag.MovValor <= 0 OrElse _Pag.MovValor > _vlMaximo Then
            MessageBox.Show("O VALOR da Entrada não pode ser igual ou menor que Zero..." & vbNewLine &
                            "bem como também não pode ser maior que o total da venda..." & vbNewLine &
                            "Favor escolher um valor para esse campo.", "Campo Vazio",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtValor.Focus()
            txtValor.SelectAll()
            Exit Sub
        End If
        '
        '--- Devolve o pagamento para o formOrigem
        Select Case _formOrigem.Name
            Case "frmVendaVista"
                _Pag.MovForma = txtFormaTipo.Text
                _Pag.IDMovForma = cmbForma.SelectedValue
                '
                If _Acao = FlagAcao.INSERIR Then
                    DirectCast(_formOrigem, frmVendaVista).Pagamentos_Manipulacao(_Pag, FlagAcao.INSERIR)
                ElseIf _Acao = FlagAcao.EDITAR Then

                End If
                '
            Case Else
                MessageBox.Show("Ainda não implementado")
        End Select
        Close()
    End Sub
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' ALTERA A FORMA DE PAGAMENTO PELA ALTERACAO DO TIPO DE ENTRADA
    '------------------------------------------------------------------------------------------
    Private Sub txtFormaTipo_Validated(sender As Object, e As EventArgs) Handles txtFormaTipo.Validated
        '
        If _VerAlteracao = False Then Exit Sub
        If IsNothing(_Pag.IDMovTipo) OrElse _Pag.IDMovTipo = 0 Then Exit Sub
        '
        dtForma.DefaultView.RowFilter = "IDMovTipo = " & _Pag.IDMovTipo
        '
        _VerAlteracao = False
        '
        If cmbForma.Items.Count = 1 Then
            cmbForma.SelectedIndex = 0
            _Pag.IDMovForma = dtForma.Rows(0).Item("IDMovForma")
        Else
            cmbForma.SelectedIndex = -1
        End If
        '
        _VerAlteracao = True
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' USAR TAB AO KEYPRESS ENTER
    '------------------------------------------------------------------------------------------
    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValor.KeyDown, txtFormaTipo.KeyDown
        '
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub frmAReceberItem_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If IsNothing(_formOrigem) Then Exit Sub
        '
        For Each c As Control In _formOrigem.Controls
            If c.Name = "Panel1" Then
                c.BackColor = Color.Silver
            End If
        Next
    End Sub
    '
    Private Sub frmVendaPagamento_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If IsNothing(_formOrigem) Then Exit Sub
        '
        For Each c As Control In _formOrigem.Controls
            If c.Name = "Panel1" Then
                c.BackColor = Color.SlateGray
            End If
        Next
    End Sub
    '
End Class
