using PosApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PosApi
{
    public  class DAL
    {
        public DBHelper db;
        public string query = "";
        List<Products> plist;
        List<OrderLog> olist;
        List<Vendors> vlist;
        Dictionary<string, string> dic = new Dictionary<string, string>();

        public DAL()
        {
           db = new DBHelper();
        }
        public  List<Products> getProducts()
        {
            query = "select * from Product";
            DataTable dt = db.ExecuteDataTable(query);

            plist = (from DataRow row in dt.Rows
                     select new Products
                     {
                         id = row[0].ToString(),
                         name = row[1].ToString(),
                         qty = row[2].ToString(),
                         price = row[3].ToString(),
                         vendid = row[4].ToString()

                     }).ToList();
            
            return plist;
        }

        public List<Sales> GetSales(string s)
        {

            dic["@date"] = s;
            query = "select p.productName as name,sum(o.qty) as qty,sum(o.total) as total from Product p,OrderLog o where p.productId = o.productId and date = @date group by p.productName";
            DataTable dt = db.ExecuteDataTableParam(query,dic);

            var olist = (from DataRow row in dt.Rows
                     select new Sales
                     {
                         name = row[0].ToString(),
                         qty = row[1].ToString(),
                         total = row[2].ToString(),

                     }).ToList();
            return olist;
        }
        public List<OrderLog> getOrders()
        {
            query = "select * from Orderlog";
            DataTable dt = db.ExecuteDataTable(query);

            olist = (from DataRow row in dt.Rows
                     select new OrderLog
                     {
                         orderid = row[0].ToString(),
                         productid = row[1].ToString(),
                         qty = row[2].ToString(),
                         total = row[3].ToString(),
                         date = row[4].ToString()

                     }).ToList();

            return olist;
        }
        public List<Vendors> getVendors()
        {
            query = "select * from Vendor";
            DataTable dt = db.ExecuteDataTable(query);

            vlist = (from DataRow row in dt.Rows
                     select new Vendors
                     {
                         vendid = row[0].ToString(),
                         vendname = row[1].ToString(),
                    
                     }).ToList();

            return vlist;
        }

        public List<Vendors> GetSingleVendor(string id)
        {

            DBHelper dbhelper = new DBHelper();
            dic["@vendid"] = id;
            string query = "select * from Vendor where vendorId = @vendid";
            DataTable dt = db.ExecuteDataTableParam(query, dic);
            vlist = (from DataRow row in dt.Rows
                     select new Vendors
                     {
                         vendname = row[0].ToString(),
                         vendid = row[1].ToString()

                     }).ToList();

            return vlist;
        }
        public List<Products> GetSingleProduct(string id)
        {

            DBHelper dbhelper = new DBHelper();
            dic["@productId"] = id;
            string query = "select * from Product where productId = @productId";
            DataTable dt = db.ExecuteDataTableParam(query, dic);
            plist = (from DataRow row in dt.Rows
                     select new Products
                     {
                         name = row[1].ToString(),
                         qty = row[2].ToString(),
                         price = row[3].ToString(),
                         vendid = row[4].ToString()

                     }).ToList();

            return plist;
        }
        
        public bool InsertProduct(Products product)
        {
            try
            {
                DBHelper dbhelper = new DBHelper();
                dic["@productName"] = product.name;
                dic["@productQty"] = product.qty;
                dic["@productPrice"] = product.price;
                dic["@vendorId"] = product.vendid;
                string query = "INSERT INTO Product VALUES (@productName,@productQty,@productPrice,@vendorId)";
                bool res = dbhelper.ExecuteNonQuery(query, dic);
                return res;
            }
            catch
            {
                return false;
            }
            
        }
        public bool InsertOrder(Orders order)
        {
            try
            {
                DBHelper dbhelper = new DBHelper();
                dic["@productId"] = order.productId;
                dic["@qty"] = order.qty;
                dic["@total"] = order.total;
              dic["@date"] = DateTime.Now.ToString("dd/MM/yy");
                string query = "INSERT INTO OrderLog VALUES (@productId,@qty,@total,@date)";
                bool res = dbhelper.ExecuteNonQuery(query, dic);
                return res;
            }
            catch
            {
                return false;
            }
        }
        public bool InsertVendor(Vendors v)
        {
            try
            {
                DBHelper dbhelper = new DBHelper();
                
                dic["@vendname"] = v.vendname;
                string query = "INSERT INTO Vendor VALUES (@vendname)";
                bool res = dbhelper.ExecuteNonQuery(query, dic);
                return res;
            }
            catch
            {
                return false;
            }
        }

        public int GetUpdatedQty(int qty,string id)
        {
            DBHelper dbhelper = new DBHelper();
            dic["@productId"] = id;
            string query = "select productQty from Product where productId = @productId";
            DataTable dt = db.ExecuteDataTableParam(query, dic);
            int newqty = Convert.ToInt32(dt.Rows[0]["productQty"]);
            newqty = newqty - qty;
            return newqty;
        }

        public bool UpdateProductQty(string id,string qty)
        {
            try
            {
                DBHelper dbhelper = new DBHelper();
                dic["@id"] = id;
                dic["@qty"] = qty;
                string query = "update Product set productQty = @qty where productId =@id ";
                bool res = dbhelper.ExecuteNonQuery(query, dic);
                return res;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProductPrice(string id, string price)
        {
            try
            {
                DBHelper dbhelper = new DBHelper();
                dic["@id"] = id;
                dic["@price"] = price;
                string query = "update Product set productPrice = @price where productId =@id";
                bool res = dbhelper.ExecuteNonQuery(query, dic);
                return res;
            }
            catch
            {
                return false;
            }
        }
        



        
    }
}