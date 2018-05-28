using Newtonsoft.Json.Linq;
using PosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosApi.Controllers
{
    [RoutePrefix("api/vendor")]
    public class VendorController : ApiController
    {
        DAL dal = new DAL();
        public IEnumerable<Vendors> GetVendors()
        {
            return dal.getVendors();
        }

        public IEnumerable<Vendors> GetSingleVendors(string id)
        {
            return dal.GetSingleVendor(id);
        }

        [Route("insertvendor")]
        [HttpPost]
        public HttpResponseMessage InsertVendor([FromBody]Vendors v)
        {
            Object obj = v;//
            try
            {

                if (dal.InsertVendor(v))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, v);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.NotModified);
                }

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, obj);
            }
        }
    }
}