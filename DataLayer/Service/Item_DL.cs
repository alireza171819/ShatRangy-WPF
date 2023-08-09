using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Repositories;
using VeiwModels;

namespace DataLayer.Service
{
    class Item_DL : IItem_DL
    {
        private ShatRangyContext context;
        public Item_DL(ShatRangyContext context)
        {
            this.context = context;
        }

        public Item GetItemByName(string itemName)
        {
            try
            {
                return context.items.Where(i => i.ItemName == itemName).Single();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public List<Item> GetItemsByDescription(string description)
        {
            return context.items.Where(i => i.Description.Contains(description)).ToList();
        }

        public List<Item> GetItemsByName(string itemName)
        {
            return context.items.Where(i => i.ItemName.Contains(itemName)).ToList();
        }

        public List<Item> GetItemsBySellPrice(decimal fromPrice, decimal toPrice)
        {
            return context.items
                .Where(i => i.SellPrice >= fromPrice
                && i.SellPrice <= toPrice).ToList();
        }

        public List<Item> GetItemsByProductionCost(decimal fromPrice, decimal toPrice)
        {
            return context.items
                .Where(i => i.ProductionCost >= fromPrice 
                && i.ProductionCost <= toPrice).ToList();
        }

        public bool DeductItemNumber(int itemId, int number)
        {
            try
            {
                var targetObject = context.items.SingleOrDefault(i => i.ID == itemId);
                if (targetObject != null)
                {
                    targetObject.Number -= number;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddItemNumber(int itemId, int number)
        {
            try
            {
                var targetObject = context.items.SingleOrDefault(i => i.ID == itemId);
                if (targetObject != null)
                {
                    targetObject.Number += number;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Item> GetItemsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.items
                .Where(i => i.Year >= fromYear
                && i.Year <= upToYear
                && i.Month >= fromMonth
                && i.Month <= upToMonth
                && i.Day >= fromDay
                && i.Day <= upToDay).ToList();
        }

        public List<Item> GetItemsByDay(int currentYear, int currentMonth, int fromDay, int upToDay)
        {
            return context.items
                .Where(i => i.Year == currentYear
                && i.Month == currentMonth
                && i.Day >= fromDay
                && i.Day <= upToDay).ToList();
        }

        public List<Item> GetItemsByMonth(int currentYear, int fromMonth, int upToMonth)
        {
            return context.items
                .Where(i => i.Year == currentYear
                && i.Month >= fromMonth
                && i.Month <= upToMonth).ToList();
        }

        public List<Item> GetItemsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.items
                .Where(i => i.Year == currentYear
                && i.Month >= fromMonth
                && i.Month <= upToMonth
                && i.Day >= fromDay
                && i.Day <= upToDay).ToList();
        }

        public List<Item> GetItemsByYear(int fromYear, int upToYear)
        {
            return context.items
                .Where(i => i.Year >= fromYear
                && i.Year <= upToYear).ToList();
        }

        public List<Item> GetItemsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth)
        {
            return context.items
                .Where(i => i.Year >= fromYear
                && i.Year <= upToYear
                && i.Month >= fromMonth
                && i.Month <= upToMonth).ToList();
        }

        public bool Exist(string itemName)
        {
            var targetObject = context.items.Where(i => i.ItemName == itemName).ToList();
            if (targetObject.Count > 1)
            {
                return true;
            }
            return false;
        }

        public List<Item> GetItemsByNumber(int fromNumber, int toNumber)
        {
            return context.items
                 .Where(i => i.Number >= fromNumber
                 && i.Number <= toNumber).ToList();
        }

        public List<Item> GetItems()
        {
            return context.items.Where(i => i.Number > 0).ToList();
        }

        public List<Item> GetAllItems()
        {
            return context.items.ToList();
        }

        public List<Item> GetItemsByName(List<Item> filterItemsByDate, string itemName)
        {
            return filterItemsByDate.Where(i => i.ItemName.Contains(itemName)).ToList();
        }

        public List<Item> GetItemsBySellPrice(List<Item> filterItemsByDate, decimal fromPrice, decimal toPrice)
        {
            return filterItemsByDate.Where(i => i.SellPrice >= fromPrice
                && i.SellPrice <= toPrice).ToList();
        }

        public List<Item> GetItemsByProductionCost(List<Item> filterItemsByDate, decimal fromPrice, decimal toPrice)
        {
            return filterItemsByDate.Where(i => i.ProductionCost >= fromPrice
                && i.ProductionCost <= toPrice).ToList();
        }

        public List<Item> GetItemsByDescription(List<Item> filterItemsByDate, string description)
        {
            return filterItemsByDate.Where(i => i.Description.Contains(description)).ToList();
        }

        public List<Item> GetItemsByNumber(List<Item> filterItemsByDate, int fromNumber, int toNumber)
        {
            return filterItemsByDate.Where(i => i.Number >= fromNumber
                 && i.Number <= toNumber).ToList();
        }

        public decimal GetItemCost(List<SellDocument> filterSellDocumentsByDate, List<SerVice> filterByDateDocuments, string itemName)
        {
            try
            {
                var item = context.items.Where(i => i.ItemName == itemName).SingleOrDefault();
                int number1 = filterSellDocumentsByDate.Where(i => i.ItemName == itemName).Sum(i => i.Number);
                int number2 = 0;
                var _list = filterByDateDocuments.Where(i => i.ItemName == itemName).ToList();
                for (int i = 0; i < _list.Count; i++)
                {
                    number2 += 1;
                }
                if (number1 != 0 && number2 == 0)
                {
                    return number1 * item.ProductionCost;
                }
                else if (number1 == 0 && number2 != 0)
                {
                    return number2 * item.ProductionCost;
                }
                else if (number1 != 0 && number2 != 0)
                {
                    return (number1 + number2) * item.ProductionCost;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public decimal GetItemSoledOutIncome(List<SellDocument> filterSellDocumentsByDate, List<SerVice> filterByDateDocuments, string itemName)
        {
            try
            {
                var _list1 = filterSellDocumentsByDate.Where(i => i.ItemName == itemName);
                var _list2 = filterByDateDocuments.Where(i => i.ItemName == itemName);
                if (_list1 != null && _list2 == null)
                {
                    return  _list1.Sum(i => i.TotalPrice);
                }
                else if (_list2 != null && _list1 == null) 
                {
                    return  _list2.Sum(i => i.Comision);
                }
                else if (_list2 != null && _list1 != null)
                {
                    return _list1.Sum(i => i.TotalPrice) + _list2.Sum(i => i.Comision);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public List<Item> GetItemsByNameOrDescription(string input)
        {
            return context.items.Where(i => i.ItemName.Contains(input) || i.Description.Contains(input)).ToList();
        }

    }
}
