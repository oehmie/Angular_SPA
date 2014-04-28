using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Angular_SPA.DAL.Attribute {

   // Singleuse attribute.
   [System.AttributeUsage(System.AttributeTargets.Property,
                          AllowMultiple = false)
   ]
   public class SortNameAttribute : System.Attribute {

      string sortName;

      public SortNameAttribute(string sortName) {
         this.sortName = sortName.ToLower();
      }

      public string GetSortName() {
         return sortName;
      }


      public static string GetFieldName(Type t, string sortField) {

         sortField = sortField.ToLower();

         string fieldName = "";
         string keyField = "";

         //Alle Public Properties der übergebenen Klasse holen
         PropertyInfo[] propertyInfos;
         propertyInfos = t.GetProperties();//BindingFlags.Public | BindingFlags.DeclaredOnly);
         foreach (PropertyInfo pi in propertyInfos) {

            IEnumerable<System.Attribute> attrs = pi.GetCustomAttributes();
            foreach (System.Attribute attr in attrs) {
               if ((attr is SortNameAttribute) && (((SortNameAttribute)attr).GetSortName() == sortField)) {
                  fieldName = pi.Name;
               }
               if (attr is KeyAttribute) {
                  keyField = pi.Name;
               }
            }

         }
         if (!String.IsNullOrWhiteSpace(fieldName))
            return fieldName;
         else
            return keyField;
      }
   }

}
