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
    public class STOREController : TableController<STORE>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<STORE>(context, Request);
        }

        // GET tables/STORE
        public IQueryable<STORE> GetAllSTORE()
        {
            return Query(); 
        }

        // GET tables/STORE/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<STORE> GetSTORE(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/STORE/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<STORE> PatchSTORE(string id, Delta<STORE> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/STORE
        public async Task<IHttpActionResult> PostSTORE(STORE item)
        {
            STORE current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/STORE/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSTORE(string id)
        {
             return DeleteAsync(id);
        }
    }
}
