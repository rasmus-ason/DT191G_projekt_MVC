using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{
       public class Recipe
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
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

