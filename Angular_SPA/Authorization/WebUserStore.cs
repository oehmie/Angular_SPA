using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Domain.Manager;
using Microsoft.AspNet.Identity;

namespace Angular_SPA.Authorization {


   public class WebUserStore : IUserStore<WebUser>, IUserPasswordStore<WebUser> {


      WebUserManager manager = new WebUserManager();

      public void Dispose() {
         manager.Dispose();
      }

      public Task CreateAsync(WebUser user) {
         throw new NotImplementedException();
      }

      public Task DeleteAsync(WebUser user) {
         throw new NotImplementedException();
      }

      public Task<WebUser> FindByIdAsync(string userId) {
         throw new NotImplementedException();
      }

      public Task<WebUser> FindByNameAsync(string userName) {
         WebUser user = new WebUser() {
            AppPassword = "password123",
            AppUserId = 1,
            AppUserName = "Alice"
         };
         return Task<WebUser>.FromResult(user);

         //Task<WebUser> task = context.AppUsers.Where(
         //                      apu => apu.AppUserName == userName)
         //                      .FirstOrDefaultAsync();

         //return task;
      }

      public Task UpdateAsync(WebUser user) {
         throw new NotImplementedException();
      }

      

      public Task<string> GetPasswordHashAsync(WebUser user) {
         if (user == null) {
            throw new ArgumentNullException("user");
         }

         return Task.FromResult(user.AppPassword);
      }

      public Task<bool> HasPasswordAsync(WebUser user) {
         return Task.FromResult(user.AppPassword != null);
      }

      public Task SetPasswordHashAsync(WebUser user, string passwordHash) {
         throw new NotImplementedException();
      }




   }
}