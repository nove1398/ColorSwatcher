using System;

namespace ColorSwatch.Shared.Dto
{
    public class UpdateColorRequest
    {
        public Guid Id { get; set; }
        public string NewColor { get; set; }
    }
}