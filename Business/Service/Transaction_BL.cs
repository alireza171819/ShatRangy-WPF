using System.Collections.Generic;
using VeiwModels;
using DataLayer.Contact;
using System.Globalization;
using System;

namespace Business
{
    public class Transaction_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool Exist(string accountName, string date, decimal payment, decimal recived)
        {
            return unit.Transaction.Exist(accountName, date, payment, recived);
        }

        public bool InsertTransaction(Transaction transaction)
        {
            if (unit.TransactionGeneric.Insert(transaction))
            {
                return true;
            }
            return false;
        }

        public bool DeleteTransaction(Transaction transaction)
        {
            if (unit.TransactionGeneric.Delete(transaction))
            {
                return true;
            }
            return false;
        }

        public bool DeleteTransaction(int transactionId)
        {
            if (unit.TransactionGeneric.Delete(transactionId))
            {
                return true;
            }
            return false;
        }

        public bool UpdateTransaction(Transaction transaction)
        {
            if (unit.TransactionGeneric.Update(transaction, transaction.ID))
            {
                return true;
            }
            return false;
        }

        public Transaction GetTransactionById(int transactionId)
        {
            return unit.TransactionGeneric.GetById(transactionId);
        }

        public List<Transaction> GetTransactionsByDescription(List<Transaction> filteredDocumentsByDate, string description)
        {
            if (filteredDocumentsByDate != null)
            {
                return unit.Transaction.GetTransactionsByDescription(filteredDocumentsByDate, description);
            }
            else
            {
                return unit.Transaction.GetTransactionsByDescription(description);
            }
        }

        public List<Transaction> GetTransactionsByAccountSide(List<Transaction> filteredDocumentsByDate, string accountSideName)
        {
            if (filteredDocumentsByDate != null)
            {
                return unit.Transaction.GetTransactionsByAccountSide( filteredDocumentsByDate, accountSideName);
            }
            else
            {
                return unit.Transaction.GetTransactionsByAccountSide(accountSideName);
            }
        }

        public List<Transaction> GetTransactionsByPaymentAmount(List<Transaction> filteredDocumentsByDate, decimal fromAmount, decimal toAmount)
        {
            if (filteredDocumentsByDate != null)
            {
                return unit.Transaction.GetTransactionsByPaymentAmount(filteredDocumentsByDate, fromAmount, toAmount);
            }
            else
            {
                return unit.Transaction.GetTransactionsByPaymentAmount(fromAmount, toAmount);
            }
        }

        public List<Transaction> GetTransactionsByRecivedAmount(List<Transaction> filteredDocumentsByDate, decimal fromAmount, decimal toAmount)
        {
            if (filteredDocumentsByDate != null)
            {
                return unit.Transaction.GetTransactionsByRecivedAmount(filteredDocumentsByDate, fromAmount, toAmount);
            }
            else
            {
                return unit.Transaction.GetTransactionsByRecivedAmount(fromAmount, toAmount);
            }
        }

        public List<Transaction> GetTransactionsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            PersianCalendar  persianCalendar = new PersianCalendar();
            int currentYear = persianCalendar.GetYear(DateTime.Now);
            int currentMonth = persianCalendar.GetMonth(DateTime.Now);
            if (fromYear != 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Transaction.GetTransactionsByDate(fromYear, upToYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.Transaction.GetTransactionsByDay(currentYear, currentMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Transaction.GetTransactionsByMonth(currentYear, fromMonth, upToMonth);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Transaction.GetTransactionsByMonth_Day(currentYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay == 0)
            {
                return unit.Transaction.GetTransactionsByYear(fromYear, upToYear);
            }
            else if (fromYear != 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Transaction.GetTransactionsByYear_Month(fromYear, upToYear, fromMonth, upToMonth);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.Transaction.GetTransactionsByYear(fromYear, upToYear);
            }
            else
            {
                return  null;
            }
        }

        public List<Transaction> GetTransactionsByNameOrDescription(string input)
        {
            return unit.Transaction.GetTransactionsByAccountSidNameOrDescription(input);
        }
    }
}
