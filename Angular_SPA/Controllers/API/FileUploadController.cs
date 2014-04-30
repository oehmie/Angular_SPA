using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Angular_SPA.Classes;

namespace Angular_SPA.Controllers.API {


   [RoutePrefix("api/fileupload")]
   public class FileUploadController : ApiController {


      // GET api/<controller>
      public IEnumerable<string> Get() {
         return new string[] { "value1", "value2" };
      }

      // GET api/<controller>/5
      public string Get(int id) {
         return "value";
      }

      // POST api/<controller>
      //public void Post([FromBody]string value) {
      //}

      //Alte Fileupload Routine
      //[HttpPost]
      //public HttpResponseMessage Post() {
      //   HttpResponseMessage result = null;
      //   var httpRequest = HttpContext.Current.Request;
      //   if (httpRequest.Files.Count > 0) {
      //      foreach (string file in httpRequest.Files) {
      //         var postedFile = httpRequest.Files[file];
      //         //var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
      //         //postedFile.SaveAs(filePath);
      //      }
      //      result = Request.CreateResponse(HttpStatusCode.Created, "XXX");
      //   }
      //   else {
      //      result = Request.CreateResponse(HttpStatusCode.BadRequest);
      //   }

      //   return result;
      //}

      [HttpPost]
      public async Task<HttpResponseMessage> Post() {
         // Check if the request contains multipart/form-data.
         if (!Request.Content.IsMimeMultipartContent()) {
            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
         }

         string root = HttpContext.Current.Server.MapPath("~/App_Data");
         var provider = new CustomMultipartFormDataStreamProvider(root);

         try {
            // Read the form data.
            await Request.Content.ReadAsMultipartAsync(provider);

            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData) {
               
            }
            return Request.CreateResponse(HttpStatusCode.OK);
         }
         catch (System.Exception e) {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
         }
      }




      [HttpPost, Route("api/upload")]
      public async Task<IHttpActionResult> Upload() {
         if (!Request.Content.IsMimeMultipartContent())
            throw new Exception(); // divided by zero

         var provider = new MultipartMemoryStreamProvider();
         await Request.Content.ReadAsMultipartAsync(provider);
         foreach (var file in provider.Contents) {
            var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
            var buffer = await file.ReadAsByteArrayAsync();
            //Do whatever you want with filename and its binaray data.
         }

         return Ok();
      }

      // PUT api/<controller>/5
      public void Put(int id, [FromBody]string value) {
      }

      // DELETE api/<controller>/5
      public void Delete(int id) {
      }
   }
}