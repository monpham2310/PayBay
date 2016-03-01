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
using System.Data.SqlClient;

namespace PayBayService.Controllers
{
    public class StatisticRatingsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/StatisticRatings
        public IQueryable<StatisticRating> GetStatisticRatings()
        {
            return db.StatisticRatings;
        }

        // GET: api/StatisticRatings/5
        [ResponseType(typeof(StatisticRating))]
        public HttpResponseMessage GetStatisticRating(int userId, int storeId)
        {
            JObject result = new JObject();
            try
            {
                var user = new SqlParameter("@UserId", userId);
                var store = new SqlParameter("@StoreId", storeId);
                var response = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetStarRated", CommandType.StoredProcedure, ref Methods.err, user, store);
                result = response[0].ToObject<JObject>();

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/StatisticRatings/5
        [ResponseType(typeof(StatisticRating))]
        public async Task<IHttpActionResult> GetStatisticRating(int id)
        {
            StatisticRating statisticRating = await db.StatisticRatings.FindAsync(id);
            if (statisticRating == null)
            {
                return NotFound();
            }

            return Ok(statisticRating);
        }

        // PUT: api/StatisticRatings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStatisticRating(int id, StatisticRating statisticRating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statisticRating.ID)
            {
                return BadRequest();
            }

            db.Entry(statisticRating).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticRatingExists(id))
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

        // POST: api/StatisticRatings
        [ResponseType(typeof(StatisticRating))]
        public HttpResponseMessage PostStatisticRating(StatisticRating statisticRating)
        {            
            JObject response = new JObject();
            try
            {
                var userId = new SqlParameter("@UserId", statisticRating.UserID);
                var storeId = new SqlParameter("@StoreId", statisticRating.StoreID);
                var rate = new SqlParameter("@Rate", statisticRating.RateOfUser);
                var result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_UserRate",CommandType.StoredProcedure,ref Methods.err, userId, storeId, rate);
                response = result[0].ToObject<JObject>();
            }
            catch (Exception ex)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        // DELETE: api/StatisticRatings/5
        [ResponseType(typeof(StatisticRating))]
        public async Task<IHttpActionResult> DeleteStatisticRating(int id)
        {
            StatisticRating statisticRating = await db.StatisticRatings.FindAsync(id);
            if (statisticRating == null)
            {
                return NotFound();
            }

            db.StatisticRatings.Remove(statisticRating);
            await db.SaveChangesAsync();

            return Ok(statisticRating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatisticRatingExists(int id)
        {
            return db.StatisticRatings.Count(e => e.ID == id) > 0;
        }
    }
}