using System.Windows;
using DevExpress.Xpf.Core;
// ...

namespace PreviewClickDemo {
    public partial class MainWindow : DXWindow {
        public MainWindow() {
            InitializeComponent();
            DataContext = MainWindowViewModel.Create((DataTemplate)Resources["detail"]);
        }

    }
}
