Imports CamadaBLL
Imports CamadaDTO
Imports ComponentOwl.BetterListView
Imports System.Drawing.Drawing2D
'
Public Class frmMovTipos
    '
    Private WithEvents dtTipos As DataTable
    Private WithEvents bindTipo As New BindingSource
    Private _Sit As FlagEstado '= 1:Registro Salvo; 2:Registro Alterado; 3:Novo registro
    '
    Private AtivarImage As Image = My.Resources.Switch_ON_PEQ
    Private DesativarImage As Image = My.Resources.Switch_OFF_PEQ
    '
#Region "EVENTO LOAD E PROPRIEDADE SIT"
    '
    Private Property Sit As FlagEstado
        Get
            Return _Sit
        End Get
        Set(value As FlagEstado)
            '
            _Sit = value
            '
            If _Sit = FlagEstado.RegistroSalvo Then
                btnSalvar.Enabled = False
                btnNovo.Enabled = True
                btnExcluir.Enabled = True
                btnCancelar.Enabled = False
                lstFormas.ReadOnly = False
                '
            ElseIf _Sit = FlagEstado.Alterado Then
                btnSalvar.Enabled = True
                btnNovo.Enabled = False
                btnExcluir.Enabled = True
                btnCancelar.Enabled = True
                lstFormas.ReadOnly = True
                '
            ElseIf _Sit = FlagEstado.NovoRegistro Then
                txtMovTipo.Select()
                btnSalvar.Enabled = True
                btnNovo.Enabled = False
                btnExcluir.Enabled = False
                btnCancelar.Enabled = True
                lstFormas.ReadOnly = True
                '
            End If
            '
        End Set
        '
    End Property
    '
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        '
        ObterDados()
        bindTipo.DataSource = dtTipos
        '
        If dtTipos.Rows.Count > 0 Then
            bindTipo.MoveFirst()
            PreencheDataBindings()
            Sit = FlagEstado.RegistroSalvo
        Else
            bindTipo.AddNew()
            Sit = FlagEstado.NovoRegistro
            PreencheDataBindings()
        End If
        '
        PreencheListagem()
        '
    End Sub
    '
    Private Sub ObterDados()
        '
        Dim fBLL As New MovimentacaoBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            dtTipos = fBLL.MovTipo_GET_Dt
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Obter a lista dos Tipos de Movimentação..." & vbNewLine &
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
#Region "BINDIGNS"
    '
    Private Sub PreencheDataBindings()
        ' ADICIONANDO O DATABINDINGS AOS CONTROLES TEXT
        ' OS COMBOS JA SÃO ADICIONADOS DATABINDINGS QUANDO CARREGA
        '
        lblIDMovTipo.DataBindings.Add("Text", bindTipo, "IDMovTipo", True, DataSourceUpdateMode.OnPropertyChanged)
        txtMovTipo.DataBindings.Add("Text", bindTipo, "MovTipo", True, DataSourceUpdateMode.OnPropertyChanged)
        lblIDMovTipo.DataBindings.Add("Tag", bindTipo, "Ativo")
        '
        ' CARREGA O COMBO
        CarregaComboMeio()
        '
        ' FORMATA OS VALORES DO DATABINDING
        AddHandler lblIDMovTipo.DataBindings("Text").Format, AddressOf idFormatRG
        '
        ' ADD HANDLER PARA DATABINGS
        AddHandler bindTipo.CurrentChanged, AddressOf handler_CurrentChanged
        AddHandler bindTipo.BindingComplete, AddressOf Handler_AoAlterar
        '
    End Sub
    '
    Private Sub idFormatRG(sender As Object, e As System.Windows.Forms.ConvertEventArgs)
        e.Value = Format(IIf(IsDBNull(e.Value), Nothing, e.Value), "00")
    End Sub
    '
    Private Sub Handler_AoAlterar()
        '
        If Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
    Private Sub handler_CurrentChanged()
        '
        '_MovForma = DirectCast(bindTipo.CurrencyManager.Current, clMovForma)
        '
        ' ADD HANDLER PARA DATABINGS
        'AddHandler bindTipo.CurrentItemChanged, AddressOf Handler_AoAlterar
        '
        '--- Clear the Error Provider
        epValida.Clear()
        '
        '--- Nesse caso é um novo registro
        If Not IsDBNull(DirectCast(bindTipo.Current, DataRowView).Item("IDMovTipo")) Then
            '
            ' ALTERAR PARA REGISTRO SALVO
            Sit = FlagEstado.RegistroSalvo
            '
            ' VERIFICA O ATIVO BUTTON
            AtivoButtonImage()
            '
        End If
        '
    End Sub
    '
    '--- COMBO MEIO
    Private Sub CarregaComboMeio()
        '
        Dim dtMeio As New DataTable
        'Adiciona todas as possibilidades de instrucao
        dtMeio.Columns.Add("IDMeio")
        dtMeio.Columns.Add("Meio")
        dtMeio.Rows.Add(New Object() {1, "Moeda"})
        dtMeio.Rows.Add(New Object() {2, "Cheque"})
        dtMeio.Rows.Add(New Object() {3, "Cartão"})
        '
        With cmbMeio
            .DataSource = dtMeio
            .ValueMember = "IDMeio"
            .DisplayMember = "Meio"
            .DataBindings.Add("SelectedValue", bindTipo, "Meio", True, DataSourceUpdateMode.OnPropertyChanged)
        End With
        '
    End Sub
    '
#End Region
    '
#Region "LISTAGEM FORMATACAO"
    '
    ' FORMATAR A LISTAGEM
    Private Sub PreencheListagem()
        lstFormas.DataSource = bindTipo
        '
        With clnIDMovForma ' column 0
            .Width = 50
            .DisplayMember = "IDMovTipo"
        End With
        '
        With clnMovForma ' column 1
            .Width = 220
            .DisplayMember = "MovTipo"
        End With
        '
    End Sub
    '
    ' ALTERA A COR DO HEADER DO LISTBOX
    Private Sub lstFormas_DrawColumnHeader(sender As Object, eventArgs As BetterListViewDrawColumnHeaderEventArgs) Handles lstFormas.DrawColumnHeader
        '
        If eventArgs.ColumnHeaderBounds.BoundsOuter.Width > 0 AndAlso
            eventArgs.ColumnHeaderBounds.BoundsOuter.Height > 0 Then
            ' Pode Colocar: AndAlso eventArgs.ColumnHeader.Index = 1 AndAlso
            '
            Dim brush As Brush = New LinearGradientBrush(eventArgs.ColumnHeaderBounds.BoundsOuter, Color.Transparent, Color.FromArgb(100, Color.LightSlateGray), LinearGradientMode.Vertical)
            Dim p As Pen = New Pen(Color.SlateGray, 2)
            '
            eventArgs.Graphics.FillRectangle(brush, eventArgs.ColumnHeaderBounds.BoundsOuter)
            '
            eventArgs.Graphics.DrawLine(p, eventArgs.ColumnHeaderBounds.BoundsOuter.X, 'x1
                                        eventArgs.ColumnHeaderBounds.BoundsOuter.Height, 'y1
                                        eventArgs.ColumnHeaderBounds.BoundsOuter.Width + eventArgs.ColumnHeaderBounds.BoundsOuter.X, 'x2
                                        eventArgs.ColumnHeaderBounds.BoundsOuter.Height) 'y2
            brush.Dispose()
            p.Dispose()
        End If
        '
    End Sub
    '
    ' ESCREVE OS TEXTOS DE TIPO DE ACESSO NA LISTAGEM
    Private Sub lstFormas_DrawItem(sender As Object, eventArgs As BetterListViewDrawItemEventArgs) Handles lstFormas.DrawItem
        '
        If IsNumeric(eventArgs.Item.Text) Then
            eventArgs.Item.Text = Format(CInt(eventArgs.Item.Text), "00")
        End If
        '
    End Sub
#End Region
    '
#Region "ACAO DOS BOTOES"
    '
    ' ATIVAR OU DESATIVAR FORMA BOTÃO
    Private Sub btnAtivo_Click(sender As Object, e As EventArgs) Handles btnAtivo.Click
        '
        If Sit = FlagEstado.NovoRegistro Then
            MessageBox.Show("Você não pode DESATIVAR um Novo Tipo de Movimentação", "Desativar Tipo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        '
        If btnAtivo.Tag = True Then '---usuario ativo
            If MessageBox.Show("Você deseja realmente DESATIVAR a Forma de Entrada:" & vbNewLine &
            txtMovTipo.Text.ToUpper, "Desativar Forma", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
        Else '---Usuário Inativo
            If MessageBox.Show("Você deseja realmente ATIVAR a Forma de Entrada:" & vbNewLine &
            txtMovTipo.Text.ToUpper, "Ativar Forma", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then Exit Sub
        End If
        '
        lblIDMovTipo.Tag = Not lblIDMovTipo.Tag
        '
        If Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
        AtivoButtonImage()
        '
    End Sub
    '
    ' BOTÃO CANCELAR
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        '
        If Sit = FlagEstado.Alterado Then ' REGISTRO ALTERADO
            If MessageBox.Show("Deseja cancelar todas as alterações feitas no registro atual?",
                               "Cancelar Alterações", MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                '
                bindTipo.CancelEdit()
                txtMovTipo.Focus()
                AtivoButtonImage()
                Sit = FlagEstado.RegistroSalvo
                '
            End If
        ElseIf Sit = FlagEstado.NovoRegistro Then ' REGISTRO NOVO
            If lstFormas.Items.Count = 0 Then
                MessageBox.Show("Não é possível cancelar porque não existe outro registro." & vbNewLine &
                                "Se deseja sair apenas feche a janela!", "Cancelar Edição", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                Exit Sub
            End If
            '
            Sit = FlagEstado.RegistroSalvo
            bindTipo.RemoveCurrent()
            '
        End If
        '
    End Sub
    '
    ' BOTÃO NOVO REGISTRO
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        '
        lstFormas.SelectedItems.Clear()
        '
        '---cria um novo DataRow com valores padrão
        Dim row As DataRow = dtTipos.NewRow
        row("Ativo") = True
        row("MovTipo") = ""
        row("IDMovTipo") = 0
        '
        '---adiciona o NewROW
        dtTipos.Rows.Add(row)
        bindTipo.MoveLast()
        '
        '---altera o SIT
        Sit = FlagEstado.NovoRegistro
        txtMovTipo.Focus()
        '
    End Sub
    '
    ' BOTÃO FECHAR
    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click, btnClose.Click
        Close()
        MostraMenuPrincipal()
    End Sub
    '
    ' BOTÃO EXCLUIR
    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        MsgBox("Ainda não implementado")
    End Sub
    '
#End Region
    '
#Region "SALVA REGISTRO"
    '
    '--- BTN SALVAR REGISTRO
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        ''
        'If VerificaDados() = False Then Exit Sub
        ''
        'Dim movBLL As New MovimentacaoBLL
        ''
        'If Sit = FlagEstado.NovoRegistro Then
        '    Try
        '        Dim newID As Int16 = movBLL.MovForma_Inserir(_MovForma)
        '        '
        '        _MovForma.IDMovForma = newID
        '        lblIDMovTipo.DataBindings("Text").ReadValue()
        '        '
        '        Sit = FlagEstado.RegistroSalvo
        '        bindTipo.EndEdit()
        '        bindTipo.ResetBindings(True)
        '        '
        '        '---informa o usuário
        '        MessageBox.Show("Registro Inserido com sucesso!", "Registro Salvo",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        '
        '    Catch ex As Exception
        '        MessageBox.Show("Esse Registro NÃO foi salvo com sucesso!" & vbNewLine &
        '                        ex.Message, "Exceção Inesperada",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        '    '
        'ElseIf Sit = FlagEstado.Alterado Then
        '    Try
        '        movBLL.MovForma_Update(_MovForma)
        '        '
        '        Sit = FlagEstado.RegistroSalvo
        '        bindTipo.EndEdit()
        '        '
        '        '---informa o usuário
        '        MessageBox.Show("Registro Atualizado com sucesso!", "Registro Salvo",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        '
        '    Catch ex As Exception
        '        MessageBox.Show("Esse Registro NÃO foi salvo com sucesso!" & vbNewLine &
        '                        ex.Message, "Exceção Inesperada",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    End Try
        'End If
        ''
    End Sub
    '
    ' VERIFICA OS DADOS ANTES DE SALVAR
    Private Function VerificaDados() As Boolean
        ''
        'Dim f As New FuncoesUtilitarias
        ''
        'If Not f.VerificaDadosClasse(txtMovTipo, "Descrição da Forma de Movimento", _MovForma, epValida) Then
        '    Return False
        'End If
        ''
        'If Not f.VerificaDadosClasse(txtMovTipo, "Tipo da Forma de Pagamento", _MovForma, epValida) Then
        '    Return False
        'End If
        ''
        'If Not IsNothing(_MovForma.IDCartao) Then
        '    '
        '    If Not f.VerificaDadosClasse(txtNoDias, "Intervalo/Prazo de pagamento do Cartão", _MovForma, epValida) Then
        '        Return False
        '    End If
        '    '
        '    If _MovForma.NoDias <= 0 Then
        '        MessageBox.Show("O número de dias não pode ser igual ou menor que Zero...", "Aviso",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        txtNoDias.Focus()
        '        Return False
        '    End If
        '    '
        '    If Not f.VerificaDadosClasse(txtParcelas, "Quantidade de Parcelas de pagamento", _MovForma, epValida) Then
        '        Return False
        '    End If
        '    '
        '    If _MovForma.Parcelas < 0 Then
        '        MessageBox.Show("O número de parcelas não pode ser menor que Zero...", "Aviso",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        txtParcelas.Focus()
        '        Return False
        '    End If
        '    '
        '    If Not f.VerificaDadosClasse(txtComissao, "Comissão da Operadora de Cartão", _MovForma, epValida) Then
        '        Return False
        '    End If
        '    '
        '    If _MovForma.Comissao < 0 Or _MovForma.Comissao > 99 Then
        '        MessageBox.Show("A taxa de comissão não pode ser menor que Zero ou maior que 100%", "Aviso",
        '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        txtComissao.Focus()
        '        Return False
        '    End If
        '    '
        'Else
        '    VerificaCartaoTipo(True)
        'End If
        ''
        'Return True
        ''
    End Function
    '
#End Region
    '
#Region "ATALHOS E FUNCOES UTILITARIAS"
    '
    ' ALTERA A IMAGEM E O TEXTO DO BOTÃO ATIVO E DESATIVO
    Private Sub AtivoButtonImage()
        '
        If lblIDMovTipo.Tag = True Then ' Nesse caso é Tipo Ativo
            btnAtivo.Image = AtivarImage
            btnAtivo.Text = "Tipo Ativo"
        ElseIf lblIDMovTipo.Tag = False Then ' Nesse caso é Tipo Inativo
            btnAtivo.Image = DesativarImage
            btnAtivo.Text = "Tipo Inativo"
        Else
            btnAtivo.Image = AtivarImage
            btnAtivo.Text = "Tipo Ativo"
        End If
        '
    End Sub
    '
    '--- SUBSTITUI A TECLA (ENTER) PELA (TAB)
    Private Sub txtControl_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMovTipo.KeyDown, txtMovTipo.KeyDown
        '
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SendKeys.Send("{Tab}")
        End If
        '
    End Sub
    '
#End Region
    '
End Class
