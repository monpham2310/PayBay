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
    public class USERSController : TableController<USERS>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<USERS>(context, Request);
        }

        // GET tables/USERS
        public IQueryable<USERS> GetAllUSERS()
        {
            return Query(); 
        }

        // GET tables/USERS/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<USERS> GetUSERS(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/USERS/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<USERS> PatchUSERS(string id, Delta<USERS> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/USERS
        public async Task<IHttpActionResult> PostUSERS(USERS item)
        {
            USERS current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/USERS/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUSERS(string id)
        {
             return DeleteAsync(id);
        }
    }
}
