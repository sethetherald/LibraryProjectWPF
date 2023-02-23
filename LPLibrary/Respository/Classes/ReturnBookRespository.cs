using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class ReturnBookRespository : IReturnBookRespository
    {
        public void ReturnBook(ReturnBookModel data, int librarianId, int condition) => ReturnBookManagement.ReturnBook(data, librarianId, condition);
    }
}