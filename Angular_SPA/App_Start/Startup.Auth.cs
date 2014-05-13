using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Angular_SPA.Authorization;
using Angular_SPA.Providers;
using Angular_SPA.Models;

namespace Angular_SPA {


   public partial class Startup {


      static Startup() {
         
         
      }

      public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

      public static string PublicClientId { get; private set; }

      // Weitere Informationen zum Konfigurieren der Authentifizierung finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301864".
      public void ConfigureAuth(IAppBuilder app) {

         // Configure the db context, user manager and role manager to use a single instance per request
         app.CreatePerOwinContext(AuthorizationContext.Create);
         app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
         app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);


         // Enable the application to use a cookie to store information for the signed in user
         // and to use a cookie to temporarily store information about a user logging in with a third party login provider
         app.UseCookieAuthentication(new CookieAuthenticationOptions());
         app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

         // Anwendung für die Verwendung eines Trägertokens zum Authentifizieren von Benutzern aktivieren
         PublicClientId = "self";

         OAuthOptions = new OAuthAuthorizationServerOptions {
            TokenEndpointPath = new PathString("/Token"),
            Provider = new ApplicationOAuthProvider(PublicClientId),
            AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            AllowInsecureHttp = true
         };
         app.UseOAuthBearerTokens(OAuthOptions);

         // Auskommentierung der folgenden Zeilen aufheben, um die Anmeldung mit Anmeldeanbietern von Drittanbietern zu ermöglichen
         //app.UseMicrosoftAccountAuthentication(
         //    clientId: "",
         //    clientSecret: "");

         //app.UseTwitterAuthentication(
         //    consumerKey: "",
         //    consumerSecret: "");

         //app.UseFacebookAuthentication(
         //    appId: "",
         //    appSecret: "");

         //app.UseGoogleAuthentication();
      }
   }
}