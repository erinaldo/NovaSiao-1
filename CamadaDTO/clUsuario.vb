Imports System.ComponentModel
Public Class clUsuario
    Implements IEditableObject
#Region "ESTRUTURA DOS DADOS"
    Structure UserDados ' alguns usam FRIEND em vez de DIM
        Dim _IdUser As Nullable(Of Integer) '1
        Dim _UsuarioApelido As String '2
        Dim _UsuarioAcesso As Integer '3
        Dim _UsuarioSenha As String '4
        Dim _UsuarioAtivo As Boolean '5
        Dim _acao As Byte 'propriedade de controle
    End Structure
#End Region
#Region "PRIVATE VARIABLES"
    Private UsuarioData As UserDados
    Private backupData As UserDados
    Private inTxn As Boolean = False
#End Region
#Region "IMPLEMENTS EVENTS"
    Public Sub New()
        UsuarioData = New UserDados()
        With UsuarioData
            ._IdUser = Nothing
            ._UsuarioApelido = ""
            ._UsuarioAcesso = 3
            ._UsuarioSenha = ""
            ._UsuarioAtivo = True
            ._acao = Nothing
        End With
    End Sub
    Public Sub BeginEdit() Implements IEditableObject.BeginEdit
        If Not inTxn Then
            Me.backupData = UsuarioData
            inTxn = True
        End If
    End Sub
    Public Sub CancelEdit() Implements IEditableObject.CancelEdit
        If inTxn Then
            Me.UsuarioData = backupData
            inTxn = False
        End If
    End Sub
    Public Sub EndEdit() Implements IEditableObject.EndEdit
        If inTxn Then
            backupData = New UserDados()
            inTxn = False
        End If
    End Sub
    '_EVENTO PUBLIC AOALTERAR
    Public Event AoAlterar()

    Public Overrides Function ToString() As String
        Return UsuarioApelido.ToString
    End Function

    Public ReadOnly Property RegistroAlterado As Boolean
        Get
            Return inTxn
        End Get
    End Property

#End Region
#Region "PROPRIEDADES"
    Property IdUser() As Nullable(Of Integer) '1
        Get
            Return UsuarioData._IdUser
        End Get
        Set(ByVal value As Nullable(Of Integer))
            UsuarioData._IdUser = value
        End Set
    End Property
    Property UsuarioApelido() As String
        Get
            Return UsuarioData._UsuarioApelido
        End Get
        Set(value As String)
            If Not IsNothing(UsuarioData._UsuarioApelido) Then
                If value <> UsuarioData._UsuarioApelido Then
                    RaiseEvent AoAlterar()
                End If
            End If
            UsuarioData._UsuarioApelido = value
        End Set
    End Property
    Property UsuarioAcesso() As Integer
        Get
            Return UsuarioData._UsuarioAcesso
        End Get
        Set(value As Integer)
            If Not IsNothing(UsuarioData._UsuarioAcesso) Then
                If value <> UsuarioData._UsuarioAcesso Then
                    RaiseEvent AoAlterar()
                End If
            End If
            UsuarioData._UsuarioAcesso = value
        End Set
    End Property
    Property UsuarioSenha() As String
        Get
            Return UsuarioData._UsuarioSenha
        End Get
        Set(value As String)
            If Not IsNothing(UsuarioData._UsuarioSenha) Then
                If value <> UsuarioData._UsuarioSenha Then
                    RaiseEvent AoAlterar()
                End If
            End If
            UsuarioData._UsuarioSenha = value
        End Set
    End Property
    Property UsuarioAtivo() As Boolean
        Get
            Return UsuarioData._UsuarioAtivo
        End Get
        Set(value As Boolean)
            If Not IsNothing(UsuarioData._UsuarioAtivo) Then
                If value <> UsuarioData._UsuarioAtivo Then
                    RaiseEvent AoAlterar()
                End If
            End If
            UsuarioData._UsuarioAtivo = value
        End Set
    End Property
    Property Acao() As Byte
        Get
            Return UsuarioData._acao
        End Get
        Set(ByVal value As Byte)
            UsuarioData._acao = value
        End Set
    End Property
#End Region
End Class
