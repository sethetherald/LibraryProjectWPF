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
        public MainWindow(Account account, Librarian librarian)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(account, librarian);
        }
    }
}