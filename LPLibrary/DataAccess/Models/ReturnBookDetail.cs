using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class ReturnBookDetail
{
    public int ReturnId { get; set; }

    public int BookId { get; set; }

    public int? ReturnCondition { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual ReturnBook Return { get; set; } = null!;
}
