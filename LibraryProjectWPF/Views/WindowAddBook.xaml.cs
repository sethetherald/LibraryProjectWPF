using LibraryProjectWPF.ViewModels;
using System.Windows;

namespace LibraryProjectWPF.Views
{
    /// <summary>
    /// Interaction logic for WindowAddBook.xaml
    /// </summary>
    public partial class WindowAddBook : Window
    {
        public WindowAddBook(int titleId)
        {
            InitializeComponent();
            DataContext = new AddBookViewModel(this, titleId);
        }
    }
}