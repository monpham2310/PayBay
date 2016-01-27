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
    public class BILLController : TableController<BILL>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<BILL>(context, Request);
        }

        // GET tables/BILL
        public IQueryable<BILL> GetAllBILL()
        {
            return Query(); 
        }

        // GET tables/BILL/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<BILL> GetBILL(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/BILL/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<BILL> PatchBILL(string id, Delta<BILL> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/BILL
        public async Task<IHttpActionResult> PostBILL(BILL item)
        {
            BILL current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/BILL/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteBILL(string id)
        {
             return DeleteAsync(id);
        }
    }
}
