using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DT191G_projekt.Models{

    public class AboutUs {

        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Ange rubrik")]
        [Display(Name = "Rubrik")]
        public string? Title {get; set;}

        [Required(ErrorMessage = "Ange Text")]
        [Display(Name = "Ange en om-oss text som kommer presenteras på webbplatsen")]
        public string? Text {get; set;}

        [Display(Name = "Sökväg till bild")]
        public string? ImageName {get; set;}

        [Display(Name = "Skriv en bildtext")]
        public string? AltText {get; set;}

        //Genereras i gränssnittet men inte i databasen
        [Display(Name = "Välj bild")]
        [NotMapped]
        public IFormFile? ImageFile {get; set;}
         
             
    }
}