using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
    public class LibraryBranchService : ILibraryBranch
    {
        #region Fields

        private LibraryContext _context;

        #endregion

        #region Constructor

        public LibraryBranchService(LibraryContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public void Add(LibraryBranch newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int branchId)
        {
            return GetAll().FirstOrDefault(b => b.Id == branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches
                .Include(b => b.Members)
                .Include(b => b.LibraryAssets);
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.LibraryBranches
                .Include(b => b.LibraryAssets)
                .FirstOrDefault(b => b.Id == branchId)
                .LibraryAssets;
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var branchHours = _context.BranchHours.Where(h => h.Branch.Id == branchId);

            return DataHelpers.MakeBusinessHoursReadible(branchHours);

        }

        public IEnumerable<Member> GetMembers(int branchId)
        {
            return _context.LibraryBranches
                .Include(b=> b.Members)
                .FirstOrDefault(b => b.Id == branchId)
                .Members;
        }

        public bool IsBranchOpen(int branchId)
        {
            var currentTimeHour = DateTime.Now.Hour;

            //doing "+1" because in BranchHours table, DayOfWeek column is set as 1-7 instead of 0-6
            var currentDayOfWeek = (int)DateTime.Now.DayOfWeek + 1;
            var branchHours = _context.BranchHours.Where(h => h.Branch.Id == branchId);
            var dayHour = branchHours.FirstOrDefault(h => h.DayOfWeek == currentDayOfWeek);

            return currentTimeHour < dayHour.CloseTime && currentTimeHour > dayHour.OpenTime;
        }

        #endregion

    }
}
