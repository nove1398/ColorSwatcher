using ColorSwatch.Domain.Paging;
using ColorSwatch.Shared.Dto;
using ColorSwatch.Shared.Helpers;
using ColorSwatch.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using SwtchApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwtchApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet("seed")]
        public async Task<IActionResult> Seeder()
        {
            //seed data
            List<string> colors = new();
            for (int i = 0; i < 200; i++)
            {
                colors.Add(ColorHelper.GenerateRandomColor().ToHex());
            }
            var success = await _colorService.SeedColors(colors);
            return Ok(success);
        }

        [HttpGet("{hex}")]
        public async Task<IActionResult> GetColor(string hex)
        {
            var color = await _colorService.GetColor(hex);
            if (color == null)
                return NotFound("Requested color not found");

            var response = new ApiResponse<ColorResponse>(color.ToDto());
            return Ok(response);
        }

        [HttpGet("name/{search}")]
        public async Task<IActionResult> SearchColorByHex(string search)
        {
            var color = await _colorService.SearchForHex(search);
            if (color == null)
                return NotFound("Requested color not found");

            var response = new ApiResponse<ColorResponse>(color.ToDto());
            return Ok(response);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetColors([FromQuery] PagingOptions paging)
        {
            var totalColors = await _colorService.GetTotalColors();
            var colors = await _colorService.GetColors(paging);
            var response = new ApiResponse<List<ColorResponse>>(colors.ToList().ToDto())
            {
                TotalItems = totalColors,
                PageNumber = paging.PageNumber,
                PerPage = paging.PerPage,
                TotalPages = string.IsNullOrEmpty(paging.SearchTerm) ?
                                (int)Math.Floor((decimal)totalColors / paging.PerPage) : 1
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAColor(string id, [FromBody] UpdateColorRequest request)
        {
            if (!id.Equals(request.Id))
            {
                return BadRequest("Invalid object submitted for update");
            }
            var data = await _colorService.UpdateColor(id, request);
            if (data == null)
            {
                return NotFound("Requested color not found");
            }

            var response = new ApiResponse<ColorResponse>(data.ToDto());

            return Ok(response);
        }
    }
}