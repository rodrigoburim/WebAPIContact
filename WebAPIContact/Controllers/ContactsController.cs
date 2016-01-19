using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIContact.Models;

namespace WebAPIContact.Controllers
{
    public class ContactsController : ApiController
    {
        //TODO: use dependecy injection
        // http://www.asp.net/web-api/overview/extensibility/using-the-web-api-dependency-resolver
        private readonly IEntityRepository<Contact> repository = new EntityRepository<Contact>(new WebAPIContactContext());

        public IEnumerable<Contact> GetAllContacts()
        {
            return repository.GetAll();
        }

        public Contact GetContact(int id)
        {
            Contact item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpPost]
        public HttpResponseMessage AddContact(Contact item)
        {
            //TODO: validate item
            // http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
            repository.Create(item);
            var response = Request.CreateResponse<Contact>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpPut]
        public void UpdateContact(int id, Contact item)
        {
            item.Id = id;
            repository.Update(item);
        }

        [HttpDelete]
        public void DeleteContract(int id)
        {
            Contact item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}
