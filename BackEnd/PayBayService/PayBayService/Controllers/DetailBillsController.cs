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
using System.Collections.ObjectModel;

namespace PayBayService.Controllers
{
    public class DetailBillsController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/DetailBills
        public IQueryable<DetailBill> GetDetailBills()
        {
            return db.DetailBills;
        }
                
        // GET: api/DetailBills/5
        [ResponseType(typeof(DetailBill))]
        public HttpResponseMessage GetDetailBill(int billId)
        {
            JArray result = new JArray();
            try {
                var bill = new SqlParameter("@BillId", billId);
                result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_GetDetailBill", CommandType.StoredProcedure, ref Methods.err, bill);
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

        // PUT: api/DetailBills/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutDetailBill(DetailBill detailBill)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
                        
            db.Entry(detailBill).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailBillExists(detailBill.Id))
                {
                    result = Methods.CustomResponseMessage(0, "Detail Bill isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Update Detail Bill is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
                
        //// POST: api/DetailBills
        //[ResponseType(typeof(HttpResponseMessage))]
        //public async Task<HttpResponseMessage> PostDetailBill(DetailBill detailBill)
        //{
        //    JObject result = new JObject();
        //    if (!ModelState.IsValid)
        //    {                
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        //    }

        //    db.DetailBills.Add(detailBill);
        //    await db.SaveChangesAsync();

        //    result = Methods.CustomResponseMessage(1, "Add Detail Bill is successful!");
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        // POST: api/DetailBills
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PostDetailBill(ObservableCollection<DetailBill> detailBill)
        {
            JObject result = new JObject();
            bool check = false;
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                foreach (DetailBill item in detailBill)
                {
                    var bill = new SqlParameter("@BillID", item.BillID);
                    var product = new SqlParameter("@ProductID", item.ProductID);
                    var numberof = new SqlParameter("@NumberOf", item.NumberOf);
                    check = Methods.GetInstance().ExecNonQuery("paybayservice.sp_InsertDetailBill", CommandType.StoredProcedure, ref Methods.err, bill, product, numberof);
                }
                if (check)
                    result = Methods.CustomResponseMessage(1, "Insert is successful!");
                else
                    result = Methods.CustomResponseMessage(0, "Insert is not successful!");
            }
            catch (Exception ex)
            {
                result = Methods.CustomResponseMessage(0, "Insert not successful!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/DetailBills/5
        //[ResponseType(typeof(HttpResponseMessage))]
        //public async Task<HttpResponseMessage> DeleteDetailBill(int id)
        //{
        //    JObject result = new JObject();
        //    DetailBill detailBill = await db.DetailBills.FindAsync(id);
        //    if (detailBill == null)
        //    {
        //        result = Methods.CustomResponseMessage(0, "Detail Bill isn't exists!");
        //        return Request.CreateResponse(HttpStatusCode.NotFound, result);
        //    }

        //    db.DetailBills.Remove(detailBill);
        //    await db.SaveChangesAsync();

        //    result = Methods.CustomResponseMessage(1, "Delete Detail Bill is successful!");
        //    return Request.CreateResponse(HttpStatusCode.OK, result);
        //}

        // DELETE: api/DetailBills/5
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage DeleteDetailBill(int billid, int productid)
        {
            JArray result = new JArray();
            try {
                var bill = new SqlParameter("@BillID", billid);
                var product = new SqlParameter("@ProductID", productid);
                result = Methods.GetInstance().ExecQueryWithResult("paybayservice.sp_DelDetailBill", CommandType.StoredProcedure, ref Methods.err, bill, product);
                if (result == null)
                {
                    var notfound = Methods.CustomResponseMessage(0, "Detail Bill isn't exists!");
                    result.Add(notfound);                    
                }
            }
            catch (Exception ex)
            {
                var error = Methods.CustomResponseMessage(0, ex.Message.ToString());
                result.Add(error);
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
                                               
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

        private bool DetailBillExists(int id)
        {
            return db.DetailBills.Count(e => e.Id == id) > 0;
        }
    }
}