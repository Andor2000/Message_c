using Message.API.Data;
using Message.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Message.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemesController : Controller
    {
        private readonly MessageDbContext themeDbContext;

        public ThemesController(MessageDbContext themeDbContext)
        {
            this.themeDbContext = themeDbContext;
        }
        // get all themes
        [HttpGet]
        public async Task<IActionResult> GetAllThemes()
        {
            var themes = await themeDbContext.Themes.ToListAsync();
            return Ok(themes);
        }

        // get theme
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetTheme")]
        public async Task<IActionResult> GetTheme([FromRoute] int id)
        {
            var theme = await themeDbContext.Themes.FirstOrDefaultAsync(x => x.Id == id);
            if (theme != null)
            {
                return Ok(theme);
            }
            return NotFound("Theme not found");
        }

        // add theme
        [HttpPost]
        public async Task<IActionResult> AddTheme([FromBody] Themes theme)
        {
            await themeDbContext.Themes.AddAsync(theme);
            await themeDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTheme), new { id = theme.Id }, theme);
        }
    }
}
