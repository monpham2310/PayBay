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
    public class PRODUCTController : TableController<PRODUCT>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<PRODUCT>(context, Request);
        }

        // GET tables/PRODUCT
        public IQueryable<PRODUCT> GetAllPRODUCT()
        {
            return Query(); 
        }

        // GET tables/PRODUCT/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PRODUCT> GetPRODUCT(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PRODUCT/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PRODUCT> PatchPRODUCT(string id, Delta<PRODUCT> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PRODUCT
        public async Task<IHttpActionResult> PostPRODUCT(PRODUCT item)
        {
            PRODUCT current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PRODUCT/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePRODUCT(string id)
        {
             return DeleteAsync(id);
        }
    }
}
