using Microsoft.AspNetCore.Mvc;
using PmServiceNetCode.DTOs;
using PmServiceNetCode.Interfaces;

namespace PmServiceNetCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarayandController : ControllerBase
    {
        private readonly IFarayandRepository _repository;

        public FarayandController(IFarayandRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<FarayandDto>>> GetAll()
        {
            var data = await _repository.GetAllAsync();

            var result = data.Select(x => new FarayandDto
            {
                ID = x.ID,
                Onvan = x.Onvan,
                Grouh = x.Grouh,
                NameSabtKonande = x.NameSabtKonande,
                TarikhSabt = x.TarikhSabt,
                FilePath = x.FilePath
            }).ToList();

            return Ok(result);
        }
    }
}
