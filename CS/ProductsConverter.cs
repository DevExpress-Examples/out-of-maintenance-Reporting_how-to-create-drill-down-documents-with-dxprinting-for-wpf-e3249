using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PreviewClickDemo {
    public class ProductsConverter : IValueConverter {

        IEnumerable<PreviewClickDemo.Northwind.ProductsRow> SelectProducts(int categoryId) {
            List<PreviewClickDemo.Northwind.ProductsRow> products = new List<Northwind.ProductsRow>();
            foreach (PreviewClickDemo.Northwind.ProductsRow product in DataSource.Products) {
                if (product.CategoryID != categoryId)
                    continue;
                products.Add(product);
            }
            return products;
        }

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            int categoryId = (int)value;
            return SelectProducts(categoryId);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
