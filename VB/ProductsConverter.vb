Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data

Namespace PreviewClickDemo
    Public Class ProductsConverter
        Implements IValueConverter

        Private Function SelectProducts(ByVal categoryId As Integer) As IEnumerable(Of PreviewClickDemo.Northwind.ProductsRow)
            Dim products As New List(Of PreviewClickDemo.Northwind.ProductsRow)()
            For Each product As PreviewClickDemo.Northwind.ProductsRow In DataSource.Products
                If product.CategoryID <> categoryId Then
                    Continue For
                End If
                products.Add(product)
            Next product
            Return products
        End Function

#Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
            Dim categoryId As Integer = CInt(Fix(value))
            Return SelectProducts(categoryId)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function

#End Region
    End Class
End Namespace
