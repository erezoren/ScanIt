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
    public partial class מוזמנים : Form
    {
        private QueryExec queryExec = new QueryExec();
        private Form2 frm2 = new Form2();
        public מוזמנים()
        {
            InitializeComponent();
            this.ActiveControl = textBarcode;

        }

        
        private void btnAddForm_Click(object sender, EventArgs e)
        {
            frm2.ShowDialog();
        }

        private void textBarcode_TextChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
           String query =  Queries.getDetailsByBarcode(textBarcode.Text);
           Dictionary<string, List<string>> dataMap =  queryExec.performSelectQuery(query);
           if (dataMap.Count.Equals(0))
           {
               setNotFound("!!!!ברקוד לא נמצא במערכת");
           }

           else
           {
               handleResults(dataMap);
               String markAsChecked = Queries.markBarcode(textBarcode.Text,true);
               queryExec.performInsertQuery(markAsChecked);
           }

        }

        private void handleResults(Dictionary<string, List<string>> dataMap)
        {
            textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#90EE90");

            foreach (var pair in dataMap)
            {
                string key = pair.Key;
                if (key.Equals("checked"))
                {
                    if(pair.Value[0]=="True"){
                        setNotFound("!!!ברקוד זה כבר נבדק");
                        return;
                    }
                }
                if (key.Equals("invitor"))
                {
                    textBox2.AppendText(":שם המזמין");
                    textBox2.AppendText(Environment.NewLine);
                    textBox2.AppendText("==================================");
                    textBox2.AppendText(Environment.NewLine);

                }
                else if (key.Equals("invited"))
                {
                    textBox2.AppendText(":שם המוזמן");
                    textBox2.AppendText(Environment.NewLine);
                    textBox2.AppendText("==================================");
                    textBox2.AppendText(Environment.NewLine);
                }
                else
                {
                    continue;
                }

                List<string> values = pair.Value;

                foreach (string val in values)
                {
                    textBox2.AppendText(val);
                    textBox2.AppendText(Environment.NewLine);
                }

                textBox2.AppendText(Environment.NewLine);
                textBox2.AppendText(Environment.NewLine);

            }
        }

        private void setNotFound(string message)
        {
            textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml("#DC143C");
            textBox2.AppendText(message);
        }
    }
}
