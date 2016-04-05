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
using PayBayService.Common;
using System.Data.SqlClient;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using PayBayService.Models.BlobStorage;
using PayBayService.Models.Products;

namespace PayBayService.Controllers
{
    public class ProductsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();
                
        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public HttpResponseMessage GetProducts(int id, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var proId = new SqlParameter("@ProductId", id);
                if (type == 0)
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetMoreProduct", CommandType.StoredProcedure, ref Methods.err, proId);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewProduct", CommandType.StoredProcedure, ref Methods.err, proId);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public HttpResponseMessage GetProductFollowName(int id, string name, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var productId = new SqlParameter("@ProductId", id);
                var productName = new SqlParameter("@ProductName", name);
                if (type == TYPE.OLD)
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetProductWithName", CommandType.StoredProcedure, ref Methods.err, productId, productName);
                }
                else
                {    
                    /////NOTICE                
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_LoadNewProductWithName", CommandType.StoredProcedure, ref Methods.err, productId, productName);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public HttpResponseMessage GetProductOfStore(int storeId, int productId, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var store = new SqlParameter("@StoreID", storeId);
                var product = new SqlParameter("@ProductId", productId);
                if(type == TYPE.OLD)
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetProductOfStore", CommandType.StoredProcedure, ref Methods.err, store, product);
                else
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewProductOfStore", CommandType.StoredProcedure, ref Methods.err, store, product);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Products/
        //[ResponseType(typeof(HttpResponseMessage))]
        //public HttpResponseMessage GetProductFollowType(TYPE typeProduct)
        //{
        //    JArray result = new JArray();
        //    try
        //    {
        //        if (typeProduct == TYPE.NEW)
        //            result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewProduct", CommandType.StoredProcedure, ref Methods.err);                
        //        else
        //            result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetBestSaleProduct", CommandType.StoredProcedure, ref Methods.err);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public HttpResponseMessage GetProductOfStoreOwner(ProductInfo request)
        {
            JArray result = new JArray();
            try
            {
                var owner = new SqlParameter("@OwnerID", request.OwnerID);
                var product = new SqlParameter("@ProductID", request.ProductID);
                if(request.Type == TYPE.OLD)
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetProductOfOwner", CommandType.StoredProcedure, ref Methods.err, owner, product);
                else
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetMoreProductOfOwner", CommandType.StoredProcedure, ref Methods.err, owner, product);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutProduct(Product product)
        {
            JObject result = new JObject();            
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (product.Image == null || Methods.CheckExpiredDateOfSasQuery(product.SasQuery))
                {
                    ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("products", product.ProductName, product.ProductId);

                    if (blob != null)
                    {
                        product.Image = blob.ImageUri;
                        product.SasQuery = blob.SasQuery;
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            result = JObject.FromObject(product);
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
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_UpdateNumOfProduct", CommandType.StoredProcedure, ref Methods.err, product, number, importdate);
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (product.Image == null || Methods.CheckExpiredDateOfSasQuery(product.SasQuery))
                {
                    var table = new SqlParameter("@table", "viethung_paybayservice.Products");
                    int productId = Convert.ToInt32(Methods.GetInstance().GetValue("viethung_paybayservice.sp_GetMaxId", CommandType.StoredProcedure, ref Methods.err, table));
                    ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("products", product.ProductName, productId + 1);

                    if (blob != null)
                    {
                        product.Image = blob.ImageUri;
                        product.SasQuery = blob.SasQuery;
                    }
                }
                db.Products.Add(product);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }                

            result = JObject.FromObject(product);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteProduct(int productId)
        {
            JObject result = new JObject();
            Product product = await db.Products.FindAsync(productId);
            if (product == null)
            {
                result = Methods.CustomResponseMessage(0, "Product isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            if (product.Image != null && Methods.CheckExpiredDateOfSasQuery(product.SasQuery))
            {
                ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("products", product.ProductName, product.ProductId);

                if (blob != null)
                {
                    product.Image = blob.ImageUri;
                    product.SasQuery = blob.SasQuery;
                }
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            //result = Methods.CustomResponseMessage(1, "Delete product is successful!");
            result = JObject.FromObject(product);
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