using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Core;
// ...

namespace PreviewClickDemo {
    public class CategoryWrapper {
        public bool IsExpanded { get; set; }
        public PreviewClickDemo.Northwind.CategoriesRow Row { get; private set; }

        public CategoryWrapper(bool isExpanded, PreviewClickDemo.Northwind.CategoriesRow row) {
            IsExpanded = isExpanded;
            Row = row;
        }
    }

    public partial class MainWindow : DXWindow {
        Dictionary<int, CategoryWrapper> categoryWrappers = new Dictionary<int, CategoryWrapper>();
        SimpleLink link = null;

        public MainWindow() {
            InitializeComponent();
            ThemeManager.ApplicationThemeName = "Office2007Blue";
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            // Populate the data source.
            FillData();

            // Assign a new SimpleLink instance to the link object, 
            // with the specified data template and the number of detail records.
            link = new SimpleLink((DataTemplate)Resources["detail"], categoryWrappers.Count);

            // Then, handle the link's CreateDetail event, where it obtaines its data.
            link.CreateDetail += link_CreateDetail;

            // Create a new instance of the LinkPreviewModel class.
            LinkPreviewModel model = new LinkPreviewModel(link);

            // Handle the PreviewClick and PreviewMouseMove events of the LinkPreviewModel.
            model.PreviewClick += model_PreviewClick;
            model.PreviewMouseMove += model_PreviewMouseMove;

            // Assign the model to the DocumentPreview.
            preview.Model = model;

            // Start the document generation.
            link.CreateDocument(true);
        }

        // Provide the drill-down functionality to the PreviewClick event handler.
        void model_PreviewClick(object sender, PreviewClickEventArgs e) {
            if (e.ElementTag == null)
                return;

            int categoryID = Int32.Parse(e.ElementTag);
            categoryWrappers[categoryID].IsExpanded = !categoryWrappers[categoryID].IsExpanded;
            link.CreateDocument(true);
        }

        // Change the mouse cursor when it hovers the label, 
        // which serves as a link to expand/collapse the detail data.
        void model_PreviewMouseMove(object sender, PreviewClickEventArgs e) {
            if (e.ElementTag == null)
                return;

            e.Control.Cursor = Cursors.Hand;
        }

        void link_CreateDetail(object sender, CreateAreaEventArgs e) {
            e.Data = categoryWrappers[DataSource.Categories[e.DetailIndex].CategoryID];
        }

        void FillData() {
            foreach (var category in DataSource.Categories) {
                categoryWrappers.Add(category.CategoryID, new CategoryWrapper(false, category));
            }
        }

    }
}
