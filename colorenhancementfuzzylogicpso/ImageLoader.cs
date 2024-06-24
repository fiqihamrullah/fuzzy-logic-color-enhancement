using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace colorenhancementfuzzylogicpso
{
    class ImageLoader
    {
        private MyImage img;

        public ImageLoader()
        {


        }

        public void LoadImageFromFile(String filepath)
        {
            Bitmap bmp = (Bitmap) Image.FromFile(filepath);
            int width = bmp.Width;
            int height = bmp.Height;
            img = new MyImage(width, height);
            for(int row=0;row<height;row++)
            {
                for(int col=0;col<width;col++)
                {
                    Color c = bmp.GetPixel(col, row);
                    int pixel = c.ToArgb();
                    img.SetPixelOriginal(row, col, pixel);

                }
            }


        }

        public MyImage GetImage()
        {
            return img;
        }


    }
}
