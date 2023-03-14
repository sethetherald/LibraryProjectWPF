using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowReaderManage.xaml
    /// </summary>
    public partial class WindowReaderManage : Window
    {
        public WindowReaderManage()
        {
            InitializeComponent();
            DataContext = new ReaderManageViewModel();
        }
    }
}