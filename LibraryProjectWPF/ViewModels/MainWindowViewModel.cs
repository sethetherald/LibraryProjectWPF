using LibraryProjectWPF.Utilities;
using LibraryProjectWPF.Views;
using LPLibrary.DataAccess.Models;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class MainWindowViewModel
    {
        private int _librarianId;
        public string WelcomeText { get; set; }
        public string ButtonVisibility { get; set; }
        public Account CurrentAccount { get; set; }
        public ICommand BookManagerCommand { get; set; }
        public ICommand ReaderManagerCommand { get; set; }
        public ICommand LendBookManagerCommand { get; set; }
        public ICommand ReturnBookManagerCommand { get; set; }
        public ICommand AccountManagerCommand { get; set; }

        public MainWindowViewModel(Account account, Librarian librarian)
        {
            CurrentAccount = account;
            _librarianId = librarian.LibrarianId;
            WelcomeText = "Welcome " + librarian.LibrarianName;
            ButtonVisibility = CurrentAccount.Role == 1 ? "Visible" : "Hidden";

            BookManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => BookManager()
                );

            ReaderManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReaderManager()
                );

            LendBookManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => LendBookManager(_librarianId)
                );

            ReturnBookManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReturnBookManager(_librarianId)
                );

            AccountManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => AccountManager(_librarianId)
                );
        }

        private void BookManager()
        {
            WindowBookManage bookManage = new();
            bookManage.ShowDialog();
        }

        private void ReaderManager()
        {
            WindowReaderManage readerManage = new();
            readerManage.ShowDialog();
        }

        private void LendBookManager(int librarianId)
        {
            WindowLendBook lendBook = new(librarianId);
            lendBook.ShowDialog();
        }

        private void ReturnBookManager(int librarianId)
        {
            WindowReturnBook returnBook = new(librarianId);
            returnBook.ShowDialog();
        }

        private void AccountManager(int librarianId)
        {
            WindowAccountManage accountManage = new(librarianId);
            accountManage.ShowDialog();
        }
    }
}