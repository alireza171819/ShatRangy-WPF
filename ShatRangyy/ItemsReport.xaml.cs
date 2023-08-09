using Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VeiwModals;

namespace ShatRangyy
{
    public partial class ItemsReport : UserControl
    {
        public ItemsReport()
        {
            InitializeComponent();
        }
        #region Varibles And Objects
        Item_BL Item_BL = new Item_BL();
        PersianCalendar PersianCalendar = new PersianCalendar();
       
        string ItemName;
        int SearchFromYear, SearchToYear, SearchFromMonth, SearchToMonth, SearchFromDay, SearchToDay;
        /// <summary>
        /// regex that matches disallowed text
        /// </summary>
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        #endregion

        #region Function

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        public void GetParameters()
        {
            if (!String.IsNullOrEmpty(txtSearchToDay.Text))
            {
                SearchToDay = int.Parse(txtSearchToDay.Text);
            }
            else
            {
                SearchToDay = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToMonth.Text))
            {
                SearchToMonth = int.Parse(txtSearchToMonth.Text);
            }
            else
            {
                SearchToMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchToYear.Text))
            {
                SearchToYear = int.Parse(txtSearchToYear.Text);
            }
            else
            {
                SearchToYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromYear.Text))
            {
                SearchFromYear = int.Parse(txtSearchFromYear.Text);
            }
            else
            {
                SearchFromYear = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromMonth.Text))
            {
                SearchFromMonth = int.Parse(txtSearchFromMonth.Text);
            }
            else
            {
                SearchFromMonth = 0;
            }
            if (!String.IsNullOrEmpty(txtSearchFromDay.Text))
            {
                SearchFromDay = int.Parse(txtSearchFromDay.Text);
            }
            else
            {
                SearchFromDay = 0;
            }
            ItemName = txtSearch.Text;
        }
        public void Close()
        {
            (this.Parent as Grid).Children.Remove(this);
        }
        public void FilterDataGrid(string itemName, int fromYear, int toYear, int fromMonth, int toMonth
            , int fromDay, int toDay)
        {
            List<ItemProfit> itemsProfit = new List<ItemProfit>();
            if (!String.IsNullOrEmpty(itemName))
            {
                var _Items = Item_BL.GetItemsByName(null, itemName);
                for (int i = 0; i < _Items.Count; i++)
                {
                    ItemProfit ItemProfit = new ItemProfit();
                    ItemProfit.ID = _Items[i].ID;
                    ItemProfit.Date = _Items[i].Date;
                    ItemProfit.ItemName = _Items[i].ItemName;
                    ItemProfit.ProductionCost = _Items[i].ProductionCost;
                    ItemProfit.SellPrice = _Items[i].SellPrice;
                    ItemProfit.ProfitItem = Item_BL.GetItemProfit(_Items[i].ItemName, fromYear, toYear
                        , fromMonth, toMonth, fromDay, toDay);
                    itemsProfit.Add(ItemProfit);
                }
                DGV.ItemsSource = itemsProfit;
            }
            else
            {
                var _Items = Item_BL.GetAllItems();
                for (int i = 0; i < _Items.Count; i++)
                {
                    ItemProfit ItemProfit = new ItemProfit();
                    ItemProfit.ID = _Items[i].ID;
                    ItemProfit.Date = _Items[i].Date;
                    ItemProfit.ItemName = _Items[i].ItemName;
                    ItemProfit.ProductionCost = _Items[i].ProductionCost;
                    ItemProfit.SellPrice = _Items[i].SellPrice;
                    ItemProfit.ProfitItem = Item_BL.GetItemProfit(_Items[i].ItemName, fromYear, toYear
                        , fromMonth, toMonth, fromDay, toDay);
                    itemsProfit.Add(ItemProfit);
                }
                DGV.ItemsSource = itemsProfit;
            }
        }
        public void _ShowMessage(string message, MessageBox_.enumType type)
        {
            MessageBox_ messageBox_ = new MessageBox_();
            messageBox_.ShowMessage(message, type);
        }

        #endregion

        #region Events

        #region ---KeyDown---

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtSearchToDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
                , SearchToMonth, SearchFromDay, SearchToDay);
            }
        }

        private void txtSearchToMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
                , SearchToMonth, SearchFromDay, SearchToDay);
            }
        }

        private void txtSearchToYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
                , SearchToMonth, SearchFromDay, SearchToDay);
            }
        }

        private void txtSearchFromDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
                , SearchToMonth, SearchFromDay, SearchToDay);
            }
        }

        private void txtSearchFromMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
                , SearchToMonth, SearchFromDay, SearchToDay);
            }
        }

        private void txtSearchFromYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetParameters();
                FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
                , SearchToMonth, SearchFromDay, SearchToDay);
            }
        }
        #endregion

        #region ---Click---

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region ---Preview Text Input---

        private void AllTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        #endregion

        #region --- User Control---

        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtSearchFromYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            txtSearchToYear.Text = PersianCalendar.GetYear(DateTime.Now).ToString();
            GetParameters();
            FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
            , SearchToMonth, SearchFromDay, SearchToDay);
        }

        #endregion

        #region ---Text Change---

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetParameters();
            FilterDataGrid(ItemName, SearchFromYear, SearchToYear, SearchFromMonth
            , SearchToMonth, SearchFromDay, SearchToDay);
        }

        #endregion

        #endregion
    }
}
