using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class ContacDetails : Form
    {
        private BusineeLogincLayer _busineeLogincLayer;
        private Contact _contact;
        public ContacDetails()
        {
            InitializeComponent();
            _busineeLogincLayer = new BusineeLogincLayer();
        }        

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();
        }

        #region PRIVATE
        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAddress.Text;

            contact.Id = _contact != null ? _contact.Id : 0;

            _busineeLogincLayer.SaveContact(contact);
        }
        private void ClearForm()
        {
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtAddress.Text = String.Empty;
        }
        #endregion

        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm();
                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAddress.Text = contact.Address;
            }
        }

    }
}
