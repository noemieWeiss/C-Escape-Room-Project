using EscapeRoom.Core.DTOs;
using EscapeRoom.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EscapeRoom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HintsController : ControllerBase
    {
        private readonly IHintService _hintService;

        public HintsController(IHintService hintService)
        {
            _hintService = hintService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HintDto>>> GetAllHints()
        {
            var hints = await _hintService.GetAllAsync();
            return Ok(hints);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HintDto>> GetHint(int id)
        {
            var hint = await _hintService.GetByIdAsync(id);
            return Ok(hint);
        }

        [HttpPost]
        public async Task<ActionResult<HintDto>> CreateHint([FromBody] HintDto hintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _hintService.CreateAsync(hintDto);
            return CreatedAtAction(nameof(GetHint), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HintDto>> UpdateHint(int id, [FromBody] HintDto hintDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _hintService.UpdateAsync(id, hintDto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHint(int id)
        {
            await _hintService.DeleteAsync(id);
            return NoContent();
        }
    }
}
