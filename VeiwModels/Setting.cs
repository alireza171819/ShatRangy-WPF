using System.ComponentModel.DataAnnotations;

namespace VeiwModels
{
    public class Setting
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Value { get; set; }
        public bool Active { get; set; }
        [MaxLength(40)]
        public string Date { get; set; }
    }
}
