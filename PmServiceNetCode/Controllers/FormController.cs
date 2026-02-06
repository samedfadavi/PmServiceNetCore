using Microsoft.AspNetCore.Mvc;
using PmServiceNetCode.DTOs;
using PmServiceNetCode.Services;

namespace PmServiceNetCode.Controllers
{
    [ApiController]
    [Route("api/forms")]
    public class FormsController : ControllerBase
    {
        private readonly IFormService _service;

        public FormsController(IFormService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FormDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{idForm:int}")]
        public async Task<ActionResult<FormDto>> GetById(int idForm)
        {
            var result = await _service.GetByIdAsync(idForm);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<FormDto>> Create(
    [FromBody] CreateFormDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { idForm = result.IdForm },
                result
            );
        }
        [HttpDelete("{idForm:int}")]
        public async Task<IActionResult> Delete(int idForm)
        {
            var success = await _service.DeleteAsync(idForm);

            if (!success)
                return NotFound();

            return NoContent();
        }

    }

}
