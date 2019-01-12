Imports CamadaBLL
Imports CamadaDTO

Public Class frmSimplesQuitar
    '
    Private _formOrigem As Form
    Private _IDFilialDestino As Integer?
    Private _IDFilialOrigem As Integer
    Private _FilialOrigem As String
    Private _EntradaSaida As Boolean
    Private simplesBLL As New SimplesMovimentacaoBLL
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
#Region "EFETUAR QUITACAO"
    '
    '------------------------------------------------------------------------------------------
    ' QUITAR RECEBER PAGAR SIMPLES
    '------------------------------------------------------------------------------------------
    Private Sub btnQuitar_Click(sender As Object, e As EventArgs) Handles btnQuitar.Click
        '
        '--- Verifica se os Campos estão preenchidos e corretos
        '--------------------------------------------------------------------------------------------------------
        If Not VerificarCamposControles() Then Return
        '
        '--- Verifica se a DATA DA ENTRADA e SAIDA é permitida pelo sistema
        '--------------------------------------------------------------------------------------------------------
        If Not VerificarDatasBloqueio() Then Return
        '
        '--- EXECUTA se for Entrada (A Receber) ou Saida (A Pagar)
        '--------------------------------------------------------------------------------------------------------
        If _EntradaSaida Then '-- A RECEBER ================================================
            '
            If ExecutaQuitacao_Entrada() Then
                MessageBox.Show("Crédito realizado com sucesso!", "Receber Simples",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                DialogResult = DialogResult.OK
            End If
            '
        Else '--- A PAGAR ===================================================================
            '
            If ExecutaQuitacao_Saida() Then
                MessageBox.Show("Pagamento realizado com sucesso!", "Pagamento Simples",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                DialogResult = DialogResult.OK
            End If
            '
        End If
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' EXECUTA QUITACAO DE ENTRADA
    '------------------------------------------------------------------------------------------
    Private Function ExecutaQuitacao_Entrada() As Boolean
        '
        '--- Verifica o valor do Total do AReceber da FilialOrigem
        '--------------------------------------------------------------------------------------------------------
        If Not VerificaTotalAReceber(_IDFilialOrigem, _IDFilialDestino) Then Return False
        '
        '--- CRIA uma BD Transaction
        '--------------------------------------------------------------------------------------------------------
        Dim tBLL As New TransactionControlBLL
        Dim dbTran As Object = tBLL.GetNewAcessoWithTransaction
        '
        '--- EFETUA o RECEBIMENTO DA FILIAL ORIGEM
        '--------------------------------------------------------------------------------------------------------
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            EfetuarRecebimentoList(_IDFilialOrigem, _IDFilialDestino, dbTran)
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Efetuar o Recebimento..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tBLL.RollbackAcessoWithTransaction(dbTran)
            Return False
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
        '
        '--- EFETUA o PAGAMENTO DA FILIAL DESTINO IF BD SERVER NOT LOCAL
        '--------------------------------------------------------------------------------------------------------
        '
        '--- verifica se o servidor é local
        Dim NodeServidorLocal As String = ObterConfigValorNode("ServidorLocal")
        If String.IsNullOrEmpty(NodeServidorLocal) OrElse NodeServidorLocal.ToUpper = "TRUE" Then Return True
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            EfetuarPagamentoList(_IDFilialDestino, _IDFilialOrigem, dbTran)
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Efetuar o Pagamento..." & vbNewLine &
                                ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tBLL.RollbackAcessoWithTransaction(dbTran)
            Return False
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
        '--- COMMIT AND RETURN
        '--------------------------------------------------------------------------------------------------------
        tBLL.CommitAcessoWithTransaction(dbTran)
        'tBLL.RollbackAcessoWithTransaction(dbTran)
        Return True
        '
    End Function
    '
    '------------------------------------------------------------------------------------------
    ' EXECUTA QUITACAO DE SAIDA
    '------------------------------------------------------------------------------------------
    Private Function ExecutaQuitacao_Saida() As Boolean
        MsgBox("Em Implementação")
        Return False
    End Function
    '
    '------------------------------------------------------------------------------------------
    ' VERIFICA O VALOR DO ARECEBER
    '------------------------------------------------------------------------------------------
    Private Function VerificaTotalAReceber(IDFilialCreditada As Integer,
                                           IDFilialDebitada As Integer) As Boolean
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            Dim totalAReceber As Double = simplesBLL.Simples_AReceberTotal_Filial(IDFilialCreditada, IDFilialDebitada)
            '
            If totalAReceber < txtValor.Text Then
                MessageBox.Show("O Valor que você deseja quitar é maior que o Valor Total do A Receber que há entre as Filiais: " &
                                vbNewLine &
                                lblFilial.Text.ToUpper & " e " & txtFilialDestino.Text.ToUpper _
                                & vbNewLine & "Valor Total A Receber: " & Format(totalAReceber, "C"),
                                "Valor Total",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtValor.Focus()
                Return False
            End If
            '
            Return True
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Verificar Valor de A Receber..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Function
    '
    '------------------------------------------------------------------------------------------
    ' VERIFICACAO DOS CAMPOS NECESSARIOS
    '------------------------------------------------------------------------------------------
    Private Function VerificarCamposControles() As Boolean
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
    '
    '------------------------------------------------------------------------------------------
    ' VERIFICAR DATAS DE RECEBIMENTO | PAGAMENTO
    '------------------------------------------------------------------------------------------
    Private Function VerificarDatasBloqueio() As Boolean
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            ' CONTA ORIGEM
            If DataBloqueada(txtEntradaData.Text,
                             cmbIDContaOrigem.SelectedValue,
                             cmbIDContaOrigem.Text,
                             lblFilial.Text) Then Return False
            '
            ' CONTA DESTINO
            If DataBloqueada(txtEntradaData.Text,
                             cmbIDContaDestino.SelectedValue,
                             cmbIDContaDestino.Text,
                             txtFilialDestino.Text) Then Return False
            '
            Return True
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Verificar Datas Bloqueadas..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Function
    '
    '------------------------------------------------------------------------------------------
    ' GET | RETURN LISTA DE ARECEBER
    '------------------------------------------------------------------------------------------
    Private Function GetListaAReceber(IDFilialCreditada As Integer,
                                      IDFilialDebitada As Integer) As List(Of clAReceberParcela)
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            Return simplesBLL.GetSimplesAReceberLista_Procura(IDFilialCreditada,
                                                              IDFilialDebitada,
                                                              Nothing, Nothing, 0)
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao Obter Lista de a Receber..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Function
    '
    '------------------------------------------------------------------------------------------
    ' GET | RETURN LISTA DE APAGAR FILIAL PADRAO
    '------------------------------------------------------------------------------------------
    Private Function GetListaAPagar(IDFilialCreditada As Integer,
                                    IDFilialDebitada As Integer) As List(Of clAPagar)
        '
        Dim pagBLL As New APagarBLL
        '
        '--- consulta o banco de dados
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            Return pagBLL.GetListAPagar_Simples_EmAberto(IDFilialCreditada, IDFilialDebitada)
            '
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu ao obter a lista de A Pagar..." & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Function
    '
    '------------------------------------------------------------------------------------------
    ' EFETUAR RECEBIMENTO | ENTRADA FILIAL PADRAO
    '------------------------------------------------------------------------------------------
    Private Sub EfetuarRecebimentoList(IDFilialCreditada As Integer,
                                       IDFilialDebitada As Integer,
                                       dbTran As Object)
        '
        '--- GET lista de AReceber
        '--------------------------------------------------------------------------------------------------------
        Dim lstAReceber As New List(Of clAReceberParcela)
        '
        Try
            lstAReceber = GetListaAReceber(IDFilialCreditada, IDFilialDebitada)
            If IsNothing(lstAReceber) Then Return
        Catch ex As Exception
            Throw ex
            Return
        End Try
        '
        '--- Quita os itens da lista até alcançar o valor
        '--------------------------------------------------------------------------------------------------------
        Dim ValorAReceber As Double = txtValor.Text
        '
        For Each rec In lstAReceber
            '
            Dim EmAbertoParcela As Double = rec.ParcelaValor - rec.ValorPagoParcela
            Dim vlRecParcela As Double
            '
            '--- verifica se o valor a receber é maior que o valor da parcela
            If ValorAReceber >= EmAbertoParcela Then
                vlRecParcela = EmAbertoParcela '--- se for maior quita totalmente a parcela
            Else
                vlRecParcela = ValorAReceber '--- se for menor quita parcialmente a parcela
            End If
            '
            '--- reajusta o novo valor a Receber que restou
            ValorAReceber = ValorAReceber - vlRecParcela
            '
            Try
                EfetuarRecebimentoItem(rec, vlRecParcela, dbTran)
            Catch ex As Exception
                Throw ex
                Exit For
            End Try
            '
        Next
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    '--- EFETUA O RECEBIMENTO DE CADA ITEM DA LISTAGEM
    '------------------------------------------------------------------------------------------
    Private Sub EfetuarRecebimentoItem(rec As clAReceberParcela,
                                       vlRecParcela As Double,
                                       dbTran As Object)
        '
        '--- MOVIMENTACAO DE ENTRADA
        '------------------------------------------------------------------------------------------
        Dim MovEntrada As New clMovimentacao(EnumMovimentacaoOrigem.AReceberParcela, EnumMovimento.Entrada) With {
            .Creditar = False,
            .IDConta = IIf(rec.IDFilial = _IDFilialOrigem, cmbIDContaOrigem.SelectedValue, cmbIDContaDestino.SelectedValue),
            .IDFilial = rec.IDFilial,
            .IDMovForma = 1,
            .IDMovTipo = 1,
            .IDOrigem = rec.IDAReceberParcela,
            .MovData = txtEntradaData.Text,
            .MovValor = vlRecParcela,
            .Movimento = 1, '--- entrada
            .Observacao = txtObservacao.Text,
            .Origem = 2 '--- ORIGEM: tblAReceberParcela
        }
        '
        '--- Insere a Movimentaçao de Entrada
        Dim mBLL As New MovimentacaoBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            mBLL.Movimentacao_Inserir(MovEntrada, dbTran)
            '
        Catch ex As Exception
            Throw ex
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
        '--- RECEBE E/OU QUITA A PARCELA
        '---------------------------------------------------------------------------------------
        Dim parcBLL As New ParcelaBLL
        '
        Try
            '--- Ampulheta ON
            Cursor = Cursors.WaitCursor
            '
            parcBLL.Quitar_Parcela(rec.IDAReceberParcela,
                                   vlRecParcela, 0,
                                   txtEntradaData.Text,
                                   dbTran)
            '
        Catch ex As Exception
            Throw ex
        Finally
            '--- Ampulheta OFF
            Cursor = Cursors.Default
        End Try
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    ' EFETUAR PAGAMENTO | SAIDA DA FILIAL PADRAO
    '------------------------------------------------------------------------------------------
    Private Sub EfetuarPagamentoList(IDFilialCreditada As Integer,
                                     IDFilialDebitada As Integer,
                                     dbTran As Object)
        '
        '--- GET lista de APagar
        '--------------------------------------------------------------------------------------------------------
        Dim lstAPagar As New List(Of clAPagar)
        '
        Try
            lstAPagar = GetListaAPagar(IDFilialDebitada, IDFilialCreditada)
            If IsNothing(lstAPagar) Then Return
        Catch ex As Exception
            Throw ex
            Return
        End Try
        '
        '--- Quita os itens da lista até alcançar o valor
        '--------------------------------------------------------------------------------------------------------
        Dim ValorAPagar As Double = txtValor.Text
        '
        For Each pag In lstAPagar
            '
            Dim EmAberto As Double = pag.APagarValor - pag.ValorPago
            Dim vlRecebido As Double
            '
            '--- verifica se o valor a pagar é maior que o valor do pag
            If ValorAPagar >= EmAberto Then
                vlRecebido = EmAberto '--- se for maior quita totalmente o apagar
            Else
                vlRecebido = ValorAPagar '--- se for menor quita parcialmente o apagar
            End If
            '
            '--- reajusta o novo valor a Pagar que restou
            ValorAPagar = ValorAPagar - vlRecebido
            '
            Try
                EfetuarPagamentoItem(pag, vlRecebido, dbTran)
            Catch ex As Exception
                Throw ex
                Exit For
            End Try
            '
        Next
        '
    End Sub
    '
    '------------------------------------------------------------------------------------------
    '--- EFETUA O PAGAMENTO DE CADA ITEM DA LISTAGEM
    '------------------------------------------------------------------------------------------
    Private Sub EfetuarPagamentoItem(pag As clAPagar,
                                     vlRecebido As Double,
                                     dbTran As Object)
        '
        '--- MOVIMENTACAO DE SAIDA
        '------------------------------------------------------------------------------------------
        Dim MovSaida As New clMovimentacao(EnumMovimentacaoOrigem.APagar, EnumMovimento.Saida) With {
            .Creditar = False,
            .IDConta = IIf(pag.IDFilial = _IDFilialOrigem, cmbIDContaOrigem.SelectedValue, cmbIDContaDestino.SelectedValue),
            .IDFilial = pag.IDFilial,
            .IDMovForma = 1,
            .IDMovTipo = 1,
            .IDOrigem = pag.IDAPagar,
            .MovData = txtEntradaData.Text,
            .MovValor = vlRecebido,
            .Movimento = 2, '--- saida
            .Observacao = txtObservacao.Text,
            .Origem = 10 '--- ORIGEM: tblAReceberParcela
        }
        '
        '--- Quita o APAGAR e cria a Mov SAIDA
        '------------------------------------------------------------------------------------------
        Dim pBLL As New APagarBLL
        Dim newID As Integer?
        '
        Try
            newID = pBLL.Quitar_APagar(MovSaida.IDOrigem,
                                       Math.Abs(MovSaida.MovValor),
                                       0,
                                       MovSaida.MovData,
                                       MovSaida.IDConta,
                                       MovSaida.Observacao,
                                       dbTran)
            '
            If Not IsNumeric(newID) Then
                Throw New Exception(newID.ToString)
            End If
            '
        Catch ex As Exception
            MessageBox.Show("Houve uma exceção ao QUITAR o APagar:" & vbNewLine &
                            ex.Message, "Exceção Inesperada",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        '
    End Sub
    '
#End Region '/ EFETUAR QUITACAO
    '
#Region "BUTONS FUNCTION"
    '
    '--- CANCELAR | FECHAR
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = DialogResult.Cancel
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
    '---------------------------------------------------------------------------------------
    '--- BLOQUEIA PRESS A TECLA (+)
    '---------------------------------------------------------------------------------------
    Private Sub me_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        '
        If e.KeyChar = "+" Then
            '--- cria uma lista de controles que serao impedidos de receber '+'
            Dim controlesBloqueados As String() = {
            "txtFilialDestino"
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
    '
    '---------------------------------------------------------------------------------------
    '--- EXECUTAR A FUNCAO DO BOTAO QUANDO PRESSIONA A TECLA (+) NO CONTROLE
    '--- ACIONA ATALHO TECLA (+) E (DEL) | IMPEDE INSERIR TEXTO NOS CONTROLES
    '---------------------------------------------------------------------------------------
    Private Sub Control_KeyDown(sender As Object, e As KeyEventArgs) _
    Handles txtFilialDestino.KeyDown
        '
        Dim ctr As Control = DirectCast(sender, Control)
        '
        If e.KeyCode = Keys.Add Then
            e.Handled = True
            '
            Select Case ctr.Name
                Case "txtFilialDestino"
                    btnProcurarFilial_Click(New Object, New EventArgs)
            End Select
            '
        ElseIf e.KeyCode = Keys.Delete Then
            e.Handled = True
            Select Case ctr.Name
                Case "txtFilialDestino"
                    txtFilialDestino.Clear()
                    _IDFilialDestino = Nothing
            End Select
            '
        Else
            '--- cria uma lista de controles que serão bloqueados de alteracao
            Dim controlesBloqueados As New List(Of String)
            controlesBloqueados.AddRange({"txtFilialDestino"})
            '
            If controlesBloqueados.Contains(ctr.Name) Then
                e.Handled = True
                e.SuppressKeyPress = True
            End If
            '
        End If
        '
    End Sub
    '
#End Region '/ OUTRAS FUNCOES
    '
#Region "ATALHOS - NAVEGAÇÃO"
    '
    '------------------------------------------------------------------------------------------
    ' USAR TAB AO KEYPRESS ENTER
    '------------------------------------------------------------------------------------------
    Private Sub txtValor_KeyDown(sender As Object, e As KeyEventArgs) Handles _
            rbtTransferencia.KeyDown,
            txtFilialDestino.KeyDown,
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
