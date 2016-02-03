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
    public class DetailBillsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/DetailBills
        public IQueryable<DetailBill> GetDetailBills()
        {
            return db.DetailBills;
        }

        // GET: api/DetailBills/5
        [ResponseType(typeof(DetailBill))]
        public async Task<IHttpActionResult> GetDetailBill(int id)
        {
            DetailBill detailBill = await db.DetailBills.FindAsync(id);
            if (detailBill == null)
            {
                return NotFound();
            }

            return Ok(detailBill);
        }

        // PUT: api/DetailBills/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDetailBill(int id, DetailBill detailBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detailBill.Id)
            {
                return BadRequest();
            }

            db.Entry(detailBill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailBillExists(id))
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

        // POST: api/DetailBills
        [ResponseType(typeof(DetailBill))]
        public async Task<IHttpActionResult> PostDetailBill(DetailBill detailBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetailBills.Add(detailBill);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = detailBill.Id }, detailBill);
        }

        // DELETE: api/DetailBills/5
        [ResponseType(typeof(DetailBill))]
        public async Task<IHttpActionResult> DeleteDetailBill(int id)
        {
            DetailBill detailBill = await db.DetailBills.FindAsync(id);
            if (detailBill == null)
            {
                return NotFound();
            }

            db.DetailBills.Remove(detailBill);
            await db.SaveChangesAsync();

            return Ok(detailBill);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetailBillExists(int id)
        {
            return db.DetailBills.Count(e => e.Id == id) > 0;
        }
    }
}