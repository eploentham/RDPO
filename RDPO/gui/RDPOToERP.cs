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

        MaterialLabel lb1;
        MaterialSingleLineTextField txtFileName;
        MaterialFlatButton btnRead;

        Color cTxtL, cTxtE, cForm;

        ControlRDPO cRDPO;
        public RDPOToERP(ControlRDPO crdpo)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(820, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            cRDPO = crdpo;
            
            initConfig();
            cTxtL = txtFileName.BackColor;
            cTxtE = Color.Yellow;
        }
        private void initConfig()
        {
            initCompoment();
            txtFileName.Text = cRDPO.initC.PathInitial + "\\01344dft.801";
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
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ReadText rd = new ReadText();
            rd.ReadTextFile(txtFileName.Text);
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
