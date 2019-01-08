Imports System.IO
Imports System.Xml
Imports VIBlend.WinForms.Controls
Imports CamadaBLL
Imports CamadaDTO

Public Class frmConfig
    '
    ' VARIÁVEIS E PROPRIEDADES
    Dim Px, Py As Integer
    Dim Mover As Boolean
    Private _Conta As clConta
    Private IDConta_Config As Short?
    Private IDFilial_Config As Integer?
    Private _Sit As FlagEstado
    Private ImageLogoMono As Image
    Private ImageLogoColor As Image
    Dim _VerAlteracao As Boolean = False
    '
#Region "LOAD"
    '
    Private Property Sit As FlagEstado
        Get
            Return _Sit
        End Get
        Set(value As FlagEstado)
            _Sit = value
            If _Sit = FlagEstado.RegistroSalvo Then
                btnSalvar.Enabled = False
                btnCancelar.Text = "Fechar"
            ElseIf _Sit = FlagEstado.Alterado Then
                btnSalvar.Enabled = True
                btnCancelar.Text = "Cancelar"
            ElseIf _Sit = FlagEstado.NovoRegistro Then
                btnSalvar.Enabled = True
                btnCancelar.Text = "Sair"
            End If
        End Set
    End Property
    '
    ' EVENTO LOAD
    Private Sub frmConfig_Load(sender As Object, e As EventArgs) Handles Me.Load
        '
        If File.Exists(Application.StartupPath & "\ConfigFiles\Config.xml") Then
            '
            PreencheOperacoesCFOP()
            '
            If LerConfigXML() = False Then
                MessageBox.Show("O arquivo de Configuração possui um erro de leitura..." & vbNewLine &
                                "Preencha todos os campos para gravar nova Configuração do Sistema.",
                                "Arquivo Corrompido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Sit = FlagEstado.Alterado
            Else
                Sit = FlagEstado.RegistroSalvo
            End If
            '
        Else
            '
            Sit = FlagEstado.NovoRegistro
            '
        End If
        '
        '-- Get Conta Padrao
        If Not IsNothing(IDConta_Config) Then
            GetConta(IDConta_Config)
        Else
            _Conta = New clConta
        End If
        '
        '--- Verifica Data de Bloqueio da Conta
        VerficaBloqueioDataConta()
        '
        '--- Get Connection String
        Dim bBLL As New BackupBLL
        txtStringConexao.Text = bBLL.GetConString
        '
        '--- Add Handlers
        HandlerChangedControles()
        _VerAlteracao = True
        '
    End Sub
    '
    ' GET OS DADOS DA CONTA PADRAO
    Private Sub GetConta(IDConta As Byte)
        Dim mBLL As New MovimentacaoBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            '--- Get Dados da Conta
            _Conta = mBLL.Conta_GET_PorIDConta(IDConta)
            VerificaValoresDefault()
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Obter a Conta pelo ID..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            _Conta = New clConta
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Sub
    '
    '--- VERIFICA INTEGRIDADE DO ARQUIVO DE CONFIG
    Private Sub VerificaValoresDefault()
        '
        '--- Descricao da Conta
        If Not _Conta.Conta.Equals(txtContaPadrao.Text) Then
            txtContaPadrao.Text = _Conta.Conta
            Sit = FlagEstado.Alterado
        End If
        '
        '--- Descricao da Filial
        If Not _Conta.ApelidoFilial.Equals(txtFilialPadrao.Text) Then
            txtFilialPadrao.Text = _Conta.ApelidoFilial
            Sit = FlagEstado.Alterado
        End If
        '
        '-- IDFilial
        If Not _Conta.IDFilial.Equals(IDFilial_Config) Then
            IDFilial_Config = _Conta.IDFilial
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
    Private Function LerLogosImagem() As Boolean
        '
        Dim resp As Boolean = False
        '
        ' Ler a imagem do arquivo da LOGO COLOR
        If txtLogoColorCaminho.Text.Length > 0 Then
            '
            Try
                ImageLogoColor = Image.FromFile(txtLogoColorCaminho.Text)
                'ImageLogoColor = Image.FromFile(Application.StartupPath + "\Imagens\LogoColor.png")
                picLogoColor.Image = ImageLogoColor
                '
                resp = True
                '
            Catch ex As Exception
                MessageBox.Show("O arquivo de imagem da LOGO Colorida não foi encontrado no caminho especificado:" & vbNewLine &
                                txtLogoMonoCaminho.Text, "Erro: Arquivo da Logo", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                resp = False
                '
            End Try
            '
        End If
        '
        ' Ler a imagem do arquivo da LOGO MONO
        If txtLogoMonoCaminho.Text.Length > 0 Then
            '
            Try
                ImageLogoMono = Image.FromFile(txtLogoMonoCaminho.Text)
                'ImageLogoMono = Image.FromFile(Application.StartupPath + "\Imagens\LogoMono.png")
                picLogoMono.Image = ImageLogoMono
                '
            Catch ex As Exception
                MessageBox.Show("O arquivo de imagem da LOGO Monocromática não foi encontrado no caminho especificado:" & vbNewLine &
                                txtLogoMonoCaminho.Text, "Erro: Arquivo da Logo", MessageBoxButtons.OK,
                                MessageBoxIcon.Information)
                '
                resp = False
                '
            End Try
            '
        End If
        '
        Return resp
        '
    End Function
    '
#End Region
    '
#Region "ARQUIVO XML"
    '
    ' LER O ARQUIVO XML
    '-----------------------------------------------------------------------------------------------
    Private Function LerConfigXML() As Boolean
        Dim myXML As New XmlDocument
        '
        myXML.Load(Application.StartupPath & "\ConfigFiles\Config.xml")
        '
        On Error GoTo Error_Handler

        With myXML.SelectSingleNode("Configuracao")

            ' Lendo a PRIMEIRA Seção
            With .SelectSingleNode("ValoresPadrao")
                '
                dtpDataPadrao.Value = .SelectSingleNode("DataPadrao").InnerText
                IDFilial_Config = If(String.IsNullOrEmpty(.SelectSingleNode("FilialPadrao").InnerText), Nothing, .SelectSingleNode("FilialPadrao").InnerText)
                txtFilialPadrao.Text = .SelectSingleNode("FilialDescricao").InnerText
                IDConta_Config = If(String.IsNullOrEmpty(.SelectSingleNode("ContaPadrao").InnerText), Nothing, .SelectSingleNode("ContaPadrao").InnerText)
                txtContaPadrao.Text = .SelectSingleNode("ContaDescricao").InnerText
                lblDataBloqueio.Text = .SelectSingleNode("DataBloqueada").InnerText
                txtCidade.Text = .SelectSingleNode("Cidade").InnerText
                txtUF.Text = .SelectSingleNode("UF").InnerText
                txtNaturalidade.Text = .SelectSingleNode("Naturalidade").InnerText
                txtMensagem.Text = .SelectSingleNode("MensagemInicial").InnerText
                'IDProdutoTipoPadrao = .SelectSingleNode("IDProdutoTipoPadrao").InnerText
                'ProdutoTipoPadrao = .SelectSingleNode("ProdutoTipoPadrao").InnerText
                'IDProdutoSubTipoPadrao = .SelectSingleNode("IDProdutoSubTipoPadrao").InnerText
                'ProdutoSubTipoPadrao = .SelectSingleNode("ProdutoSubTipoPadrao").InnerText
                txtPermanencia.Text = .SelectSingleNode("TaxaPermanencia").InnerText
                '
            End With
            '
            ' Lendo a SEGUNDA seção
            With .SelectSingleNode("DadosEmpresa")
                '
                txtRazao.Text = .SelectSingleNode("RazaoSocial").InnerText
                txtFantasia.Text = .SelectSingleNode("NomeFantasia").InnerText
                txtCNPJ.Text = .SelectSingleNode("CNPJ").InnerText
                txtIncricao.Text = .SelectSingleNode("InscricaoSocial").InnerText
                txtTelPrincipal.Text = .SelectSingleNode("TelefonePrincipal").InnerText
                txtTelGerencia.Text = .SelectSingleNode("TelefoneGerencia").InnerText
                txtContatoGerencia.Text = .SelectSingleNode("ContatoGerencia").InnerText
                txtTelFinanceiro.Text = .SelectSingleNode("TelefoneFinanceiro").InnerText
                txtContatoFinanceiro.Text = .SelectSingleNode("ContatoFinanceiro").InnerText
                '
            End With

            ' Lendo a TERCEIRA seção
            txtLogoMonoCaminho.Text = .SelectSingleNode("ArquivoLogo").ChildNodes(0).InnerText
            txtLogoColorCaminho.Text = .SelectSingleNode("ArquivoLogo").ChildNodes(1).InnerText
            '
            ' Lendo a QUARTA seção
            txtStringConexao.Text = .SelectSingleNode("ServidorDados").ChildNodes(0).InnerText
            chkVerBackup.Checked = CType(.SelectSingleNode("ServidorDados").ChildNodes(1).InnerText, Boolean)
            '
            If .SelectSingleNode("ServidorDados").ChildNodes(2).InnerText Then
                rbtServLocal.Checked = True
            Else
                rbtServRemoto.Checked = True
            End If
            '
        End With
        Return True
        '
Error_Handler:
        MessageBox.Show(Err.Description)
        Resume Next
        '
    End Function
    '
    ' CRIA ARQUIVO XML
    '-----------------------------------------------------------------------------------------------
    Private Function CriaConfigXML() As Boolean
        '
        Try
            '
            Dim writer As New XmlTextWriter("ConfigFiles\Config.xml", Nothing)
            With writer
                .WriteStartDocument()
                '
                'define a indentação do arquivo
                .Formatting = Formatting.Indented
                .WriteStartElement("Configuracao")

                ' Primeira Seção: VALORES PADRAO
                '----------------------------------------------------------------------------------
                .WriteStartElement("ValoresPadrao")
                ' Sub Elementos
                .WriteElementString("DataPadrao", dtpDataPadrao.Value.ToShortDateString)
                .WriteElementString("FilialPadrao", _Conta.IDFilial)
                .WriteElementString("FilialDescricao", _Conta.ApelidoFilial)
                .WriteElementString("ContaPadrao", _Conta.IDConta)
                .WriteElementString("ContaDescricao", _Conta.Conta)
                .WriteElementString("DataBloqueada", If(_Conta.BloqueioData, ""))
                .WriteElementString("Cidade", txtCidade.Text)
                .WriteElementString("UF", txtUF.Text)
                .WriteElementString("Naturalidade", txtNaturalidade.Text)
                .WriteElementString("MensagemInicial", txtMensagem.Text)
                .WriteElementString("IDProdutoTipoPadrao", "")
                .WriteElementString("ProdutoTipoPadrao", "")
                .WriteElementString("IDProdutoSubTipoPadrao", "")
                .WriteElementString("ProdutoSubTipoPadrao", "")
                .WriteElementString("TaxaPermanencia", txtPermanencia.Text)
                'encerra o elemento
                .WriteEndElement()

                ' Segunda Seção: DADOS DA EMPRESA
                '----------------------------------------------------------------------------------
                .WriteStartElement("DadosEmpresa")
                ' Sub Elementos
                .WriteElementString("RazaoSocial", txtRazao.Text)
                .WriteElementString("NomeFantasia", txtFantasia.Text)
                .WriteElementString("CNPJ", txtCNPJ.Text)
                .WriteElementString("InscricaoSocial", txtIncricao.Text)
                .WriteElementString("TelefonePrincipal", txtTelPrincipal.Text)
                .WriteElementString("TelefoneGerencia", txtTelGerencia.Text)
                .WriteElementString("ContatoGerencia", txtContatoGerencia.Text)
                .WriteElementString("TelefoneFinanceiro", txtTelFinanceiro.Text)
                .WriteElementString("ContatoFinanceiro", txtContatoFinanceiro.Text)
                'encerra o elemento
                .WriteEndElement()

                ' Terceira Seção: ARQUIVOS IMPORTANTES
                '----------------------------------------------------------------------------------
                .WriteStartElement("ArquivoLogo")
                ' Sub Elementos
                .WriteElementString("ArquivoLogoMono", txtLogoMonoCaminho.Text)
                .WriteElementString("ArquivoLogoColor", txtLogoColorCaminho.Text)
                .WriteElementString("BackupDir", "")
                'encerra o elemento
                .WriteEndElement()

                ' Quarta Seção: SERVIDOR
                '----------------------------------------------------------------------------------
                .WriteStartElement("ServidorDados")
                ' Sub Elementos
                .WriteElementString("StringConexao", txtStringConexao.Text)
                .WriteElementString("ControlaBackup", chkVerBackup.Checked.ToString)
                .WriteElementString("ServidorLocal", rbtServLocal.Checked.ToString)
                'encerra o elemento
                .WriteEndElement()
                '
                'FECHA CONFIGURAÇÃO
                .WriteEndElement()
                'escreve o xml para o arquivo e fecha o objeto escritor
                .Close()
                '
                '--- return
                Return True
                '
            End With
            '
        Catch ex As Exception
            '
            MessageBox.Show(ex.Message)
            Return False
            '
        End Try
        '
    End Function
    '
#End Region
    '
#Region "DATAGRID OPERACOES"
    '
    ' PREENCHER DATAGRIDVIEW OPERACOES
    '-----------------------------------------------------------------------------------------------
    Private Sub PreencheOperacoesCFOP()
        Dim SQL As New SQLControl
        Dim dtOp As New DataTable
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            SQL.ExecQuery("SELECT * FROM tblOperacao ORDER BY MovimentoEstoque")
            dtOp = SQL.DBDT
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao obter lista de Operações..." & vbNewLine &
            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
        dgvOperacao.DataSource = dtOp
        FormataListagem()
        '
    End Sub
    '
    Private Sub FormataListagem()
        '
        With dgvOperacao
            ' limpa as colunas do datagrid
            .Columns.Clear()
            .AutoGenerateColumns = False
            ' altera as propriedades importantes
            .MultiSelect = False
            .ColumnHeadersVisible = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = False
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            .StandardTab = True
            .RowHeadersVisible = False
        End With
        '
        ' (1) COLUNA ID
        dgvOperacao.Columns.Add("clnID", "ID")
        With dgvOperacao.Columns("clnID")
            .DataPropertyName = "IDOperacao"
            .Width = 70
            .Resizable = DataGridViewTriState.False
            .Visible = False
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Format = "00"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (2) COLUNA OPERACAO
        dgvOperacao.Columns.Add("clnOperacao", "Operação")
        With dgvOperacao.Columns("clnOperacao")
            .DataPropertyName = "Operacao"
            .Width = 220
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Format = "00"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.Font = New Font("Arial Narrow", 12)
            .DefaultCellStyle.BackColor = Color.Ivory
        End With
        '
        ' (3) COLUNA CFOP DENTRO
        dgvOperacao.Columns.Add("clnCFOPDentro", "Dentro")
        With dgvOperacao.Columns("clnCFOPDentro")
            .DataPropertyName = "CFOPDentro"
            .Width = 60
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Format = "0,000"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        '
        ' (3) COLUNA CFOP FORA
        dgvOperacao.Columns.Add("clnCFOPFora", "Fora")
        With dgvOperacao.Columns("clnCFOPFora")
            .DataPropertyName = "CFOPFora"
            .Width = 60
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Format = "0,000"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

        End With
        '
        ' (2) COLUNA DESCRIÇÃO
        dgvOperacao.Columns.Add("clnDescricao", "Descrição")
        With dgvOperacao.Columns("clnDescricao")
            .DataPropertyName = "Descricao"
            .Width = 300
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .DefaultCellStyle.Font = New Font("Arial Narrow", 12)
        End With
        '
    End Sub
    '
#End Region
    '
#Region "FILIAL E CONTA"
    '
    Private Sub btnFilialAdd_Click(sender As Object, e As EventArgs) Handles btnFilialAdd.Click
        '
        '--- Abre o frmFilial
        Dim fFil As New frmFilial()
        '
        Me.Opacity = 0.6
        fFil.ShowDialog()
        Me.Opacity = 1
        '
    End Sub
    '
    Private Sub btnContaAdd_Click(sender As Object, e As EventArgs) Handles btnContaAdd.Click
        '
        '--- verifica se a Filial já foi escolhida
        If IsNothing(_Conta.IDFilial) Then
            MessageBox.Show("Favor escolher em primeiro lugar FILIAL PADRÃO", "Escolha Filial", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnAlteraFilial.Focus()
            Exit Sub
        End If
        '
        '--- Abre o frmContas
        Dim frmConta As New frmContas(Me, _Conta.IDFilial, _Conta.ApelidoFilial)
        '
        Opacity = 0.6
        frmConta.ShowDialog()
        Opacity = 1
        '
    End Sub
    '
    Private Sub btnAlteraFilial_Click(sender As Object, e As EventArgs) Handles btnAlteraFilial.Click
        '
        '--- Abre o frmFilial
        Dim fFil As New frmFilialEscolher()
        '
        Me.Opacity = 0.6
        fFil.ShowDialog()
        Me.Opacity = 1
        '
        If fFil.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        If fFil.propIdFilial_Escolha <> _Conta.IDFilial Then
            Sit = FlagEstado.Alterado
            _Conta = New clConta '--- clear _Conta as NEW clConta
            txtContaPadrao.Clear()
        End If
        '
        _Conta.IDFilial = fFil.propIdFilial_Escolha
        _Conta.ApelidoFilial = fFil.propFilial_Escolha
        txtFilialPadrao.Text = fFil.propFilial_Escolha
        '
    End Sub
    '
    Private Sub btnAlteraConta_Click(sender As Object, e As EventArgs) Handles btnAlteraConta.Click
        '
        '--- verifica se a Filial já foi escolhida
        If IsNothing(_Conta.IDFilial) Then
            MessageBox.Show("Favor escolher em primeiro lugar FILIAL PADRÃO", "Escolha Filial", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnAlteraFilial.Focus()
            Exit Sub
        End If
        '
        '--- Abre o frmContas
        Dim frmConta As New frmContaProcurar(Me, _Conta.IDFilial, _Conta.IDConta)
        '
        Me.Opacity = 0.6
        frmConta.ShowDialog()
        Me.Opacity = 1
        '
        If frmConta.DialogResult = DialogResult.Cancel Then Exit Sub
        '
        If frmConta.propConta_Escolha.IDConta <> _Conta.IDConta Then
            Sit = FlagEstado.Alterado
        End If
        '
        _Conta = frmConta.propConta_Escolha
        txtContaPadrao.Text = frmConta.propConta_Escolha.Conta
        '
        '--- determina a DataPadrao do Sistema pela conta
        VerficaBloqueioDataConta()
        '
    End Sub
    '
    '--- EXECUTAR A FUNCAO DO BOTAO QUANDO PRESSIONA A TECLA (+) NO CONTROLE
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilialPadrao.KeyDown, txtContaPadrao.KeyDown
        '
        Dim ctr As Control = DirectCast(sender, Control)
        '
        If e.KeyCode = Keys.Add Then
            e.Handled = True
            Select Case ctr.Name
                Case "txtFilialPadrao"
                    btnAlteraFilial_Click(New Object, New EventArgs)
                    txtFilialPadrao.Text = txtFilialPadrao.Text.Replace("+", "")
                    txtFilialPadrao.SelectAll()
                Case "txtContaPadrao"
                    btnAlteraConta_Click(New Object, New EventArgs)
                    txtContaPadrao.Text = txtContaPadrao.Text.Replace("+", "")
                    txtContaPadrao.SelectAll()
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
    '--- VERIFICA BLOQUEIO DA DATA INICIAL DA CONTA ESCOLHIDA
    'return FALSE se a DataBloqueio não for encontrada
    Private Function VerficaBloqueioDataConta() As Boolean
        '
        Dim blData As Date? = _Conta.BloqueioData
        '
        '--- DEFINE OS VALORES
        '---------------------------------------------------
        If Not IsNothing(blData) Then
            'blData = CDate(blData).AddDays(1)
            '
            '-- verifica se a data adicionada é DOMINGO, sendo adiciona um dia
            If CDate(blData).DayOfWeek = DayOfWeek.Sunday Then CDate(blData).AddDays(1)
            '
            '-- define a propriedade DATA PADRAO
            dtpDataPadrao.MinDate = blData
            dtpDataPadrao.MaxDate = Today
            dtpDataPadrao.Value = blData
            '
            '-- define a etiqueta
            lblDataBloqueio.Text = blData
            '
            Return True
            '
        Else '-- Se não houver DataBloqueio definida escolhe o dia de HOJE
            MessageBox.Show("A CONTA PADRÃO escolhida: " & txtContaPadrao.Text.ToUpper & vbNewLine &
                            "ainda não tem data de bloqueio definida..." & vbNewLine &
                            "A DATA PADRÃO do sistema será escolhida como" & vbNewLine &
                            "DATA ATUAL: " & Now.ToLongDateString, "Data Padrão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '
            dtpDataPadrao.MinDate = Today.AddYears(-10)
            dtpDataPadrao.Value = Today.ToShortDateString
            dtpDataPadrao.MaxDate = Today
            '
            '-- define a etiqueta
            lblDataBloqueio.Text = dtpDataPadrao.MinDate
            '
            Return False
            '
        End If
        '
    End Function
    '
#End Region
    '
#Region "NAVEGABILIDADE E VALIDACOES"
    ' CRIA TECLA DE ATALHO PARA O TAB
    '---------------------------------------------------------------------------------------------------
    Private Sub frmClientesPF_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        '
        If e.Alt AndAlso e.KeyCode = Keys.D1 Then
            TabPrincipal.SelectedTab = Tab1
            tabPrincipal_SelectedIndexChanged(New Object, New System.EventArgs)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D2 Then
            TabPrincipal.SelectedTab = Tab2
            tabPrincipal_SelectedIndexChanged(New Object, New System.EventArgs)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D3 Then
            TabPrincipal.SelectedTab = Tab3
            tabPrincipal_SelectedIndexChanged(New Object, New System.EventArgs)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D4 Then
            TabPrincipal.SelectedTab = Tab4
            tabPrincipal_SelectedIndexChanged(New Object, New System.EventArgs)
        End If
        '
    End Sub
    '
    Private Sub tabPrincipal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabPrincipal.SelectedIndexChanged
        If TabPrincipal.SelectedIndex = 0 Then
            dtpDataPadrao.Focus()
        ElseIf TabPrincipal.SelectedIndex = 1 Then
            txtRazao.Focus()
        ElseIf TabPrincipal.SelectedIndex = 2 Then
            txtLogoColorCaminho.Focus()
            '--- somente faz a leitura dos logos nessa TAB
            LerLogosImagem()
        ElseIf TabPrincipal.SelectedIndex = 3 Then
            txtStringConexao.Focus()
        ElseIf TabPrincipal.SelectedIndex = 4 Then
            dgvOperacao.Focus()
        End If
    End Sub
    '
    ' CAMPO UF SOMENTE MAIUSCULAS COM 2 CARACTERES
    Private Sub txtUF_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUF.KeyPress
        ' MAXIMO DE 2 CARACTERES
        If Len(txtUF.Text) >= 2 And Not e.KeyChar = vbBack Then
            e.Handled = True
            Exit Sub
        End If

        ' SOMENTE LETRAS MAIUSCULA
        If Not Char.IsLetter(e.KeyChar) And Not e.KeyChar = vbBack Then
            e.Handled = True
        ElseIf Char.IsLower(e.KeyChar) Then
            'Converte o texto para caixa alta
            txtUF.SelectedText = Char.ToUpper(e.KeyChar)
            e.Handled = True
        End If
    End Sub
    '
    ' EVENTO CHANGE DOS CONTROLES
    Private Sub HandlerChangedControles()
        '--- para cada TabPage no tabPrincipal
        For Each t As vTabPage In TabPrincipal.TabPages
            '--- para cada Controle no TabPage
            For Each c In t.Controls.OfType(Of TextBox)
                '--- se o controle for textbox então
                AddHandler c.TextChanged, AddressOf Controles_TextChanged
                c.ShortcutsEnabled = False
            Next
        Next
    End Sub
    '
    Private Sub Controles_TextChanged(sender As Object, e As EventArgs)
        '
        If Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
    Private Sub dtpDataPadrao_ValueChanged(sender As Object, e As EventArgs) Handles dtpDataPadrao.ValueChanged
        '
        If _VerAlteracao AndAlso Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
    Private Sub chkVerBackup_CheckedChanged(sender As Object, e As EventArgs) Handles _
        chkVerBackup.CheckedChanged,
        rbtServLocal.CheckedChanged,
        rbtServRemoto.CheckedChanged
        '
        If Sit = FlagEstado.RegistroSalvo Then
            Sit = FlagEstado.Alterado
        End If
        '
    End Sub
    '
    '--- FAZER O txtTaxa formatar como 0,00
    Private Sub txtPermanencia_Validated(sender As Object, e As EventArgs) Handles txtPermanencia.Validated
        If txtPermanencia.TextLength > 0 Then
            txtPermanencia.Text = FormatNumber(txtPermanencia.Text, 2, TriState.True, TriState.UseDefault, TriState.UseDefault)
        Else
            txtPermanencia.Text = "0,00"
        End If
    End Sub
    '
#End Region
    '
#Region "SALVAR DADOS"
    ' SALVAR OS DADOS NO CONFIG
    '-----------------------------------------------------------------------------------------------
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        '
        ' Verifica os Dados
        If VerificaDados() = False Then
            Exit Sub
        End If
        '
        ' se existe o config.xml pergunta se deseja sobreescrever o arquivo
        If File.Exists(Application.StartupPath & "\ConfigFiles\Config.xml") Then
            If MessageBox.Show("Já existe um arquivo de Configuração salvo..." & vbNewLine &
                               "Deseja sobrescrever o arquivo de Configuração?",
                                "Salvar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
        End If
        '
        '--- Salva os registro de CFOP das operações
        Salvar_CFOP()
        '
        '--- Copia as imagens da LOGO para o diretorio padrao
        Copia_LogoColor()
        Copia_LogoMono()
        '
        ' cria o arquivo de configuração
        If CriaConfigXML() Then
            '
            Dim frmP As frmPrincipal = Application.OpenForms().Item("frmPrincipal")
            frmP.propContaPadrao = _Conta
            frmP.propDataPadrao = dtpDataPadrao.Value
            '
            MessageBox.Show("Arquivo de Configuração SALVO com sucesso!",
                            "Configuração do Sistema", MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            '
            Sit = FlagEstado.RegistroSalvo
        Else
            MessageBox.Show("Erro ao gravar o arquivo de Configuração",
                "Configuração do Sistema", MessageBoxButtons.OK,
                MessageBoxIcon.Stop)
        End If
        '
    End Sub
    '
    Private Function VerificaDados() As Boolean
        Dim f As New FuncoesUtilitarias
        '
        ' Verifica a validade de todos os campos
        ' Controles na TAB1
        TabPrincipal.SelectedTab = Tab1
        If Not f.VerificaControlesForm(dtpDataPadrao, "Data Padrão", EProvider) Then
            Return False
        End If
        '
        If IsNothing(_Conta) OrElse IsNothing(_Conta.IDConta) Then
            MessageBox.Show("A Conta Padrão não pode ficar vazia...", "Conta Padrão",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtContaPadrao.Focus()
            Return False
        End If
        '
        If IsNothing(_Conta.IDFilial) Then
            MessageBox.Show("A Filial Padrão não pode ficar vazio...", "Filial Padrão",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtFilialPadrao.Focus()
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtCidade, "Cidade Padrão", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtUF, "Estado/UF Padrão", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtNaturalidade, "Naturalidade Padrão", EProvider) Then
            Return False
        End If
        '
        ' Controles na TAB2
        TabPrincipal.SelectedTab = Tab2
        If Not f.VerificaControlesForm(txtRazao, "Razão Social", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtFantasia, "Nome Fantasia", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtCNPJ, "CNPJ", EProvider) Then
            Return False
        End If
        '
        If Not f.VerificaControlesForm(txtIncricao, "Inscrição", EProvider) Then
            Return False
        End If
        '
        ' Se não encontra nenhum problema limpa o Eprovider e retorna true
        EProvider.Clear()
        TabPrincipal.SelectedTab = Tab1
        Return True
        '
    End Function
    '
    Private Function Salvar_CFOP() As Boolean
        If dgvOperacao.Rows.Count = 0 Then Return True
        '
        Dim dt As DataTable = dgvOperacao.DataSource
        Dim tBLL As New TransacaoBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            For Each r As DataRow In dt.Rows
                tBLL.SalvaOperacao_CFOP(r("IDOperacao"), r("CFOPDentro"), r("CFOPFora"))
            Next
            '
        Catch ex As Exception
            '
            MessageBox.Show("Uma exceção ocorreu ao salvar registros de Operações e CFOP no BD..." & vbNewLine &
            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '
            Return False
            '
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
            '
        End Try
        '
        Return True
        '
    End Function
    '
#End Region
    '
#Region "LOGO MANUTENCAO"
    '
    Private Function Copia_LogoColor() As Boolean
        '
        Try
            '
            '--- se o arquivo foi selecionado
            If txtLogoColorCaminho.Text.Length > 0 Then
                '
                '--- copia LOGO COLOR
                If txtLogoColorCaminho.Text <> Application.StartupPath + "\Imagens\LogoColor.png" Then
                    File.Copy(txtLogoColorCaminho.Text, Application.StartupPath + "\Imagens\LogoColor.png", True)
                    txtLogoColorCaminho.Text = Application.StartupPath + "\Imagens\LogoColor.png"
                End If
                '
                '--- ler a imagem se estiver na TAB 2
                If TabPrincipal.SelectedIndex = 2 Then
                    ImageLogoColor = Image.FromFile(txtLogoColorCaminho.Text)
                    picLogoColor.Image = ImageLogoColor
                End If
                '
            Else
                Return False
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Um erro aconteceu ao copiar a LogoColor para o diretório padrão..." + vbNewLine +
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
        Return True
        '
    End Function
    '
    Private Function Copia_LogoMono() As Boolean
        '
        Try
            '
            '--- se o arquivo foi selecionado
            If txtLogoMonoCaminho.Text.Length > 0 Then
                '
                '--- copia LOGO MONO
                If txtLogoMonoCaminho.Text <> Application.StartupPath + "\Imagens\LogoMono.png" Then
                    File.Copy(txtLogoMonoCaminho.Text, Application.StartupPath + "\Imagens\LogoMono.png", True)
                    txtLogoMonoCaminho.Text = Application.StartupPath + "\Imagens\LogoMono.png"
                End If
                '
                '--- ler a imagem se estiver na TAB 2
                If TabPrincipal.SelectedIndex = 2 Then
                    ImageLogoMono = Image.FromFile(txtLogoMonoCaminho.Text)
                    picLogoMono.Image = ImageLogoMono
                End If
                '
            Else
                Return False
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Um erro aconteceu ao copiar a LogoMono para o diretório padrão..." + vbNewLine +
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        '
        Return True
        '
    End Function
    '
    ' PROCURAR A IMAGEM DA LOGO
    Private Sub btnProcurarImagem_Click(sender As Object, e As EventArgs) Handles btnProcurarImagem.Click ' logo MONO
        '
        Using OFD As New OpenFileDialog With {.Filter = "PNG (*.png)|*.png"}
            If OFD.ShowDialog = DialogResult.OK Then
                txtLogoMonoCaminho.Text = OFD.FileName
                ImageLogoMono = Image.FromFile(OFD.FileName)
                picLogoMono.Image = ImageLogoMono
                Sit = FlagEstado.Alterado
            End If
        End Using
        '
    End Sub
    '
    Private Sub btnProcLogoColor_Click(sender As Object, e As EventArgs) Handles btnProcLogoColor.Click ' LOGO COLOR
        '
        Using OFD As New OpenFileDialog With {.Filter = "PNG (*.png)|*.png"}
            If OFD.ShowDialog = DialogResult.OK Then
                txtLogoColorCaminho.Text = OFD.FileName
                ImageLogoColor = Image.FromFile(OFD.FileName)
                picLogoColor.Image = ImageLogoColor
                Sit = FlagEstado.Alterado
            End If
        End Using
        '
    End Sub
    '
#End Region
    '
#Region "MENU SUSPENSO CFOP"
    '
    '--- REVELA O MENU CFOP
    Private Sub dgvOperacao_MouseDown(sender As Control, e As MouseEventArgs) Handles dgvOperacao.MouseDown
        '
        If e.Button = MouseButtons.Right Then
            Dim c As Control = DirectCast(sender, Control)
            Dim hit As DataGridView.HitTestInfo = dgvOperacao.HitTest(e.X, e.Y)
            dgvOperacao.ClearSelection()

            If Not hit.Type = DataGridViewHitTestType.Cell Then Exit Sub
            '
            If hit.ColumnIndex = 2 Or hit.ColumnIndex = 3 Then
                ' seleciona a CELL
                If hit.ColumnIndex = 2 Then
                    dgvOperacao.CurrentCell = dgvOperacao.Rows(hit.RowIndex).Cells(2)
                Else
                    dgvOperacao.CurrentCell = dgvOperacao.Rows(hit.RowIndex).Cells(3)
                End If
                '
                ' mostra o MENU
                menuCFOP.Show(c.PointToScreen(e.Location))
                '
            Else
                dgvOperacao.Rows(hit.RowIndex).Selected = True
            End If
        End If
    End Sub
    '
    '--- BTN PROCURAR MENUSTRIP
    Private Sub ProcurarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProcurarToolStripMenuItem.Click
        Dim f As New frmCodigosFiscais
        '
        Me.Opacity = 0.6
        f.ShowDialog()
        Me.Opacity = 1

        If f.DialogResult = DialogResult.OK Then
            '
            For Each r As DataGridViewRow In dgvOperacao.Rows
                If Not IsDBNull(r.Cells("clnCFOPDentro").Value) AndAlso r.Cells("clnCFOPDentro").Value = f.Escolhido Then
                    MessageBox.Show("Esse CFOP já está associado a uma operação",
                                    "CFOP Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                '
                If Not IsDBNull(r.Cells("clnCFOPFora").Value) AndAlso r.Cells("clnCFOPFora").Value = f.Escolhido Then
                    MessageBox.Show("Esse CFOP já está associado a uma operação",
                                    "CFOP Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Next
            '
            '--- preenche com o valor escolhido
            dgvOperacao.CurrentCell.Value = f.Escolhido
            '
            Sit = FlagEstado.Alterado
            '
        End If
        '
    End Sub
    '
    '--- BTN LIMPAR MENUSTRIP
    Private Sub LimparToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimparToolStripMenuItem.Click
        dgvOperacao.CurrentCell.Value = DBNull.Value
        '
        Sit = FlagEstado.Alterado
        '
    End Sub
    '
#End Region
    '
#Region "BTN CANCELAR SAIR"
    '--------------------------------------------------------------------------------------------------------
    ' CANCELAR e SAIR
    '--------------------------------------------------------------------------------------------------------
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        '
        If _Sit = FlagEstado.Alterado Then
            '
            LerConfigXML()
            '
            '-- Get Conta Padrao
            If Not IsNothing(IDConta_Config) Then
                GetConta(IDConta_Config)
            Else
                _Conta = New clConta
            End If
            '
            Sit = FlagEstado.RegistroSalvo
            '
        ElseIf _Sit = FlagEstado.NovoRegistro Then
            DialogResult = DialogResult.Cancel
            Close()
        Else
            DialogResult = DialogResult.Cancel
            Me.Close()
        End If
        '
    End Sub
    '
#End Region
    '
#Region "MOVER FORM SEM BORDA"
    ' MOVER O FORMULÁRIO SEM BORDA
    '-----------------------------------------------------------------------------------------------
    Private Sub Painel_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        Px = e.X
        Py = e.Y
        Mover = True
    End Sub
    Private Sub Painel_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        Mover = False
    End Sub
    '
    Private Sub Painel_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If Mover = True Then
            Me.Location = Me.PointToScreen(New Point(MousePosition.X - Me.Location.X - Px,
                                                     MousePosition.Y - Me.Location.Y - Py))
        End If
    End Sub
    '
#End Region
    '
End Class