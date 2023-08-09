using System;
using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface IAccountGroup_DL
    {
        AccountGroup GetAccountGroupByName(string name);
        List<AccountGroup> GetAccountGroupsByName(string name);
        AccountGroup GetAccountGroupById(int id);
        List<AccountGroup> GetAllAccountGroups();
        bool Exist(string nameGroup);
    }
}
