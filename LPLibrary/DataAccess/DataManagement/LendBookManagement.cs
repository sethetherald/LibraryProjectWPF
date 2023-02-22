using LPLibrary.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class LendBookManagement
    {
        public static List<LendBookDetail> GetLendBookDetails()
        {
            using var context = new LibraryManagementContext();
            return context.LendBookDetails.Include(x => x.Books).ToList();
        }

        public static LendBookDetail? GetLendBookDetail(int ticketId)
        {
            using var context = new LibraryManagementContext();
            return context.LendBookDetails.Include(x => x.Books).FirstOrDefault(x => x.LendBookDetailId == ticketId);
        }

        public static void AddLendBook(LendBookDetail data, Book lendBook)
        {
            using var context = new LibraryManagementContext();
            BookInfo? bookInfo = BookInfoManagement.GetBookInfo(lendBook.TitleId);
            if (bookInfo != null)
            {
                bookInfo.InStock -= 1;
                BookInfoManagement.UpdateBookInfo(bookInfo);

                data.Books.Add(lendBook);
                context.LendBookDetails.Attach(data).State = EntityState.Added;
            }
            lendBook.Condition = 3;
            context.Books.Update(lendBook);
            context.SaveChanges();
        }
    }
}