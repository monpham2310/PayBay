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
    public class SaleInfoesController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/SaleInfoes
        public IQueryable<SaleInfo> GetSaleInfoes()
        {
            return db.SaleInfoes;
        }

        // GET: api/SaleInfoes/5
        [ResponseType(typeof(SaleInfo))]
        public async Task<IHttpActionResult> GetSaleInfo(int id)
        {
            SaleInfo saleInfo = await db.SaleInfoes.FindAsync(id);
            if (saleInfo == null)
            {
                return NotFound();
            }

            return Ok(saleInfo);
        }

        // PUT: api/SaleInfoes/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutSaleInfo(SaleInfo saleInfo)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,ModelState);
            }
                        
            db.Entry(saleInfo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleInfoExists(saleInfo.SaleId))
                {
                    result = Methods.CustomResponseMessage(0, "Sale Info isn't exists!");
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                }
                else
                {
                    throw;
                }
            }

            result = Methods.CustomResponseMessage(1, "Update sale info is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST: api/SaleInfoes
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PostSaleInfo(SaleInfo saleInfo)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            db.SaleInfoes.Add(saleInfo);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Add sale info is successful!");
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // DELETE: api/SaleInfoes/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> DeleteSaleInfo(int id)
        {
            JObject result = new JObject();
            SaleInfo saleInfo = await db.SaleInfoes.FindAsync(id);
            if (saleInfo == null)
            {
                result = Methods.CustomResponseMessage(0, "Sale info isn't exists!");
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }

            db.SaleInfoes.Remove(saleInfo);
            await db.SaveChangesAsync();

            result = Methods.CustomResponseMessage(1, "Delete sale info is successful!");
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

        private bool SaleInfoExists(int id)
        {
            return db.SaleInfoes.Count(e => e.SaleId == id) > 0;
        }
    }
}