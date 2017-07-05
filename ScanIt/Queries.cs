using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScanIt
{
    class Queries
    {
        public static string insertNewInvitor(string fullName, string comments)
        {
            return "INSERT INTO invitors (full_name,comments) VALUES('"+fullName+"','"+comments+"')";
        }

        public static string insertNewInvited(string fullName, string comments)
        {
            return "INSERT INTO invited (full_name,comments) VALUES('" + fullName + "','" + comments + "')";
        }

        public static string insertNewInvitation(long invitorId, long invitedId)
        {
            return "INSERT INTO invetations (invitor_id,invited_id) VALUES(" + invitorId + "," + invitedId + ")";
        }


        public static string insertNewTicket(string barcode, long invitedId)
        {
            return "INSERT INTO tickets (barcode,invited_id) VALUES('" + barcode + "'," + invitedId + ")";
        }


        public static string getDetailsByBarcode(string barcode)
        {
            return "SELECT i.full_name as invitor,i.comments as invitor_com,e.full_name as invited,e.comments as invited_com,t.checked" + 
                 " FROM tickets t,invitors i, invited e,invetations s"+
                 " WHERE t.barcode='"+barcode+"'"+
                 " AND t.invited_id = e.invited_id" +
                 " AND i.invitor_id = s.invitor_id"+
                 " AND e.invited_id = s.invited_id";
        }

        public static string markBarcode(string barcode,bool isChecked)
        {
            return "UPDATE tickets SET checked ="+isChecked+"  WHERE barcode='"+barcode+"'";
        }

        public static string selectAllInvitors(){
            return "SELECT invitor_id,full_name FROM invitors order by full_name ASC";
        }



    }
}
