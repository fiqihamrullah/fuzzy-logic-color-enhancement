using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
 

namespace colorenhancementfuzzylogicpso
{
    class ImageViewer
    {
        private MyImage img;
        private PictureBox pbbox; 


        public ImageViewer()
        {

        }

        private Bitmap ToBitmapFromOriginal()
        {
            Bitmap bmp = new Bitmap(img.GetWidth(), img.GetHeight());

            for(int row=0;row<img.GetHeight();row++)
            {
                for(int col=0;col<img.GetWidth();col++)
                {
                    int pixel = img.GetPixelOriginal(row, col);
                    Color cl = Color.FromArgb(pixel); 
                    bmp.SetPixel(col, row, cl);
                }
            }


            return bmp;

        }


        private Bitmap ToBitmapFromModified()
        {
            Bitmap bmp = new Bitmap(img.GetWidth(), img.GetHeight());

            for (int row = 0; row < img.GetHeight(); row++)
            {
                for (int col = 0; col < img.GetWidth(); col++)
                {
                    int pixel = img.GetNewPixel (row, col);
                    Color cl = Color.FromArgb(pixel);
                    bmp.SetPixel(col, row, cl);
                }
            }


            return bmp;

        }

        public void SetImage(MyImage img)
        {
            this.img = img;
        }

        public void SetViewer(PictureBox pbox)
        {
            this.pbbox = pbox;
        }


        public void ViewOriginalImage()
        {
            this.pbbox.Image = ToBitmapFromOriginal();

        }


        public void ViewNewImage()
        {
            this.pbbox.Image = ToBitmapFromModified();
        }

    }


}
