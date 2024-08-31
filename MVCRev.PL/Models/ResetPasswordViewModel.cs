using System.ComponentModel.DataAnnotations;

namespace MVCRev.PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(length: 5, ErrorMessage = "Minimum Password Length is 5")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }



        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm password does not match password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }



    }
}
