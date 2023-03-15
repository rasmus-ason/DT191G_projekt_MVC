using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace DT191G_projekt.Models{

    public class Product {
        public int ProductId {get; set;}

        [Display(Name = "Artikelnummer")]
        public int ArticleNumber {get; set;}
        
        [Required(ErrorMessage = "Ange antal varor i lager")]
        [Display(Name = "Antal i lager")]
        public int AmountInStock {get; set;}

        [Required(ErrorMessage = "Sätt ett namn på varan")]
        [Display(Name = "Namn på produkt")]
        public string? Title {get; set;}

        [Required(ErrorMessage = "Skriv en beskrivning av varan")]
        [Display(Name = "Beskrivning av produkt")]
        public string? ProductInfo {get; set;}

        [Display(Name = "Sökväg till bild")]
        public string? ImageName {get; set;}

        [Display(Name = "Skriv en bildtext")]
        public string? AltText {get; set;}

        [Required(ErrorMessage = "Bestäm kategori på vara")]
        [Display(Name = "Kategori")]
        public string? Category {get; set;}

        [Display(Name = "Vikt i gram (för mjöler)")]
        public string? Weight {get; set;}

        [Required(ErrorMessage = "Ange pris inkl moms")]
        [Display(Name = "Pris inkl moms")]
        public int? Price {get; set;}

        [Required(ErrorMessage = "Ange vilket märke varan har")]
        [Display(Name = "Märke")]
        public string? Brand {get; set;}

        [Display(Name = "Skapad")]
        [DataType(DataType.Date)]
        public DateTime Created {get; set;} = DateTime.Now;

        [Display(Name = "Ska produkten vara en del i startkittet?")]
        public bool? IsInStartkit {get; set;} = false;

        //Genereras i gränssnittet men inte i databasen
        [Display(Name = "Välj bild")]
        [NotMapped]
        public IFormFile? ImageFile {get; set;}
         
    }
}