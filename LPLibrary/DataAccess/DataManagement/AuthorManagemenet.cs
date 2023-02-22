using LPLibrary.DataAccess.Models;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class AuthorManagemenet
    {
        public static List<Author> GetAuthors()
        {
            using var context = new LibraryManagementContext();
            return context.Authors.ToList();
        }
    }
}