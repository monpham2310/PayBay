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

        // PUT: api/ProductStatistics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductStatistic(int id, ProductStatistic productStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productStatistic.Id)
            {
                return BadRequest();
            }

            db.Entry(productStatistic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatisticExists(id))
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

        // POST: api/ProductStatistics
        [ResponseType(typeof(ProductStatistic))]
        public async Task<IHttpActionResult> PostProductStatistic(ProductStatistic productStatistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductStatistics.Add(productStatistic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = productStatistic.Id }, productStatistic);
        }

        // DELETE: api/ProductStatistics/5
        [ResponseType(typeof(ProductStatistic))]
        public async Task<IHttpActionResult> DeleteProductStatistic(int id)
        {
            ProductStatistic productStatistic = await db.ProductStatistics.FindAsync(id);
            if (productStatistic == null)
            {
                return NotFound();
            }

            db.ProductStatistics.Remove(productStatistic);
            await db.SaveChangesAsync();

            return Ok(productStatistic);
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