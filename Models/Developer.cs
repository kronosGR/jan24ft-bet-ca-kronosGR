using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jan24ft_bet_ca_kronosGR.Models
{
    public class Developer
    {
        public int Id { get; set; }

        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int RoleId { get; set; }
        public int TeamId { get; set; }
    }
}