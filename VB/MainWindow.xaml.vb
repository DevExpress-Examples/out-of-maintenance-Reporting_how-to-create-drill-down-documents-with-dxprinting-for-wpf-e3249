Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Printing
Imports DevExpress.Xpf.Core
' ...

Namespace PreviewClickDemo
	Public Class CategoryWrapper
		Private privateIsExpanded As Boolean
		Public Property IsExpanded() As Boolean
			Get
				Return privateIsExpanded
			End Get
			Set(ByVal value As Boolean)
				privateIsExpanded = value
			End Set
		End Property
		Private privateRow As PreviewClickDemo.Northwind.CategoriesRow
		Public Property Row() As PreviewClickDemo.Northwind.CategoriesRow
			Get
				Return privateRow
			End Get
			Private Set(ByVal value As PreviewClickDemo.Northwind.CategoriesRow)
				privateRow = value
			End Set
		End Property

		Public Sub New(ByVal isExpanded As Boolean, ByVal row As PreviewClickDemo.Northwind.CategoriesRow)
            Me.IsExpanded = isExpanded
            Me.Row = row
		End Sub
	End Class

	Partial Public Class MainWindow
		Inherits DXWindow
		Private categoryWrappers As New Dictionary(Of Integer, CategoryWrapper)()
		Private link As SimpleLink = Nothing

		Public Sub New()
			InitializeComponent()
			ThemeManager.ApplicationThemeName = "Office2007Blue"
			AddHandler Loaded, AddressOf MainWindow_Loaded
		End Sub

		Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Populate the data source.
			FillData()

			' Assign a new SimpleLink instance to the link object, 
			' with the specified data template and the number of detail records.
			link = New SimpleLink(CType(Resources("detail"), DataTemplate), categoryWrappers.Count)

			' Then, handle the link's CreateDetail event, where it obtaines its data.
			AddHandler link.CreateDetail, AddressOf link_CreateDetail

			' Create a new instance of the LinkPreviewModel class.
			Dim model As New LinkPreviewModel(link)

			' Handle the PreviewClick and PreviewMouseMove events of the LinkPreviewModel.
			AddHandler model.PreviewClick, AddressOf model_PreviewClick
			AddHandler model.PreviewMouseMove, AddressOf model_PreviewMouseMove

			' Assign the model to the DocumentPreview.
			preview.Model = model

			' Start the document generation.
			link.CreateDocument(True)
		End Sub

		' Provide the drill-down functionality to the PreviewClick event handler.
		Private Sub model_PreviewClick(ByVal sender As Object, ByVal e As PreviewClickEventArgs)
			If e.ElementTag Is Nothing Then
				Return
			End If

			Dim categoryID As Integer = Int32.Parse(e.ElementTag)
			categoryWrappers(categoryID).IsExpanded = Not categoryWrappers(categoryID).IsExpanded
			link.CreateDocument(True)
		End Sub

		' Change the mouse cursor when it hovers the label, 
		' which serves as a link to expand/collapse the detail data.
		Private Sub model_PreviewMouseMove(ByVal sender As Object, ByVal e As PreviewClickEventArgs)
			If e.ElementTag Is Nothing Then
				Return
			End If

			e.Control.Cursor = Cursors.Hand
		End Sub

		Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
			e.Data = categoryWrappers(DataSource.Categories(e.DetailIndex).CategoryID)
		End Sub

		Private Sub FillData()
			For Each category In DataSource.Categories
				categoryWrappers.Add(category.CategoryID, New CategoryWrapper(False, category))
			Next category
		End Sub

	End Class
End Namespace
