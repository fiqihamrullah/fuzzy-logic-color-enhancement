using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace colorenhancementfuzzylogicpso
{
    class ColorConverter
    {
        public HSV ConvertRGBPixeltoHSV(int pixel)
        {
            HSV hsv = new HSV();

            Color c = Color.FromArgb(pixel);
         

            int r = c.R;
            int g = c.G;
            int b = c.B;

            float hue, saturation, value,delta;
            float R1, G1, B1;
            R1 =  r / 255.0f;
            G1 =  g / 255.0f;
            B1 =  b / 255.0f;

            float min, max;
            

            min =(float) Math.Min(Math.Min(R1, G1), B1);
            max =   (float) Math.Max(Math.Max(R1, G1), B1);
           
            value = max;

            if (value == 0)
            {
                saturation = 0;
                hue = 0;
                return new HSV(0.0f, 0.0f, 0.0f);
            }


            delta = (max - min);

            if (delta == 0)
            {
                saturation = 0;
                hue = 0;
                return new HSV(hue , saturation, value);
            }
            
          

            if (R1 == max)
                hue = (float) (60 * ((((G1 - B1)/ delta))%6));
            else if (G1 == max)
                hue = (float)(60 * ((((B1 - R1) / delta)) +2));
            else
                hue = (float)(60 * ((((R1 - G1) / delta)) + 4));

            saturation = delta / max;

           		
            if (hue < 0)
                hue += 360;
           
            hsv.SetHue(hue);
            hsv.SetSaturation(saturation);
            hsv.SetValue(value);
            return hsv;

        }



        public   Color HSVToRGBPixel(float h, float s, float v)
        {
            int hi = (int)Math.Floor(h / 60.0) % 6;
            double f = (h / 60.0) - Math.Floor(h / 60.0);

            double p = v * (1.0 - s);
            double q = v * (1.0 - (f * s));
            double t = v * (1.0 - ((1.0 - f) * s));

           
           /* System.Diagnostics.Trace.WriteLine("hi:" + (hi).ToString());
            System.Diagnostics.Trace.WriteLine("P:" + ((byte)(p*255.0)).ToString());
            System.Diagnostics.Trace.WriteLine("Q:" + (q*255.0).ToString());
            System.Diagnostics.Trace.WriteLine("T:" + (t*255.0).ToString());
            System.Diagnostics.Trace.WriteLine("V:" + (v * 255.0).ToString()); */

            Color ret;

            switch (hi)
            {
                case 0:
                    ret =  GetRgb(v, t, p);
                    break;
                case 1:
                    ret =  GetRgb(q, v, p);
                    break;
                case 2:
                    ret =  GetRgb(p, v, t);
                    break;
                case 3:
                    ret =  GetRgb(p, q, v);
                    break;
                case 4:
                    ret =  GetRgb(t, p, v);
                    break;
                case 5:
                    ret =  GetRgb(v, p, q);
                    break;
                default:
                    ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    break;
            }
            return ret;
        }
        public static Color GetRgb(double r, double g, double b)
        {
            byte rr = (byte)(r * 255.0);
            double rrx = (r * 255.0) - rr;
            if (rrx>=0.5)
            {
                rr++;
            }

            byte gg = (byte)(g * 255.0);
            double ggx= (g * 255.0) - gg;
            if (ggx >= 0.5)
            {
                gg++;
            }

            byte bb = (byte)(b * 255.0);
            double bbx = (b * 255.0) - bb;
            if (bbx >= 0.5)
            {
                bb++;
            }

            return Color.FromArgb(255,rr , gg, bb);
        }



 


        public HSV[][] CreateHSVColorSpaceFromImage(MyImage img)
        {
            int height = img.GetHeight();int width = img.GetWidth();
            HSV[][] hsvtemp = new HSV[height][];
            for(int row=0;row<height;row++)
            {
                hsvtemp[row] = new HSV[width];

            }

            for(int row=0;row<height;row++)
            {
                for(int col=0;col<width;col++)
                {
                    int pixel = img.GetPixelOriginal(row, col);
                    HSV hsv = ConvertRGBPixeltoHSV(pixel);
                    hsvtemp[row][col] = hsv;
                }
            }

            return hsvtemp;
        }

        public MyImage FromHSVToImage(HSV[][]  hsvmatrixs)
        {
            MyImage imgnew = new MyImage(hsvmatrixs[0].Length, hsvmatrixs.Length);
          //  System.Diagnostics.Trace.WriteLine("Panjang Pertamo : " + hsvmatrixs[0][221].GetHue().ToString());


            for (int row = 0; row < hsvmatrixs.Length; row++)
            {
                for (int col = 0; col < hsvmatrixs[row].Length; col++)
                {
                    int rgb =  HSVToRGBPixel(hsvmatrixs[row][col].GetHue(), hsvmatrixs[row][col].GetSaturation(), hsvmatrixs[row][col].GetValue()).ToArgb() ;
                    imgnew.SetPixelOriginal(row, col, rgb); 

                }
            }

            return imgnew;
        }

    }
}
