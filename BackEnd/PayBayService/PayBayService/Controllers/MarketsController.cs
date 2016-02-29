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
using PayBayService.Models.BlobStorage;
using System.Data.SqlClient;

namespace PayBayService.Controllers
{
    public class MarketsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();
                
        // GET: api/Markets/5
        [ResponseType(typeof(Market))]
        public HttpResponseMessage GetMoreMarket(int id, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var marketId = new SqlParameter("@MarketId", id);
                if (type == TYPE.OLD)
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMoreMarket", CommandType.StoredProcedure, ref Methods.err, marketId);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetNewMarket", CommandType.StoredProcedure, ref Methods.err, marketId);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Markets/BenThanh
        [ResponseType(typeof(Market))]
        public HttpResponseMessage GetMarket(int id, string name, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var marketId = new SqlParameter("@MarketId", id);
                var marketName = new SqlParameter("@MarketName", name);
                if (type == TYPE.OLD)
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMarketWithName", CommandType.StoredProcedure, ref Methods.err, marketId, marketName);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMarketWithName", CommandType.StoredProcedure, ref Methods.err, marketId, marketName);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Markets/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutMarket(Market market)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
                        
            db.Entry(market).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarketExists(market.MarketId))
                {
                    result = Methods.CustomResponseMessage(0, "Market isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Update market is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Markets
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostMarket(Market market)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var table = new SqlParameter("@table", "paybayservice.Markets");
            int marketId = Convert.ToInt32(Methods.GetInstance().GetValue("paybayservice.sp_GetMaxId", CommandType.StoredProcedure, ref Methods.err, table));
            ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("markets", market.MarketName, marketId + 1);

            if (blob != null)
            {
                market.Image = blob.ImageUri;
                market.SasQuery = blob.SasQuery;
                db.Markets.Add(market);
                await db.SaveChangesAsync();

                result = JObject.FromObject(market);
            }
            else
            {
                result = Methods.CustomResponseMessage(0, "Could not retrieve Sas and Uri settings!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Markets/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteMarket(int id)
        {
            JObject result = new JObject();
            Market market = await db.Markets.FindAsync(id);
            if (market == null)
            {
                result = Methods.CustomResponseMessage(0, "Market isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.Markets.Remove(market);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete market is successful!");
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

        private bool MarketExists(int id)
        {
            return db.Markets.Count(e => e.MarketId == id) > 0;
        }
    }
}