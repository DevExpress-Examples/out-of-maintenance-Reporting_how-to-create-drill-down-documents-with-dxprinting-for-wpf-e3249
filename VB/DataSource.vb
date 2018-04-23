Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace PreviewClickDemo
	Public NotInheritable Class DataSource
		Private Shared categories_Renamed As New PreviewClickDemo.Northwind.CategoriesDataTable()
		Private Shared products_Renamed As New PreviewClickDemo.Northwind.ProductsDataTable()

		Private Sub New()
		End Sub
		Shared Sub New()
			Dim categoriesAdapter As New NorthwindTableAdapters.CategoriesTableAdapter()
			Dim productsAdapter As New NorthwindTableAdapters.ProductsTableAdapter()
			categoriesAdapter.Fill(categories_Renamed)
			productsAdapter.Fill(products_Renamed)
		End Sub

		Public Shared ReadOnly Property Categories() As PreviewClickDemo.Northwind.CategoriesDataTable
			Get
				Return categories_Renamed
			End Get
		End Property
		Public Shared ReadOnly Property Products() As PreviewClickDemo.Northwind.ProductsDataTable
			Get
				Return products_Renamed
			End Get
		End Property
	End Class
End Namespace
