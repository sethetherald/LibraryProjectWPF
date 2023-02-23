using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository.Interfaces
{
    public interface IAccountRespository
    {
        List<Account> GetAccounts();

        Account? GetAccount(string account, string password);

        Account? GetAccount(int id);

        Account? GetAccount(string searchString);

        void UpdateAccount(Account data);
    }
}