using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ScanIt
{
    public partial class מוזמנים : Form
    {
        private SerialPort _serialPort;
        private delegate void SetTextDeleg(string text);

        private QueryExec queryExec = new QueryExec();
        private Form2 frm2 = new Form2();
        public מוזמנים()
        {
            InitializeComponent();
            this.ActiveControl = textBarcode;

        /*    _serialPort = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One);
            _serialPort.Handshake = Handshake.None;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            _serialPort.Open();
            */

        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = _serialPort.ReadLine();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }
        private void si_DataReceived(string data)
        {
            textBarcode.Text = data.Trim();
        }


        private void btnAddForm_Click(object sender, EventArgs e)
        {
            frm2.ShowDialog();
        }

        private void textBarcode_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            String query = Queries.getDetailsByBarcode(textBarcode.Text);
            Dictionary<string, List<string>> dataMap = queryExec.performSelectQuery(query);
            if (textBarcode.Text.Trim().Equals(""))
            {
                setTextArea("", Constants.DEFAULT_COLOR);
                return;
            }
            if (dataMap.Count.Equals(0))
            {
                setTextArea("ברקוד לא נמצא במערכת!!!!", Constants.ERR_COLOR);
            }

            else
            {
                handleResults(dataMap);
                String markAsChecked = Queries.markBarcode(textBarcode.Text, true);
                queryExec.performInsertQuery(markAsChecked);
            }

        }

        private void handleResults(Dictionary<string, List<string>> dataMap)
        {
           

            foreach (var pair in dataMap)
            {
                string key = pair.Key;
                if (key.Equals("checked"))
                {
                    if (pair.Value[0] == "True")
                    {
                        setTextArea("ברקוד זה כבר נבדק!!!", Constants.WARN_COLOR);
                        return;
                    }
                    else
                    {
                        setTextArea("", Constants.SUCCESS_COLOR);
                    }
                }
               
                if (key.Equals("invitor"))
                {
                    setHeader(richTextBox1,"שם המזמין:");
                }
                else if (key.Equals("invited"))
                {
                    setHeader(richTextBox1,"שם המוזמן:");
                }
                else
                {
                    continue;
                }

                List<string> values = pair.Value;

                foreach (string val in values)
                {
                    setBody(richTextBox1, val);                           
                }

                richTextBox1.AppendText(Environment.NewLine);

            }
        }

        private void setHeader(RichTextBox rtb, string text)
        {
            setHeaderStyle(rtb);
            setHeaderContent(rtb, text);
        }

        private void setHeaderContent(RichTextBox rtb,string text)
        {
            rtb.AppendText(text);
            rtb.AppendText(Environment.NewLine);
           // rtb.AppendText(Constants.HR);
            rtb.AppendText(Environment.NewLine);
        }
        private void setHeaderStyle(RichTextBox rtb)
        {
            rtb.SelectionFont = new Font("Tahoma", 14, FontStyle.Bold|FontStyle.Underline);
            rtb.SelectionColor = System.Drawing.Color.BlueViolet;
        }



        private void setBody(RichTextBox rtb, string text)
        {
            setBodyStyle(rtb);
            setBodyContent(rtb, text);
        }

        private void setBodyContent(RichTextBox rtb, string text)
        {
            rtb.AppendText(text);
            rtb.AppendText(Environment.NewLine);
        }
        private void setBodyStyle(RichTextBox rtb)
        {
            rtb.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold);
            rtb.SelectionColor = System.Drawing.Color.Brown;
        }

        private void setTextArea(string message, string htmlColor)
        {
            richTextBox1.SelectionFont = new Font("Tahoma", 20, FontStyle.Bold);
            richTextBox1.SelectionColor = System.Drawing.Color.Black;
            richTextBox1.BorderStyle = BorderStyle.Fixed3D;
            richTextBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(htmlColor);
            richTextBox1.AppendText(message);
        }
    }
}