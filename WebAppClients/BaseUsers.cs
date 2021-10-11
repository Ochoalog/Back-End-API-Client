using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppClients
{
    public static class BaseUsers
    {
        public static IEnumerable<User> Users()
        {
            return new List<User>
            {
                new User { Name = "Vitor@Gmail.com", Password = "123456",
                    Functions = new string[] {Function.Dev, Function.Admin} },
                new User { Name = "Admin@Gmail.com", Password = "123456",
                    Functions = new string[] {Function.Admin} },
                new User { Name = "Tempus@Gmail.com", Password = "123456",
                    Functions = new string[] {Function.Admin} }
            };
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string[] Functions { get; set; }
    }

    public class Function
    {
        public const string Admin = "Admin";
        public const string Operator = "Operator";
        public const string Dev = "Dev";
    }
}