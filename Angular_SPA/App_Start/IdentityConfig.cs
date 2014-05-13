﻿using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;

using Angular_SPA.Providers;
using Angular_SPA.Authorization;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace Angular_SPA.Models {


	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

	public class ApplicationUserManager : UserManager<WebUser> {
		public ApplicationUserManager(IUserStore<WebUser> store)
			: base(store) {
		}

		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) {
			
         var manager = new ApplicationUserManager(new WebUserStore(context.Get<AuthorizationContext>()));

         //Passwordhasher zuordnen
         manager.PasswordHasher = new WebUserPasswordHasher();

			// Configure validation logic for usernames
         //manager.UserValidator = new UserValidator<WebUser>(manager) {
         //   AllowOnlyAlphanumericUserNames = false,
         //   RequireUniqueEmail = true
         //};

			// Configure validation logic for passwords
         //manager.PasswordValidator = new PasswordValidator {
         //   RequiredLength = 6,
         //   RequireNonLetterOrDigit = true,
         //   RequireDigit = true,
         //   RequireLowercase = true,
         //   RequireUppercase = true,
         //};
			// Configure user lockout defaults

         //manager.UserLockoutEnabledByDefault = true;
         //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
         //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug in here.
			manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<WebUser> {
				MessageFormat = "Your security code is: {0}"
			});
			manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<WebUser> {
				Subject = "SecurityCode",
				BodyFormat = "Your security code is {0}"
			});
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null) {
				manager.UserTokenProvider = new DataProtectorTokenProvider<WebUser>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}

		/// <summary>
		/// Method to add user to multiple roles
		/// </summary>
		/// <param name="userId">user id</param>
		/// <param name="roles">list of role names</param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> AddUserToRolesAsync(string userId, IList<string> roles) {
			var userRoleStore = (IUserRoleStore<WebUser, string>)Store;

			var user = await FindByIdAsync(userId).ConfigureAwait(false);
			if (user == null) {
				throw new InvalidOperationException("Invalid user Id");
			}

			var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
			// Add user to each role using UserRoleStore
			foreach (var role in roles.Where(role => !userRoles.Contains(role))) {
				await userRoleStore.AddToRoleAsync(user, role).ConfigureAwait(false);
			}

			// Call update once when all roles are added
			return await UpdateAsync(user).ConfigureAwait(false);
		}

		/// <summary>
		/// Remove user from multiple roles
		/// </summary>
		/// <param name="userId">user id</param>
		/// <param name="roles">list of role names</param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, IList<string> roles) {
			var userRoleStore = (IUserRoleStore<WebUser, string>)Store;

			var user = await FindByIdAsync(userId).ConfigureAwait(false);
			if (user == null) {
				throw new InvalidOperationException("Invalid user Id");
			}

			var userRoles = await userRoleStore.GetRolesAsync(user).ConfigureAwait(false);
			// Remove user to each role using UserRoleStore
			foreach (var role in roles.Where(userRoles.Contains)) {
				await userRoleStore.RemoveFromRoleAsync(user, role).ConfigureAwait(false);
			}

			// Call update once when all roles are removed
			return await UpdateAsync(user).ConfigureAwait(false);
		}
	}

	// Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
	public class ApplicationRoleManager : RoleManager<IdentityRole> {
		public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
			: base(roleStore) {
		}

		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context) {
         var manager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<AuthorizationContext>()));

			return manager;
		}
	}

	

	public class SmsService : IIdentityMessageService {
		public Task SendAsync(IdentityMessage message) {
			// Plug in your sms service here to send a text message.
			return Task.FromResult(0);
		}
	}

	// This is useful if you do not want to tear down the database each time you run the application.
	//public class ApplicationDbInitializer : DropCreateDatabaseAlways<AppContext> {
	// This example shows you how to create a new database if the Model changes
   public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<AuthorizationContext> {
      protected override void Seed(AuthorizationContext context) {
			InitializeIdentityForEF(context);
			base.Seed(context);
		}

		//Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
      public static void InitializeIdentityForEF(AuthorizationContext db) {
			var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
			var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
			const string name = "admin@admin.com";
			const string password = "Admin@123456";
			const string roleName = "Admin";

			//Create Role Admin if it does not exist
			var role = roleManager.FindByName(roleName);
			if (role == null) {
				role = new IdentityRole(roleName);
				var roleresult = roleManager.Create(role);
			}

			var user = userManager.FindByName(name);
			if (user == null) {
				user = new WebUser { UserName = name, Email = name };
				var result = userManager.Create(user, password);
				result = userManager.SetLockoutEnabled(user.Id, false);
			}

			// Add user admin to Role Admin if not already added
			var rolesForUser = userManager.GetRoles(user.Id);
			if (!rolesForUser.Contains(role.Name)) {
				var result = userManager.AddToRole(user.Id, role.Name);
			}
		}
	}

	public enum SignInStatus {
		Success,
		LockedOut,
		RequiresTwoFactorAuthentication,
		Failure
	}

	// These help with sign and two factor (will possibly be moved into identity framework itself)
	public class SignInHelper {
		public SignInHelper(ApplicationUserManager userManager, IAuthenticationManager authManager) {
			UserManager = userManager;
			AuthenticationManager = authManager;
		}

		public ApplicationUserManager UserManager { get; private set; }
		public IAuthenticationManager AuthenticationManager { get; private set; }

		public async Task SignInAsync(WebUser user, bool isPersistent, bool rememberBrowser) {
			// Clear any partial cookies from external or two factor partial sign ins
			AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
			
			var oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager);
			var cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
					CookieAuthenticationDefaults.AuthenticationType);
			var properties = ApplicationOAuthProvider.CreateProperties(user.AppUserName);//, isPersistent);
			
			if (rememberBrowser) {
				var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
				AuthenticationManager.SignIn(properties, oAuthIdentity, cookieIdentity, rememberBrowserIdentity);
			} else {
				AuthenticationManager.SignIn(properties, oAuthIdentity, cookieIdentity);
			}
		}

		public async Task<bool> SendTwoFactorCode(string provider) {
			var userId = await GetVerifiedUserIdAsync();
			if (userId == null) {
				return false;
			}

			var token = await UserManager.GenerateTwoFactorTokenAsync(userId, provider);
			// See IdentityConfig.cs to plug in Email/SMS services to actually send the code
			await UserManager.NotifyTwoFactorTokenAsync(userId, provider, token);
			return true;
		}

		public async Task<string> GetVerifiedUserIdAsync() {
			var result = await AuthenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie);
			if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId())) {
				return result.Identity.GetUserId();
			}
			return null;
		}

		public async Task<bool> HasBeenVerified() {
			return await GetVerifiedUserIdAsync() != null;
		}

		public async Task<SignInStatus> TwoFactorSignIn(string provider, string code, bool isPersistent, bool rememberBrowser) {
			var userId = await GetVerifiedUserIdAsync();
			if (userId == null) {
				return SignInStatus.Failure;
			}
			var user = await UserManager.FindByIdAsync(userId);
			if (user == null) {
				return SignInStatus.Failure;
			}
			if (await UserManager.IsLockedOutAsync(user.Id)) {
				return SignInStatus.LockedOut;
			}
			if (await UserManager.VerifyTwoFactorTokenAsync(user.Id, provider, code)) {
				// When token is verified correctly, clear the access failed count used for lockout
				await UserManager.ResetAccessFailedCountAsync(user.Id);
				await SignInAsync(user, isPersistent, rememberBrowser);
				return SignInStatus.Success;
			}
			// If the token is incorrect, record the failure which also may cause the user to be locked out
			await UserManager.AccessFailedAsync(user.Id);
			return SignInStatus.Failure;
		}

		public async Task<SignInStatus> ExternalSignIn(ExternalLoginInfo loginInfo, bool isPersistent) {
			var user = await UserManager.FindAsync(loginInfo.Login);
			if (user == null) {
				return SignInStatus.Failure;
			}
			if (await UserManager.IsLockedOutAsync(user.Id)) {
				return SignInStatus.LockedOut;
			}
			return await SignInOrTwoFactor(user, isPersistent);
		}

		private async Task<SignInStatus> SignInOrTwoFactor(WebUser user, bool isPersistent) {
			if (await UserManager.GetTwoFactorEnabledAsync(user.Id) &&
				!await AuthenticationManager.TwoFactorBrowserRememberedAsync(user.Id)) {
				var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
				identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
				AuthenticationManager.SignIn(identity);
				return SignInStatus.RequiresTwoFactorAuthentication;
			}
			await SignInAsync(user, isPersistent, false);
			return SignInStatus.Success;

		}

		public async Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout) {
			var user = await UserManager.FindByNameAsync(userName);
			if (user == null) {
				return SignInStatus.Failure;
			}
			if (await UserManager.IsLockedOutAsync(user.Id)) {
				return SignInStatus.LockedOut;
			}
			if (await UserManager.CheckPasswordAsync(user, password)) {
				return await SignInOrTwoFactor(user, isPersistent);
			}
			if (shouldLockout) {
				// If lockout is requested, increment access failed count which might lock out the user
				await UserManager.AccessFailedAsync(user.Id);
				if (await UserManager.IsLockedOutAsync(user.Id)) {
					return SignInStatus.LockedOut;
				}
			}
			return SignInStatus.Failure;
		}
	}
}