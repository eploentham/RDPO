using System;
using System.Collections.Generic;
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
        public ControlRDPO()
        {
            iniFile = new IniFile(Environment.CurrentDirectory + "\\" + Application.ProductName + ".ini");
            initC = new InitC();
            GetConfig();

            conn = new ConnectDB("kfc_po", initC);

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
    }
}
