using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface IAuthorBookRespository
    {
        List<AuthorBook> GetAuthorBooks();

        List<AuthorBook> GetAuthorBooks(int titleId);

        AuthorBook? GetAuthorBookByTitleId(int titleId);

        void AddAuthorBook(AuthorBook data);

        void UpdateAuthorBook(string titleId, int authorId);

        void DeleteAuthorBook(List<AuthorBook> data);
    }
}