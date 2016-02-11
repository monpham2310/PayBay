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

namespace PayBayService.Controllers
{
    public class BillsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/Bills
        public IQueryable<Bill> GetBills()
        {
            return db.Bills;
        }

        // GET: api/Bills/StoreID
        [ResponseType(typeof(Bill))]
        public HttpResponseMessage GetBillOfStore(int storeId)
        {
            JArray result = new JArray();
            try
            {
                var store = new SqlParameter("@StoreID", storeId);
                result = Methods.ExecQueryWithResult("paybayservice.sp_GetBillOfStore", CommandType.StoredProcedure, ref Methods.err, store);
                if (result == null)
                {
                    var error = Methods.CustomResponseMessage(0, "Data not found!");
                    result.Add(error);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/Bills/UserID
        [ResponseType(typeof(Bill))]
        public HttpResponseMessage GetBillOfUser(int userId)
        {
            JArray result = new JArray();
            try
            {
                var store = new SqlParameter("@UserID", userId);
                result = Methods.ExecQueryWithResult("paybayservice.sp_GetBillOfUser", CommandType.StoredProcedure, ref Methods.err, store);
                if (result == null)
                {
                    var error = Methods.CustomResponseMessage(0, "Data not found!");
                    result.Add(error);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/Bills/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutBill(Bill bill)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
                        
            db.Entry(bill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(bill.BillId))
                {
                    result = Methods.CustomResponseMessage(0, "Bill isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = JObject.FromObject(bill);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/Bills
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostBill(Bill bill)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.Bills.Add(bill);
            await db.SaveChangesAsync();

            result = JObject.FromObject(bill);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/Bills/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteBill(int id)
        {
            JObject result = new JObject();
            Bill bill = await db.Bills.FindAsync(id);
            if (bill == null)
            {
                result = Methods.CustomResponseMessage(0, "Bill isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.Bills.Remove(bill);
            var billId = new SqlParameter("@BillID", id);
            var product = new SqlParameter("@ProductID", 0);
            bool check = Methods.ExecNonQuery("paybayservice.sp_DelDetailBill", CommandType.StoredProcedure, ref Methods.err, billId, product);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete Bill is successful!");
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

        private bool BillExists(int id)
        {
            return db.Bills.Count(e => e.BillId == id) > 0;
        }
    }
}