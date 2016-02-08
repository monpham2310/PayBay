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
        public async Task<IHttpActionResult> GetDetailBill(int id)
        {
            DetailBill detailBill = await db.DetailBills.FindAsync(id);
            if (detailBill == null)
            {
                return NotFound();
            }

            return Ok(detailBill);
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

        // POST: api/DetailBills
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostDetailBill(DetailBill detailBill)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {                
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.DetailBills.Add(detailBill);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Add Detail Bill is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/DetailBills/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteDetailBill(int id)
        {
            JObject result = new JObject();
            DetailBill detailBill = await db.DetailBills.FindAsync(id);
            if (detailBill == null)
            {
                result = Methods.CustomResponseMessage(0, "Detail Bill isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.DetailBills.Remove(detailBill);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete Detail Bill is successful!");
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