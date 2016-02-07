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

        // PUT: api/Users/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutUser(User user)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            db.Entry(user).State = EntityState.Modified;

            //var id = new SqlParameter("@UserID", user.UserId);
            //var name = new SqlParameter("@Name", user.Name);
            //var birthday = new SqlParameter("@Birthday", user.Birthday);
            //var phone = new SqlParameter("@Phone", user.Phone);
            //var gender = new SqlParameter("@Gender", user.Gender);
            //var address = new SqlParameter("@Address", user.Address);
            //var avatar = new SqlParameter("@Avatar", user.Avatar);
            //var pass = new SqlParameter("@Pass", user.Pass);
            //var type = new SqlParameter("@TypeID", user.TypeID);

            //db.Users.SqlQuery("paybayservice.sp_UpdateUser @UserID,@Name,@Birthday,@Phone,@Gender,@Address,@Avatar,@Pass,@TypeID",
            //                    id,name,birthday,phone,gender,address,avatar,pass,type);

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

            result = Methods.CustomResponseMessage(1, "Update User is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Users
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostUser(User user)
        {
            JObject result = new JObject();     
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            if (UsernameExists(user.Username, user.Email))
            {
                result = Methods.CustomResponseMessage(0, "Username had already exists!");
                return Request.CreateResponse(HttpStatusCode.BadRequest,result);
            }

            db.Users.Add(user);

            //var name = new SqlParameter("@Name", user.Name);
            //var birthday = new SqlParameter("@Birthday", user.Birthday);
            //var email = new SqlParameter("@Email", user.Email);
            //var phone = new SqlParameter("@Phone", user.Phone);
            //var gender = new SqlParameter("@Gender", user.Gender);
            //var address = new SqlParameter("@Address", user.Address);
            //var avatar = new SqlParameter("@Avatar", user.Avatar);
            //var username = new SqlParameter("@Username", user.Username);
            //var pass = new SqlParameter("@Pass", user.Pass);
            //var type = new SqlParameter("@TypeID", user.TypeID);

            //db.Users.SqlQuery("paybayservice.sp_AddUser @Name,@Birthday,@Email,@Phone,@Gender,@Address,@Avatar,@Username,@Pass,@TypeID",
            //                    name,birthday,email,phone,gender,address,avatar,username,pass,type);

            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Add user is successful!");            
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

        private bool UsernameExists(string username,string email)
        {
            IQueryable<User> checkExists = db.Users.Where(e => e.Email == email && e.Username == username);
            return checkExists.Count() > 0;
        }

    }
}