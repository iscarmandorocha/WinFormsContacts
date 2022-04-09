﻿using System;
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
    public partial class Main : Form
    {
        private BusineeLogincLayer _busineeLogincLayer;
        public Main()
        {
            InitializeComponent();
            _busineeLogincLayer = new BusineeLogincLayer();
        }
        #region EVENTS
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDeatailDialog();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            PopulateContacts();
        }
        #endregion

        #region PRIVATE METHODS
        private void OpenContactDeatailDialog()
        {
            ContacDetails cd = new ContacDetails();
            cd.ShowDialog(this);
        }
        public void PopulateContacts(string Search = null)
        {
            List<Contact> contacts = _busineeLogincLayer.GetContact(Search);
            gridContacts.DataSource = contacts;
        }
        #endregion

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContacDetails cd = new ContacDetails();
                cd.LoadContact(new Contact
                {
                    Id = int.Parse((gridContacts.Rows[e.RowIndex].Cells[0]).Value.ToString()),
                    FirstName = (gridContacts.Rows[e.RowIndex].Cells[1]).Value.ToString(),
                    LastName = (gridContacts.Rows[e.RowIndex].Cells[2]).Value.ToString(),
                    Phone = (gridContacts.Rows[e.RowIndex].Cells[3]).Value.ToString(),
                    Address = (gridContacts.Rows[e.RowIndex].Cells[4]).Value.ToString(),
                });
                cd.ShowDialog(this);
            }
            if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse((gridContacts.Rows[e.RowIndex].Cells[0]).Value.ToString()));
            }

        }

        private void DeleteContact(int id)
        {
            _busineeLogincLayer.DeleteContact(id);
            PopulateContacts();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateContacts(TxtSearch.Text);
            TxtSearch.Text = string.Empty;
        }
    }
}
