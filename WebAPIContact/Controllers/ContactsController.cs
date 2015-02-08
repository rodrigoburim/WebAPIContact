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
        static readonly IContactRepository repository = new ContactRepository();

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
            item = repository.Add(item);
            var response = Request.CreateResponse<Contact>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [HttpPut]
        public void UpdateContact(int id, Contact item)
        {
            item.Id = id;
            if(!repository.Update(item))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        public void DeleteContract(int id)
        {
            Contact item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
