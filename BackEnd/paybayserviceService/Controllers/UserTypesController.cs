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
using paybayserviceService.DataObjects;

namespace paybayserviceService.Controllers
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
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserType(int id, UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userType.TypeId)
            {
                return BadRequest();
            }

            db.Entry(userType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/UserTypes
        [ResponseType(typeof(UserType))]
        public async Task<IHttpActionResult> PostUserType(UserType userType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserTypes.Add(userType);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserTypeExists(userType.TypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userType.TypeId }, userType);
        }

        // DELETE: api/UserTypes/5
        [ResponseType(typeof(UserType))]
        public async Task<IHttpActionResult> DeleteUserType(int id)
        {
            UserType userType = await db.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            db.UserTypes.Remove(userType);
            await db.SaveChangesAsync();

            return Ok(userType);
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