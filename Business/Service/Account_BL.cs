using System.Collections.Generic;
using VeiwModels;
using DataLayer.Contact;

namespace Business
{
    public class Account_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool InsertAccount(Account account)
        {
            if (unit.AccountGeneric.Insert(account))
            {
                return true;
            }
            return false;
        }

        public bool UpdateAccount(Account account)
        {
            if (unit.AccountGeneric.Update(account, account.ID))
            {
                return true;
            }
            return false;
        }

        public bool DeleteAccount(Account account)
        {
            if (unit.AccountGeneric.Delete(account))
            {
                return true;
            }
            return false;
        }

        public bool DeleteAccount(int accountId)
        {
            if (unit.AccountGeneric.Delete(accountId))
            {
                return true;
            }
            return false;
        }

        public List<Account> GetAll()
        {
            return unit.Account.GetAll();
        }

        public Account GetAccountById(int accountId)
        {
            return unit.AccountGeneric.GetById(accountId);
        }

        public Account GetAccountByName(string accountName)
        {
            return unit.Account.GetAccountByName(accountName);
        }

        public List<Account> GetAccountsByName(string accountName)
        {
            return unit.Account.GetAccountsByName(accountName);
        }

        public List<Account> GetAccountsByGroupName(string groupName)
        {
            return unit.Account.GetAccountByGroup(groupName);
        }

        public List<Account> GetAccountsByPhoneNumber(string phoneNumber)
        {
            return unit.Account.GetAccountByPhoneNumber(phoneNumber);
        }

        public List<Account> GetAccountsByAddress(string address)
        {
            return unit.Account.GetAccountByAddress(address);
        }

        public List<Account> GetAccountsByNote(string note)
        {
            return unit.Account.GetAccountByNote(note);
        }

        public List<Account> GetAccountsByCredit(decimal fromAmount, decimal toAmount)
        {
            if (fromAmount == 0 && toAmount == 0)
            {
                return unit.Account.GetAccountByCredit();
            }
            return  unit.Account.GetAccountByCredit(fromAmount, toAmount);
        }

        public List<Account> GetAccountsByDebt(decimal fromAmount, decimal toAmount)
        {
            if (fromAmount == 0 && toAmount == 0)
            {
                return unit.Account.GetAccountByDebt();
            }
            return unit.Account.GetAccountByDebt(fromAmount, toAmount);
        }

        public bool AddToAccountCredit(int accountId, decimal amount) 
        {
            if (unit.Account.AddToAccountCredit(accountId, amount))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddToAccountDebt(int accountId, decimal amount) 
        {
            if (unit.Account.AddToAccountDebt(accountId, amount))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeductFromAccountCredit(int accountId, decimal amount)
        {
            if (unit.Account.DeductFromAccountCredit(accountId, amount))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeductFromAccountDebt(int accountId, decimal amount) 
        {
            if (unit.Account.DeductFromAccountCredit(accountId, amount))
            {
                unit.Save();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
