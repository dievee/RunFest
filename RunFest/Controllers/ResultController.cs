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

        public ResultController(UserManager<User> userManager = null)
        {
            _userManager = userManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<User> Users = _userManager.Users.ToList();
            Users = GetFinishedUsers(Users);
            Users = OrderFinishedUsers(Users);

            return View( Users );
        }

        public List<User> GetFinishedUsers(List<User> Users)
        {

            return Users.Where(User => User.FinishTime.CompareTo(emptyTime) > 0).ToList();
        }

        public List<User> OrderFinishedUsers(List<User> Users)
        {

            return Users.OrderBy(User => User.ResultTime).ToList();
        }
    }
}