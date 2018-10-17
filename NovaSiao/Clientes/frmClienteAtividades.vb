Imports CamadaBLL
Imports ComponentOwl.BetterListView
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class frmClienteAtividades
    'Private SQL As New SQLControl
    Private dtAtiv As New DataTable
    Private Sit As FlagEstado
    '
    'EVENTO LOAD
    Private Sub frmClienteAtividades_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = frmPrincipal
        '
        ' PREENCHE A LISTA E OS COMBOS
        If PreencheLista() = False Then Exit Sub
        PreencheComboClienteTipo()
        PreencheComboVendaTipo()
        '
        ' ALTERA A SITUAÇÃO DO REGISTRO
        If dtAtiv.Rows.Count = 0 Then
            AlteraSit(FlagEstado.NovoRegistro)
            btnNovo_Click(New Object, New System.EventArgs)
        Else
            AlteraSit(FlagEstado.RegistroSalvo)
        End If
        '
    End Sub
    '
    ' PREENCHE A LISTAGEM
    Private Function PreencheLista() As Boolean
        Dim CliBLL As New ClienteBLL
        '
        Try
            dtAtiv = CliBLL.GetClienteAtividades()
            '
            blvAtividades.DataSource = dtAtiv
            blvAtividades.Columns(0).DisplayMember = "RGAtividade"
            blvAtividades.Columns(0).ValueMember = "RGAtividade"
            blvAtividades.Columns(1).DisplayMember = "Atividade"
            '
            Return True
        Catch ex As Exception
            MessageBox.Show("Uma exceção ocorreu:" & vbNewLine &
                            ex.Message, "Exceção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End Try
        '
    End Function

    ' OBTEM OS VALORES DOS CAMPOS
    Private Sub PreencheCampos(RG As Byte)
        Try
            Dim r As DataRow = dtAtiv.[Select]("RGAtividade = " & RG)(0)
            '
            lblRG.Text = Format(r("RGAtividade"), "0000")
            txtAtividade.Text = r("Atividade")
            cmbClienteTipo.SelectedValue = r("ClienteTipo")
            cmbVendaTipo.SelectedValue = r("VendasTipo")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        '
        AlteraSit(FlagEstado.RegistroSalvo)
        '
    End Sub

    ' PREENCHE COMBOS
    Private Sub PreencheComboClienteTipo()
        Dim dtTipo As New DataTable

        'Adiciona todas as possibilidades de instrucao
        dtTipo.Columns.Add("id")
        dtTipo.Columns.Add("Tipo")
        dtTipo.Rows.Add(New Object() {False, "Pessoa Física"})
        dtTipo.Rows.Add(New Object() {True, "Pessoa Jurídica"})

        With cmbClienteTipo
            .DataSource = dtTipo
            .ValueMember = "id"
            .DisplayMember = "Tipo"
        End With
    End Sub
    Private Sub PreencheComboVendaTipo()
        Dim dtTipo As New DataTable

        'Adiciona todas as possibilidades de instrucao
        dtTipo.Columns.Add("id")
        dtTipo.Columns.Add("Tipo")
        dtTipo.Rows.Add(New Object() {False, "Varejo"})
        dtTipo.Rows.Add(New Object() {True, "Atacado"})

        With cmbVendaTipo
            .DataSource = dtTipo
            .ValueMember = "id"
            .DisplayMember = "Tipo"
        End With
    End Sub

    ' EVENTOS
    Private Sub btnFechar_Click(sender As Object, e As EventArgs) Handles btnFechar.Click

        ' VERIFICA SE HOUVE ALTERAÇÃO DE REGISTRO
        If btnSalvar.Enabled = True Then
            If MessageBox.Show("Houve alteração nesse Registro de Atividade de Cliente..." & vbNewLine &
                               "Se você decidir sair agora o registro não será salvo!" & vbNewLine &
                               "Você deseja sair sem salvar o registro?", "Registro Alterado",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button2) = vbNo Then
                btnSalvar.Select()
                Exit Sub
            End If
        End If

        ' FECHA O FORMULÁRIO
        Me.Close()
        MostraMenuPrincipal()
    End Sub

    Private Sub blvAtividades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles blvAtividades.SelectedIndexChanged
        If blvAtividades.SelectedItems.Count > 0 AndAlso IsNumeric(blvAtividades.SelectedItems(0).Text) Then
            PreencheCampos(CByte(blvAtividades.SelectedItems(0).Text))
        End If
    End Sub

    Private Sub tsMenu_GotFocus(sender As Object, e As EventArgs) Handles tsMenu.GotFocus
        btnNovo.Select()
    End Sub

    ' ALTERA A COR DO HEADER DO LISTBOX
    Private Sub blvAtividades_DrawColumnHeader(sender As Object, eventArgs As BetterListViewDrawColumnHeaderEventArgs) Handles blvAtividades.DrawColumnHeader
        If eventArgs.ColumnHeaderBounds.BoundsOuter.Width > 0 AndAlso
            eventArgs.ColumnHeaderBounds.BoundsOuter.Height > 0 Then
            ' Pode Colocar: AndAlso eventArgs.ColumnHeader.Index = 1 AndAlso

            Dim brush As Brush = New LinearGradientBrush(eventArgs.ColumnHeaderBounds.BoundsOuter, Color.Transparent, Color.FromArgb(100, Color.LightSlateGray), LinearGradientMode.Vertical)
            eventArgs.Graphics.FillRectangle(brush, eventArgs.ColumnHeaderBounds.BoundsOuter)
            brush.Dispose()
        End If
    End Sub
    '
    ' COMPARA SE HOUVE ALGUMA ALTERAÇÃO NOS CAMPOS
    Private Sub RegAlterado()
        Dim Reg As DataRow() = dtAtiv.[Select]("RGAtividade = " & lblRG.Text)
        '
        If Reg.Length > 0 Then
            If txtAtividade.Text <> Reg(0)("Atividade") OrElse
                cmbClienteTipo.SelectedValue <> Reg(0)("ClienteTipo") OrElse
                cmbVendaTipo.SelectedValue <> Reg(0)("VendasTipo") Then
                AlteraSit(FlagEstado.Alterado)
            End If
        End If
        '
    End Sub
    '
    Private Sub txtAtividade_TextChanged(sender As Object, e As EventArgs) Handles txtAtividade.TextChanged
        If Sit = FlagEstado.NovoRegistro Then Exit Sub
        RegAlterado()
    End Sub
    '
    Private Sub cmbClienteTipo_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cmbClienteTipo.SelectionChangeCommitted, cmbVendaTipo.SelectionChangeCommitted
        If Sit = FlagEstado.NovoRegistro Then Exit Sub
        RegAlterado()
    End Sub
    '
    ' SALVAR O REGISTRO NOVO OU ALTERADO
    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        ' VALIDAR OS CAMPOS
        Dim Campos As New List(Of Object)

        Campos.Add(txtAtividade)
        Campos.Add(cmbClienteTipo)
        Campos.Add(cmbVendaTipo)
        If ValidaCampos(Campos) = False Then Exit Sub
        '
        Dim sql As New SQLControl
        '
        ' ADICIONA OS PARÂMETROS
        sql.AddParam("@RGAtividade", lblRG.Text)
        SQL.AddParam("@Atividade", txtAtividade.Text)
        SQL.AddParam("@ClienteTipo", cmbClienteTipo.SelectedValue)
        SQL.AddParam("@VendasTipo", cmbVendaTipo.SelectedValue)

        If Sit = FlagEstado.NovoRegistro Then
            'PARA UM REGISTRO NOVO - INSERT
            SQL.ExecQuery("INSERT INTO tblClienteAtividade " &
              "(RGAtividade, Atividade, ClienteTipo, VendasTipo) " &
              "VALUES (@RGAtividade, @Atividade, @ClienteTipo, @VendasTipo);")
        ElseIf Sit = FlagEstado.Alterado Then
            'PARA UM REGISTRO ALTERADO - UPDATE
            SQL.ExecQuery("UPDATE tblClienteAtividade " &
                          "SET Atividade=@Atividade, ClienteTipo=@ClienteTipo, VendasTipo=@VendasTipo " &
                          "WHERE RGAtividade=@RGAtividade;")
        End If

        If SQL.HasException(True) Then Exit Sub

        MessageBox.Show("Esse Registro foi SALVO com sucesso!", "Registro Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        btnFechar.Focus()
        PreencheLista()
        AlteraSit(FlagEstado.RegistroSalvo)

    End Sub

    ' NOVO REGISTRO
    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click

        AlteraSit(FlagEstado.NovoRegistro)
        lblRG.Text = Format(FindMaxRG(), "0000") ' OBTÉM O MAIOR REGISTRO
        txtAtividade.Text = ""
        cmbClienteTipo.SelectedValue = vbNull
        cmbVendaTipo.SelectedValue = vbNull
        txtAtividade.Focus()

    End Sub

    ' EXCLUIR REGISTRO
    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Not IsNumeric(lblRG.Text) Then
            MessageBox.Show("Selecione uma Atividade para ser excluída", "Excluir Atividade")
            Exit Sub
        End If

        Try
            Dim sql As New SQLControl
            '
            sql.ExecQuery("DELETE FROM ClienteAtividade where RGAtividade = " & CInt(lblRG.Text))
            If Sql.HasException(False) Then
                MessageBox.Show("Essa Atividade não pôde ser excluída porque existem Clientes que estão cadastrados nela..." & vbNewLine &
                                "Altere todos os Clientes que estão cadastrados nessa atividade e depois tente excluir novamente",
                                "Excluir Atividade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            PreencheLista()
            AlteraSit(FlagEstado.RegistroSalvo)
            MessageBox.Show("Atividade excluída com sucesso!", "Atividade Excluída", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    ' CANCELAR EDITAR OU ADICIONAR
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If Sit = FlagEstado.Alterado Then
            PreencheCampos(lblRG.Text)
        ElseIf Sit = FlagEstado.NovoRegistro Then
            PreencheLista()
        End If
    End Sub

    ' PROCURA E RETORNA O ÚLTIMO RG
    Private Function FindMaxRG() As Integer
        Dim sql As New SQLControl
        '
        sql.ExecQuery("SELECT * FROM tblClienteAtividade;")
        '
        If Sql.RecordCount < 1 Then
            Return 1
        Else
            Return Sql.DBDT.AsEnumerable().Max(Function(r) r.Field(Of Byte)("RGAtividade")) + 1
        End If
        '
    End Function

    ' ALTERA CONTROLES PELO SIT
    Private Sub AlteraSit(mySIT As FlagEstado)
        Select Case mySIT
            Case FlagEstado.RegistroSalvo
                Sit = FlagEstado.RegistroSalvo
                btnSalvar.Enabled = False
                btnNovo.Enabled = True
                btnExcluir.Enabled = True
                btnCancel.Enabled = False
            Case FlagEstado.Alterado
                Sit = FlagEstado.Alterado
                btnSalvar.Enabled = True
                btnNovo.Enabled = False
                btnExcluir.Enabled = False
                btnCancel.Enabled = True
            Case FlagEstado.NovoRegistro
                Sit = FlagEstado.NovoRegistro
                btnSalvar.Enabled = True
                btnNovo.Enabled = False
                btnExcluir.Enabled = False
                btnCancel.Enabled = True
        End Select
    End Sub

    ' VALIDA CAMPOS
    Private Function ValidaCampos(Campos As List(Of Object)) As Boolean
        For i = 0 To Campos.Count - 1
            Select Case Campos(i).GetType.ToString
                Case "System.Windows.Forms.TextBox" ' SE FOR TEXTBOX
                    If DirectCast(Campos(i), TextBox).TextLength = 0 Then
                        epValida.SetError(Campos(i), "Preencha o valor desse campo ")
                        DirectCast(Campos(i), TextBox).Focus()
                        Return False
                    End If
                Case "System.Windows.Forms.ComboBox" ' SE FOR COMBOBOX
                    If IsNothing(DirectCast(Campos(i), ComboBox).SelectedValue) Then
                        epValida.SetError(Campos(i), "Preencha o valor desse campo!")
                        DirectCast(Campos(i), ComboBox).Focus()
                        Return False
                    End If
                Case "System.Windows.Forms.MaskedTextBox" ' SE FOR MASKEDTEXTBOX
                    If IsNothing(DirectCast(Campos(i), MaskedTextBox).Text) Then
                        epValida.SetError(Campos(i), "Preencha o valor desse campo!")
                        DirectCast(Campos(i), MaskedTextBox).Focus()
                        Return False
                    End If
            End Select
        Next
        Return True
    End Function

    ' VALIDAR OS COMBOS
    Private Sub ValidaCombos(sender As Object, e As CancelEventArgs) Handles cmbClienteTipo.Validating, cmbVendaTipo.Validating

        If Not IsNothing(DirectCast(sender, ComboBox).SelectedValue) Then
            e.Cancel = False
        Else
            MessageBox.Show("O Valor digitado não existe na listagem" & vbNewLine &
                             "Favor escolher um item da listagem...", "Escolha um item", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If Sit <> FlagEstado.NovoRegistro AndAlso Not String.IsNullOrEmpty(DirectCast(sender, ComboBox).Text) Then
                e.Cancel = True
                DirectCast(sender, ComboBox).Focus()
                SendKeys.Send("%{DOWN}")
            End If
        End If
    End Sub

End Class