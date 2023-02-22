using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository
{
    internal interface IReturnBookRespository
    {
        void ReturnBook(ReturnBookModel data, int librarianId, int condition);
    }
}