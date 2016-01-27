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
    public class USERController : TableController<USER>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<USER>(context, Request);
        }

        // GET tables/USER
        public IQueryable<USER> GetAllUSER()
        {
            return Query(); 
        }

        // GET tables/USER/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<USER> GetUSER(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/USER/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<USER> PatchUSER(string id, Delta<USER> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/USER
        public async Task<IHttpActionResult> PostUSER(USER item)
        {
            USER current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/USER/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUSER(string id)
        {
             return DeleteAsync(id);
        }
    }
}
