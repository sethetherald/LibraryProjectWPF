using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class Book
{
    public int BookId { get; set; }

    public int TitleId { get; set; }

    public int Condition { get; set; }

    public virtual ICollection<ReturnBookDetail> ReturnBookDetails { get; } = new List<ReturnBookDetail>();

    public virtual BookInfo Title { get; set; } = null!;

    public virtual ICollection<LendBookDetail> LendBookDetails { get; } = new List<LendBookDetail>();
}
