using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface IBuyDocument_DL
    {
        bool Exist(string accountName, string date, string itemName, decimal price, int number, string payType);

        List<BuyDocument> GetBuyDocumentsByPrice(decimal fromAmount, decimal toAmount);

        List<BuyDocument> GetBuyDocumentsByItemNumber(int fromNumber, int toNumber);

        List<BuyDocument> GetBuyDocumentsBySellerName(string sellerName);

        List<BuyDocument> GetBuyDocumentsByName(string name);

        List<BuyDocument> GetBuyDocumentsBySellerName(List<BuyDocument> filterByDateBuyDocuments, string sellerName);

        List<BuyDocument> GetBuyDocumentsByCosts(decimal fromAmount, decimal toAmount);

        List<BuyDocument> GetBuyDocumentsByTotalPrice(decimal fromAmount, decimal toAmount);

        List<BuyDocument> GetBuyDocumentsByPayType(string payType);

        List<BuyDocument> GetBuyDocumentsByItemName(string itemName);

        List<BuyDocument> GetBuyDocumentsByPrice(List<BuyDocument> filterByDateBuyDocuments, decimal fromAmount, decimal toAmount);

        List<BuyDocument> GetBuyDocumentsByItemNumber(List<BuyDocument> filterByDateBuyDocuments, int fromNumber, int toNumber);

        List<BuyDocument> GetBuyDocumentsByCosts(List<BuyDocument> filterByDateBuyDocuments, decimal fromAmount, decimal toAmount);

        List<BuyDocument> GetBuyDocumentsByTotalPrice(List<BuyDocument> filterByDateBuyDocuments, decimal fromAmount, decimal toAmount);

        List<BuyDocument> GetBuyDocumentsByPayType(List<BuyDocument> filterByDateBuyDocuments, string payType);

        List<BuyDocument> GetBuyDocumentsByItemName(List<BuyDocument> filterByDateBuyDocuments, string itemName);

        List<BuyDocument> GetBuyDocumentsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<BuyDocument> GetBuyDocumentsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth);

        List<BuyDocument> GetBuyDocumentsByYear(int fromYear, int upToYear);

        List<BuyDocument> GetBuyDocumentsByMonth(int currentYear, int fromMonth, int upToMonth);

        List<BuyDocument> GetBuyDocumentsByDay(int currentYear, int currentMonth, int fromDay, int upToDay);

        List<BuyDocument> GetBuyDocumentsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        decimal GetBuyDocumentsTotalPrice(List<BuyDocument> filterByDateBuyDocuments);
    }
}
