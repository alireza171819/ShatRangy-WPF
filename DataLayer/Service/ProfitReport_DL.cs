using DataLayer.Repositories;
using System.Collections.Generic;
using System.Linq;
using VeiwModels;

namespace DataLayer.Service
{
    public class ProfitReport_DL : IProfitReport_DL
    {
        public decimal GetOtherPayments(List<Transaction> filterTransactionByDate)
        {
            return filterTransactionByDate.Where(i => 
            !i.Description.StartsWith("خرید")).ToList().Sum(i => i.Payment);
        }

        public decimal GetOtherReciveds(List<Transaction> filterTransactionByDate)
        {
            return filterTransactionByDate.Where(i =>
            !i.Description.StartsWith("خدمات") ||
            !i.Description.StartsWith("فروش")).ToList().Sum(i => i.Recived);
        }

        public decimal GetTotalBuys(List<Transaction> filterTransactionByDate)
        {
            return filterTransactionByDate.Where(i =>
            i.Description.StartsWith("خرید")).ToList().Sum(i => i.Payment);
        }

        public decimal GetTotalSells(List<Transaction> filterTransactionByDate)
        {
            return filterTransactionByDate.Where(i => 
            i.Description.StartsWith("فروش")).ToList().Sum(i => i.Recived);
        }

        public decimal GetTotalSerVice(List<Transaction> filterTransactionByDate)
        {
            return filterTransactionByDate.Where(i => 
            i.Description.StartsWith("خدمات")).ToList().Sum(i => i.Recived);
        }
    }
}
