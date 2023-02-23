using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface ILendBookRespository
    {
        List<LendBookDetail> GetLendBookDetails();

        LendBookDetail? GetLendBookDetail(int ticketId);

        void AddLendBook(LendBookDetail data, Book lendBook);
    }
}