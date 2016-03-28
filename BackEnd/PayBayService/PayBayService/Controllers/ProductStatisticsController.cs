using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PayBayService.Models;
using Newtonsoft.Json.Linq;
using PayBayService.Common;
using System.Data.SqlClient;

namespace PayBayService.Controllers
{
    public class ProductStatisticsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/ProductStatistics
        public IQueryable<ProductStatistic> GetProductStatistics()
        {
            return db.ProductStatistics;
        }

        // GET: api/ProductStatistics/5
        [ResponseType(typeof(ProductStatistic))]
        public async Task<IHttpActionResult> GetProductStatistic(int id)
        {
            ProductStatistic productStatistic = await db.ProductStatistics.FindAsync(id);
            if (productStatistic == null)
            {
                return NotFound();
            }

            return Ok(productStatistic);
        }

        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage GetNewProduct(TYPE typeProduct, int productId)
        {
            JArray result = new JArray();
            try
            {
                var product = new SqlParameter("@ProductID", productId);
                if (typeProduct == TYPE.OLD)
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewMechandise", CommandType.StoredProcedure, ref Methods.err, product);
                else
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetMoreNewMechandise", CommandType.StoredProcedure, ref Methods.err, product);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage GetBestSaleProduct(int productId, TYPE typeProduct, bool type)
        {
            JArray result = new JArray();
            try
            {
                var product = new SqlParameter("@ProductID", productId);
                if (typeProduct == TYPE.OLD)
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetBestSaleProduct", CommandType.StoredProcedure, ref Methods.err, product);
                else
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewBestSaleProduct", CommandType.StoredProcedure, ref Methods.err, product);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/ProductStatistics/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutProductStatistic(ProductStatistic productStatistic)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
                        
            db.Entry(productStatistic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatisticExists(productStatistic.Id))
                {
                    result = Methods.CustomResponseMessage(0, "Request isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Put Request is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/ProductStatistics
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostProductStatistic(ProductStatistic productStatistic)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.ProductStatistics.Add(productStatistic);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(0, "Post Request is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/ProductStatistics/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteProductStatistic(int id)
        {
            JObject result = new JObject();
            ProductStatistic productStatistic = await db.ProductStatistics.FindAsync(id);
            if (productStatistic == null)
            {
                result = Methods.CustomResponseMessage(0, "Request isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.ProductStatistics.Remove(productStatistic);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete Request is successful!");
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

        private bool ProductStatisticExists(int id)
        {
            return db.ProductStatistics.Count(e => e.Id == id) > 0;
        }
    }
}