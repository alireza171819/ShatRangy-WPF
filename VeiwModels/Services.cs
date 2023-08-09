using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeiwModels
{
    public class SerVice
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(150)]
        public string DescriptionService { get; set; }
        [MaxLength(150)]
        public string CostomerName { get; set; }
        [MaxLength(150)]
        public string ItemName { get; set; }
        public decimal Comision { get; set; }
        public string PayType { get; set; }
        [MaxLength(10)]
        public string StartDate { get; set; }
        [MaxLength(10)]
        public string EndDate { get; set; }
        public int StartYear { get; set; } 
        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int EndYear { get; set; }
        public int EndMonth { get; set; }
        public int EndDay { get; set; }

        public int TransactionId { get; set; }
        public int ItemId { get; set; }
        public int CustomerAccountId { get; set; }
    }
}
