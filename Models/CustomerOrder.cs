using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

   public class CustomerOrder {
    [Key]
    public int OrderId {get; set;}

    [Display(Name = "Ordernummer")]
    public int OrderNumber {get; set;}

    [Required(ErrorMessage = "Ange förnamn")]
    [Display(Name = "Förnamn")]
    public string? Firstname {get; set;}

    [Required(ErrorMessage = "Ange efternamn")]
    [Display(Name = "Efternamn")]
    public string? Lastname {get; set;}

    [Required(ErrorMessage = "Ange e-post")]
    [Display(Name = "E-post")]
    public string? Email {get; set;}

    [Required(ErrorMessage = "Ange telefonnummer")]
    [Display(Name = "Telefonnummer")]
    public double Phonenumber {get; set;}

    [Required(ErrorMessage = "Ange adress")]
    [Display(Name = "Adress")]
    public string? Adress {get; set;}

    [Required(ErrorMessage = "Ange postnummer")]
    [Display(Name = "Postnummer")]
    public int ZipCode {get; set;}

    [Required(ErrorMessage = "Ange stad")]
    [Display(Name = "Stad")]
    public string? City {get; set;}

    [Required]
    [Display(Name = "Datum för köp")]
    public DateTime? PurchaseDate {get; set;}

    [Required]
    [Display(Name = "Totalpris ex. frakt")]
    public decimal TotalPrice {get; set;}

    [Required]
    [Display(Name = "Fraktkostnad")]
    public decimal ShippingCost {get; set;}

    [Required]
    [Display(Name = "Är varan paketerad?")]
    public bool? IsPacked {get; set;}

    [Required]
    [Display(Name = "Är varan skickad?")]
    public bool? IsShipped {get; set;} 
}
}