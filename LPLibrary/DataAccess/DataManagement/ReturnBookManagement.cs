using LPLibrary.DataAccess.Models;

namespace LPLibrary.DataAccess.DataManagement
{
    public static class ReturnBookManagement
    {
        public static void ReturnBook(ReturnBookModel data, int librarianId, int condition)
        {
            using var context = new LibraryManagementContext();
            LendBookDetail? lendDetail = LendBookManagement.GetLendBookDetail(data.TicketId);
            Book? returnBook = BookManagement.GetBook(data.BookId);

            ReturnBook general = new()
            {
                CardNumber = data.ReaderCard,
                LibrarianId = librarianId,
                ReturnDate = DateTime.Now
            };

            ReturnBookDetail detail = new()
            {
                BookId = data.BookId,
                ReturnCondition = condition
            };

            general.ReturnBookDetails.Add(detail);
            context.ReturnBooks.Add(general);

            if (lendDetail != null) context.LendBookDetails.Remove(lendDetail);

            if (returnBook != null)
            {
                BookInfo? returnBookInfo = BookInfoManagement.GetBookInfo(returnBook.TitleId);
                if (returnBookInfo != null && condition != 3)
                {
                    returnBook.Condition = condition;
                    BookManagement.UpdateBook(returnBook);

                    returnBookInfo.InStock += 1;
                    context.BookInfos.Update(returnBookInfo);
                }
            }

            context.SaveChanges();
        }
    }
}