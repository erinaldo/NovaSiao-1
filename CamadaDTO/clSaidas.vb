Imports System.ComponentModel
'
Public Class clSaidas : Implements IEditableObject
#Region "ESTRUTURA DOS DADOS"
    Structure SaidaStructure ' alguns usam FRIEND em vez de DIM
        '--- tblSaida
        Dim _IDSaida As Integer?
        Dim _Origem As Byte '--- 1:APAGAR | 3:CREDITOS (NIVELAMENTOS)
        Dim _IDOrigem As Integer? '--- ID Transacao ou Credito
        Dim _IDConta As Byte
        Dim _IDMovForma As Int16?
        Dim _SaidaData As Date
        Dim _SaidaValor As Double
        Dim _Creditar As Boolean
        Dim _IDCaixa As Integer?
        Dim _Descricao As String
        Dim _Observacao As String
        '
        '--- OUTRAS FONTES
        'IDFilial As Int
        'ApelidoFilial As String
        'Conta As String
        'IDMovTipo() As Int16?
        'MovForma As String
        'IDOperadora as Int16?
        'Operadora As String
        'Cartao As String
    End Structure
#End Region
    '
#Region "PRIVATE VARIABLES"
    Private SData As SaidaStructure
    Private backupData As SaidaStructure
    Private inTxn As Boolean = False
#End Region
    '
#Region "IMPLEMENTS EVENTS"
    Public Sub New()
        SData = New SaidaStructure()
        With SData
            ._SaidaValor = 0
            ._Creditar = False
        End With
    End Sub
    '
    '-- IMPLEMENTS IEditableObject
    Public Sub BeginEdit() Implements IEditableObject.BeginEdit
        If Not inTxn Then
            Me.backupData = SData
            inTxn = True
        End If
    End Sub
    '
    Public Sub CancelEdit() Implements IEditableObject.CancelEdit
        If inTxn Then
            Me.SData = backupData
            inTxn = False
        End If
    End Sub
    '
    Public Sub EndEdit() Implements IEditableObject.EndEdit
        If inTxn Then
            backupData = New SaidaStructure()
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
    '--- Propriedade IDSaida
    Public Property IDSaida() As Integer?
        Get
            Return SData._IDSaida
        End Get
        Set(ByVal value As Integer?)
            If value <> SData._IDSaida Then
                RaiseEvent AoAlterar()
            End If
            SData._IDSaida = value
        End Set
    End Property
    '
    '--- Propriedade Origem
    Public Property Origem() As Byte
        Get
            Return SData._Origem
        End Get
        Set(ByVal value As Byte)
            If value <> SData._Origem Then
                RaiseEvent AoAlterar()
            End If
            SData._Origem = value
        End Set
    End Property
    '
    '--- Propriedade IDOrigem
    Public Property IDOrigem() As Integer?
        Get
            Return SData._IDOrigem
        End Get
        Set(ByVal value As Integer?)
            If value <> SData._IDOrigem Then
                RaiseEvent AoAlterar()
            End If
            SData._IDOrigem = value
        End Set
    End Property
    '
    '--- Propriedade IDConta
    Public Property IDConta() As Byte
        Get
            Return SData._IDConta
        End Get
        Set(ByVal value As Byte)
            If value <> SData._IDConta Then
                RaiseEvent AoAlterar()
            End If
            SData._IDConta = value
        End Set
    End Property
    '
    '--- Propriedade IDMovForma
    Public Property IDMovForma() As Int16?
        Get
            Return SData._IDMovForma
        End Get
        Set(ByVal value As Int16?)
            If value <> SData._IDMovForma Then
                RaiseEvent AoAlterar()
            End If
            SData._IDMovForma = value
        End Set
    End Property
    '
    '--- Propriedade SaidaData
    Public Property SaidaData() As Date
        Get
            Return SData._SaidaData
        End Get
        Set(ByVal value As Date)
            If value <> SData._SaidaData Then
                RaiseEvent AoAlterar()
            End If
            SData._SaidaData = value
        End Set
    End Property
    '
    '--- Propriedade EntradaValor
    Public Property SaidaValor() As Double
        Get
            Return SData._SaidaValor
        End Get
        Set(ByVal value As Double)
            If value <> SData._SaidaValor Then
                RaiseEvent AoAlterar()
            End If
            SData._SaidaValor = value
        End Set
    End Property
    '
    '--- Propriedade Observacao
    Public Property Observacao() As String
        Get
            Return SData._Observacao
        End Get
        Set(ByVal value As String)
            If value <> SData._Observacao Then
                RaiseEvent AoAlterar()
            End If
            SData._Observacao = value
        End Set
    End Property
    '
    '--- Propriedade Creditar
    Public Property Creditar() As Boolean
        Get
            Return SData._Creditar
        End Get
        Set(ByVal value As Boolean)
            If value <> SData._Creditar Then
                RaiseEvent AoAlterar()
            End If
            SData._Creditar = value
        End Set
    End Property
    '
    '--- Propriedade IDCaixa
    Public Property IDCaixa() As Integer?
        Get
            Return SData._IDCaixa
        End Get
        Set(ByVal value As Integer?)
            If value <> SData._IDCaixa Then
                RaiseEvent AoAlterar()
            End If
            SData._IDCaixa = value
        End Set
    End Property
    '
    '--- Propriedade Descricao
    Public Property Descricao() As String
        Get
            Return SData._Descricao
        End Get
        Set(ByVal value As String)
            If value <> SData._Descricao Then
                RaiseEvent AoAlterar()
            End If
            SData._Descricao = value
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
