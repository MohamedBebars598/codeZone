using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services.IRepositories;

namespace WebApplication1.Services.Repositories
{
    public class ItemBranchRepository : IItemBranchRepository
    {
        private readonly StockContext _db;
        private readonly DbSet<ItemBranch> _itemBranch;
        private readonly DbSet<Item> _item;
        public ItemBranchRepository(StockContext db)
        {
            _db = db;
            _itemBranch = _db.ItemBranches;
            _item = _db.Items;
        }
        public async Task<List<Item>> GetAllItemsForBranch(int id)
        {

            return await _itemBranch.Where(i => i.BranchId == id).Select(i => i.Item).Select(i => i).ToListAsync();


        }


        public async Task<List<ItemOfBranchViewModel>> GetItemsQuntityInBranch(int branchId, List<Item> items)
        {

            List<ItemOfBranchViewModel> result = new List<ItemOfBranchViewModel>();
            foreach (var item in items)
            {
                var itemBranch = await _itemBranch.FindAsync(item.ID, branchId);
                var quantity = itemBranch?.Quantity;
                result.Add(new ItemOfBranchViewModel() { Item = item, Quntity = quantity ??= 0, Id = branchId });
            }

            return result;


        }



        public async Task<int> GetItemQuntityInBranch(int itemID, int branchId)
        {
            var itemBranch = await _itemBranch.FindAsync(itemID, branchId);
            var quantity = itemBranch?.Quantity;



            return quantity ??= 0;


        }

        public async Task<List<string>> GetItemsNameNotInBranch(int branchID)
        {
            var itemsWillBeIgnored = await _itemBranch.Where(i => i.BranchId == branchID).Select(i => i.ItemId).ToListAsync();

            var names = await _item.Where(i => !itemsWillBeIgnored.Any(e => e == i.ID)).Select(i => i.Name).ToListAsync();

            return names;
        }


        public async Task<List<int>> GetItemsIDsNotInBranch(int branchID)
        {
            var itemsWillBeIgnored = await _itemBranch.Where(i => i.BranchId == branchID).Select(i => i.ItemId).ToListAsync();

            var names = await _item.Where(i => !itemsWillBeIgnored.Any(e => e == i.ID)).Select(i => i.ID).ToListAsync();

            return names;
        }


        public async Task<Boolean> AddItemToBranch(ItemBranch itemBranch)
        {
            var recordEntry = await _itemBranch.AddAsync(itemBranch);
            _db.SaveChanges();
            if (recordEntry != null)
            {
                return true;
            }

            return false;
        }



        public async Task<Boolean> DeleteItemFromBranch(int branchId, int ItemId)
        {
            var item = await _itemBranch.FindAsync(ItemId, branchId);
            if (item != null)
            {
                _itemBranch.Remove(item);
                _db.SaveChanges();
                return true;
            }

            return false;

        }


        public async Task<Boolean> UpdateItemFromBranch(int oldItemId,int brnachID,ItemBranch newItemBranch)
        {

            var old = await _itemBranch.FindAsync(oldItemId,brnachID);
            if (old != null)
            {
                _itemBranch.Remove(old);
                await _itemBranch.AddAsync(newItemBranch);
                await _db.SaveChangesAsync();
                return true;
            }


            return false;
        }




        public async Task<Boolean> purchaseMore(int itemID, int branchItem,int quantity)
        {
            var itemWillBePurshased=await _itemBranch.FindAsync(itemID, branchItem);
            if (itemWillBePurshased != null)
            {
                itemWillBePurshased.Quantity += quantity;
                await _db.SaveChangesAsync();
                return true;

            }

            return false;
        }



        public async Task<List<int>> GetItemsIDsInBranch(int branchID)
        {

           var idList=await _itemBranch.Where(i=>i.BranchId==branchID).Select(i=>i.ItemId).ToListAsync();
            return idList;
        }



        public async Task<List<string>> GetItemsNameInBranch(int branchID)
        {
            var idList = await _itemBranch.Where(i => i.BranchId == branchID).Select(i => i.Item.Name).ToListAsync();

            return idList;

        }

        public async Task<string> GetItemsName(int itemId)
        {

            var name = await _item.FindAsync(itemId);

            if(name != null)
            {
            return name.Name;

            }

            return "";
        }

    }



    
}
