using LPLibrary.DataAccess.Models;
using System.Net.Mail;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class AccountManagement
    {
        public static List<Account> GetAccounts()
        {
            using var context = new LibraryManagementContext();
            return context.Accounts.ToList();
        }

        public static Account? GetAccount(string account, string password)
        {
            using var context = new LibraryManagementContext();
            return context.Accounts
                .FirstOrDefault(x =>
                x.Username.Equals(account) &&
                x.Password.Equals(password));
        }

        public static Account? GetAccount(int id)
        {
            using var context = new LibraryManagementContext();
            return context.Accounts.FirstOrDefault(x => x.LibrarianId == id);
        }

        public static Account? GetAccount(string searchString)
        {
            using var context = new LibraryManagementContext();
            if (MailAddress.TryCreate(searchString, out _))
            {
                return context.Accounts.FirstOrDefault(x => x.Gmail.Equals(searchString));
            }
            else
            {
                return context.Accounts.FirstOrDefault(x => x.Username.Equals(searchString));
            }
        }

        public static void UpdateAccount(Account data)
        {
            Account? checkExist = GetAccount(data.LibrarianId);
            if (checkExist != null)
            {
                using var context = new LibraryManagementContext();
                context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}