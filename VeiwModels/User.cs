using System.ComponentModel.DataAnnotations;

namespace VeiwModels
{
    public class User
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(18)]
        public string Password { get; set; }
        [MaxLength(150)]
        public string NameAndFamily { get; set; }
        [MaxLength(100)]
        public string BusinessName { get; set; }
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        /// <summary>
        /// Licence code
        /// </summary>
        [MaxLength(50)]
        public string ActiveCode { get; set; }
    }
}
