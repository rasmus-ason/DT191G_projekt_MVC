using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

    public class ProductBrand {

        [Key]
        public int BrandId {get; set;}

        [Required(ErrorMessage = "Ange namn på märke/leverantör")]
        [Display(Name = "Märke")]
        public string? BrandName {get; set;}
             
    }
}