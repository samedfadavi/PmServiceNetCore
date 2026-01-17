using PmServiceNetCode.Models;

namespace PmServiceNetCode.Interfaces
{
    public interface IFarayandRepository
    {
        Task<List<TblFarayand>> GetAllAsync();
    }
}
