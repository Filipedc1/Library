using LibraryData;
using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace LibraryServices
{
    public class LibraryBranchService : ILibraryBranch
    {
        private LibraryContext _context;

        public LibraryBranchService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryBranch newBranch)
        {
            _context.LibraryBranches.Add(newBranch);
        }

        public LibraryBranch Get(int branchId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetMembers(int branchId)
        {
            throw new NotImplementedException();
        }

        public bool IsBranchOpen(int branchId)
        {
            throw new NotImplementedException();
        }
    }
}
