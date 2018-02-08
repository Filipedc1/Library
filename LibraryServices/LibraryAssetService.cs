using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        #region Private Fields

        private LibraryContext dBContext;

        #endregion

        #region Constructor

        public LibraryAssetService(LibraryContext context)
        {
            dBContext = context;
        }

        #endregion

        #region Public Members

        public void Add(LibraryAsset newAsset)
        {
            dBContext.Add(newAsset);
            dBContext.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            //.Include() allows us to include Status and Location entities in the query result
            return dBContext.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = dBContext.LibraryAssets.OfType<Book>()
                .Where(asset => asset.Id == id).Any();

            var isVideo = dBContext.LibraryAssets.OfType<Video>()
                .Where(asset => asset.Id == id).Any();

            return isBook ?
                dBContext.Books.FirstOrDefault(b => b.Id == id).Author :
                dBContext.Videos.FirstOrDefault(v => v.Id == id).Director
                ?? "Unknown"; //if not a book nor a video return unknown
        }

        public string GetBookIndex(int id)
        {
            if (dBContext.Books.Any(book => book.Id == id))
            {
                return dBContext.Books
                    .FirstOrDefault(book => book.Id == id).bookIndex;
            }

            return "";
        }

        public LibraryAsset GetById(int id)
        {
            return GetAll().FirstOrDefault(asset => asset.Id == id);

            //return await dBContext.LibraryAssets
            //    .Include(asset => asset.Status)
            //    .Include(asset => asset.Location)
            //    .FirstOrDefaultAsync(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
            //return dBContext.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Location;
        }

        public string GetISBN(int id)
        {
            if (dBContext.Books.Any(book => book.Id == id))
            {
                return dBContext.Books
                    .FirstOrDefault(book => book.Id == id).ISBN;
            }

            return "";
        }

        public string GetTitle(int id)
        {
            return dBContext.LibraryAssets.FirstOrDefault(a => a.Id == id).Title;
        }

        //get asset type. e.g. book or video
        public string GetType(int id)
        {
            var book = dBContext.LibraryAssets.OfType<Book>()
                .Where(b => b.Id == id);

            return book.Any() ? "Book" : "Video"; //create enum instead called "Type"
        }

        #endregion
    }
}
