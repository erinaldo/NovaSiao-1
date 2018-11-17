Imports System.Xml
Imports System.Reflection
'
Public Class CriarXML
    Private Const filename As String = "sampledata.xml"
    '
    Public Shared Sub WriteObjProperty_XML(o As Object, writer As XmlTextWriter)
        '
        '--- verify if object is list(of) or class
        If IsList(o) Then '--- if list then pass to all ite
            Dim iCount As Integer = DirectCast(o, IList).Count
            '
            If iCount = 0 Then Return
            '
            '--- write header of class
            ' Write the root element.
            Dim ClasseTipo As String = DirectCast(o, IList).Item(0).GetType().Name
            writer.WriteStartElement(ClasseTipo)

            '
            For i = 0 To iCount - 1
                Dim c As Object = DirectCast(o, IList).Item(i)
                '
                ' Write the root item
                writer.WriteStartElement(ClasseTipo & "-ITEM")
                writer.WriteAttributeString("Item", i + 1)
                '
                WritePropValuesXML(c, writer)
                '
                writer.WriteEndElement()
            Next
            '
        Else
            '
            ' Write the root element.
            Dim ClasseTipo As String = o.GetType().Name
            writer.WriteStartElement(ClasseTipo)
            '
            WritePropValuesXML(o, writer)
            '
            writer.WriteEndElement()
        End If
        '
    End Sub
    '
    Public Shared Sub WritePropValuesXML(minhaclasse As Object, writer As XmlTextWriter)
        '
        'Dim myProps As New List(Of PropertyInfo)
        'Dim str As String = ""
        '
        For Each p As PropertyInfo In minhaclasse.GetType().GetProperties()
            'myProps.Add(p)
            writer.WriteElementString(p.Name, If(p.GetValue(minhaclasse), "").ToString)
            'str = str + p.Name + ":" + If(p.GetValue(minhaclasse), "").ToString + vbNewLine
        Next
        '
        'MsgBox(minhaclasse.GetType().Name & vbNewLine & str)
        '
    End Sub
    '
    Public Shared Function IsList(o As Object) As Boolean
        '
        If o Is Nothing Then Return False
        Return TypeOf o Is IList AndAlso
                o.[GetType]().IsGenericType AndAlso
                o.[GetType]().GetGenericTypeDefinition().IsAssignableFrom(GetType(List(Of)))
        '
    End Function
    '
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
