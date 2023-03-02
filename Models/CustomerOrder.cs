using System.ComponentModel.DataAnnotations;

namespace DT191G_projekt.Models{

   public class CustomerOrder {
    [Key]
    public int OrderId {get; set;}
    public int OrderNumber {get; set;}
    public string? Firstname {get; set;}
    public string? Lastname {get; set;}
    public string? Email {get; set;}
    public double Phonenumber {get; set;}
    public string? Adress {get; set;}
    public int ZipCode {get; set;}
    public string? City {get; set;}
    public DateTime? PurchaseDate {get; set;}
    public decimal TotalPrice {get; set;}
    public decimal ShippingCost {get; set;}
    public bool? IsPacked {get; set;}
    public bool? IsShipped {get; set;} 
}
}