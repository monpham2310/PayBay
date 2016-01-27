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
    public class REVENUESTATISTICController : TableController<REVENUESTATISTIC>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<REVENUESTATISTIC>(context, Request);
        }

        // GET tables/REVENUESTATISTIC
        public IQueryable<REVENUESTATISTIC> GetAllREVENUESTATISTIC()
        {
            return Query(); 
        }

        // GET tables/REVENUESTATISTIC/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<REVENUESTATISTIC> GetREVENUESTATISTIC(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/REVENUESTATISTIC/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<REVENUESTATISTIC> PatchREVENUESTATISTIC(string id, Delta<REVENUESTATISTIC> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/REVENUESTATISTIC
        public async Task<IHttpActionResult> PostREVENUESTATISTIC(REVENUESTATISTIC item)
        {
            REVENUESTATISTIC current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/REVENUESTATISTIC/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteREVENUESTATISTIC(string id)
        {
             return DeleteAsync(id);
        }
    }
}
