using LPLibrary.DataAccess.Models;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class BookManagement
    {
        public static List<Book> GetBooks()
        {
            using var context = new LibraryManagementContext();
            return context.Books.ToList();
        }

        public static List<Book> GetBooks(int titleId)
        {
            using var context = new LibraryManagementContext();
            return context.Books.Where(x => x.TitleId == titleId).ToList();
        }

        public static Book? GetBook(int bookId)
        {
            using var context = new LibraryManagementContext();
            return context.Books.FirstOrDefault(x => x.BookId == bookId);
        }

        public static void AddBooks(int amount, int titleId)
        {
            using var context = new LibraryManagementContext();
            BookInfo? temp = BookInfoManagement.GetBookInfo(titleId);
            if (temp != null)
            {
                for (int i = 0; i < amount; i++)
                {
                    context.Books.Add(new()
                    {
                        TitleId = titleId,
                        Condition = 1
                    });
                }
                temp.InStock += amount;
                context.BookInfos.Update(temp);
            }
            context.SaveChanges();
        }

        public static void UpdateBook(Book data)
        {
            using var context = new LibraryManagementContext();
            context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteBook(int bookId, int titleId)
        {
            using var context = new LibraryManagementContext();
            BookInfo? tempInfo = BookInfoManagement.GetBookInfo(titleId);
            Book? bookToDelete = GetBook(bookId);
            if (tempInfo != null && bookToDelete != null)
            {
                context.Books.Remove(bookToDelete);
                tempInfo.InStock -= 1;
                context.BookInfos.Update(tempInfo);
            }
            context.SaveChanges();
        }

        public static void DeleteBooks(List<Book> books)
        {
            using var context = new LibraryManagementContext();
            context.Books.RemoveRange(books);
            context.SaveChanges();
        }
    }
}