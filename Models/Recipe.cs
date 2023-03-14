using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DT191G_projekt.Models{
       public class Recipe
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Lägg till namn på receptet")]
            [Display(Name = "Lägg till en namn på receptet")]
            public string? Title { get; set; }

            [Required(ErrorMessage = "Lägg till en beskrivning på receptet")]
            [Display(Name = "Beskriv receptet")]
            public string? Description { get; set; }

            [Display(Name = "Sökväg till bild")]
            public string? ImageName {get; set;}

            [Required(ErrorMessage = "Skriv en bildtext till bilden")]
            [Display(Name = "Skriv en bildtext")]
            public string? AltText {get; set;}
            //Genereras i gränssnittet men inte i databasen
            [Display(Name = "Välj bild")]
            [NotMapped]
            public IFormFile? ImageFile {get; set;}
            public List<Ingredient>? Ingredients { get; set; }
        }

        public class Ingredient
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int? Quantity { get; set; }
            public string? Unit { get; set; }
        }

}

