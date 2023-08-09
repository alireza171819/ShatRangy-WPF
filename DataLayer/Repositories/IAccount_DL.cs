using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface IAccount_DL
    {
        bool Exist(string accountName);
        bool AddToAccountCredit(int accountId, decimal amount);
        bool AddToAccountDebt(int accountId, decimal amount);
        bool DeductFromAccountCredit(int accountId, decimal amount);
        bool DeductFromAccountDebt(int accountId, decimal amount);
        List<Account> GetAccountsByName(string accountName);
        List<Account> GetAll();
        Account GetAccountByName(string accountName);
        List<Account> GetAccountByGroup(string accountGrop);
        List<Account> GetAccountByPhoneNumber(string phoneNumber);
        List<Account> GetAccountByAddress(string address);
        List<Account> GetAccountByNote(string note);
        List<Account> GetAccountByCredit();
        List<Account> GetAccountByDebt();
        List<Account> GetAccountByCredit(decimal fromAmount, decimal toAmount);
        List<Account> GetAccountByDebt(decimal fromAmount, decimal toAmount);
    }
}
