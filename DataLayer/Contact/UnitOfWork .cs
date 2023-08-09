using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DataLayer.Repositories;
using DataLayer.Service;
using VeiwModels;

namespace DataLayer.Contact
{
    public class UnitOfWork : IDisposable
    {
        ShatRangyContext context = new ShatRangyContext();

        private Generic<Transaction> _transactionGeneric;
        private Generic<Account> _accountGeneric;
        private Generic<AccountGroup> _accountGroupGeneric;
        private Generic<SellDocument> _sellDocumentGeneric;
        private Generic<BuyDocument> _buyDocumentGeneric;
        private Generic<SerVice> _serviceGeneric;
        private Generic<Setting> _settingGeneric;
        private Generic<Item> _itemGeneric;
        private Generic<User> _userGeneric;
        private ITransaction_DL _transaction;
        private IAccount_DL _account_DL;
        private IAccountGroup_DL _accountGroup_DL;
        private ISellDocument_DL _sellDocument;
        private IBuyDocument_DL _buyDocument;
        private ISerVice_DL _serVice_DL;
        private ISetting_DL _setting;
        private IItem_DL _item;
        private IProfitReport_DL _profitReport_DL;

        public Generic<Transaction> TransactionGeneric
        {
            get
            {
                if (_transactionGeneric == null)
                {
                    _transactionGeneric = new Generic<Transaction>(context);
                }
                return _transactionGeneric;
            }
        }

        public Generic<Account> AccountGeneric
        {
            get
            {
                if (_accountGeneric == null)
                {
                    _accountGeneric = new Generic<Account>(context);
                }
                return _accountGeneric;
            }
        }

        public Generic<AccountGroup> AccountGroupGeneric
        {
            get
            {
                if (_accountGroupGeneric == null)
                {
                    _accountGroupGeneric = new Generic<AccountGroup>(context);
                }
                return _accountGroupGeneric;
            }
        }

        public Generic<SellDocument> SellDocumentGeneric
        {
            get
            {
                if (_sellDocumentGeneric == null)
                {
                    _sellDocumentGeneric = new Generic<SellDocument>(context);
                }
                return _sellDocumentGeneric;
            }
        }

        public Generic<BuyDocument> BuyDocumentGeneric
        {
            get
            {
                if (_buyDocumentGeneric == null)
                {
                    _buyDocumentGeneric = new Generic<BuyDocument>(context);
                }
                return _buyDocumentGeneric;
            }
        }

        public Generic<SerVice> ServiceGeneric
        {
            get
            {
                if (_serviceGeneric == null)
                {
                    _serviceGeneric = new Generic<SerVice>(context);
                }
                return _serviceGeneric;
            }
        }

        public Generic<Item> ItemGeneric
        {
            get
            {
                if (_itemGeneric == null)
                {
                    _itemGeneric = new Generic<Item>(context);
                }
                return _itemGeneric;
            }
        }

        public Generic<Setting> SettingGeneric
        {
            get
            {
                if (_settingGeneric == null)
                {
                    _settingGeneric = new Generic<Setting>(context);
                }
                return _settingGeneric;
            }
        }

        public Generic<User> UserGeneric
        {
            get
            {
                if (_userGeneric == null)
                {
                    _userGeneric = new Generic<User>(context);
                }
                return _userGeneric;
            }
        }

        public ITransaction_DL Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = new Transaction_DL(context);
                }
                return _transaction;
            }
        }

        public IAccount_DL Account
        {
            get
            {
                if (_account_DL == null)
                {
                    _account_DL = new Account_DL(context);
                }
                return _account_DL;
            }
        }

        public IAccountGroup_DL AccountGroup
        {
            get
            {
                if (_accountGroup_DL == null)
                {
                    _accountGroup_DL = new AccountGroup_DL(context);
                }
                return _accountGroup_DL;
            }
        }

        public ISellDocument_DL SellDocument
        {
            get
            {
                if (_sellDocument == null)
                {
                    _sellDocument = new SellDocument_DL(context);
                }
                return _sellDocument;
            }
        }

        public IBuyDocument_DL BuyDocument
        {
            get
            {
                if (_buyDocument == null)
                {
                    _buyDocument = new BuyDocument_DL(context);
                }
                return _buyDocument;
            }
        }

        public ISerVice_DL Service
        {
            get
            {
                if (_serVice_DL == null)
                {
                    _serVice_DL = new SerVice_DL(context);
                }
                return _serVice_DL;
            }
        }

        public IItem_DL Item
        {
            get
            {
                if (_item == null)
                {
                    _item = new Item_DL(context);
                }
                return _item;
            }
        }

        public ISetting_DL Setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = new Setting_DL(context);
                }
                return _setting;
            }
        }

        public IProfitReport_DL ProfitReport
        {
            get
            {
                if (_profitReport_DL == null)
                {
                    _profitReport_DL = new ProfitReport_DL();
                }
                return _profitReport_DL;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
