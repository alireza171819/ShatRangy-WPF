using System;
using System.Linq;
using DataLayer.Repositories;
using VeiwModels;

namespace DataLayer.Service
{
    public class Setting_DL : ISetting_DL
    {
        private ShatRangyContext context;
        public Setting_DL(ShatRangyContext context)
        {
            this.context = context;
        }
        public Setting GetSettingByName(string settingName)
        {
            try
            {
                return context.settings.Where(i => i.Name == settingName).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Setting GetSettingByValue(string settingValue)
        {
            try
            {
                return context.settings.Where(i => i.Value == settingValue).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
