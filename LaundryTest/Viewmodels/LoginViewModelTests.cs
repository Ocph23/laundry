using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaundryApp.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryApp.Viewmodels.Tests
{
    [TestClass()]
    public class LoginViewModelTests
    {
        [TestMethod()]
        public void LoginActionTestUserNotHaveAccess()
        {
            var user = new Viewmodels.LoginViewModel();
            user.UserName = "admin";
            user.Password = "admin";

            user.LoginCommand.Execute(null);

            Assert.IsNotNull(user.MessageError);
        }

        [TestMethod()]
        public void LoginActionTestPasswordNotMach()
        {
            var user = new Viewmodels.LoginViewModel();
            user.UserName = "ocph23";
            user.Password = "admin";

            user.LoginCommand.Execute(null);
            Console.WriteLine(user.MessageError);
            Assert.IsNotNull(user.MessageError);
        }

        [TestMethod()]
        public void LoginActionTestCanAccessApp()
        {
            var user = new Viewmodels.LoginViewModel();
            user.UserName = "ocph23";
            user.Password = "Sony";

            user.LoginCommand.Execute(null);
            Assert.IsNotNull(user.UserLogin);
        }


    }
}