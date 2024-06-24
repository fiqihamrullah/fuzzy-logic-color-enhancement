using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class Particle
    {
        private double[] position;
        private double visualappeal;
        private double[] velocity;
        private double[] bestPosition;
        private double bestVisualAppeal;

        public Particle()
        {

        }

        public Particle(double[] position, double visualappeal, double[] velocity,
           double[] bestPosition, double bestVisualAppeal)
        {
            this.position = new double[position.Length];
         
            Array.Copy(position, 0, this.position, 0, position.Length );
            this.visualappeal = visualappeal;
            this.velocity = new double[velocity.Length ];
            Array.Copy(velocity, 0, this.velocity, 0, position.Length );
            this.bestPosition = new double[bestPosition.Length];
            Array.Copy(bestPosition, 0, this.bestPosition, 0, position.Length);
            this.bestVisualAppeal = bestVisualAppeal;

            //this.age = 0;
        }


        public void SetPosition(int idx, double val)
        {
            this.position[idx] = val;
        }

        public double GetPosition(int idx)
        {
            return position[idx];
        }

        public void SetVisualAppeal(double visualappeal)
        {
            this.visualappeal = visualappeal;
        }

        public double GetBestVisualAppeal()
        {
            return bestVisualAppeal;
        }

        public void SetBestVisualAppeal(double bestVisualAppeal)
        {
            this.bestVisualAppeal = bestVisualAppeal;
        }



        public double GetVisualAppeal()
        {
            return visualappeal;
        }

        public double[] GetBestPosition()
        {
            return bestPosition;
        }

        public double GetBestPosition(int idx)
        {
            return bestPosition[idx];
        }

        public void SetVelocity(int idx, double newvelocity)
        {
            this.velocity[idx] = newvelocity;
        }

        public double GetVelocity(int idx)
        {
            return velocity[idx];
        }

        public double[] GetVelocity()
        {
            return velocity;
        }

        public double[] GetPosition()
        {
            return position;
        }



        public int GetVelocityLength()
        {
            return velocity.Length;
        }

        public int GetPositionLength()
        {
            return position.Length;
        }
    }
}
