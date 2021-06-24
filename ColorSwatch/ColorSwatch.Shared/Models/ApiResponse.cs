using System;

namespace ColorSwatch.Shared.Models
{
    public class ApiResponse<T> where T : class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PerPage { get; set; }
        public string Error { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
            Success = true;
        }
    }
}