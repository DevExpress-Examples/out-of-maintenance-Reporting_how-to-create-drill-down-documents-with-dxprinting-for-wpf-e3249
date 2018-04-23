#region reference
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Printing;
#endregion reference

namespace PreviewClickDemo {
    [POCOViewModel]
    #region code
    public class MainWindowViewModel {
        readonly Northwind.CategoriesDataTable categories;
        readonly IList<Northwind.CategoriesRow> expandedCategories = new List<Northwind.CategoriesRow>();

        public SimpleLink Link { get; private set; }

        public static MainWindowViewModel Create(DataTemplate detailDataTemplate) {
            return ViewModelSource.Create(() => new MainWindowViewModel(detailDataTemplate));
        }

        protected MainWindowViewModel(DataTemplate detailDataTemplate) {
            // Populate the data source.
            var nwind = DataSource.CreateNorthwindData();
            categories = nwind.Categories;

            // Assign a new SimpleLink instance to the link object, 
            // with the specified data template and the number of detail records.
            Link = new SimpleLink(detailDataTemplate, categories.Count);

            // Handle the link's CreateDetail event, where it obtains its data.
            Link.CreateDetail += OnLinkCreateDetail;
        }

        void OnLinkCreateDetail(object sender, CreateAreaEventArgs e) {
            var category = categories[e.DetailIndex];
            e.Data = new CategoryWrapper(expandedCategories.Contains(category), category);
        }

        // Provide the drill-down functionality in the OnPreviewMouseClick event handler.
        public void OnPreviewMouseClick(DocumentPreviewMouseEventArgs args) {
            if (string.IsNullOrEmpty((string)args.ElementTag)) {
                return;
            }
            var categoryID = int.Parse(args.ElementTag.ToString());
            var category = categories.FindByCategoryID(categoryID);

            if (expandedCategories.Contains(category)) {
                expandedCategories.Remove(category);
            }
            else {
                expandedCategories.Add(category);
            }
            Link.CreateDocument(true);
        }

        // Change the mouse cursor when it hovers the label 
        // that serves as a link to expand/collapse the detail data.
        public void OnPreviewMouseMove(DocumentPreviewMouseEventArgs args) {
            if (string.IsNullOrEmpty((string)args.ElementTag)) {
                return;
            }
            Mouse.SetCursor(Cursors.Hand);
        }
    }
    #endregion code
}
