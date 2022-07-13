using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services.IRepositories;

namespace WebApplication1.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemBranchRepository _itemBranchRepository;
        private readonly IStore _store;
        public ItemController(IItemBranchRepository itemBranchRepository, IStore store)
        {
            _itemBranchRepository = itemBranchRepository;
            _store = store;

        }
        public async Task<IActionResult> Index(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            try
            {
                var itemOfBranch = await _itemBranchRepository.GetItemsQuntityInBranch(id, await _itemBranchRepository.GetAllItemsForBranch(id));
                ViewData["Title"] = await _store.GetTitle(id);
                return View(itemOfBranch);
            }
            catch
            {
                return Problem("somthing might went wrong while you are showing list of items inside branch");
            }
            
        }


        public async Task<IActionResult> AddItemToBranch(int id)
        {
            try
            {

                if (id == 0)
                {

                    return NotFound();
                }
                var names = await _itemBranchRepository.GetItemsNameNotInBranch(id);
                var ids=await _itemBranchRepository.GetItemsIDsNotInBranch(id);
                return View(new AddUpdateItemViewModel {BranchID=id, ItemNames=names,ItemIds=ids});

            }
            catch
            {

               return  Problem("somthing might went wrong while showing addition form");
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> AddItemToBranch(int id,ItemBranch itemBranch)
        {

            try
            {

                if(id==0||itemBranch==null)
                {
                    return NotFound();

                }


                if(!ModelState.IsValid)return BadRequest();
                itemBranch.BranchId = id;
                await _itemBranchRepository.AddItemToBranch(itemBranch);

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return Problem("there is somthing might Went Wrong while you are adding new item to branch");
            }
           

        }



        public async Task<IActionResult> DeleteItemFromBranch(int id,int branchId)
        {
            try
            {

                if (id == 0 || branchId == 0)
                {
                    return BadRequest();
                }
                await _itemBranchRepository.DeleteItemFromBranch(branchId, id);

                return RedirectToAction("Index", new { id = branchId });
            }
            catch
            {
                return Problem("there somthing might went Wrong while Deleting Item From Branch");
            }
           

        }


        public async Task<IActionResult> UpdateItemFromBranch(int id,int oldItemId)
        {

            try
            {

                 if (id == 0)
                 {

                   return NotFound();
                 }
             var names = await _itemBranchRepository.GetItemsNameNotInBranch(id);
             var ids = await _itemBranchRepository.GetItemsIDsNotInBranch(id);
                TempData["oldItemId"] = oldItemId;
             return View(new AddUpdateItemViewModel { OldItemID = oldItemId, BranchID = id, ItemNames = names, ItemIds = ids });
            }catch
            {
                return Problem("there is somthing might Went Wrong while you are showing update page of item in branch");
            }

        }


        [HttpPost]
        public async Task<IActionResult> UpdateItemFromBranch(int id, ItemBranch newItemBranch)
        {


            try
            {

                if (id == 0 || newItemBranch == null)
                {
                    return NotFound();

                }


                if (!ModelState.IsValid) return BadRequest();
                newItemBranch.BranchId = id;
                await _itemBranchRepository.UpdateItemFromBranch((int)TempData["oldItemId"],id,newItemBranch);


                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return Problem("there is somthing might Went Wrong while you are updating  item in branch");
            }

        }


        [HttpGet]
        public async Task<IActionResult> PurchaseMoreFromItem(int id, int oldItemId)
        {

            try
            {

                if (id == 0)
                {

                    return NotFound();
                }
                var names = await _itemBranchRepository.GetItemsNameInBranch(id);
                var ids = await _itemBranchRepository.GetItemsIDsInBranch(id);
                ViewData["ItemName"] = await _itemBranchRepository.GetItemsName(oldItemId);
                return View(new AddUpdateItemViewModel {OldItemID=oldItemId, BranchID = id, ItemIds = ids });

            }
            catch
            {

                return Problem("somthing might went wrong while showing addition form");
            }


        }


        
        [HttpPost]
        public async Task<IActionResult> PurchaseMoreFromItem(int id,int oldItemId, int Quantity)
        {

            try
            {

                if (id == 0 )
                {
                    return NotFound();

                }


                if (!ModelState.IsValid) return BadRequest();
                await _itemBranchRepository.purchaseMore(oldItemId, id, Quantity);


                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return Problem("there is somthing might Went Wrong while you are updating  item in branch");
            }

        }


    }
}
