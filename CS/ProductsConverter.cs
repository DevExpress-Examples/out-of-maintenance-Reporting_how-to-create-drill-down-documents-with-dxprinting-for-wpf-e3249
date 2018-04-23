using System;
using System.Globalization;
using System.Windows.Data;
// ...

namespace PreviewClickDemo {
    public class ProductsConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var row = (Northwind.CategoriesRow)value;
            return row.GetProductsRows();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
