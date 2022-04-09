using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    internal class BusineeLogincLayer
    {
        private DataAccessLayer _dataAccessLayer;

        public BusineeLogincLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }

        public Contact SaveContact(Contact contact)
        {
            if (contact.Id == 0)
                _dataAccessLayer.InsertContact(contact);
            else
                _dataAccessLayer.UpdateContact(contact);
            return contact;
        }

        public List<Contact> GetContact(string Search = null)
        {
            return _dataAccessLayer.GetContacts(Search);
        }

        public void DeleteContact(int contact)
        {
            _dataAccessLayer.Delete(contact);
        }
    }
}
