using System.ComponentModel.DataAnnotations;

namespace MVCRev.PL.Models
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is Requierd")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
