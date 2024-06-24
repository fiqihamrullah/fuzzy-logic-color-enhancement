using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace colorenhancementfuzzylogicpso
{
    public partial class Form1 : Form
    {
        private MyImage myimg;
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Dispose();
        }

        private DialogResult openFileImageDialog()
        {
            return openFileDialog1.ShowDialog();
        }

        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileImageDialog()==DialogResult.OK )
            {
                ImageLoader imgloader = new ImageLoader();
                imgloader.LoadImageFromFile(openFileDialog1.FileName);
                myimg = imgloader.GetImage();

                ImageViewer imgviewer = new ImageViewer();
                imgviewer.SetImage(myimg);
                imgviewer.SetViewer(pbsebelumnya);
                imgviewer.ViewOriginalImage();   
            }
        }

        private void enhanceColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorEnhancer colorenhancer = new ColorEnhancer(myimg);
            colorenhancer.Enhance();

            MyImage newimg = colorenhancer.GetRefinedImage(); 
            ImageViewer imgviewer = new ImageViewer();
            imgviewer.SetImage(newimg);
            imgviewer.SetViewer(pbsesudah);
            imgviewer.ViewOriginalImage();

            MessageBox.Show("Citra Berhasil diperbaiki!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        

        private void saveResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            float fh = 144f;
            double hasil = MembershipFunction.UnderExposureGaussianFunc(6, 255,  fh);
            MessageBox.Show(hasil.ToString());
            double ihasil = MembershipFunction.InverseUnderExposureGaussianFunc(hasil, 255,  fh);
            MessageBox.Show(ihasil.ToString());
        }
    }
}
