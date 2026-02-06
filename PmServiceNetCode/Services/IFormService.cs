using PmServiceNetCode.DTOs;

namespace PmServiceNetCode.Services
{
    public interface IFormService
    {
        Task<List<FormDto>> GetAllAsync();
        Task<FormDto?> GetByIdAsync(int idForm);
        Task<FormDto> CreateAsync(CreateFormDto dto);
        Task<bool> DeleteAsync(int idForm);

    }

}
