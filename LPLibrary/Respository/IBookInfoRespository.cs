using LPLibrary.DataAccess.Models;

namespace LPLibrary.Respository
{
    public interface IBookInfoRespository
    {
        List<BookInfo> GetBookInfos();

        List<BookInfo> GetBookInfos(int titleId);

        BookInfo? GetBookInfo(int titleId);

        BookInfo? GetBookInfo(string title);

        void AddBookInfo(BookInfo data);

        void UpdateBookInfo(BookInfo data);

        void DeleteBookInfo(List<BookInfo> data);
    }
}