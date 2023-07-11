Imports System.Windows
Imports DevExpress.Xpf.Core
' ...

Namespace PreviewClickDemo
    Partial Public Class MainWindow
        Inherits DXWindow

        Public Sub New()
            InitializeComponent()
            DataContext = MainWindowViewModel.Create(CType(Resources("detail"), DataTemplate))
        End Sub

    End Class
End Namespace
