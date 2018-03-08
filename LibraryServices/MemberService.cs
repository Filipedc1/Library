using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices
{
    public class MemberService : IMember
    {
        #region Fields

        private LibraryContext _context;

        #endregion

        #region Constructor

        public MemberService(LibraryContext context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public void Add(Member newMember)
        {
            //no need to do "_context.Members.Add()" because entity framework is smart enough
            _context.Add(newMember);
            _context.SaveChanges();
        }

        public Member Get(int id)
        {
            return GetAll()
                .FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members
                .Include(m => m.LibraryCard)
                .Include(m => m.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int memberId)
        {
            var cardId = Get(memberId).LibraryCard.Id;

            return _context.CheckoutHistories
                .Include(c => c.LibraryCard)
                .Include(c => c.LibraryAsset)
                .Where(c => c.LibraryCard.Id == cardId)
                .OrderByDescending(c => c.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int memberId)
        {
            var cardId = Get(memberId).LibraryCard.Id;
                
            return _context.Checkouts
                .Include(c => c.LibraryCard)
                .Include(c => c.LibraryAsset)
                .Where(c => c.LibraryCard.Id == cardId);
        }

        public IEnumerable<Hold> GetHolds(int memberId)
        {
            var cardId = Get(memberId).LibraryCard.Id;

            return _context.Holds
                .Include(h => h.LibraryCard)
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryCard.Id == cardId)
                .OrderByDescending(h => h.HoldPlaced); 
        }

        #endregion
    }
}
