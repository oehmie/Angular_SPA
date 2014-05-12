using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Angular_SPA.Authorization {
   public class AuthorizationContext : DbContext {
      public AuthorizationContext()
         : base("AuthorizationConnection") {

      }

      public static AuthorizationContext Create() {
         return new AuthorizationContext();
      }


      //protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingEntitySetNameConvention>();
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
      //   modelBuilder.Conventions.Add<TableNameConvention>();
      //}

      public DbSet<WebUser> WebUserSet { get; set; }

   }
}