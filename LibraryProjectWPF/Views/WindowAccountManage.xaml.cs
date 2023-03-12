using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowAccountManage.xaml
    /// </summary>
    public partial class WindowAccountManage : Window
    {
        public WindowAccountManage(int librarianId)
        {
            InitializeComponent();
            DataContext = new AccountManageViewModel(librarianId);
        }
    }
}