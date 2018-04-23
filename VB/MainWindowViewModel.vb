#Region "reference"
Imports System.Collections.Generic
Imports System.Windows
Imports System.Windows.Input
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Printing
#End Region ' reference

Namespace PreviewClickDemo
    #Region "code"
    <POCOViewModel> _
    Public Class MainWindowViewModel
        Private ReadOnly categories As Northwind.CategoriesDataTable
        Private ReadOnly expandedCategories As IList(Of Northwind.CategoriesRow) = New List(Of Northwind.CategoriesRow)()

        Private privateLink As SimpleLink
        Public Property Link() As SimpleLink
            Get
                Return privateLink
            End Get
            Private Set(ByVal value As SimpleLink)
                privateLink = value
            End Set
        End Property

        Public Shared Function Create(ByVal detailDataTemplate As DataTemplate) As MainWindowViewModel
            Return ViewModelSource.Create(Function() New MainWindowViewModel(detailDataTemplate))
        End Function

        Protected Sub New(ByVal detailDataTemplate As DataTemplate)
            ' Populate the data source.
            Dim nwind = DataSource.CreateNorthwindData()
            categories = nwind.Categories

            ' Assign a new SimpleLink instance to the link object, 
            ' with the specified data template and the number of detail records.
            Link = New SimpleLink(detailDataTemplate, categories.Count)

            ' Handle the link's CreateDetail event, where it obtains its data.
            AddHandler Link.CreateDetail, AddressOf OnLinkCreateDetail
        End Sub

        Private Sub OnLinkCreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            Dim category = categories(e.DetailIndex)
            e.Data = New CategoryWrapper(expandedCategories.Contains(category), category)
        End Sub

        ' Provide the drill-down functionality in the OnPreviewMouseClick event handler.
        Public Sub OnPreviewMouseClick(ByVal args As DocumentPreviewMouseEventArgs)
            If String.IsNullOrEmpty(CStr(args.ElementTag)) Then
                Return
            End If
            Dim categoryID = Integer.Parse(args.ElementTag.ToString())
            Dim category = categories.FindByCategoryID(categoryID)

            If expandedCategories.Contains(category) Then
                expandedCategories.Remove(category)
            Else
                expandedCategories.Add(category)
            End If
            Link.CreateDocument(True)
        End Sub

        ' Change the mouse cursor when it hovers the label 
        ' that serves as a link to expand/collapse the detail data.
        Public Sub OnPreviewMouseMove(ByVal args As DocumentPreviewMouseEventArgs)
            If String.IsNullOrEmpty(CStr(args.ElementTag)) Then
                Return
            End If
            Mouse.SetCursor(Cursors.Hand)
        End Sub
    End Class
    #End Region ' code
End Namespace
