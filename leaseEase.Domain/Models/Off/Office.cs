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

        [Required(ErrorMessage = "Title is required.")]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [NotMapped]
        public TypesOfOffice Type { get; set; }

        public int TypeId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number.")]
        [Display(Name = "Price")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [Display(Name = "Location")]
        [DataType(DataType.Text)]
        public string Location { get; set; }
        [Display(Name = "Created")]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }  = DateTime.Now;

        [Required(ErrorMessage = "Team size is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Team size must be a positive number.")]
        [Display(Name = "TeamSize")]
        public int? TeamSize { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required(ErrorMessage = "Main image is required.")]
        [Display(Name = "Image")]
        public byte[] Image { get; set; }

        public List<OfficeImg> Images { get; set; } = new List<OfficeImg>();

        [Required(ErrorMessage = "Rooms number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Rooms number must be a positive number.")]
        [Display(Name = "Rooms")]
        public int? Rooms { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Size must be a positive number.")]
        [Display(Name = "Size")]
        public decimal? Size { get; set; }

        [Display(Name = "Facilities")]
        public List<Facility> Facilities { get; set; } = new List<Facility>();

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
