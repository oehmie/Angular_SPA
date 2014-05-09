using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Angular_SPA.Startup))]

namespace Angular_SPA {

   public partial class Startup {


      public void Configuration(IAppBuilder app) {
         // Weitere Informationen zm Konfigurieren Ihrer Anwendung finden Sie unter "http://go.microsoft.com/fwlink/?LinkID=316888".
         ConfigureAuth(app);
      }
   }
}
