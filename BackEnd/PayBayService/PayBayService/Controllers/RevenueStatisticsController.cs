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

namespace PayBayService.Controllers
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
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutRevenueStatistic(RevenueStatistic revenueStatistic)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
                        
            db.Entry(revenueStatistic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RevenueStatisticExists(revenueStatistic.Id))
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

        // POST: api/RevenueStatistics
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostRevenueStatistic(RevenueStatistic revenueStatistic)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.RevenueStatistics.Add(revenueStatistic);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Post Request is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/RevenueStatistics/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteRevenueStatistic(int id)
        {
            RevenueStatistic revenueStatistic = await db.RevenueStatistics.FindAsync(id);
            JObject result = new JObject();
            if (revenueStatistic == null)
            {
                result = Methods.CustomResponseMessage(0, "Request isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.RevenueStatistics.Remove(revenueStatistic);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(0, "Delete request is successful!");
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

        private bool RevenueStatisticExists(int id)
        {
            return db.RevenueStatistics.Count(e => e.Id == id) > 0;
        }
    }
}