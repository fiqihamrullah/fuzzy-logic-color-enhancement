using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class ColorEnhancer
    {
        private MyImage myimg,myrefinedimage;
        private Histogram histogram;

        private float lowerT;
        private float upperT;

        private float initlowerT;
        private float initupperT;

        public float initpivot; 

        public static int L = 256;

        private static float md_cu = 0.4f;
        private static float md_co = 0.5f;

        private double[] md_u, md_o;
        private double[] imd_u, imd_o;

        float qui, qoi;

        private int maxlevelgray;
        private float avggray;


        public ColorEnhancer(MyImage myimg)
        {
            this.myimg = myimg;
            md_u = new double[L];
            md_o = new double[L];

            imd_u = new double[L];
            imd_o = new double[L];


        }

        private  void InitThreshold(double POROS)
        {
            float u = 0.1f;
            float l = 0.1f;
            upperT = (float) (L * (POROS - u));
            lowerT  = (float)( L * (POROS + l));
            initlowerT = lowerT;
            initupperT = upperT;
        }

        public void reCountThreshold(double POROS)
        {
            float u = 0.1f;
            float l = 0.1f;
            upperT = (float)(L * (POROS - u));
            lowerT = (float)(L * (POROS + l));
           
        }

        private float CountFuzzifier()
        {
            float fh = 0.0f;
            double sum = 0.0;
            double sumb = 0.0;            
            float vmax = getMax();

            for (int i = 0; i < ColorEnhancer.L; i++)
            {
                //System.Diagnostics.Trace.WriteLine(histogram.GetValue(i).ToString());
                sum += Math.Pow((vmax - i ), 4) * histogram.GetValue(i);
                sumb += Math.Pow((vmax - i), 2) * histogram.GetValue(i);
            }

            fh = (float)Math.Sqrt((sum / sumb) * 0.5);
            //  fh = (float)((sum / sumb) * 0.5);
            return fh;
        }

        public void ClassifyHistogramRegion()
        {
            for(int v=0;v<L;v++)
            {
                if (v < upperT)
                {
                    histogram.SetUnderExposed(v);
                }else if (v >= lowerT   && v < L )
                {
                    histogram.SetOverExposed(v);
                }else
                {
                    histogram.SetMixedExposed(v);
                }
            }


            for (int v = 0; v < L; v++)
            {
                if (histogram.IsUnderExposed(v))
                {
                    System.Diagnostics.Trace.Write("U");
                }
                else if (histogram.IsOverExposed(v))
                {
                    System.Diagnostics.Trace.Write("O");
                }
                else if (histogram.IsMixedExposed(v))
                {
                    System.Diagnostics.Trace.Write("M");
                }
            }


        }

        private void Fuzzify(float fh)
        {
            float vmax = getMax();
            float vavg = getAverage();

            System.Diagnostics.Trace.WriteLine("\nVMax " + vmax.ToString());
            System.Diagnostics.Trace.WriteLine("VAvg " + vavg.ToString());
            System.Diagnostics.Trace.WriteLine("Fh-----> " + fh.ToString());

            double sum = 0.0;
            for (int v=0;v<L;v++)
            {
                //if (histogram.IsUnderExposed(v))
                {
                    md_u[v] = MembershipFunction.UnderExposureGaussianFunc(v, vmax,  fh);
                    // sum += md_u[v];
                    System.Diagnostics.Trace.WriteLine(md_u[v]);
                }

              //  if (histogram.IsOverExposed(v))
                {
                  //  md_o[v] = MembershipFunction.OverExposureGaussianFunc(v, vmax,  fh);
                   // System.Diagnostics.Trace.WriteLine(md_o[v]);
                 //  sum += md_o[v];
                }
               // System.Diagnostics.Trace.WriteLine("Total " + sum.ToString());

            }
        }
 

        private float scale(float max,float min,float max_val,float val)
        {
            return ((max-min) / max_val) * val;
        }

        private void Defuzzy(HSV[][] hsvmatriks,double fh)
        {
            float s_u = 3/4.0f ;
            float s_o = 4/3.0f;
            double max_ux = double.MinValue;
            double max_ox = double.MinValue;
            double max_mix = double.MinValue;

            float vmax = getMax();
            float vavg = getAverage();

            System.Diagnostics.Trace.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            for(int v=0;v<L;v++)
            {
               double newV =   MembershipFunction.InverseUnderExposureGaussianFunc(imd_u[v], vmax, fh);
               System.Diagnostics.Trace.WriteLine(newV);
            }
            System.Diagnostics.Trace.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

            for (int row = 0; row < hsvmatriks.Length; row++)
            {
                for (int col = 0; col < hsvmatriks[row].Length; col++)
                {
                    int v = (int)(hsvmatriks[row][col].GetValue() * 255);
                    float saturation= (hsvmatriks[row][col].GetSaturation());
                   // System.Diagnostics.Trace.Write("Before Saturation " + saturation.ToString() + "\t");

                    double newv = 0.0f;float newsaturation = 0.0f;
                 //   if (v < upperT)
                    {
                        newv =  MembershipFunction.InverseUnderExposureGaussianFunc(imd_u[v],vmax,fh);    
                        
                        if (max_ux < newv)
                        {
                            max_ux = newv;
                        }
                        newsaturation = (float) Math.Pow(saturation, s_u);
                    }
                   // else if (v >= lowerT  && v < L)
                    {
                      //  newv =    MembershipFunction.InverseOverExposureGaussianFunc(imd_o[v], vmax,  fh);  
                     
                        if (max_ox < newv)
                        {
                            max_ox = newv;
                        }
                        newsaturation = (float) Math.Pow(saturation, s_o);
                    }//else
                    {
                     //   newv = v ;///255.0f;
                        if (max_mix < v)
                        {
                            max_mix = v;
                        }
                        newsaturation = saturation;
                    }


                     //System.Diagnostics.Trace.WriteLine("New Value " + newv.ToString());
                     // hsvmatriks[row][col].SetSaturation(newsaturation);
                      hsvmatriks[row][col].SetValue((float)newv/255);
                }
            }
            System.Diagnostics.Trace.WriteLine("Max UX " + max_ux.ToString());

            //for (int row = 0; row < hsvmatriks.Length; row++)
            //{
            //    for (int col = 0; col < hsvmatriks[row].Length; col++)
            //    {
            //        float v = hsvmatriks[row][col].GetValue();
            //        float saturation = (hsvmatriks[row][col].GetSaturation());
            //        float newv = 0.0f; float newsaturation = 0.0f;
            //        if (v < upperT)
            //        {
            //            newv = scale(upperT - 1, 0, max_ux, v)  ;

            //            newsaturation = (float)Math.Pow(saturation, s_u);
            //        }
            //        else if (v >= lowerT && v < L)
            //        {
            //            newv = scale(L - 1, lowerT, max_ox, v)  ;

            //            newsaturation = (float)Math.Pow(saturation, s_o);
            //        }
            //        else
            //        {
            //            newv = scale(lowerT - 1, upperT, max_mix, v)   ;
            //            newsaturation = saturation;
            //        }

            //        //  System.Diagnostics.Trace.WriteLine("NewV " + newv.ToString());

            //        // hsvmatriks[row][col].SetSaturation(newsaturation);
            //        hsvmatriks[row][col].SetValue(newv);
            //    }
            //}


        }

        private void ItensifyMF(float t,float g)
        {
            System.Diagnostics.Trace.WriteLine("--------------------------------------");
            for (int v = 0; v < L; v++)
            {
               // if (histogram.IsUnderExposed(v))
                {
                    imd_u[v] =  MembershipFunction.UnderExposureIntensRegion(t, md_u[v], md_cu);
                  //   System.Diagnostics.Trace.WriteLine(imd_u[v].ToString()); 
                }

               // if (histogram.IsOverExposed(v))
                {
                  //  imd_o[v] =  MembershipFunction.OverExposureIntensRegion(g, md_o[v], md_co);
                   //  System.Diagnostics.Trace.WriteLine(imd_o[v].ToString());
                }

            }
        }


        private float CountFuzzyContrastUnderExposure(double[] md_iu,float md_cu  )
        {
            float fuzzycontrast = 0.0f;
            float sum = 0.0f;

            for(int i=0;i<upperT;i++)
            {
                sum += (float) Math.Pow(md_iu[i] - md_cu, 2);
            }

            fuzzycontrast = (1 / upperT) * sum;
            return fuzzycontrast;
        }

        private float CountAverageFuzzyContrastUnderExposure(double[] md_iu, float md_cu)
        {
            float fuzzycontrast = 0.0f;
            double sum = 0.0f;

            for (int i = 0; i < upperT; i++)
            {
                sum += (md_iu[i] - md_cu);
            }

            fuzzycontrast =(float) ((1 / upperT) * sum);
            return fuzzycontrast;
        }

        private float CountFuzzyContrastOverExposure(double[] md_io, float md_co)
        {
            float fuzzycontrast = 0.0f;
            float sum = 0.0f;

            for (int i = (int)lowerT  ; i < L ; i++)
            {
                sum += (float)Math.Pow(md_io[i] - md_co, 2);
            }

            fuzzycontrast = (1 / (L-lowerT)) * sum;
            return fuzzycontrast;
        }

        private float CountAverageFuzzyContrastOverExposure(double[] md_io, float md_co)
        {
            float fuzzycontrast = 0.0f;
            float sum = 0.0f;

            for (int i = (int)lowerT; i < L ; i++)
            {
                sum += (float)(md_io[i] - md_co);
            }

            fuzzycontrast = (1 / (L - lowerT)) * sum;
            return fuzzycontrast;
        }


        private float CountInitialFuzzyContrastUnderExposure(double[] md_u, float md_cu)
        {
            float fuzzycontrast = 0.0f;
            float sum = 0.0f;

            for (int i = 0; i < upperT ; i++)
            {
                sum += (float)Math.Pow(md_u[i] - md_cu, 2);
            }

            fuzzycontrast = (1 / upperT) * sum;
            return fuzzycontrast;
        }

        private float CountInitialAverageFuzzyContrastUnderExposure(double[] md_u, float md_cu)
        {
            float fuzzycontrast = 0.0f;
            double sum = 0.0f;

            for (int i = 0; i < upperT ; i++)
            {
                sum +=  (md_u[i] - md_cu);
            }

            fuzzycontrast =(float) ((1 / upperT) * sum);
            return fuzzycontrast;
        }

        private float CountInitialFuzzyContrastOverExposure(double[] md_o, float md_co)
        {
            float fuzzycontrast = 0.0f;
            float sum = 0.0f;


            for (int i = (int)lowerT; i < L ; i++)
            {
                sum += (float)Math.Pow(md_o[i] - md_co, 2);
            }

            fuzzycontrast = (1 / (L - lowerT)) * sum;
            return fuzzycontrast;
        }

        private float CountInitialAverageFuzzyContrastOverExposure(double[] md_o, float md_co)
        {
            float fuzzycontrast = 0.0f;
            double sum = 0.0f;

            for (int i = (int)lowerT; i < L ; i++)
            {
                sum +=  (md_o[i] - md_co);
            }

            fuzzycontrast =(float) ((1 / (L - lowerT)) * sum);
            return fuzzycontrast;
        }


        private float CountQualityFactor(float avgfuzzycontrast,float fuzzycontrast)
        {
            return Math.Abs(avgfuzzycontrast / fuzzycontrast);
        }

        private float CountInitialQualityFactor(float init_avgfuzzycontrast, float init_fuzzycontrast)
        {
            return Math.Abs(init_avgfuzzycontrast / init_fuzzycontrast);
        }

        private float CountUnderVisualFactor(float qualityfactor,float initqualityfactor)
        {
            return qualityfactor / initqualityfactor;
        }

        private float CountOverVisualFactor(float qualityfactor, float initqualityfactor)
        {
            return initqualityfactor / qualityfactor  ;
        }

        private float CountTrueVisualFactor(float visualfactorunderexposure,float visualfactoroverexposure)
        {
            return (upperT / L) * visualfactorunderexposure + (1 - (lowerT / L)) * visualfactoroverexposure;
        }

        public float CountSoothingVisualFactor(float  poros)
        {
            return 1.5f - ((0.9f / 255f) * poros);
        }


        private int getMax()
        {
            return (maxlevelgray==L-1)? maxlevelgray-1: maxlevelgray;
        }

        private float getAverage()
        {
            return avggray;
        }

        private void CountHistogram(HSV[][] hsvmatriks)
        {
            histogram = new Histogram(L);
            maxlevelgray = Int16.MinValue;
            avggray = 0.0f;
           // System.Diagnostics.Trace.WriteLine("Panjang " + hsvmatriks.Length.ToString()); 
            for (int row = 0; row < hsvmatriks.Length; row++)
            {
                for (int col = 0; col < hsvmatriks[row].Length; col++)
                {
                    int bin = (int)(hsvmatriks[row][col].GetValue() * 255);
                  //  System.Diagnostics.Trace.WriteLine(hsvmatriks[row][col].GetValue().ToString());
                    histogram.Count(bin);
                    avggray += bin;
                    if (maxlevelgray < bin)
                    {
                        maxlevelgray = bin;
                      //  System.Diagnostics.Trace.WriteLine("V:" + hsvmatriks[row][col].GetValue().ToString());
                    }

                }
            }
            avggray = avggray / (hsvmatriks.Length* hsvmatriks[0].Length);

            System.Diagnostics.Trace.WriteLine("Max Level Gray:" + maxlevelgray.ToString());

            histogram.CountHistProbability(hsvmatriks.Length, hsvmatriks[0].Length);
             


        }

        public float CountShannonEntropy()
        {
            float sum_u = 0.0f;
            float sum_o = 0.0f;

            for (int v = 0; v < upperT  ; v++)
            {
                if (histogram.IsUnderExposed(v))
                {
                    sum_u += (float)(imd_u[v] * Math.Log(imd_u[v]) + (1 - imd_u[v]) * Math.Log(1 - imd_u[v]));
                }
            }

            for (int v =(int) lowerT; v < L  ; v++)
            {
                if (histogram.IsOverExposed(v))
                {
                    sum_o += (float)(imd_o[v] * Math.Log(imd_o[v]) + (1 - imd_o[v]) * Math.Log(1 - imd_o[v]));
                }
            }

            

            float entropy = (float) (-1 / (L * Math.Log(2))) * (sum_u + sum_o);
            return entropy;
        }


        public float MeasureVisualFactor(float t,float g)
        {
            //Meningkatkan Nilai Keanggotaan Fuzzy
            ItensifyMF(t, g);

            //Menghitung Fuzzy Contrast
            float cu = CountFuzzyContrastUnderExposure(imd_u, md_cu);
            float avgcu = CountAverageFuzzyContrastUnderExposure(imd_u, md_cu);
            float co = CountFuzzyContrastOverExposure(imd_o, md_co);
            float avgco = CountAverageFuzzyContrastOverExposure(imd_o, md_co);
            //Menghitung  Quality Factor
            float qu = CountQualityFactor(avgcu, cu);
            System.Diagnostics.Trace.WriteLine("Modified Quality Factor(UnderExposed) " + qu.ToString());
            float qo = CountQualityFactor(avgco, co);
            System.Diagnostics.Trace.WriteLine("Modified Quality Factor(OverExposed) " + qo.ToString());
            //----------------------------------------------------------------
            //Menghitung Visual Factor
            //----------------------------------------------------------------
            float vu = CountUnderVisualFactor(qu, qui);
            System.Diagnostics.Trace.WriteLine("Vu " + vu.ToString());
            float vo = CountOverVisualFactor(qo, qoi);
            System.Diagnostics.Trace.WriteLine("Vo " + vo.ToString());
            float vf = CountTrueVisualFactor(vu, vo);

            return vf;
        }

        public void runFuzzification(float fh)
        {
            ClassifyHistogramRegion();
            Fuzzify(fh);
            initContrastAndQualityFactor();
        }

        private void initContrastAndQualityFactor()
        {
            //Menghitung Inisial Fuzzy Contrast
            float cui = CountInitialFuzzyContrastUnderExposure(md_u, md_cu);
            float avgcui = CountInitialAverageFuzzyContrastUnderExposure(md_u, md_cu);
            float coi = CountInitialFuzzyContrastOverExposure(md_o, md_co);
            float avgcoi = CountInitialAverageFuzzyContrastOverExposure(md_o, md_co);

            //Menghitung Inisial Quality Factor
            qui = CountInitialQualityFactor(avgcui, cui);
            System.Diagnostics.Trace.WriteLine("Initial Quality Factor(UnderExposed) " + qui.ToString());
            qoi = CountInitialQualityFactor(avgcoi, coi);
            System.Diagnostics.Trace.WriteLine("Initial Quality Factor(OverExposed) " + qoi.ToString());
        }

        public void Enhance()
        {
            ColorConverter colorconverter = new ColorConverter();
            HSV[][] hsvmatriks =  colorconverter.CreateHSVColorSpaceFromImage(myimg);
            CountHistogram(hsvmatriks);
            float fh = CountFuzzifier();
            double exposure =  histogram.CountExposure();
            initpivot =(float) (exposure*255);
            InitThreshold(exposure);

            System.Diagnostics.Trace.WriteLine("Exposure " + exposure.ToString());
           // System.Diagnostics.Trace.WriteLine("Init Pivot " + initpivot.ToString());
          //  System.Diagnostics.Trace.WriteLine("Init UT " + initupperT.ToString());
       //     System.Diagnostics.Trace.WriteLine("init LT " + initlowerT.ToString());

            runFuzzification(fh);


            Random random = new Random();

            float t = 0.0f;// (float)random.Next(10);
            float g = 0.0f;// (float)random.Next(10);
            while (t <= 1)
            {
                t = (float)random.Next(10);
            }

            // t = 4f;
            //while (g <= 1)
            //{
            //    g = (float)random.Next(10);
            //}
            //t *= 10;
            ////  g *= 10;

            System.Diagnostics.Trace.WriteLine("t " + t.ToString());
            //System.Diagnostics.Trace.WriteLine("g " + g.ToString()); 

            float vf = MeasureVisualFactor(t, g);
            float soothingvf = CountSoothingVisualFactor(initpivot);

            System.Diagnostics.Trace.WriteLine("Visual Factor " + vf.ToString());
            System.Diagnostics.Trace.WriteLine("Soothing Visual Factor " + soothingvf.ToString());

            //Jalankan Modified PSO
            /*    ModifiedPSO mPSO = new ModifiedPSO(this, 10, 100); //10 Partikel dengan 100 Iterasi
                mPSO.setVisualSF(soothingvf);
                double[] optimisedparameters = mPSO.OptimiseParameters();

                float new_t = (float)optimisedparameters[0];
                float new_g = (float)optimisedparameters[1];
                float pivot = (float)optimisedparameters[2];
                float newfh = (float)optimisedparameters[3];

                reCountThreshold(pivot / 255.0f);
                runFuzzification(newfh);
                float vf_now = MeasureVisualFactor(new_t, new_g);

            System.Diagnostics.Trace.WriteLine("-----------------------------------------------");
            System.Diagnostics.Trace.WriteLine("Hasil Optimasi PSO Parameter ");
            System.Diagnostics.Trace.WriteLine("-----------------------------------------------");
            System.Diagnostics.Trace.WriteLine("New Visual Factor :" + vf_now.ToString());
            System.Diagnostics.Trace.WriteLine("Soothing Visual Factor :" + soothingvf.ToString());
            System.Diagnostics.Trace.WriteLine("Fuzzifier Terpilih" + pivot.ToString());
            System.Diagnostics.Trace.WriteLine("T Terpilih :" + new_t.ToString());
            System.Diagnostics.Trace.WriteLine("G Terpilih :" + new_g.ToString());
            System.Diagnostics.Trace.WriteLine("Pivot Terpilih :" + pivot.ToString());*/

            //Defuzzy
            Defuzzy(hsvmatriks, fh);
            // Defuzzy(hsvmatriks, newfh); 

            myrefinedimage = colorconverter.FromHSVToImage(hsvmatriks);



        }

        public MyImage GetRefinedImage()
        {
            return myrefinedimage;
        }

    }
}
