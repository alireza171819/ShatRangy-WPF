using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Repositories;
using VeiwModels;

namespace DataLayer.Service
{
    public class SerVice_DL : ISerVice_DL
    {
        private ShatRangyContext context;
        public SerVice_DL(ShatRangyContext _context)
        {
            context = _context;
        }

        public bool Exist(string customerName, string startDate, decimal comision)
        {
            var targetObjects = context.services.Where(i => i.CostomerName == customerName
            && i.StartDate == startDate 
            && i.Comision == comision).ToList();
            if (targetObjects.Count > 1)
            {
                return true;
            }
            return false;
        }

        public List<SerVice> GetSerVicesByComision(List<SerVice> filterByDateDocuments, decimal fromAmount, decimal toAmount)
        {
            throw new NotImplementedException();
        }

        public List<SerVice> GetSerVicesByComision(decimal fromAmount, decimal toAmount)
        {
            throw new NotImplementedException();
        }

        public List<SerVice> GetSerVicesByCostomerName(string costomerName)
        {
            return context.services
                .Where(i=> i.CostomerName.Contains(costomerName)).ToList();
        }

        public List<SerVice> GetSerVicesByCostomerName(List<SerVice> filterByDateDocuments, string costomerName)
        {
            return filterByDateDocuments.Where(i => i.CostomerName.Contains(costomerName)).ToList();
        }

        public List<SerVice> GetSerVicesByDescription(string description)
        {
            return context.services
                 .Where(i => i.DescriptionService.Contains(description)).ToList();
        }

        public List<SerVice> GetSerVicesByDescription(List<SerVice> filterByDateDocuments, string description)
        {
            return filterByDateDocuments.Where(i => i.DescriptionService.Contains(description)).ToList();
        }

        public List<SerVice> GetServicesByEndDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.services
                .Where(i => i.EndYear >= fromYear
                && i.EndYear <= upToYear
                && i.EndMonth >= fromMonth
                && i.EndMonth <= upToMonth
                && i.EndDay >= fromDay
                && i.EndDay <= upToDay).ToList();
        }

        public List<SerVice> GetServicesByEndDay(int currentYear, int currentMonth, int fromDay, int upToDay)
        {
            return context.services
                .Where(i => i.EndYear == currentYear
                && i.EndMonth == currentMonth
                && i.EndDay >= fromDay
                && i.EndDay <= upToDay).ToList();
        }

        public List<SerVice> GetServicesByEndMonth(int currentYear, int fromMonth, int upToMonth)
        {
            return context.services
                .Where(i => i.EndYear == currentYear
                && i.EndMonth >= fromMonth
                && i.EndMonth <= upToMonth).ToList();
        }

        public List<SerVice> GetServicesByEndMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.services
                .Where(i => i.EndYear == currentYear
                && i.EndMonth >= fromMonth
                && i.EndMonth <= upToMonth
                && i.EndDay >= fromDay
                && i.EndDay <= upToDay).ToList();
        }

        public List<SerVice> GetServicesByEndYear(int fromYear, int upToYear)
        {
            return context.services
                .Where(i => i.EndYear >= fromYear
                && i.EndYear <= upToYear).ToList();
        }

        public List<SerVice> GetServicesByEndYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth)
        {
            return context.services
                .Where(i => i.EndYear >= fromYear
                && i.EndYear <= upToYear
                && i.EndMonth >= fromMonth
                && i.EndMonth <= upToMonth).ToList();
        }

        public List<SerVice> GetSerVicesByItemName(string itemName)
        {
            return  context.services.Where(i => i.ItemName.Contains(itemName)).ToList();
        }

        public List<SerVice> GetSerVicesByNameOrDescription(string input)
        {
            return context.services.Where(i =>
            i.CostomerName.Contains(input) ||
            i.ItemName.Contains(input) ||
            i.DescriptionService.Contains(input)).ToList();
        }

        public List<SerVice> GetServicesByStartDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.services
                .Where(i => i.StartYear >= fromYear
                && i.StartYear <= upToYear
                && i.StartMonth >= fromMonth
                && i.StartMonth <= upToMonth
                && i.StartDay >= fromDay
                && i.StartDay <= upToDay).ToList();
        }

        public List<SerVice> GetServicesByStartDay(int currentYear, int currentMonth, int fromDay, int upToDay)
        {
            return context.services
                .Where(i => i.StartYear == currentYear
                && i.StartMonth == currentMonth
                && i.StartDay >= fromDay
                && i.StartDay <= upToDay).ToList();
        }

        public List<SerVice> GetServicesByStartMonth(int currentYear, int fromMonth, int upToMonth)
        {
            return context.services
                .Where(i => i.StartYear == currentYear
                 && i.StartMonth >= fromMonth
                && i.StartMonth <= upToMonth).ToList();
        }

        public List<SerVice> GetServicesByStartMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.services
                .Where(i => i.StartYear == currentYear
                && i.StartMonth >= fromMonth
                && i.StartMonth <= upToMonth
                && i.StartDay >= fromDay
                && i.StartDay <= upToDay).ToList();
        }

        public List<SerVice> GetServicesByStartYear(int fromYear, int upToYear)
        {
            return context.services
                .Where(i => i.StartYear >= fromYear
                && i.StartYear <= upToYear).ToList();
        }

        public List<SerVice> GetServicesByStartYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth)
        {
            return context.services
                .Where(i => i.StartYear >= fromYear
                && i.StartYear <= upToYear
                && i.StartMonth >= fromMonth
                && i.StartMonth <= upToMonth).ToList();
        }

        public decimal GetSerVicesComision(List<SerVice> filterByDateDocuments)
        {
            return filterByDateDocuments.Where(i => i.TransactionId > 0).Sum(i => i.Comision);
        }
    }
}
