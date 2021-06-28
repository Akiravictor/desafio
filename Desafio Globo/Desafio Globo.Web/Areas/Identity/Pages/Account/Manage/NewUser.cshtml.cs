using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
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
    public class NewUserModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public NewUserModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public SelectList RoleList { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Role")]
            public string Role { get; set; }

        }

        public IActionResult OnGet()
		{
            RoleList = new SelectList(_roleManager.Roles.Select(r => r.Name).ToList());

            return Page();
		}

        public async Task<IActionResult> OnPostAsync()
        {
            string returnUrl = Url.Content("~/Identity/Account/Manage/Index");

            if (ModelState.IsValid)
            {
                IdentityResult result = null;
                IdentityUser user = null;

                var findRole = await _roleManager.FindByNameAsync(Input.Role);


                if (findRole == null)
                {
                    result = await _roleManager.CreateAsync(new IdentityRole(Input.Role));

                }

                user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(user, Input.Role);
                }


                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
