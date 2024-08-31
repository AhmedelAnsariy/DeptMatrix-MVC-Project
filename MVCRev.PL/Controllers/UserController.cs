using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCRev.DAL.Models;
using MVCRev.PL.Helper;
using MVCRev.PL.Models;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace MVCRev.PL.Controllers
{


    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;

        public UserController( UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }



        public async Task<IActionResult> Index(string searchInput)
        {
            var users = Enumerable.Empty<UserViewModel>();

            if (string.IsNullOrEmpty(searchInput))
            {

                users = await _userManager.Users.Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FirstName = u.FName,
                    LastName = u.FName,
                    Email = u.Email,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToListAsync();



            }
            else
            {
                users = await _userManager.Users.Where(u => u.Email
                                                 .ToLower()
                                                 .Contains(searchInput.ToLower())
                                                 ).Select(u => new UserViewModel
                                                 {
                                                     Id = u.Id,
                                                     FirstName = u.FName,
                                                     LastName = u.FName,
                                                     Email = u.Email,
                                                     Roles = _userManager.GetRolesAsync(u).Result
                                                 }).ToListAsync();
            }


            return View(users);

        }



        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var userfROMdB = await _userManager.FindByIdAsync(id);



            if (userfROMdB is null)
            {
                return NotFound();
            }

            var result = new UserViewModel()
            {
                Id = userfROMdB.Id,
                FirstName = userfROMdB.FName,
                LastName = userfROMdB.FName,
                Email = userfROMdB.Email,
                Roles = _userManager.GetRolesAsync(userfROMdB).Result
            };


            return View(ViewName, result);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                var userfROMdB = await _userManager.FindByIdAsync(id);



                if (userfROMdB is null)
                {
                    return NotFound();
                }

                userfROMdB.FName = model.FirstName;
                userfROMdB.LName = model.LastName;
                userfROMdB.Email = model.Email;

                var count = await _userManager.UpdateAsync(userfROMdB);
               
                    return RedirectToAction(nameof(Index));                
            }

            return View(model);


        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                var userfromDb = await _userManager.FindByIdAsync(id);

                if (userfromDb is null)
                    return NotFound();




                await _userManager.DeleteAsync(userfromDb);

                return RedirectToAction("Index");
            }



            return View(model);
        }
    }
}
