using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    internal interface IReturnBookRespository
    {
        void ReturnBook(ReturnBookModel data, int librarianId, int condition);
    }
}