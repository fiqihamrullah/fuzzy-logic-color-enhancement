using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class MyImage
    {
        private int width;
        private int height;

        private int[][] originalpixels;
        private int[][] modifiedpixels;

        public MyImage(int width,int height)
        {
            this.width = width;
            this.height = height;

            this.originalpixels = new int[height][];
            this.modifiedpixels = new int[height][];

            for(int row=0;row<height;row++)
            {
                this.originalpixels[row] = new int[width];
                this.modifiedpixels[row] = new int[width];
            }

        }

        public int GetWidth()
        {
            return this.width;
        }

        public int GetHeight()
        {
            return this.height;
        }


        public void SetPixelOriginal(int row,int col,int origpixel)
        {
            this.originalpixels[row][col] = origpixel;

        }

        public int GetPixelOriginal(int row,int col)
        {
            return this.originalpixels[row][col];
        }

        public void SetNewPixel(int row,int col,int newpixel)
        {
            this.modifiedpixels[row][col] = newpixel;
        }

        public int GetNewPixel(int row,int col)
        {
            return this.modifiedpixels[row][col];
        }
 

    }
}
