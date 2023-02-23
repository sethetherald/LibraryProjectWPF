using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class AuthorBookRespository : IAuthorBookRespository
    {
        public void AddAuthorBook(AuthorBook data) => AuthorBookManagement.AddAuthorBook(data);

        public void DeleteAuthorBook(List<AuthorBook> data) => AuthorBookManagement.DeleteAuthorBook(data);

        public AuthorBook? GetAuthorBookByTitleId(int titleId) => AuthorBookManagement.GetAuthorBookByTitleId(titleId);

        public List<AuthorBook> GetAuthorBooks() => AuthorBookManagement.GetAuthorBooks();

        public List<AuthorBook> GetAuthorBooks(int titleId) => AuthorBookManagement.GetAuthorBooks(titleId);

        public void UpdateAuthorBook(string titleId, int authorId) => AuthorBookManagement.UpdateAuthorBook(titleId, authorId);
    }
}