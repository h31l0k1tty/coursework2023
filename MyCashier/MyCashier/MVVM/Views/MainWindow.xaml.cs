using MyCashier.MVVM.ViewModels;
using System.Windows;

namespace MyCashier
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
    }
}
