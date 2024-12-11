using System;
using System.Collections.Generic;

namespace DBGenerator.Models;

public partial class Follower
{
    public Guid Id { get; set; }

    public DateTime Createddate { get; set; }

    public DateTime Modifieddate { get; set; }

    public Guid? Followinguserid { get; set; }

    public Guid? Userid { get; set; }
}
