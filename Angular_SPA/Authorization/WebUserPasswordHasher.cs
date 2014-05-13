using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Angular_SPA.Authorization {


   /// <summary>
   /// Ein eigener Passwordhasher, um kompatibel zur alten Version zu sein.
   /// </summary>
   public class WebUserPasswordHasher : IPasswordHasher {


      public string HashPassword(string password) {
         
         MD5CryptoServiceProvider HashMD5 = new MD5CryptoServiceProvider();
         return Convert.ToBase64String(HashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password)));
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