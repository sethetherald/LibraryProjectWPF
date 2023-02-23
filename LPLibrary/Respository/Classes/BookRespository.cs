using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class BookRespository : IBookRespository
    {
        public void AddBooks(int amount, int titleId) => BookManagement.AddBooks(amount, titleId);

        public void DeleteBook(int bookId, int titleId) => BookManagement.DeleteBook(bookId, titleId);

        public void DeleteBooks(List<Book> books) => BookManagement.DeleteBooks(books);

        public Book? GetBook(int bookId) => BookManagement.GetBook(bookId);

        public List<Book> GetBooks() => BookManagement.GetBooks();

        public List<Book> GetBooks(int titleId) => BookManagement.GetBooks(titleId);

        public void UpdateBook(Book data) => BookManagement.UpdateBook(data);
    }
}