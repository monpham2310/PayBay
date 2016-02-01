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
using paybayserviceService.DataObjects;

namespace paybayserviceService.Controllers
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
        public async Task<IHttpActionResult> GetDetailBill(string id)
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

            if (id != detailBill.BillID)
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

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DetailBillExists(detailBill.BillID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = detailBill.BillID }, detailBill);
        }

        // DELETE: api/DetailBills/5
        [ResponseType(typeof(DetailBill))]
        public async Task<IHttpActionResult> DeleteDetailBill(string id)
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
            return db.DetailBills.Count(e => e.BillID == id) > 0;
        }
    }
}