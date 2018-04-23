Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Xpf.Printing
' ...

Namespace PreviewClickDemo
    Public Class MainWindowViewModel
        Private ReadOnly categories As Northwind.CategoriesDataTable
        Private ReadOnly expandedCategoris As IList(Of Northwind.CategoriesRow) = New List(Of Northwind.CategoriesRow)()
        Private ReadOnly link As SimpleLink

        Private privateDocumentPreviewModel As LinkPreviewModel
        Public Property DocumentPreviewModel() As LinkPreviewModel
            Get
                Return privateDocumentPreviewModel
            End Get
            Private Set(ByVal value As LinkPreviewModel)
                privateDocumentPreviewModel = value
            End Set
        End Property

        Public Sub New(ByVal detailDataTemplate As DataTemplate)
            ' Populate the data source.
            Dim nwind = DataSource.CreateNorthwindData()
            categories = nwind.Categories

            ' Assign a new SimpleLink instance to the link object, 
            ' with the specified data template and the number of detail records.
            link = New SimpleLink(detailDataTemplate, categories.Count)

            ' Then, handle the link's CreateDetail event, where it obtaines its data.
            AddHandler link.CreateDetail, AddressOf link_CreateDetail

            ' Create a new instance of the LinkPreviewModel class.
            DocumentPreviewModel = New LinkPreviewModel(link)

            ' Handle the PreviewClick and PreviewMouseMove events of the LinkPreviewModel.
            AddHandler DocumentPreviewModel.PreviewClick, AddressOf model_PreviewClick
            AddHandler DocumentPreviewModel.PreviewMouseMove, AddressOf model_PreviewMouseMove

            ' Start the document generation.
            link.CreateDocument(True)
        End Sub

        ' Provide the drill-down functionality to the PreviewClick event handler.
        Private Sub model_PreviewClick(ByVal sender As Object, ByVal e As PreviewClickEventArgs)
            If e.ElementTag Is Nothing Then
                Return
            End If
            Dim categoryID = Integer.Parse(e.ElementTag)
            Dim category = categories.FindByCategoryID(categoryID)

            If expandedCategoris.Contains(category) Then
                expandedCategoris.Remove(category)
            Else
                expandedCategoris.Add(category)
            End If

            link.CreateDocument(True)
        End Sub

        ' Change the mouse cursor when it hovers the label, 
        ' which serves as a link to expand/collapse the detail data.
        Private Sub model_PreviewMouseMove(ByVal sender As Object, ByVal e As PreviewClickEventArgs)
            If e.ElementTag Is Nothing Then
                Return
            End If
            e.Element.Cursor = Cursors.Hand
        End Sub

        Private Sub link_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            Dim category = categories(e.DetailIndex)
            e.Data = New CategoryWrapper(expandedCategoris.Contains(category), category)
        End Sub
    End Class
End Namespace
