using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Angular_SPA.Authorization {

   public class WebUser : IdentityUser {

      //Existing database fields
      public long AppUserId { get; set; }
      public string AppUserName { get; set; }
      public string AppPassword { get; set; }

      public WebUser() {
         this.Id = Guid.NewGuid().ToString();
      }

      //Keine Ahnung, wofür das gebraucht werden kann
      //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType = OAuthDefaults.AuthenticationType) {
      //   // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      //   var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
      //   // Add custom user claims here
      //   return userIdentity;
      //}


      public override string Id { get; set; }

      public override string UserName {
         get {
            return AppUserName;
         }
         set {
            AppUserName = value;
         }
      }

   }
}