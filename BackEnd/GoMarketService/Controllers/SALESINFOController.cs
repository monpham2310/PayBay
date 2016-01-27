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
    public class SALESINFOController : TableController<SALESINFO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<SALESINFO>(context, Request);
        }

        // GET tables/SALESINFO
        public IQueryable<SALESINFO> GetAllSALESINFO()
        {
            return Query(); 
        }

        // GET tables/SALESINFO/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<SALESINFO> GetSALESINFO(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/SALESINFO/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<SALESINFO> PatchSALESINFO(string id, Delta<SALESINFO> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/SALESINFO
        public async Task<IHttpActionResult> PostSALESINFO(SALESINFO item)
        {
            SALESINFO current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/SALESINFO/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteSALESINFO(string id)
        {
             return DeleteAsync(id);
        }
    }
}
