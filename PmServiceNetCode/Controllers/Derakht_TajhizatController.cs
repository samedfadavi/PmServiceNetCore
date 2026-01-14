using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pmService.Models;
using System.Security;

namespace pmService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Derakht_TajhizatController : ControllerBase
    {
        private readonly MaznetModel _db;
        private readonly ClassData _data;

        public Derakht_TajhizatController(
            MaznetModel db,
            ClassData data)
        {
            _db = db;
            _data = data;
        }

        // ==========================
        // WHITELISTS (CRITICAL)
        // ==========================
        private static readonly HashSet<string> AllowedTables = new()
        {
            "Tbl_Tajhiz_A",
            "Tbl_Tajhiz_B"
        };

        private static readonly HashSet<string> AllowedColumns = new()
        {
            "ID",
            "Code",
            "Name"
        };

        // ==========================
        // POST: List_Iradat
        // ==========================
        [HttpPost("List_Iradat")]
        public async Task<IActionResult> List_Iradat([FromBody] JArray items)
        {
            var sql = "";
            var parameters = new List<SqlParameter>();
            int i = 0;

            foreach (var item in items)
            {
                var table = item["Name_Jadval_Pm"]?.ToString();
                var fieldName = item["Name_Pm"]?.ToString();
                var fieldCode = item["Code_Pm"]?.ToString();
                var noeTajhiz = item["ID"]?.Value<int>() ?? 0;
                var whereIrads = item["whereirad"]?.ToString();

                // 🔐 Whitelisting
                if (!AllowedTables.Contains(table))
                    throw new SecurityException("Invalid table");

                if (!AllowedColumns.Contains(fieldName) ||
                    !AllowedColumns.Contains(fieldCode))
                    throw new SecurityException("Invalid column");

                if (i++ > 0)
                    sql += " UNION ALL ";

                var paramName = $"@NoeTajhiz{i}";
                parameters.Add(new SqlParameter(paramName, noeTajhiz));

                sql += $@"
SELECT 
    B.*, 
    T.{fieldName} COLLATE Arabic_CI_AI AS name_tajhiz,
    (I.onvan_irad + ' ' + ISNULL(IC.name_irad,'') + ' ' + OG.name_o) AS onvane_irad,
    OG.shomare,
    OG.code_o,
    I.code_irad,
    IC.code_irad AS code_rizirad
FROM TBL_Bazdid_Shode B
INNER JOIN Tbl_jozeiatezamanbandibazdid J 
    ON B.Code_Bazdid = J.code
INNER JOIN {table} T 
    ON J.code_tajhiz = T.{fieldCode}
INNER JOIN tbl_irad I 
    ON B.Kharabi = I.code_irad
INNER JOIN tbl_olaviat_goroh OG 
    ON B.olaviat = OG.code_o
LEFT JOIN Tbl_IradatCheck IC 
    ON B.Code_rizIrad = IC.code_irad
WHERE 
    B.Noe_Tajhiz = {paramName}
    AND B.Flag = 0
    AND B.Tarikh LIKE '1404%'
";

                if (!string.IsNullOrWhiteSpace(whereIrads))
                    sql += $" AND B.Kharabi IN ({whereIrads}) ";
            }

            var result = await _data.ExecuteSqlAsync(sql, parameters.ToArray());
            return Ok(result);
        }

        // ==========================
        // GET: Stored Procedure
        // ==========================
        [HttpGet("List_Iradat_Noe_Tajhiz")]
        public async Task<IActionResult> List_Iradat_Noe_Tajhiz(int ID_Tajhiz)
        {
            var result = await _data.ExecuteProcedureAsync(
                "cmms_get_list_irad_noe_tajhiz",
                new SqlParameter("@ID_Tajhiz", ID_Tajhiz)
            );

            return Ok(result);
        }

        // ==========================
        // STANDARD EF CORE CRUD
        // ==========================
        [HttpGet("GetDerakht_Tajhizat")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Tbl_Derakht_Tajhizat.AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _db.Tbl_Derakht_Tajhizat.FindAsync(id);
            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tbl_Derakht_Tajhizat model)
        {
            _db.Tbl_Derakht_Tajhizat.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = model.ID }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tbl_Derakht_Tajhizat model)
        {
            if (id != model.ID) return BadRequest();

            _db.Entry(model).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Tbl_Derakht_Tajhizat.FindAsync(id);
            if (entity == null) return NotFound();

            _db.Remove(entity);
            await _db.SaveChangesAsync();
            return Ok(entity);
        }
    }
}
