using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using pmService.Models;
using PmServiceNetCode.DTOs;
using PmServiceNetCode.Interfaces;
using System.Text;

namespace pmService.Repositories
{
    public class DerakhtTajhizatRepository : IDerakhtTajhizatRepository
    {
        // Year filter that was hardcoded in the original query (Tarikh LIKE '1404%').
        private const string TarikhPrefix = "1404";

        // Identifiers cannot be parameterized in SQL, so they must be whitelisted.
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

        private readonly MaznetModel _db;
        private readonly ClassData _data;

        public DerakhtTajhizatRepository(MaznetModel db, ClassData data)
        {
            _db = db;
            _data = data;
        }

        // ==========================
        // Raw SQL / stored procedure
        // ==========================
        public async Task<object> ListIradatAsync(IReadOnlyList<IradatQuery> queries)
        {
            if (queries.Count == 0)
                throw new ArgumentException("At least one item is required.");

            var sql = new StringBuilder();
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@TarikhPrefix", TarikhPrefix + "%")
            };

            for (var i = 0; i < queries.Count; i++)
            {
                var q = queries[i];

                if (!AllowedTables.Contains(q.TableName))
                    throw new ArgumentException("Invalid table");

                if (!AllowedColumns.Contains(q.NameColumn))
                    throw new ArgumentException("Invalid column");

                if (!AllowedColumns.Contains(q.CodeColumn))
                    throw new ArgumentException("Invalid code column");

                if (i > 0)
                    sql.Append(" UNION ALL ");

                var noeParam = $"@NoeTajhiz{i}";
                parameters.Add(new SqlParameter(noeParam, q.NoeTajhizId));

                sql.Append($@"
SELECT
    B.*,
    T.{q.NameColumn} COLLATE Arabic_CI_AI AS name_tajhiz,
    (I.onvan_irad + ' ' + ISNULL(IC.name_irad,'') + ' ' + OG.name_o) AS onvane_irad,
    OG.shomare,
    OG.code_o,
    I.code_irad,
    IC.code_irad AS code_rizirad
FROM TBL_Bazdid_Shode B
INNER JOIN Tbl_jozeiatezamanbandibazdid J
    ON B.Code_Bazdid = J.code
INNER JOIN {q.TableName} T
    ON J.code_tajhiz = T.{q.CodeColumn}
INNER JOIN tbl_irad I
    ON B.Kharabi = I.code_irad
INNER JOIN tbl_olaviat_goroh OG
    ON B.olaviat = OG.code_o
LEFT JOIN Tbl_IradatCheck IC
    ON B.Code_rizIrad = IC.code_irad
WHERE
    B.Noe_Tajhiz = {noeParam}
    AND B.Flag = 0
    AND B.Tarikh LIKE @TarikhPrefix
");

                // IN (...) list built from parameters, never from raw text
                if (q.IradIds.Count > 0)
                {
                    var names = new List<string>();
                    for (var j = 0; j < q.IradIds.Count; j++)
                    {
                        var name = $"@Irad{i}_{j}";
                        parameters.Add(new SqlParameter(name, q.IradIds[j]));
                        names.Add(name);
                    }

                    sql.Append($" AND B.Kharabi IN ({string.Join(",", names)}) ");
                }
            }

            return await _data.ExecuteSqlAsync(sql.ToString(), parameters.ToArray());
        }

        public async Task<object> ListIradatByNoeTajhizAsync(int idTajhiz)
        {
            return await _data.ExecuteProcedureAsync(
                "cmms_get_list_irad_noe_tajhiz",
                new SqlParameter("@ID_Tajhiz", idTajhiz));
        }

        // ==========================
        // EF Core CRUD
        // ==========================
        public Task<List<Tbl_Derakht_Tajhizat>> GetAllAsync()
            => _db.Tbl_Derakht_Tajhizat.AsNoTracking().ToListAsync();

        public async Task<Tbl_Derakht_Tajhizat?> GetByIdAsync(int id)
            => await _db.Tbl_Derakht_Tajhizat.FindAsync(id);

        public async Task<Tbl_Derakht_Tajhizat> AddAsync(Tbl_Derakht_Tajhizat model)
        {
            _db.Tbl_Derakht_Tajhizat.Add(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<bool> UpdateAsync(Tbl_Derakht_Tajhizat model)
        {
            _db.Entry(model).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
               // when (!await _db.Tbl_Derakht_Tajhizat.AnyAsync(e => e.ID == model.ID))
            {
                return false;
            }
        }

        public async Task<Tbl_Derakht_Tajhizat?> DeleteAsync(int id)
        {
            var entity = await _db.Tbl_Derakht_Tajhizat.FindAsync(id);
            if (entity == null) return null;

            _db.Tbl_Derakht_Tajhizat.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}