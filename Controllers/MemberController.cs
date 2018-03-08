using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.ViewModels.Catalog;
using Library.ViewModels.Member;
using LibraryData;
using LibraryData.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class MemberController : Controller
    {
        #region Private Fields

        private readonly IMember _member;

        #endregion

        #region Constructor

        public MemberController(IMember member)
        {
            _member = member;
        }

        #endregion

        #region Public Methods

        public IActionResult Index()
        {
            var members = _member.GetAll();

            var memberVMs = members.Select(m => new MemberDetailViewModel
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                LibraryCardId = m.LibraryCard.Id,
                OverdueFees = m.LibraryCard.Fees,
                HomeLibraryBranch = m.HomeLibraryBranch.Name
            }).ToList();

            var vM = new MemberIndexViewModel
            {
                Members = memberVMs
            };

            return View(vM);
        }

        public IActionResult Detail(int id)
        {
            var member = _member.Get(id);

            var vM = new MemberDetailViewModel
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                LibraryCardId = member.LibraryCard.Id,
                Address = member.Address,
                MemberSince = member.LibraryCard.Created,
                OverdueFees = member.LibraryCard.Fees,
                PhoneNumber = member.PhoneNumber,
                HomeLibraryBranch = member.HomeLibraryBranch.Name,
                AssetsCheckedOut = _member.GetCheckouts(id) ?? new List<Checkout>(),
                CheckoutHistory = _member.GetCheckoutHistory(id),
                Holds = _member.GetHolds(id)
            };

            return View(vM);
        }

        #endregion
    }
}