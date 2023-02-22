using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string AuthorName { get; set; } = null!;

    public virtual ICollection<AuthorBook> AuthorBooks { get; } = new List<AuthorBook>();
}
