using System.Data.Entity;
using VeiwModels;

namespace DataLayer
{
    public partial class ShatRangyContext : DbContext
    {
        public ShatRangyContext() : base ("name = constr") { }
        public virtual DbSet<Account> accounts { get; set; }
        public virtual DbSet<AccountGroup> accountGroups { get; set; }
        public virtual DbSet<Transaction> transactions { get; set; }
        public virtual DbSet<SerVice> services { get; set; }
        public virtual DbSet<BuyDocument> buyDocuments { get; set; }
        public virtual DbSet<SellDocument> sellDocuments { get; set; }
        public virtual DbSet<Setting> settings { get; set; }
        public virtual DbSet<Item> items { get; set; }
        public virtual DbSet<User> users { get; set; }


    }
}
