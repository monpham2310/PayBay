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
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetMoreMarket", CommandType.StoredProcedure, ref Methods.err, marketId);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewMarket", CommandType.StoredProcedure, ref Methods.err, marketId);
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
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetMarketWithName", CommandType.StoredProcedure, ref Methods.err, marketId, marketName);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewMarketWithName", CommandType.StoredProcedure, ref Methods.err, marketId, marketName);
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
        public HttpResponseMessage GetAllMarket()
        {
            JArray result = new JArray();
            try
            {
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetAllMarket", CommandType.StoredProcedure, ref Methods.err);                
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (market.Image == null || Methods.CheckExpiredDateOfSasQuery(market.SasQuery))
                {
                    ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("markets", market.MarketName, market.MarketId);

                    if (blob != null)
                    {
                        market.Image = blob.ImageUri;
                        market.SasQuery = blob.SasQuery;
                    }
                }
                db.Entry(market).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = Methods.CustomResponseMessage(0, "Update market is not successful!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = JObject.FromObject(market);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Markets
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostMarket(Market market)
        {
            JObject result = new JObject();
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (market.Image == null || Methods.CheckExpiredDateOfSasQuery(market.SasQuery))
                {
                    var table = new SqlParameter("@table", "viethung_paybayservice.Markets");
                    int marketId = Convert.ToInt32(Methods.GetInstance().GetValue("viethung_paybayservice.sp_GetMaxId", CommandType.StoredProcedure, ref Methods.err, table));
                    ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("markets", market.MarketName, marketId + 1);

                    if (blob != null)
                    {
                        market.Image = blob.ImageUri;
                        market.SasQuery = blob.SasQuery;
                    }
                }
                db.Markets.Add(market);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            result = JObject.FromObject(market);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Markets/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteMarket(int marketId)
        {
            JObject result = new JObject();
            Market market = await db.Markets.FindAsync(marketId);
            if (market == null)
            {
                result = Methods.CustomResponseMessage(0, "Market isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            if (market.Image != null && Methods.CheckExpiredDateOfSasQuery(market.SasQuery))
            {
                ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("markets", market.MarketName, market.MarketId);

                if (blob != null)
                {
                    market.Image = blob.ImageUri;
                    market.SasQuery = blob.SasQuery;
                }
            }

            db.Markets.Remove(market);
            await db.SaveChangesAsync();

            //result = Methods.CustomResponseMessage(1, "Delete market is successful!");
            result = JObject.FromObject(market);
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