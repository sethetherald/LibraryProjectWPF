using LibraryProjectWPF.ViewModels;
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
            DataContext = new MainWindowViewModel();
            CurrentAccount = account;
            tbWelcome.Text = "Hello " + librarian.LibrarianName;
            if (CurrentAccount.Role == 0) btnAccount.Visibility = Visibility.Hidden;
        }
    }
}