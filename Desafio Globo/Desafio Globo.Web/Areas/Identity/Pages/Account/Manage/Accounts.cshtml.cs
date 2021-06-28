using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desafio_Globo.Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Administrador")]
    public class AccountsModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

		public AccountsModel(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
		{
            this.userManager = userManager;
            this.roleManager = roleManager;
		}

        [BindProperty]
        public InputModel Input { get; set; }

        public List<UserRole> userRoles { get; set; }

        public class InputModel
		{
            public List<UserRole> userRoles { get; set; }
		}

        public class UserRole
		{
            public string UserId { get; set; }
			public string Username { get; set; }

			public string Role { get; set; }
		}

        private async Task LoadPage()
		{
            userRoles = new List<UserRole>();

            var roles = roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                var users = await userManager.GetUsersInRoleAsync(role.Name);

                foreach (var user in users)
                {
                    userRoles.Add(new UserRole
                    {
                        UserId = user.Id,
                        Username = user.UserName,
                        Role = role.Name
                    });
                }
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadPage();

            return Page();
        }

        public async Task<IActionResult> OnPostDelete(string id)
		{
            var user = await userManager.FindByIdAsync(id);

            var roles = await userManager.GetRolesAsync(user);

            await userManager.RemoveFromRolesAsync(user, roles);

            await userManager.DeleteAsync(user);

            await LoadPage();

            return Page();
		}
    }
}
