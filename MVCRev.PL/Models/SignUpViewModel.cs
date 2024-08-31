using System.ComponentModel.DataAnnotations;

namespace MVCRev.PL.Models
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "Email is Requierd")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }






		[Required(ErrorMessage = "First Name is Required")]
		public string FirstName { get; set; }



		[Required(ErrorMessage = "Last Name is Required")]
		public string LastName { get; set; }




		public string NationalNumber { get; set; }


		[Required(ErrorMessage = "Phone number is required")]
		[Phone(ErrorMessage = "Invalid phone number")]
		public string PhoneNumber { get; set; }




		[Required(ErrorMessage = "User Name is Required")]
		public string UserName { get; set; }



		[Required(ErrorMessage = "Password is Required")]
		[MinLength(length: 5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }



		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(Password), ErrorMessage = "Confirm password does not match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }



		public bool IsAgree { get; set; }
	}
}
