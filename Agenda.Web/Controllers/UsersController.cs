﻿using AutoMapper;
using Agenda.Data.Context;
using Agenda.Domain.Entity;
using Agenda.Domain.Models;
using Agenda.Domain.Util;
using Agenda.Services.Interface;
using Agenda.Web.Helpers;
using Agenda.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.Web.Controllers
{
    [ServiceFilter(typeof(SecurityAttribute))]
    public class UsersController : Controller
    {
        private IUserService _userService;
        public readonly IMapper _mapper;

        public UsersController(ADbContext context, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public async Task<IActionResult> Index()
        {
            return View(new UserVM());
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public async Task<IActionResult> Index(UserVM userVM, int page, int qtPage)
        {
            var result = new PaginateResult<IEnumerable<User>>();

            try
            {
                var filter = _mapper.Map<User>(userVM);

                result = await _userService.GetAllPaginate(filter, page, qtPage);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            try
            {
                var user = _mapper.Map<User>(userVM);

                var result = _userService.Insert(user);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var result = await _userService.GetById(id);

                if (result.Success)
                {
                    return View(_mapper.Map<UserVM>(result.Object));
                }
                else
                {
                    TempData[Constants.SYSTEM_ERROR_KEY] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData[Constants.SYSTEM_ERROR_KEY] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public async Task<IActionResult> Edit(int id, UserVM userVM)
        {
            try
            {
                var user = _mapper.Map<User>(userVM);

                var result = _userService.Update(user);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TypeFilter(typeof(ExceptionLog))]
        [Authorize(Roles = Constants.PROFILE_TI)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _userService.Delete(id);

                return Json(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpPost]
        //public async Task<IEnumerable<SelectListItem>> GetProfileList(Domain.Entity.Profile filter)
        //{
        //    var result = await _profileService.GetAll(filter);

        //    if (result.Success)
        //    {
        //        IList<SelectListItem> items = new List<SelectListItem>();

        //        foreach (Domain.Entity.Profile item in result.Object)
        //        {
        //            items.Add(new SelectListItem() { Value = item.Name, Text = item.Name, Selected = filter.Id == item.Id ? true : false });
        //        }

        //        return items;
        //    }
        //    else
        //    {
        //        return new List<SelectListItem>();
        //    }
        //}
    }
}
