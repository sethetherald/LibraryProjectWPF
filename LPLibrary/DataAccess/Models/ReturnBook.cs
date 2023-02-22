using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class ReturnBook
{
    public int ReturnId { get; set; }

    public int CardNumber { get; set; }

    public int LibrarianId { get; set; }

    public DateTime ReturnDate { get; set; }

    public virtual Reader CardNumberNavigation { get; set; } = null!;

    public virtual Librarian Librarian { get; set; } = null!;

    public virtual ICollection<ReturnBookDetail> ReturnBookDetails { get; } = new List<ReturnBookDetail>();
}
