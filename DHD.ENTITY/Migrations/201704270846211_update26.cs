namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchoolNews", "IsTop", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchoolNews", "IsTop");
        }
    }
}
