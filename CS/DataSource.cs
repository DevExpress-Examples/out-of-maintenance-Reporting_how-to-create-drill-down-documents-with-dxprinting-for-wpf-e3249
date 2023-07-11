using PreviewClickDemo.NorthwindTableAdapters;
// ...

namespace PreviewClickDemo {
    public static class DataSource {
        public static Northwind CreateNorthwindData() {
            var northwind = new Northwind();
            using(var categoriesAdapter = new CategoriesTableAdapter())
            using(var productsAdapter = new ProductsTableAdapter()) {
                categoriesAdapter.Fill(northwind.Categories);
                productsAdapter.Fill(northwind.Products);
            }
            return northwind;
        }
    }
}
