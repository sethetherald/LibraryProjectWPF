using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface IBookRespository
    {
        List<Book> GetBooks();

        List<Book> GetBooks(int titleId);

        Book? GetBook(int bookId);

        void AddBooks(int amount, int titleId);

        void UpdateBook(Book data);

        void DeleteBook(int bookId, int titleId);

        void DeleteBooks(List<Book> books);
    }
}