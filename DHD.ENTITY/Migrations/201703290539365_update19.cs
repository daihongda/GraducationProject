namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update19 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolNews", "ThubmUp", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolNews", "ThubmUp");
        }
    }
}
