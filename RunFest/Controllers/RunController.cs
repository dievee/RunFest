﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RunFest.Models;
using Microsoft.AspNetCore.Authorization;

namespace RunFest.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RunController : BaseController
    {
        private readonly DateTimeOffset emptyTime = new DateTimeOffset();

        public RunController(UserManager<User> userManager = null)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Start()
        {
            List<User> Users = _userManager.Users.ToList();
            Users = GetNotStartedUsers(Users).OrderBy(User => User.RunningNumber).ToList();

            return View(Users);
        }

        public List<User> GetNotStartedUsers(List<User> Users)
        {

            return Users.Where(User => User.StartTime.CompareTo(emptyTime) == 0).ToList();
        }


        [HttpPost]
        public async Task<IActionResult> Start(string userId)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.StartTime = DateTimeOffset.Now;

                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                List<User> Users = _userManager.Users.ToList();
                Users = GetNotStartedUsers(Users).OrderBy(User => User.RunningNumber).ToList();

                return View(Users);
            }
            else
            {
                return View();
            }            
        }

        [HttpGet]
        public IActionResult Finish()
        {
            List<User> Users = _userManager.Users.ToList();
            Users = GetStartedNotFinishedUsers(Users).OrderBy(User => User.RunningNumber).ToList();

            return View(Users);
        }

        public List<User> GetStartedNotFinishedUsers(List<User> Users)
        {

            return Users.Where(User => User.StartTime.CompareTo(emptyTime) > 0 && User.FinishTime.CompareTo(emptyTime) == 0).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Finish(string userId)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.FinishTime = DateTimeOffset.Now;
                    user.ResultTime = user.FinishTime - user.StartTime;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                List<User> Users = _userManager.Users.ToList();
                Users = GetStartedNotFinishedUsers(Users).OrderBy(User => User.RunningNumber).ToList();

                return View(Users);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Finished()
        {
            List<User> Users = _userManager.Users.ToList();
            Users = GetFinishedUsers(Users).OrderBy(User => User.RunningNumber).ToList();

            return View(Users);
        }

        public List<User> GetFinishedUsers(List<User> Users)
        {

            return Users.Where(User => User.FinishTime.CompareTo(emptyTime) > 0).ToList();
        }

    }
}