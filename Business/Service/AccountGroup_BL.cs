using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Contact;
using VeiwModels;

namespace Business
{
    public class AccountGroup_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool InsertGroup(AccountGroup group)
        {
            if (unit.AccountGroupGeneric.Insert(group))
            {
                unit.Save();
                return true;
            }
            return false;
        }

        public bool UpdateGroup(AccountGroup group)
        {
            if (unit.AccountGroupGeneric.Update(group, group.ID))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteGroup(AccountGroup group)
        {
            if (unit.AccountGroupGeneric.Delete(group))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteGroup(int groupId)
        {
            if (unit.AccountGroupGeneric.Delete(groupId))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public AccountGroup GetGroupById(int groupId)
        {
            return unit.AccountGroup.GetAccountGroupById(groupId);
        }

        public List<AccountGroup> GetAll()
        {
            return unit.AccountGroupGeneric.Get().ToList();
        }

        public AccountGroup GetAccountGroupByName(string name)
        {
            return unit.AccountGroup.GetAccountGroupByName(name);

        }

        public List<AccountGroup> GetAccountGroupsByName(string nameGroup)
        {
            return unit.AccountGroup.GetAccountGroupsByName(nameGroup);

        }

        public bool Exist(string name)
        {
            return unit.AccountGroup.Exist(name);
        }
    }
}
