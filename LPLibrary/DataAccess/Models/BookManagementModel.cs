namespace LPLibrary.DataAccess.Models
{
    internal class BookManagementModel
    {
        public int TitleId { get; set; }
        public string? BookId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public int InStock { get; set; }
        public int NumberOfPages { get; set; }
        public string? Condition { get; set; }
    }
}