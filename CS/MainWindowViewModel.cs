using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Printing;
// ...

namespace PreviewClickDemo {
    public class MainWindowViewModel {
        readonly Northwind.CategoriesDataTable categories;
        readonly IList<Northwind.CategoriesRow> expandedCategoris = new List<Northwind.CategoriesRow>();
        readonly SimpleLink link;

        public LinkPreviewModel DocumentPreviewModel { get; private set; }

        public MainWindowViewModel(DataTemplate detailDataTemplate) {
            // Populate the data source.
            var nwind = DataSource.CreateNorthwindData();
            categories = nwind.Categories;

            // Assign a new SimpleLink instance to the link object, 
            // with the specified data template and the number of detail records.
            link = new SimpleLink(detailDataTemplate, categories.Count);

            // Then, handle the link's CreateDetail event, where it obtaines its data.
            link.CreateDetail += link_CreateDetail;

            // Create a new instance of the LinkPreviewModel class.
            DocumentPreviewModel = new LinkPreviewModel(link);

            // Handle the PreviewClick and PreviewMouseMove events of the LinkPreviewModel.
            DocumentPreviewModel.PreviewClick += model_PreviewClick;
            DocumentPreviewModel.PreviewMouseMove += model_PreviewMouseMove;

            // Start the document generation.
            link.CreateDocument(true);
        }

        // Provide the drill-down functionality to the PreviewClick event handler.
        void model_PreviewClick(object sender, PreviewClickEventArgs e) {
            if(e.ElementTag == null) {
                return;
            }
            var categoryID = int.Parse(e.ElementTag);
            var category = categories.FindByCategoryID(categoryID);

            if(expandedCategoris.Contains(category)) {
                expandedCategoris.Remove(category);
            } else {
                expandedCategoris.Add(category);
            }

            link.CreateDocument(true);
        }

        // Change the mouse cursor when it hovers the label, 
        // which serves as a link to expand/collapse the detail data.
        void model_PreviewMouseMove(object sender, PreviewClickEventArgs e) {
            if(e.ElementTag == null) {
                return;
            }
            e.Element.Cursor = Cursors.Hand;
        }

        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            var category = categories[e.DetailIndex];
            e.Data = new CategoryWrapper(expandedCategoris.Contains(category), category);
        }
    }
}
