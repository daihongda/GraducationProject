namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ThumbUp", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "ThumbDown", c => c.Int(nullable: false));
            DropColumn("dbo.SchoolNews", "GoodNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchoolNews", "GoodNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "ThumbDown");
            DropColumn("dbo.Comments", "ThumbUp");
        }
    }
}
