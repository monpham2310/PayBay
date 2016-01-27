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
    public class MARKETController : TableController<MARKET>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<MARKET>(context, Request);
        }

        // GET tables/MARKET
        public IQueryable<MARKET> GetAllMARKET()
        {
            return Query(); 
        }

        // GET tables/MARKET/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<MARKET> GetMARKET(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/MARKET/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<MARKET> PatchMARKET(string id, Delta<MARKET> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/MARKET
        public async Task<IHttpActionResult> PostMARKET(MARKET item)
        {
            MARKET current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/MARKET/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMARKET(string id)
        {
             return DeleteAsync(id);
        }
    }
}
