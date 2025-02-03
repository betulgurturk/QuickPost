using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class FollowerDto
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string UserName { get; set; }
    }
}
