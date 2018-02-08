using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ViewModels.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        //Note: In order to use the methods from this interface that I already implemented in LibraryAssetService.cs,
        //need to inject the LibraryAssetService into this controller by adding it to the startup class under ConfigureServices
        private ILibraryAsset _assets;

        public CatalogController(ILibraryAsset assets)
        {
            this._assets = assets;
        }

        public IActionResult Index()
        {
            var assets = _assets.GetAll();

            //.Select() here will map every element in the collection of asset models into a new model
            var listingResult = assets
                .Select(result => new AssetIndexListingViewModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageURL,
                    AuthorOrDirector = _assets.GetAuthorOrDirector(result.Id),
                    bookIndex = _assets.GetBookIndex(result.Id),
                    Title = result.Title,
                    Type = _assets.GetType(result.Id)
                });

            var vM = new AssetIndexViewModel()
            {
                Assets = listingResult
            };

            return View(vM);
        }
    }
}