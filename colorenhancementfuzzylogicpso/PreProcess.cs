using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace colorenhancementfuzzylogicpso
{
    class PreProcess
    {
        public static void Grayscale(MyImage img)
        {
            for (int row = 0; row < img.GetHeight(); row++)
            {
                for (int col = 0; col < img.GetWidth(); col++)
                {
                    int pixel = img.GetPixelOriginal(row, col);
                    Color cl = Color.FromArgb(pixel);
                    int r = cl.R;
                    int g = cl.G;
                    int b = cl.B;
                    int gray = (r + g + b) / 3;
                    img.SetNewPixel(row, col, gray);
                }
            }
        }
    }
}
