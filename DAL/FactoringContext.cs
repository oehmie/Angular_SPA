using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using Angular_SPA.DAL.Models;



namespace Angular_SPA.DAL {


   public class FactoringContext : DbContext {
      public FactoringContext()
         : base("FactoringConnection") {

      }

      //protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingEntitySetNameConvention>();
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
      //   modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();
      //   modelBuilder.Conventions.Add<TableNameConvention>();
      //}

      public DbSet<Kooperationspartner> KooperationspartnerSet { get; set; }
      public DbSet<WebUser> WebUserSet { get; set; }

   }
}