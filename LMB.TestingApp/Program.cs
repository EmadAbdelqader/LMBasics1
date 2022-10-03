using LMB.BAL;
using LMB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMB.TestingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserBO userBO = new UserBO();

            //var users = userBO.GetUsers();

            //User user1 = new User()
            //{
            //    // userId
            //    UserId = 11,
            //    FirstName = "Tahseen",
            //};

            //int newId = userBO.Save(user1);

            userBO.FakeDelete(9);
        }
    }
}
