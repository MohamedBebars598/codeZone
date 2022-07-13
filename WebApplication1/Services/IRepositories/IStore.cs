using WebApplication1.Models;

namespace WebApplication1.Services.IRepositories
{
    public interface IStore
    {
        Task<List<Branch>> GetAllBranches();
        Task<Branch> GetBranchById(int? id);
        Task<Boolean> AddBranch(Branch b);
        Task<Boolean> UpdateBranch(Branch b);
        Task<Boolean> DeleteBranch(int Id);
        Task<Boolean> BranchExist(int id);
        Task<string> GetTitle(int id);
    }
}
