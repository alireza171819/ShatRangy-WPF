using System.Collections.Generic;
using System.Linq;
using DataLayer.Repositories;
using VeiwModels;

namespace DataLayer.Service
{
    class BuyDocument_DL : IBuyDocument_DL
    {
        private ShatRangyContext context;
        public BuyDocument_DL(ShatRangyContext context)
        {
            this.context = context;
        }

        public bool Exist(string accountName, string date, string itemName, decimal price, int number, string payType)
        {
            var tatgetObject = context.buyDocuments.Where(i=>
            i.SellerName == accountName &&
            i.Date == date &&
            i.ItemName == itemName &&
            i.Price == price &&
            i.Number == number &&
            i.PayType == payType).ToList();
            if (tatgetObject.Count >= 1)
            {
                return true;
            }
            return false;
        }

        public List<BuyDocument> GetBuyDocumentsBySellerName(string sellerName)
        {
            return context.buyDocuments
                .Where(i => i.SellerName.Contains(sellerName)).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByItemName(string itemName)
        {
            return context.buyDocuments
               .Where(i => i.ItemName.Contains(itemName)).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByPrice(decimal fromAmount, decimal toAmount)
        {
            return context.buyDocuments
               .Where(i => i.Price >= fromAmount && i.Price <= toAmount).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.buyDocuments
                .Where(i => i.Year >= fromYear && i.Year <= upToYear 
                && i.Month >= fromMonth && i.Month <= upToMonth
                && i.Day >= fromDay && i.Day >= upToDay).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByDay(int currentYear, int currentMonth, int fromDay, int upToDay)
        {
            return context.buyDocuments
                .Where(i => i.Year == currentYear 
                && i.Month == currentMonth
                && i.Day >= fromDay && i.Day >= upToDay).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByMonth(int currentYear, int fromMonth, int upToMonth)
        {
            return context.buyDocuments
                .Where(i => i.Year == currentYear 
                && i.Month >= fromMonth 
                && i.Month <= upToMonth).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay)
        {
            return context.buyDocuments
                .Where(i => i.Year == currentYear
                && i.Month >= fromMonth
                && i.Month <= upToMonth
                && i.Day >= fromDay
                && i.Day >= upToDay).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByYear(int fromYear, int upToYear)
        {
            return context.buyDocuments
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth)
        {
            return context.buyDocuments
                .Where(i => i.Year >= fromYear 
                && i.Year <= upToYear 
                && i.Month >= fromMonth
                && i.Month <= upToMonth).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByItemNumber(int fromNumber , int toNumber)
        {
            return context.buyDocuments
               .Where(i => i.Number >= fromNumber && i.Number <= toNumber).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByCosts(decimal fromAmount, decimal toAmount)
        {
            return context.buyDocuments
               .Where(i => i.Costs >= fromAmount && i.Costs <= toAmount).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByTotalPrice(decimal fromAmount, decimal toAmount)
        {
            return context.buyDocuments
               .Where(i => i.TotalPrice >= fromAmount && i.TotalPrice <= toAmount).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByPayType(string payType)
        {
            return context.buyDocuments
               .Where(i => i.PayType == payType).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByPrice(List<BuyDocument> filterByDateBuyDocuments, decimal fromAmount, decimal toAmount)
        {
            return filterByDateBuyDocuments.Where(i => i.Price >= fromAmount && i.Price <= toAmount).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByItemNumber(List<BuyDocument> filterByDateBuyDocuments, int fromNumber, int toNumber)
        {
            return filterByDateBuyDocuments.Where(i => i.Number >= fromNumber && i.Number <= toNumber).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByCosts(List<BuyDocument> filterByDateBuyDocuments, decimal fromAmount, decimal toAmount)
        {
            return filterByDateBuyDocuments.Where(i => i.Costs >= fromAmount && i.Costs <= toAmount).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByTotalPrice(List<BuyDocument> filterByDateBuyDocuments, decimal fromAmount, decimal toAmount)
        {
            return filterByDateBuyDocuments.Where(i => i.TotalPrice >= fromAmount && i.TotalPrice <= toAmount).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByPayType(List<BuyDocument> filterByDateBuyDocuments, string payType)
        {
            return filterByDateBuyDocuments.Where(i => i.PayType == payType).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByItemName(List<BuyDocument> filterByDateBuyDocuments, string itemName)
        {
            return filterByDateBuyDocuments.Where(i => i.ItemName.Contains(itemName)).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsBySellerName(List<BuyDocument> filterByDateBuyDocuments, string sellerName)
        {
            return filterByDateBuyDocuments.Where(i => i.SellerName.Contains(sellerName)).ToList();
        }

        public List<BuyDocument> GetBuyDocumentsByName(string name)
        {
            return context.buyDocuments.Where(i => i.SellerName.Contains(name) || i.ItemName.Contains(name)).ToList();
        }

        public decimal GetBuyDocumentsTotalPrice(List<BuyDocument> filterByDateBuyDocuments)
        {
            return filterByDateBuyDocuments.Where(i => i.TransactionId > 0).Sum(i => i.TotalPrice);
        }
    }
}
