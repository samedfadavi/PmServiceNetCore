using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using pmService.Models;
using pmService.Repositories;
using PmServiceNetCode.DTOs;
using PmServiceNetCode.Interfaces;

namespace pmService.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class Derakht_TajhizatController : ControllerBase
    {
        private readonly IDerakhtTajhizatRepository _repo;

        public Derakht_TajhizatController(IDerakhtTajhizatRepository repo)
        {
            _repo = repo;
        }

        // ==========================
        // POST: List_Iradat
        // ==========================
        [AllowAnonymous]
        [HttpPost("List_Iradat")]
        public async Task<IActionResult> List_Iradat([FromBody] JArray items)
        {
            try
            {
                var queries = items.Select(ToQuery).ToList();
                return Ok(await _repo.ListIradatAsync(queries));
            }
            catch (Exception ex) when (ex is ArgumentException or FormatException or InvalidCastException)
            {
                return BadRequest(ex.Message);
            }
        }

        // ==========================
        // GET: Stored Procedure
        // ==========================
        [AllowAnonymous]
        [HttpGet("List_Iradat_Noe_Tajhiz")]
        public async Task<IActionResult> List_Iradat_Noe_Tajhiz(int ID_Tajhiz)
        {
            return Ok(await _repo.ListIradatByNoeTajhizAsync(ID_Tajhiz));
        }

        // ==========================
        // CRUD
        // ==========================
        [AllowAnonymous]
        [HttpGet("GetDerakht_Tajhizat")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tbl_Derakht_Tajhizat model)
        {
            var created = await _repo.AddAsync(model);
            return CreatedAtAction(nameof(Get), new { id = created.ID }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tbl_Derakht_Tajhizat model)
        {
            if (id != model.ID) return BadRequest();

            return await _repo.UpdateAsync(model) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repo.DeleteAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        // ==========================
        // Helpers
        // ==========================
        private static IradatQuery ToQuery(JToken item) => new(
            TableName: item["Name_Jadval_Pm"]?.ToString() ?? string.Empty,
            NameColumn: item["Name_Pm"]?.ToString() ?? string.Empty,
            CodeColumn: item["Code_Pm"]?.ToString() ?? string.Empty,
            NoeTajhizId: item["ID"]?.Value<int>() ?? 0,
            IradIds: ParseIds(item["whereirad"]?.ToString()));

        // "1, 2,3" -> [1, 2, 3]; throws FormatException for anything that is not an integer
        private static List<int> ParseIds(string? csv)
        {
            if (string.IsNullOrWhiteSpace(csv)) return new List<int>();

            return csv
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToList();
        }
    }
}