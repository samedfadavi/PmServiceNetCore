using Microsoft.EntityFrameworkCore;
using pmService.Models;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Models;

namespace PmServiceNetCode.Repositories
{
    public class FarayandRepository : IFarayandRepository
    {
        private readonly MaznetModel _context;

        public FarayandRepository(MaznetModel context)
        {
            _context = context;
        }

        public async Task<List<TblFarayand>> GetAllAsync()
        {
            return await _context.TblFarayand.AsNoTracking().ToListAsync();
        }
    }
}
