using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface ISellDocument_DL
    {
        bool Exist(string accountName, string itemName, string date, int number, decimal price, string payType);

        List<SellDocument> GetSellDocumentsByBuyerName(string buyerName);

        List<SellDocument> GetSellDocumentsByName(string name);

        List<SellDocument> GetSellDocumentsByBuyerName(List<SellDocument> filterByDateSellDocuments, string buyerName);

        List<SellDocument> GetSellDocumentsByItemName(string itemName);

        List<SellDocument> GetSellDocumentsByItemNumber(int fromNumber, int toNumber);

        List<SellDocument> GetSellDocumentsByPrice(decimal fromAmount, decimal toAmount);

        List<SellDocument> GetSellDocumentsByCosts(decimal fromAmount, decimal toAmount);

        List<SellDocument> GetSellDocumentsByTotalPrice(decimal fromAmount, decimal toAmount);

        List<SellDocument> GetSellDocumentsByPayType(string payType);

        List<SellDocument> GetSellDocumentsByItemName(List<SellDocument> filterByDateSellDocuments, string itemName);

        List<SellDocument> GetSellDocumentsByItemNumber(List<SellDocument> filterByDateSellDocuments, int fromNumber, int toNumber);

        List<SellDocument> GetSellDocumentsByPrice(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount);

        List<SellDocument> GetSellDocumentsByCosts(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount);

        List<SellDocument> GetSellDocumentsByTotalPrice(List<SellDocument> filterByDateSellDocuments, decimal fromAmount, decimal toAmount);

        List<SellDocument> GetSellDocumentsByPayType(List<SellDocument> filterByDateSellDocuments, string payType);

        List<SellDocument> GetSellDocumentsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<SellDocument> GetSellDocumentsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth);

        List<SellDocument> GetSellDocumentsByYear(int fromYear, int upToYear);

        List<SellDocument> GetSellDocumentsByMonth(int currentYear, int fromMonth, int upToMonth);

        List<SellDocument> GetSellDocumentsByDay(int currentYear, int currentMonth, int fromDay, int upToDay);

        List<SellDocument> GetSellDocumentsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        decimal GetSellDocumentsTotalPrice(List<SellDocument> filterByDateSellDocuments);
    }
}
