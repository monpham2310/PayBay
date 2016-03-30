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
using PayBayService.Services.MobileServices;

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
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetBillOfStore", CommandType.StoredProcedure, ref Methods.err, store);
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
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetBillOfUser", CommandType.StoredProcedure, ref Methods.err, store);
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                db.Bills.Add(bill);
                await db.SaveChangesAsync();

                //var createDate = new SqlParameter("@CreatedDate", bill.CreatedDate);
                var store = new SqlParameter("@StoreID", bill.StoreID);
                //var total = new SqlParameter("@TotalPrice", bill.TotalPrice);
                //var reduce = new SqlParameter("@ReducedPrice", bill.ReducedPrice);
                var user = new SqlParameter("@UserID", bill.UserID);
                //var shipMethod = new SqlParameter("@ShipMethod", bill.ShipMethod);
                //var trade = new SqlParameter("@TradeTerm", bill.TradeTerm);
                //var agree = new SqlParameter("@Agree", bill.AgreeredShippingDate);
                //var shipDate = new SqlParameter("@ShipDate", bill.ShippingDate);

                //var response = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_AddBill", CommandType.StoredProcedure, ref Methods.err,
                //                  createDate, store, total, reduce, user, shipMethod, trade, agree, shipDate);

                var response = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetUserJustOrder", CommandType.StoredProcedure, ref Methods.err, store, user);

                if (response.Count > 0)
                    result = response[0].ToObject<JObject>();

                await PushHelper.SendToastAsync(WebApiConfig.Services, result["Username"].ToString() + " just order to you!","You have new order!"
                                                    ,new Uri(result["Avatar"].ToString()), result["OwnerID"].ToString());
            }
            catch (Exception ex)
            {
                result = Methods.CustomResponseMessage(0, "Submit bill is not successful!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

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
            bool check = Methods.GetInstance().ExecNonQuery("viethung_paybayservice.sp_DelDetailBill", CommandType.StoredProcedure, ref Methods.err, billId, product);
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