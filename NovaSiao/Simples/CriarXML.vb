Imports System.Xml
Imports System.Reflection
'
Public Class CriarXML
    Private Const filename As String = "sampledata.xml"
    '
    Shared Sub RecebeObjProperty_InsereXML(minhaClasse As Object) '  , writer As XmlTextWriter)
        'writer As XmlTextWriter, minhaClasse As Object

        Dim myProps As New List(Of PropertyInfo)
        '
        Dim str As String = ""

        For Each p As PropertyInfo In minhaClasse.GetType().GetProperties()
            myProps.Add(p)
            str = str + p.Name + ":" + If(p.GetValue(minhaClasse), "").ToString + vbNewLine
        Next
        '
        MsgBox(minhaClasse.GetType().Name & vbNewLine & str)
        'Main()
        '
    End Sub

    Public Shared Sub Main()

        Dim settings As XmlWriterSettings = New XmlWriterSettings()
        settings.Indent = True
        Dim writer As XmlWriter = XmlWriter.Create(filename, settings)

        ' Write the Processing Instruction node.
        Dim PItext As String = "type=""text/xsl"" href=""book.xsl"""
        writer.WriteProcessingInstruction("xml-stylesheet", PItext)

        'Write the DocumentType node.
        writer.WriteDocType("book", Nothing, Nothing, "<!ENTITY h ""hardcover"">")

        ' Write a Comment node.
        writer.WriteComment("sample XML")

        ' Write the root element.
        writer.WriteStartElement("book")

        ' Write the genre attribute
        writer.WriteAttributeString("genre", "novel")

        ' Write the ISBN attribute.
        writer.WriteAttributeString("ISBN", "1-8630-014")

        ' Write the title.
        writer.WriteElementString("title", "The Handmaid's Tale")

        ' Write the style element.
        writer.WriteStartElement("style")
        writer.WriteEntityRef("h")
        writer.WriteEndElement()

        ' Write the price.
        writer.WriteElementString("price", "19.95")

        ' Write CDATA.
        writer.WriteCData("Prices 15% off!!")

        ' Write the close tag for the root element.
        writer.WriteEndElement()

        writer.WriteEndDocument()

        ' Write the XML to file and close the writer
        writer.Flush()
        writer.Close()

    End Sub 'Main 

    '
    Private Function CriaConfigXML() As Boolean
        Try
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
                '.WriteElementString("DataPadrao", dtpDataPadrao.Value.ToShortDateString)
                '.WriteElementString("FilialPadrao", _IDFilial)
                '.WriteElementString("FilialDescricao", txtFilialPadrao.Text)
                '.WriteElementString("ContaPadrao", _IDConta)
                '.WriteElementString("ContaDescricao", txtContaPadrao.Text)
                '.WriteElementString("Cidade", txtCidade.Text)
                '.WriteElementString("UF", txtUF.Text)
                '.WriteElementString("Naturalidade", txtNaturalidade.Text)
                '.WriteElementString("MensagemInicial", txtMensagem.Text)
                '.WriteElementString("IDProdutoTipoPadrao", "")
                '.WriteElementString("ProdutoTipoPadrao", "")
                '.WriteElementString("IDProdutoSubTipoPadrao", "")
                '.WriteElementString("ProdutoSubTipoPadrao", "")
                '.WriteElementString("TaxaPermanencia", txtPermanencia.Text)
                'encerra o elemento
                .WriteEndElement()

                ' Segunda Seção: DADOS DA EMPRESA
                '----------------------------------------------------------------------------------
                .WriteStartElement("DadosEmpresa")
                ' Sub Elementos
                '.WriteElementString("RazaoSocial", txtRazao.Text)
                '.WriteElementString("NomeFantasia", txtFantasia.Text)
                '.WriteElementString("CNPJ", txtCNPJ.Text)
                '.WriteElementString("InscricaoSocial", txtIncricao.Text)
                '.WriteElementString("TelefonePrincipal", txtTelPrincipal.Text)
                '.WriteElementString("TelefoneGerencia", txtTelGerencia.Text)
                '.WriteElementString("ContatoGerencia", txtContatoGerencia.Text)
                '.WriteElementString("TelefoneFinanceiro", txtTelFinanceiro.Text)
                '.WriteElementString("ContatoFinanceiro", txtContatoFinanceiro.Text)
                ''encerra o elemento
                .WriteEndElement()

                ' Terceira Seção: ARQUIVOS IMPORTANTES
                '----------------------------------------------------------------------------------
                .WriteStartElement("ArquivoLogo")
                ' Sub Elementos
                '.WriteElementString("ArquivoLogoMono", txtLogoMonoCaminho.Text)
                '.WriteElementString("ArquivoLogoColor", txtLogoColorCaminho.Text)
                '.WriteElementString("BackupDir", "")
                'encerra o elemento
                .WriteEndElement()

                ' Quarta Seção: SERVIDOR
                '----------------------------------------------------------------------------------
                .WriteStartElement("ServidorDados")
                ' Sub Elementos
                '.WriteElementString("StringConexao", txtStringConexao.Text)
                '.WriteElementString("ControlaBackup", chkVerBackup.Checked.ToString)
                'encerra o elemento
                .WriteEndElement()
                '
                'FECHA CONFIGURAÇÃO
                .WriteEndElement()
                'escreve o xml para o arquivo e fecha o objeto escritor
                .Close()
                Return True
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function
End Class
