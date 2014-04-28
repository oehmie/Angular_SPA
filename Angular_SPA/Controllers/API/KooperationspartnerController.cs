using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Angular_SPA.Domain.Manager;
using Angular_SPA.Domain.Models;
using Angular_SPA.Domain.Classes;

namespace Angular_SPA.Controllers.API {

   [RoutePrefix("api/kooperationspartner")]
   public class KooperationspartnerController : ApiController {


      // GET api/<controller>
      //[HttpGet]
      //public List<Kooperationspartner> Get() {
      //   KooperationspartnerManager manager = new KooperationspartnerManager();
      //   return manager.GetKooperationspartner();
      //   //return new string[] { "value1", "value2", "Ende der Liste" };
      //}

      //[HttpGet]
      //public List<Kooperationspartner> Get([FromUri] int page, int pagesize) {
      //   KooperationspartnerManager manager = new KooperationspartnerManager();
      //   return manager.GetKooperationspartner();
      //}

      //[HttpGet]
      //public List<Kooperationspartner> Get([FromUri] int skip, int take, string sort, string filter) {
      //   KooperationspartnerManager manager = new KooperationspartnerManager();
      //   return manager.GetKooperationspartner();
      //}

      [HttpGet]
      public ApiResult<PagedResponse<Kooperationspartner>> Get([FromUri] PagedRequest request) {
         try {
            //throw new Exception("Hat nicht geklappt");
            KooperationspartnerManager manager = new KooperationspartnerManager();
            PagedResponse<Kooperationspartner> data = manager.GetKooperationspartner(request);
            return new ApiResult<PagedResponse<Kooperationspartner>>(data, true);
         }
         catch (Exception ex) {
            return new ApiResult<PagedResponse<Kooperationspartner>>(null, false, ex.Message);
         }
      }

      //// GET api/<controller>/5
      //[HttpGet]
      //public string Get(int id) {
      //   return "value";
      //}

      // POST api/<controller>
      public void Post([FromBody]string value) {
      }

      // PUT api/<controller>/5
      public void Put(int id, [FromBody]string value) {
      }

      // DELETE api/<controller>/5
      public void Delete(int id) {
      }
   }
}
