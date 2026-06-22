using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultyLevelsController : ControllerBase
    {
        private readonly IDifficultyLevelService _difficultyLevelService;

        public DifficultyLevelsController(IDifficultyLevelService difficultyLevelService)
        {
            _difficultyLevelService = difficultyLevelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DifficultyLevelDto>>> GetAllDifficultyLevels()
        {
            var levels = await _difficultyLevelService.GetAllAsync();
            return Ok(levels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DifficultyLevelDto>> GetDifficultyLevel(int id)
        {
            var level = await _difficultyLevelService.GetByIdAsync(id);
            return Ok(level);
        }

        [HttpPost]
        public async Task<ActionResult<DifficultyLevelDto>> CreateDifficultyLevel([FromBody] DifficultyLevelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _difficultyLevelService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetDifficultyLevel), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DifficultyLevelDto>> UpdateDifficultyLevel(int id, [FromBody] DifficultyLevelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _difficultyLevelService.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDifficultyLevel(int id)
        {
            await _difficultyLevelService.DeleteAsync(id);
            return NoContent();
        }
    }
}
