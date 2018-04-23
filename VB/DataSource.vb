Imports PreviewClickDemo.NorthwindTableAdapters
' ...

Namespace PreviewClickDemo
    Public NotInheritable Class DataSource

        Private Sub New()
        End Sub

        Public Shared Function CreateNorthwindData() As Northwind

            Dim northwind_Renamed = New Northwind()
            Using categoriesAdapter = New CategoriesTableAdapter()
            Using productsAdapter = New ProductsTableAdapter()
                categoriesAdapter.Fill(northwind_Renamed.Categories)
                productsAdapter.Fill(northwind_Renamed.Products)
            End Using
            End Using
            Return northwind_Renamed
        End Function
    End Class
End Namespace
