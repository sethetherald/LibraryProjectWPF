using LibraryProjectWPF.Ultilities;
using LibraryProjectWPF.Ultilities.Converter;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Classes;
using LPLibrary.Respository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LibraryProjectWPF.ViewModels
{
    internal class LendBookViewModel
    {
        public ICommand CreateTicketCommand { get; set; }

        private readonly int _librarianId;
        private IReaderRespository _readerRespository = new ReaderRespository();
        private IBookRespository _bookRespository = new BookRespository();
        private IBookInfoRespository _bookInfoRespository = new BookInfoRespository();
        private IPublisherRespository _publisherRespository = new PublisherRespository();
        private IAuthorRespository _authorRespository = new AuthorRespository();
        private IAuthorBookRespository _authorBookRespository = new AuthorBookRespository();
        private ILendBookRespository _lendBookRespository = new LendBookRespository();

        private ObservableCollection<BookManagementModel> _books;
        private ObservableCollection<Reader> _readers;

        public ObservableCollection<BookManagementModel> Books => _books;

        public ObservableCollection<Reader> Readers => _readers;

        public LendBookViewModel(int librarianId)
        {
            _librarianId = librarianId;
            InitializeBooks();
            InitializeReaders();

            CreateTicketCommand = new RelayCommand<LendTicketConverterModel>(
                (ticket) => ticket != null,
                (ticket) => CreateTicket(ticket)
                );
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
            _books = new ObservableCollection<BookManagementModel>(temp);
        }

        private void InitializeReaders()
        {
            List<Reader> temp = _readerRespository.GetReaders();
            _readers = new ObservableCollection<Reader>(temp);
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
                ReloadList();
            }
        }

        private void ReloadList()
        {
            InitializeReaders();
            InitializeBooks();
        }
    }
}