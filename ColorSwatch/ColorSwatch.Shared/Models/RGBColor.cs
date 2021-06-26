using System;

namespace ColorSwatch.Shared.Models
{
    public class RGBColor
    {
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }

        public RGBColor ToSeededTint(float seed)
        {
            return new RGBColor
            {
                Red = (int)(Red * seed),
                Green = (int)(Green * seed),
                Blue = (int)(Blue * seed)
            };
        }
    }
}