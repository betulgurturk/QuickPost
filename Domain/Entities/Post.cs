using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Post
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string Content { get; set; } = null!;

    public int? Likecount { get; set; }

    public int? Viewcount { get; set; }

    public DateTime Createddate { get; set; }

    public DateTime Modifieddate { get; set; }

    public bool IsDeleted { get; set; }
    public virtual User User { get; set; } = null!;
}
