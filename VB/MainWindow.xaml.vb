Imports System.Windows
Imports DevExpress.Xpf.Core
' ...

Namespace PreviewClickDemo
    Partial Public Class MainWindow
        Inherits DXWindow

        Public Sub New()
            InitializeComponent()
            ThemeManager.ApplicationThemeName = "Office2007Blue"
            DataContext = New MainWindowViewModel(CType(Resources("detail"), DataTemplate))
        End Sub
    End Class
End Namespace
