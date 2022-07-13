using WebApplication1.Models;

namespace WebApplication1.Services.IRepositories
{
    public interface IItemBranchRepository
    {


        Task<List<Item>> GetAllItemsForBranch(int id);
        Task<List<ItemOfBranchViewModel>> GetItemsQuntityInBranch(int branchId, List<Item> items);
        Task<List<string>> GetItemsNameNotInBranch(int branchID);
        Task<List<int>> GetItemsIDsNotInBranch(int branchID);
        Task<int> GetItemQuntityInBranch(int itemID, int branchId);
        Task<Boolean> AddItemToBranch(ItemBranch itemBranch);
        Task<Boolean> DeleteItemFromBranch(int branchId, int ItemId);

        Task<Boolean> UpdateItemFromBranch(int oldItemId, int brnachID, ItemBranch newItemBranch);

        Task<Boolean> purchaseMore(int itemID, int branchItem, int quantity);

        Task<List<int>> GetItemsIDsInBranch(int branchID);
        Task<List<string>> GetItemsNameInBranch(int branchID);
        Task<string> GetItemsName(int itemId);
    }
}
