using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCRev.DAL.Models;
using MVCRev.PL.Helper;
using MVCRev.PL.Models;
using System.Threading.Tasks;

namespace MVCRev.PL.Controllers
{

    public class AccountController : Controller
    {
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController( UserManager<ApplicationUser>  userManager, SignInManager<ApplicationUser> signInManager )
        {
			_signInManager = signInManager;
			_userManager = userManager;
		}



        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
		public async Task< IActionResult> SignUp(SignUpViewModel model )
		{
			if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);


                if(user == null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);

                    if(user == null)
                    {
						user = new ApplicationUser()
						{
							UserName = model.UserName,
							Email = model.Email,
							FName = model.FirstName,
							LName = model.LastName,
							IsAgree = model.IsAgree,
							NationalNumber = model.NationalNumber,
							PhoneNumber = model.PhoneNumber,
						};

						var result = await _userManager.CreateAsync(user, model.Password);

						if(result.Succeeded)
						{
							return RedirectToAction(nameof(SignIn));
						}
						else
						{
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
						}
					}
                }
				ModelState.AddModelError(string.Empty, errorMessage: "User  Already Exists");
			}
			return View(model);
		}


		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}

		
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);

				if (user != null)
				{
					var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

					if (passwordValid)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsAgree, lockoutOnFailure: false);

						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}
			}
			ModelState.AddModelError(string.Empty, "Invalid login attempt.");

			return View(model);
		}


		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn", "Account");
		}

		public IActionResult ForgerPassword()
		{
			return View();
		}

		public async Task< IActionResult > SendResetPasswordUrl(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					
					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					var url =  Url.Action("ResetPassword", "Account", new
					{
						email = model.Email,
						token = token
					},
					Request.Scheme);



					var email = new Email()
					{
						Subject = "Reset Your Password "  , 
						Body = url, 
						Receipts = model.Email
					};

					EmailSettings.SendEmail(email);


					return RedirectToAction(nameof(CheckYourInbox));
				}

				ModelState.AddModelError(string.Empty, "Invalid Email");
			}

			return View(nameof(ForgerPassword) , model);
		}




		public IActionResult CheckYourInbox()
		{
			return View();
		}

		                                    // Catch From Url
		public IActionResult ResetPassword( string email , string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}


		[HttpPost]
		public async Task< IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if(ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;

				var user = await  _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
					var result =  await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError(string.Empty, item.Description);

					}
                }
				ModelState.AddModelError(string.Empty, "Invalid Operation");

            }

			return View(model);
		}



		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}








    }
}
