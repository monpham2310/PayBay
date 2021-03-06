﻿using System;
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
using PayBayService.Common;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using PayBayService.Services.MobileServices;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.ServiceBus.Notifications;
using Microsoft.WindowsAzure.Mobile.Service.Security;

namespace PayBayService.Controllers
{
    //[AuthorizeLevel(AuthorizationLevel.User)]
    public class CommentsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();
                
        // GET: api/Comments
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // GET: api/Comments/Store
        [ResponseType(typeof(Comment))]
        public HttpResponseMessage GetCommentOfStore(int storeId, int commentId, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var id = new SqlParameter("@StoreID", storeId);
                var comment = new SqlParameter("@CommentId", commentId);
                if (type == TYPE.OLD)
                {
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_ViewCommentOfStore", CommandType.StoredProcedure, ref Methods.err, id, comment);
                }
                else
                {
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_ViewNewCmtOfStore", CommandType.StoredProcedure, ref Methods.err, id, comment);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutComment(Comment comment)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(comment.Id))
                {
                    result = Methods.CustomResponseMessage(0, "Comment isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(0, "Update Comment is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Comments
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostComment(Comment comment)
        {
            JArray result = new JArray();
            JObject ownerInfo = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            //db.Comments.Add(comment);
            //await db.SaveChangesAsync();

            try
            {
                var cmtDate = new SqlParameter("@Date", comment.CommentDate);
                var storeId = new SqlParameter("@StoreID", comment.StoreID);
                var userId = new SqlParameter("@UserID", comment.UserID);
                var content = new SqlParameter("@Content", comment.Content);
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_AddComment", CommandType.StoredProcedure, ref Methods.err, cmtDate, storeId, userId, content);

                var store = new SqlParameter("@StoreID", comment.StoreID);
                var user = new SqlParameter("@UserID", comment.UserID);       
                var owner = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetOwnerInfo", CommandType.StoredProcedure, ref Methods.err, store, user);
                if(owner.Count > 0)
                {
                    ownerInfo = owner[0].ToObject<JObject>();
                }

                //var currentUser = this.User as ServiceUser;
                //await PushHelper.SendToastAsync(WebApiConfig.Services, ownerInfo["UserName"].ToString(), comment.Content, new Uri(ownerInfo["Avatar"].ToString()));
                await PushHelper.SendToastAsync(WebApiConfig.Services, ownerInfo["UserName"].ToString() + " just commented!", comment.Content, new Uri(ownerInfo["Avatar"].ToString()), ownerInfo["OwnerID"].ToString());                               
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);                        
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteComment(int id)
        {
            JObject result = new JObject();
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                result = Methods.CustomResponseMessage(0, "Comment isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete comment is successful!");
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

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}