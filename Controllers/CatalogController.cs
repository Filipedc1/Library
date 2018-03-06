using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ViewModels;
using Library.ViewModels.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class CatalogController : Controller
    {
        #region Fields

        //Note: In order to use the methods from this interface that I already implemented in LibraryAssetService.cs,
        //need to inject the LibraryAssetService into this controller by adding it to the startup class under ConfigureServices
        private ILibraryAsset _assets;
        private ICheckout _checkouts;

        #endregion

        #region Constructor

        public CatalogController(ILibraryAsset assets, ICheckout checkouts)
        {
            _assets = assets;
            _checkouts = checkouts;
        }

        #endregion

        #region Public Members

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

        public IActionResult Detail(int id)
        {
            var asset = _assets.GetById(id);
            var currentHolds = _checkouts.GetCurrentHolds(id)
                .Select(a => new AssetHoldModel
                {
                    HoldPlaced = _checkouts.GetCurrentHoldPlaced(a.Id),
                    MemberName = _checkouts.GetCurrentHoldMemberName(a.Id),
                });

            if (asset == null)
            {
                return BadRequest();
            }

            var vM = new AssetDetailViewModel()
            {
                AssetId = asset.Id,
                Title = asset.Title,
                Type = _assets.GetType(id),
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageURL = asset.ImageURL,
                AuthorOrDirector = _assets.GetAuthorOrDirector(asset.Id),
                CurrentLocation = _assets.GetCurrentLocation(asset.Id).Name,
                bookIndex = _assets.GetBookIndex(asset.Id),
                ISBN = _assets.GetISBN(asset.Id),
                CheckoutHistory = _checkouts.GetCheckoutHistory(id),
                LatestCheckout = _checkouts.GetLatestCheckout(id),
                MemberName = _checkouts.GetCurrentCheckoutMemberName(id),
                CurrentHolds = currentHolds
            };

            return View(vM);
        }

        public IActionResult Checkout(int id)
        {
            var asset = _assets.GetById(id);

            var vM = new CheckoutViewModel
            {
                AssetId = id,
                ImageURL = asset.ImageURL,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id)
            };

            return View(vM);
        }

        public IActionResult CheckIn(int id)
        {
            _checkouts.CheckInItem(id);
            return RedirectToAction("Detail", new { id = id });
        }

        public IActionResult Hold(int id)
        {
            var asset = _assets.GetById(id);

            var vM = new CheckoutViewModel
            {
                AssetId = id,
                ImageURL = asset.ImageURL,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkouts.IsCheckedOut(id),
                HoldCount = _checkouts.GetCurrentHolds(id).Count()
            };

            return View(vM);
        }

        public IActionResult MarkLost(int assetId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        public IActionResult MarkFound(int assetId)
        {
            _checkouts.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkouts.CheckOutItem(assetId, libraryCardId);
            
            //the second parameters specifies which detail page we want
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkouts.PlaceHold(assetId, libraryCardId);

            //the second parameters specifies which detail page we want
            return RedirectToAction("Detail", new { id = assetId });
        }

        #endregion
    }
}