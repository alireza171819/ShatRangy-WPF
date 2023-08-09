using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface IProfitReport_DL
    {
        decimal GetTotalSells(List<Transaction> filterTransactionByDate);

        decimal GetTotalBuys(List<Transaction> filterTransactionByDate);

        decimal GetTotalSerVice(List<Transaction> filterTransactionByDate);

        decimal GetOtherReciveds(List<Transaction> filterTransactionByDate);

        decimal GetOtherPayments(List<Transaction> filterTransactionByDate);
    }
}
