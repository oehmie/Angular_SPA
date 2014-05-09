using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Angular_SPA.Models {


   // Modelle, die als Parameter für AccountController-Aktionen verwendet werden.

   public class AddExternalLoginBindingModel {
      [Required]
      [Display(Name = "Externes Zugriffstoken")]
      public string ExternalAccessToken { get; set; }
   }

   public class ChangePasswordBindingModel {
      [Required]
      [DataType(DataType.Password)]
      [Display(Name = "Aktuelles Kennwort")]
      public string OldPassword { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "\"{0}\" muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Neues Kennwort")]
      public string NewPassword { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Neues Kennwort bestätigen")]
      [Compare("NewPassword", ErrorMessage = "Das neue Kennwort stimmt nicht mit dem Bestätigungskennwort überein.")]
      public string ConfirmPassword { get; set; }
   }

   public class RegisterBindingModel {
      [Required]
      [Display(Name = "Benutzername")]
      public string UserName { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "\"{0}\" muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Kennwort")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Kennwort bestätigen")]
      [Compare("Password", ErrorMessage = "Das Kennwort stimmt nicht mit dem Bestätigungskennwort überein.")]
      public string ConfirmPassword { get; set; }
   }

   public class RegisterExternalBindingModel {
      [Required]
      [Display(Name = "Benutzername")]
      public string UserName { get; set; }
   }

   public class RemoveLoginBindingModel {
      [Required]
      [Display(Name = "Anmeldeanbieter")]
      public string LoginProvider { get; set; }

      [Required]
      [Display(Name = "Anbieterschlüssel")]
      public string ProviderKey { get; set; }
   }

   public class SetPasswordBindingModel {
      [Required]
      [StringLength(100, ErrorMessage = "\"{0}\" muss mindestens {2} Zeichen lang sein.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Neues Kennwort")]
      public string NewPassword { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Neues Kennwort bestätigen")]
      [Compare("NewPassword", ErrorMessage = "Das neue Kennwort stimmt nicht mit dem Bestätigungskennwort überein.")]
      public string ConfirmPassword { get; set; }
   }
}
