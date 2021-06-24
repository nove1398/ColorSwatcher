using ColorSwatch.Domain;
using ColorSwatch.Shared.Dto;
using ColorSwatch.Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ColorSwatch.Shared.Helpers
{
    public static class ColorHelper
    {
        public static string ToHex(this RGBColor source)
        {
            return source.Red.ToString("X2") + source.Green.ToString("X2") + source.Blue.ToString("X2");
        }

        public static List<string> ToHex(this List<RGBColor> source)
        {
            return source.Select(color => color.ToHex()).ToList();
        }

        public static ColorResponse ToDto(this ColorSwatcher source)
        {
            return new ColorResponse
            {
                Id = source.Id,
                CreatedAt = source.CreatedAt,
                ColorValue = source.HexColor
            };
        }

        public static List<ColorResponse> ToDto(this List<ColorSwatcher> source)
        {
            return source.Select(x => x.ToDto()).ToList();
        }

        public static RGBColor FromHexToRGB(string hexString)
        {
            if (hexString.StartsWith("#") && (hexString.Length == 7))
            {
                var r = int.Parse(hexString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                var g = int.Parse(hexString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                var b = int.Parse(hexString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
                return new RGBColor { Red = r, Green = g, Blue = b };
            }
            throw new Exception($"Cannot convert '{hexString}' to RGB");
        }

        public static RGBColor GenerateRandomColor()
        {
            Random rnd = new();
            var r = rnd.Next(1, 256);
            var g = rnd.Next(1, 256);
            var b = rnd.Next(1, 256);
            return new RGBColor { Red = r, Green = g, Blue = b };
        }

        public static RGBColor GenerateOrderedColors()
        {
            Random rnd = new();
            var r = rnd.Next(1, 256);
            var g = rnd.Next(1, 256);
            var b = rnd.Next(1, 256);
            return new RGBColor { Red = r, Green = g, Blue = b };
        }
    }
}