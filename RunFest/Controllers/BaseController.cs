using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RunFest.Models;

namespace RunFest.Controllers
{
    public class BaseController : Controller
    {
      protected UserManager<User> _userManager;
    }
}