namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountName = c.String(maxLength: 150),
                        GroupName = c.String(maxLength: 150),
                        PhoneNumber = c.String(maxLength: 15),
                        Debt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Address = c.String(maxLength: 250),
                        Note = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BuyDocuments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SellerName = c.String(maxLength: 150),
                        ItemName = c.String(maxLength: 150),
                        PayType = c.String(maxLength: 150),
                        Costs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.String(maxLength: 20),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        SellerAccountId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(maxLength: 150),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductionCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.Int(nullable: false),
                        Description = c.String(maxLength: 250),
                        Date = c.String(),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SellDocuments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BuyerName = c.String(maxLength: 150),
                        ItemName = c.String(maxLength: 150),
                        PayType = c.String(maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Costs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Number = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.String(maxLength: 20),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        BuyerAccountId = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SerVices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DescriptionService = c.String(maxLength: 150),
                        CostomerName = c.String(maxLength: 150),
                        ItemName = c.String(maxLength: 150),
                        Comision = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayType = c.String(),
                        StartDate = c.String(maxLength: 10),
                        EndDate = c.String(maxLength: 10),
                        StartYear = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        StartDay = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        EndDay = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        CustomerAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Value = c.String(maxLength: 150),
                        Active = c.Boolean(nullable: false),
                        Date = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountSideName = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Recived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.String(maxLength: 20),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        AccountSideId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 100),
                        Password = c.String(maxLength: 18),
                        NameAndFamily = c.String(maxLength: 150),
                        BusinessName = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 15),
                        Address = c.String(maxLength: 250),
                        ActiveCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Transactions");
            DropTable("dbo.Settings");
            DropTable("dbo.SerVices");
            DropTable("dbo.SellDocuments");
            DropTable("dbo.Items");
            DropTable("dbo.BuyDocuments");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountGroups");
        }
    }
}
