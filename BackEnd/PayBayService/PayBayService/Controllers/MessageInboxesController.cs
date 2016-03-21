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
using PayBayService.Services.MobileServices;

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
        public HttpResponseMessage GetMessageInbox(int messageId, int userId, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var mess = new SqlParameter("@MessageID", messageId);
                var user = new SqlParameter("@UserID", userId);
                if(type == TYPE.OLD)
                {
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMessageOfStore", CommandType.StoredProcedure, ref Methods.err, mess, user);
                }
                else
                {
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMoreNewMessage", CommandType.StoredProcedure, ref Methods.err, mess, user);
                }
                               
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
        public async Task<HttpResponseMessage> PostMessageInbox(MessageInbox messageInbox)
        {
            JObject result = new JObject();
            try
            {
                var userId = new SqlParameter("@UserID", messageInbox.UserID);
                var ownerId = new SqlParameter("@OwnerID", messageInbox.OwnerID);
                var content = new SqlParameter("@Content", messageInbox.Content);
                var inboxDate = new SqlParameter("@InboxDate", messageInbox.InboxDate);
                var response = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_AddNewMessage", CommandType.StoredProcedure, ref Methods.err, userId, ownerId, content, inboxDate);
                if (response.Count > 0)
                {
                    result = response[0].ToObject<JObject>();
                    await PushHelper.SendToastAsync(WebApiConfig.Services, result["Username"].ToString(), result["Content"].ToString(), new Uri(result["Avatar"].ToString()), result["UserID"].ToString());
                }                              
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
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