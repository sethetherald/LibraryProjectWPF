using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowTitleManage.xaml
    /// </summary>
    public partial class WindowTitleManage : Window
    {
        public WindowTitleManage()
        {
            InitializeComponent();
            DataContext = new TitleManageViewModel(this);
        }

        public WindowTitleManage(int titleId)
        {
            InitializeComponent();
            DataContext = new TitleManageViewModel(this, titleId);
        }
    }
}