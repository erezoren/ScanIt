using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using ScanIt.Logic;

namespace ScanIt
{
    public partial class Form2 : Form
    {
        private QueryExec queryExec = new QueryExec();
        private Dictionary<long, string> invitorsMap;
        public Form2()
        {
            InitializeComponent();
            string allInvitorsQuery = Queries.selectAllInvitors();
            invitorsMap = flatten(queryExec.performSelectQuery(allInvitorsQuery));

        }

        private List<String> names = new List<string>();
        private Dictionary<long, string> flatten(Dictionary<string, List<string>> invotorsMap)
        {
            Dictionary<long, string> flattened = new Dictionary<long, string>();
            List<String> ids = new List<string>();
            foreach (var pair in invotorsMap)
            {
                List<string> values = pair.Value;
                if (pair.Key.Equals("invitor_id"))
                {
                    ids.AddRange(values);
                }
                else
                {
                    names.AddRange(values);
                }
            }
            int i = 0;
            foreach (string id in ids)
            {
               string name =  names[i++];
                flattened.Add(long.Parse(id), name);
                ComboboxItem item = new ComboboxItem();
                item.id = id;
                item.name = name;
                comboInvitors.Items.Add(item);
            }
            return flattened;
        }

       
  

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                errorProvider1.Clear();
                statusLine.Text = "";
                freezUnfreezForm(true);
                try { 
                long invotorId = long.Parse(((ComboboxItem)comboInvitors.SelectedItem).id);
                string invitedName = txtInvtedFullName.Text;
                string invitedQuery = Queries.insertNewInvited(invitedName, "Added at " + DateTime.Now);
                long invitedId = queryExec.performInsertQuery(invitedQuery);
                string ticketQuery = Queries.insertNewTicket(txtBarcode.Text, invitedId);
                queryExec.performInsertQuery(ticketQuery);
                string invToInvLink = Queries.insertNewInvitation(invotorId, invitedId);
                long id = queryExec.performInsertQuery(invToInvLink);
                if (id > -1) {
                        statusLine.Text = "מוזמן חדש ("+ id + ") הוסף בהצלחה";
                }
                txtInvtedFullName.Clear();
                txtBarcode.Clear();
            }

            catch(QueryExecException ex)
            {
                    errorProvider1.SetError(btnAdd, ex.Message);
            }
                freezUnfreezForm(false);


            }

            
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            errorProvider1.Clear();
            if (txtInvtedFullName.Text.Trim() == "")
            {
                errorProvider1.SetError(txtInvtedFullName, "נא להזין שם מוזמן");
                return false;
            }
            if (!invitorsMap.ContainsValue(comboInvitors.Text.Trim()))
            {
                errorProvider1.SetError(comboInvitors, "נא לבחור מזמין מהרשימה");
                return false;
            }

            if (txtBarcode.Text.Trim() == "")
            {
                errorProvider1.SetError(txtBarcode, "נא להזין ברקוד ולידי");
                return false;
            }
            return true;
        }

        private void import_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                freezUnfreezForm(true);
                errorProvider1.Clear();
                statusLine.Text = "";
                List <string> errors = new List<string>();
                TextFieldParser parser = new TextFieldParser(openFileDialog1.FileName,Encoding.Default);
                int numImported = 0;
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    string invitorName = fields[0];
                    string inviedName = fields[1];
                    string barcode = fields[2];

                    long invotorId;
                    try { 
                    if (invitorsMap.ContainsValue(invitorName))
                    {
                        invotorId = invitorsMap.FirstOrDefault(x => x.Value == invitorName).Key;
                    }
                    else
                    {
                        string newInvitorQuery = Queries.insertNewInvitor(invitorName, "Added at " + DateTime.Now);
                        invotorId = queryExec.performInsertQuery(newInvitorQuery);
                    }

                    string invitedQuery = Queries.insertNewInvited(inviedName, "Added at " + DateTime.Now);
                    long invitedId = queryExec.performInsertQuery(invitedQuery);
                    string ticketQuery = Queries.insertNewTicket(barcode, invitedId);
                    queryExec.performInsertQuery(ticketQuery);
                    string invToInvLink = Queries.insertNewInvitation(invotorId, invitedId);
                    long id = queryExec.performInsertQuery(invToInvLink);
                    }
                    catch(QueryExecException ex)
                    {
                        errors.Add(ex.Message);
                        continue;
                    }
                    numImported++;
                }
                if (errors.Count>0)
                {
                    errorProvider1.SetError(import, String.Join("\r\n",errors));
                }
                statusLine.Text = numImported + " הזמנות יובאו בהצלחה " ;
                freezUnfreezForm(false);
            }
        }

        private void freezUnfreezForm(bool freeze)
        {
            if (freeze) { Cursor.Current = Cursors.WaitCursor; }
            else { Cursor.Current = Cursors.Default; }
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Enabled = !freeze;
                }

        }
    }

    
}
