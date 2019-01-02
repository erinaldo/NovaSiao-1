﻿Imports System.Xml
Imports System.IO
Imports CamadaBLL

Public Module GlobalPrincipal
    '
#Region "PUBLIC VARIANTS"
    '
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
    ' ENUM PARA Origem do Preco
    Public Enum precoOrigem
        PRECO_VENDA = 1
        PRECO_COMPRA = 2
    End Enum
    '
#End Region '/ PUBLIC VAR
    '
#Region "PUBLIC FUNCTIONS"
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
    '--- VERIFICA SE A DATA ESTA BLOQUEADA PELO SISTEMA
    '--- PARECE COM mBLL.Conta_GetDataBloqueio (retorna a data de bloqueio)
    Public Function DataBloqueada(myData As Date, IDConta As Byte) As Boolean
        '
        Dim mBLL As New MovimentacaoBLL
        Dim dtBloq As Date? = Nothing
        '
        Try
            dtBloq = mBLL.Conta_GetDataBloqueio(IDConta)
            '
            If IsNothing(dtBloq) Then
                Return False
            Else
                If myData < dtBloq Then
                    '--- obter a descricao da Conta Padrao
                    Dim ContaPadrao As String = ObterDefault("ContaDescricao")
                    '
                    MessageBox.Show("A Conta Padrão: " & ContaPadrao.ToUpper & vbNewLine &
                                    "já está bloqueada nessa DATA, pelo sistema..." & vbNewLine & vbNewLine &
                                    "Já existe caixa efetuado posterior a essa data." & vbNewLine & vbNewLine &
                                    "Favor utilizar outra data ou altere a conta padrão.", "Data Bloqueada",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return True
                Else
                    Return False
                End If
            End If
            '
        Catch ex As Exception
            Throw ex
        End Try
        '
    End Function
    '
    '--- OBTER DATA DO USUARIO MULTIPLAS FUNCOES
    Public Function DataInformar(subTitulo As String,
                                 DataTipo As DataTipo,
                                 dataPadrao As Date,
                                 formOrigem As Form) As Date?
        '
        Dim frmD As New frmDataInformar(subTitulo, DataTipo, dataPadrao, formOrigem)
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
#End Region '/PUBLIC FUNCTIONS
    '
#Region "CONFIG XML - OBTER SETAR DADOS"
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
        Dim xmlConfig As XmlDocument = MyConfig()
        Dim MyXMLNode As XmlNode = xmlConfig.SelectSingleNode("/Configuracao/ValoresPadrao/" & CampoDefault)
        '
        If MyXMLNode IsNot Nothing Then
            MyXMLNode.InnerText = myValor
        Else
            MessageBox.Show("O XMLNode não foi encontrado...", "Salvar Config", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If ' Save the Xml.
        '
        xmlConfig.Save("ConfigFiles\Config.xml")
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
        '
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
#End Region ' /CONFIG XML - OBTER DADOS
    '
#Region "CONFIG XML - CONTROLE"
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
#End Region '/CONFIG XML - CONTROLE
    '
End Module
