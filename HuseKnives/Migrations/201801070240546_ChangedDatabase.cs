namespace HuseKnives.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WeaponName = c.String(nullable: false, maxLength: 25),
                        WeaponDescription = c.String(maxLength: 255),
                        BladeSteel = c.String(nullable: false, maxLength: 25),
                        HandleMaterial = c.String(nullable: false, maxLength: 50),
                        RCHardness = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inventories", "ImageId", "dbo.Images");
            DropIndex("dbo.Inventories", new[] { "ImageId" });
            DropTable("dbo.Images");
            DropTable("dbo.Inventories");
        }
    }
}
