namespace LPLibrary.DataAccess.Models
{
    public class ReturnBookModel
    {
        public int BookId { get; set; }
        public int TicketId { get; set; }
        public int ReaderCard { get; set; }
        public string? ReaderName { get; set; }
        public int LibrarianId { get; set; }
        public DateTime LendDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public string? LendCondition { get; set; }
    }
}