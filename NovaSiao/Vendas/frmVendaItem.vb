Imports CamadaDTO
Imports CamadaBLL
Imports System.ComponentModel
'
Public Class frmVendaItem
    Private _clItem As clTransacaoItem
    Private _acao As FlagAcao
    Private _precoOrigem As precoOrigem
    Private _formOrigem As Form
    Private _filial As Integer?
    Private _IDAlterado As Boolean = False
    Private BindItem As New BindingSource
    '
#Region "NEW | PROPERTYS"
    '
    Sub New(fOrigem As Form, pOrigem As precoOrigem, Filial As Integer, Item As clTransacaoItem)

        ' This call is required by the designer.
        InitializeComponent()
        ' 
        ' Add any initialization after the InitializeComponent() call.
        '
        '--- Define se é entrada ou saida no caso de TROCA de produtos
        _precoOrigem = pOrigem '--- DEFINE SE É ENTRADA OU SAÍDA
        BackColor = Color.Azure
        Panel1.BackColor = Color.SlateGray
        'BackColor = Color.Beige
        'Panel1.BackColor = Color.Brown
        '
        If fOrigem.Name = "frmTrocaSimples" Then
            lblTitulo.Text = "Entrada de Produto - Item"
        End If
        '
        _formOrigem = fOrigem '--- DEFINE O FORMULARIO DE ORIGEM PARA RETORNAR
        _filial = Filial
        '
        '--- DEFINE E PREECHE A CLASSE
        propItem = Item
        propItem.IDFilial = Filial
        '
        If IsNothing(Item.IDTransacaoItem) Then '--- DEFINE SE É NOVA OU ALTERAÇÃO
            propAcao = FlagAcao.INSERIR
        Else
            propAcao = FlagAcao.EDITAR
        End If
        '
    End Sub
    '
    '--- Propriedade propAcao
    Public Property propAcao() As FlagAcao
        Get
            Return _acao
        End Get
        Set(ByVal value As FlagAcao)
            _acao = value
            '
            If _acao = FlagAcao.INSERIR Then
                btnOK.Text = "&Inserir"
                lblToolStripInfo.Text = "Inserindo Novo Item"
            ElseIf _acao = FlagAcao.EDITAR Then
                btnOK.Text = "&Alterar"
                lblToolStripInfo.Text = "Editando Item"
            End If
            '
        End Set
    End Property
    '
    '--- Propriedade propItem
    Public Property propItem() As clTransacaoItem
        '
        Get
            Return _clItem
        End Get
        '
        Set(ByVal value As clTransacaoItem)
            _clItem = value
            '
            If IsNothing(BindItem.DataSource) Then
                BindItem.DataSource = _clItem
                PreencheDataBindings()
            Else
                BindItem.Clear()
                BindItem.DataSource = _clItem
                BindItem.ResetBindings(True)
            End If
        End Set
        '
    End Property
    '
#End Region
    '
#Region "DATA BINDINGS"
    '
    Private Sub PreencheDataBindings()
        '
        '--- PREENCHE OS DATABINDGS DOS CONTROLES
        '
        txtRGProduto.DataBindings.Add("Text", BindItem, "RGProduto", True, DataSourceUpdateMode.OnValidation)
        txtDesconto.DataBindings.Add("Text", BindItem, "Desconto", True, DataSourceUpdateMode.OnPropertyChanged)
        txtQuantidade.DataBindings.Add("Text", BindItem, "Quantidade", True, DataSourceUpdateMode.OnPropertyChanged)
        lblProduto.DataBindings.Add("Text", BindItem, "Produto", True, DataSourceUpdateMode.OnPropertyChanged)
        lblEstoque.DataBindings.Add("Text", BindItem, "Estoque", True, DataSourceUpdateMode.OnPropertyChanged)
        lblReservado.DataBindings.Add("Text", BindItem, "Reservado", True, DataSourceUpdateMode.OnPropertyChanged)
        lblPreco.DataBindings.Add("Text", BindItem, "Preco", True, DataSourceUpdateMode.OnPropertyChanged)
        lblSubTotal.DataBindings.Add("Text", BindItem, "SubTotal")
        lblTotal.DataBindings.Add("Text", BindItem, "Total")
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler txtRGProduto.DataBindings("text").Format, AddressOf idFormatRG
        AddHandler txtQuantidade.DataBindings("text").Format, AddressOf idFormatQ2
        AddHandler lblPreco.DataBindings("text").Format, AddressOf idFormatCUR
        AddHandler txtDesconto.DataBindings("text").Format, AddressOf idFormatPerc
        AddHandler lblSubTotal.DataBindings("text").Format, AddressOf idFormatCUR
        AddHandler lblTotal.DataBindings("text").Format, AddressOf idFormatCUR
        AddHandler lblEstoque.DataBindings("text").Format, AddressOf idFormatQ2
        AddHandler lblReservado.DataBindings("text").Format, AddressOf idFormatQ2
        '
        '--- Detecta quando o IDProduto foi alterado
        AddHandler _clItem.AoAlterarIDProduto, AddressOf IDProdutoAlterado
        '
    End Sub
    '
    ' FORMATA OS BINDINGS
    Private Sub idFormatRG(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "0000")
    End Sub
    Private Sub idFormatQ2(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "00")
    End Sub
    Private Sub idFormatCUR(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = FormatCurrency(e.Value, 2)
    End Sub
    Private Sub idFormatPerc(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "0.00")
    End Sub
    '
#End Region
    '
#Region "FUNCOES NECESSARIAS"
    '
    Private Sub IDProdutoAlterado()
        _IDAlterado = True
    End Sub
    '
    Private Sub txtRGProduto_Validating(sender As Object, e As CancelEventArgs) Handles txtRGProduto.Validating
        '
        '-- Verifica se o controle RGProduto sofreu alguma alteracao
        Dim txtVal As Integer = If(txtRGProduto.Text.Length > 0, CInt(txtRGProduto.Text), 0)
        If txtVal = If(_clItem.RGProduto, 0) Then Exit Sub
        '
        Dim itemBLL As New TransacaoItemBLL
        '
        'If String.IsNullOrEmpty(txtRGProduto.Text) Then
        '    propItem = New clTransacaoItem
        '    e.Cancel = False
        '    Exit Sub
        'End If
        '
        '--- obter as informacoes do produto digitado
        Try
            Dim ItemProduto As clTransacaoItem = itemBLL.TransacaoItem_Get_New(txtRGProduto.Text, _filial)
            '
            If String.IsNullOrEmpty(ItemProduto.Produto) Then
                MessageBox.Show("Registro de Produto não encontrado..." & vbNewLine &
                                "Favor digite um Registro válido.", "Reg. Inválido",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                If _acao = FlagAcao.INSERIR Then txtRGProduto.Text = String.Empty
                txtRGProduto.Text = _clItem.RGProduto
                e.Cancel = True
                Return
            End If
            '
            With _clItem
                .Produto = ItemProduto.Produto
                .PVenda = ItemProduto.PVenda
                .PCompra = ItemProduto.PCompra
                .IDProduto = ItemProduto.IDProduto
                .CodBarrasA = ItemProduto.CodBarrasA
                .ProdutoAtivo = ItemProduto.ProdutoAtivo
                .Estoque = ItemProduto.Estoque
                .Reservado = ItemProduto.Reservado
                .IDFilial = ItemProduto.IDFilial
                .RGProduto = ItemProduto.RGProduto
                '
                '--- define o preco de VENDA OU DE COMPRA
                If _precoOrigem = precoOrigem.PRECO_VENDA Then
                    .Preco = ItemProduto.PVenda
                ElseIf _precoOrigem = precoOrigem.PRECO_COMPRA Then
                    .Preco = ItemProduto.PCompra
                    .Desconto = ItemProduto.DescontoCompra
                End If
                '
            End With
            '
            BindItem.ResetBindings(True)
            '
        Catch ex As Exception
            MessageBox.Show("Um erro inesperado ocorreu..." & vbNewLine &
                            ex.Message, "Obter Produto",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        '
    End Sub
    '
    ' CONTROLA O KEYPRESS DO RGPRODUTO
    Private Sub txtRGProduto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRGProduto.KeyPress
        If Char.IsNumber(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = vbBack Then
            e.Handled = False
        ElseIf e.KeyChar = "+" Then
            e.Handled = True
            '
            '--- abre o form de Procura de Produto
            Dim p As New frmProdutoProcurar(Me, True)
            p.ShowDialog()
            '
            '--- verifica se retornou algum valor
            If p.DialogResult = vbCancel Then Exit Sub
            '
            '--- se retornou entao preenche o RGProduto
            txtRGProduto.Text = p.RGEscolhido
            SendKeys.Send("{TAB}")
            '
        Else
            e.Handled = True
        End If
    End Sub
    '    
    ' CONTROLA O KEYPRESS DO DESCONTO
    Private Sub txtDesconto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesconto.KeyPress
        '
        If Char.IsNumber(e.KeyChar) OrElse e.KeyChar = New Char() {vbBack, ",", "."} Then
            e.Handled = False
        ElseIf e.KeyChar = "+" Then
            e.Handled = True
            '
            '--- abre o form de Procura de Produto
            Dim fDesc As New frmProdutoValorDesconto(_clItem.PVenda * _clItem.Quantidade, Me)
            fDesc.ShowDialog()
            '
            '--- verifica se retornou algum valor
            If fDesc.DialogResult = vbCancel Then Exit Sub
            '
            '--- se retornou entao preenche o desconto ou aumenta o valor
            If fDesc.DialogResult = DialogResult.OK Then '--- desconto
                txtDesconto.Text = fDesc.propDesconto
            ElseIf fDesc.DialogResult = DialogResult.Yes Then '--- aumento do preco
                txtDesconto.Text = 0
                _clItem.Preco = fDesc.propDesconto
                lblPreco.DataBindings("text").ReadValue()
                lblSubTotal.DataBindings("text").ReadValue()
                lblTotal.DataBindings("text").ReadValue()
            End If
            '
        Else
            e.Handled = True
        End If
    End Sub
    '
    ' AO RECEBER O FOCO SELECIONA O TEXTO
    Private Sub Text_GotFocus(sender As Object, e As EventArgs) Handles txtRGProduto.GotFocus,
            txtDesconto.GotFocus, txtQuantidade.GotFocus
        If CType(sender, TextBox).Text.Length > 0 Then sender.SelectAll()
    End Sub
    '
    ' AO PRESSIONAR ENTER ENVIA TAB NAVEGA ENTRE CONTROLES
    Private Sub txtRGProduto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRGProduto.KeyDown,
        txtQuantidade.KeyDown, txtDesconto.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
    End Sub
    '
#End Region
    '
#Region "BUTTONS FUNCTION"

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        '--- VERIFICA SE OS VALORES ESTÃO PREENCHIDOS
        Dim A As Boolean = IsNothing(_clItem.RGProduto) OrElse String.IsNullOrEmpty(_clItem.RGProduto)
        Dim B As Boolean = IsNothing(_clItem.Produto) OrElse String.IsNullOrEmpty(_clItem.Produto)
        '
        If A Or B Then
            MessageBox.Show("")
            Exit Sub
        End If
        '
        '--- RETORNA O OBJETO PARA O FORMULARIO ORIGEM
        DialogResult = DialogResult.OK
        '
    End Sub
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        BindItem.CancelEdit()
        DialogResult = DialogResult.Cancel
    End Sub
    '
#End Region
    '
#Region "DESIGN VISUAL"
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
    Private Sub frmTransacaoItem_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            e.Handled = True
            '
            btnCancelar_Click(New Object, New EventArgs)
        End If
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' CRIAR EFEITO VISUAL DE FORM SELECIONADO
    '-------------------------------------------------------------------------------------------------
    Private Sub form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.Silver
    End Sub
    '
    Private Sub form_closed(sender As Object, e As EventArgs) Handles Me.Closed
        Dim pnl As Panel = _formOrigem.Controls("Panel1")
        pnl.BackColor = Color.SlateGray
    End Sub
    '
#End Region
    '
End Class
