using PmServiceNetCode.Models;

namespace PmServiceNetCode.Interfaces
{
 

    public interface IFormRepository
    {
        Task<List<Form>> GetAllAsync();
        Task<Form?> GetByIdAsync(int idForm);
        Task<Form> AddAsync(Form form);
        Task<bool> SoftDeleteAsync(int idForm);

    }
}
