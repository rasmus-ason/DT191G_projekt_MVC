using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

    public class ProductCategory {

        [Key]
        public int CategoryId {get; set;}

        [Display(Name = "Namn på kategori")]
        public string? CategoryName {get; set;}
               
    }
}