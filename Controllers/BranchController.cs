using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Library.ViewModels.Branch;

namespace Library.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branch;

        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }

        public IActionResult Index()
        {
            var branches = _branch.GetAll().Select(branch => new BranchDetailViewModel
            {
                Id = branch.Id,
                Name = branch.Name,
                IsOpen = _branch.IsBranchOpen(branch.Id),
                NumberOfAssets = _branch.GetAssets(branch.Id).Count(),
                NumberOfMembers = _branch.GetMembers(branch.Id).Count()
            });

            var vM = new BranchIndexViewModel()
            {
                Branches = branches
            };

            return View(vM);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branch.Get(id);

            var vM = new BranchDetailViewModel
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                PhoneNumber = branch.PhoneNumber,
                OpenDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfAssets = _branch.GetAssets(id).Count(),
                NumberOfMembers = _branch.GetMembers(id).Count(),
                TotalAssetValue = _branch.GetAssets(id).Sum(a => a.Cost),
                ImageURL = branch.ImageURL,
                HoursOpen = _branch.GetBranchHours(id)
            };

            return View(vM);
        }
    }
}