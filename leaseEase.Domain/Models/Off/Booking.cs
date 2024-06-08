using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using leaseEase.Domain.Enum.Off;

namespace leaseEase.Domain.Models.Off
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public Office Office { get; set; }

        [Display(Name = "Office")]
        public int OfficeId { get; set; }

        [Display(Name = "days")]
        public int days { get; set; }
        [Display(Name = "Summ")]
        public decimal summ {  get; set; }
        [Display(Name = "DateStart")]
        public DateTime DateStart { get; set; }
        [Display(Name = "DateEnd")]
        public DateTime DateEnd { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public statusBooking statusBooking { get; set; }
        [NotMapped]
        public leaseEase.Domain.Models.User.User Creator { get; set; }
        public int CreatorId { get; set; }
    }
}
