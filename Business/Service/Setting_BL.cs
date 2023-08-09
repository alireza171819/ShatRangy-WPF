using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Contact;
using VeiwModels;

namespace Business
{
    public class Setting_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool InsertSetting(Setting setting)
        {
            if (unit.SettingGeneric.Insert(setting))
            {
                unit.Save();
                unit.Dispose();
                return true;
            }
            else
            {
                unit.Dispose();
                return false;
            }
        }

        public bool UpdateSetting(Setting setting)
        {
            if (unit.SettingGeneric.Update(setting, setting.ID))
            {
                unit.Save();
                unit.Dispose();
                return true;
            }
            else
            {
                unit.Dispose();
                return false;
            }
        }

        public bool DeleteSetting(Setting setting)
        {
            if (unit.SettingGeneric.Delete(setting))
            {
                unit.Save();
                unit.Dispose();
                return true;
            }
            else
            {
                unit.Dispose();
                return false;
            }
        }

        public bool DeleteSetting(int settingId)
        {
            if (unit.SettingGeneric.Delete(settingId))
            {
                unit.Save();
                unit.Dispose();
                return true;
            }
            else
            {
                unit.Dispose();
                return false;
            }
        }

        public Setting GetSettingById(int settingId)
        {
            return unit.SettingGeneric.GetById(settingId);
        }

        public Setting GetSettingByName(string settingName)
        {
            return unit.Setting.GetSettingByName(settingName);
        }

        public Setting GetSettingByValue(string settingValue)
        {
            return unit.Setting.GetSettingByValue(settingValue);
        }
    }
}
