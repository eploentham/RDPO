using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPO
{
    public class XcustLinfoxPrTblDB
    {
        XcustLinfoxPrTbl xCLFPT;
        ControlRDPO cRDPO;
        public XcustLinfoxPrTblDB(ControlRDPO crdpo)
        {
            cRDPO = crdpo;
            initConfig();
        }
        private void initConfig()
        {
            xCLFPT = new XcustLinfoxPrTbl();
            xCLFPT.COMPANY = "COMPANY";
            xCLFPT.DELIVERY_INSTRUCTION = "DELIVERY_INSTRUCTION";
            xCLFPT.ERROR_MSG = "ERROR_MSG";
            xCLFPT.ITEM_NUMBER = "ITEM_NUMBER";
            xCLFPT.LINE_NUMBER = "LINE_NUMBER";
            xCLFPT.ORDER_DATE = "ORDER_DATE";
            xCLFPT.ORDER_TIME = "ORDER_TIME";
            xCLFPT.PO_NUMBER = "PO_NUMBER";
            xCLFPT.PROCESS_FLAG = "PROCESS_FLAG";
            xCLFPT.QTY = "QTY";
            xCLFPT.SUPPLIER_CODE = "SUPPLIER_CODE";
            xCLFPT.UOM = "UOM";
            xCLFPT.VALIDATE_FLAG = "VALIDATE_FLAG";

            xCLFPT.table = "xcust_linfox_pr_tbl";
        }
        //public static void BulkToMySQL()
        //{
        //    string ConnectionString = "server=192.168.1xxx";
        //    StringBuilder sCommand = new StringBuilder("INSERT INTO User (FirstName, LastName) VALUES ");
        //    using (cRDPO.conn = new MySqlConnection(ConnectionString))
        //    {
        //        List<string> Rows = new List<string>();
        //        for (int i = 0; i < 100000; i++)
        //        {
        //            Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString("test"), MySqlHelper.EscapeString("test")));
        //        }
        //        sCommand.Append(string.Join(",", Rows));
        //        sCommand.Append(";");
        //        mConnection.Open();
        //        using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
        //        {
        //            myCmd.CommandType = CommandType.Text;
        //            myCmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}
