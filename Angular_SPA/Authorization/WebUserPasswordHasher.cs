using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Angular_SPA.Authorization {
   public class WebUserPasswordHasher : IPasswordHasher {
      public string HashPassword(string password) {
         return password;
      }

      public PasswordVerificationResult VerifyHashedPassword
                    (string hashedPassword, string providedPassword) {
         if (hashedPassword == HashPassword(providedPassword))
            return PasswordVerificationResult.Success;
         else
            return PasswordVerificationResult.Failed;
      }

   }
}