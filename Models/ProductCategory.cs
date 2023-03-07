using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

    public class ProductCategory {

        [Key]
        public int CategoryId {get; set;}

        [Required(ErrorMessage = "Ange namn på kategori")]
        [Display(Name = "Namn på kategori")]
        public string? CategoryName {get; set;}
               
    }
}