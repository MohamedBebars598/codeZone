using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication1.Models;
using WebApplication1.Services.IRepositories;

namespace WebApplication1.Controllers
{
    public class StoreController : Controller
    {

        private readonly IStore _store;
        public StoreController(IStore store)
        {
            _store = store;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var stores=await _store.GetAllBranches();
                return View(stores);
            }
            catch
            {

                return Problem("Somthing Might went Wrong while Getting All Branches");
            }

            
           
        }


        
        public IActionResult AddBranch()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddBranch(Branch branch)
        {
            if (ModelState.IsValid)
            {
               var status= await _store.AddBranch(branch);

                if (status == false)
                {
                    return Problem("Somthing Might went Wrong while Adding New Branch");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }



        public async Task<IActionResult> EditBranch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _store.GetBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }



        [HttpPost]
        public async Task<IActionResult> EditBranch(int id,Branch branch)
        {
            if (id != branch.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
               var updatedBranch= await _store.UpdateBranch(branch);

                if (updatedBranch)
                {

                return RedirectToAction(nameof(Index));
                }

                return Problem("Somthing Might went Wrong while Editing Branch Info");
            }
            return View(branch);
        }




        public async Task<IActionResult> DeleteBranch(int id)
        {


            var branchStatus = await _store.DeleteBranch(id);
               
            if (branchStatus == false)
            {
                return Problem("Somthing Might went Wrong while Deleting Branch");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
