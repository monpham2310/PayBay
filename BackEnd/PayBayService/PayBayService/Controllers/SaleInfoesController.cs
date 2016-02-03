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
    public class SaleInfoesController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/SaleInfoes
        public IQueryable<SaleInfo> GetSaleInfoes()
        {
            return db.SaleInfoes;
        }

        // GET: api/SaleInfoes/5
        [ResponseType(typeof(SaleInfo))]
        public async Task<IHttpActionResult> GetSaleInfo(int id)
        {
            SaleInfo saleInfo = await db.SaleInfoes.FindAsync(id);
            if (saleInfo == null)
            {
                return NotFound();
            }

            return Ok(saleInfo);
        }

        // PUT: api/SaleInfoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSaleInfo(int id, SaleInfo saleInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saleInfo.SaleId)
            {
                return BadRequest();
            }

            db.Entry(saleInfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleInfoExists(id))
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

        // POST: api/SaleInfoes
        [ResponseType(typeof(SaleInfo))]
        public async Task<IHttpActionResult> PostSaleInfo(SaleInfo saleInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SaleInfoes.Add(saleInfo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = saleInfo.SaleId }, saleInfo);
        }

        // DELETE: api/SaleInfoes/5
        [ResponseType(typeof(SaleInfo))]
        public async Task<IHttpActionResult> DeleteSaleInfo(int id)
        {
            SaleInfo saleInfo = await db.SaleInfoes.FindAsync(id);
            if (saleInfo == null)
            {
                return NotFound();
            }

            db.SaleInfoes.Remove(saleInfo);
            await db.SaveChangesAsync();

            return Ok(saleInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaleInfoExists(int id)
        {
            return db.SaleInfoes.Count(e => e.SaleId == id) > 0;
        }
    }
}