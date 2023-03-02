using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

    public class ProductBrand {

        [Key]
        public int BrandId {get; set;}

        [Display(Name = "Märke")]
        public string? BrandName {get; set;}
             
    }
}