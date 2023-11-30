using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class notesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage list(string id, string date_from, string date_until)
        {
            var notes = bl.notes.list(id, date_from, date_until);

            return Request.CreateResponse(HttpStatusCode.OK, notes, Configuration.Formatters.JsonFormatter);
        }
    }
}
