using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPO
{
    public class InitC
    {
        public String PathInitial = "", PathProcess="",PathError="",PathArchive="";

        public String databaseDBBIT = "bithis_demo1";             //bit
        public String hostDBBIT = "172.25.1.153";
        public String userDBBIT = "sa";
        public String passDBBIT = "Orawanhospital1*";
        public String portDBBIT = "3306";

        public String databaseDBBITDemo = "bithis_demo";             //bit demo
        public String hostDBBITDemo = "172.25.1.153";
        public String userDBBITDemo = "sa";
        public String passDBBITDemo = "Orawanhospital1*";
        public String portDBBITDemo = "3306";

        public String databaseDBORCMA = "hisorc_ma";        //orc master
        public String hostDBORCMA = "172.25.1.153";
        public String userDBORCMA = "root";
        public String passDBORCMA = "Ekartc2c5";
        public String portDBORCMA = "3306";

        public String databaseDBORCBA = "hisorc_ba";        // orc backoffice
        public String hostDBORCBA = "172.25.1.153";
        public String userDBORCBA = "root";
        public String passDBORCBA = "Ekartc2c5";
        public String portDBORCBA = "3306";

        public String databaseDBKFCPO = "bithis";        // orc BIT
        public String hostDBKFCPO = "172.25.1.153";
        public String userDBKFCPO = "root";
        public String passDBKFCPO = "Ekartc2c5";
        public String portDBKFCPO = "3306";
    }
}
