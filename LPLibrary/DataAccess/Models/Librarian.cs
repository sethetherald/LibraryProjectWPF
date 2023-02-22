using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class Librarian
{
    public int LibrarianId { get; set; }

    public string LibrarianName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();

    public virtual ICollection<LendBookDetail> LendBookDetails { get; } = new List<LendBookDetail>();

    public virtual ICollection<ReturnBook> ReturnBooks { get; } = new List<ReturnBook>();
}
