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
using PayBayService.App_Code;
using System.Data.SqlClient;
using PayBayService.Models.BlobStorage;

namespace PayBayService.Controllers
{
    public class StoresController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/Stores
        public IQueryable<Store> GetStores()
        {
            return db.Stores;
        }

        // GET: api/Stores/5
        [ResponseType(typeof(Store))]
        public async Task<IHttpActionResult> GetStore(int id)
        {
            Store store = await db.Stores.FindAsync(id);
            
            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        // GET: api/Stores/MarketID
        [ResponseType(typeof(Store))]
        public HttpResponseMessage GetStoreOfMarket(int marketId)
        {
            JArray result = new JArray();
            try
            {
                var market = new SqlParameter("@MarketID", marketId);
                result = Methods.ExecQueryWithResult("paybayservice.sp_GetStoreOfMarket", CommandType.StoredProcedure, ref Methods.err, market);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Stores/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutStore(Store store)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
                        
            db.Entry(store).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(store.StoreId))
                {
                    result = Methods.CustomResponseMessage(0, "Store isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Update store is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Stores
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostStore(Store store)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }

            int storeId = (int)Methods.GetValue("paybayservice.sp_GetMaxStoreId", CommandType.StoredProcedure, ref Methods.err);
            ModelBlob blob = await Methods.GetSasAndImageUriFromBlob("users", store.StoreName, storeId);

            if (blob != null)
            {
                store.Image = blob.ImageUri;
                store.SasQuery = blob.SasQuery;
                db.Stores.Add(store);
                await db.SaveChangesAsync();
            }
            else
            {
                result = Methods.CustomResponseMessage(0, "Could not retrieve Sas and Uri settings!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = Methods.CustomResponseMessage(1, "Add store is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Stores
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage UsersLike(int storeId, int number)
        {
            int result = 0;
            try
            {
                var store = new SqlParameter("@StoreID", storeId);
                var num = new SqlParameter("@NumberOf", number);
                result = (int)Methods.GetValue("paybayservice.sp_UpdateLike", CommandType.StoredProcedure, ref Methods.err, store, num);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Stores/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteStore(int id)
        {
            JObject result = new JObject();
            Store store = await db.Stores.FindAsync(id);
            if (store == null)
            {
                result = Methods.CustomResponseMessage(0, "Store isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.Stores.Remove(store);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete store is successful!");
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

        private bool StoreExists(int id)
        {
            return db.Stores.Count(e => e.StoreId == id) > 0;
        }
    }
}