using System.Collections.Generic;
using VeiwModels;
using DataLayer.Contact;
using System.Globalization;
using System;

namespace Business
{
    public class BuyDocument_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        private int SaveItem(Item _item)
        {
            if (unit.Item.GetItemByName(_item.ItemName) == null)
            {
                //Insert Item
                var item = new Item();
                item.ItemName = _item.ItemName;
                item.Date = _item.Date;
                item.ProductionCost = _item.ProductionCost;
                item.Number = _item.Number;
                item.Year = _item.Year;
                item.Month = _item.Month;
                item.Day = _item.Day;
                if (unit.ItemGeneric.Insert(item))
                {
                    return item.ID;
                }
                return 0;
            }
            else
            {
                //Update Item
                var item = new Item();
                item.Number += _item.Number;
                item.Year = _item.Year;
                item.Month = _item.Month;
                item.Day = _item.Day;
                item.Date = _item.Date;
                if (unit.ItemGeneric.Update(item, item.ID))
                {
                    return item.ID;
                }
                return 0;
            }
        }

        public bool Exist(string accountName, string date, string itemName, decimal price, int number, string payType)
        {
            return unit.BuyDocument.Exist(accountName, date, itemName, price, number, payType);
        }

        public bool InsertBuyDocument(BuyDocument buyDocument)
        {
            Item item = new Item();
            item.ItemName = buyDocument.ItemName;
            item.Date = buyDocument.Date;
            item.ProductionCost = buyDocument.Price;
            item.Number = buyDocument.Number;
            item.Year = buyDocument.Year;
            item.Month = buyDocument.Month;
            item.Day = buyDocument.Day;
            item.ID = SaveItem(item);
            if (item.ID != 0)
            {
                if (buyDocument.PayType == "نقدی")
                {
                    Transaction_BL transaction_BL = new Transaction_BL();
                    Transaction transaction = new Transaction();
                    transaction.AccountSideId = buyDocument.SellerAccountId;
                    transaction.AccountSideName = buyDocument.SellerName;
                    transaction.Date = buyDocument.Date;
                    transaction.Year = buyDocument.Year;
                    transaction.Month = buyDocument.Month;
                    transaction.Day = buyDocument.Day;
                    transaction.Payment = buyDocument.TotalPrice;
                    transaction.Description = $"خرید {buyDocument.ItemName}";
                    if (transaction_BL.InsertTransaction(transaction))
                    {
                        buyDocument.TransactionId = transaction.ID;
                        buyDocument.ItemId = item.ID;
                        if (unit.BuyDocumentGeneric.Insert(buyDocument))
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else if (buyDocument.PayType == "نسیه")
                {
                    buyDocument.TransactionId = 0;
                    buyDocument.ItemId = item.ID;
                    if (unit.BuyDocumentGeneric.Insert(buyDocument))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public bool UpdateBuyDocument(int id, BuyDocument buyDocument)
        {
            var oldDocumnet = unit.BuyDocumentGeneric.GetById(id);
            buyDocument.ID = id;
            Item item = new Item();
            #region Get Data
            item = unit.ItemGeneric.GetById(oldDocumnet.ItemId);
            item.Date = buyDocument.Date;
            item.Year = buyDocument.Year;
            item.Month = buyDocument.Month;
            item.Day = buyDocument.Day;
            item.ItemName = buyDocument.ItemName;
            item.Number = buyDocument.Number;
            item.ProductionCost = buyDocument.Price;
            #endregion
            if (unit.ItemGeneric.Update(item, item.ID))
            {
                if (oldDocumnet.PayType == "نقدی" && buyDocument.PayType == "نقدی")
                {
                    //update transaction
                    Transaction transaction = new Transaction();
                    #region Get Data
                    transaction = unit.TransactionGeneric.GetById(oldDocumnet.TransactionId);
                    transaction.AccountSideId = buyDocument.SellerAccountId;
                    transaction.AccountSideName = buyDocument.SellerName;
                    transaction.Payment = buyDocument.TotalPrice;
                    transaction.Date = buyDocument.Date;
                    transaction.Year = buyDocument.Year;
                    transaction.Month = buyDocument.Month;
                    transaction.Day = buyDocument.Day;
                    transaction.Description = $"خرید {buyDocument.ItemName}";
                    #endregion
                    if (unit.TransactionGeneric.Update(transaction , transaction.ID))
                    {
                        buyDocument.TransactionId = transaction.ID;
                        buyDocument.ItemId = item.ID;
                        if (unit.BuyDocumentGeneric.Update(buyDocument, id))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }   
                else if (oldDocumnet.PayType == "نقدی" && buyDocument.PayType == "نسیه")
                {
                    //remove transaction
                    if (unit.TransactionGeneric.Delete(oldDocumnet.TransactionId))
                    {
                        //add new credit to seller account
                        unit.Account.AddToAccountCredit(buyDocument.SellerAccountId, buyDocument.TotalPrice);
                        if (unit.BuyDocumentGeneric.Update(buyDocument, id))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                else if (oldDocumnet.PayType == "نسیه" && buyDocument.PayType == "نسیه")
                {
                    //deduct old credit to seller account
                    if (unit.Account.DeductFromAccountCredit(oldDocumnet.SellerAccountId, oldDocumnet.TotalPrice))
                    {
                        //add new credit to seller account
                        unit.Account.AddToAccountCredit(buyDocument.SellerAccountId, buyDocument.TotalPrice);
                        if (unit.BuyDocumentGeneric.Update(buyDocument, id))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                else if (oldDocumnet.PayType == "نسیه" && buyDocument.PayType == "نقدی")
                {
                    //deduct old credit to seller account
                    if (unit.Account.DeductFromAccountCredit(oldDocumnet.SellerAccountId, oldDocumnet.TotalPrice))
                    {
                        //insert transaction
                        Transaction transaction = new Transaction();
                        #region Get Data
                        transaction = unit.TransactionGeneric.GetById(oldDocumnet.TransactionId);
                        transaction.AccountSideId = buyDocument.SellerAccountId;
                        transaction.AccountSideName = buyDocument.SellerName;
                        transaction.Payment = buyDocument.TotalPrice;
                        transaction.Date = buyDocument.Date;
                        transaction.Year = buyDocument.Year;
                        transaction.Month = buyDocument.Month;
                        transaction.Day = buyDocument.Day;
                        transaction.Description = $"خرید {buyDocument.ItemName}";
                        #endregion
                        if (unit.TransactionGeneric.Insert(transaction))
                        {
                            buyDocument.TransactionId = transaction.ID;
                            if (unit.BuyDocumentGeneric.Update(buyDocument, id))
                            {
                                return true;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
            }
            return false;
        }

        public bool DeleteBuyDocument(BuyDocument buyDocument)
        {
            if (buyDocument.PayType == "نقدی")
            {
                //if exist transaction in document
                if (unit.TransactionGeneric.Delete(buyDocument.TransactionId))
                {
                    if (unit.BuyDocumentGeneric.Delete(buyDocument))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if(buyDocument.PayType == "نسیه")
            {
                //if pay type == Credit
                if (buyDocument.SellerAccountId != 0)
                {
                    unit.Account.DeductFromAccountCredit(buyDocument.SellerAccountId, buyDocument.TotalPrice);
                }
                if (unit.BuyDocumentGeneric.Delete(buyDocument))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteBuyDocument(int buyDocumentId)
        {
            BuyDocument document = new BuyDocument();
            document = GetBuyDocumentById(buyDocumentId);
            return DeleteBuyDocument(document);
        }

        public BuyDocument GetBuyDocumentById(int buyDocumentId)
        {
            return unit.BuyDocumentGeneric.GetById(buyDocumentId);
        }

        public List<BuyDocument> GetBuyDocumentsByItemNumber( List<BuyDocument> filterByDateBuyDocuments, int fromNumber, int toNumber)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsByItemNumber( filterByDateBuyDocuments, fromNumber, toNumber);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsByItemNumber(fromNumber, toNumber);
            }
        }

        public List<BuyDocument> GetBuyDocumentsByItemName( List<BuyDocument> filterByDateBuyDocuments, string itemName)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsByItemName( filterByDateBuyDocuments, itemName);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsByItemName(itemName);
            }
        }

        public List<BuyDocument> GetBuyDocumentsByPrice( List<BuyDocument> filterByDateBuyDocuments, decimal fromPrice, decimal toPrice)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsByPrice( filterByDateBuyDocuments, fromPrice, toPrice);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsByPrice( fromPrice, toPrice);
            }
        }

        public List<BuyDocument> GetBuyDocumentsByPayType( List<BuyDocument> filterByDateBuyDocuments, string payType)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsByPayType( filterByDateBuyDocuments, payType);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsByPayType(payType);
            }
        }

        public List<BuyDocument> GetBuyDocumentsByCosts( List<BuyDocument> filterByDateBuyDocuments, decimal fromCosts, decimal toCosts)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsByCosts( filterByDateBuyDocuments, fromCosts, toCosts);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsByCosts( fromCosts, toCosts);
            }
        }

        public List<BuyDocument> GetBuyDocumentsByTotalPrice( List<BuyDocument> filterByDateBuyDocuments, decimal fromTotalPrice, decimal toTotalPrice)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsByPrice( filterByDateBuyDocuments, fromTotalPrice, toTotalPrice);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsByPrice( fromTotalPrice, toTotalPrice);
            }
        }

        public List<BuyDocument> GetBuyDocumentsBySellerName(List<BuyDocument> filterByDateBuyDocuments, string buyerName)
        {
            if (filterByDateBuyDocuments != null)
            {
                return unit.BuyDocument.GetBuyDocumentsBySellerName(filterByDateBuyDocuments, buyerName);
            }
            else
            {
                return unit.BuyDocument.GetBuyDocumentsBySellerName(buyerName);
            }
        }

        public List<BuyDocument> GetBuyDocumentsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int _year = persianCalendar.GetYear(DateTime.Now);
            int _month = persianCalendar.GetMonth(DateTime.Now);
            if (fromYear != 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByDate(fromYear, upToYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByDay(_year, _month, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByMonth(_year, fromMonth, upToMonth);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByMonth_Day(_year, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay == 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByYear(fromYear, upToYear);
            }
            else if (fromYear != 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByYear_Month(fromYear, upToYear, fromMonth, upToMonth);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.BuyDocument.GetBuyDocumentsByYear(fromYear, upToYear);
            }
            else
            {
                return null;
            }
        }

        public List<BuyDocument> GetBuyDocumentsByName(string name)
        {
            return unit.BuyDocument.GetBuyDocumentsByName(name);
        }
    }
}
