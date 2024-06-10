using leaseEase.Domain.Enum.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using leaseEase.Domain.Models.Off;

namespace leaseEase.Domain.Models.User
{
    public class UserMinData
    {
        public string Username { get; set; }
        public string Email {  get; set; }  
        public Roles Role { get; set; }
        public byte[] Image { get; set; }
        public List<Office> Favourites { get; set; }

    }
}
