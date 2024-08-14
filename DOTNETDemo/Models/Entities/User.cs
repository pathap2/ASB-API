namespace DOTNETDemo.Models.Entities;

public class User
{
    public int Id { get; set; }           // Primary Key, auto-incremented
    public string? Name { get; set; }      // Name of the user
    public string? CardNumber { get; set; } // Card number (16 digits)
    public string? CVC { get; set; }       // Card CVC (3 digits)
    public DateTime ExpiryDate { get; set; } // Card expiry date (Year, Month, Day)
}
