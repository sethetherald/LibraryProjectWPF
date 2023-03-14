using LibraryProjectWPF.Utilities;
using LibraryProjectWPF.Views;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class MainWindowViewModel
    {
        public ICommand BookManagerCommand { get; set; }
        public ICommand ReaderManagerCommand { get; set; }
        public ICommand LendBookManagerCommand { get; set; }
        public ICommand ReturnBookManagerCommand { get; set; }
        public ICommand AccountManagerCommand { get; set; }

        public MainWindowViewModel(int librarianId)
        {
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
                (_) => LendBookManager(librarianId)
                );

            ReturnBookManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReturnBookManager(librarianId)
                );

            AccountManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => AccountManager(librarianId)
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