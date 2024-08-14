using System.ComponentModel.DataAnnotations;

namespace DOTNETDemo.Models.Request;

public class UserCardRequest
{
    [Required(ErrorMessage = "ID is required.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Card number is required.")]
    [CreditCard(ErrorMessage = "Invalid card number.")]
    public string? CardNumber { get; set; }

    [Required(ErrorMessage = "CVC is required.")]
    [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC must be 3 digits.")]
    public string? CVC { get; set; }

    [Required(ErrorMessage = "Expiry date is required.")]
    [DataType(DataType.Date, ErrorMessage = "Invalid expiry date.")]
    [FutureDate(ErrorMessage = "Expiry date must be in the future.")]
    public DateTime ExpiryDate { get; set; }
}

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
        return false;
    }
}