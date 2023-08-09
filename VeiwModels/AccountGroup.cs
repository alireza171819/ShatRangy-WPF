using System.ComponentModel.DataAnnotations;

namespace VeiwModels
{
    public class AccountGroup
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string GroupName { get; set; }
    }
}
