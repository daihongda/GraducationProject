namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolNews", "ThumbUp", c => c.Int(nullable: false));
            DropColumn("dbo.SchoolNews", "ThubmUp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchoolNews", "ThubmUp", c => c.Int(nullable: false));
            DropColumn("dbo.SchoolNews", "ThumbUp");
        }
    }
}
