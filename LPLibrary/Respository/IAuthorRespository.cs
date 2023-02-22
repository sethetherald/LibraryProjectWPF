using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository
{
    public interface IAuthorRespository
    {
        List<Author> GetAuthors();
    }
}