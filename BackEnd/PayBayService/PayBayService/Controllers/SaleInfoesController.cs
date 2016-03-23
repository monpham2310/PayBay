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
    public class SaleInfoesController : ApiController
    {
        private PayBayDatabaseEntities db = new PayBayDatabaseEntities();

        // GET: api/SaleInfoes
        public HttpResponseMessage GetSaleInfoes()
        {
            JArray result = new JArray();
            try
            {
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetAllSaleInfo", CommandType.StoredProcedure, ref Methods.err);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/SaleInfoes/5
        [ResponseType(typeof(SaleInfo))]
        public HttpResponseMessage GetSaleInfo(int id, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var saleId = new SqlParameter("@SaleId", id);
                var required = new SqlParameter("@isRequired", true);
                if (type == TYPE.OLD)
                {                                     
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_LoadAllSale", CommandType.StoredProcedure, ref Methods.err, saleId, required);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_LoadNewSale", CommandType.StoredProcedure, ref Methods.err, saleId, required);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/SaleInfoes/5
        [ResponseType(typeof(SaleInfo))]
        public HttpResponseMessage GetSaleInfo(int id, string title, TYPE type)
        {
            JArray result = new JArray();
            try
            {
                var saleId = new SqlParameter("@SaleId", id);
                var saleName = new SqlParameter("@SaleName", title);
                var required = new SqlParameter("@isRequired", true);
                if (type == TYPE.OLD)
                {                                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_LoadAllSaleWithTitle", CommandType.StoredProcedure, ref Methods.err, saleId, saleName, required);
                }
                else
                {                    
                    result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_LoadNewSaleWithTitle", CommandType.StoredProcedure, ref Methods.err, saleId, saleName, required);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/SaleInfoes/KM
        [ResponseType(typeof(SaleInfo))]
        public HttpResponseMessage GetSaleInfo(int storeId, bool required)
        {
            JArray result = new JArray();
            try
            {
                var id = new SqlParameter("@StoreID", storeId);
                var pRequired = new SqlParameter("@isRequired", required);
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetSaleInfoOfStore", CommandType.StoredProcedure, ref Methods.err, id, pRequired);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: api/SaleInfoes/KM
        [ResponseType(typeof(SaleInfo))]
        public HttpResponseMessage GetImageSale(bool required)
        {
            JArray result = new JArray();
            try
            {                
                var pRequired = new SqlParameter("@isRequired", required);
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_GetImageSale", CommandType.StoredProcedure, ref Methods.err, pRequired);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message.ToString());
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // PUT: api/SaleInfoes/5
        [ResponseType(typeof(HttpResponseMessage))]
        public async Task<HttpResponseMessage> PutSaleInfo(SaleInfo saleInfo)
        {
            JObject result = new JObject();
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
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

        // PUT: api/SaleInfoes/isRequired
        [ResponseType(typeof(HttpResponseMessage))]
        public HttpResponseMessage PutHomePage(int saleId, bool isRequired)
        {
            JArray result = new JArray();
            try
            {
                var sale = new SqlParameter("@SaleId", saleId);
                var required = new SqlParameter("@isRequired", isRequired);
                result = Methods.GetInstance().ExecQueryWithResult("viethung_paybayservice.sp_AllowShowHomePage", CommandType.StoredProcedure, ref Methods.err, sale, required);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

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

            var table = new SqlParameter("@table", "viethung_paybayservice.SaleInfo");
            int saleId = Convert.ToInt32(Methods.GetInstance().GetValue("viethung_paybayservice.sp_GetMaxId", CommandType.StoredProcedure, ref Methods.err, table));
            ModelBlob blob = await Methods.GetInstance().GetSasAndImageUriFromBlob("sales", saleInfo.Title, saleId + 1);

            if (blob != null)
            {
                saleInfo.Image = blob.ImageUri;
                saleInfo.SasQuery = blob.SasQuery;
                db.SaleInfoes.Add(saleInfo);
                await db.SaveChangesAsync();
                result = JObject.FromObject(saleInfo);
            }
            else
            {
                result = Methods.CustomResponseMessage(0, "Could not retrieve Sas and Uri settings!");
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
                        
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