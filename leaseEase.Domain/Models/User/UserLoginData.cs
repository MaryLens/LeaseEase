using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.User
{
    public class UserLoginData
    {
        public string Credential { get; set; }
        public string Password { get; set; }
        public string UserIp {  get; set; }
        public DateTime LastLogin { get; set; }
    }
}
