using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RunFest.Models;

namespace RunFest.Controllers
{
    public class RunController : BaseController
    {
        private readonly DateTimeOffset emptyTime = new DateTimeOffset();

        public RunController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Start()
        {
            List<User> Users = _userManager.Users.Where( User => User.StartTime.CompareTo(emptyTime) == 0 ).ToList();

            return View(Users);
        }

        [HttpGet]
        public IActionResult Finish()
        {
            List<User> Users = _userManager.Users.Where(User => User.StartTime.CompareTo(emptyTime) > 0 &&
                                                                User.FinishTime.CompareTo(emptyTime) == 0).ToList();
            return View(Users);
        }

        [HttpGet]
        public IActionResult Finished()
        {
            List<User> Users = _userManager.Users.Where(User => User.StartTime.CompareTo(emptyTime) > 0 &&
                                                                User.FinishTime.CompareTo(emptyTime) > 0).ToList();
            return View(Users);
        }
    }
}