using LibraryProjectWPF.Utilities;
using LibraryProjectWPF.Views;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class ReturnBookViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementation

        #region Commands

        public ICommand SetLendBookSearchModeCommand { get; set; }
        public ICommand LendBookSearchCommand { get; set; }
        public ICommand ReturnBookCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        #endregion Commands

        #region Fields

        private readonly int _librarianId;
        private ILendBookRespository _lendBookRespository = new LendBookRespository();
        private IReaderRespository _readerRespository = new ReaderRespository();
        private ObservableCollection<ReturnBookModel> _returnBooks;

        public ObservableCollection<ReturnBookModel> ReturnBooks
        {
            get { return _returnBooks; }
            set { _returnBooks = value; OnPropertyChanged(nameof(ReturnBooks)); }
        }

        public ReturnBookModel CurrentlySelectedReturn
        {
            get;
            set;
        }

        public string LendBookSearchMode { get; set; }
        public string LendBookSearchString { get; set; }

        #endregion Fields

        public ReturnBookViewModel(int librarianId)
        {
            _librarianId = librarianId;
            SetLendBookSearchMode("Reader");
            InitializeLendBooks();

            SetLendBookSearchModeCommand = new RelayCommand<string>(
                (mode) => true,
                (mode) => SetLendBookSearchMode(mode));

            LendBookSearchCommand = new RelayCommand<object>(
                (_) => true,
                (_) => SearchLendBook());

            ReturnBookCommand = new RelayCommand<object>(
                (_) => true,
                (_) => ReturnBook());

            RefreshCommand = new RelayCommand<object>(
                (_) => true,
                (_) => InitializeLendBooks());
        }

        private void InitializeLendBooks()
        {
            List<LendBookDetail> tempLendDetails = _lendBookRespository.GetLendBookDetails();
            List<Reader> tempReaders = _readerRespository.GetReaders();

            List<ReturnBookModel> temp = (from ld in tempLendDetails
                                          join
                                              r in tempReaders on ld.CardNumber equals r.CardNumber
                                          where ld.Books.Count > 0
                                          select new ReturnBookModel()
                                          {
                                              BookId = ld.Books.FirstOrDefault()!.BookId,
                                              TicketId = ld.LendBookDetailId,
                                              ReaderCard = ld.CardNumber,
                                              ReaderName = r.FullName,
                                              LibrarianId = ld.LibrarianId,
                                              LendDate = ld.LendDate,
                                              ExpectedReturnDate = ld.ExpectedReturnDate,
                                              LendCondition = ld.ReturnCondition switch
                                              {
                                                  1 => "Good",
                                                  2 => "Damaged",
                                                  _ => "No Data"
                                              },
                                          }).ToList();
            Application.Current.Dispatcher.Invoke(() =>
                ReturnBooks = new ObservableCollection<ReturnBookModel>(temp)
            );
        }

        private void SetLendBookSearchMode(string parameter) => LendBookSearchMode = parameter;

        private void SearchLendBook()
        {
            InitializeLendBooks();
            switch (LendBookSearchMode)
            {
                case "Reader":
                    if (!LendBookSearchString.IsNullOrEmpty())
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                            ReturnBooks = new ObservableCollection<ReturnBookModel>(
                                ReturnBooks.Where(x => x.ReaderName.Contains(LendBookSearchString, StringComparison.OrdinalIgnoreCase))
                                )
                        );
                    }
                    break;

                case "Ticket":
                    if (!LendBookSearchString.IsNullOrEmpty() || int.TryParse(LendBookSearchString, out int value))
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                            ReturnBooks = new ObservableCollection<ReturnBookModel>(
                                ReturnBooks.Where(x => x.TicketId == int.Parse(LendBookSearchString)))
                        );
                    }
                    break;
            }
        }

        private void ReturnBook()
        {
            if (CurrentlySelectedReturn != null)
            {
                WindowReturnBookSub sub = new(_librarianId, CurrentlySelectedReturn);
                sub.ShowDialog();
            }
        }
    }
}