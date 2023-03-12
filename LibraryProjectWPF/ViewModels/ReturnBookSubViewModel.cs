using LibraryProjectWPF.Ultilities;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class ReturnBookSubViewModel
    {
        #region Commands

        public ICommand ReturnBookCommand { get; set; }

        #endregion Commands

        #region Fields

        private readonly int _librarianId;
        private ReturnBookModel _returnBook;
        private IReturnBookRespository _returnBookRespository = new ReturnBookRespository();
        public Window ThisWindow { get; set; }
        public string TicketId { get; set; }
        public string ReaderCard { get; set; }
        public string LibrarianId { get; set; }
        public string ExpectedReturnDate { get; set; }
        public string ReaderName { get; set; }
        public string BookId { get; set; }
        public string Due { get; set; }
        public ComboBoxModel[] Condition { get; set; }
        public ComboBoxModel CurrentCondition { get; set; }

        #endregion Fields

        public ReturnBookSubViewModel(Window thisWindow, int librarianId, ReturnBookModel returnBook)
        {
            _librarianId = librarianId;
            _returnBook = returnBook;
            ThisWindow = thisWindow;
            InitializeFields();

            ReturnBookCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReturnBook());
        }

        private void InitializeFields()
        {
            Condition = new ComboBoxModel[]{
                new ComboBoxModel(){Display="Good", Value=1},
                new ComboBoxModel(){Display="Damaged", Value=2},
                new ComboBoxModel(){Display="Lost", Value=3},
            };
            int dueDays = (DateTime.Now - _returnBook.ExpectedReturnDate).Days;
            dueDays = dueDays < 0 ? 0 : dueDays;
            TicketId = _returnBook.TicketId.ToString();
            ReaderCard = _returnBook.ReaderCard.ToString();
            LibrarianId = _librarianId.ToString();
            ExpectedReturnDate = _returnBook.ExpectedReturnDate.ToString("dd/MM/yyyy");
            ReaderName = _returnBook.ReaderName;
            BookId = _returnBook.BookId.ToString();
            Due = dueDays + " day(s)";
        }

        private void ReturnBook()
        {
            if (CurrentCondition != null)
            {
                var result = MessageBox.Show("Submit this return book request?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _returnBookRespository.ReturnBook(_returnBook, _librarianId, CurrentCondition.Value);
                    MessageBox.Show("Finished!");
                    ThisWindow.Close();
                }
                else { return; }
            }
        }
    }
}