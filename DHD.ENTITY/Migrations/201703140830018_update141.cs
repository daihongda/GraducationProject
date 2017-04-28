namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update141 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolNews", "ViewNumber", c => c.Int(nullable: false));
            AddColumn("dbo.SchoolNews", "GoodNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolNews", "GoodNumber");
            DropColumn("dbo.SchoolNews", "ViewNumber");
        }
    }
}
