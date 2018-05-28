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
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        DAL dal = new DAL();
        public IEnumerable<Products> Get()
        {
            return dal.getProducts();
        }

        
        public HttpResponseMessage Post([FromBody]Products product)
        {
            Object obj = product;//
            try
            {

                if (dal.InsertProduct(product))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, product);
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

        [Route("updateqty")]
        [HttpPost]
        public HttpResponseMessage updateqty([FromBody]Products product)
        {
            Object obj = product;
            try
            {

                if (dal.UpdateProductQty(product.id,product.qty))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, product);
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

        [Route("updateprice")]
        [HttpPost]
        public HttpResponseMessage updateprice([FromBody]Products product)
        {
            Object obj = product;
            try
            {

                if (dal.UpdateProductPrice(product.id, product.price))
                {
                     return Request.CreateResponse(HttpStatusCode.OK, product);
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
