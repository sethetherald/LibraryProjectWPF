using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowBookManage.xaml
    /// </summary>
    public partial class WindowBookManage : Window
    {
        public WindowBookManage()
        {
            InitializeComponent();
            DataContext = new BookManageViewModel();
        }
    }
}