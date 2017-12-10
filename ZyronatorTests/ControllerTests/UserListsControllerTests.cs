using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zyronator.Controllers;

namespace ZyronatorTests.ControllerTests
{
    [TestClass]
    public class UserListsControllerTests
    {
        [TestMethod]
        public void GetUserLists()
        {
            var controller = new UserListsController();

            var response = controller.GetUserLists();
        }
    }
}
