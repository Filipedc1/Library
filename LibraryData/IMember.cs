using LibraryData.Models;
using System.Collections.Generic;

namespace LibraryData
{
    public interface IMember
    {
        Member Get(int id);
        IEnumerable<Member> GetAll();
        void Add(Member newMember);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int memberId);
        IEnumerable<Hold> GetHolds(int memberId);
        IEnumerable<Checkout> GetCheckouts(int memberId);
    }
}
