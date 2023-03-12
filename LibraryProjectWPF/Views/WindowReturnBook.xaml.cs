using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowReturnBook.xaml
    /// </summary>
    public partial class WindowReturnBook : Window
    {
        public WindowReturnBook(int librarianId)
        {
            InitializeComponent();
            DataContext = new ReturnBookViewModel(librarianId);
        }
    }
}