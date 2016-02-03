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

        // PUT: api/Markets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMarket(int id, Market market)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != market.MarketId)
            {
                return BadRequest();
            }

            db.Entry(market).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Markets
        [ResponseType(typeof(Market))]
        public async Task<IHttpActionResult> PostMarket(Market market)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Markets.Add(market);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = market.MarketId }, market);
        }

        // DELETE: api/Markets/5
        [ResponseType(typeof(Market))]
        public async Task<IHttpActionResult> DeleteMarket(int id)
        {
            Market market = await db.Markets.FindAsync(id);
            if (market == null)
            {
                return NotFound();
            }

            db.Markets.Remove(market);
            await db.SaveChangesAsync();

            return Ok(market);
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