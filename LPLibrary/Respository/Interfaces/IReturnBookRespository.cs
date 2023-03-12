using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface IReturnBookRespository
    {
        void ReturnBook(ReturnBookModel data, int librarianId, int condition);
    }
}