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
    public class USERTYPEController : TableController<USERTYPE>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            GoMarketContext context = new GoMarketContext();
            DomainManager = new EntityDomainManager<USERTYPE>(context, Request);
        }

        // GET tables/USERTYPE
        public IQueryable<USERTYPE> GetAllUSERTYPE()
        {
            return Query(); 
        }

        // GET tables/USERTYPE/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<USERTYPE> GetUSERTYPE(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/USERTYPE/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<USERTYPE> PatchUSERTYPE(string id, Delta<USERTYPE> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/USERTYPE
        public async Task<IHttpActionResult> PostUSERTYPE(USERTYPE item)
        {
            USERTYPE current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/USERTYPE/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUSERTYPE(string id)
        {
             return DeleteAsync(id);
        }
    }
}
