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
    public class InboxDetailsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/InboxDetails
        public IQueryable<InboxDetail> GetInboxDetails()
        {
            return db.InboxDetails;
        }

        // GET: api/InboxDetails/5
        [ResponseType(typeof(InboxDetail))]
        public HttpResponseMessage GetInboxDetail(int messageId)
        {
            JArray result = new JArray();
            try
            {
                var messDetail = new SqlParameter("@MessageID", messageId);
                result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMessageDetail", CommandType.StoredProcedure, ref Methods.err, messDetail);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/InboxDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInboxDetail(int id, InboxDetail inboxDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inboxDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(inboxDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InboxDetailExists(id))
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

        // POST: api/InboxDetails
        [ResponseType(typeof(InboxDetail))]
        public async Task<IHttpActionResult> PostInboxDetail(InboxDetail inboxDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InboxDetails.Add(inboxDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = inboxDetail.ID }, inboxDetail);
        }

        // DELETE: api/InboxDetails/5
        [ResponseType(typeof(InboxDetail))]
        public async Task<IHttpActionResult> DeleteInboxDetail(int id)
        {
            InboxDetail inboxDetail = await db.InboxDetails.FindAsync(id);
            if (inboxDetail == null)
            {
                return NotFound();
            }

            db.InboxDetails.Remove(inboxDetail);
            await db.SaveChangesAsync();

            return Ok(inboxDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InboxDetailExists(int id)
        {
            return db.InboxDetails.Count(e => e.ID == id) > 0;
        }
    }
}