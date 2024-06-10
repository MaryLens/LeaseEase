using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.User
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public string content { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
