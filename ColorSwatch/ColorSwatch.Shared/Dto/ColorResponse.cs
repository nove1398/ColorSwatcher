using System;

namespace ColorSwatch.Shared.Dto
{
    public class ColorResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ColorValue { get; set; }
    }
}