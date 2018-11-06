Imports System.ComponentModel

Public Class clTroca : Implements IEditableObject
    '
#Region "ESTRUTURA DOS DADOS"
    Structure TrocaDados ' alguns usam FRIEND em vez de DIM
        '
        'tblTroca =====================================================
        Dim _IDTroca As Integer
        Dim _TrocaData As Date
        Dim _IDFilial As Integer
        Dim _IDPessoaTroca As Integer
        Dim _IDVendedor As Integer
        Dim _IDTransacaoEntrada As Integer
        Dim _IDTransacaoSaida As Integer
        Dim _CobrancaTipo As Byte 'tinyint
        Dim _ValorEntrada As Decimal '--- Valor dos PRODUTOS que entraram
        Dim _ValorSaida As Decimal '--- Valor dos PRODUTOS a que sairam
        Dim _ValorAcrescimos As Decimal '--- Valor dos acrescimos caso houver
        Dim _TotalTroca As Decimal '--- Valor final da TROCA (Saídas - Entradas)
        Dim _JurosMes As Decimal '--- VAlor do Juros aplicado caso parcelado
        Dim _Observacao As String
        Dim _IDSituacao As Byte 'tinyint
        '
        'tblAReceber =====================================================
        Dim _IDAReceber As Integer?
        Dim _ValorPagoTotal As Decimal
        Dim _SituacaoAReceber As Byte '--- 0:EmAberto | 1:Pago | 2:Cancelada
        Dim _IDCobrancaForma As Int16? 'smallint
        Dim _IDPlano As Int16?
        '
        '--- PROPRIEDADES PARA LEITURA
        ' PessoaTroca As String
        ' ApelidoFilial As String
        ' ApelidoVenda As String
        ' CobrancaTipoTexto As String
        ' Situacao As String
        ' CobrancaForma As String
        '
    End Structure
#End Region
    '
#Region "PRIVATE VARIABLES"
    Private TData As TrocaDados
    Private backupData As TrocaDados
    Private inTxn As Boolean = False
#End Region
    '
#Region "IMPLEMENTS EVENTS"
    Public Sub New()
        TData = New TrocaDados()
        With TData
            ._IDTroca = Nothing
            ._IDVendedor = Nothing
            ._TrocaData = DateTime.Today
            ._CobrancaTipo = 0 'tinyint
            ._IDSituacao = 0
            ._ValorEntrada = 0
            ._ValorSaida = 0
            ._ValorAcrescimos = 0
            ._TotalTroca = 0
            ._JurosMes = 0
        End With
    End Sub
    '
    '-- IMPLEMENTS IEditableObject
    Public Sub BeginEdit() Implements IEditableObject.BeginEdit
        If Not inTxn Then
            Me.backupData = TData
            inTxn = True
        End If
    End Sub
    '
    Public Sub CancelEdit() Implements IEditableObject.CancelEdit
        If inTxn Then
            Me.TData = backupData
            inTxn = False
        End If
    End Sub
    '
    Public Sub EndEdit() Implements IEditableObject.EndEdit
        If inTxn Then
            backupData = New TrocaDados()
            inTxn = False
        End If
    End Sub
    '
    '-- _EVENTO PUBLIC AOALTERAR
    Public Event AoAlterar()
    '
    Public Overrides Function ToString() As String
        Return IDTroca.ToString
    End Function
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
    '===========================================================================================
    '--- TBLTROCA
    '===========================================================================================
    '
    Property IDTroca() As Integer
        Get
            Return TData._IDTroca
        End Get
        Set(value As Integer)
            TData._IDTroca = value
        End Set
    End Property
    '
    '--- Propriedade TrocaData
    '------------------------------------------------------
    Public Property TrocaData() As Date
        Get
            Return TData._TrocaData
        End Get
        Set(ByVal value As Date)
            If value <> TData._TrocaData Then
                RaiseEvent AoAlterar()
            End If
            TData._TrocaData = value
        End Set
    End Property
    '
    '--- Propriedade IDFilial
    '------------------------------------------------------
    Public Property IDFilial() As Integer
        Get
            Return TData._IDFilial
        End Get
        Set(ByVal value As Integer)
            If value <> TData._IDFilial Then
                RaiseEvent AoAlterar()
            End If
            TData._IDFilial = value
        End Set
    End Property
    '
    '--- Propriedade IDPessoaTroca
    '------------------------------------------------------
    Public Property IDPessoaTroca() As Integer
        Get
            Return TData._IDPessoaTroca
        End Get
        Set(ByVal value As Integer)
            If value <> TData._IDPessoaTroca Then
                RaiseEvent AoAlterar()
            End If
            TData._IDPessoaTroca = value
        End Set
    End Property
    '
    '--- Propriedade IDVendedor
    '------------------------------------------------------
    Public Property IDVendedor() As Integer
        Get
            Return TData._IDVendedor
        End Get
        Set(ByVal value As Integer)
            If value <> TData._IDVendedor Then
                RaiseEvent AoAlterar()
            End If
            TData._IDVendedor = value
        End Set
    End Property
    '
    '--- Propriedade IDTransacaoEntrada
    '------------------------------------------------------
    Public Property IDTransacaoEntrada() As Integer
        Get
            Return TData._IDTransacaoEntrada
        End Get
        Set(ByVal value As Integer)
            If value <> TData._IDTransacaoEntrada Then
                RaiseEvent AoAlterar()
            End If
            TData._IDTransacaoEntrada = value
        End Set
    End Property
    '
    '--- Propriedade IDTransacaoSaida
    '------------------------------------------------------
    Public Property IDTransacaoSaida() As Integer
        Get
            Return TData._IDTransacaoSaida
        End Get
        Set(ByVal value As Integer)
            If value <> TData._IDTransacaoSaida Then
                RaiseEvent AoAlterar()
            End If
            TData._IDTransacaoSaida = value
        End Set
    End Property
    '
    '--- Propriedade CobrancaTipo
    '------------------------------------------------------
    Public Property CobrancaTipo() As Byte
        Get
            Return TData._CobrancaTipo
        End Get
        Set(ByVal value As Byte)
            If value <> TData._CobrancaTipo Then
                RaiseEvent AoAlterar()
            End If
            TData._CobrancaTipo = value
        End Set
    End Property
    '
    '--- Propriedade ValorEntrada
    '------------------------------------------------------
    Public Property ValorEntrada() As Decimal
        Get
            Return TData._ValorEntrada
        End Get
        Set(ByVal value As Decimal)
            If value <> TData._ValorEntrada Then
                RaiseEvent AoAlterar()
            End If
            TData._ValorEntrada = value
        End Set
    End Property
    '
    '--- Propriedade ValorSaida
    '------------------------------------------------------
    Public Property ValorSaida() As Decimal
        Get
            Return TData._ValorSaida
        End Get
        Set(ByVal value As Decimal)
            If value <> TData._ValorSaida Then
                RaiseEvent AoAlterar()
            End If
            TData._ValorSaida = value
        End Set
    End Property
    '
    '--- Propriedade ValorAcrescimos
    '------------------------------------------------------
    Public Property ValorAcrescimos() As Decimal
        Get
            Return TData._ValorAcrescimos
        End Get
        Set(ByVal value As Decimal)
            If value <> TData._ValorAcrescimos Then
                RaiseEvent AoAlterar()
            End If
            TData._ValorAcrescimos = value
        End Set
    End Property
    '
    '--- Propriedade TotalTroca
    '------------------------------------------------------
    Public ReadOnly Property TotalTroca() As Decimal
        Get
            Return (ValorSaida + ValorAcrescimos - ValorEntrada)
        End Get
        'Set(ByVal value As Decimal)
        '    TData._TotalTroca = value
        'End Set
    End Property
    '
    '--- Propriedade JurosMes
    '------------------------------------------------------
    Public Property JurosMes() As Decimal
        Get
            Return TData._JurosMes
        End Get
        Set(ByVal value As Decimal)
            If value <> TData._JurosMes Then
                RaiseEvent AoAlterar()
            End If
            TData._JurosMes = value
        End Set
    End Property
    '
    '--- Propriedade Observacao
    '------------------------------------------------------
    Public Property Observacao() As String
        Get
            Return TData._Observacao
        End Get
        Set(ByVal value As String)
            If value <> TData._Observacao Then
                RaiseEvent AoAlterar()
            End If
            TData._Observacao = value
        End Set
    End Property
    '
    '--- Propriedade IDSituacao
    '------------------------------------------------------
    Public Property IDSituacao() As Byte
        Get
            Return TData._IDSituacao
        End Get
        Set(ByVal value As Byte)
            If value <> TData._IDSituacao Then
                RaiseEvent AoAlterar()
            End If
            TData._IDSituacao = value
        End Set
    End Property
    '
    '===========================================================================================
    '--- TBLARECEBER
    '===========================================================================================
    '
    '--- Propriedade IDAReceber
    Public Property IDAReceber() As Integer?
        Get
            Return TData._IDAReceber
        End Get
        Set(ByVal value As Integer?)
            If value <> TData._IDAReceber Then
                RaiseEvent AoAlterar()
            End If
            TData._IDAReceber = value
        End Set
    End Property
    '
    '--- Propriedade SituacaoAReceber
    Public Property SituacaoAReceber() As Byte
        Get
            Return TData._SituacaoAReceber
        End Get
        Set(ByVal value As Byte)
            If value <> TData._SituacaoAReceber Then
                RaiseEvent AoAlterar()
            End If
            TData._SituacaoAReceber = value
        End Set
    End Property
    '
    '--- Propriedade ValorPagoTotal
    Public Property ValorPagoTotal() As Decimal
        Get
            Return TData._ValorPagoTotal
        End Get
        Set(ByVal value As Decimal)
            If value <> TData._ValorPagoTotal Then
                RaiseEvent AoAlterar()
            End If
            TData._ValorPagoTotal = value
        End Set
    End Property
    '
    '--- Propriedade IDCobrancaForma
    Public Property IDCobrancaForma() As Int16?
        Get
            Return TData._IDCobrancaForma
        End Get
        Set(ByVal value As Int16?)
            If value <> TData._IDCobrancaForma Then
                RaiseEvent AoAlterar()
            End If
            TData._IDCobrancaForma = value
        End Set
    End Property
    '
    '--- Propriedade IDPlano
    Public Property IDPlano() As Int16?
        Get
            Return TData._IDPlano
        End Get
        Set(ByVal value As Int16?)
            If value <> TData._IDPlano Then
                RaiseEvent AoAlterar()
            End If
            TData._IDPlano = value
        End Set
    End Property
    '
    '===========================================================================================
    '--- PROPRIEDADES PARA LEITURA
    '===========================================================================================
    '
    Public Property PessoaTroca As String
    Public Property ApelidoFilial As String
    Public Property ApelidoVenda As String
    Public Property CobrancaTipoTexto As String
    Public Property Situacao As String
    Public Property CobrancaForma As String
    '
#End Region
    '
End Class
