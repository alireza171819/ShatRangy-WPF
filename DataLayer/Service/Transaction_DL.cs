using System.Collections.Generic;
using System.Linq;
using DataLayer.Repositories;
using VeiwModels;

namespace DataLayer.Service
{
    public class Transaction_DL : ITransaction_DL
    {
        private ShatRangyContext context;
        public Transaction_DL(ShatRangyContext context)
        {
            this.context = context;
        }

        public bool Exist(string accountName, string date, decimal payment, decimal recived)
        {
            var targetObjects = context.transactions.Where(i => i.AccountSideName == accountName
            && i.Date == date
            && i.Payment == payment
            && i.Recived == recived).ToList();
            if (targetObjects.Count > 1)
            {
                return true;
            }
            return false;
        }

        public List<Transaction> GetTransactionsByRecivedAmount(decimal fromAmount, decimal toAmount) 
        {
            return context.transactions
                .Where(i => i.Recived >= fromAmount
                && i.Recived <= toAmount).ToList();
        }

        public List<Transaction> GetTransactionsByPaymentAmount(decimal fromAmount, decimal toAmount)
        {
            return context.transactions
                .Where(i => i.Payment >= fromAmount
                && i.Payment <= toAmount).ToList();
        }

        public List<Transaction> GetTransactionsByDescription(string description)
        {
            return context.transactions
                .Where(i => i.Description.Contains(description)).ToList();
        }

        public List<Transaction> GetTransactionsByAccountSide(string accountSideName)
        {
            return context.transactions
                .Where(i => i.AccountSideName.Contains(accountSideName)).ToList();
        }

        public List<Transaction> GetTransactionsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.transactions
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear 
                && i.Month >= fromMonth
                && i.Month <= upToMonth 
                && i.Day >= fromDay 
                && i.Day <= upToDay).ToList();
        }

        public List<Transaction> GetTransactionsByDay(int currentYear, int currentMonth, int fromDay, int upToDay)
        {
            return context.transactions
                .Where(i => i.Year == currentYear 
                && i.Month == currentMonth
                && i.Day >= fromDay 
                && i.Day <= upToDay).ToList();
        }

        public List<Transaction> GetTransactionsByMonth(int currentYear, int fromMonth, int upToMonth)
        {
            return context.transactions
                .Where(i => i.Year == currentYear 
                && i.Month >= fromMonth
                && i.Month <= upToMonth).ToList();
        }

        public List<Transaction> GetTransactionsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.transactions
                .Where(i => i.Year == currentYear 
                && i.Month >= fromMonth
                && i.Month <= upToMonth 
                && i.Day >= fromDay 
                && i.Day <= upToDay).ToList();
        }

        public List<Transaction> GetTransactionsByYear(int fromYear, int upToYear)
        {
            return context.transactions
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear).ToList();
        }

        public List<Transaction> GetTransactionsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth)
        {
            return context.transactions
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear 
                && i.Month >= fromMonth
                && i.Month <= upToMonth).ToList();
        }

        public List<Transaction> GetTransactionsByRecivedAmount(List<Transaction> filterDocumentByDate, decimal fromAmount, decimal toAmount)
        {
            return filterDocumentByDate.Where(i => i.Recived>= fromAmount && i.Recived <= toAmount).ToList();
        }

        public List<Transaction> GetTransactionsByPaymentAmount(List<Transaction> filterDocumentByDate, decimal fromAmount, decimal toAmount)
        {
            return filterDocumentByDate.Where(i => i.Payment >= fromAmount && i.Payment <= toAmount).ToList();
        }

        public List<Transaction> GetTransactionsByDescription(List<Transaction> filterDocumentByDate, string description)
        {
            return filterDocumentByDate.Where(i => i.Description.Contains(description)).ToList();
        }

        public List<Transaction> GetTransactionsByAccountSide(List<Transaction> filterDocumentByDate, string accountSideName)
        {
            return filterDocumentByDate.Where(i => i.AccountSideName.Contains(accountSideName)).ToList();
        }

        public List<Transaction> GetTransactionsByAccountSidNameOrDescription(string input)
        {
            return context.transactions.Where(i => i.AccountSideName.Contains(input) || i.Description.Contains(input)).ToList();    
        }

        public decimal GetTransactionTotalPayment(List<Transaction> filterDocumentByDate)
        {
            return filterDocumentByDate.Where(i => !i.Description.Contains("خرید")).Sum(i => i.Payment);
        }

        public decimal GetTransactionTotalRcived(List<Transaction> filterDocumentByDate)
        {
            return filterDocumentByDate.Where(i => !i.Description.Contains("فروش")).Sum(i => i.Recived);
        }
    }
}
