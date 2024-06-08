using leaseEase.Domain.Enum.User;
using leaseEase.Domain.Models.Off;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace leaseEase.Domain.Models.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; } 
        public DateTime Created { get; set; }
        public string UserIp { get; set; }
        public Roles Role { get; set; } = Roles.User;
        public bool Blocked { get; set; } = false;
        public List<Office> WishList { get; set; } = new List<Office>();
        public List<Booking> MyBookings { get; set; } = new List<Booking>();
        public List<Review> MyReviews { get; set; } = new List<Review>();
        public virtual TobeCreatorData creatorData { get; set; }

    }
}
