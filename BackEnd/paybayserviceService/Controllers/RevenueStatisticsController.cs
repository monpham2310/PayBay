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
    public class RevenueStatisticsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/RevenueStatistics
        public IQueryable<RevenueStatistic> GetRevenueStatistics()
        {
            return db.RevenueStatistics;
        }

        // GET: api/RevenueStatistics/5
        [ResponseType(typeof(RevenueStatistic))]
        public async Task<IHttpActionResult> GetRevenueStatistic(int id)
        {
            RevenueStatistic revenueStatistic = await db.RevenueStatistics.FindAsync(id);
            if (revenueStatistic == null)
            {
                return NotFound();
            }

            return Ok(revenueStatistic);
        }

        // PUT: api/RevenueStatistics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRevenueStatistic(int id, RevenueStatistic revenueStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != revenueStatistic.Id)
            {
                return BadRequest();
            }

            db.Entry(revenueStatistic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueStatisticExists(id))
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

        // POST: api/RevenueStatistics
        [ResponseType(typeof(RevenueStatistic))]
        public async Task<IHttpActionResult> PostRevenueStatistic(RevenueStatistic revenueStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RevenueStatistics.Add(revenueStatistic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = revenueStatistic.Id }, revenueStatistic);
        }

        // DELETE: api/RevenueStatistics/5
        [ResponseType(typeof(RevenueStatistic))]
        public async Task<IHttpActionResult> DeleteRevenueStatistic(int id)
        {
            RevenueStatistic revenueStatistic = await db.RevenueStatistics.FindAsync(id);
            if (revenueStatistic == null)
            {
                return NotFound();
            }

            db.RevenueStatistics.Remove(revenueStatistic);
            await db.SaveChangesAsync();

            return Ok(revenueStatistic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RevenueStatisticExists(int id)
        {
            return db.RevenueStatistics.Count(e => e.Id == id) > 0;
        }
    }
}