Imports System.ComponentModel
'
Public Class clEntradas : Implements IEditableObject
#Region "ESTRUTURA DOS DADOS"
    Structure EntradaStructure ' alguns usam FRIEND em vez de DIM
        '--- tblEntrada
        Dim _IDEntrada As Integer?
        Dim _Origem As Byte '--- 1:Transacao | 2:AReceberParcela | 3:Creditos
        Dim _IDOrigem As Integer '--- IDTransacao | IDAReceberParcela
        Dim _IDConta As Byte
        Dim _IDMovForma As Int16?
        Dim _EntradaData As Date
        Dim _EntradaValor As Double
        Dim _Creditar As Boolean
        Dim _IDCaixa As Integer?
        Dim _Descricao As String
        Dim _Observacao As String
        '
        '--- OUTRAS FONTES
        'IDFilial As Int
        'ApelidoFilial As String
        'Conta As String
        'IDMovTipo As Int16?
        'MovForma As String
        'IDOperadora as Int16?
        'Operadora As String
        'Cartao As String
    End Structure
#End Region
    '
#Region "PRIVATE VARIABLES"
    Private EData As EntradaStructure
    Private backupData As EntradaStructure
    Private inTxn As Boolean = False
#End Region
    '
#Region "IMPLEMENTS EVENTS"
    Public Sub New()
        EData = New EntradaStructure()
        With EData
            ._EntradaValor = 0
            ._Creditar = False
        End With
    End Sub
    '
    '-- IMPLEMENTS IEditableObject
    Public Sub BeginEdit() Implements IEditableObject.BeginEdit
        If Not inTxn Then
            Me.backupData = EData
            inTxn = True
        End If
    End Sub
    '
    Public Sub CancelEdit() Implements IEditableObject.CancelEdit
        If inTxn Then
            Me.EData = backupData
            inTxn = False
        End If
    End Sub
    '
    Public Sub EndEdit() Implements IEditableObject.EndEdit
        If inTxn Then
            backupData = New EntradaStructure()
            inTxn = False
        End If
    End Sub
    '
    '-- _EVENTO PUBLIC AOALTERAR
    Public Event AoAlterar()
    '
    Public ReadOnly Property RegistroAlterado As Boolean
        Get
            Return inTxn
        End Get
    End Property
    '
#End Region
    '
#Region "PROPRIEDADES"
    '
    '--- Propriedade IDEntrada
    Public Property IDEntrada() As Integer?
        Get
            Return EData._IDEntrada
        End Get
        Set(ByVal value As Integer?)
            If value <> EData._IDEntrada Then
                RaiseEvent AoAlterar()
            End If
            EData._IDEntrada = value
        End Set
    End Property
    '
    '--- Propriedade Origem
    Public Property Origem() As Byte
        Get
            Return EData._Origem
        End Get
        Set(ByVal value As Byte)
            If value <> EData._Origem Then
                RaiseEvent AoAlterar()
            End If
            EData._Origem = value
        End Set
    End Property
    '
    '--- Propriedade IDOrigem
    Public Property IDOrigem() As Integer
        Get
            Return EData._IDOrigem
        End Get
        Set(ByVal value As Integer)
            If value <> EData._IDOrigem Then
                RaiseEvent AoAlterar()
            End If
            EData._IDOrigem = value
        End Set
    End Property
    '
    '--- Propriedade IDConta
    Public Property IDConta() As Byte
        Get
            Return EData._IDConta
        End Get
        Set(ByVal value As Byte)
            If value <> EData._IDConta Then
                RaiseEvent AoAlterar()
            End If
            EData._IDConta = value
        End Set
    End Property
    '
    '--- Propriedade EntradaData
    Public Property EntradaData() As Date
        Get
            Return EData._EntradaData
        End Get
        Set(ByVal value As Date)
            If value <> EData._EntradaData Then
                RaiseEvent AoAlterar()
            End If
            EData._EntradaData = value
        End Set
    End Property
    '
    '--- Propriedade EntradaValor
    Public Property EntradaValor() As Double
        Get
            Return EData._EntradaValor
        End Get
        Set(ByVal value As Double)
            If value <> EData._EntradaValor Then
                RaiseEvent AoAlterar()
            End If
            EData._EntradaValor = value
        End Set
    End Property
    '
    '--- Propriedade IDMovForma
    Public Property IDMovForma() As Int16?
        Get
            Return EData._IDMovForma
        End Get
        Set(ByVal value As Int16?)
            If value <> EData._IDMovForma Then
                RaiseEvent AoAlterar()
            End If
            EData._IDMovForma = value
        End Set
    End Property
    '
    '--- Propriedade Observacao
    Public Property Observacao() As String
        Get
            Return EData._Observacao
        End Get
        Set(ByVal value As String)
            If value <> EData._Observacao Then
                RaiseEvent AoAlterar()
            End If
            EData._Observacao = value
        End Set
    End Property
    '
    '--- Propriedade Creditar
    Public Property Creditar() As Boolean
        Get
            Return EData._Creditar
        End Get
        Set(ByVal value As Boolean)
            If value <> EData._Creditar Then
                RaiseEvent AoAlterar()
            End If
            EData._Creditar = value
        End Set
    End Property
    '
    '--- Propriedade IDCaixa
    Public Property IDCaixa() As Integer?
        Get
            Return EData._IDCaixa
        End Get
        Set(ByVal value As Integer?)
            If value <> EData._IDCaixa Then
                RaiseEvent AoAlterar()
            End If
            EData._IDCaixa = value
        End Set
    End Property
    '
    '--- Propriedade Descricao
    Public Property Descricao() As String
        Get
            Return EData._Descricao
        End Get
        Set(ByVal value As String)
            If value <> EData._Descricao Then
                RaiseEvent AoAlterar()
            End If
            EData._Descricao = value
        End Set
    End Property
    '
    '--- Propriedades Externas
    Property IDFilial() As Integer
    Property ApelidoFilial() As String
    Property Conta() As String
    Property IDMovTipo() As Int16?
    Property MovForma As String
    Property IDOperadora As Int16?
    Property Operadora As String
    Property Cartao As String
    '
#End Region
    '
End Class
