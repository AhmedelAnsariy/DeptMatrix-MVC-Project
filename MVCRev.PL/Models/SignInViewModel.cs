using System.ComponentModel.DataAnnotations;

namespace MVCRev.PL.Models
{
	public class SignInViewModel
	{
		[Required(ErrorMessage = "Email is Requierd")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }



		[Required(ErrorMessage = "Password is Required")]
		[MinLength(length: 5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string Password { get; set; }


        public bool IsAgree   { get; set; }
    }
}
