using LibraryProjectWPF.Utilities;
using LibraryProjectWPF.Utilities.Converter;
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
    internal class LendBookViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged Implementation

        #region Commands

        public ICommand CreateTicketCommand { get; set; }
        public ICommand SetReaderSearchModeCommand { get; set; }
        public ICommand SearchReaderCommand { get; set; }
        public ICommand SetBookSearchModeCommand { get; set; }
        public ICommand SearchBookCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        #endregion Commands

        #region Fields

        private readonly int _librarianId;
        private IReaderRespository _readerRespository = new ReaderRespository();
        private IBookRespository _bookRespository = new BookRespository();
        private IBookInfoRespository _bookInfoRespository = new BookInfoRespository();
        private IPublisherRespository _publisherRespository = new PublisherRespository();
        private IAuthorRespository _authorRespository = new AuthorRespository();
        private IAuthorBookRespository _authorBookRespository = new AuthorBookRespository();
        private ILendBookRespository _lendBookRespository = new LendBookRespository();

        public string ReaderSearchMode { get; set; }
        public string BookSearchMode { get; set; }
        public string ReaderSearchString { get; set; }
        public string BookSearchString { get; set; }

        private ObservableCollection<BookManagementModel> _books;
        private ObservableCollection<Reader> _readers;

        public ObservableCollection<BookManagementModel> Books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged(nameof(Books)); }
        }

        public ObservableCollection<Reader> Readers
        {
            get { return _readers; }
            set { _readers = value; OnPropertyChanged(nameof(Readers)); }
        }

        #endregion Fields

        public LendBookViewModel(int librarianId)
        {
            _librarianId = librarianId;

            SetReaderSearchMode("Card");
            SetBookSearchMode("Id");

            LoadData();

            CreateTicketCommand = new RelayCommand<LendTicketConverterModel>(
                (ticket) => ticket != null,
                (ticket) => CreateTicket(ticket));

            SearchReaderCommand = new RelayCommand<object>(
                (_) => true,
                (_) => SearchReader());

            SearchBookCommand = new RelayCommand<object>(
                (_) => true,
                (_) => SearchBook());

            RefreshCommand = new RelayCommand<object>(
                (_) => true,
                (_) => LoadData());

            SetReaderSearchModeCommand = new RelayCommand<string>(
                (mode) => !mode.IsNullOrEmpty(),
                (mode) => SetReaderSearchMode(mode));

            SetBookSearchModeCommand = new RelayCommand<string>(
                (mode) => !mode.IsNullOrEmpty(),
                (mode) => SetBookSearchMode(mode));
        }

        private void InitializeBooks()
        {
            List<Book> tempBooks = _bookRespository.GetBooks();
            List<BookInfo> tempBookInfos = _bookInfoRespository.GetBookInfos();
            List<Publisher> tempPublishers = _publisherRespository.GetPublishers();
            List<Author> tempAuthors = _authorRespository.GetAuthors();
            List<AuthorBook> tempAuthorBooks = _authorBookRespository.GetAuthorBooks();

            List<BookManagementModel> temp = (from bookInfo in tempBookInfos
                                              join
                                                   publisher in tempPublishers on
                                                   bookInfo.PublisherId equals publisher.PublisherId
                                              join
                                                   authorBook in tempAuthorBooks on
                                                   bookInfo.TitleId equals authorBook.TitleId
                                              join
                                                   author in tempAuthors on
                                                   authorBook.AuthorId equals author.AuthorId
                                              join
                                                   book in tempBooks on
                                                   bookInfo.TitleId equals book.TitleId
                                                   into groupedData
                                              from data in groupedData
                                              where data?.Condition != 3
                                              let condition = data?.Condition switch
                                              {
                                                  1 => "Good",
                                                  2 => "Damaged",
                                                  _ => "No Data"
                                              }
                                              select new BookManagementModel()
                                              {
                                                  TitleId = bookInfo.TitleId,
                                                  BookId = data?.BookId == null ? "No book in stock" : data.BookId.ToString(),
                                                  Title = bookInfo.Title,
                                                  Author = author.AuthorName,
                                                  Publisher = publisher.PublisherName,
                                                  InStock = bookInfo.InStock,
                                                  NumberOfPages = bookInfo.NumberOfPages,
                                                  Condition = condition,
                                              }).ToList();

            Application.Current.Dispatcher.Invoke(() =>
            Books = new ObservableCollection<BookManagementModel>(temp)
            );
        }

        private void InitializeReaders()
        {
            List<Reader> temp = _readerRespository.GetReaders();

            Application.Current.Dispatcher.Invoke(() =>
                Readers = new ObservableCollection<Reader>(temp)
            );
        }

        private void CreateTicket(LendTicketConverterModel ticket)
        {
            if (ticket == null || ticket.ReaderId.IsNullOrEmpty() || ticket.ReaderId.IsNullOrEmpty() || ticket.ExpectedReturnDate < DateTime.Now)
            {
                MessageBox.Show("Create lend ticket failed!");
                return;
            }

            Book? bookToLend = _bookRespository.GetBook(int.Parse(ticket.BookId));

            if (bookToLend != null)
            {
                _lendBookRespository.AddLendBook(new()
                {
                    CardNumber = Convert.ToInt32(ticket.ReaderId),
                    LibrarianId = _librarianId,
                    LendDate = DateTime.Now,
                    ExpectedReturnDate = ticket.ExpectedReturnDate,
                    ReturnCondition = bookToLend.Condition
                }, bookToLend);

                MessageBox.Show("Create ticket successfully!");
                LoadData();
            }
        }

        private void SetReaderSearchMode(string parameter) => ReaderSearchMode = parameter;

        private void SetBookSearchMode(string parameter) => BookSearchMode = parameter;

        private void SearchReader()
        {
            InitializeReaders();
            switch (ReaderSearchMode)
            {
                case "Card":
                    if (!ReaderSearchString.IsNullOrEmpty() && int.TryParse(ReaderSearchString, out int value))
                    {
                        Application.Current.Dispatcher.Invoke(
                            () => Readers = new ObservableCollection<Reader>(Readers.Where(x => x.CardNumber == value))
                            );
                    }
                    break;

                case "Name":
                    if (!ReaderSearchString.IsNullOrEmpty())
                    {
                        Application.Current.Dispatcher.Invoke(
                            () => Readers = new ObservableCollection<Reader>(Readers.Where(x => x.FullName.Contains(ReaderSearchString, StringComparison.OrdinalIgnoreCase)))
                            );
                    }
                    break;
            }
        }

        private void SearchBook()
        {
            InitializeBooks();
            switch (BookSearchMode)
            {
                case "Id":
                    if (!BookSearchString.IsNullOrEmpty() && int.TryParse(BookSearchString, out int _))
                    {
                        Application.Current.Dispatcher.Invoke(
                            () => Books = new ObservableCollection<BookManagementModel>(Books.Where(x => x.BookId.Equals(BookSearchString)))
                            );
                    }
                    break;

                case "Name":
                    if (!BookSearchString.IsNullOrEmpty())
                    {
                        Application.Current.Dispatcher.Invoke(
                            () => Books = new ObservableCollection<BookManagementModel>(Books.Where(x => x.Title.Contains(BookSearchString)))
                            );
                    }
                    break;
            }
        }

        private void LoadData()
        {
            InitializeBooks();
            InitializeReaders();
        }
    }
}