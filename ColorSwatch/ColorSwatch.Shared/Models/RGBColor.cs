namespace ColorSwatch.Shared.Models
{
    public class RGBColor
    {
        public int Red { get; set; }
        public int Blue { get; set; }
        public int Green { get; set; }

        public RGBColor ToTinted()
        {
            return new RGBColor
            {
                Red = (int)(Red * 0.25),
                Green = (int)(Green * 0.25),
                Blue = (int)(Blue * 0.25)
            };
        }

        public RGBColor ToSeededTint(double seed)
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