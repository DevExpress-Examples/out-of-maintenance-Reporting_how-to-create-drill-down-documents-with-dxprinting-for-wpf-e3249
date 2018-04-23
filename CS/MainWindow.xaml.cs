using System.Windows;
using DevExpress.Xpf.Core;
// ...

namespace PreviewClickDemo {
    public partial class MainWindow : DXWindow {
        public MainWindow() {
            InitializeComponent();
            ThemeManager.ApplicationThemeName = "Office2007Blue";
            DataContext = new MainWindowViewModel((DataTemplate)Resources["detail"]);
        }
    }
}
