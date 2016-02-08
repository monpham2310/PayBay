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
using PayBayService.App_Code;
using Newtonsoft.Json.Linq;

namespace PayBayService.Controllers
{
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
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Add Comment is successful!");
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