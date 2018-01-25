
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RunFest.Controllers;
using RunFest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RunFest.Tests
{
    public class ResultControllerTests
    {
        private static readonly DateTimeOffset emptyTime = new DateTimeOffset();

        private List<User> Users = new List<User>
        {
            new User(){ FinishTime = emptyTime, ResultTime = TimeSpan.FromDays(2) },
            new User(){ FinishTime = emptyTime.AddDays(1), ResultTime = TimeSpan.FromSeconds(3) },
            new User(){ FinishTime = emptyTime, ResultTime = TimeSpan.FromHours(2) }
        };

        [Fact(DisplayName = "IsReturnFinishedUsers")]
        public void IsReturnFinishedUsers()
        {
            var test = new Mock<IUserStore<User>>();
            UserManager<User> userManager = new UserManager<User>(test.Object, null, null, null, null, null, null, null, null);

            //Arrange
            
            var controller = new ResultController(userManager);

            //Act
            controller.Index();
            var result = (ActionResult)controller.Index();

            // Assert
            //Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [Fact(DisplayName = "GetFinishedUsers_FinishTimeMoreThanZero_FinishedUsers")]
        public void GetFinishedUsers()
        {
            //Arrange

            var controller = new ResultController();

            //Act
            var real = controller.GetFinishedUsers(Users);
            List<User> excepted = new List<User>() { Users[1] };

            // Assert
            Assert.Equal(real, excepted);
        }

        [Fact(DisplayName = "OrderFinishedUsers_ByAscendingResultTime_OrderedFinishedUsers")]
        public void OrderFinishedUsers()
        {
            //Arrange

            var controller = new ResultController();

            //Act
            var real = controller.OrderFinishedUsers(Users);
            List<User> excepted = Users.OrderBy(User => User.ResultTime).ToList();

            // Assert
            Assert.Equal(real, excepted);

        }
    }
}
