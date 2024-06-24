using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class MembershipFunction
    {
        public static double UnderExposureGaussianFunc(float v, float vmax, float fh)
        {
            // System.Diagnostics.Trace.WriteLine("TT " + ((vmax - (vavg - v)) / (Math.Sqrt(2) * fh))).ToString());
            // return  Math.Exp(-  (Math.Pow((vmax -  v),2) / (2 * fh)));
            //  return  Math.Exp(-1 * (Math.Pow(((vmax -   v) / (Math.Sqrt(2 * fh))), 2)));
           return  Math.Exp(-1 * (Math.Pow(vmax  - v,2) / (2 * Math.Pow(fh,2))));
        }

        public static double InverseUnderExposureGaussianFunc(double y,float max, double fh)
        {
            if (y != 0)
            {

                return Math.Abs((max - Math.Sqrt(-2 * Math.Log(y) * Math.Pow(fh, 2))));
            }
            else
            {
                return 0;
            }
            // return   Math.Log(y / (1 - y));
        }

        public static double InverseOverExposureGaussianFunc(double y,float max,   double fh)
        {
            if (y != 0)
            {
                return  ColorEnhancer.L - (((max) - Math.Sqrt(-2 *  Math.Log(y)  * Math.Pow(fh,2))));
            }
            else
            {
                return 0;
            }
         
        }

        public static double OverExposureGaussianFunc(float v, float vmax,  float fh)
        {
            return  Math.Exp(-1 * (Math.Pow(vmax -  (ColorEnhancer.L - v),2) / (2 * Math.Pow(fh, 2))));
        }

        public  static double UnderExposureIntensRegion(float t,double md_u,float md_cu)
        {
            return  1.0 / (1 +  Math.Exp((-1 * t * (md_u - md_cu))));
        }

        public static double OverExposureIntensRegion(float g, double md_o, float md_co)
        {
            return 1.0  / (1 +  Math.Exp((-1 * g) * (md_o - md_co)));
        }
    }
}
