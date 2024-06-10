using leaseEase.Domain.Enum.Off;
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
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public Chat Chat { get; set; }

        [Display(Name = "Chat")]
        public int ChatId { get; set; }
        public string content { get; set; }
        [NotMapped]
        public leaseEase.Domain.Models.User.User Creator { get; set; }
        public int CreatorId { get; set; }
    }
}
