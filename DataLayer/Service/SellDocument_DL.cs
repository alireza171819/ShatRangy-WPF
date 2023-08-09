using System.Collections.Generic;
using System.Linq;
using DataLayer.Repositories;
using VeiwModels;

namespace DataLayer.Service
{
    class SellDocument_DL : ISellDocument_DL    
    {
        private ShatRangyContext context;
        public SellDocument_DL(ShatRangyContext context)
        {
            this.context = context;
        }

        public bool Exist(string accountName, string itemName, string date, int number, decimal price, string payType)
        {
            var tagetObject = context.sellDocuments.Where(i =>
            i.BuyerName == accountName &&
            i.ItemName == itemName &&
            i.Date == date &&
            i.Number == number &&
            i.Price == price &&
            i.PayType == payType).ToList();
            if (tagetObject.Count > 1)
            {
                return true;
            }
            return false;
        }

        public List<SellDocument> GetSellDocumentsByBuyerName(string buyerName)
        {
            return context.sellDocuments
                .Where(i => i.BuyerName.Contains(buyerName)).ToList();
        }

        public List<SellDocument> GetSellDocumentsByItemNumber(int fromNumber, int toNumber)
        {
            return context.sellDocuments
               .Where(i => i.Number >= fromNumber && i.Number <= toNumber).ToList();
        }

        public List<SellDocument> GetSellDocumentsByPayType(string payType)
        {
            return context.sellDocuments
                .Where(i => i.PayType == payType).ToList();
        }

        public List<SellDocument> GetSellDocumentsByCosts(decimal fromAmount, decimal toAmount)
        {
            return context.sellDocuments
               .Where(i => i.Costs >= fromAmount && i.Costs <= toAmount).ToList();
        }

        public List<SellDocument> GetSellDocumentsByTotalPrice(decimal fromAmount, decimal toAmount)
        {
            return context.sellDocuments
               .Where(i => i.TotalPrice >= fromAmount && i.TotalPrice <= toAmount).ToList();
        }

        public List<SellDocument> GetSellDocumentsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.sellDocuments
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear 
                && i.Month >= fromMonth 
                && i.Month <= upToMonth 
                && i.Day >= fromDay 
                && i.Day <= upToDay).ToList();
        }

        public List<SellDocument> GetSellDocumentsByDay(int currentYear, int currentMonth, int fromDay, int upToDay)
        {
            return context.sellDocuments
                .Where(i => i.Year == currentYear 
                && i.Month == currentMonth
                && i.Day >= fromDay 
                && i.Day <= upToDay).ToList();
        }

        public List<SellDocument> GetSellDocumentsByItemName(string itemName)
        {
            return context.sellDocuments
                .Where(i => i.ItemName == itemName).ToList();
        }

        public List<SellDocument> GetSellDocumentsByMonth(int currentYear, int fromMonth, int upToMonth)
        {
            return context.sellDocuments
                .Where(i => i.Year == currentYear 
                && i.Month >= fromMonth 
                && i.Month <= upToMonth).ToList();
        }

        public List<SellDocument> GetSellDocumentsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.sellDocuments
                .Where(i => i.Year == currentYear
                && i.Month >= fromMonth 
                && i.Month <= upToMonth 
                && i.Day >= fromDay 
                && i.Day <= upToDay).ToList();
        }

        public List<SellDocument> GetSellDocumentsByYear(int fromYear, int upToYear)
        {
            return context.sellDocuments
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear).ToList();
        }

        public List<SellDocument> GetSellDocumentsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth)
        {
            return context.sellDocuments
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear 
                && i.Month >= fromMonth 
                && i.Month <= upToMonth).ToList();
        }

        public List<SellDocument> GetSellDocumentsByPrice(decimal fromAmount, decimal toAmount)
        {
            return context.sellDocuments
              .Where(i => i.Price >= fromAmount && i.Price <= toAmount).ToList();
        }

        public List<SellDocument> GetSellDocumentsByItemName(List<SellDocument> filterByDateSellDocuments, string itemName)
        {
            return filterByDateSellDocuments.Where(i => i.ItemName.Contains(itemName)).ToList();
        }

        public List<SellDocument> GetSellDocumentsByItemNumber(List<SellDocument> filterByDateSellDocuments, int fromNumber, int toNumber)
        {
            return filterByDateSellDocuments.Where(i => i.Number >= fromNumber && i.Number <= toNumber).ToList();
        }

        public List<SellDocument> GetSellDocumentsByPrice(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount)
        {
            return filterByDateSellDocuments.Where(i => i.Price >= fromAmount && i.Price <= toAmount).ToList();
        }

        public List<SellDocument> GetSellDocumentsByCosts(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount)
        {
            return filterByDateSellDocuments.Where(i => i.Costs >= fromAmount && i.Costs <= toAmount).ToList();
        }

        public List<SellDocument> GetSellDocumentsByTotalPrice(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount)
        {
            return filterByDateSellDocuments.Where(i => i.TotalPrice >= fromAmount && i.TotalPrice <= toAmount).ToList();
        }

        public List<SellDocument> GetSellDocumentsByPayType(List<SellDocument> filterByDateSellDocuments, string payType)
        {
            return filterByDateSellDocuments.Where(i => i.PayType == payType).ToList();
        }

        public List<SellDocument> GetSellDocumentsByBuyerName(List<SellDocument> filterByDateSellDocuments, string buyerName)
        {
            return filterByDateSellDocuments.Where(i => i.BuyerName.Contains(buyerName)).ToList();
        }

        public List<SellDocument> GetSellDocumentsByName(string name)
        {
            return context.sellDocuments.Where(i => i.BuyerName.Contains(name) || i.ItemName.Contains(name)).ToList();
        }

        public decimal GetSellDocumentsTotalPrice(List<SellDocument> filterByDateSellDocuments)
        {
            return filterByDateSellDocuments.Where(i => i.TransactionId > 0).Sum(i => i.TotalPrice);
        }
    }
}
