using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using paybayserviceService.DataObjects;
using paybayserviceService.Models;

namespace paybayserviceService.Controllers
{
    public class UserTypeController : TableController<UserType>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            paybayserviceContext context = new paybayserviceContext();
            DomainManager = new EntityDomainManager<UserType>(context, Request, Services);
        }

        // GET tables/UserType
        public IQueryable<UserType> GetAllUserType()
        {
            return Query(); 
        }

        // GET tables/UserType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<UserType> GetUserType(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/UserType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<UserType> PatchUserType(string id, Delta<UserType> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/UserType
        public async Task<IHttpActionResult> PostUserType(UserType item)
        {
            UserType current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/UserType/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteUserType(string id)
        {
             return DeleteAsync(id);
        }

    }
}