using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Like
{
    public Guid Id { get; set; }

    public DateTime Createddate { get; set; }

    public DateTime Modifieddate { get; set; }

    public Guid Userid { get; set; }

    public Guid Postid { get; set; }
}
