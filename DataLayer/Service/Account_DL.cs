using System;
using System.Collections.Generic;
using System.Linq;
using VeiwModels;
using DataLayer.Repositories;

namespace DataLayer.Service
{
    public class Account_DL : IAccount_DL
    {
        private ShatRangyContext context;
        public Account_DL(ShatRangyContext _context)
        {
            context = _context;
        }

        public bool AddToAccountCredit(int accountId, decimal amount)
        {
            try
            {
                var targetObject = context.accounts.Find(accountId);
                targetObject.Credit += amount;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddToAccountDebt(int accountId, decimal amount)
        {
            try
            {
                var targetObject = context.accounts.Find(accountId);
                targetObject.Debt += amount;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeductFromAccountCredit(int accountId, decimal amount)
        {
            try
            {
                var targetObject = context.accounts.Find(accountId);
                targetObject.Credit -= amount;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeductFromAccountDebt(int accountId, decimal amount)
        {
            try
            {
                var targetObject = context.accounts.Find(accountId);
                targetObject.Debt -= amount;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Exist(string accountName)
        {
            var targetObjects = context.accounts.Where(i => i.AccountName == accountName);
            if (targetObjects != null)
            {
                return true;
            }
            return false;
        }

        public List<Account> GetAccountByAddress(string address)
        {
            try
            {
                return context.accounts.Where(i => i.Address.Contains(address)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByCredit()
        {
            try
            {
                return context.accounts.Where(i => i.Credit > 0).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByCredit(decimal fromAmount, decimal toAmount)
        {
            try
            {
                return context.accounts.Where(i => i.Credit >= fromAmount && i.Credit <= toAmount).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByDebt()
        {
            try
            {
                return context.accounts.Where(i => i.Debt > 0).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByDebt(decimal fromAmount, decimal toAmount)
        {
            try
            {
                return context.accounts.Where(i => i.Debt >= fromAmount && i.Debt <= toAmount).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByGroup(string accountGrop)
        {
            try
            {
                return context.accounts.Where(i => i.GroupName == accountGrop).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountsByName(string accountName)
        {
            try
            {
                return context.accounts.Where(i => i.AccountName.Contains(accountName)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Account GetAccountByName(string accountName)
        {
            try
            {
                return context.accounts.Where(i => i.AccountName == accountName).Single();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByNote(string note)
        {
            try
            {
                return context.accounts.Where(i => i.Note.Contains(note)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAccountByPhoneNumber(string phoneNumber)
        {
            try
            {
                return context.accounts.Where(i => i.PhoneNumber.Contains(phoneNumber)).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Account> GetAll()
        {
            return context.accounts.ToList();
        }
    }
}
 