using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class Histogram
    {
        private int[] bins;
        private int[] binsclass;

        private float[] p_bins;
     
 

        public Histogram(int lengthbin )
        {
            bins = new int[lengthbin];
            binsclass = new int[lengthbin];

            p_bins = new float[lengthbin];


        }

        public void Count(int i)
        {
            bins[i]++;
        }

        public float GetValue(int idxbin)
        {
            return p_bins[idxbin];
        }

        public void SetOverExposed(int idxbin)
        {
            binsclass[idxbin] = 1;
        }

        public bool IsOverExposed(int idxbin)
        {
            return (binsclass[idxbin] == 1);
        }


        public void SetUnderExposed(int idxbin)
        {
            binsclass[idxbin] = -1;
        }

        public bool IsUnderExposed(int idxbin)
        {
            return (binsclass[idxbin] == -1);
        }

        public void SetMixedExposed(int idxbin)
        {
            binsclass[idxbin] = 0;
        }

        public bool IsMixedExposed(int idxbin)
        {
            return (binsclass[idxbin] == 0);
        }



        public double CountExposure()
        {
            double sum = 0.0;
            double sumb = 0.0;
            for (int i = 0; i < ColorEnhancer.L; i++)
            {
                sum += p_bins[i] * i;
                sumb +=p_bins[i];
            }
            double exposure = sum / sumb;
            return exposure / ColorEnhancer.L;
        }

        public void CountHistProbability(int w,int h)
        {
            for (int i = 0; i < ColorEnhancer.L; i++)
            {
                p_bins[i] = (float) bins[i] / (w * h);
                //System.Diagnostics.Trace.WriteLine(p_bins[i].ToString());
            }


        }

        public float GetMin()
        {
            float temp = float.MaxValue;

            for (int i = 0; i < ColorEnhancer.L; i++)
            {
                if (temp > bins[i])
                {
                    temp = bins[i];
                }
            }

            return temp;

        }

        

 
    }
}
