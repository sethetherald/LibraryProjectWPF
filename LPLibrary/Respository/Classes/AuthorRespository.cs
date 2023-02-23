using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class AuthorRespository : IAuthorRespository
    {
        public List<Author> GetAuthors() => AuthorManagemenet.GetAuthors();
    }
}