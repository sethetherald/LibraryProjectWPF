using LibraryProjectWPF.ViewModels;
using LPLibrary.DataAccess.Models;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowReturnBookSub.xaml
    /// </summary>
    public partial class WindowReturnBookSub : Window
    {
        public WindowReturnBookSub(int librarianId, ReturnBookModel returnBook)
        {
            InitializeComponent();
            DataContext = new ReturnBookSubViewModel(this, librarianId, returnBook);
        }
    }
}