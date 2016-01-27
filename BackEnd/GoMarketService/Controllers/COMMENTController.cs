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
    public class COMMENTController : TableController<COMMENT>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<COMMENT>(context, Request);
        }

        // GET tables/COMMENT
        public IQueryable<COMMENT> GetAllCOMMENT()
        {
            return Query(); 
        }

        // GET tables/COMMENT/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<COMMENT> GetCOMMENT(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/COMMENT/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<COMMENT> PatchCOMMENT(string id, Delta<COMMENT> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/COMMENT
        public async Task<IHttpActionResult> PostCOMMENT(COMMENT item)
        {
            COMMENT current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/COMMENT/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCOMMENT(string id)
        {
             return DeleteAsync(id);
        }
    }
}
