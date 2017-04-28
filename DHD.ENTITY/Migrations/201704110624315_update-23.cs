namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Areas", "IsDelete", c => c.Int(nullable: false));
            AddColumn("dbo.Provinces", "IsDelete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Provinces", "IsDelete");
            DropColumn("dbo.Areas", "IsDelete");
        }
    }
}
