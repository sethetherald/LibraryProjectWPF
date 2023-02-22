using System;
using System.Collections.Generic;

namespace LPLibrary.DataAccess.Models;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public virtual ICollection<BookInfo> BookInfos { get; } = new List<BookInfo>();
}
