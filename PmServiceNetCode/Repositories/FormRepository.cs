using Microsoft.EntityFrameworkCore;
using pmService.Models;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Models;
using System;

namespace PmServiceNetCode.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly MaznetModel _context;

        public FormRepository(MaznetModel context)
        {
            _context = context;
        }

        public async Task<List<Form>> GetAllAsync()
        {
            return await _context.Forms
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Form?> GetByIdAsync(int idForm)
        {
            return await _context.Forms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdForm == idForm);
        }
        public async Task<Form> AddAsync(Form form)
        {
            _context.Forms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }
        public async Task<bool> SoftDeleteAsync(int idForm)
        {
            var entity = await _context.Forms
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(x => x.IdForm == idForm);

            if (entity == null)
                return false;

            if (entity.IsDeleted)
                return true; // idempotent

            entity.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

    }

}
