using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeiwModels
{
    public class SellDocument
    {
        [Key]
        public int ID { get; set; }

       

        [MaxLength(150)]
        public string BuyerName { get; set; }
        [MaxLength(150)]
        public string ItemName { get; set; }
        [MaxLength(50)]
        public string PayType { get; set; }
        public decimal Price { get; set; }
        public decimal Costs { get; set; }
        public int Number { get; set; }
        public decimal TotalPrice { get; set; }
        [MaxLength(20)]
        public string Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public int BuyerAccountId { get; set; }

        public int TransactionId { get; set; }

        public int ItemId { get; set; }

    }
}
