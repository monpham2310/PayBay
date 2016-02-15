using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PayBayService.Models;
using Newtonsoft.Json.Linq;
using PayBayService.App_Code;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using PayBayService.Models.BlobStorage;

namespace PayBayService.Controllers
{
    public class ProductsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public HttpResponseMessage GetProductOfStore(int storeId)
        {
            JArray result = new JArray();
            try
            {
                var store = new SqlParameter("@StoreID", storeId);
                result = Methods.ExecQueryWithResult("paybayservice.sp_GetProductOfStore", CommandType.StoredProcedure, ref Methods.err, store);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Products/
        //[ResponseType(typeof(HttpResponseMessage))]
        //public HttpResponseMessage GetProductFollowType(int typeProduct)
        //{
        //    JArray result = new JArray();
        //    try
        //    {
        //        if (typeProduct == (int)Methods.TypeProduct.NEW)
        //            result = Methods.ExecQueryWithResult("paybayservice.sp_GetNewProduct", CommandType.StoredProcedure, ref Methods.err);
        //        else if (typeProduct == (int)Methods.TypeProduct.SALE)
        //            result = Methods.ExecQueryWithResult("paybayservice.sp_GetSaleProduct", CommandType.StoredProcedure, ref Methods.err);
        //        else
        //            result = Methods.ExecQueryWithResult("paybayservice.sp_GetBestSaleProduct", CommandType.StoredProcedure, ref Methods.err);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        // PUT: api/Products/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutProduct(Product product)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    result = Methods.CustomResponseMessage(0, "Product isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Update product is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Products/NumberOf
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutNumberOfProduct(int productid, int numberof)
        {
            JArray result = new JArray();
            try
            {
                var product = new SqlParameter("@ProductID", productid);
                var number = new SqlParameter("@NumberOf", numberof);
                var importdate = new SqlParameter("@ImportDate", DateTime.Now);
                result = Methods.ExecQueryWithResult("paybayservice.sp_UpdateNumOfProduct", CommandType.StoredProcedure, ref Methods.err, product, number, importdate);
            }
            catch (Exception ex)
            {
                var error = Methods.CustomResponseMessage(0, ex.Message.ToString());
                result.Add(error);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
                
        // POST: api/Products
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostProduct(Product product)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int productId = (int)Methods.GetValue("paybayservice.sp_GetMaxProductId", CommandType.StoredProcedure, ref Methods.err);
            ModelBlob blob = await Methods.GetSasAndImageUriFromBlob("products", product.ProductName, productId);
                        
            if (blob != null)
            {
                product.Image = blob.ImageUri;
                product.SasQuery = blob.SasQuery;
                db.Products.Add(product);
                await db.SaveChangesAsync();
            }
            else
            {
                result = Methods.CustomResponseMessage(0, "Could not retrieve Sas and Uri settings!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = JObject.FromObject(product);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            JObject result = new JObject();
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                result = Methods.CustomResponseMessage(0, "Product isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete product is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}