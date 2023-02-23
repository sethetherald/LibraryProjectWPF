using LPLibrary.DataAccess.DataManagement;
using LPLibrary.DataAccess.Models;
using LPLibrary.Respository.Interfaces;

namespace LPLibrary.Respository.Classes
{
    public class BookInfoRespository : IBookInfoRespository
    {
        public void AddBookInfo(BookInfo data) => BookInfoManagement.AddBookInfo(data);

        public void DeleteBookInfo(List<BookInfo> data) => BookInfoManagement.DeleteBookInfo(data);

        public BookInfo? GetBookInfo(int titleId) => BookInfoManagement.GetBookInfo(titleId);

        public BookInfo? GetBookInfo(string title) => BookInfoManagement.GetBookInfo(title);

        public List<BookInfo> GetBookInfos() => BookInfoManagement.GetBookInfos();

        public List<BookInfo> GetBookInfos(int titleId) => BookInfoManagement.GetBookInfos(titleId);

        public void UpdateBookInfo(BookInfo data) => BookInfoManagement.UpdateBookInfo(data);
    }
}