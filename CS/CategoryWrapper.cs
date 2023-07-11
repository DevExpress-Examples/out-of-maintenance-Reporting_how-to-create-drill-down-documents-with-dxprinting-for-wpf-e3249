namespace PreviewClickDemo {
    public class CategoryWrapper {
        public bool IsExpanded { get; set; }
        public Northwind.CategoriesRow Row { get; private set; }

        public CategoryWrapper(bool isExpanded, Northwind.CategoriesRow row) {
            IsExpanded = isExpanded;
            Row = row;
        }
    }
}
