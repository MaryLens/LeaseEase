using leaseEase.Domain.Enum.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.User
{
    public class UserMinData
    {
        public string Username { get; set; }
        public string Email {  get; set; }  
        public Roles Role { get; set; }
        public byte[] Image { get; set; }

    }
}
