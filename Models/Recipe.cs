using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{
        public class Recipe
        {
            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public ICollection<Ingredient>? Ingredients { get; set; }
        }

        public class Ingredient
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public decimal? Quantity { get; set; }
        }

}

