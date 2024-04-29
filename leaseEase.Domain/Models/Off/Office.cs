using leaseEase.Domain.Enum.Off;
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
    [Table("Offices")]
    public class Office
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Type")]
        public TypesOfOffice Type { get; set; }
        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Location")]
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Created")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }  = DateTime.Now;
        [Required]
        [Display(Name = "TeamSize")]
        public int TeamSize { get; set; }
        [Required]
        [Display(Name = "Image")]
        public byte[] Image { get; set; }
        [Display(Name = "Images")]
        public byte[][] Images { get; set; }
        [Required]
        [Display(Name = "Rooms")]
        public int Rooms { get; set; }
        [Required]
        [Display(Name = "Size")]
        public decimal Size { get; set; }
        [Display(Name = "Facilities")]
        public Facility[] Facilities { get; set; }
        [Required]
        [Display(Name = "MinimalRentalPeriod")]
        public MinimalRentalPeriod MinimalRentalPeriod { get; set; }
        [Display(Name = "UnavaliableDates")]
        public DateTime[] unavaliableDates { get; set; }
        [Required]
        [Display(Name = "IsActive")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [Required]
        [Display(Name = "Rating")]
        [DefaultValue(0)]
        public double Rating { get; set; }
    }
}
