using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowLendBook.xaml
    /// </summary>
    public partial class WindowLendBook : Window
    {
        public WindowLendBook(int librarianId)
        {
            InitializeComponent();
            DataContext = new LendBookViewModel(librarianId);
        }
    }
}