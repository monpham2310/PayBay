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
using PayBayService.Models.BlobStorage;

namespace PayBayService.Controllers
{
    public class UsersController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/Accounts/
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostLogin(string username, byte[] password)
        {
            JArray result = new JArray();
            if (!AccountExists(username, password))
            {
                var check = Methods.CustomResponseMessage(0, "Login isn't successful!");
                result.Add(check);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            var uid = new SqlParameter("@Username", username);
            var pwd = new SqlParameter("@Pass", password);
            result = Methods.ExecQueryWithResult("paybayservice.sp_UserLogin", CommandType.StoredProcedure, ref Methods.err, uid, pwd);         
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<HttpResponseMessage> PutUser(User user)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            db.Entry(user).State = EntityState.Modified;
                        
            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserId))
                {
                    result = Methods.CustomResponseMessage(0, "User is not exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = JObject.FromObject(user);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<HttpResponseMessage> PostUser(User user)
        {
            JObject result = new JObject();     
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            int userId = (int)Methods.GetValue("paybayservice.sp_GetMaxUserId", CommandType.StoredProcedure, ref Methods.err);
            ModelBlob blob = await Methods.GetSasAndImageUriFromBlob("users", user.Username, userId);

            if (blob != null)
            {
                user.Avatar = blob.ImageUri;
                user.SasQuery = blob.SasQuery;
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
            else
            {
                result = Methods.CustomResponseMessage(0, "Could not retrieve Sas and Uri settings!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = JObject.FromObject(user);        
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteUser(int id)
        {
            JObject result = new JObject();
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                result = Methods.CustomResponseMessage(0, "User is not exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete user is successful!");
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

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
              
        private bool AccountExists(string username, byte[] password)
        {
            return db.Users.Count(e => e.Username == username && e.Pass == password) > 0;
        }
          
    }
}