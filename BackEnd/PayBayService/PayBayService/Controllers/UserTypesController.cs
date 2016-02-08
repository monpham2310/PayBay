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
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using PayBayService.App_Code;

namespace PayBayService.Controllers
{
    public class UserTypesController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();
        
        // GET: api/UserTypes
        public IQueryable<UserType> GetUserTypes()
        {
            return db.UserTypes;
        }

        // GET: api/UserTypes/5
        [ResponseType(typeof(UserType))]
        public async Task<IHttpActionResult> GetUserType(int id)
        {
            UserType userType = await db.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            return Ok(userType);
        }

        // PUT: api/UserTypes/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutUserType(UserType userType)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            
            db.Entry(userType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(userType.TypeId))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Update user type successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/UserTypes
        //[ResponseType(typeof(UserType))]
        //public async Task<IHttpActionResult> PostUserType(UserType userType)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.UserTypes.Add(userType);

        //    //var name = new SqlParameter("@typeName", userType.TypeName);
        //    //db.UserTypes.SqlQuery("paybayservice.sp_AddUserType @typeName", name);

        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("Api", new { id = userType.TypeId }, userType);
        //}

        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostUserType(UserType userType)
        {
            JObject result = new JObject();

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            db.UserTypes.Add(userType);

            //try
            //{
            //    var name = new SqlParameter("@typeName", userType.TypeName);
            //    result = Methods.ExecQueryWithResult("paybayservice.sp_AddUserType",CommandType.StoredProcedure,ref Methods.err, name);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
           
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Add user type successful!");
            //return CreatedAtRoute("Api", new { id = userType.TypeId }, userType);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/UserTypes/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteUserType(int id)
        {
            JObject message = new JObject();
            UserType userType = await db.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.UserTypes.Remove(userType);
            await db.SaveChangesAsync();

            message = Methods.CustomResponseMessage(1, "Delete user type successful!");
            return Request.CreateResponse(HttpStatusCode.OK, message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserTypeExists(int id)
        {
            return db.UserTypes.Count(e => e.TypeId == id) > 0;
        }
                
    }
}