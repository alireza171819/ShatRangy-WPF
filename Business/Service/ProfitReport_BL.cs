using DataLayer.Contact;
using System.Collections.Generic;
using VeiwModels;

namespace Business.Service
{
    public class ProfitReport_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public decimal GetBuysTotal(List<Transaction> transactionsByDate)
        {
            return unit.ProfitReport.GetTotalBuys(transactionsByDate);
        }

        public decimal GetSellsTotal(List<Transaction> transactionsByDate)
        {
            return unit.ProfitReport.GetTotalSells(transactionsByDate);
        }

        public decimal GetServiceTotal(List<Transaction> transactionsByDate)
        {
            return unit.ProfitReport.GetTotalSerVice(transactionsByDate);
        }

        public decimal GetTotalOtherRecived(List<Transaction> transactionsByDate)
        {
            return unit.ProfitReport.GetOtherReciveds(transactionsByDate);
        }

        public decimal GetTotalOtherPayment(List<Transaction> transactionsByDate)
        {
            return unit.ProfitReport.GetOtherPayments(transactionsByDate);
        }

    }
}
