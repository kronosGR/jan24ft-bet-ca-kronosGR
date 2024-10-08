using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jan24ft_bet_ca_kronosGR.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}