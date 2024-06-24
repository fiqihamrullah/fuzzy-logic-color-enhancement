using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace colorenhancementfuzzylogicpso
{
    class HSV
    {
        private float hue, saturation, value;

        public HSV()
        {

        }

        public HSV(float hue,float saturation,float value)
        {
            this.hue = hue;this.saturation = saturation;this.value = value;
        }

        public void SetHue(float hue)
        {
            this.hue = hue;
        }

        public void SetSaturation(float saturation)
        {
            this.saturation = saturation;
        }

        public void SetValue(float value)
        {
            this.value = value;
        }

        public float GetHue()
        {
            return hue;
        }

        public float GetSaturation()
        {
            return saturation;
        }

        public float GetValue()
        {
            return value;
        }
    }
}
