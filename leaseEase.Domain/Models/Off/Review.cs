using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leaseEase.Domain.Models.Off
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public Office Office { get; set; }

        [Display(Name = "Office")]
        public int OfficeId { get; set; }

        [Display(Name = "Text")]
        [DataType(DataType.Text)]
        public string Text { get; set; }
        [Display(Name = "Rating")]
        public double Rating { get; set; }
    }
}
