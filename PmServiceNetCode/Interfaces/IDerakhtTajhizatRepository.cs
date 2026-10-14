using global::pmService.Models;
using PmServiceNetCode.DTOs;

namespace PmServiceNetCode.Interfaces
{
    
        public interface IDerakhtTajhizatRepository
        {
            // ---- Raw SQL / stored procedure ----

            /// <summary>
            /// Runs the UNION ALL report. Throws ArgumentException when a table/column
            /// is not whitelisted or the input is empty.
            /// </summary>
            Task<object> ListIradatAsync(IReadOnlyList<IradatQuery> queries);

            Task<object> ListIradatByNoeTajhizAsync(int idTajhiz);

            // ---- EF Core CRUD ----

            Task<List<Tbl_Derakht_Tajhizat>> GetAllAsync();

            Task<Tbl_Derakht_Tajhizat?> GetByIdAsync(int id);

            Task<Tbl_Derakht_Tajhizat> AddAsync(Tbl_Derakht_Tajhizat model);

            /// <returns>false when the row no longer exists.</returns>
            Task<bool> UpdateAsync(Tbl_Derakht_Tajhizat model);

            /// <returns>The deleted entity, or null when it was not found.</returns>
            Task<Tbl_Derakht_Tajhizat?> DeleteAsync(int id);
        }
    
}
