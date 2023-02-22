using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class Reader
{
    public int CardNumber { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public DateTime CardCreationDate { get; set; }

    public string Address { get; set; } = null!;

    public string Occupation { get; set; } = null!;

    public virtual ICollection<LendBookDetail> LendBookDetails { get; } = new List<LendBookDetail>();

    public virtual ICollection<ReturnBook> ReturnBooks { get; } = new List<ReturnBook>();
}
