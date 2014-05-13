using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Angular_SPA.Authorization {


   /// <summary>
   /// UserStore für den WebUser
   /// </summary>
   public class WebUserStore : IUserStore<WebUser>, IUserPasswordStore<WebUser>, IUserEmailStore<WebUser> {


      //Datencontext für den Zugriff auf die Datenbank mit den Usern
      protected AuthorizationContext manager;

      public WebUserStore() : this(new AuthorizationContext()){

      }

      public WebUserStore(AuthorizationContext context) {
         manager = context;
      }

      public void Dispose() {
         manager.Dispose();
      }


      //------ IUserStore -----

      public Task CreateAsync(WebUser user) {
         throw new NotImplementedException();
      }

      public Task UpdateAsync(WebUser user) {
         throw new NotImplementedException();
      }

      public Task DeleteAsync(WebUser user) {
         throw new NotImplementedException();
      }


      public Task<WebUser> FindByIdAsync(string userId) {
         //Nur als Dummy, um etwas zurückzuliefern:
         return FindByNameAsync(userId);
      }

      public Task<WebUser> FindByNameAsync(string userName) {


         string pw = "password123";
         MD5CryptoServiceProvider HashMD5 = new MD5CryptoServiceProvider();
         pw = Convert.ToBase64String(HashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pw)));

         WebUser user = new WebUser() {
            UserName = userName,
            //Email = "m.oehmichen@sozialbank.de",
            Email = "markus@oehmie.de",
            EmailConfirmed = true,
            PasswordHash = pw
         };
         return Task<WebUser>.FromResult(user);

         //Task<WebUser> task = context.AppUsers.Where(
         //                      apu => apu.AppUserName == userName)
         //                      .FirstOrDefaultAsync();

         //return task;
      }

      

      
      //--------IUserPasswordStore -------------

      public Task<string> GetPasswordHashAsync(WebUser user) {
         if (user == null) {
            throw new ArgumentNullException("user");
         }

         return Task.FromResult(user.PasswordHash);
      }

      public Task SetPasswordHashAsync(WebUser user, string passwordHash) {
         throw new NotImplementedException();
      }

      public Task<bool> HasPasswordAsync(WebUser user) {
         return Task.FromResult(user.AppPassword != null);
      }

      
      //------ IUserEmailStore ------------

      public Task<WebUser> FindByEmailAsync(string email) {

         //Nur als Dummy, um etwas zurückzuliefern:
         return FindByNameAsync(email);
      }


      public Task<string> GetEmailAsync(WebUser user) {
         return Task.FromResult(user.Email);
      }

      public Task<bool> GetEmailConfirmedAsync(WebUser user) {
         throw new NotImplementedException();
      }

      public Task SetEmailAsync(WebUser user, string email) {
         throw new NotImplementedException();
      }

      public Task SetEmailConfirmedAsync(WebUser user, bool confirmed) {
         throw new NotImplementedException();
      }


      
   }
}