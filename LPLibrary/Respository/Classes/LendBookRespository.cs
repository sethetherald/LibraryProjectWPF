using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class LendBookRespository : ILendBookRespository
    {
        public void AddLendBook(LendBookDetail data, Book lendBook) => LendBookManagement.AddLendBook(data, lendBook);

        public LendBookDetail? GetLendBookDetail(int ticketId) => LendBookManagement.GetLendBookDetail(ticketId);

        public List<LendBookDetail> GetLendBookDetails() => LendBookManagement.GetLendBookDetails();
    }
}