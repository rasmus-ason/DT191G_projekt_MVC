using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DT191G_projekt.Models{

        public class DetailedOrder {

            [Key]
            public int Id {get; set;}
            public int OrderNumber { get; set; }
            public List<Article>? Articles { get; set; }
        }

        public class Article {

            [Key]
            public int Id {get; set;}
            public int ArticleNumber { get; set; }
            public int Amount { get; set; }

            // Foreign key property
            public int DetailedOrderId { get; set; }

           
        }  
       
}