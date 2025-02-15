using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jan24ft_bet_ca_kronosGR.Models
{
    public class Team
    {
        public Team(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Project>? Projects { get; set; }
        public ICollection<Developer>? Developers { get; set; }
    }
}