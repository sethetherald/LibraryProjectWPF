using LibraryProjectWPF.Ultilities;
using System;
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

        public MainWindowViewModel()
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
                (_) => LendBookManager()
                );

            ReturnBookManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReturnBookManager()
                );

            AccountManagerCommand = new RelayCommand<object>(
                (_) => true,
                (_) => AccountManager()
                );
        }

        private void BookManager()
        {
            MessageBox.Show("Show book manager");
        }

        private void ReaderManager()
        {
            MessageBox.Show("Show reader manager");
        }

        private void LendBookManager()
        {
            MessageBox.Show("Show lend book manager");
        }

        private void ReturnBookManager()
        {
            MessageBox.Show("Show return book manager");
        }

        private void AccountManager()
        {
            MessageBox.Show("Show account manager");
        }
    }
}