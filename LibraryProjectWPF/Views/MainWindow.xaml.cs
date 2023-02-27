using LPLibrary.DataAccess.Models;
using System.Windows;

namespace LibraryProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Account CurrentAccount { get; set; }

        public MainWindow(Account account, Librarian librarian)
        {
            InitializeComponent();
            CurrentAccount = account;
            tbTest.Text = "Hello " + librarian.LibrarianName;
        }
    }
}