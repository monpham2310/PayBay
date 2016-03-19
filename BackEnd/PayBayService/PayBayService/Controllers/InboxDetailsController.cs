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
using PayBayService.Services.MobileServices;

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
        public HttpResponseMessage GetInboxDetail(int id, int messageId, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var detailId = new SqlParameter("@ID", id);
                var messDetail = new SqlParameter("@MessageID", messageId);
                if (type == TYPE.OLD)
                {
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetMoreMessageDetail", CommandType.StoredProcedure, ref Methods.err, detailId, messDetail);
                }
                else
                {
                    result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetNewMessageDetail", CommandType.StoredProcedure, ref Methods.err, detailId, messDetail);
                }
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
        public async Task<HttpResponseMessage> PostInboxDetail(InboxDetail inboxDetail)
        {
            JObject result = new JObject();
            try
            {
                var msgId = new SqlParameter("@MessageID", inboxDetail.MessageID);
                var dtime = new SqlParameter("@InboxDate", inboxDetail.InboxDate);
                var content = new SqlParameter("@Content", inboxDetail.Content);
                var response = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_AddMsgDetail", CommandType.StoredProcedure, ref Methods.err, msgId, dtime, content);
                if(response.Count > 0)
                {
                    result = response[0].ToObject<JObject>();
                    await PushHelper.SendToastAsync(WebApiConfig.Services, result, result["UserID"].ToString());
                }                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
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