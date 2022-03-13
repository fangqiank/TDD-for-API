using CloudCustomer.API.Models;
using System.Collections.Generic;

namespace CloudCustomer.UnitTests.Fixtures
{
    public static class Userfixture
    {
        public static List<User> GetTestusers() => new List<User>
        {
            new User
            {
                Name = "Zhang San",
                Email = "zhangsan@mail.com",
                Address= new Address
                {
                    Street = "123 center st sw",
                    City = "Calgary",
                    ZipCode = "T2Y 3C2"
                }
            },
            new User
            {
                Name = "Li Si",
                Email = "lisi@mail.com",
                Address= new Address
                {
                    Street = "234 Market st sw",
                    City = "Calgary",
                    ZipCode = "T2W 5X5"
                }
            },
            new User
            {
                Name = "Wang Er",
                Email = "wanger@mail.com",
                Address= new Address
                {
                    Street = "1987 Maple st se",
                    City = "Calgary",
                    ZipCode = "T2C 3S6"
                }
            }
        };
    }
}
