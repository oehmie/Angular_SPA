using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Angular_SPA.Authorization;
using Angular_SPA.Providers;

namespace Angular_SPA {
   public partial class Startup {
      static Startup() {
         PublicClientId = "self";

         UserManagerFactory = () => new UserManager<WebUser>(new WebUserStore()) { PasswordHasher = new WebUserPasswordHasher() };

         OAuthOptions = new OAuthAuthorizationServerOptions {
            TokenEndpointPath = new PathString("/Token"),
            Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
            AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            AllowInsecureHttp = true
         };
      }

      public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

      public static Func<UserManager<WebUser>> UserManagerFactory { get; set; }

      public static string PublicClientId { get; private set; }

      // Weitere Informationen zum Konfigurieren der Authentifizierung finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301864".
      public void ConfigureAuth(IAppBuilder app) {
         // Enable the application to use a cookie to store information for the signed in user
         // and to use a cookie to temporarily store information about a user logging in with a third party login provider
         app.UseCookieAuthentication(new CookieAuthenticationOptions());
         app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

         // Anwendung für die Verwendung eines Trägertokens zum Authentifizieren von Benutzern aktivieren
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