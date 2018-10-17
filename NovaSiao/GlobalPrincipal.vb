Imports System.Xml
Imports System.IO

Public Module GlobalPrincipal
    ' VARIANT PUBLICA PARA PASSAGEM DE DADOS
    Public UsuarioAcesso(1) As Integer ' (0)IdUser; (1)UsuarioAcesso

    ' ENUM PUBLICO ESTADO DO REGISTRO
    Public Enum FlagEstado
        RegistroSalvo = 1
        Alterado = 2
        NovoRegistro = 3
        RegistroBloqueado = 4
    End Enum

    ' ENUM PULICO ACAO NO REGISTRO
    Public Enum FlagAcao
        INSERIR = 1
        EDITAR = 2
        VISUALIZAR = 3
        EXCLUIR = 4
    End Enum

    ' VARIANTE PUBLICA TIPO DE ACESSO DOS USUÁRIO
    Public Enum AcessoTipo
        Administrador = 1
        UsuarioSenior = 2
        UsuarioComum = 3
    End Enum
    '
    ' ENUM PARA frmDataInformar
    Enum DataTipo
        Passado = 1
        PassadoPresente = 2
        Futuro = 3
        FuturoPresente = 4
    End Enum
    '
    ' OCULTAR E REVELAR O MENU PRINCIPAL
    Public Sub OcultaMenuPrincipal()
        frmPrincipal.SContainerPrincipal.Visible = False
        frmPrincipal.Panel1.BackColor = Color.Gainsboro
    End Sub
    '
    '--- REVELA MENU PRINCIPAL
    Public Sub MostraMenuPrincipal()
        frmPrincipal.SContainerPrincipal.Visible = True
        frmPrincipal.Panel1.BackColor = Color.SlateGray
    End Sub
    '
    '--- VERIFICAR SE EXISTE O ARQUIVO CONFIG XML
    Private Function MyConfig() As XmlDocument
        '
        If File.Exists(Application.StartupPath & "\ConfigFiles\Config.xml") = True Then
            ' ler o arquivo config xml
            Dim myXML As New XmlDocument
            '
            Try
                myXML.Load(Application.StartupPath & "\ConfigFiles\Config.xml")
                Return myXML
            Catch ex As Exception
                Throw ex
            End Try
            '
        Else
            Return Nothing
        End If
        '
    End Function
    '
    ' OBTER O VALORES DEFAULT DE CONTROLE
    Public Function ObterDefault(CampoDefault As String) As String
        '
        Try
            Return MyConfig.SelectSingleNode("Configuracao").SelectSingleNode("ValoresPadrao").SelectSingleNode(CampoDefault).InnerText
        Catch ex As Exception
            Return String.Empty
        End Try
        '
    End Function
    '
    ' SETAR OS VALORES DEFAULT DE CONTROLE
    Public Function SetarDefault(CampoDefault As String, myValor As String) As Boolean
        '
        Dim MyXMLNode As XmlNode = MyConfig.SelectSingleNode("/Configuracao/ValoresPadrao/" & CampoDefault)
        '
        If MyXMLNode IsNot Nothing Then
            MyXMLNode.InnerText = myValor
        Else
            MessageBox.Show("O XMLNode não foi encontrado...", "Salvar Config", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If ' Save the Xml.
        '
        MyConfig.Save("ConfigFiles\Config.xml")
        Return True
        '
    End Function
    '
    '--- OBTER O VALOR DA FILIAL PADRAO DO SISTEMA
    Public Function Obter_FilialPadrao() As Integer?
        Dim myFil As String = String.Empty
        '
        myFil = ObterDefault("FilialPadrao")
        '
        If myFil <> String.Empty Then
            Return CInt(myFil)
        Else
            MessageBox.Show("Não há Filial Padrão no Config do Sistema..." & vbNewLine & vbNewLine &
                            "Favor informar a Filial Padrão no Config.", "Filial Padrão",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End If
        '
    End Function
    '
    '--- OBTER O VALOR DA CONTA PADRAO DO SISTEMA
    Public Function Obter_ContaPadrao() As Integer?
        Dim myCt As String = String.Empty
        '
        myCt = ObterDefault("ContaPadrao")
        '
        If myCt <> String.Empty Then
            Return CInt(myCt)
        Else
            MessageBox.Show("Não há Conta Padrão no Config do Sistema..." & vbNewLine & vbNewLine &
                            "Favor informar a Conta Padrão no Config.", "Conta Padrão",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End If
        '
    End Function
    '
    '--- OBTER A DATA PADRAO DO SISTEMA
    Public Function Obter_DataPadrao() As Date?
        Dim myDt As String = String.Empty
        '
        myDt = ObterDefault("DataPadrao")
        '
        If myDt <> String.Empty Then
            Return CDate(myDt)
        Else
            MessageBox.Show("Não há Data Padrão no Config do Sistema..." & vbNewLine & vbNewLine &
                            "Favor informar a Data Padrão no Config.", "Data Padrão",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        End If
        '
    End Function
    '
    '--- VERIFICA SE A DATA ESTA BLOQUEADA PELO SISTEMA
    Public Function DataPermitida(myData As Date, IDFilial As Integer, IDConta As Byte) As Boolean
        '
        If 1 = 0 Then
            MessageBox.Show("Essa data já está bloqueada pelo sistema..." & vbNewLine &
                "Já existe caixa efetuado posterior a essa data." & vbNewLine &
                "Favor utilizar outra data.", "Data Bloqueada",
                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        Else
            MsgBox("Verificação de Data ainda não implementada")
            Return True
        End If
        '
    End Function
    '
    '--- OBTER VALOR DO NODE XML DO ARQUIVO CONFIGXML PELO NOME
    Public Function ObterConfigValorNode(NodeName) As String
        '    
        Dim elemList As XmlNodeList = MyConfig.GetElementsByTagName(NodeName)
        Dim myValor As String = ""
        '
        Dim i As Integer
        For i = 0 To elemList.Count - 1
            myValor = elemList(i).InnerXml
        Next i
        '
        Return myValor
        '
    End Function
    '
    Public Function SetarNode(NodeName As String, NodeValue As String) As Boolean
        '
        Dim config As XmlDocument = MyConfig()
        Dim elemList As XmlNodeList = config.GetElementsByTagName(NodeName)
        Dim myXMLNode As XmlNode = Nothing
        '
        Dim i As Integer
        For i = 0 To elemList.Count - 1
            myXMLNode = elemList(i)
        Next i
        '
        If myXMLNode IsNot Nothing Then
            myXMLNode.InnerText = NodeValue
        Else
            MessageBox.Show("O XMLNode não foi encontrado...", "Salvar Config", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        '
        ' Save the Xml.
        config.Save("ConfigFiles\Config.xml")
        Return True
        '
    End Function
    '
    '--- OBTER DATA DO USUARIO MULTIPLAS FUNCOES
    Public Function DataInformar(subTitulo As String,
                                 dataTipo As DataTipo,
                                 dataPadrao As Date,
                                 formOrigem As Form) As Date?
        '
        Dim frmD As New frmDataInformar(subTitulo, dataTipo, dataPadrao, formOrigem)
        '
        frmD.ShowDialog()
        '
        If frmD.DialogResult = DialogResult.OK Then
            Return frmD.propDataInfo
        Else
            Return Nothing
        End If
        '
    End Function
    '
End Module
