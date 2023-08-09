using System.Collections.Generic;
using VeiwModels;

namespace DataLayer.Repositories
{
    public interface IItem_DL
    {
        bool Exist(string itemName);

        Item GetItemByName(string itemName);

        List<Item> GetItems();

        List<Item> GetAllItems();

        List<Item> GetItemsByName(string itemName);

        List<Item> GetItemsByNameOrDescription(string input);

        List<Item> GetItemsByName(List<Item> filterItemsByDate, string itemName);

        List<Item> GetItemsBySellPrice(decimal fromPrice, decimal toPrice);

        List<Item> GetItemsBySellPrice(List<Item> filterItemsByDate, decimal fromPrice, decimal toPrice);

        List<Item> GetItemsByProductionCost(decimal fromPrice, decimal toPrice);

        List<Item> GetItemsByProductionCost(List<Item> filterItemsByDate, decimal fromPrice, decimal toPrice);

        List<Item> GetItemsByDescription(List<Item> filterItemsByDate, string description);

        List<Item> GetItemsByDescription(string description);

        List<Item> GetItemsByNumber(int fromNumber, int toNumber);

        List<Item> GetItemsByNumber(List<Item> filterItemsByDate, int fromNumber, int toNumber);

        bool DeductItemNumber(int itemId, int number);

        bool AddItemNumber(int itemId, int number);

        List<Item> GetItemsByDate(int fromYear, int upToYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        List<Item> GetItemsByYear_Month(int fromYear, int upToYear, int fromMonth, int upToMonth);

        List<Item> GetItemsByYear(int fromYear, int upToYear);

        List<Item> GetItemsByMonth(int currentYear, int fromMonth, int upToMonth);

        List<Item> GetItemsByDay(int currentYear, int currentMonth, int fromDay, int upToDay);

        List<Item> GetItemsByMonth_Day(int currentYear, int fromMonth, int upToMonth, int fromDay, int upToDay);

        decimal GetItemSoledOutIncome(List<SellDocument> filterSellDocumentsByDate, List<SerVice> filterByDateDocuments, string itemName);

        decimal GetItemCost(List<SellDocument> filterSellDocumentsByDate, List<SerVice> filterByDateDocuments, string itemName);
    }
}
