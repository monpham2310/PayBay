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
        public HttpResponseMessage FindStore(string name,int markId, int storeId, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var storeName = new SqlParameter("@StoreName", name);
                var marId = new SqlParameter("@MarketID", markId);
                var storeid = new SqlParameter("@StoreId", storeId);
                if(type == TYPE.OLD)
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_FindStore", CommandType.StoredProcedure, ref Methods.err, storeName, marId, storeid);
                else
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_FindNewStore", CommandType.StoredProcedure, ref Methods.err, storeName, marId, storeid);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Stores/5
        [ResponseType(typeof(Store))]
        public HttpResponseMessage GetStoresOfOwner(int ownerId)
        {
            JArray result = new JArray();
            try
            {
                var owner = new SqlParameter("@OwnerID", ownerId);                                
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetStoreOfOwner", CommandType.StoredProcedure, ref Methods.err, owner);                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Stores/MarketID
        [ResponseType(typeof(Store))]
        public HttpResponseMessage GetStoreOfMarket(int marketId, int storeId, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var market = new SqlParameter("@MarketID", marketId);
                var store = new SqlParameter("@StoreID", storeId);
                if(type == TYPE.OLD)
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetStoreOfMarket", CommandType.StoredProcedure, ref Methods.err, market, store);
                else
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetNewStoreOfMarket", CommandType.StoredProcedure, ref Methods.err, market, store);
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (store.Image == null)
                {                    
                    ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("stores", store.StoreName, store.StoreId);

                    if (blob != null)
                    {
                        store.Image = blob.ImageUri;
                        store.SasQuery = blob.SasQuery;
                    }
                }
                db.Entry(store).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = Methods.CustomResponseMessage(0, "Update store is not successful!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            result = JObject.FromObject(store);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Stores
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostStore(Store store)
        {
            JObject result = new JObject();            
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                if (store.Image == null)
                {
                    var table = new SqlParameter("@table", "viethung_paybayservice.Stores");
                    int storeId = Convert.ToInt32(Methods.GetInstance().GetValue("viethung_paybayservice.sp_GetMaxId", CommandType.StoredProcedure, ref Methods.err, table));
                    ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("stores", store.StoreName, storeId + 1);

                    if (blob != null)
                    {
                        store.Image = blob.ImageUri;
                        store.SasQuery = blob.SasQuery;                        
                    }                    
                }
                db.Stores.Add(store);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            result = JObject.FromObject(store);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Stores
        //[ResponseType(typeof(HttpResponseMessage))]
        //public HttpResponseMessage UsersLike(int storeId, int number)
        //{
        //    int result = 0;
        //    try
        //    {
        //        var store = new SqlParameter("@StoreID", storeId);
        //        var num = new SqlParameter("@NumberOf", number);
        //        result = Convert.ToInt32(Methods.GetInstance().GetValue("viethung_paybayservice.sp_UpdateLike", CommandType.StoredProcedure, ref Methods.err, store, num));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        // DELETE: api/Stores/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteStore(int storeId)
        {
            JObject result = new JObject();
            Store store = await db.Stores.FindAsync(storeId);
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