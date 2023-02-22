using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class BookInfo
{
    public int TitleId { get; set; }

    public string Title { get; set; } = null!;

    public int InStock { get; set; }

    public int NumberOfPages { get; set; }

    public int? Frequency { get; set; }

    public int PublisherId { get; set; }

    public virtual ICollection<AuthorBook> AuthorBooks { get; } = new List<AuthorBook>();

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual Publisher Publisher { get; set; } = null!;
}
