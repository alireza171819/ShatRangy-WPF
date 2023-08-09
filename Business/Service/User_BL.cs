using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Contact;
using VeiwModels;

namespace Business
{
    public class User_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool InsertUser(User user)
        {
            if (unit.UserGeneric.Insert(user))
            {
                return true;
            }
            return false;
        }

        public bool UpdateUser(User user)
        {
            if (unit.UserGeneric.Update(user, user.ID))
            {
                return true;
            }
            return false;
        }

        public bool DeleteUser(User user)
        {
            if (unit.UserGeneric.Delete(user))
            {
                return true;
            }
            return false;
        }

        public bool DeleteUser(int userId)
        {
            if (unit.UserGeneric.Delete(userId))
            {
                return true;
            }
            return false;
        }

        public User GetUserById(int userId)
        {
            return unit.UserGeneric.GetById(userId);
        }

        public List<User> GetAllUsers()
        {
            return unit.UserGeneric.Get().ToList();
        }
    }
}
