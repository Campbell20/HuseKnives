namespace HuseKnives.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeaponName = c.String(nullable: false),
                        WeaponDescription = c.String(),
                        BladeSteel = c.String(nullable: false),
                        HandleMaterial = c.String(nullable: false),
                        RCHardness = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inventories");
        }
    }
}
