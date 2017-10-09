using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDPO
{
    public class ControlRDPO
    {
        static String fontName = "Microsoft Sans Serif";
        public String backColor1 = "#1E1E1E";
        public String backColor2 = "#2D2D30";
        public String foreColor1 = "#fff";
        static float fontSize9 = 9.75f;
        static float fontSize8 = 8.25f;
        public Font fV1B, fV1;
        public int tcW = 0, tcH = 0, tcWMinus = 25, tcHMinus = 70, formFirstLineX = 5, formFirstLineY = 5;

        public ConnectDB conn;

        private IniFile iniFile;
        public InitC initC;
        public XcustLinfoxPrTblDB xCLPRTDB;
        public XcustPorReqHeaderIntAllDB xCPRHIADB;
        public XcustPorReqLineIntAllDB xCPRLIADB;
        public XcustPorReqDistIntAllDB xCPRDIADB;
        public ControlRDPO()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");
            initC = new InitC();
            GetConfig();

            conn = new ConnectDB("kfc_po", initC);
            xCLPRTDB = new XcustLinfoxPrTblDB(conn);
            xCPRHIADB = new XcustPorReqHeaderIntAllDB(conn);
            xCPRLIADB = new XcustPorReqLineIntAllDB(conn);
            xCPRDIADB = new XcustPorReqDistIntAllDB(conn);

            fontSize9 = 9.75f;
            fontSize8 = 8.25f;
            fV1B = new Font(fontName, fontSize9, FontStyle.Bold);
            fV1 = new Font(fontName, fontSize8, FontStyle.Regular);
            
        }
        public void GetConfig()
        {
            initC.PathArchive = iniFile.Read("PathArchive");    //bit
            initC.PathError = iniFile.Read("PathError");
            initC.PathInitial = iniFile.Read("PathInitial");
            initC.PathProcess = iniFile.Read("PathProcess");
            initC.portDBBIT = iniFile.Read("portDBBIT");

            initC.databaseDBBITDemo = iniFile.Read("databaseDBBITDemo");    //bit demo
            initC.hostDBBITDemo = iniFile.Read("hostDBBITDemo");
            initC.userDBBITDemo = iniFile.Read("userDBBITDemo");
            initC.passDBBITDemo = iniFile.Read("passDBBITDemo");
            initC.portDBBITDemo = iniFile.Read("portDBBITDemo");

            initC.databaseDBORCMA = iniFile.Read("databaseDBORCMA");      //orc master
            initC.hostDBORCMA = iniFile.Read("hostDBORCMA");
            initC.userDBORCMA = iniFile.Read("userDBORCMA");
            initC.passDBORCMA = iniFile.Read("passDBORCMA");
            initC.portDBORCMA = iniFile.Read("portDBORCMA");

            initC.databaseDBORCBA = iniFile.Read("databaseDBORCBA");        // orc backoffice
            initC.hostDBORCBA = iniFile.Read("hostDBORCBA");
            initC.userDBORCBA = iniFile.Read("userDBORCBA");
            initC.passDBORCBA = iniFile.Read("passDBORCBA");
            initC.portDBORCBA = iniFile.Read("portDBORCBA");

            initC.databaseDBKFCPO = iniFile.Read("databaseDBKFCPO");        // orc BIT
            initC.hostDBKFCPO = iniFile.Read("hostDBKFCPO");
            initC.userDBKFCPO = iniFile.Read("userDBKFCPO");
            initC.passDBKFCPO = iniFile.Read("passDBKFCPO");
            initC.portDBKFCPO = iniFile.Read("portDBKFCPO");
            //initC.quoLine6 = iniFile.Read("quotationline6");

            //initC.grdQuoColor = iniFile.Read("gridquotationcolor");

            //initC.HideCostQuotation = iniFile.Read("hidecostquotation");
            //if (initC.grdQuoColor.Equals(""))
            //{
            //    initC.grdQuoColor = "#b7e1cd";
            //}
            //initC.Password = regE.getPassword();
        }
        public void CreateIfMissing(String path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public String[] getFileinFolder(String path)
        {
            string[] filePaths = Directory.GetFiles(@path);
            return filePaths;
        }
        public void moveFile(String sourceFile, String destinationFile)
        {
            System.IO.File.Move(@sourceFile, @destinationFile);
        }
        private Boolean validateLinfox(DataRow row)
        {
            //row[dc].ToString().Trim()
            return true;
        }
        public void processRDPO(String[] filePO)
        {
            ReadText rd = new ReadText();
            String[] filePOProcess;
            DataTable dt = new DataTable();
            Boolean chk = false;

            // b.	Program ทำการ Move File มาไว้ที่ Path ตาม Parameter Path Process
            foreach (string aa in filePO)
            {
                moveFile(aa, initC.PathProcess + aa.Replace(initC.PathInitial, ""));
            }

            //c.	จากนัน Program ทำการอ่าน File ใน Folder Path Process มาไว้ยัง Table XCUST_LINFOX_PR_TBL ด้วย Validate Flag = ‘N’ ,PROCES_FLAG = ‘N’
            // insert XCUST_LINFOX_PR_TBL
            filePOProcess = getFileinFolder(initC.PathProcess);
            foreach (string aa in filePOProcess)
            {
                List<String> linfox = rd.ReadTextFile(aa);
                conn.BulkToMySQL("kfc_po", linfox);

                dt.Clear();
                //d.	จากนั้น Program จะเอาข้อมูลจาก Table XCUST_LINFOX_PR_TBL มาทำการ Validate 
                //e.กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL, XCUST_POR_REQ_LINE_INT_ALL, XCUST_POR_REQ_DIST_INT_ALL
                dt = xCLPRTDB.selectLinfox();
                foreach (DataRow row in dt.Rows)
                {
                    chk = validateLinfox(row);
                    if (chk)
                    {
                        //e.	กรณีที่ Validat ผ่าน จะเอาข้อมูล Insert ลง table XCUST_POR_REQ_HEADER_INT_ALL,XCUST_POR_REQ_LINE_INT_ALL ,XCUST_POR_REQ_DIST_INT_ALLและ Update Validate_flag = ‘Y’
                        insertXcustPorReqHeaderIntAll(row);

                        insertXcustPorReqLineIntAll(row);

                        insertXcustPorReqDistIntAll(row);
                        //และ Update Validate_flag = ‘Y’

                    }
                    else
                    {
                        //f.	กรณีที่ Validate ไม่ผ่าน จะะ Update Validate_flag = ‘E’ พร้อมระบุ Error Message
                    }
                }

            }
            
        }
        private void insertXcustPorReqHeaderIntAll(DataRow row)
        {
            XcustPorReqHeaderIntAll xCPRHA = new XcustPorReqHeaderIntAll();
            xCPRHA.ATTRIBUTE1 = "";
            xCPRHA.ATTRIBUTE_DATE1 = "";
            xCPRHA.ATTRIBUTE_TIMESTAMP1 = "";
            xCPRHA.Batch_ID = "";
            xCPRHA.Description = "";
            xCPRHA.ENTER_BY = "";
            xCPRHA.import_source = "";
            xCPRHA.LINFOX_PR = "";
            xCPRHA.PO_NUMBER = "";
            xCPRHA.PROCESS_FLAG = "";
            xCPRHA.PR_APPROVER = "";
            xCPRHA.PR_STATAUS = "";
            xCPRHA.Requisitioning_BU = "";
            xCPRHA.Requisition_Number = "";
            xCPRHIADB.insert(xCPRHA);
        }
        private void insertXcustPorReqLineIntAll(DataRow row)
        {
            XcustPorReqLineIntAll xCPRLIA = new XcustPorReqLineIntAll();
            xCPRLIA.ATTRIBUTE1 = "";
            xCPRLIA.ATTRIBUTE_DATE1 = "";
            xCPRLIA.ATTRIBUTE_NUMBER1 = "";
            xCPRLIA.ATTRIBUTE_TIMESTAMP1 = "";
            xCPRLIA.Category_Name = "";
            xCPRLIA.CURRENCY_CODE = "";
            xCPRLIA.Deliver_to_Location = "";
            xCPRLIA.Deliver_to_Organization = "";
            xCPRLIA.Goods = "";
            xCPRLIA.INVENTORY = "";
            xCPRLIA.ITEM_NUMBER = "";
            xCPRLIA.LINFOX_PR = "";
            xCPRLIA.Need_by_Date = "";
            xCPRLIA.PO_LINE_NUMBER = "";
            xCPRLIA.PO_NUMBER = "";
            xCPRLIA.Price = "";
            xCPRLIA.PROCESS_FLAG = "";
            xCPRLIA.Procurement_BU = "";
            xCPRLIA.PR_APPROVER = "";
            xCPRLIA.QTY = "";
            xCPRLIA.requester = "";
            xCPRLIA.Requisitioning_BU = "";
            xCPRLIA.Subinventory = "";
            xCPRLIA.SUPPLIER_CODE = "";
            xCPRLIA.Supplier_Site = "";
            xCPRLIADB.insert(xCPRLIA);
        }
        private void insertXcustPorReqDistIntAll(DataRow row)
        {
            XcustPorReqDistIntAll xCPRDIA = new XcustPorReqDistIntAll();
            xCPRDIA.ATTRIBUTE1 = "";
            xCPRDIA.ATTRIBUTE_CATEGORY = "";
            xCPRDIA.ATTRIBUTE_DATE1 = "";
            xCPRDIA.ATTRIBUTE_NUMBER1 = "";
            xCPRDIA.ATTRIBUTE_TIMESTAMP1 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT1 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT2 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT3 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT4 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT5 = "";
            xCPRDIA.CHARGE_ACCOUNT_SEGMENT6 = "";
            xCPRDIA.PO_LINE_NUMBER = "";
            xCPRDIA.PO_NUMBER = "";
            xCPRDIA.PROCESS_FLAG = "";
            xCPRDIA.Program_running = "";
            xCPRDIA.QTY = "";
            xCPRDIADB.insert(xCPRDIA);
        }
    }
}
