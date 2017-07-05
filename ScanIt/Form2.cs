using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ScanIt
{
    public partial class Form2 : Form
    {
        private QueryExec queryExec = new QueryExec();

        public Form2()
        {
            InitializeComponent();
            string allInvitorsQuery = Queries.selectAllInvitors();
            Dictionary<string, List<string>> invitorsMap = queryExec.performSelectQuery(allInvitorsQuery);
            List<Invitor> invitors = fromMap(invitorsMap);
            foreach (Invitor invitor in invitors)
            {
                 ComboboxItem item = new ComboboxItem();
                 item.id = invitor.getId();
                 item.name = invitor.getName();
                 comboInvitors.Items.Add(item);
              }
        }

        private List<String> names = new List<string>();
        private List<Invitor> fromMap(Dictionary<string, List<string>> invitorsMap)
        {
            List<Invitor> invitors = new List<Invitor>(invitorsMap.Count);
            List<String> ids = new List<string>();
            foreach (var pair in invitorsMap)
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
                invitors.Add(new Invitor(id, names[i++]));
            }


            return invitors;
        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {

                long invotorId = long.Parse(((ComboboxItem)comboInvitors.SelectedItem).id);
                string invitedName = txtInvtedFullName.Text;
                string invitedQuery = Queries.insertNewInvited(invitedName, "Added by Erez");
                long invitedId = queryExec.performInsertQuery(invitedQuery);             
                string ticketQuery = Queries.insertNewTicket(txtBarcode.Text, invitedId);
                queryExec.performInsertQuery(ticketQuery);
                string invToInvLink = Queries.insertNewInvitation(invotorId, invitedId);
               long id =  queryExec.performInsertQuery(invToInvLink);
                if(id>-1){
                    MessageBox.Show("A new invitee was successfully added id=" + id, "Success");
                }
                txtInvtedFullName.Clear();
                txtBarcode.Clear();


               
            }

            
           
        }

        private bool ValidateForm()
        {
            if (txtInvtedFullName.Text.Trim() == "")
            {
                MessageBox.Show("You must insert an invited name");
                return false;
            }
            if (!names.Contains(comboInvitors.Text.Trim()))
            {
                MessageBox.Show("You must choos a valid invitor name");
                return false;
            }

            if (txtBarcode.Text.Trim() == "")
            {
                MessageBox.Show("You must insert a Barcode");
                return false;
            }
            return true;
        } 
    }

    
}
