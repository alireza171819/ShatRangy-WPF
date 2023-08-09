using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeiwModels
{
    public class Transaction
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string AccountSideName { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public decimal Payment { get; set; }
        public decimal Recived { get; set; }
        [MaxLength(20)]
        public string Date { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public int AccountSideId { get; set; }
    }
}
