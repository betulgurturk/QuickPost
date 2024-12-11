using System;
using System.Collections.Generic;

namespace DBGenerator.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Emailaddress { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string Password { get; set; } = null!;

    public DateTime Createddate { get; set; }

    public DateTime Modifieddate { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
