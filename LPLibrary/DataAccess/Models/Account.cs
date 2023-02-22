using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class Account
{
    public string Username { get; set; } = null!;

    public string Gmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int LibrarianId { get; set; }

    public int Role { get; set; }

    public virtual Librarian Librarian { get; set; } = null!;
}
