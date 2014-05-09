﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Angular_SPA.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace Angular_SPA.Providers {
   public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider {
      private readonly string _publicClientId;
      private readonly Func<UserManager<WebUser>> _userManagerFactory;

      public ApplicationOAuthProvider(string publicClientId, Func<UserManager<WebUser>> userManagerFactory) {
         if (publicClientId == null) {
            throw new ArgumentNullException("publicClientId");
         }

         if (userManagerFactory == null) {
            throw new ArgumentNullException("userManagerFactory");
         }

         _publicClientId = publicClientId;
         _userManagerFactory = userManagerFactory;
      }

      public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
         using (UserManager<WebUser> userManager = _userManagerFactory()) {
            WebUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null) {
               context.SetError("invalid_grant", "Der Benutzername oder das Kennwort ist falsch.");
               return;
            }
            try {
               ClaimsIdentity oAuthIdentity = await userManager.CreateIdentityAsync(user,
                   context.Options.AuthenticationType);
               ClaimsIdentity cookiesIdentity = await userManager.CreateIdentityAsync(user,
                   CookieAuthenticationDefaults.AuthenticationType);
               AuthenticationProperties properties = CreateProperties(user.UserName);
               AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
               context.Validated(ticket);
               context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            catch (Exception ex) {
               throw;
            }
         }
      }

      public override Task TokenEndpoint(OAuthTokenEndpointContext context) {
         foreach (KeyValuePair<string, string> property in context.Properties.Dictionary) {
            context.AdditionalResponseParameters.Add(property.Key, property.Value);
         }

         return Task.FromResult<object>(null);
      }

      public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
         // Die Kennwortanmeldeinformationen des Ressourcenbesitzers stellen keine Client-ID zur Verfügung.
         if (context.ClientId == null) {
            context.Validated();
         }

         return Task.FromResult<object>(null);
      }

      public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context) {
         if (context.ClientId == _publicClientId) {
            Uri expectedRootUri = new Uri(context.Request.Uri, "/");

            if (expectedRootUri.AbsoluteUri == context.RedirectUri) {
               context.Validated();
            }
         }

         return Task.FromResult<object>(null);
      }

      public static AuthenticationProperties CreateProperties(string userName) {
         IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
         return new AuthenticationProperties(data);
      }
   }
}