using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Follower
{
    public Guid Id { get; set; }

    public DateTime Createddate { get; set; }

    public DateTime Modifieddate { get; set; }

    public Guid Followinguserid { get; set; }

    public Guid Userid { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual User Followinguser { get; set; } = null!;
}
