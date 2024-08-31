using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCRev.DAL.Models;
using MVCRev.PL.Helper;
using MVCRev.PL.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCRev.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        public async Task< IActionResult > Index(string searchInput)
        {
            var roles = Enumerable.Empty<RoleViewModel>();




            if(string.IsNullOrEmpty( searchInput))
            {
                roles = await _roleManager.Roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    RoleName = r.Name,
                }).ToListAsync();
            }
            else
            {
                roles = await _roleManager.Roles.Where(r => r.Name
                                       .ToLower()
                                       .Contains(searchInput.ToLower())
                                       ).Select(u => new RoleViewModel
                                       {
                                           Id = u.Id,
                                         
                                          RoleName = u.Name,
                                         
                                       }).ToListAsync();
            }


           

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }



        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {



                var role = new IdentityRole()
                {
                    Name = model.RoleName,
                };
                await _roleManager.CreateAsync(role);
               
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var role = await _roleManager.FindByIdAsync(id);



            if (role is null)
            {
                return NotFound();
            }



            var response = new RoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name,
               
            };


            return View(ViewName, response);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);



                if (role is null)
                {
                    return NotFound();
                }

                role.Name = model.RoleName;
               

                var count = await _roleManager.UpdateAsync(role);


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

        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }



            if (ModelState.IsValid)
            {

                var role = await _roleManager.FindByIdAsync(id);

                if (role is null)
                    return NotFound();




                await _roleManager.DeleteAsync(role);

                return RedirectToAction("Index");
            }



            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if(role is null)
            {
                return NotFound();
            }


            ViewData["RoleId"] = roleId;

            var UsersInRole = new List<UserInRoleViewModel>();

            var users = await _userManager.Users.ToListAsync();


            foreach (var user in users)
            {
                var UserInRole = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if(await _userManager.IsInRoleAsync(user , role.Name))
                {
                    UserInRole.IsSelected = true;
                }
                else
                {
                    UserInRole.IsSelected = false;
                }
                UsersInRole.Add(UserInRole);
            }

            return View(UsersInRole);


        }






        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);

                    if (appUser is not null)
                    {



                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }

                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);

                        }




                    }


                }

                //return RedirectToAction(nameof(Edit), new { id = roleId });
                return RedirectToAction("Index" , "User");



            }



            return View(users);


        }


    }
}
