using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreviewClickDemo {
    public static class DataSource {
        static PreviewClickDemo.Northwind.CategoriesDataTable categories = new PreviewClickDemo.Northwind.CategoriesDataTable();
        static PreviewClickDemo.Northwind.ProductsDataTable products = new PreviewClickDemo.Northwind.ProductsDataTable();
        
        static DataSource() {
            NorthwindTableAdapters.CategoriesTableAdapter categoriesAdapter = new NorthwindTableAdapters.CategoriesTableAdapter();
            NorthwindTableAdapters.ProductsTableAdapter productsAdapter = new NorthwindTableAdapters.ProductsTableAdapter();
            categoriesAdapter.Fill(categories);
            productsAdapter.Fill(products);
        }
        
        public static PreviewClickDemo.Northwind.CategoriesDataTable Categories {
            get {
                return categories;
            }
        }
        public static PreviewClickDemo.Northwind.ProductsDataTable Products {
            get {
                return products;
            }
        }
    }
}
