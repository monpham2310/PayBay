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
    public class PRODUCTSTATISTICController : TableController<PRODUCTSTATISTIC>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<PRODUCTSTATISTIC>(context, Request);
        }

        // GET tables/PRODUCTSTATISTIC
        public IQueryable<PRODUCTSTATISTIC> GetAllPRODUCTSTATISTIC()
        {
            return Query(); 
        }

        // GET tables/PRODUCTSTATISTIC/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PRODUCTSTATISTIC> GetPRODUCTSTATISTIC(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PRODUCTSTATISTIC/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PRODUCTSTATISTIC> PatchPRODUCTSTATISTIC(string id, Delta<PRODUCTSTATISTIC> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PRODUCTSTATISTIC
        public async Task<IHttpActionResult> PostPRODUCTSTATISTIC(PRODUCTSTATISTIC item)
        {
            PRODUCTSTATISTIC current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PRODUCTSTATISTIC/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePRODUCTSTATISTIC(string id)
        {
             return DeleteAsync(id);
        }
    }
}
