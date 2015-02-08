using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIContact.Models
{
    public class ContactRepository : IContactRepository
    {
        private List<Contact> contacts = new List<Contact>();
        private int _nextId = 1;

        public ContactRepository()
        {
            Add(new Contact { FirstName = "John", LastName = "Marx", Email = "john@ggg.com", Phone = "+554433334444" });
            Add(new Contact { FirstName = "Maria", LastName = "McDonald", Email = "mary@ggg.com", Phone = "+554488889999" });
            Add(new Contact { FirstName = "Zac", LastName = "King", Email = "kaack@ggg.com", Phone = "+5544987652343" });
        }

        public IEnumerable<Contact> GetAll()
        {
            return contacts;
        }

        public Contact Get(int id)
        {
            return contacts.Find(p => p.Id == id);
        }

        public Contact Add(Contact item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            contacts.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            contacts.RemoveAll(p => p.Id == id);
        }

        public bool Update(Contact item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = contacts.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            contacts.RemoveAt(index);
            contacts.Add(item);
            return true;
        }
    }
}