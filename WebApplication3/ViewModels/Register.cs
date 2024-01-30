using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using WebApplication3.Model;

namespace WebApplication3.ViewModels
{
    public class Register
    {
		[Required]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[RegularExpression(@"^\d{16}$", ErrorMessage = "Credit Card No must be 16 digits.")]
		[Display(Name = "Credit Card No")]
		public string CreditCardNo { get; set; }

		[Required]
		[RegularExpression(@"^\d{8}$", ErrorMessage = "Mobile No must be 8 digits.")]
		[Display(Name = "Mobile No")]
		public string MobileNo { get; set; }

		[Required]
		[Display(Name = "Billing Address")]
		public string BillingAddress { get; set; }

		[Required]
		[Display(Name = "Shipping Address")]
		public string ShippingAddress { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email Address")]
        [UniqueEmail(ErrorMessage = "Email already exists. Please use a different email.")]
        public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[NotMapped]
		[Display(Name = "Photo (.JPG only)")]
		[DataType(DataType.Upload)]
		public IFormFile Photo { get; set; }
	}
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value?.ToString();

            // Access the DbContext using dependency injection
            var dbContext = validationContext.GetService(typeof(AuthDbContext)) as AuthDbContext;

            // Check if the email already exists in the database
            var isEmailUnique = !dbContext.Users.Any(u => u.Email == email);

            if (!isEmailUnique)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
