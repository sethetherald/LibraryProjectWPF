using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface IAuthorRespository
    {
        List<Author> GetAuthors();
    }
}