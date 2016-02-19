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
using PayBayService.App_Code;
using PayBayService.Models.BlobStorage;

namespace PayBayService.Controllers
{
    public class MarketsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/Markets
        public IQueryable<Market> GetMarkets()
        {
            return db.Markets;
        }

        // GET: api/Markets/5
        [ResponseType(typeof(Market))]
        public async Task<IHttpActionResult> GetMarket(int id)
        {
            Market market = await db.Markets.FindAsync(id);
            if (market == null)
            {
                return NotFound();
            }

            return Ok(market);
        }

        // GET: api/Markets/BenThanh
        [ResponseType(typeof(Market))]
        public async Task<IHttpActionResult> GetMarket(string name)
        {
            Market market = await db.Markets.FindAsync(name);
            if (market == null)
            {
                return NotFound();
            }

            return Ok(market);
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

            int marketId = (int)Methods.GetValue("paybayservice.sp_GetMaxMarketId", CommandType.StoredProcedure, ref Methods.err);
            ModelBlob blob = await Methods.GetSasAndImageUriFromBlob("markets", market.MarketName, marketId);

            if (blob != null)
            {
                market.Image = blob.ImageUri;
                market.SasQuery = blob.SasQuery;
                db.Markets.Add(market);
                await db.SaveChangesAsync();
            }
            else
            {
                result = Methods.CustomResponseMessage(0, "Could not retrieve Sas and Uri settings!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            result = Methods.CustomResponseMessage(1, "Add market is successful!");
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