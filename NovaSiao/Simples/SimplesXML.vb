Imports System.Xml
Imports System.Reflection
Imports CamadaDTO
'
Public Class SimplesXML
    Private Const filename As String = "sampledata.xml"
    '
    Public Shared Sub WriteObjProperty_XML(o As Object, writer As XmlWriter)
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
                Try
                    WritePropValuesXML(c, writer)
                Catch ex As Exception
                    Throw ex
                End Try
                '
                writer.WriteEndElement()
            Next
            '
            writer.WriteEndElement()
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
    Public Shared Sub WritePropValuesXML(minhaclasse As Object, writer As XmlWriter)
        '
        Try
            '
            For Each p As PropertyInfo In minhaclasse.GetType().GetProperties()
                '--- verify if value is money or date
                Dim myValue As String = ""
                '
                If Not IsNothing(p.GetValue(minhaclasse)) Then
                    Dim T As Type = If(Nullable.GetUnderlyingType(p.PropertyType), p.PropertyType)
                    '
                    If T Is GetType(Date) Then
                        myValue = DirectCast(p.GetValue(minhaclasse), Date).ToShortDateString
                    ElseIf T Is GetType(Decimal) OrElse T Is GetType(Double) Then
                        myValue = Format(p.GetValue(minhaclasse), "#,##0.00").ToString
                        myValue = myValue.Replace(",", ".")
                    Else
                        myValue = p.GetValue(minhaclasse).ToString
                    End If
                    '
                Else
                    myValue = ""
                End If
                '
                writer.WriteElementString(p.Name, myValue)
                '
            Next
            '
        Catch ex As Exception
            Throw ex
        End Try
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
    Public Shared Function CriarSimplesEntradaXML() As clSimplesSaida
        Return New clSimplesSaida
    End Function
    '
End Class

