Imports CamadaBLL
Imports CamadaDTO
Imports System.Drawing.Drawing2D
'
Public Class frmOperacaoEntradaProcurar
    Private cmpBLL As New CompraBLL
    Private cmpLista As DataTable
    Private ImgAtivo As Image = My.Resources.accept
    Private ImgInativo As Image = My.Resources.block
    Private ImgLock As Image = My.Resources.lock
    Private _myMes As Date
    '
    Private Property myMes() As DateTime
        Get
            Return _myMes
        End Get
        Set(ByVal value As DateTime)
            If CDate(value.ToShortDateString) > CDate(Now.ToShortDateString) Then
                value = Now.ToShortDateString
                btnPeriodoPosterior.Enabled = False
            Else
                btnPeriodoPosterior.Enabled = True
            End If
            _myMes = value
            lblPeriodo.Text = Format(_myMes, "MMMM | yyyy")
        End Set
    End Property
    '
#Region "EVENTO LOAD"
    '
    Private Sub frmOperacaoEntradaProcurar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        PreencheComboOperacao()
        myMes = ObterDefault("DataPadrao")
        lblPeriodo.Text = Format(myMes, "MMMM | yyyy")
        PreencheListagem()
        '
        AddHandler cmbOperacao.SelectedIndexChanged, AddressOf Handler_PreencheListagem
        AddHandler btnProcurar.Click, AddressOf Handler_PreencheListagem
        AddHandler dtMes.DateChanged, AddressOf dtMes_DateChanged
        '
    End Sub
    '
    Private Sub PreencheComboOperacao()
        Dim db As New TransacaoBLL
        Dim dtOp As New DataTable
        '
        Try
            dtOp = db.Get_Operacao_DT(TransacaoBLL.MovimentoEstoque.ENTRADA)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '
        With cmbOperacao
            .DataSource = dtOp
            .ValueMember = "IdOperacao"
            .DisplayMember = "Operacao"
        End With
    End Sub
    '
#End Region
    '
#Region "LISTAGEM CONFIGURAÇÃO"
    Private Sub PreencheListagem()
        '--- limpa as colunas do datagrid
        dgvListagem.Columns.Clear()
        dgvListagem.AutoGenerateColumns = False
        '
        ' altera as propriedades importantes
        dgvListagem.MultiSelect = False
        dgvListagem.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvListagem.ColumnHeadersVisible = False
        dgvListagem.AllowUserToResizeRows = False
        dgvListagem.AllowUserToResizeColumns = False
        dgvListagem.RowHeadersVisible = True
        dgvListagem.RowHeadersWidth = 30
        dgvListagem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvListagem.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvListagem.StandardTab = True
        '
        If cmbOperacao.SelectedValue = 2 Then
            PreencheListagem_Compra()
        Else
            MsgBox("Ainda não implementado")
        End If
    End Sub
    '
    Private Sub PreencheListagem_Compra()
        '--- consulta o banco de dados
        Try
            '--- verifica o filtro das datas
            If chkPeriodoTodos.Checked = True Then
                cmpLista = cmpBLL.GetCompraLista_Procura(cmbOperacao.SelectedValue, txtProcura.Text)
            Else
                Dim f As New FuncoesUtilitarias
                Dim dtInicial As Date = f.FirstDayOfMonth(myMes)
                Dim dtFinal As Date = f.LastDayOfMonth(myMes)
                '
                cmpLista = cmpBLL.GetCompraLista_Procura(cmbOperacao.SelectedValue, txtProcura.Text, dtInicial, dtFinal)
            End If
            '
            dgvListagem.DataSource = cmpLista
            '
        Catch ex As Exception
            MessageBox.Show("Em erro ao obter a lista de Operações de Entrada..." & vbNewLine &
            ex.Message, "Falha no Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        '
        '
        ' (1) COLUNA REG
        dgvListagem.Columns.Add("IDCompra", "Reg.")
        With dgvListagem.Columns("IDCompra")
            .DataPropertyName = "IDCompra"
            .Width = 50
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Format = "0000"
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
        '
        ' (2) COLUNA DATACOMPRA
        dgvListagem.Columns.Add("CompraData", "Data")
        With dgvListagem.Columns("CompraData")
            .DataPropertyName = "TransacaoData"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.DefaultCellStyle.Font = New Font("Arial Narrow", 12)
        End With
        '
        ' (3) COLUNA NOME
        dgvListagem.Columns.Add("CadastroNome", "Fornecedor")
        With dgvListagem.Columns("CadastroNome")
            .DataPropertyName = "Cadastro"
            .Width = 400
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
        ' (4) COLUNA VALOR
        dgvListagem.Columns.Add("TotalCompra", "Valor")
        With dgvListagem.Columns("TotalCompra")
            .DataPropertyName = "TotalCompra"
            .Width = 100
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "C"
        End With
        '
        ' (5) COLUNA COBRANCA TIPO
        dgvListagem.Columns.Add("CobrancaTipoTexto", "Cobrança")
        With dgvListagem.Columns("CobrancaTipoTexto")
            .DataPropertyName = "CobrancaTipoTexto"
            .Width = 120
            .Resizable = DataGridViewTriState.False
            .Visible = True
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End With
        '
        ' (6) COLUNA IMAGEM SITUACAO
        Dim colImage As New DataGridViewImageColumn
        With colImage
            .HeaderText = "Situação"
            .Resizable = False
            .Width = 50
        End With
        dgvListagem.Columns.Add(colImage)
        '
        ' (7) COLUNA SITUAÇÃO
        dgvListagem.Columns.Add("Situacao", "Situação")
        With dgvListagem.Columns("Situacao")
            .DataPropertyName = "IDSituacao"
            .Width = 80
            .Resizable = DataGridViewTriState.False
            .Visible = False
            .ReadOnly = True
            .SortMode = DataGridViewColumnSortMode.NotSortable
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        '
    End Sub
    '
    Private Sub Handler_PreencheListagem(sender As Object, e As EventArgs)
        PreencheListagem()
    End Sub
    '
    Private Sub dgvListagem_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvListagem.CellFormatting
        '
        '--- altera a imagem da coluna 5
        If e.ColumnIndex = 5 Then '--- coluna Imagem Situação
            Dim sit As Integer = dgvListagem.Rows(e.RowIndex).DataBoundItem("IDSituacao")
            '
            Select Case sit
                Case 1
                    dgvListagem.Rows(e.RowIndex).Cells(5).Value = ImgInativo
                Case 2
                    dgvListagem.Rows(e.RowIndex).Cells(5).Value = ImgAtivo
                Case 3
                    dgvListagem.Rows(e.RowIndex).Cells(5).Value = ImgLock
            End Select
        End If
        '
    End Sub
    '
#End Region
    '
#Region "ACAO DOS BOTOES"
    '
    Private Sub btnEscolher_Click(sender As Object, e As EventArgs) Handles btnEscolher.Click
        If dgvListagem.Rows.Count = 0 OrElse dgvListagem.SelectedRows.Count = 0 Then
            MessageBox.Show("Selecione uma OPERAÇÃO DE ENTRADA antes de pressionar ESCOLHER...",
                            "Escolher Registro", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvListagem.Focus()
            Exit Sub
        End If
        '
        '
        Select Case cmbOperacao.SelectedValue
            Case 2 ' COMPRAS
                '
                Dim cmpBLL As New CompraBLL
                Dim _cmp As New clCompra
                '
                '--- ampulheta ON
                Cursor = Cursors.WaitCursor
                '
                _cmp = cmpBLL.GetCompra_PorID_OBJ(dgvListagem.SelectedRows(0).Cells(0).Value)
                '
                Dim frm As New frmCompra(_cmp)
                frm.MdiParent = frmPrincipal
                frm.StartPosition = FormStartPosition.CenterScreen
                Close()
                frm.Show()
                '
                '--- ampulheta OFF
                Cursor = Cursors.Default
                '
            Case 3 ' SIMPLES ENTRADA
                MsgBox("Operação de Entrada: SIMPLES ENTRADA, ainda não foi implementada")
            Case 5 ' DEVOLUÇÃO DE VENDA
                MsgBox("Operação de Entrada: DEVOLUÇÃO DE VENDA, ainda não foi implementada")
            Case 7 ' CONSIGNAÇÃO DE ENTRADA
                MsgBox("Operação de Entrada: CONSIGNAÇÃO DE ENTRADA, ainda não foi implementada")

        End Select
    End Sub
    '
    Private Sub dgvListagem_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvListagem.CellDoubleClick
        btnEscolher_Click(New Object, New EventArgs)
    End Sub
    '
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click, btnClose.Click
        Close()
        MostraMenuPrincipal()
    End Sub
    '
    '--- SELECIONAR ITEM QUANDO PRESSIONA A TECLA (ENTER)
    Private Sub dgvListagem_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvListagem.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            '
            btnEscolher_Click(New Object, New EventArgs)
        End If
    End Sub
    '
    '--- AO PRESSIONAR A TECLA (ENTER) ENVIAR (TAB)
    Private Sub txt_KeyDown(sender As Object, e As KeyEventArgs) Handles txtProcura.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            PreencheListagem()
            SendKeys.Send("{Tab}")
        End If
    End Sub
    '
#End Region
    '
#Region "CONTROLE DO PERÍODO"
    Private Sub btnPeriodoAnterior_Click(sender As Object, e As EventArgs) Handles btnPeriodoAnterior.Click
        myMes = myMes.AddMonths(-1)
        dtMes.SelectionStart = myMes
    End Sub
    '
    Private Sub btnPeriodoPosterior_Click(sender As Object, e As EventArgs) Handles btnPeriodoPosterior.Click
        myMes = myMes.AddMonths(1)
        dtMes.SelectionStart = myMes
        '
        If Year(myMes) = Year(Now) AndAlso Month(myMes) = Month(Now) Then
            btnPeriodoPosterior.Enabled = False
        Else
            btnPeriodoPosterior.Enabled = True
        End If
        '
    End Sub
    '
    Private Sub btnMesAtual_Click(sender As Object, e As EventArgs) Handles btnMesAtual.Click
        myMes = Now
        dtMes.SelectionStart = myMes
        btnPeriodoPosterior.Enabled = False
    End Sub
    '
    Private Sub dtMes_DateChanged(sender As Object, e As DateRangeEventArgs)
        '
        If CDate(e.Start.ToShortDateString) <= CDate(Today.ToShortDateString) Then
            myMes = e.Start
            lblPeriodo.Text = Format(myMes, "MMMM | yyyy")
            PreencheListagem()
        Else
            MessageBox.Show("Escolha um mês anterior ou igual ao mês atual...",
                "Período", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dtMes.SelectionStart = Now
            myMes = Now
        End If
        '
    End Sub
    '
    Private Sub chkPeriodoTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkPeriodoTodos.CheckedChanged
        If chkPeriodoTodos.Checked = False Then
            btnPeriodoAnterior.Enabled = True
            btnPeriodoPosterior.Enabled = True
            btnMesAtual.Enabled = True
            lblPeriodo.Visible = True
        Else
            btnPeriodoAnterior.Enabled = False
            btnPeriodoPosterior.Enabled = False
            btnMesAtual.Enabled = False
            lblPeriodo.Visible = False
        End If
        '
        PreencheListagem()
    End Sub
    '
    Private Sub lblPerido_Click(sender As Object, e As EventArgs) Handles lblPeriodo.Click
        pnlMes.Visible = True
        dtMes.Focus()
    End Sub
    '
    Private Sub pnlMes_Leave(sender As Object, e As EventArgs) Handles pnlMes.MouseLeave, dtMes.LostFocus
        pnlMes.Visible = False
    End Sub
    '
#End Region
    '
#Region "TRATAMENTO VISUAL"
    Private Sub pnlVenda_Paint(sender As Object, e As PaintEventArgs) Handles pnlVenda.Paint
        '
        Dim brush As Brush = New LinearGradientBrush(e.ClipRectangle, Color.LightSteelBlue, Color.FromArgb(100, Color.SlateGray), LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(brush, e.ClipRectangle)
        '
    End Sub
#End Region
    '
End Class
