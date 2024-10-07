using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jan24ft_bet_ca_kronosGR.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectTypeId { get; set; }
        public int TeamId { get; set; }

        public ProjectType? ProjectType { get; set; }
        public Team? Team { get; set; }
    }
}