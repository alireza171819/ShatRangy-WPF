using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface ITransaction_DL
    {
        bool Exist(string accountName, string date, decimal payment, decimal recived);

        List<Transaction> GetTransactionsByRecivedAmount(decimal fromAmount, decimal toAmount);

        List<Transaction> GetTransactionsByPaymentAmount(decimal fromAmount, decimal toAmount);

        List<Transaction> GetTransactionsByDescription(string description);

        List<Transaction> GetTransactionsByAccountSidNameOrDescription(string input);

        List<Transaction> GetTransactionsByRecivedAmount(List<Transaction> filterDocumentByDate, decimal fromAmount, decimal toAmount);

        List<Transaction> GetTransactionsByPaymentAmount(List<Transaction> filterDocumentByDate, decimal fromAmount, decimal toAmount);

        List<Transaction> GetTransactionsByDescription(List<Transaction> filterDocumentByDate, string description);

        List<Transaction> GetTransactionsByAccountSide(string accountSideName);

        List<Transaction> GetTransactionsByAccountSide(List<Transaction> filterDocumentByDate, string accountSideName);

        List<Transaction> GetTransactionsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<Transaction> GetTransactionsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth);

        List<Transaction> GetTransactionsByYear(int fromYear, int upToYear);

        List<Transaction> GetTransactionsByMonth(int currentYear, int fromMonth, int upToMonth);

        List<Transaction> GetTransactionsByDay(int currentYear, int currentMonth, int fromDay, int upToDay);

        List<Transaction> GetTransactionsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        decimal GetTransactionTotalPayment(List<Transaction> filterDocumentByDate);

        decimal GetTransactionTotalRcived(List<Transaction> filterDocumentByDate);
    }
}
