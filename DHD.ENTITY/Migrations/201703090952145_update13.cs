namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolNews", "HaveImg", c => c.Int(nullable: false));
            AddColumn("dbo.SchoolNews", "HasCatched", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolNews", "HasCatched");
            DropColumn("dbo.SchoolNews", "HaveImg");
        }
    }
}
