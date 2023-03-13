using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

    public class EmailForm {

        public int Id {get; set;}

        [Required]
        public string? SenderName {get; set;}

        [Required]
        public string? SenderEmail {get; set;}

        [Required]
        public string? SenderMessage {get; set;}
             
    }
}