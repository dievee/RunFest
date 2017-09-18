using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RunFest.Models;
using Microsoft.AspNetCore.Authorization;

namespace RunFest.Controllers
{
    public class ResultController : BaseController
    {
        private readonly DateTimeOffset emptyTime = new DateTimeOffset();

        public ResultController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<User> Users = _userManager.Users.Where(User => User.FinishTime.CompareTo(emptyTime) > 0).OrderBy(User => User.ResultTime).ToList(); //.Where(User => User.StartTime.CompareTo(emptyTime) == 0)

            return View(Users);
        }
    }
}