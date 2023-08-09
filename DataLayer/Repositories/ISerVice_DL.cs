using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface ISerVice_DL
    {
        bool Exist(string customerName, string startDate, decimal comision);

        List<SerVice> GetServicesByStartDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<SerVice> GetServicesByStartYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth);

        List<SerVice> GetServicesByStartYear(int fromYear, int upToYear);

        List<SerVice> GetServicesByStartMonth(int currentYear, int fromMonth, int upToMonth);

        List<SerVice> GetServicesByStartDay(int currentYear, int currentMonth, int fromDay, int upToDay);

        List<SerVice> GetServicesByStartMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<SerVice> GetServicesByEndDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<SerVice> GetServicesByEndYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth);

        List<SerVice> GetServicesByEndYear(int fromYear, int upToYear);

        List<SerVice> GetServicesByEndMonth(int currentYear, int fromMonth, int upToMonth);

        List<SerVice> GetServicesByEndDay(int currentYear, int currentMonth, int fromDay, int upToDay);

        List<SerVice> GetServicesByEndMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<SerVice> GetSerVicesByCostomerName(string costomerName);

        List<SerVice> GetSerVicesByItemName(string itemName);

        List<SerVice> GetSerVicesByNameOrDescription(string input);

        List<SerVice> GetSerVicesByCostomerName(List<SerVice> filterByDateDocuments, string costomerName);

        List<SerVice> GetSerVicesByDescription(string description);

        List<SerVice> GetSerVicesByDescription(List<SerVice> filterByDateDocuments, string description);

        List<SerVice> GetSerVicesByComision(List<SerVice> filterByDateDocuments, decimal fromAmount, decimal toAmount);

        List<SerVice> GetSerVicesByComision(decimal fromAmount, decimal toAmount);

        decimal GetSerVicesComision(List<SerVice> filterByDateDocuments);
    }
}
