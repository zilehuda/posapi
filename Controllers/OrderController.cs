using PosApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PosApi.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        DAL dal = new DAL();
        [HttpGet]
        public IEnumerable<Products> GetProduct(string id)
        {

            return dal.GetSingleProduct(id);
        }
        [Route("sales")]
        [HttpPost]
        public List<Sales> GetSales([FromBody]OrderLog order)
        {

            return dal.GetSales(order.date);
        }

        [Route("getorders")]
        [HttpGet]
        public IEnumerable<OrderLog> getOrders()
        {

            return dal.getOrders();
        }


        [Route("addorder")]
        [HttpPost]
        public HttpResponseMessage AddOrder([FromBody]Orders order)
        {
            Object obj = order;
            
            try
            {
                
                if (dal.InsertOrder(order))
                {
                    int qty = dal.GetUpdatedQty(Convert.ToInt32(order.qty), order.productId);
                    bool a = dal.UpdateProductQty(order.productId,qty.ToString());
                    if(!a)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotModified);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, order);
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
