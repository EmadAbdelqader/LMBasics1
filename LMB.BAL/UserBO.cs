using LMB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LMB.BAL
{
    public class UserBO
    {
        #region Private Members
        private LMDBDataContext dc;
        #endregion

        #region Ctors..
        public UserBO()
        {
            dc = new LMDBDataContext();
        }
        #endregion

        #region Get Methods

        public List<User> GetUsers()
        {
            // SELECT * FROM Users WHERE IsDeleted = False
            return dc.Users.ToList();
        }

        public User GetUser(int Id)
        {
            // SELECT * FROM Users Where UserId = @Id
            return dc.Users.SingleOrDefault(u => u.UserId == Id);
        }

        public List<User> GetUsersBySearch()
        {
            // TODO
            return new List<User>();
        }

        // Write a method that search for user based on the first name column 
        public List<User> GetUserSea(string firstName)
        {
            //return dc.Users.Where(u => u.FirstName == firstName).ToList();

            var Query = from u in dc.Users
                        where u.UserId == 3
                        select u;

            return Query.ToList();
        }


        #endregion

        #region Insert/Update Methods
        public int Save(User user)
        {
            var _user = dc.Users.Where(c => c.UserId == user.UserId).FirstOrDefault();
            bool isInsert = false;

            // If the user exists -> update operation
            // if the used does not exist -> save
            if (_user == null)
            {
                _user = new User();
                _user.CreatedOn = DateTime.UtcNow;
                _user.CreatedBy = user.CreatedBy;
                isInsert = true;
            }
            else
            {
                _user.UpdatedOn = DateTime.UtcNow;
                _user.UpdatedBy = user.UpdatedBy;
            }

            // AutMapper -> Manual
            _user.FirstName = user.FirstName;

            // Check for null if the coming lastName from the UI is null
            if (user.LastName != null)
                _user.LastName = user.LastName;
            _user.Username = user.Username;
            _user.IsDeleted = user.IsDeleted;

            // Saving the user to the database
            if (isInsert == true)
                dc.Users.InsertOnSubmit(_user);

            dc.SubmitChanges();

            return _user.UserId;

        }
        #endregion

        #region Delete Methods

        public void Delete(int userId)
        {
            var delUser = dc.Users.Where(c=>c.UserId == userId).FirstOrDefault();

            // check for null
            if (delUser == null) return;

            dc.Users.DeleteOnSubmit(delUser);
            dc.SubmitChanges();
        }

        public void FakeDelete(int userId)
        {
            var delUser = dc.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (delUser == null) return;

            delUser.IsDeleted = true;
            
            dc.SubmitChanges();
        }

        #endregion
    }
}


// Important Linq Methods
// Where()
// First()
// FirstOrDefault()
// Single
// SingleOfDefault
// OrderBy
// Join
// Aggregate Functions, Max(), Min(), dc.Users.Where(u=> u.FirstName == "Sobhi").Average(c=> c.Salary)