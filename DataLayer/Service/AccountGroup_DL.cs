using DataLayer.Contact;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using VeiwModels;

namespace DataLayer.Service
{
    internal class AccountGroup_DL : IAccountGroup_DL
    {
        private ShatRangyContext context;
        public AccountGroup_DL(ShatRangyContext context)
        {
            this.context = context;
        }

        public bool Exist(string nameGroup)
        {
            var targetObject = context.accountGroups.Where(i => i.GroupName == nameGroup).ToList();
            if (targetObject.Count >= 1)
            {
                return true;
            }
            return false;
        }

        public AccountGroup GetAccountGroupById(int id)
        {
            return context.accountGroups.Find(id);
        }

        public AccountGroup GetAccountGroupByName(string nameGroup)
        {
            try
            {
                return context.accountGroups.Where(i => i.GroupName == nameGroup).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AccountGroup> GetAccountGroupsByName(string nameGroup)
        {
            try
            {
                return context.accountGroups.Where(i => i.GroupName.Contains(nameGroup)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<AccountGroup> GetAllAccountGroups()
        {
            return context.accountGroups.ToList();
        }
    }
}
