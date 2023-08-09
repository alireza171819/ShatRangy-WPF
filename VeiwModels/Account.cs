using System.ComponentModel.DataAnnotations;

namespace VeiwModels
{
    public class Account
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(150)]
        public string AccountName { get; set; }
        [MaxLength(150)]
        public string GroupName { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// A varible for debt person
        /// </summary>
        public decimal Debt { get; set; }
        /// <summary>
        /// A varible for credit person
        /// </summary>
        public decimal Credit { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        /// <summary>
        /// A short summary and nots about person
        /// </summary>
        [MaxLength(150)]
        public string Note { get; set; }
    }
}
