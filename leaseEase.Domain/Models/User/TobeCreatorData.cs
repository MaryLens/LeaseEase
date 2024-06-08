using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using leaseEase.Domain.Models.Off;

namespace leaseEase.Domain.Models.User
{
    public class TobeCreatorData
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        [Display(Name = "Location")]
        [DataType(DataType.Text)]
        public string Location { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        public List<Office> MyOffices { get; set; } = new List<Office>();
        public int UserId { get; set; }
        public virtual leaseEase.Domain.Models.User.User User { get; set; }
    }
}
