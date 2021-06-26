using ColorSwatch.Domain;
using ColorSwatch.Domain.Paging;
using ColorSwatch.Shared.Dto;
using ColorSwatch.Shared.Models;
using ColorSwatch.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using SwtchApi.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SwtchApi.Services
{
    public interface IColorService
    {
        Task<bool> SeedColors(List<string> data);

        Task<ColorSwatcher> GetColor(string id);

        Task<IEnumerable<ColorSwatcher>> GetColors(PagingOptions pagingOptions);

        Task<int> GetTotalColors();

        Task<ColorSwatcher> UpdateColor(string id, UpdateColorRequest request);

        Task<ColorSwatcher> SearchForHex(string request);

        Task<ColorSwatcher> GrabRandomColor();
    }

    public class ColorService : IColorService
    {
        private readonly SwatchDbContext _dbContext;

        public ColorService(SwatchDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ColorSwatcher> GetColor(string id)
        => await _dbContext.Colors.FirstOrDefaultAsync(x => x.HexColor.Equals(id));

        public async Task<IEnumerable<ColorSwatcher>> GetColors(PagingOptions pagingOptions)
        {
            var query = _dbContext.Colors.AsNoTracking();

            if (!string.IsNullOrEmpty(pagingOptions.SearchTerm))
            {
                query = query.Where(x => x.HexColor == pagingOptions.SearchTerm);
            }

            var results = await query.Skip((pagingOptions.PageNumber - 1) * pagingOptions.PerPage)
              .Take(pagingOptions.PerPage)
              .OrderBy(x => x.HexColor)
              .ToListAsync();
            return results;
        }

        public async Task<int> GetTotalColors() => await _dbContext.Colors.AsNoTracking().CountAsync();

        public async Task<ColorSwatcher> GrabRandomColor()
        {
            var total = await GetTotalColors();
            var fullList = await _dbContext.Colors.AsNoTracking()
                                .ToListAsync();
            if (fullList == null || fullList.Count < 1)
            {
                return null;
            }

            var random = fullList[new Random().Next(1, total + 1)];

            return random;
        }

        public Task<ColorSwatcher> SearchForHex(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return null;
            }

            var color = Color.FromName(searchTerm);
            var rgbColor = new RGBColor { Red = color.R, Green = color.G, Blue = color.B };
            return Task.FromResult(new ColorSwatcher() { HexColor = rgbColor.ToHex() });
        }

        public async Task<bool> SeedColors(List<string> data)
        {
            if (data != null)
            {
                if (_dbContext.Colors.Any())
                {
                    var current = await _dbContext.Colors.ToListAsync();
                    _dbContext.Colors.RemoveRange(current);
                }
                _dbContext.Colors.AddRange(data.Select(c => new ColorSwatcher
                {
                    HexColor = c,
                    CreatedAt = DateTime.UtcNow
                }));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ColorSwatcher> UpdateColor(string id, UpdateColorRequest request)
        {
            var existingColor = await GetColor(id);
            if (existingColor == null)
            {
                return null;
            }

            existingColor.HexColor = request.NewColor;
            existingColor.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return default;
        }
    }
}