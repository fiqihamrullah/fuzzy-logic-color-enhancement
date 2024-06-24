using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class ModifiedPSO
    {
        private int numParticles;
        private int maxEpochs;
        private double exitError = 0.060;
        private double probDeath = 0.005;

        private double[] parameters;

        private float targetVSF;

        private ColorEnhancer clrEnhancer;


        public ModifiedPSO(ColorEnhancer clrEnhancer,int numParticles,int maxEpochs)
        {
            this.numParticles = numParticles;
            this.maxEpochs = maxEpochs;
            this.clrEnhancer = clrEnhancer;
        }

        public void setVisualSF(float targetVSF)
        {
            this.targetVSF = targetVSF;
        }

        private void SetParameters(double[] parameters)
        {
            this.parameters[0] = parameters[0];//t 
            this.parameters[1] = parameters[1];//q
            this.parameters[2] = parameters[2];//pivot
            this.parameters[3] = parameters[3];//Fuzzifier (fh)
        }
 

        private double CountObjectiveMeasurement(double[] positions)
        {
            double J = 0.0;

            System.Diagnostics.Trace.WriteLine("T Uji: " + positions[0].ToString());
            System.Diagnostics.Trace.WriteLine("G Uji: " + positions[1].ToString());
            System.Diagnostics.Trace.WriteLine("Pivot Uji: " + positions[2].ToString());
            System.Diagnostics.Trace.WriteLine("FH Uji: " + positions[3].ToString());

            clrEnhancer.reCountThreshold(positions[2]/255.0f);
            clrEnhancer.ClassifyHistogramRegion();
            //clrEnhancer.runFuzzification((float)positions[3]);

            float vf = clrEnhancer.MeasureVisualFactor((float)positions[0], (float) positions[1]);
            System.Diagnostics.Trace.WriteLine("Visual Factor(Uji) " + vf.ToString());
            float desiredvf = targetVSF;
            System.Diagnostics.Trace.WriteLine("Desired Visual Factor(Uji) " + desiredvf.ToString());
            float entropy = clrEnhancer.CountShannonEntropy();
            float error_visual = Math.Abs(vf - desiredvf);
            double finalentropy = entropy + Math.Exp(error_visual);
            System.Diagnostics.Trace.WriteLine("Objective Function Result " + finalentropy.ToString());
            return finalentropy;
        }

        private static void Shuffle(int[] sequence, Random rnd)
        {
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }


        public double[] OptimiseParameters()
        {
            
            int epoch = 0;
            int numParams = 4;
            double minX = 2;
            double[] maxX = new double[] {10,10,255,255 };
            double w = 0.729; 
            double c1 = 1.49445;  
            double c2 = 1.49445; 
            double r1, r2;

            parameters = new double[numParams];

            Particle[] swarm = new Particle[numParticles];
           
            double[] bestGlobalPosition = new double[numParams];
            double bestGlobalVisualAppeal = double.MaxValue;

            //double minV = -0.01 * maxX;  // velocities
            //double maxV = 0.01 * maxX;

            // swarm initialization

            Random rnd = new Random();
           
            for (int i = 0; i < swarm.Length; ++i)
            {
                double[] randomPosition = new double[numParams];
                for (int j = 0; j < randomPosition.Length; ++j)
                {
                     
                    randomPosition[j] = ((maxX[j] - minX) * rnd.NextDouble()) + minX;
                    //System.Diagnostics.Trace.WriteLine("Coba " + randomPosition[j].ToString());
                }


                double visualappeal = CountObjectiveMeasurement(randomPosition); 
                double[] randomVelocity = new double[numParams];

                for (int j = 0; j < randomVelocity.Length; ++j)
                {
                    
                    double lo = 0.1 * minX;
                    double hi = 0.1 * maxX[j];
                    randomVelocity[j] = (hi - lo) * rnd.NextDouble() + lo;

                }
                swarm[i] = new Particle(randomPosition, visualappeal, randomVelocity, randomPosition, visualappeal);  

              
                if (swarm[i].GetVisualAppeal() < bestGlobalVisualAppeal)
                {
                    bestGlobalVisualAppeal = swarm[i].GetVisualAppeal();
                    swarm[i].GetPosition().CopyTo(bestGlobalPosition, 0);
                }
            }
           

 

            int[] sequence = new int[numParticles];  
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            while (epoch < maxEpochs)
            {
                System.Diagnostics.Trace.WriteLine("Epoch -------" + epoch.ToString() + "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"); 
                if (bestGlobalVisualAppeal < exitError) break; 

                double[] newVelocity = new double[numParams];  
                double[] newPosition = new double[numParams]; 
                double newVisualAppeal; // step 3

                Shuffle(sequence, rnd); 

                for (int pi = 0; pi < swarm.Length; ++pi)  
                {
                    int i = sequence[pi];
                    Particle currP = swarm[i];  

               
                    for (int j = 0; j < currP.GetVelocityLength(); ++j)  
                    {
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                       
                        newVelocity[j] = (w * currP.GetVelocity()[j]) +
                          (c1 * r1 * (currP.GetBestPosition()[j] - currP.GetPosition()[j])) +
                          (c2 * r2 * (bestGlobalPosition[j] - currP.GetPosition()[j]));
                    }

                    newVelocity.CopyTo(currP.GetVelocity(), 0);

                   
                    for (int j = 0; j < currP.GetPosition().Length; ++j)
                    {
                        newPosition[j] = currP.GetPosition()[j] + newVelocity[j];  
                        if (newPosition[j] < minX)  
                            newPosition[j] = minX;
                        else if (newPosition[j] > maxX[j])
                            newPosition[j] = maxX[j];
                    }

                    newPosition.CopyTo(currP.GetPosition(), 0);


                    newVisualAppeal = CountObjectiveMeasurement(newPosition);   
                    currP.SetVisualAppeal(newVisualAppeal);

                    if (newVisualAppeal < currP.GetBestVisualAppeal()) // new particle best?
                    {
                        newPosition.CopyTo(currP.GetBestPosition(), 0);
                        currP.SetBestVisualAppeal(newVisualAppeal);
                    }

                    if (newVisualAppeal < bestGlobalVisualAppeal) // new global best?
                    {
                        newPosition.CopyTo(bestGlobalPosition, 0);
                        bestGlobalVisualAppeal = newVisualAppeal;
                    }

                    
                    double die = rnd.NextDouble();
                    if (die < probDeath)
                    {

                        for (int j = 0; j < currP.GetPositionLength(); ++j)
                            currP.SetPosition(j, (maxX[j] - minX) * rnd.NextDouble() + minX);
                        currP.SetVisualAppeal(CountObjectiveMeasurement(currP.GetPosition())); 
                        currP.GetPosition().CopyTo(currP.GetBestPosition(), 0);
                        currP.SetBestVisualAppeal(currP.GetVisualAppeal());

                        if (currP.GetVisualAppeal() < bestGlobalVisualAppeal) 
                        {
                            bestGlobalVisualAppeal = currP.GetVisualAppeal();
                            currP.GetPosition().CopyTo(bestGlobalPosition, 0);
                        }
                    }

                }   

                ++epoch;

            } // while

            this.SetParameters(bestGlobalPosition);   

            double[] retResult = new double[numParams];
            Array.Copy(bestGlobalPosition, retResult, retResult.Length);
            return retResult;
        }
    }
}
