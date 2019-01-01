﻿Imports System.ComponentModel
Imports CamadaBLL
Imports CamadaDTO
Imports System.Math

Public Class frmProduto
    Private prodBLL As New ProdutoBLL
    Private _produto As clProduto
    Private WithEvents bindP As New BindingSource
    Private _Sit As FlagEstado '= 1:Registro Salvo; 2:Registro Alterado; 3:Novo registro
    Private AtivarImage As Image = My.Resources.Switch_ON_PEQ
    Private DesativarImage As Image = My.Resources.Switch_OFF_PEQ
    Private _acao As FlagAcao
    Private _formOrigem As Form
    '
#Region "EVENTO LOAD E PROPRIEDADE SIT"
    '
    Public Property RGEscolhido As Integer? '--- Propriedade para escolha do produto
    '
    'PROPERTY SIT
    Private Property Sit As FlagEstado
        Get
            Return _Sit
        End Get
        Set(value As FlagEstado)
            _Sit = value
            If _Sit = FlagEstado.RegistroSalvo Then
                btnSalvar.Enabled = False
                btnNovoProduto.Enabled = True
                btnExcluir.Enabled = True
                btnCancelar.Enabled = False
                btnProcurar.Enabled = True
                lblIDProduto.Text = Format(_produto.IDProduto, "0000")
                CalcMargemDescontoLabel()
                AtivoButtonImage()
            ElseIf _Sit = FlagEstado.Alterado Then
                btnSalvar.Enabled = True
                btnNovoProduto.Enabled = False
                btnExcluir.Enabled = True
                btnCancelar.Enabled = True
                btnProcurar.Enabled = False
                AtivoButtonImage()
            ElseIf _Sit = FlagEstado.NovoRegistro Then
                txtProduto.Select()
                btnSalvar.Enabled = True
                btnNovoProduto.Enabled = False
                btnExcluir.Enabled = False
                btnProcurar.Enabled = False
                lblIDProduto.Text = "NOVO"
                AtivoButtonImage()
                If IsNothing(_formOrigem) Then '--- Não tem formulário de origem
                    btnCancelar.Enabled = True
                Else '--- Tem origem em outro processo
                    btnCancelar.Enabled = False
                End If
            End If
        End Set
    End Property
    '
    Public Sub New(myAcao As FlagAcao, myProduto As clProduto, Optional formOrigem As Form = Nothing)
        '
        ' This call is required by the designer.
        InitializeComponent()
        '
        ' Add any initialization after the InitializeComponent() call.
        _formOrigem = formOrigem
        propProduto = myProduto
        propAcao = myAcao
        '
    End Sub
    '
    '--- Propriedade propClientePF
    Public Property propProduto() As clProduto
        Get
            Return _produto
        End Get
        Set(ByVal value As clProduto)

            _produto = value
            '
            If IsNothing(bindP.DataSource) Then
                bindP.DataSource = _produto
                PreencheDataBindings()
                '
                ' LER OS DADOS AGORA FORMATADOS
                txtPCompra.DataBindings("Text").ReadValue()
                txtRGProduto.DataBindings("Text").ReadValue()
                txtPVenda.DataBindings("Text").ReadValue()
                txtUnidade.DataBindings("Text").ReadValue()
                '
            Else
                bindP.Clear()
                bindP.DataSource = _produto
                bindP.ResetBindings(True)
                AddHandler _produto.AoAlterar, AddressOf HandlerAoAlterar
            End If
            '
        End Set
    End Property
    '
    Public Property propAcao As FlagAcao
        Get
            Return _acao
        End Get
        Set(value As FlagAcao)
            _acao = value
            If value = FlagAcao.INSERIR Then
                Sit = FlagEstado.NovoRegistro
            ElseIf value = FlagAcao.EDITAR Then
                Sit = FlagEstado.RegistroSalvo
            End If
        End Set
    End Property
    '
    ' EVENTO LOAD
    Private Sub frmProduto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        addToolTipHandler()
        '
    End Sub
    '
#End Region '// EVENTO LOAD E PROPRIEDADE SIT
    '
#Region "BINDINGS"
    Private Sub PreencheDataBindings()
        ' ADICIONANDO O DATABINDINGS AOS CONTROLES TEXT
        ' OS COMBOS JA SÃO ADICIONADOS DATABINDINGS QUANDO CARREGA

        lblIDProduto.DataBindings.Add("Tag", bindP, "IDProduto")
        txtRGProduto.DataBindings.Add("Text", bindP, "RGProduto", True, DataSourceUpdateMode.OnPropertyChanged)
        txtProduto.DataBindings.Add("Text", bindP, "Produto", True, DataSourceUpdateMode.OnPropertyChanged)

        txtProdutoTipo.DataBindings.Add("Text", bindP, "ProdutoTipo", True, DataSourceUpdateMode.OnPropertyChanged)
        txtProdutoSubTipo.DataBindings.Add("Text", bindP, "ProdutoSubTipo", True, DataSourceUpdateMode.OnPropertyChanged)
        txtProdutoCategoria.DataBindings.Add("Text", bindP, "ProdutoCategoria", True, DataSourceUpdateMode.OnPropertyChanged)

        txtAutor.DataBindings.Add("Text", bindP, "Autor", True, DataSourceUpdateMode.OnPropertyChanged)
        txtUnidade.DataBindings.Add("Text", bindP, "Unidade", True, DataSourceUpdateMode.OnPropertyChanged)
        txtPCompra.DataBindings.Add("Text", bindP, "PCompra", True, DataSourceUpdateMode.OnPropertyChanged)
        txtDescontoCompra.DataBindings.Add("Text", bindP, "DescontoCompra", True, DataSourceUpdateMode.OnPropertyChanged)
        txtPVenda.DataBindings.Add("Text", bindP, "PVenda", True, DataSourceUpdateMode.OnPropertyChanged)
        txtNCM.DataBindings.Add("Text", bindP, "NCM", True, DataSourceUpdateMode.OnPropertyChanged)
        txtCodBarrasA.DataBindings.Add("Text", bindP, "CodBarrasA", True, DataSourceUpdateMode.OnPropertyChanged)
        txtEstoqueIdeal.DataBindings.Add("Text", bindP, "EstoqueIdeal", True, DataSourceUpdateMode.OnPropertyChanged)
        txtEstoqueNivel.DataBindings.Add("Text", bindP, "EstoqueNivel", True, DataSourceUpdateMode.OnPropertyChanged)
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler txtPCompra.DataBindings("Text").Format, AddressOf FormatCUR
        AddHandler txtRGProduto.DataBindings("Text").Format, AddressOf idFormatRG
        AddHandler txtPVenda.DataBindings("Text").Format, AddressOf FormatCUR
        AddHandler txtUnidade.DataBindings("Text").Format, AddressOf FormataN2
        AddHandler txtEstoqueIdeal.DataBindings("Text").Format, AddressOf FormataN2
        AddHandler txtEstoqueNivel.DataBindings("Text").Format, AddressOf FormataN2
        AddHandler txtDescontoCompra.DataBindings("text").Format, AddressOf FormatPercent
        '
        ' CARREGA OS COMBOBOX
        CarregaComboFabricante()
        CarregaComboSitTributaria()
        '
        ' ADD HANDLER PARA DATABINGS
        AddHandler _produto.AoAlterar, AddressOf HandlerAoAlterar
    End Sub
    '
    Private Sub HandlerAoAlterar()
        If _produto.RegistroAlterado = True And Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
    End Sub
    '
    ' FORMATA OS BINDINGS
    Private Sub idFormatRG(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "0000")
    End Sub
    '
    Private Sub FormataN2(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value, "00")
    End Sub
    '
    Private Sub FormatCUR(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = FormatCurrency(e.Value, 2)
    End Sub
    '
    Private Sub FormatPercent(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(e.Value / 100, "0.00%")
    End Sub
    '
    ' CARREGA OS COMBOBOX
    '--------------------------------------------------------------------------------------------------------
    '
    '--- COMBO FABRICANTE
    Private Sub CarregaComboFabricante()
        Dim sql As New SQLControl
        '
        If Not IsNothing(_produto.IDFabricante) Then
            sql.ExecQuery("SELECT * FROM tblProdutoFabricante WHERE FabricanteAtivo = 'TRUE' OR " &
                          "IDFabricante = " & _produto.IDFabricante)
        Else
            sql.ExecQuery("SELECT * FROM tblProdutoFabricante WHERE FabricanteAtivo = 'TRUE';")
        End If
        '
        If sql.HasException(True) Then
            Exit Sub
        End If
        '
        With cmbIDFabricante
            .DataSource = sql.DBDT
            .ValueMember = "IDFabricante"
            .DisplayMember = "Fabricante"
            If .DataBindings.Count = 0 Then
                .DataBindings.Add("SelectedValue", bindP, "IDFabricante", True, DataSourceUpdateMode.OnPropertyChanged)
            End If
        End With
        sql = Nothing
    End Sub
    '
    '--- COMBO SITUACAO TRIBUTARIA
    Private Sub CarregaComboSitTributaria()
        Dim dtSexo As New DataTable
        'Adiciona todas as possibilidades de instrucao
        dtSexo.Columns.Add("IDSitTributaria")
        dtSexo.Columns.Add("SitTributaria")
        dtSexo.Rows.Add(New Object() {0, "Tributação Normal"})
        dtSexo.Rows.Add(New Object() {40, "Isento"})
        dtSexo.Rows.Add(New Object() {41, "Não Tributada"})
        dtSexo.Rows.Add(New Object() {60, "Subst. Tributária"})

        With cmbSitTributaria
            .DataSource = dtSexo
            .ValueMember = "IDSitTributaria"
            .DisplayMember = "SitTributaria"
            .DataBindings.Add("SelectedValue", bindP, "SitTributaria", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
    End Sub
    '
#End Region '// BINDINGS
    '
#Region "SALVAR REGISTRO"
    ' SALVAR O REGISTRO
    '---------------------------------------------------------------------------------------------------
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        'verifica os dados se os campos estão preechidos
        If VerificaDados() = False Then Exit Sub
        '
        ' procurar se já existe cliente com o mesmo RGPRODUTO
        If VerificaRGProduto() = False Then Exit Sub
        '
        ' verifica a filial padrão
        Dim myFilial As Integer? = Obter_FilialPadrao()
        If IsNothing(myFilial) Then Exit Sub
        '
        'define os dados da classe
        Dim NewProdID As Long
        '
        Try
            'Salva o Produto, mas antes define se é ATUALIZAR OU UM NOVO REGISTRO
            If Sit = FlagEstado.NovoRegistro Then 'Nesse caso é um NOVO REGISTRO
                NewProdID = prodBLL.SalvaNovoProduto_Procedure_ID(_produto, myFilial)
            ElseIf _Sit = FlagEstado.Alterado Then 'Nesse caso é um REGISTRO EDITADO
                _produto.UltAltera = Now.ToShortDateString
                NewProdID = prodBLL.AtualizaProduto_Procedure_ID(_produto, myFilial)
            End If
        Catch ex As Exception
            MessageBox.Show("Um erro ocorreu ao salvar o registro!" & vbCrLf &
                            ex.Message, "Erro ao Salvar Registro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'Verifica se houve Retorno da Função de Salvar
        If NewProdID <> 0 Then
            '
            'Obtem o número de Registro do Novo Produto Cadastrado
            If _Sit = FlagEstado.NovoRegistro Then
                _produto.IDProduto = NewProdID
                lblIDProduto.Tag = NewProdID
                lblIDProduto.Text = Format(NewProdID, "D4")
            End If

            If IsNothing(_formOrigem) Then '--- nesse caso mantém o formulário aberto depois de salvar
                'Altera a Situação
                Sit = FlagEstado.RegistroSalvo
                bindP.CurrencyManager.EndCurrentEdit()

                'Mensagem
                MessageBox.Show("Registro Salvo com sucesso!", "Registro Salvo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else '--- nesse caso fecha o formulário depois de salvar e retorna o IDProduto
                '
                '--- retorna o valor do RGProduto
                RGEscolhido = _produto.RGProduto
                '--- Mensagem
                MessageBox.Show("Registro Salvo com sucesso!", "Registro Salvo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                '--- fecha o formulario de cadastro
                DialogResult = DialogResult.OK
                '
            End If
        Else
            MessageBox.Show("Registro NÃO pode ser salvo!", "Erro ao Salvar",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    '
    ' FAZER VERIFICAÇÃO ANTES DE SALVAR
    Private Function VerificaDados() As Boolean

        Dim f As New FuncoesUtilitarias
        '
        If Not f.VerificaControlesForm(txtProduto, "Descrição do Produto", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtRGProduto, "Registro do Produto", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtProdutoTipo, "Tipo do Produto", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtProdutoSubTipo, "Classificação do Produto", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(cmbSitTributaria, "Situação Tributária do Produto", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtPCompra, "Preço de Compra", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtPVenda, "Preço de Venda", EProvider) Then
            Return False
        End If
        '
        'Se nenhuma das condições acima forem verdadeira retorna TRUE
        EProvider.Clear()
        Return True
    End Function
    '
    Private Function VerificaRGProduto() As Boolean
        '
        '--- verifica se existe RGProduto
        If String.IsNullOrWhiteSpace(txtRGProduto.Text) Then
            Dim r As DialogResult
            r = MessageBox.Show("O campo Registro Interno do Produto está vazio..." & vbNewLine &
                            "Você deseja que o sistema preencha automaticamente o valor desse campo?",
                            "Campo Vazio", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button3)
            If r = DialogResult.Yes Then
                '
                ' Procura o valor para o campo
                btnProcuraRG_Click(New Object, New EventArgs)
                '
                '--- verifica novamente se o campo foi automaticamente preenchido
                If IsNothing(_produto.RGProduto) OrElse _produto.RGProduto = 0 Then
                    Return False
                Else
                    Return True
                End If
                '
            ElseIf r = DialogResult.No Then
                txtRGProduto.Focus()
                Return False
            End If
        End If

        Dim ProdRG As String = ""

        If Sit = FlagEstado.NovoRegistro Then
            ProdRG = ProcurarProduto_RG(CInt(txtRGProduto.Text))
        ElseIf Sit = FlagEstado.Alterado Then
            ProdRG = ProcurarProduto_RG(CInt(txtRGProduto.Text), _produto.RGProduto)
        End If

        If ProdRG.Length > 0 Then
            MessageBox.Show("Já foi encontrado um Produto com esse mesmo número de Reg. Interno..." & vbNewLine &
                                ProdRG.ToUpper & vbNewLine &
                                "Insira outro Reg. Interno ou altere o registro do outro Produto.",
                                "Reg. Interno Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRGProduto.Focus()
            Return False
        Else
            Return True
        End If

    End Function
    '---------------------------------------------------------------------------------------------------
#End Region '// SALVAR REGISTRO
    '
#Region "OPERAÇÕES DE REGISTRO"
    '
    Private Sub btnProcurar_Click(sender As Object, e As EventArgs) Handles btnProcurar.Click
        '
        'Abre o formulário de procurar Produto
        Dim frmP As New frmProdutoProcurar()
        frmP.ShowDialog()
        '
        If frmP.DialogResult = DialogResult.Cancel Then
            MessageBox.Show("Procura Cancelada...", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Dim pReg As Integer = frmP.IDEscolhido
            propProduto = prodBLL.GetProduto_PorID(pReg, Obter_FilialPadrao)
            '
            Sit = FlagEstado.RegistroSalvo
        End If
        '
    End Sub
    '------------------------------------------------------------------------------------------------
    '
    ' CANCELAR ALTERAÇÃO DO REGISTRO ATUAL
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        If MessageBox.Show("Deseja cancelar todas as alterações feitas no registro atual?",
                           "Cancelar Alterações", MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            bindP.CancelEdit()
            txtProduto.Focus()
            Sit = FlagEstado.RegistroSalvo
        End If
    End Sub
    '
    ' ATIVAR OU DESATIVAR CLIENTE BOTÃO
    Private Sub btnAtivo_Click(sender As Object, e As EventArgs) Handles btnAtivo.Click
        If Sit = FlagEstado.NovoRegistro Then
            MessageBox.Show("Você não pode DESATIVAR um Produto Novo", "Desativar Produto",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        If _produto.ProdutoAtivo = True Then ' Produto esta ativo
            If MessageBox.Show("Você deseja realmente DESATIVAR o Produto:" & vbNewLine &
                        txtProduto.Text.ToUpper, "Desativar Produto", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                _produto.BeginEdit()
                _produto.ProdutoAtivo = False
                AtivoButtonImage()
            End If
        ElseIf _produto.ProdutoAtivo = False Then ' Produto esta Inativo
            If MessageBox.Show("Você deseja realmente ATIVAR o Produto:" & vbNewLine &
            txtProduto.Text.ToUpper, "Ativar Produto", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                _produto.BeginEdit()
                _produto.ProdutoAtivo = True
                AtivoButtonImage()
            End If
        End If
    End Sub
    '
    ' ALTERA A IMAGEM E O TEXTO DO BOTÃO ATIVO E DESATIVO
    Private Sub AtivoButtonImage()
        If _produto.ProdutoAtivo = True Then ' Nesse caso é Produto Ativo
            btnAtivo.Image = AtivarImage
            btnAtivo.Text = "Produto Ativo"
        ElseIf _produto.ProdutoAtivo = False Then ' Nesse caso é Produto Inativo
            btnAtivo.Image = DesativarImage
            btnAtivo.Text = "Produto Inativo"
        End If
    End Sub
    '
    ' FECHA O FORMULÁRIO
    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click
        If IsNothing(_formOrigem) Then
            Close()
            MostraMenuPrincipal()
        Else
            DialogResult = DialogResult.Cancel
            RGEscolhido = Nothing
        End If
    End Sub
    '
    ' BOTÃO NOVO REGISTRO
    Private Sub btnNovoProduto_Click(sender As Object, e As EventArgs) Handles btnNovoProduto.Click
        propProduto = New clProduto
        propAcao = FlagAcao.INSERIR
    End Sub
    '
#End Region '// OPERAÇÕES DE REGISTRO
    '
#Region "OUTRAS FUNÇÕES"
    '
    ' PROCURAR PRODUTO COM O MESMO RGProduto
    Private Function ProcurarProduto_RG(myRGProduto As Integer, Optional myID As Long = 0) As String
        Dim lista As New List(Of clProduto)

        ' VERIFICA A CONEXÃO COM O SQL
        Try
            If Sit = FlagEstado.NovoRegistro Then ' nesse caso é um novo registro
                lista = prodBLL.GetProdutos_Where("RGProduto = " & myRGProduto)
            ElseIf Sit = FlagEstado.RegistroSalvo Then
                lista = prodBLL.GetProdutos_Where("IDProduto <> " & myID & " AND RGProduto = " & myRGProduto)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return String.Empty
            Exit Function
        End Try

        If lista.Count < 1 Then
            Return String.Empty
        Else
            Return lista(0).Produto.ToString
        End If
    End Function
    '
    '--- BUSCA UM NOVO NUMERO DE REGISTRO SEM USO
    Private Sub btnProcuraRG_Click(sender As Object, e As EventArgs) Handles btnProcuraRG.Click
        '
        If txtRGProduto.Text.Length > 0 Then
            If MessageBox.Show("Deseja substituir o Reg. Anterior desse Produto" & vbNewLine &
                               "por um novo registro validado pelo sistema?", "Novo Reg. Anterior",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) = DialogResult.No Then
                txtRGProduto.Focus()
                Exit Sub
            End If
        End If
        '
        '--- acessa BD
        Try
            Dim maxRG As Integer = prodBLL.ProcuraMaxRGProduto
            txtRGProduto.Text = Format(maxRG + 1, "0000")
            txtRGProduto.Focus()
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Procurar RGProduto..." & vbNewLine &
            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
    ' USAR SOMENTE NÚMERO NO CAMPO RGPRODUTO
    Private Sub txtRGProduto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRGProduto.KeyPress
        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = vbBack Then
            e.Handled = True
        End If
    End Sub
    '
    ' AO ENTRAR NO MENU SELECIONAR O btnProcurar
    Private Sub tsMenu_Enter(sender As Object, e As EventArgs) Handles tsMenu.Enter
        If btnProcurar.Enabled Then btnProcurar.Select()
    End Sub
    '
    ' REMOVE OS ACENTOS DA DIGITAÇÃO NO txtProduto
    Private Sub txtProduto_Validating(sender As Object, e As CancelEventArgs) Handles txtProduto.Validating
        '
        If txtProduto.Text.Trim.Length > 0 Then
            Dim f As New FuncoesUtilitarias
            txtProduto.Text = f.removeAcentos(txtProduto.Text)
        End If
        '
    End Sub
    '
    '--- BLOQUEIA PRESS A TECLA (+)
    Private Sub me_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        '
        If e.KeyChar = "+" Then
            '--- cria uma lista de controles que serao impedidos de receber '+'
            Dim controlesBloqueados As String() = {
                "txtRGProduto",
                "txtProdutoTipo",
                "txtProdutoSubTipo",
                "txtProdutoCategoria",
                "txtAutor",
                "cmbIDFabricante",
                "txtDescontoCompra",
                "txtPCompra",
                "txtPVenda",
                "txtMargem",
                "txtMargemMin",
                "txtDesconto"
            }
            '
            If controlesBloqueados.Contains(ActiveControl.Name) Then
                e.Handled = True
            End If
            '
        End If
        '
    End Sub
    '
    '--- EXECUTAR A FUNCAO DO BOTAO QUANDO PRESSIONA A TECLA (+) NO CONTROLE
    '--- ACIONA ATALHO TECLA (+) E (DEL) | IMPEDE INSERIR TEXTO NOS CONTROLES
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) _
        Handles txtRGProduto.KeyDown,
                txtProdutoTipo.KeyDown,
                txtProdutoSubTipo.KeyDown,
                txtProdutoCategoria.KeyDown,
                txtAutor.KeyDown,
                cmbIDFabricante.KeyDown,
                txtDescontoCompra.KeyDown,
                txtPCompra.KeyDown,
                txtPVenda.KeyDown,
                txtMargem.KeyDown,
                txtMargemMin.KeyDown,
                txtDesconto.KeyDown 'cmbIDProdutoSubTipo.KeyDown, cmbIDCategoria.KeyDown,
        '
        Dim ctr As Control = DirectCast(sender, Control)
        '
        If e.KeyCode = Keys.Add Then
            e.Handled = True
            '
            Select Case ctr.Name
                Case "txtRGProduto"
                    btnProcuraRG_Click(New Object, New EventArgs)
                Case "txtProdutoTipo"
                    ProcurarEscolherTipo(sender)
                Case "txtProdutoSubTipo"
                    ProcurarEscolherTipo(sender)
                Case "txtProdutoCategoria"
                    ProcurarEscolherTipo(sender)
                Case "txtAutor"
                    btnAutoresLista_Click(New Object, New EventArgs)
                Case "cmbIDFabricante"
                    btnFabricantes_Click(New Object, New EventArgs)
                Case "txtPCompra"
                    AbrePainelMargem(txtMargem, txtPCompra)
                Case "txtPVenda"
                    AbrePainelMargem(txtDesconto, txtPVenda)
                Case "txtDescontoCompra"
                    AbrePainelMargem(txtDesconto, txtDescontoCompra)
                Case "txtMargem"
                    FechaPainelMargem()
                Case "txtMargemMin"
                    FechaPainelMargem()
                Case "txtDesconto"
                    FechaPainelMargem()
            End Select
            '
        ElseIf e.KeyCode = Keys.Delete Then
            e.Handled = True
            Select Case ctr.Name
                Case "txtProdutoTipo"
                    If Not IsNothing(_produto.IDProdutoTipo) Then Sit = FlagEstado.Alterado
                    txtProdutoTipo.Clear()
                    _produto.IDProdutoTipo = Nothing
                Case "txtProdutoSubTipo"
                    If Not IsNothing(_produto.IDProdutoSubTipo) Then Sit = FlagEstado.Alterado
                    txtProdutoSubTipo.Clear()
                    _produto.IDProdutoSubTipo = Nothing
                Case "txtProdutoCategoria"
                    If Not IsNothing(_produto.IDCategoria) Then Sit = FlagEstado.Alterado
                    txtProdutoCategoria.Clear()
                    _produto.IDCategoria = Nothing
            End Select
            '
        Else
            '--- cria uma lista de controles que serão bloqueados de alteracao
            Dim controlesBloqueados As New List(Of String)
            controlesBloqueados.AddRange({"txtProdutoTipo", "txtProdutoSubTipo", "txtProdutoCategoria"})
            '
            If controlesBloqueados.Contains(ctr.Name) Then
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End If
        '
    End Sub
    '
    '--- SUBSTITUI A TECLA (ENTER) PELA (TAB)
    Private Sub txtControl_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProduto.KeyDown, txtRGProduto.KeyDown,
            txtCodBarrasA.KeyDown, txtAutor.KeyDown, txtUnidade.KeyDown, txtEstoqueNivel.KeyDown,
            txtEstoqueIdeal.KeyDown, txtNCM.KeyDown, txtPCompra.KeyDown, txtPVenda.KeyDown
        '
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
    '--- PROCURA PELO CADASTRO DADOS ANTERIORES DA NA X_TBLPRODUTOS
    Private Sub txtRGProduto_Validating(sender As Object, e As CancelEventArgs) Handles txtRGProduto.Validating
        '
        Try
            '--- procura no cadastro antigo o registro do produto pelo RG
            Dim clP As clProduto = prodBLL.ProcuraProduto_CadastroAntigo(txtRGProduto.Text)
            '
            If IsNothing(clP) Then Return
            '
            Dim msn As String = String.Format("Deseja preencher automaticamente com os dados anteriores? {4}{4} " +
                                              "PRODUTO: {0}{4} AUTOR: {1}{4} PREÇO DE VENDA: {2}{4} PREÇO DE COMPRA: {3}",
                                              clP.Produto, clP.Autor, Format(clP.PVenda, "C"), Format(clP.PCompra, "C"), vbNewLine)
            '
            If MessageBox.Show(msn, "Obter Dados Anteriores",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = vbNo Then Return
            '
            _produto.Produto = clP.Produto
            _produto.Autor = clP.Autor
            _produto.PVenda = clP.PVenda
            _produto.PCompra = clP.PCompra
            _produto.EstoqueNivel = clP.EstoqueNivel
            _produto.EstoqueIdeal = clP.EstoqueIdeal
            _produto.DescontoCompra = clP.DescontoCompra
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Obter cadastro de produto anterior..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
    End Sub
    '
#End Region '// OUTRAS FUNÇÕES
    '
#Region "CONTROLE PAINEL MARGEM"
    '
    Private Sub AbrePainelMargem(FocusTo As TextBox, Sender As Control)
        '
        Dim x As Integer = Sender.Location.X - pnlCalculo.Width + Sender.Width
        Dim y As Integer = Sender.Location.Y - pnlCalculo.Height - 2
        '
        pnlCalculo.Location = New Point(x, y)
        '
        pnlCalculo.Visible = True
        pnlCalculo.Tag = Sender.Name
        txtMargem.Text = Format(CalcMargemDescontoLabel(0), "#,##0.00")
        txtDesconto.Text = Format(CalcMargemDescontoLabel(1), "#,##0.00")
        txtMargemMin.Text = Format(0, "#,##0.00")
        '
        FocusTo.Focus()
        FocusTo.SelectAll()
        '
    End Sub
    '
    Private Sub FechaPainelMargem()
        '
        pnlCalculo.Visible = False
        Me.Controls(pnlCalculo.Tag).Focus()
        Me.Controls(pnlCalculo.Tag).SelectAll()
        '
    End Sub
    '
    Private Sub txtMargem_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMargem.KeyDown, txtDesconto.KeyDown, txtMargemMin.KeyDown
        '
        If e.KeyCode = Keys.Escape AndAlso pnlCalculo.Visible = True Then
            e.Handled = True
            FechaPainelMargem()
        End If
        '
    End Sub
    '
    Private Sub SoNumero(sender As Object, e As KeyPressEventArgs) Handles txtMargem.KeyPress, txtDesconto.KeyPress, txtMargemMin.KeyPress
        ' converte (.) em (,)
        If e.KeyChar = "." Then
            e.KeyChar = ","
        End If
        ' verifica se foi digitado numero, backspace ou virgula
        If Not Char.IsNumber(e.KeyChar) And Not e.KeyChar = vbBack And Not e.KeyChar = "." And Not e.KeyChar = "," Then
            e.Handled = True
        End If
    End Sub
    '
    Private Sub txtMargem_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMargem.KeyPress
        '
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            If Not IsNumeric(txtMargem.Text) Then
                txtPCompra.Focus()
                txtPCompra.SelectAll()
                pnlCalculo.Visible = False
                Exit Sub
            End If

            If IsNumeric(txtPCompra.Text) Then
                If CDbl(txtPCompra.Text) <= 0 Then
                    MsgBox("Qual é Valor do Preço de Compra?",, "Margem")
                    pnlCalculo.Visible = False
                    txtPCompra.Focus()
                    txtPCompra.SelectAll()
                    Exit Sub
                End If

                txtMargem.Text = Format(CDbl(txtMargem.Text), "#,##0.00")
                If chkArredondar.Checked = True Then
                    txtPVenda.Text = Ceiling(Round(txtPCompra.Text + txtPCompra.Text * ((CDbl(txtMargem.Text)) / 100), 2) * 10) / 10
                Else
                    txtPVenda.Text = Round(txtPCompra.Text + txtPCompra.Text * ((CDbl(txtMargem.Text)) / 100), 2)
                End If

                txtMargem.SelectAll()
                CalcMargemDescontoLabel()
                txtPVenda.DataBindings("Text").WriteValue()
            End If
        End If
    End Sub
    '
    Private Sub txtDesconto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDesconto.KeyPress
        If e.KeyChar = Convert.ToChar(13) Then
            e.Handled = True
            ' verifica se o txtDesconto e numero
            If Not IsNumeric(txtDesconto.Text) Then
                txtPVenda.Focus()
                txtPVenda.SelectAll()
                pnlCalculo.Visible = False
                Exit Sub
            End If
            ' formata o txtdesconto
            txtDesconto.Text = Format(CDbl(txtDesconto.Text), "#,##0.00")
            ' verifica se o txtPCompra e numero e se e maior que zero
            If Not IsNumeric(txtPCompra.Text) OrElse CDbl(txtPCompra.Text) <= 0 Then
                MsgBox("Qual é Valor do Preço de Venda?",, "Desconto")
                pnlCalculo.Visible = False
                txtPCompra.Focus()
                txtPCompra.SelectAll()
                Exit Sub
            End If
            ' executa o calculo
            Dim Pmin As Double

            If IsNumeric(txtMargemMin.Text) Then
                Pmin = txtPCompra.Text + txtPCompra.Text * txtMargemMin.Text / 100
            ElseIf Not IsNumeric(txtMargemMin.Text) OrElse txtMargemMin.Text <= 0 Then
                Pmin = txtPCompra.Text
            End If

            If chkArredondar.Checked = True Then
                txtPVenda.Text = Ceiling(Round(Pmin / (1 - txtDesconto.Text / 100), 2) * 10) / 10
            Else
                txtPVenda.Text = Round(Pmin / (100 - txtDesconto.Text), 2)
            End If

            txtDesconto.SelectAll()
            CalcMargemDescontoLabel()
            txtPCompra.DataBindings("Text").WriteValue()
        End If

    End Sub
    '
    Private Function CalcMargemDescontoLabel() As Double()
        Dim m As String = "Margem de "
        Dim d As String = "Desconto de "
        Dim n As Double
        Dim ret(1) As Double
        '
        If Not IsNothing(_produto.PCompra) AndAlso Not IsNothing(_produto.PVenda) Then
            '
            ' verifica se o valor é dos preços ainda é igual a Zero
            If _produto.PCompra = 0 AndAlso _produto.PVenda = 0 Then
                lblMargem.Text = "Margem de 0,00%"
                lblDesconto.Text = "Desconto de 0,00%"
                ret(0) = 0
                ret(1) = 0
                Return ret
            End If
            '
            ' calcula a margem e coloca no label
            n = Round((CDbl(_produto.PVenda) - CDbl(_produto.PCompra)) / CDbl(_produto.PCompra) * 100, 2)
            ' escreve o label
            lblMargem.Text = m & Format(n, "#,##0.00") & "%"
            ret(0) = n
            ' calcula o desconto e coloca no label
            n = Round((CDbl(_produto.PVenda) - CDbl(_produto.PCompra)) / CDbl(_produto.PVenda) * 100, 2)
            lblDesconto.Text = d & Format(n, "#,##0.00") & "%"
            ret(1) = n
            Return ret
        Else
            ret(0) = 0
            ret(1) = 0
            Return ret
        End If
        '
    End Function
    '
    Private Sub pnlCalculo_Leave(sender As Object, e As EventArgs) Handles pnlCalculo.Leave
        '
        '--- verifica se existe active control no frmProduto
        If IsNothing(ActiveControl) Then Return
        '
        Dim c As Panel = sender
        Dim voltar As Boolean = True
        '
        For Each ctl As Control In pnlCalculo.Controls
            If Me.ActiveControl.Name = ctl.Name Then
                voltar = False
            End If
        Next
        '
        If voltar = True Then
            txtMargem.Focus()
        End If
        '
    End Sub
    '
    Private Sub txtMargem_Leave(sender As Object, e As EventArgs) Handles txtMargem.Leave, txtDesconto.Leave, txtMargemMin.Leave
        '
        Dim txt As TextBox = sender
        If txt.Text.Trim.Length > 0 AndAlso IsNumeric(txt.Text) Then
            txt.Text = Format(CDbl(txt.Text), "#,##0.00")
        End If
        '
    End Sub
    '
    Private Sub txtPVenda_Validated(sender As Object, e As EventArgs) Handles txtPVenda.Validated
        CalcMargemDescontoLabel()
    End Sub
    '
#End Region '// CONTROLE PAINEL MARGEM
    '
#Region "BOTOES FUNCAO"
    '
    '--- BOTAO PROCURAR TIPO
    Private Sub btnProcurarTipo_Click(sender As Object, e As EventArgs) Handles btnProcurarTipo.Click
        ProcurarEscolherTipo(txtProdutoTipo)
    End Sub
    '
    '--- ESCOLHER TIPO DE PRODUTO | SUBTIPO DE PRODUTO | CATEGORIA
    Private Sub ProcurarEscolherTipo(sender As Control)
        '
        Dim frmT As Form = Nothing
        Dim oldID As Integer?
        '
        '--- abre o formulário de ProdutoTipo, SubTipo e Categoria
        Select Case sender.Name
            '
            Case "txtProdutoTipo"
                '
                oldID = _produto.IDProdutoTipo
                frmT = New frmProdutoProcurarTipo(Me, oldID)
                '
            Case "txtProdutoSubTipo"
                '
                ' verifica se existe TIPO selecionado
                If IsNothing(_produto.IDProdutoTipo) Then
                    MessageBox.Show("Favor escolher o TIPO de produto, antes de escolher o SUBTIPO/CLASSIFICAÇÃO...",
                        "Escolher Tipo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtProdutoTipo.Focus()
                    Return
                End If
                '
                oldID = _produto.IDProdutoSubTipo
                frmT = New frmProdutoProcurarSubTipo(Me, oldID, _produto.IDProdutoTipo)
                '
            Case "txtProdutoCategoria"
                '
                ' verifica se existe TIPO selecionado
                If IsNothing(_produto.IDProdutoTipo) Then
                    MessageBox.Show("Favor escolher o TIPO de produto, antes de escolher a CATEGORIA...",
                        "Escolher Tipo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtProdutoTipo.Focus()
                    Return
                End If
                '
                oldID = _produto.IDCategoria
                frmT = New frmProdutoProcurarCategoria(Me, oldID, _produto.IDProdutoTipo)
                '
        End Select
        '
        ' revela o formulario dependendo do controle acionado
        frmT.ShowDialog()
        '
        ' ao fechar dialog verifica o resultado
        If frmT.DialogResult <> DialogResult.Cancel Then
            '
            Select Case sender.Name
            '
                Case "txtProdutoTipo"
                    txtProdutoTipo.Text = DirectCast(frmT, frmProdutoProcurarTipo).propTipo_Escolha
                    _produto.IDProdutoTipo = DirectCast(frmT, frmProdutoProcurarTipo).propIdTipo_Escolha
                    '
                    ' altera o FlagEstado para ALTERADO
                    If IIf(IsNothing(oldID), 0, oldID) <> IIf(IsNothing(_produto.IDProdutoTipo), 0, _produto.IDProdutoTipo) Then
                        '
                        ' remove o SUBTIPO e a CATEGORIA porque o TIPO foi alterado
                        txtProdutoSubTipo.Text = ""
                        _produto.IDProdutoSubTipo = Nothing
                        txtProdutoCategoria.Text = ""
                        _produto.IDCategoria = Nothing
                        '
                        ' altera o FLAGestado
                        Sit = FlagEstado.Alterado
                        '
                    End If
                    '
                    ' move focus
                    txtProdutoTipo.Focus()
                    '
                Case "txtProdutoSubTipo"
                    '
                    ' define o SubTipo escolhido
                    txtProdutoSubTipo.Text = DirectCast(frmT, frmProdutoProcurarSubTipo).propSubTipo_Escolha
                    _produto.IDProdutoSubTipo = DirectCast(frmT, frmProdutoProcurarSubTipo).propIdSubTipo_Escolha
                    '
                    ' altera o FlagEstado para ALTERADO
                    If IIf(IsNothing(oldID), 0, oldID) <> IIf(IsNothing(_produto.IDProdutoSubTipo), 0, _produto.IDProdutoSubTipo) Then
                        Sit = FlagEstado.Alterado
                    End If
                    '
                    ' move focus
                    txtProdutoSubTipo.Focus()
                    '
                Case "txtProdutoCategoria"
                    '
                    ' define a categoria escolhida
                    txtProdutoCategoria.Text = DirectCast(frmT, frmProdutoProcurarCategoria).propCategoria_Escolha
                    _produto.IDCategoria = DirectCast(frmT, frmProdutoProcurarCategoria).propIdCategoria_Escolha
                    '
                    ' altera o FlagEstado para ALTERADO
                    If IIf(IsNothing(oldID), 0, oldID) <> IIf(IsNothing(_produto.IDCategoria), 0, _produto.IDCategoria) Then
                        Sit = FlagEstado.Alterado
                    End If
                    '
                    ' move focus
                    txtProdutoCategoria.Focus()
                    '
            End Select
            '
        End If
        '
    End Sub
    '
    Private Sub btnNovoTipo_Click(sender As Object, e As EventArgs) Handles btnNovoTipo.Click
        '
        Dim myTipo As Integer? = _produto.IDProdutoTipo
        Dim frmTipo As Form = New frmProdutoTipo(frmProdutoTipo.ProcurarPor.Tipo, myTipo)
        '
        Panel1.BackColor = Color.Silver
        frmTipo.ShowDialog()
        Panel1.BackColor = Color.SlateGray
        '
        '--- suspende o Binding para preservar os valores
        lblIDProduto.Tag = 0
        bindP.SuspendBinding()
        '
        '--- carrega os combos novamente
        'VerificaAlteracoes_Tipo_SubTipo_Categoria

        '
        '--- retoma os Binding que foi suspenso
        bindP.ResumeBinding()
        '
    End Sub

    Private Sub btnAutoresLista_Click(sender As Object, e As EventArgs) Handles btnAutoresLista.Click
        Dim frmAut As New frmAutoresProcurar(Me)
        '
        Panel1.BackColor = Color.Silver
        frmAut.ShowDialog()
        '
        If frmAut.DialogResult = DialogResult.Yes Then
            txtAutor.Text = frmAut.propAutorEscolhido
        End If
        '
        Panel1.BackColor = Color.SlateGray
        '
        txtAutor.Focus()
        txtAutor.SelectAll()
    End Sub

    Private Sub btnFabricantes_Click(sender As Object, e As EventArgs) Handles btnFabricantes.Click
        '
        '--- abre o formulário de ProdutoTipo, SubTipo e Categoria
        Dim frmFab As New frmFabricante(True, Me)
        frmFab.ShowDialog()
        '
        '--- suspende o Binding para preservar os valores
        lblIDProduto.Tag = 0
        bindP.SuspendBinding()
        '
        '--- carrega os combos novamente
        CarregaComboFabricante()
        '
        '--- retoma os Binding que foi suspenso
        bindP.ResumeBinding()
        '
        '--- seleciona o focu
        cmbIDFabricante.Focus()
        '
    End Sub
    '
    Private Sub frmProduto_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        If e.KeyCode = Keys.Escape AndAlso pnlCalculo.Visible = False Then
            e.Handled = True
            '
            If IsNothing(_formOrigem) Then
                If btnCancelar.Enabled = True Then btnCancelar_Click(New Object, New EventArgs)
            Else
                If MessageBox.Show("Você deseja realmente fechar o formulário de cadastro de Produto?",
                                   "Fechar Formulário", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    btnFechar_Click(New Object, New EventArgs)
                End If
            End If
        End If
        '
    End Sub
    '
#End Region '// BOTOES FUNCAO
    '
#Region "EFEITO VISUAL"
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
    Private Sub frmProduto_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If Not IsNothing(_formOrigem) Then
            Dim pnl As Panel = _formOrigem.Controls("Panel1")
            pnl.BackColor = Color.SlateGray
        End If
    End Sub
    '
    '-------------------------------------------------------------------------------------------------
    ' DESENHA DUAS LINHAS NAS LATERAIS DO PNLCALCULO
    '-------------------------------------------------------------------------------------------------
    Private Sub pnlCalculo_Paint(sender As Object, e As PaintEventArgs) Handles pnlCalculo.Paint
        '
        Dim p As New Pen(Color.Tan, 10)
        '
        Dim p1 As New Point(pnlCalculo.ClientRectangle.Left, pnlCalculo.ClientRectangle.Top)
        Dim p2 As New Point(pnlCalculo.ClientRectangle.Left, pnlCalculo.ClientRectangle.Bottom)
        Dim p3 As New Point(pnlCalculo.ClientRectangle.Right, pnlCalculo.ClientRectangle.Top)
        Dim p4 As New Point(pnlCalculo.ClientRectangle.Right, pnlCalculo.ClientRectangle.Bottom)
        '
        e.Graphics.DrawLine(p, p1, p2)
        e.Graphics.DrawLine(p, p3, p4)
        '
    End Sub
    '
#End Region '// EFEITO VISUAL
    '
#Region "TOOLTIPS"
    '
    Private Sub addToolTipHandler()
        '
        ' Define o texto da ToolTip para o Button, TextBox, Checkbox e Label
        txtMargem.Tag = "Margem Bruta aplicada sobre Preço de Compra"
        txtMargemMin.Tag = "Margem Mínima de lucro, mesmo com desconto máximo"
        txtDesconto.Tag = "Desconto Máximo para alcançar a Margem Mínima"
        txtDescontoCompra.Tag = "Abrir caixa para cálculo do Preço de Venda"
        chkArredondar.Tag = "Arrendondar centavos para cima"
        btnProcuraRG.Tag = "Procurar um Novo Reg. vazio para o Produto"
        btnFabricantes.Tag = "Editar Fabricantes de Produtos"
        btnAutoresLista.Tag = "Escolher Autor pelo Nome"
        '
        Dim listControles As New List(Of Control)
        '
        listControles.Add(txtProdutoTipo)
        listControles.Add(txtProdutoSubTipo)
        listControles.Add(txtProdutoCategoria)
        listControles.Add(txtAutor)
        listControles.Add(cmbIDFabricante)
        listControles.Add(txtMargem)
        listControles.Add(txtMargemMin)
        listControles.Add(txtDesconto)
        listControles.Add(txtDescontoCompra)
        listControles.Add(chkArredondar)
        listControles.Add(btnProcuraRG)
        listControles.Add(btnFabricantes)
        listControles.Add(btnAutoresLista)
        '
        For Each c As Control In listControles
            AddHandler c.GotFocus, AddressOf showToolTip
            AddHandler c.MouseHover, AddressOf showToolTip
        Next
        '
    End Sub
    '
    Private Sub showToolTip(sender As Object, e As EventArgs)
        '
        Dim myControl As Control = DirectCast(sender, Control)
        '
        ' Cria a ToolTip e associa com o Form container.
        Dim toolTip1 As New ToolTip()
        '
        ' Define o delay para a ToolTip.
        With toolTip1
            .AutoPopDelay = 2000
            .InitialDelay = 1000
            .ReshowDelay = 500
            .IsBalloon = True
            .UseAnimation = True
            .UseFading = True
        End With
        '
        If myControl.Tag = "" Then
            toolTip1.Show("Pressione '+'  para escolher...", myControl, myControl.Width - 30, -40, 1000)
        Else
            toolTip1.Show(myControl.Tag, myControl, myControl.Width - 30, -40, 1000)
        End If
        '
    End Sub
    '
#End Region '// TOOLTIPS
    ' 
End Class
