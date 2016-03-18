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
using System.Data.SqlClient;
using PayBayService.Common;

namespace PayBayService.Controllers
{
    public class MessageInboxesController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/MessageInboxes
        public IQueryable<MessageInbox> GetMessageInboxes()
        {
            return db.MessageInboxes;
        }

        // GET: api/MessageInboxes/5
        [ResponseType(typeof(MessageInbox))]
        public HttpResponseMessage GetMessageInbox(int ownerId)
        {
            JArray result = new JArray();
            try
            {
                var ownerStore = new SqlParameter("@OwnerID", ownerId);
                result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMessageOfStore", CommandType.StoredProcedure, ref Methods.err, ownerStore);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/MessageInboxes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessageInbox(int id, MessageInbox messageInbox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != messageInbox.MessageID)
            {
                return BadRequest();
            }

            db.Entry(messageInbox).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageInboxExists(id))
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

        // POST: api/MessageInboxes
        [ResponseType(typeof(MessageInbox))]
        public async Task<IHttpActionResult> PostMessageInbox(MessageInbox messageInbox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MessageInboxes.Add(messageInbox);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = messageInbox.MessageID }, messageInbox);
        }

        // DELETE: api/MessageInboxes/5
        [ResponseType(typeof(MessageInbox))]
        public async Task<IHttpActionResult> DeleteMessageInbox(int id)
        {
            MessageInbox messageInbox = await db.MessageInboxes.FindAsync(id);
            if (messageInbox == null)
            {
                return NotFound();
            }

            db.MessageInboxes.Remove(messageInbox);
            await db.SaveChangesAsync();

            return Ok(messageInbox);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageInboxExists(int id)
        {
            return db.MessageInboxes.Count(e => e.MessageID == id) > 0;
        }
    }
}