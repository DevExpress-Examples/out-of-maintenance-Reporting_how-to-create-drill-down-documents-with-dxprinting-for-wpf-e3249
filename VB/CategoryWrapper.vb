Namespace PreviewClickDemo
    Public Class CategoryWrapper
        Public Property IsExpanded() As Boolean
        Private privateRow As Northwind.CategoriesRow
        Public Property Row() As Northwind.CategoriesRow
            Get
                Return privateRow
            End Get
            Private Set(ByVal value As Northwind.CategoriesRow)
                privateRow = value
            End Set
        End Property

        Public Sub New(ByVal isExpanded As Boolean, ByVal row As Northwind.CategoriesRow)
            Me.IsExpanded = isExpanded
            Me.Row = row
        End Sub
    End Class
End Namespace
