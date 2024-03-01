using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedAsync(AppIdentityDbContext db , RoleManager<IdentityRole> rolemanager,UserManager<ApplicationUser> usermanager)
		{
			await db.Database.MigrateAsync();

			if (!await rolemanager.RoleExistsAsync(AuthorizationConstants.Roles.ADMINISTRATOR))
			{

				await rolemanager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.ADMINISTRATOR)) ;
			}
			if(!await usermanager.Users.AnyAsync(x=>x.Email== AuthorizationConstants.DEFAULT_ADMIN_USER))
			{
				var adminUser = new ApplicationUser()
				{
					Email = AuthorizationConstants.DEFAULT_ADMIN_USER,
					UserName = AuthorizationConstants.DEFAULT_ADMIN_USER,
					EmailConfirmed = true
				};
				await usermanager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
				await usermanager.AddToRoleAsync(adminUser, AuthorizationConstants.Roles.ADMINISTRATOR);
			}
			if(!await usermanager.Users.AnyAsync(x=>x.Email== AuthorizationConstants.DEFAULT_DEMO_USER))
			{
				var demoUser = new ApplicationUser()
				{
					Email = AuthorizationConstants.DEFAULT_DEMO_USER,
					UserName = AuthorizationConstants.DEFAULT_DEMO_USER,
					EmailConfirmed = true
				};
				await usermanager.CreateAsync(demoUser, AuthorizationConstants.DEFAULT_PASSWORD);
			}
				
		}
	}
}
