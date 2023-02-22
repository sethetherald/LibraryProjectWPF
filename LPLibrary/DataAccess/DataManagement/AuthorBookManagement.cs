using LPLibrary.DataAccess.Models;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class AuthorBookManagement
    {
        public static List<AuthorBook> GetAuthorBooks()
        {
            using var context = new LibraryManagementContext();
            return context.AuthorBooks.ToList();
        }

        public static List<AuthorBook> GetAuthorBooks(int titleId)
        {
            using var context = new LibraryManagementContext();
            return context.AuthorBooks.Where(x => x.TitleId == titleId).ToList();
        }

        public static AuthorBook? GetAuthorBookByTitleId(int titleId)
        {
            using var context = new LibraryManagementContext();
            return context.AuthorBooks.FirstOrDefault(x => x.TitleId == titleId);
        }

        public static void AddAuthorBook(AuthorBook data)
        {
            using var context = new LibraryManagementContext();
            context.AuthorBooks.Add(data);
            context.SaveChanges();
        }

        public static void UpdateAuthorBook(string titleId, int authorId)
        {
            using var context = new LibraryManagementContext();
            AuthorBook? tempAuthorBook = GetAuthorBookByTitleId(Convert.ToInt32(titleId));
            if (tempAuthorBook != null)
            {
                context.AuthorBooks.Remove(tempAuthorBook);
                context.SaveChanges();

                tempAuthorBook.AuthorId = authorId;
                context.AuthorBooks.Add(tempAuthorBook);
                context.SaveChanges();
            }
        }

        public static void DeleteAuthorBook(List<AuthorBook> data)
        {
            using var context = new LibraryManagementContext();
            context.AuthorBooks.RemoveRange(data);
            context.SaveChanges();
        }
    }
}