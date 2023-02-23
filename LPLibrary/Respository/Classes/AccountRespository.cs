using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class AccountRespository : IAccountRespository
    {
        public Account? GetAccount(string account, string password) => AccountManagement.GetAccount(account, password);

        public Account? GetAccount(int id) => AccountManagement.GetAccount(id);

        public Account? GetAccount(string searchString) => AccountManagement.GetAccount(searchString);

        public List<Account> GetAccounts() => AccountManagement.GetAccounts();

        public void UpdateAccount(Account data) => AccountManagement.UpdateAccount(data);
    }
}