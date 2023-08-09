using System;
using System.Collections.Generic;
using System.Globalization;
using Business.Service;
using DataLayer.Contact;
using VeiwModels;

namespace Business
{
    public class Item_BL
    {
        private UnitOfWork unit = new UnitOfWork();

        public bool InsertItem(Item item, bool saveBuyDocument)
        {
            if (saveBuyDocument)
            {
                if (unit.ItemGeneric.Insert(item))
                {
                    BuyDocument buyDocument = new BuyDocument();
                    #region Get Data
                    buyDocument.SellerName = String.Empty;
                    buyDocument.ItemName = item.ItemName;
                    buyDocument.Number = item.Number;
                    buyDocument.Price = item.ProductionCost;
                    buyDocument.Costs = 0;
                    buyDocument.TotalPrice = item.ProductionCost * item.Number;
                    buyDocument.Year = item.Year;
                    buyDocument.Month = item.Month;
                    buyDocument.Day = item.Day;
                    buyDocument.Date = item.Date;
                    buyDocument.PayType = "نقدی";
                    buyDocument.SellerAccountId = 0;
                    #endregion
                    Transaction_BL transaction_BL = new Transaction_BL();
                    Transaction transaction = new Transaction();
                    #region Get Date
                    transaction.AccountSideId = 0;
                    transaction.AccountSideName = String.Empty;
                    transaction.Date = item.Date;
                    transaction.Year = item.Year;
                    transaction.Month = item.Month;
                    transaction.Day = item.Day;
                    transaction.Payment = item.ProductionCost * item.Number;
                    transaction.Description = $"خرید {item.ItemName}";
                    #endregion
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
                return false;
            }
            else
            {
                if (unit.ItemGeneric.Insert(item))
                {
                    return true;
                }
                return false;
            }
        }

        public bool UpdateItem(Item item)
        {
            if (unit.ItemGeneric.Update(item, item.ID))
            {
                return true;
            }
            return false;
        }

        public bool DeleteItem(Item item)
        {
            if (unit.ItemGeneric.Delete(item))
            {
                return true;
            }
            return false;
        }

        public bool DeleteItem(int itemId)
        {
            if (unit.ItemGeneric.Delete(itemId))
            {
                return true;
            }
            return false;
        }

        public bool ExistItem(string itemName)
        {
            return unit.Item.Exist(itemName);
        }

        public Item GetItemById(int itemId)
        {
            return unit.ItemGeneric.GetById(itemId);
        }

        public Item GetItemByName(string itemName)
        {
            return unit.Item.GetItemByName(itemName);
        }

        public List<Item> GetItems()
        {
            return unit.Item.GetItems();
        }

        public List<Item> GetAllItems()
        {
            return unit.Item.GetAllItems();
        }

        public List<Item> GetItemsByDescription(List<Item> filterItemsByDate, string description)
        {
            if (filterItemsByDate != null)
            {
                return unit.Item.GetItemsByDescription(filterItemsByDate, description);
            }
            else
            {
                return unit.Item.GetItemsByDescription(description);
            }
        }

        public List<Item> GetItemsByName(List<Item> filterItemsByDate, string itemName)
        {
            if (filterItemsByDate != null)
            {
                return unit.Item.GetItemsByName(filterItemsByDate, itemName);
            }
            else
            {
                return unit.Item.GetItemsByName(itemName);
            }
        }

        public List<Item> GetItemsByNameOrDescription(string input)
        {
            return unit.Item.GetItemsByNameOrDescription(input);
        }

        public List<Item> GetItemsByNumber(List<Item> filterItemsByDate, int fromNumber, int toNumber)
        {
            if (filterItemsByDate != null)
            {
                return unit.Item.GetItemsByNumber(filterItemsByDate, fromNumber, toNumber);
            }
            else
            {
                return unit.Item.GetItemsByNumber(fromNumber, toNumber);
            }
        }

        public List<Item> GetItemsBySellPrice(List<Item> filterItemsByDate, decimal fromPrice, decimal toPrice)
        {
            if (filterItemsByDate != null)
            {
                return unit.Item.GetItemsBySellPrice(filterItemsByDate, fromPrice, toPrice);
            }
            else
            {
                return unit.Item.GetItemsBySellPrice(fromPrice, toPrice);
            }
        }

        public List<Item> GetItemsByCost(List<Item> filterItemsByDate, decimal fromPrice, decimal toPrice)
        {
            if (filterItemsByDate != null)
            {
                return unit.Item.GetItemsByProductionCost(filterItemsByDate, fromPrice, toPrice);
            }
            else
            {
                return unit.Item.GetItemsByProductionCost(fromPrice, toPrice);
            } 
        }

        public bool DeductItemNumber(int idItem, int number)
        {
            if (unit.Item.DeductItemNumber(idItem, number))
            {
                unit.Save();
                return true;
            }
            return false;
        }

        public bool AddItemNumber(int idItem, int number)
        {
            if (unit.Item.AddItemNumber(idItem, number))
            {
                unit.Save();
                return true;
            }
            return false;
        }

        public List<Item> GetItemsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            int _year = persianCalendar.GetYear(DateTime.Now);
            int _month = persianCalendar.GetMonth(DateTime.Now);
            if (fromYear != 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Item.GetItemsByDate(fromYear, upToYear, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if(fromYear == 0 && fromMonth == 0 && fromDay != 0)
            {
                return unit.Item.GetItemsByDay(_year, _month, fromDay, upToDay);
            }
            else if(fromYear == 0 && fromMonth != 0 && fromDay != 0)
            {
                return unit.Item.GetItemsByMonth_Day(_year, fromMonth, upToMonth, fromDay, upToDay);
            }
            else if(fromYear == 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Item.GetItemsByMonth(_year, fromMonth, upToMonth);
            }
            else if(fromYear != 0 && fromMonth == 0 && fromDay == 0)
            {
                return unit.Item.GetItemsByYear(fromYear, upToYear);
            }
            else if(fromYear != 0 && fromMonth != 0 && fromDay == 0)
            {
                return unit.Item.GetItemsByYear_Month(fromYear, upToYear, fromMonth, upToMonth);
            }
            else
            {
                return null;
            }
        }

        public decimal GetItemProfit(string itemName, int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            SellDocument_BL sellDocument_BL = new SellDocument_BL();
            SerVice_BL service_BL = new SerVice_BL();
            decimal Income, Cost;
            List<SellDocument> sellDocList = sellDocument_BL.GetSellDocumentsByDate(fromYear, upToYear, fromMonth, upToMonth
                , fromDay, upToDay);
            List<SerVice> serviceList = service_BL.GetServicesByStartDate(fromYear, upToYear, fromMonth, upToMonth
                , fromDay, upToDay);
            Income = unit.Item.GetItemSoledOutIncome(sellDocList, serviceList, itemName);
            Cost = unit.Item.GetItemCost(sellDocList, serviceList, itemName);
            return Income - Cost;
        }
    }
}
