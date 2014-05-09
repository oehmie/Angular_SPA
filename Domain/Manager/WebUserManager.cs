using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Angular_SPA.DAL.Manager;
using Angular_SPA.Domain.Models;
using Angular_SPA.Domain.Classes;
using Angular_SPA.Domain.Extensions;
using Angular_SPA.DAL.Attribute;

namespace Domain.Manager {
   public class WebUserManager: IDisposable {

      WebUserDataManager webUserDataManager = new WebUserDataManager(false);

      public void Dispose() {
         webUserDataManager.Dispose();
      }


      public Task CreateAsync(WebUser user) {
         throw new NotImplementedException();
      }

   }
}
