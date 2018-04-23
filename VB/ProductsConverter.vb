Imports System
Imports System.Globalization
Imports System.Windows.Data
' ...

Namespace PreviewClickDemo
    Public Class ProductsConverter
        Implements IValueConverter

        #Region "IValueConverter Members"
        Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim row = DirectCast(value, Northwind.CategoriesRow)
            Return row.GetProductsRows()
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException()
        End Function
        #End Region
    End Class
End Namespace
