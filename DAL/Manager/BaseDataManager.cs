using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angular_SPA.DAL.Manager {


   /// <summary>
   /// Basisklasse für alle DataManager / mit EF 6.0
   /// </summary>

   abstract public class BaseDataManager {

      // Eine Instanz des Framework-Kontextes pro Manager-Instanz
      protected FactoringContext context;
      protected bool DisposeContext = true;
      protected bool preloading = false;
      protected bool lazyLoading = false;
      protected bool trackingDefault = false;

      /// <summary>
      /// Log-Properties nach außen durchreichen
      /// Erwartet ja nur Methode, die string erwartet --> Kein Architekturproblem
      /// </summary>
      public Action<string> Log {
         get {
            return context.Database.Log;
         }
         set {
            context.Database.Log = value;
         }
      }

      protected void Init(bool lazyLoading = false, bool tracking = false, FactoringContext context = null) {
         this.lazyLoading = lazyLoading;
         this.trackingDefault = tracking;
         // Falls ein Kontext hineingereicht wurde, nehme diesen!
         if (context != null) { this.context = context; DisposeContext = false; }
         else { this.context = new FactoringContext(); }

         // (De-)aktiviert das transparent (automatische) Nachladen verbundener Objekte
         this.context.Configuration.LazyLoadingEnabled = lazyLoading;
         // ... dafür braucht man aber auch Proxies!!!
         this.context.Configuration.ProxyCreationEnabled = lazyLoading;

         //AutoDetectChangesEnabled kann man ggf. abschalten, wenn man Proxies verwendet und alle Properties virtual sind
         //this.ctx.Configuration.AutoDetectChangesEnabled = false;
      }

      /// <summary>
      /// DataManager vernichten (vernichtet auch den EF-Kontext)
      /// </summary>
      public void Dispose() {
         // Falls der Kontext von außen hineingereicht wurde, darf man nicht Dispose() aufrufen.
         // Das ist dann Sache des Aufrufers
         if (DisposeContext) context.Dispose();
      }

      /// <summary>
      /// Grundabfrage mit NoTracking-Optionen und optionalen Includes
      /// </summary>
      protected DbQuery<TEntity> Query<TEntity>(bool? Tracking = null, List<string> includes = null)
       where TEntity : class {
         DbQuery<TEntity> q;
         if (Tracking.GetValueOrDefault() == true || trackingDefault) {
            // Tracking erwünscht
            q = context.Set<TEntity>();
         }
         else {
            // Tracking nicht erwünscht
            q = context.Set<TEntity>().AsNoTracking();
         }
         if (includes != null) {
            // Eager Loading?
            foreach (var include in includes) {
               q = q.Include(include);
            }
         }
         return q;
      }

      /// <summary>
      /// Die neu hinzugefügten Objekte muss die Speichern-Routine wieder zurückgeben, da die IDs für die 
      /// neuen Objekte erst beim Speichern von der Datenbank vergeben werden
      /// </summary>
      protected List<TEntity> Save<TEntity>(List<TEntity> menge, out string Statistik)
      where TEntity : class {
         var neue = new List<TEntity>();

         // Änderungen für jeden einzelnen Passagier übernehmen
         foreach (dynamic p in menge) {
            // Anfügen an diesen Kontext
            context.Set<TEntity>().Attach((TEntity)p);
            if (p.Id == 0) {
               context.Entry(p).State = EntityState.Added;
               // Neue Datensätze merken, da diese nach Speichern zurückgegeben werden müssen (haben dann erst ihre IDs!)
               neue.Add(p);
            }
            else {
               context.Entry(p).State = EntityState.Modified;
            }
         }

         // Statistik der Änderungen zusammenstellen
         Statistik = GetStatistik<TEntity>();

         // Änderungen speichern
         var e = context.SaveChanges();

         return neue;
      }


      /// <summary>
      /// Liefert Informationen über ChangeTracker-Status als Zeichenkette
      /// </summary>
      protected string GetStatistik<TEntity>() 
         where TEntity : class {

         string Statistik = "";
         Statistik += "Geändert: " + context.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Modified).Count();
         Statistik += " Neu: " + context.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Added).Count();
         Statistik += " Gelöscht: " + context.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Deleted).Count();
         return Statistik;
      }
   }


}
