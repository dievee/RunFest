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
    public class RunControllerTests
    {
        private static readonly DateTimeOffset emptyTime = new DateTimeOffset();

        private List<User> Users = new List<User>
        {
            new User(){ StartTime = emptyTime, RunningNumber = 3, FinishTime = emptyTime.AddDays(1) },
            new User(){ StartTime = emptyTime.AddDays(1), RunningNumber = 1, FinishTime = emptyTime },
            new User(){ StartTime = emptyTime, RunningNumber = 2 },
            new User(){ StartTime = emptyTime.AddHours(1), RunningNumber = 2, FinishTime = emptyTime },
            new User(){ StartTime = emptyTime.AddDays(3), RunningNumber = 2, FinishTime = emptyTime.AddDays(4) }
        };


        [Fact(DisplayName = "GetNotStartedUsers_StartTimeIsZero_NotStartedUsers")]
        public void GetNotStartedUsers()
        {
            //Arrange

            var controller = new RunController();

            //Act
            var real = controller.GetNotStartedUsers(Users);
            List<User> excepted = new List<User>() { Users[0], Users[2] };

            // Assert
            Assert.Equal(real, excepted);
        }

        [Fact(DisplayName = "GetStartedNotFinishedUsers_StartTimeBiggerZeroFinishTimeIsZero_StartedNotFinishedUsers")]
        public void GetStartedNotFinishedUsers()
        {
            //Arrange

            var controller = new RunController();

            //Act
            var real = controller.GetStartedNotFinishedUsers(Users);
            List<User> excepted = new List<User>() { Users[1], Users[3] };

            // Assert
            Assert.Equal(real, excepted);

        }

        [Fact(DisplayName = "GetFinishedUsers_FinishBiggerZero_FinishedUsers")]
        public void GetFinishedUsers()
        {
            //Arrange

            var controller = new RunController();

            //Act
            var real = controller.GetFinishedUsers(Users);
            List<User> excepted = new List<User>() { Users[0], Users[4] };

            // Assert
            Assert.Equal(real, excepted);

        }
    }
}
