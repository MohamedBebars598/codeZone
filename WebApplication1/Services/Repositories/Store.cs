using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services.IRepositories;

namespace WebApplication1.Services.Repositories
{
    public class Store:IStore
    {
        private readonly StockContext _db;
        private readonly DbSet<Branch> _branches;
        public Store(StockContext db)
        {   _db = db; 
            _branches = _db.Branches;
        }
       
        public async Task<Boolean> AddBranch(Branch b) {

            try
            {
                _branches.Add(b);
               await _db.SaveChangesAsync();

            }
            catch
            {
                return false;
            }

          return true;
        
        }
        public async Task<Boolean> UpdateBranch(Branch b)
        {
            try
            {
                _branches.Update(b);
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;

            }

            return true;
            

        }
        public async Task<Boolean> DeleteBranch(int Id)
        {
            try
            {
                var branch = await _branches.FindAsync(Id);
                _branches.Remove(branch);
                await _db.SaveChangesAsync();

            }
            catch
            {

                return false;
            }
            return true;
            
        }

        public async Task<List<Branch>> GetAllBranches()
        {
           

            return await _branches.Select(i=>i).ToListAsync();
          
         
        }

        public async Task<Branch> GetBranchById(int? id)
        {

                return await _branches.Where(i=>i.ID==id).Select(i=>i).FirstOrDefaultAsync();
            
        }

        public async Task<Boolean> BranchExist(int id)
        {
            return await _branches.Where(i=>i.ID==id).AnyAsync();
        }


        public async Task<string> GetTitle(int id)
        {
            var store = await _branches.FindAsync(id);
            return store != null ? store.Name : "";
        }
    }
}
