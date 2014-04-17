using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Angular_SPA.Domain.Manager;
using Angular_SPA.Domain.Models;

namespace Angular_SPA.Controllers.API
{
    public class KooperationspartnerController : ApiController
    {


        // GET api/<controller>
        public List<Kooperationspartner> Get()
        {
           KooperationspartnerManager manager = new KooperationspartnerManager();
           return manager.GetKooperationspartner();
           //return new string[] { "value1", "value2", "Ende der Liste" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
