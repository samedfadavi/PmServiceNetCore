using PmServiceNetCode.DTOs;
using PmServiceNetCode.Interfaces;
using PmServiceNetCode.Models;

namespace PmServiceNetCode.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _repository;

        public FormService(IFormRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FormDto>> GetAllAsync()
        {
            var forms = await _repository.GetAllAsync();

            return forms.Select(f => new FormDto(
                f.IdForm,
                f.Onvan,
                f.Description
            )).ToList();
        }

        public async Task<FormDto?> GetByIdAsync(int idForm)
        {
            var form = await _repository.GetByIdAsync(idForm);
            if (form == null) return null;

            return new FormDto(
                form.IdForm,
                form.Onvan,
                form.Description
            );
        }
        public async Task<FormDto> CreateAsync(CreateFormDto dto)
        {
            var entity = new Form
            {
                Onvan = dto.Onvan,
                Description = dto.Description
            };

            var created = await _repository.AddAsync(entity);

            return new FormDto(
                created.IdForm,
                created.Onvan,
                created.Description
            );
        }
        public async Task<bool> DeleteAsync(int idForm)
        {
            return await _repository.SoftDeleteAsync(idForm);
        }


    }

}
