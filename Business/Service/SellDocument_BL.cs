using System;
using System.Collections.Generic;
using VeiwModels;
using DataLayer.Contact;
using System.Globalization;

namespace Business
{
    public class SellDocument_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool Exist(string accountName, string date, string itemName, decimal price, int number, string payType)
        {
            return unit.SellDocument.Exist(accountName, itemName, date, number, price, payType);
        }

        private void UpdateItemPrice(int itemId, decimal price)
        {
            var item = new Item();
            item = unit.ItemGeneric.GetById(itemId);
            item.SellPrice = price;
            unit.Save();
        }

        public bool InsertSellDocument(SellDocument sellDocument)
        {
            UpdateItemPrice(sellDocument.ItemId, sellDocument.Price);
            if (unit.Item.DeductItemNumber(sellDocument.ItemId, sellDocument.Number))
            {
                Transaction transaction = new Transaction();
                transaction.AccountSideId = sellDocument.BuyerAccountId;
                transaction.AccountSideName = sellDocument.BuyerName;
                transaction.Description = $"فروش {sellDocument.ItemName}";
                transaction.Date = sellDocument.Date;
                transaction.Year = sellDocument.Year;
                transaction.Month = sellDocument.Month;
                transaction.Day = sellDocument.Day;
                transaction.Recived = sellDocument.TotalPrice;
                if (sellDocument.PayType == "نقدی")
                {
                    if (unit.TransactionGeneric.Insert(transaction))
                    {
                        sellDocument.TransactionId = transaction.ID;
                        if (unit.SellDocumentGeneric.Insert(sellDocument))
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else if (sellDocument.PayType == "نسیه")
                {
                    sellDocument.TransactionId = 0;
                    if (unit.SellDocumentGeneric.Insert(sellDocument))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public bool UpdateSellDocument( int oldDocumentId, SellDocument sellDocument)
        {
            var oldDocumnet = unit.SellDocumentGeneric.GetById(oldDocumentId);
            sellDocument.ID = oldDocumnet.ID;
            if (unit.Item.AddItemNumber(oldDocumnet.ItemId, oldDocumnet.Number))
            {
                if (oldDocumnet.PayType == "نقدی" && sellDocument.PayType == "نقدی")
                {
                    //update transaction
                    Transaction transaction = new Transaction();
                    #region Get Data
                    transaction = unit.TransactionGeneric.GetById(oldDocumnet.TransactionId);
                    transaction.AccountSideId = sellDocument.BuyerAccountId;
                    transaction.AccountSideName = sellDocument.BuyerName;
                    transaction.Recived = sellDocument.TotalPrice;
                    transaction.Date = sellDocument.Date;
                    transaction.Year = sellDocument.Year;
                    transaction.Month = sellDocument.Month;
                    transaction.Day = sellDocument.Day;
                    transaction.Description = $"فروش {sellDocument.ItemName}";
                    #endregion
                    if (unit.TransactionGeneric.Update(transaction, transaction.ID))
                    {
                        sellDocument.TransactionId = transaction.ID;
                        if (unit.SellDocumentGeneric.Update(sellDocument, sellDocument.ID))
                        {
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                else if (oldDocumnet.PayType == "نقدی" && sellDocument.PayType == "نسیه")
                {
                    //remove transaction
                    if (unit.TransactionGeneric.Delete(oldDocumnet.TransactionId))
                    {
                        //add new debt to buyer account
                        if ( unit.Account.AddToAccountDebt(sellDocument.BuyerAccountId, sellDocument.TotalPrice))
                        {
                            if (unit.SellDocumentGeneric.Update(sellDocument, sellDocument.ID))
                            {    
                                return true;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                else if (oldDocumnet.PayType == "نسیه" && sellDocument.PayType == "نسیه")
                {
                    //deduct old debt to buyer account
                    if (unit.Account.DeductFromAccountDebt(sellDocument.BuyerAccountId, sellDocument.TotalPrice))
                    {
                        //add new debt to buyer account
                        if (unit.Account.AddToAccountDebt(sellDocument.BuyerAccountId, sellDocument.TotalPrice))
                        {
                            if (unit.SellDocumentGeneric.Update(sellDocument, sellDocument.ID))
                            {
                                return true;
                            }
                            return false;
                        }
                        return false;
                    }
                    return false;
                }
                else if (oldDocumnet.PayType == "نسیه" && sellDocument.PayType == "نقدی")
                {
                    //deduct old debt from buyer account
                    if (unit.Account.DeductFromAccountDebt(oldDocumnet.BuyerAccountId, oldDocumnet.TotalPrice))
                    {
                        //insert transaction
                        Transaction transaction = new Transaction();
                        #region Get Data
                        transaction = unit.TransactionGeneric.GetById(oldDocumnet.TransactionId);
                        transaction.AccountSideId = sellDocument.BuyerAccountId;
                        transaction.AccountSideName = sellDocument.BuyerName;
                        transaction.Recived = sellDocument.TotalPrice;
                        transaction.Date = sellDocument.Date;
                        transaction.Year = sellDocument.Year;
                        transaction.Month = sellDocument.Month;
                        transaction.Day = sellDocument.Day;
                        transaction.Description = $"فروش {sellDocument.ItemName}";
                        #endregion
                        if (unit.TransactionGeneric.Insert(transaction))
                        {
                            sellDocument.TransactionId = transaction.ID;
                            if (unit.SellDocumentGeneric.Update(sellDocument, sellDocument.ID))
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

        public bool DeleteSellDocument(SellDocument sellDocument)
        {
            if (sellDocument.PayType == "نقدی")
            {
                //if exist transaction in document
                if (unit.TransactionGeneric.Delete(sellDocument.TransactionId))
                {
                    if (unit.SellDocumentGeneric.Delete(sellDocument))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            else if (sellDocument.PayType == "نسیه")
            {
                //if pay type == Credit
                if (sellDocument.BuyerAccountId != 0)
                {
                    unit.Account.DeductFromAccountDebt(sellDocument.BuyerAccountId, sellDocument.TotalPrice);
                }
                if (unit.SellDocumentGeneric.Delete(sellDocument))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool DeleteSellDocument(int sellDocumentId)
        {
            SellDocument sellDocument = new SellDocument();
            sellDocument = unit.SellDocumentGeneric.GetById(sellDocumentId);
            return DeleteSellDocument(sellDocument);
        }

        public SellDocument GetSellDocumentById(int sellDocumentId)
        {
            return unit.SellDocumentGeneric.GetById(sellDocumentId);
        }

        public List<SellDocument> GetSellDocumentsByItemName(List<SellDocument> filterByDateSellDocuments, string itemName)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByItemName( filterByDateSellDocuments, itemName);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByItemName(itemName);
            }
        }

        public List<SellDocument> GetSellDocumentsByItemNumber(List<SellDocument> filterByDateSellDocuments, int fromNumber, int toNumber)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByItemNumber(filterByDateSellDocuments, fromNumber, toNumber);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByItemNumber(fromNumber, toNumber);
            }
        }

        public List<SellDocument> GetSellDocumentsByPrice(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByPrice(filterByDateSellDocuments, fromAmount, toAmount);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByPrice(fromAmount, toAmount);
            }
        }

        public List<SellDocument> GetSellDocumentsByCosts(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByCosts(filterByDateSellDocuments, fromAmount, toAmount);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByCosts(fromAmount, toAmount);
            }
        }

        public List<SellDocument> GetSellDocumentsByTotalPrice(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByTotalPrice(filterByDateSellDocuments, fromAmount, toAmount);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByTotalPrice(fromAmount, toAmount);
            }
        }

        public List<SellDocument> GetSellDocumentsByPayType(List<SellDocument> filterByDateSellDocuments, string payType)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByPayType(filterByDateSellDocuments, payType);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByPayType(payType);
            }
        }

        public List<SellDocument> GetSellDocumentsByBuyerName(List<SellDocument> filterByDateSellDocuments, string buyerName)
        {
            if (filterByDateSellDocuments != null)
            {
                return unit.SellDocument.GetSellDocumentsByBuyerName( filterByDateSellDocuments, buyerName);
            }
            else
            {
                return unit.SellDocument.GetSellDocumentsByBuyerName(buyerName);
            }
        }

        public List<SellDocument> GetSellDocumentsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int _year = persianCalendar.GetYear(DateTime.Now);
            int _month = persianCalendar.GetMonth(DateTime.Now);
            if (fromYear != 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.SellDocument.GetSellDocumentsByDate(fromYear, upToYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.SellDocument.GetSellDocumentsByDay(_year, _month, fromDay, upToDay);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.SellDocument.GetSellDocumentsByMonth(_year, fromMonth, upToMonth);
            }
            else if (fromYear == 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.SellDocument.GetSellDocumentsByMonth_Day(_year, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay == 0)
            {
                return unit.SellDocument.GetSellDocumentsByYear(fromYear, upToYear);
            }
            else if (fromYear != 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.SellDocument.GetSellDocumentsByYear_Month(fromYear, upToYear, fromMonth, upToMonth);
            }
            else if (fromYear != 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.SellDocument.GetSellDocumentsByYear(fromYear, upToYear);
            }
            else
            {
                return null;
            }
        }

        public List<SellDocument> GetSellDocumentsByName(string name)
        {
            return unit.SellDocument.GetSellDocumentsByName(name);
        }
    }
}