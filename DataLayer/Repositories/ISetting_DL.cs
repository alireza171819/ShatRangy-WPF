using VeiwModels;

namespace DataLayer.Repositories
{
    public interface ISetting_DL
    {
        Setting GetSettingByName(string settingName);

        Setting GetSettingByValue(string settingValue);
    }
}
