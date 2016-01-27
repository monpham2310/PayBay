using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using GoMarketService.DataObjects;
using GoMarketService.Models;

namespace GoMarketService.Controllers
{
    public class DETAILBILLController : TableController<DETAILBILL>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<DETAILBILL>(context, Request);
        }

        // GET tables/DETAILBILL
        public IQueryable<DETAILBILL> GetAllDETAILBILL()
        {
            return Query(); 
        }

        // GET tables/DETAILBILL/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<DETAILBILL> GetDETAILBILL(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/DETAILBILL/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<DETAILBILL> PatchDETAILBILL(string id, Delta<DETAILBILL> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/DETAILBILL
        public async Task<IHttpActionResult> PostDETAILBILL(DETAILBILL item)
        {
            DETAILBILL current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/DETAILBILL/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteDETAILBILL(string id)
        {
             return DeleteAsync(id);
        }
    }
}
