using System.ComponentModel.DataAnnotations;

namespace VeiwModels
{
    public class Item
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(150)]
        public string ItemName { get; set; }
        /// <summary>
        /// Item price for selling
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// Item final price for owner
        /// </summary>
        public decimal ProductionCost { get; set; }
        /// <summary>
        /// Item number in inventory
        /// </summary>
        public int Number { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public string Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

    }
}
