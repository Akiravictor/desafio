using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Desafio_Globo.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Administrador")]
    public class EditUserModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;

        public EditUserModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public SelectList RoleList { get; set; }

        public string UserId { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Display(Name = "Role")]
            public string Role { get; set; }

        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = await _userManager.GetRolesAsync(user);

            Input = new InputModel()
            {
                Email = user.UserName,
                Role = role.First()
			};

            UserId = id;

            RoleList = new SelectList(_roleManager.Roles.Select(r => r.Name).ToList());

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            string returnUrl = Url.Content("~/Identity/Account/Manage/Index");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                var role = await _userManager.GetRolesAsync(user);

                if(user.UserName != Input.Email)
				{
                    var result = await _userManager.SetUserNameAsync(user, Input.Email);
                    await _userManager.SetEmailAsync(user, Input.Email);

                    if (result.Succeeded)
                    {
                        return LocalRedirect(returnUrl);

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
				}

                if(role.First() != Input.Role)
				{
                    await _userManager.RemoveFromRoleAsync(user, role.First());
                    var result = await _userManager.AddToRoleAsync(user, Input.Role);

                    if (result.Succeeded)
                    {
                        return LocalRedirect(returnUrl);

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }


            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
