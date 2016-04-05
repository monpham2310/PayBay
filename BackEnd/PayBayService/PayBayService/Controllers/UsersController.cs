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
using PayBayService.Common;
using PayBayService.Models.BlobStorage;
using PayBayService.Models.Accounts;

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

        // POST: api/Users/Account
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostLogin(Account account, string type)
        {
            JArray result = new JArray();
            JObject body = new JObject();      
            if (account != null)
            {                
                var uid = new SqlParameter("@Email", account.Email);
                var pwd = new SqlParameter("@Pass", account.Password);
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_UserLogin", CommandType.StoredProcedure, ref Methods.err, uid, pwd);
                if (result.Count > 0)
                {
                    body = result[0].ToObject<JObject>();
                }
                else
                {
                    body = Methods.CustomResponseMessage(0, "Login isn't successful!");                    
                    return Request.CreateResponse(HttpStatusCode.BadRequest, body);
                }                
            }
            return Request.CreateResponse(HttpStatusCode.OK, body);
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
                        
            try
            {
                var userId = new SqlParameter("@UserID", user.UserId);
                string email = Convert.ToString(Methods.GetInstance().GetValue("viethung_paybayservice.sp_GetEmailOfUser", CommandType.StoredProcedure, ref Methods.err, userId));
                if (email == user.Email || !AccountExists(user.Email))
                {
                    if (user.Avatar == null || Methods.CheckExpiredDateOfSasQuery(user.SasQuery))
                    {
                        ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("users", user.Username, user.UserId);
                        if (blob != null)
                        {
                            user.Avatar = blob.ImageUri;
                            user.SasQuery = blob.SasQuery;
                        }
                    }
                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else
                {
                    result = Methods.CustomResponseMessage(0, "Update user is not successful!");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
                }
            }
            catch (Exception ex)
            {                
                result = Methods.CustomResponseMessage(0, "Update user is not successful!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);                
            }

            result = JObject.FromObject(user);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
                
        // PUT: api/Users/{account}
        [ResponseType(typeof(User))]
        public async Task<HttpResponseMessage> ResetPassword(Account account,int code)
        {
            JObject result = new JObject();
            try
            {
                var email = new SqlParameter("@Email", account.Email);
                var pass = new SqlParameter("@Pass", account.Password);
                bool check = Methods.GetInstance().ExecNonQuery("viethung_paybayservice.sp_ResetPassword",CommandType.StoredProcedure,ref Methods.err, email, pass);
                if (check)
                {
                    //TODO: send mail
                    await Methods.GetInstance().SendMail(account.Email, account.Pwd);
                    result = Methods.CustomResponseMessage(1, "Reset pass is successful!");                    
                }
                else
                {
                    result = Methods.CustomResponseMessage(0, "Reset pass is NOT successful!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<HttpResponseMessage> PostUser(User user)
        {
            JObject result = new JObject();                 
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (!AccountExists(user.Email))
                {
                    if (user.Avatar == null || Methods.CheckExpiredDateOfSasQuery(user.SasQuery))
                    {
                        var table = new SqlParameter("@table", "viethung_paybayservice.Users");
                        int userId = Convert.ToInt32(Methods.GetInstance().GetValue("viethung_paybayservice.sp_GetMaxId", CommandType.StoredProcedure, ref Methods.err, table));
                        ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("users", user.Username, userId + 1);

                        if (blob != null)
                        {
                            user.Avatar = blob.ImageUri;
                            user.SasQuery = blob.SasQuery;
                        }
                    }                
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Email had already exists!");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
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

            if (user.Avatar != null && Methods.CheckExpiredDateOfSasQuery(user.SasQuery))
            {
                ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("users", user.Username, user.UserId);

                if (blob != null)
                {
                    user.Avatar = blob.ImageUri;
                    user.SasQuery = blob.SasQuery;
                }
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            //result = Methods.CustomResponseMessage(1, "Delete user is successful!");
            result = JObject.FromObject(user);
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
              
        private bool AccountExists(string email)
        {
            return db.Users.Count(e => e.Email == email) > 0;
        }
          
    }
}