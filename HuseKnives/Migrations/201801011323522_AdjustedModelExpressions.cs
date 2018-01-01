namespace HuseKnives.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdjustedModelExpressions : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inventories", "WeaponName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Inventories", "WeaponDescription", c => c.String(maxLength: 255));
            AlterColumn("dbo.Inventories", "BladeSteel", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Inventories", "HandleMaterial", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Inventories", "HandleMaterial", c => c.String(nullable: false));
            AlterColumn("dbo.Inventories", "BladeSteel", c => c.String(nullable: false));
            AlterColumn("dbo.Inventories", "WeaponDescription", c => c.String());
            AlterColumn("dbo.Inventories", "WeaponName", c => c.String(nullable: false));
        }
    }
}
