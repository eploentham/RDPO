using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RDPO
{
    class RDPOToERP:Form
    {
        int gapLine = 5;
        int grd0 = 0, grd1 = 100, grd2 = 240, grd3 = 320, grd4 = 570, grd5 = 650, grd51 = 700, grd6 = 820, grd7 = 900, grd8 = 1070, grd9 = 1200;
        int line1 = 30, line2 = 57, line3 = 85, line4 = 105, line41 = 120, line5 = 270, ControlHeight = 21, lineGap = 5;
        int formwidth = 820, formheight=600;

        MaterialLabel lb1;
        MaterialSingleLineTextField txtFileName;
        MaterialFlatButton btnRead;
        MaterialListView lv1;
       

        Color cTxtL, cTxtE, cForm;

        ControlRDPO cRDPO;

        String[] filePO;
        public RDPOToERP(ControlRDPO crdpo)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(formwidth, formheight);
            this.StartPosition = FormStartPosition.CenterScreen;
            cRDPO = crdpo;
            
            initConfig();
            cTxtL = txtFileName.BackColor;
            cTxtE = Color.Yellow;
        }
        private void initConfig()
        {
            initCompoment();
            txtFileName.Text = cRDPO.initC.PathInitial + "PR03102017.txt";
            cRDPO.CreateIfMissing(cRDPO.initC.PathArchive);
            cRDPO.CreateIfMissing(cRDPO.initC.PathError);
            cRDPO.CreateIfMissing(cRDPO.initC.PathInitial);
            cRDPO.CreateIfMissing(cRDPO.initC.PathProcess);

            lv1.Columns.Add("List File", formwidth - 40 - 100, HorizontalAlignment.Left);
            lv1.Columns.Add("   process   ", 100, HorizontalAlignment.Center);
            //lv1.Columns.Add(" Azimuth ", 100, HorizontalAlignment.Center);

            filePO = cRDPO.getFileinFolder(cRDPO.initC.PathInitial);
            foreach(string aa in filePO)
            {
                lv1.Items.Add(aa);
            }
            
        }
        private void initCompoment()
        {
            line1 = 30 + gapLine;
            line2 = 57 + gapLine;
            line3 = 85 + gapLine;
            line4 = 105 + gapLine;
            line41 = 120 + gapLine;
            line5 = 270 + gapLine;

            lb1 = new MaterialLabel();
            lb1.Font = cRDPO.fV1;
            lb1.Text = "Text File";
            lb1.AutoSize = true;
            Controls.Add(lb1);
            lb1.Location = new System.Drawing.Point(cRDPO.formFirstLineX, cRDPO.formFirstLineY + gapLine);

            txtFileName = new MaterialSingleLineTextField();
            txtFileName.Font = cRDPO.fV1;
            txtFileName.Text = "";
            txtFileName.Size = new System.Drawing.Size(800 - grd1-20-30, ControlHeight);
            Controls.Add(txtFileName);
            txtFileName.Location = new System.Drawing.Point(grd1, cRDPO.formFirstLineY + gapLine);
            txtFileName.Hint = lb1.Text;
            txtFileName.Enter += txtFileName_Enter; ;
            txtFileName.Leave += txtFileName_Leave;


            btnRead = new MaterialFlatButton();
            btnRead.Font = cRDPO.fV1;
            btnRead.Text = "...";
            btnRead.Size = new System.Drawing.Size(30, ControlHeight);
            Controls.Add(btnRead);
            btnRead.Location = new System.Drawing.Point(grd1 + txtFileName.Width + 5, cRDPO.formFirstLineY + gapLine);
            btnRead.Click += btnRead_Click;

            lv1 = new MaterialListView();
            lv1.Font =cRDPO.fV1;
            lv1.FullRowSelect = true;
            lv1.Size = new System.Drawing.Size(formwidth-40, formheight- line3-80);
            lv1.Location = new System.Drawing.Point(cRDPO.formFirstLineX+5, line3);
            lv1.FullRowSelect = true;
            lv1.View = View.Details;
            //lv1.Dock = System.Windows.Forms.DockStyle.Fill;
            lv1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            Controls.Add(lv1);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            // move file
            cRDPO.processRDPO(filePO);
            
        }

        private void txtFileName_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtL;
        }

        private void txtFileName_Enter(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            txtFileName.BackColor = cTxtE;
        }
    }
}
