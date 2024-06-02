using leaseEase.Domain.Enum.Off;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace leaseEase.Domain.Models.Off
{
    [Table("Offices")]
    public class Office
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
        [NotMapped]
        public TypesOfOffice Type { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Location")]
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [Display(Name = "Created")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }  = DateTime.Now;

        [Display(Name = "TeamSize")]
        public int TeamSize { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [Display(Name = "Image")]
        public byte[] Image { get; set; }
        [Display(Name = "Images")]
        public byte[][] Images { get; set; }

        [Display(Name = "Rooms")]
        public int Rooms { get; set; }

        [Display(Name = "Size")]
        public decimal Size { get; set; }

        public List<Facility> Facilities { get; set; }

        [Display(Name = "Facilities")]
        public List<int> FacilitiesId { get; set; }

        [Display(Name = "MinimalRentalPeriod")]
        public MinimalRentalPeriod MinimalRentalPeriod { get; set; }
        [Display(Name = "UnavaliableDates")]
        public DateTime[] unavaliableDates { get; set; }
        [Display(Name = "IsActive")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        [Display(Name = "Rating")]
        [DefaultValue(0)]
        public double Rating { get; set; }
        public int Views { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
